using WebApplication1.Models;

namespace WebApplication1.Services.Recommendations;

public sealed class CategoryRecommendationStrategy : IProductRecommendationStrategy
{
    public string Nome => "Categoria de interesse";

    public int Score(Produto produto, ProductRecommendationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Categoria))
        {
            return 0;
        }

        return produto.Categoria.Equals(request.Categoria, StringComparison.OrdinalIgnoreCase) ? 30 : 0;
    }
}

