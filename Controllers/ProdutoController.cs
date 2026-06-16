using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProdutoController : Controller
    {
        private static readonly HashSet<string> OrdenacoesPermitidas = new(StringComparer.OrdinalIgnoreCase)
        {
            "menor-preco",
            "maior-preco",
            "nome-desc",
            "estoque"
        };

        private readonly ApplicationDbContext _context;

        public ProdutoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
            string? busca,
            string? category,
            string? pele,
            string? cabelo,
            string? marca,
            string? lojista,
            string? tom,
            string? acabamento,
            bool? vegano,
            decimal? precoMin,
            decimal? precoMax,
            string? ordenacao)
        {
            if (!string.IsNullOrWhiteSpace(ordenacao) && !OrdenacoesPermitidas.Contains(ordenacao))
            {
                ordenacao = null;
            }

            var produtosAprovados = _context.Produtos.Where(p => p.StatusModeracao == ProdutoStatusModeracao.Aprovado);
            var query = produtosAprovados.Include(p => p.Lojista).AsQueryable();

            if (!string.IsNullOrWhiteSpace(busca))
            {
                var termo = busca.Trim();
                query = query.Where(p =>
                    p.Nome.Contains(termo) ||
                    p.Descricao.Contains(termo) ||
                    p.Marca.Contains(termo) ||
                    p.Categoria.Contains(termo) ||
                    p.Composicao.Contains(termo) ||
                    p.TipoPele.Contains(termo) ||
                    p.TipoCabelo.Contains(termo) ||
                    p.Tom.Contains(termo) ||
                    p.Acabamento.Contains(termo) ||
                    (p.Lojista != null && p.Lojista.NomeFantasia.Contains(termo)));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(p => p.Categoria == category);
            }

            if (!string.IsNullOrWhiteSpace(pele))
            {
                query = query.Where(p => p.TipoPele == pele || p.TipoPele == "Todos");
            }

            if (!string.IsNullOrWhiteSpace(cabelo))
            {
                query = query.Where(p => p.TipoCabelo == cabelo || p.TipoCabelo == "Todos");
            }

            if (!string.IsNullOrWhiteSpace(marca))
            {
                query = query.Where(p => p.Marca == marca);
            }

            if (!string.IsNullOrWhiteSpace(lojista))
            {
                query = query.Where(p => p.Lojista != null && p.Lojista.NomeFantasia == lojista);
            }

            if (!string.IsNullOrWhiteSpace(tom))
            {
                query = query.Where(p => p.Tom == tom || p.Tom == "Universal");
            }

            if (!string.IsNullOrWhiteSpace(acabamento))
            {
                query = query.Where(p => p.Acabamento == acabamento);
            }

            if (vegano == true)
            {
                query = query.Where(p => p.Vegano);
            }

            if (precoMin.HasValue && precoMax.HasValue && precoMin.Value > precoMax.Value)
            {
                ViewBag.FiltroErro = "O preço mínimo não pode ser maior que o preço máximo.";
            }
            else
            {
                if (precoMin.HasValue)
                {
                    query = query.Where(p => p.Preco >= precoMin.Value);
                }

                if (precoMax.HasValue)
                {
                    query = query.Where(p => p.Preco <= precoMax.Value);
                }
            }

            ViewBag.Categorias = await produtosAprovados
                .Select(p => p.Categoria)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
            ViewBag.Peles = await produtosAprovados
                .Select(p => p.TipoPele)
                .Where(v => v != "Todos" && !string.IsNullOrWhiteSpace(v))
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
            ViewBag.Cabelos = await produtosAprovados
                .Select(p => p.TipoCabelo)
                .Where(v => v != "Todos" && !string.IsNullOrWhiteSpace(v))
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
            ViewBag.Marcas = await produtosAprovados
                .Select(p => p.Marca)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
            ViewBag.Lojistas = await produtosAprovados
                .Include(p => p.Lojista)
                .Where(p => p.Lojista != null && !string.IsNullOrWhiteSpace(p.Lojista!.NomeFantasia))
                .Select(p => p.Lojista!.NomeFantasia)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
            ViewBag.Tons = await produtosAprovados
                .Select(p => p.Tom)
                .Where(v => v != "Universal" && !string.IsNullOrWhiteSpace(v))
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
            ViewBag.Acabamentos = await produtosAprovados
                .Select(p => p.Acabamento)
                .Where(a => !string.IsNullOrWhiteSpace(a))
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();
            ViewBag.TotalProdutos = await query.CountAsync();

            query = ordenacao switch
            {
                "menor-preco" => query.OrderBy(p => p.Preco).ThenBy(p => p.Nome),
                "maior-preco" => query.OrderByDescending(p => p.Preco).ThenBy(p => p.Nome),
                "nome-desc" => query.OrderByDescending(p => p.Nome),
                "estoque" => query.OrderByDescending(p => p.Estoque).ThenBy(p => p.Nome),
                _ => query.OrderBy(p => p.Nome)
            };

            return View(await query.ToListAsync());
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var produto = await _context.Produtos
                .Include(p => p.Lojista)
                .FirstOrDefaultAsync(p => p.Id == id && p.StatusModeracao == ProdutoStatusModeracao.Aprovado);

            if (produto == null)
            {
                return NotFound();
            }

            ViewBag.Avaliacoes = await _context.Avaliacoes
                .Where(a => a.ProdutoId == id)
                .OrderByDescending(a => a.CriadoEm)
                .ToListAsync();

            ViewBag.Recomendados = await _context.Produtos
                .Where(p => p.Id != id &&
                    p.StatusModeracao == ProdutoStatusModeracao.Aprovado &&
                    (p.Categoria == produto.Categoria || p.TipoPele == produto.TipoPele || p.TipoCabelo == produto.TipoCabelo))
                .Take(4)
                .ToListAsync();

            return View(produto);
        }
    }
}
