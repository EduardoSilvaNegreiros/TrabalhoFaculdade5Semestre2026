namespace WebApplication1.Dtos.Api;

public sealed record AiRecommendationRequest(
    string? TipoPele,
    string? TipoCabelo,
    string? Objetivo,
    string? Categoria,
    bool? Vegano,
    decimal? PrecoMax);

public sealed record AiRecommendationProductResponse(
    int ProdutoId,
    string Nome,
    string Categoria,
    string Marca,
    decimal Preco,
    string ImagemUrl,
    string DetalhesUrl,
    string Motivo);

public sealed record AiRecommendationResponse(
    string Provedor,
    string Resumo,
    string AlertaCompatibilidade,
    IReadOnlyList<AiRecommendationProductResponse> Produtos);
