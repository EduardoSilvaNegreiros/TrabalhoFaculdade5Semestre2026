using WebApplication1.Models;

namespace WebApplication1.Services.Recommendations;

public sealed class SkinHairRecommendationStrategy : IProductRecommendationStrategy
{
    public string Nome => "Perfil de pele e cabelo";

    public int Score(Produto produto, ProductRecommendationRequest request)
    {
        var score = 0;

        if (Matches(request.TipoPele, produto.TipoPele, "Todos"))
        {
            score += 40;
        }

        if (Matches(request.TipoCabelo, produto.TipoCabelo, "Todos"))
        {
            score += 35;
        }

        if (!string.IsNullOrWhiteSpace(request.Objetivo) &&
            (produto.Descricao.Contains(request.Objetivo, StringComparison.OrdinalIgnoreCase) ||
             produto.Composicao.Contains(request.Objetivo, StringComparison.OrdinalIgnoreCase)))
        {
            score += 15;
        }

        return score;
    }

    private static bool Matches(string? requested, string actual, string universal)
    {
        return string.IsNullOrWhiteSpace(requested) ||
               actual.Equals(requested, StringComparison.OrdinalIgnoreCase) ||
               actual.Equals(universal, StringComparison.OrdinalIgnoreCase);
    }
}

