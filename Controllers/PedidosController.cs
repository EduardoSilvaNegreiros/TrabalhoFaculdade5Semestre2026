using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Consumidor")]
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name ?? string.Empty;
            var pedidos = await _context.Pedidos
                .Include(p => p.Itens)
                .Where(p => p.UsuarioEmail == email || string.IsNullOrEmpty(email))
                .OrderByDescending(p => p.CriadoEm)
                .ToListAsync();

            return View(pedidos);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name ?? string.Empty;
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == id && p.UsuarioEmail == email);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }
    }
}
