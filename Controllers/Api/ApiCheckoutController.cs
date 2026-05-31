using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.Api;
using WebApplication1.Extensions;
using WebApplication1.Models;
using WebApplication1.Services.Checkout;

namespace WebApplication1.Controllers.Api;

[ApiController]
[Authorize(Roles = "Consumidor")]
[Route("api/checkout")]
[Produces("application/json")]
public sealed class ApiCheckoutController : ControllerBase
{
    private readonly ICheckoutFacade _checkoutFacade;

    public ApiCheckoutController(ICheckoutFacade checkoutFacade)
    {
        _checkoutFacade = checkoutFacade;
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
        var carrinho = ObterCarrinho();
        var result = await _checkoutFacade.FinalizarAsync(ObterEmail(), carrinho, request, cancellationToken);

        if (!result.Success || result.Pedido == null)
        {
            return BadRequest(new { mensagem = result.Message });
        }

        SalvarCarrinho(new List<CarrinhoItem>());

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

    private List<CarrinhoItem> ObterCarrinho()
    {
        return HttpContext.Session.GetObjectFromJson<List<CarrinhoItem>>($"Carrinho_{ObterEmail()}") ?? new List<CarrinhoItem>();
    }

    private void SalvarCarrinho(List<CarrinhoItem> carrinho)
    {
        HttpContext.Session.SetObjectAsJson($"Carrinho_{ObterEmail()}", carrinho);
    }
}

