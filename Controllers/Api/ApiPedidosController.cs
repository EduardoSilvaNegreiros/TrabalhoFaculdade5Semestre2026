using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos.Api;

namespace WebApplication1.Controllers.Api;

[ApiController]
[Authorize(Roles = "Consumidor")]
[Route("api/pedidos")]
[Produces("application/json")]
public sealed class ApiPedidosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ApiPedidosController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Lista os pedidos do consumidor logado.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PedidoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<PedidoResponse>>> Listar(CancellationToken cancellationToken)
    {
        var email = ObterEmail();
        var pedidos = await _context.Pedidos
            .Include(p => p.Itens)
            .Where(p => p.UsuarioEmail == email)
            .OrderByDescending(p => p.CriadoEm)
            .ToListAsync(cancellationToken);

        return Ok(pedidos.Select(p => p.ToResponse()));
    }

    /// <summary>
    /// Consulta um pedido especifico do consumidor logado.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PedidoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PedidoResponse>> ObterPorId(int id, CancellationToken cancellationToken)
    {
        var email = ObterEmail();
        var pedido = await _context.Pedidos
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id && p.UsuarioEmail == email, cancellationToken);

        return pedido == null ? NotFound() : Ok(pedido.ToResponse());
    }

    private string ObterEmail()
    {
        return User.FindFirstValue(ClaimTypes.Email) ?? User.Identity?.Name ?? string.Empty;
    }
}

