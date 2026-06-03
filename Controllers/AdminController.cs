using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Lojistas()
        {
            ViewBag.Comissoes = await _context.ComissoesCategoria.OrderBy(c => c.Categoria).ToListAsync();
            ViewBag.ProdutosPendentes = await _context.Produtos
                .Include(p => p.Lojista)
                .Where(p => p.StatusModeracao == ProdutoStatusModeracao.Pendente)
                .OrderBy(p => p.Nome)
                .ToListAsync();
            return View(await _context.Lojistas.OrderBy(l => l.Status).ThenBy(l => l.NomeFantasia).ToListAsync());
        }

        public IActionResult MapaAtendimento()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AlterarStatus(int id, string status)
        {
            var lojista = await _context.Lojistas.FindAsync(id);
            if (lojista == null)
            {
                return NotFound();
            }

            lojista.Status = status;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Lojistas));
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarComissao(int id, decimal percentual)
        {
            var comissao = await _context.ComissoesCategoria.FindAsync(id);
            if (comissao == null)
            {
                return NotFound();
            }

            comissao.Percentual = Math.Clamp(percentual, 0m, 100m);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Lojistas));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarStatusProduto(int id, string status)
        {
            if (!ProdutoStatusModeracao.Todos.Contains(status))
            {
                return BadRequest();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            produto.StatusModeracao = status;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Lojistas));
        }
    }
}
