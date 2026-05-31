using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services.Recommendations;

public sealed class ProductRecommendationService : IProductRecommendationService
{
    private readonly ApplicationDbContext _context;
    private readonly IEnumerable<IProductRecommendationStrategy> _strategies;

    public ProductRecommendationService(ApplicationDbContext context, IEnumerable<IProductRecommendationStrategy> strategies)
    {
        _context = context;
        _strategies = strategies;
    }

    public async Task<IReadOnlyList<Produto>> RecommendAsync(ProductRecommendationRequest request, int limit, CancellationToken cancellationToken)
    {
        var produtos = await _context.Produtos
            .Include(p => p.Lojista)
            .Where(p => p.Estoque > 0)
            .ToListAsync(cancellationToken);

        return produtos
            .Select(produto => new
            {
                Produto = produto,
                Score = _strategies.Sum(strategy => strategy.Score(produto, request))
            })
            .OrderByDescending(item => item.Score)
            .ThenByDescending(item => item.Produto.Estoque)
            .ThenBy(item => item.Produto.Preco)
            .Take(Math.Max(1, limit))
            .Select(item => item.Produto)
            .ToList();
    }
}

