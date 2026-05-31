using WebApplication1.Models;

namespace WebApplication1.Services.Recommendations;

public interface IProductRecommendationService
{
    Task<IReadOnlyList<Produto>> RecommendAsync(ProductRecommendationRequest request, int limit, CancellationToken cancellationToken);
}

