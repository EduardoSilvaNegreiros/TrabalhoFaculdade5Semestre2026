using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.Api;
using WebApplication1.Services.Cart;
using WebApplication1.Services.Checkout;

namespace WebApplication1.Controllers.Api;

[ApiController]
[Authorize(Roles = "Consumidor")]
[Route("api/checkout")]
[Produces("application/json")]
public sealed class ApiCheckoutController : ControllerBase
{
    private readonly ICheckoutFacade _checkoutFacade;
    private readonly ICartService _cartService;

    public ApiCheckoutController(ICheckoutFacade checkoutFacade, ICartService cartService)
    {
        _checkoutFacade = checkoutFacade;
        _cartService = cartService;
    }

    /// <summary>
    /// Finaliza a compra do carrinho atual.
    /// </summary>
    /// <remarks>
    /// Exemplo de request: { "metodoPagamento": "Pix", "cep": "01001000" }
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(CheckoutResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CheckoutResponse>> Finalizar(CheckoutRequest request, CancellationToken cancellationToken)
    {
        var email = ObterEmail();
        var carrinho = await _cartService.GetAsync(HttpContext, email, cancellationToken);
        var result = await _checkoutFacade.FinalizarAsync(email, carrinho, request, cancellationToken);

        if (!result.Success || result.Pedido == null)
        {
            return BadRequest(new { mensagem = result.Message });
        }

        await _cartService.ClearAsync(HttpContext, email, cancellationToken);

        var response = new CheckoutResponse(
            result.Pedido.Id,
            result.Pedido.Status,
            result.Pedido.Subtotal,
            result.Pedido.Frete,
            result.Pedido.Total,
            result.Prazo,
            result.Pedido.Itens.Select(i => i.ToResponse()).ToList());

        return CreatedAtAction("ObterPorId", "ApiPedidos", new { id = result.Pedido.Id }, response);
    }

    private string ObterEmail()
    {
        return User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name ?? "cliente@beautymarket.com";
    }
}
