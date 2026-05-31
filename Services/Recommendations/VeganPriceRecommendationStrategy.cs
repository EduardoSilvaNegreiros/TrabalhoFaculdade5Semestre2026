using WebApplication1.Models;

namespace WebApplication1.Services.Recommendations;

public sealed class VeganPriceRecommendationStrategy : IProductRecommendationStrategy
{
    public string Nome => "Preferencia vegana e preco";

    public int Score(Produto produto, ProductRecommendationRequest request)
    {
        var score = 0;

        if (request.Vegano == true && produto.Vegano)
        {
            score += 25;
        }

        if (request.PrecoMax.HasValue && produto.Preco <= request.PrecoMax.Value)
        {
            score += 20;
        }

        return score;
    }
}

