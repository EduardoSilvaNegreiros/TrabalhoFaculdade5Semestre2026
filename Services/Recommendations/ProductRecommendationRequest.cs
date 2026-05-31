namespace WebApplication1.Services.Recommendations;

public sealed record ProductRecommendationRequest(
    string? TipoPele,
    string? TipoCabelo,
    string? Categoria,
    bool? Vegano,
    decimal? PrecoMax,
    string? Objetivo);

