using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Consumidor")]
    public class ConsumidorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsumidorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name ?? string.Empty;
            ViewBag.TotalPedidos = await _context.Pedidos.CountAsync(p => p.UsuarioEmail == email);
            ViewBag.TotalDesejos = await _context.ListaDesejosItens.CountAsync(i => i.UsuarioEmail == email);
            return View();
        }

        public IActionResult RecomendacaoIa()
        {
            return View();
        }
    }
}
