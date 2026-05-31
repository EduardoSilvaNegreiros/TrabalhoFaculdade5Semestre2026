using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.Api;
using WebApplication1.Services.AI;

namespace WebApplication1.Controllers.Api;

[ApiController]
[Route("api/ia")]
[Produces("application/json")]
public sealed class ApiIaController : ControllerBase
{
    private readonly IAiRecommendationServiceFactory _factory;

    public ApiIaController(IAiRecommendationServiceFactory factory)
    {
        _factory = factory;
    }

    /// <summary>
    /// Gera recomendações de produtos com IA ou fallback local.
    /// </summary>
    /// <remarks>
    /// Exemplo de request: { "tipoPele": "Oleosa", "tipoCabelo": "Cacheado", "objetivo": "hidratar", "vegano": true, "precoMax": 120 }
    /// </remarks>
    [HttpPost("recomendacoes")]
    [ProducesResponseType(typeof(AiRecommendationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AiRecommendationResponse>> Recomendar(AiRecommendationRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return BadRequest(new { mensagem = "Informe os critérios de recomendação." });
        }

        var service = _factory.Create();
        return Ok(await service.RecommendAsync(request, cancellationToken));
    }
}
