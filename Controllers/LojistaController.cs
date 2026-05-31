using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Services.ProductImages;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Lojista")]
    public class LojistaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductImageService _productImageService;

        public LojistaController(ApplicationDbContext context, IProductImageService productImageService)
        {
            _context = context;
            _productImageService = productImageService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var lojista = await ObterLojistaLogadoAsync();
            if (lojista == null)
            {
                return NotFound();
            }

            var itensVendidos = await _context.PedidoItens
                .Where(i => i.LojistaId == lojista.Id)
                .OrderByDescending(i => i.Id)
                .ToListAsync();

            ViewBag.ItensVendidos = itensVendidos;
            ViewBag.Notificacoes = itensVendidos
                .Where(i => i.StatusEntrega.StartsWith("Separ", StringComparison.OrdinalIgnoreCase))
                .Take(5)
                .ToList();

            return View(lojista);
        }

        public async Task<IActionResult> NovoProduto()
        {
            var lojista = await ObterLojistaLogadoAsync();
            if (lojista == null)
            {
                return NotFound();
            }

            await PrepararFormularioAsync();
            return View("ProdutoForm", new ProdutoFormViewModel
            {
                Preco = 49.90m,
                Estoque = 10,
                Categoria = "Cuidados com a Pele",
                TipoPele = "Todos",
                TipoCabelo = "Todos",
                CurvaturaCachos = "Todos",
                Tom = "Universal",
                Acabamento = "Natural"
            });
        }

        public async Task<IActionResult> EditarProduto(int id)
        {
            var lojista = await ObterLojistaLogadoAsync();
            if (lojista == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id && p.LojistaId == lojista.Id);
            if (produto == null)
            {
                return NotFound();
            }

            await PrepararFormularioAsync();
            return View("ProdutoForm", MapearProdutoForm(produto));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarProduto(ProdutoFormViewModel model, CancellationToken cancellationToken)
        {
            var lojista = await ObterLojistaLogadoAsync();
            if (lojista == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await PrepararFormularioAsync();
                return View("ProdutoForm", model);
            }

            var produto = model.Id.HasValue
                ? await _context.Produtos.FirstOrDefaultAsync(p => p.Id == model.Id.Value && p.LojistaId == lojista.Id, cancellationToken)
                : new Produto { LojistaId = lojista.Id };

            if (produto == null)
            {
                return NotFound();
            }

            produto.Slug = await CriarSlugUnicoAsync(model.Nome, model.Id, cancellationToken);

            if (model.Imagem != null)
            {
                var imageResult = await _productImageService.SaveAsync(model.Imagem, lojista.Id, produto.Slug, cancellationToken);
                if (!imageResult.Success)
                {
                    ModelState.AddModelError(nameof(model.Imagem), imageResult.ErrorMessage ?? "Não foi possível salvar a imagem.");
                    await PrepararFormularioAsync();
                    return View("ProdutoForm", model);
                }

                produto.ImagemUrl = imageResult.Url ?? produto.ImagemUrl;
            }
            else if (string.IsNullOrWhiteSpace(produto.ImagemUrl))
            {
                produto.ImagemUrl = ObterImagemFallback(model.Categoria);
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Nome == model.Categoria, cancellationToken);

            produto.Nome = model.Nome.Trim();
            produto.Descricao = model.Descricao.Trim();
            produto.Categoria = model.Categoria;
            produto.CategoriaId = categoria?.Id;
            produto.Marca = model.Marca.Trim();
            produto.Preco = model.Preco;
            produto.Estoque = model.Estoque;
            produto.TipoPele = model.TipoPele;
            produto.TipoCabelo = model.TipoCabelo;
            produto.CurvaturaCachos = string.IsNullOrWhiteSpace(model.CurvaturaCachos) ? "Todos" : model.CurvaturaCachos;
            produto.Tom = model.Tom;
            produto.Acabamento = model.Acabamento;
            produto.Vegano = model.Vegano;
            produto.Composicao = model.Composicao.Trim();

            if (!model.Id.HasValue)
            {
                _context.Produtos.Add(produto);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Redirect($"{Url.Action(nameof(Dashboard))}#estoque");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarEstoque(int produtoId, int estoque)
        {
            var lojista = await ObterLojistaLogadoAsync();
            if (lojista == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == produtoId && p.LojistaId == lojista.Id);
            if (produto == null)
            {
                return NotFound();
            }

            produto.Estoque = Math.Max(0, estoque);
            await _context.SaveChangesAsync();

            return Redirect($"{Url.Action(nameof(Dashboard))}#estoque");
        }

        private async Task PrepararFormularioAsync()
        {
            ViewBag.Categorias = await _context.Categorias.OrderBy(c => c.Nome).Select(c => c.Nome).ToListAsync();
            ViewBag.Peles = new[] { "Todos", "Oleosa", "Seca", "Mista", "Sensível" };
            ViewBag.Cabelos = new[] { "Todos", "Cacheado", "Crespo", "Liso", "Ondulado", "Danificado", "Frágil" };
            ViewBag.Curvaturas = new[] { "Todos", "2A-2C", "3A-3C", "4A-4C" };
            ViewBag.Tons = new[] { "Universal", "Claro", "Médio", "Escuro", "Preto" };
            ViewBag.Acabamentos = new[] { "Natural", "Matte", "Glow", "Suave", "Hidratante", "Brilho", "Definição", "Nutritivo", "Toque seco", "Cremoso", "Volume" };
        }

        private ProdutoFormViewModel MapearProdutoForm(Produto produto)
        {
            return new ProdutoFormViewModel
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Categoria = produto.Categoria,
                Marca = produto.Marca,
                Preco = produto.Preco,
                Estoque = produto.Estoque,
                TipoPele = produto.TipoPele,
                TipoCabelo = produto.TipoCabelo,
                CurvaturaCachos = produto.CurvaturaCachos,
                Tom = produto.Tom,
                Acabamento = produto.Acabamento,
                Vegano = produto.Vegano,
                Composicao = produto.Composicao,
                ImagemUrlAtual = produto.ImagemUrl
            };
        }

        private async Task<string> CriarSlugUnicoAsync(string nome, int? produtoId, CancellationToken cancellationToken)
        {
            var baseSlug = SlugHelper.Generate(nome);
            var slug = baseSlug;
            var index = 2;

            while (await _context.Produtos.AnyAsync(p => p.Slug == slug && (!produtoId.HasValue || p.Id != produtoId.Value), cancellationToken))
            {
                slug = $"{baseSlug}-{index++}";
            }

            return slug;
        }

        private static string ObterImagemFallback(string categoria)
        {
            return $"/images/produtos/seed/fallback-{SlugHelper.Generate(categoria)}.png";
        }

        private async Task<Lojista?> ObterLojistaLogadoAsync()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name ?? string.Empty;
            return await _context.Lojistas
                .Include(l => l.Produtos)
                .FirstOrDefaultAsync(l => l.Email == email);
        }
    }
}
