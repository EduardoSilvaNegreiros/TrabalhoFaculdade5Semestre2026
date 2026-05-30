using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Lojista")]
    public class LojistaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LojistaController(ApplicationDbContext context)
        {
            _context = context;
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

        [HttpPost]
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

            return RedirectToAction(nameof(Dashboard));
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
