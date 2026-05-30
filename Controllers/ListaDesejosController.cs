using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Consumidor")]
    public class ListaDesejosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListaDesejosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var email = ObterEmail();
            var itens = await _context.ListaDesejosItens
                .Include(i => i.Produto)
                .ThenInclude(p => p!.Lojista)
                .Where(i => i.UsuarioEmail == email)
                .OrderByDescending(i => i.CriadoEm)
                .ToListAsync();

            return View(itens);
        }

        [HttpPost]
        public async Task<IActionResult> Alternar(int produtoId)
        {
            var email = ObterEmail();
            var existente = await _context.ListaDesejosItens.FirstOrDefaultAsync(i => i.UsuarioEmail == email && i.ProdutoId == produtoId);

            if (existente == null)
            {
                _context.ListaDesejosItens.Add(new ListaDesejosItem { UsuarioEmail = email, ProdutoId = produtoId });
            }
            else
            {
                _context.ListaDesejosItens.Remove(existente);
            }

            await _context.SaveChangesAsync();
            return Redirect(Request.Headers.Referer.ToString() ?? Url.Action("Index", "Produto")!);
        }

        private string ObterEmail()
        {
            return User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name ?? "cliente@beautymarket.com";
        }
    }
}
