using WebApplication1.Dtos.Api;
using WebApplication1.Services.Recommendations;

namespace WebApplication1.Services.AI;

public sealed class LocalAiRecommendationService : IAiRecommendationService
{
    private readonly IProductRecommendationService _recommendationService;

    public LocalAiRecommendationService(IProductRecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }

    public async Task<AiRecommendationResponse> RecommendAsync(AiRecommendationRequest request, CancellationToken cancellationToken)
    {
        var produtos = await _recommendationService.RecommendAsync(
            new ProductRecommendationRequest(request.TipoPele, request.TipoCabelo, request.Categoria, request.Vegano, request.PrecoMax, request.Objetivo),
            5,
            cancellationToken);

        var itens = produtos.Select(produto => new AiRecommendationProductResponse(
            produto.Id,
            produto.Nome,
            produto.Categoria,
            produto.Marca,
            produto.Preco,
            CriarMotivo(produto.TipoPele, produto.TipoCabelo, request))).ToList();

        return new AiRecommendationResponse(
            "Local demonstrativo",
            "Recomendacao gerada por regras locais que simulam a camada de IA para apresentacao sem chave externa.",
            "As sugestoes nao substituem avaliacao dermatologica ou profissional.",
            itens);
    }

    private static string CriarMotivo(string tipoPeleProduto, string tipoCabeloProduto, AiRecommendationRequest request)
    {
        var motivos = new List<string>();

        if (!string.IsNullOrWhiteSpace(request.TipoPele))
        {
            motivos.Add($"compatibilidade com pele {request.TipoPele}");
        }

        if (!string.IsNullOrWhiteSpace(request.TipoCabelo))
        {
            motivos.Add($"compatibilidade com cabelo {request.TipoCabelo}");
        }

        if (request.Vegano == true)
        {
            motivos.Add("preferencia por produtos veganos");
        }

        return motivos.Any()
            ? $"Produto sugerido por {string.Join(", ", motivos)}."
            : $"Produto versatil para pele {tipoPeleProduto} e cabelo {tipoCabeloProduto}.";
    }
}

