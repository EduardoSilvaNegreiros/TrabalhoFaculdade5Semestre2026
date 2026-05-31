using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dtos.Api;
using WebApplication1.Services.Recommendations;

namespace WebApplication1.Controllers.Api;

[ApiController]
[Route("api/produtos")]
[Produces("application/json")]
public sealed class ApiProdutosController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IProductRecommendationService _recommendationService;

    public ApiProdutosController(ApplicationDbContext context, IProductRecommendationService recommendationService)
    {
        _context = context;
        _recommendationService = recommendationService;
    }

    /// <summary>
    /// Lista produtos do catalogo com filtros de beleza.
    /// </summary>
    /// <remarks>
    /// Exemplo: GET /api/produtos?pele=Oleosa&amp;vegano=true&amp;precoMax=120
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProdutoResumoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProdutoResumoResponse>>> Listar(
        [FromQuery] string? busca,
        [FromQuery] string? categoria,
        [FromQuery] string? pele,
        [FromQuery] string? cabelo,
        [FromQuery] string? marca,
        [FromQuery] bool? vegano,
        [FromQuery] decimal? precoMax,
        CancellationToken cancellationToken)
    {
        var query = _context.Produtos.Include(p => p.Lojista).AsQueryable();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            query = query.Where(p =>
                p.Nome.Contains(busca) ||
                p.Descricao.Contains(busca) ||
                p.Marca.Contains(busca) ||
                p.Categoria.Contains(busca));
        }

        if (!string.IsNullOrWhiteSpace(categoria))
        {
            query = query.Where(p => p.Categoria == categoria);
        }

        if (!string.IsNullOrWhiteSpace(pele))
        {
            query = query.Where(p => p.TipoPele == pele || p.TipoPele == "Todos");
        }

        if (!string.IsNullOrWhiteSpace(cabelo))
        {
            query = query.Where(p => p.TipoCabelo == cabelo || p.TipoCabelo == "Todos");
        }

        if (!string.IsNullOrWhiteSpace(marca))
        {
            query = query.Where(p => p.Marca == marca);
        }

        if (vegano == true)
        {
            query = query.Where(p => p.Vegano);
        }

        if (precoMax.HasValue)
        {
            query = query.Where(p => p.Preco <= precoMax.Value);
        }

        var produtos = await query
            .OrderBy(p => p.Nome)
            .Take(100)
            .ToListAsync(cancellationToken);

        return Ok(produtos.Select(p => p.ToResumoResponse()));
    }

    /// <summary>
    /// Consulta os detalhes de um produto.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProdutoDetalheResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProdutoDetalheResponse>> ObterPorId(int id, CancellationToken cancellationToken)
    {
        var produto = await _context.Produtos
            .Include(p => p.Lojista)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        return produto == null ? NotFound() : Ok(produto.ToDetalheResponse());
    }

    /// <summary>
    /// Lista avaliacoes de um produto.
    /// </summary>
    [HttpGet("{id:int}/avaliacoes")]
    [ProducesResponseType(typeof(IEnumerable<AvaliacaoResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AvaliacaoResponse>>> ListarAvaliacoes(int id, CancellationToken cancellationToken)
    {
        var avaliacoes = await _context.Avaliacoes
            .Where(a => a.ProdutoId == id)
            .OrderByDescending(a => a.CriadoEm)
            .ToListAsync(cancellationToken);

        return Ok(avaliacoes.Select(a => a.ToResponse()));
    }

    /// <summary>
    /// Recomenda produtos relacionados ao item informado.
    /// </summary>
    [HttpGet("{id:int}/recomendacoes")]
    [ProducesResponseType(typeof(IEnumerable<ProdutoRecomendadoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProdutoRecomendadoResponse>>> RecomendarPorProduto(int id, CancellationToken cancellationToken)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (produto == null)
        {
            return NotFound();
        }

        var recomendados = await _recommendationService.RecommendAsync(
            new ProductRecommendationRequest(produto.TipoPele, produto.TipoCabelo, produto.Categoria, produto.Vegano, null, produto.Categoria),
            4,
            cancellationToken);

        return Ok(recomendados
            .Where(p => p.Id != id)
            .Select(p => new ProdutoRecomendadoResponse(p.Id, p.Nome, p.Categoria, p.Marca, p.Preco, "Produto relacionado por perfil de beleza e categoria.")));
    }
}

