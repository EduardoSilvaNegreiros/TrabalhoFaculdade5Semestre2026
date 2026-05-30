using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

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
            return Redirect($"{Url.Action(nameof(Lojistas))}#comissoes");
        }
    }
}
