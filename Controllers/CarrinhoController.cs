using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos.Api;
using WebApplication1.Models;
using WebApplication1.Services.Cart;
using WebApplication1.Services.Checkout;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Consumidor")]
    public class CarrinhoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;
        private readonly ICheckoutFacade _checkoutFacade;

        public CarrinhoController(ApplicationDbContext context, ICartService cartService, ICheckoutFacade checkoutFacade)
        {
            _context = context;
            _cartService = cartService;
            _checkoutFacade = checkoutFacade;
        }

        [HttpPost("Carrinho/Adicionar/{id}")]
        public async Task<IActionResult> Adicionar(int id, CancellationToken cancellationToken)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Unauthorized();
            }

            var result = await _cartService.AddAsync(HttpContext, ObterEmail(), id, 1, cancellationToken);
            var response = new
            {
                sucesso = result.Success,
                mensagem = result.Message,
                quantidade = result.Items.Sum(p => p.Quantidade)
            };

            return result.Success ? Json(response) : StatusCode(result.StatusCode, response);
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("AvisoLogin");
            }

            var carrinho = await _cartService.GetAsync(HttpContext, ObterEmail(), cancellationToken);
            ViewBag.FreteEstimado = _checkoutFacade.CalcularFrete(carrinho, "01001000");
            ViewBag.CarrinhoAbandonado = carrinho.Any();
            return View(carrinho);
        }

        [HttpPost("Carrinho/Atualizar/{id}")]
        public async Task<IActionResult> Atualizar(int id, int quantidade, CancellationToken cancellationToken)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Unauthorized();
            }

            await _cartService.UpdateAsync(HttpContext, ObterEmail(), id, quantidade, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("Carrinho/Remover/{id}")]
        public async Task<IActionResult> Remover(int id, CancellationToken cancellationToken)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Unauthorized();
            }

            await _cartService.RemoveAsync(HttpContext, ObterEmail(), id, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ObterQuantidade(CancellationToken cancellationToken)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return Json(new { quantidade = 0 });
            }

            var carrinho = await _cartService.GetAsync(HttpContext, ObterEmail(), cancellationToken);
            return Json(new { quantidade = carrinho.Sum(p => p.Quantidade) });
        }

        [AllowAnonymous]
        public IActionResult AvisoLogin()
        {
            return View();
        }

        public async Task<IActionResult> EscolherPagamento(string cep = "01001000", CancellationToken cancellationToken = default)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("AvisoLogin");
            }

            var carrinho = await _cartService.GetAsync(HttpContext, ObterEmail(), cancellationToken);
            ViewBag.Cep = cep;
            ViewBag.Frete = _checkoutFacade.CalcularFrete(carrinho, cep);
            ViewBag.Prazo = _checkoutFacade.CalcularPrazo(carrinho);
            return View(carrinho);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarCompra(string pagamento, string cep, CancellationToken cancellationToken)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("AvisoLogin");
            }

            var email = ObterEmail();
            var carrinho = await _cartService.GetAsync(HttpContext, email, cancellationToken);
            var result = await _checkoutFacade.FinalizarAsync(email, carrinho, new CheckoutRequest(pagamento, cep), cancellationToken);
            if (!result.Success || result.Pedido == null)
            {
                TempData["ErroCarrinho"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            await _cartService.ClearAsync(HttpContext, email, cancellationToken);
            TempData["PedidoId"] = result.Pedido.Id;

            return RedirectToAction(nameof(CompraConcluida));
        }

        public async Task<IActionResult> CompraConcluida()
        {
            var pedidoId = TempData["PedidoId"] as int?;
            if (pedidoId == null)
            {
                return View(null);
            }

            var pedido = await _context.Pedidos.Include(p => p.Itens).FirstOrDefaultAsync(p => p.Id == pedidoId);
            return View(pedido);
        }

        private string ObterEmail()
        {
            return User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name ?? "visitante@beautymarket.com";
        }
    }
}
