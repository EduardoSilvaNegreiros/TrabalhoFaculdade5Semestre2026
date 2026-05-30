using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProdutoController : Controller
    {
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
            var query = _context.Produtos.Include(p => p.Lojista).AsQueryable();

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

            if (precoMin.HasValue)
            {
                query = query.Where(p => p.Preco >= precoMin.Value);
            }

            if (precoMax.HasValue)
            {
                query = query.Where(p => p.Preco <= precoMax.Value);
            }

            ViewBag.Categorias = await _context.Produtos.Select(p => p.Categoria).Distinct().OrderBy(c => c).ToListAsync();
            ViewBag.Peles = await _context.Produtos.Select(p => p.TipoPele).Where(v => v != "Todos").Distinct().OrderBy(c => c).ToListAsync();
            ViewBag.Cabelos = await _context.Produtos.Select(p => p.TipoCabelo).Where(v => v != "Todos").Distinct().OrderBy(c => c).ToListAsync();
            ViewBag.Marcas = await _context.Produtos.Select(p => p.Marca).Distinct().OrderBy(c => c).ToListAsync();
            ViewBag.Lojistas = await _context.Produtos.Include(p => p.Lojista).Where(p => p.Lojista != null).Select(p => p.Lojista!.NomeFantasia).Distinct().OrderBy(c => c).ToListAsync();
            ViewBag.Tons = await _context.Produtos.Select(p => p.Tom).Where(v => v != "Universal").Distinct().OrderBy(c => c).ToListAsync();
            ViewBag.Acabamentos = await _context.Produtos.Select(p => p.Acabamento).Distinct().OrderBy(c => c).ToListAsync();
            ViewBag.TotalProdutos = await query.CountAsync();

            var produtos = await query.ToListAsync();
            produtos = ordenacao switch
            {
                "menor-preco" => produtos.OrderBy(p => p.Preco).ToList(),
                "maior-preco" => produtos.OrderByDescending(p => p.Preco).ToList(),
                "nome-desc" => produtos.OrderByDescending(p => p.Nome).ToList(),
                "estoque" => produtos.OrderByDescending(p => p.Estoque).ToList(),
                _ => produtos.OrderBy(p => p.Nome).ToList()
            };

            return View(produtos);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var produto = await _context.Produtos
                .Include(p => p.Lojista)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
            {
                return NotFound();
            }

            ViewBag.Avaliacoes = await _context.Avaliacoes
                .Where(a => a.ProdutoId == id)
                .OrderByDescending(a => a.CriadoEm)
                .ToListAsync();

            ViewBag.Recomendados = await _context.Produtos
                .Where(p => p.Id != id && (p.Categoria == produto.Categoria || p.TipoPele == produto.TipoPele || p.TipoCabelo == produto.TipoCabelo))
                .Take(4)
                .ToListAsync();

            return View(produto);
        }
    }
}
