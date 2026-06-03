using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.Api;
using WebApplication1.Models;
using WebApplication1.Services.Cart;
using WebApplication1.Services.Checkout;

namespace WebApplication1.Controllers.Api;

[ApiController]
[Authorize(Roles = "Consumidor")]
[Route("api/carrinho")]
[Produces("application/json")]
public sealed class ApiCarrinhoController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ICheckoutFacade _checkoutFacade;

    public ApiCarrinhoController(ICartService cartService, ICheckoutFacade checkoutFacade)
    {
        _cartService = cartService;
        _checkoutFacade = checkoutFacade;
    }

    /// <summary>
    /// Retorna o carrinho do consumidor logado.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(CarrinhoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CarrinhoResponse>> Obter(CancellationToken cancellationToken)
    {
        var carrinho = await _cartService.GetAsync(HttpContext, ObterEmail(), cancellationToken);
        return Ok(CriarResposta(carrinho));
    }

    /// <summary>
    /// Adiciona um item ao carrinho.
    /// </summary>
    /// <remarks>
    /// Exemplo de request: { "produtoId": 1, "quantidade": 2 }
    /// </remarks>
    [HttpPost("itens")]
    [ProducesResponseType(typeof(CarrinhoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarrinhoResponse>> Adicionar(CarrinhoAdicionarRequest request, CancellationToken cancellationToken)
    {
        var result = await _cartService.AddAsync(HttpContext, ObterEmail(), request.ProdutoId, request.Quantidade, cancellationToken);
        if (!result.Success)
        {
            return StatusCode(result.StatusCode, new { mensagem = result.Message });
        }

        return Ok(CriarResposta(result.Items));
    }

    /// <summary>
    /// Atualiza a quantidade de um item no carrinho.
    /// </summary>
    [HttpPut("itens/{produtoId:int}")]
    [ProducesResponseType(typeof(CarrinhoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarrinhoResponse>> Atualizar(int produtoId, CarrinhoAtualizarRequest request, CancellationToken cancellationToken)
    {
        var result = await _cartService.UpdateAsync(HttpContext, ObterEmail(), produtoId, request.Quantidade, cancellationToken);
        if (!result.Success)
        {
            return StatusCode(result.StatusCode, new { mensagem = result.Message });
        }

        return Ok(CriarResposta(result.Items));
    }

    /// <summary>
    /// Remove um item do carrinho.
    /// </summary>
    [HttpDelete("itens/{produtoId:int}")]
    [ProducesResponseType(typeof(CarrinhoResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<CarrinhoResponse>> Remover(int produtoId, CancellationToken cancellationToken)
    {
        var carrinho = await _cartService.RemoveAsync(HttpContext, ObterEmail(), produtoId, cancellationToken);
        return Ok(CriarResposta(carrinho));
    }

    private CarrinhoResponse CriarResposta(List<CarrinhoItem> carrinho)
    {
        var frete = _checkoutFacade.CalcularFrete(carrinho, "01001000");
        var subtotal = carrinho.Sum(i => i.Subtotal);

        return new CarrinhoResponse(
            carrinho.Select(i => i.ToResponse()).ToList(),
            carrinho.Sum(i => i.Quantidade),
            subtotal,
            frete,
            subtotal + frete);
    }

    private string ObterEmail()
    {
        return User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name ?? "cliente@beautymarket.com";
    }
}
