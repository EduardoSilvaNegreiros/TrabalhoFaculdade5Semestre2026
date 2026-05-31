using WebApplication1.Models;

namespace WebApplication1.Services.Recommendations;

public interface IProductRecommendationStrategy
{
    string Nome { get; }
    int Score(Produto produto, ProductRecommendationRequest request);
}

