using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WebApplication1.Dtos.Api;

namespace WebApplication1.Services.AI;

public sealed class OpenAiRecommendationService : IAiRecommendationService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly LocalAiRecommendationService _fallback;

    public OpenAiRecommendationService(HttpClient httpClient, IConfiguration configuration, LocalAiRecommendationService fallback)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _fallback = fallback;
    }

    public async Task<AiRecommendationResponse> RecommendAsync(AiRecommendationRequest request, CancellationToken cancellationToken)
    {
        var apiKey = _configuration["OpenAI:ApiKey"] ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return await _fallback.RecommendAsync(request, cancellationToken);
        }

        try
        {
            using var message = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/responses");
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var payload = new
            {
                model = _configuration["OpenAI:Model"] ?? "gpt-5-mini",
                input = $"Recomende produtos de beleza para pele={request.TipoPele}, cabelo={request.TipoCabelo}, objetivo={request.Objetivo}, categoria={request.Categoria}, vegano={request.Vegano}, precoMax={request.PrecoMax}. Responda em portugues com uma frase curta."
            };

            message.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            using var response = await _httpClient.SendAsync(message, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return await _fallback.RecommendAsync(request, cancellationToken);
            }

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            var resumo = ExtrairTexto(json);
            var local = await _fallback.RecommendAsync(request, cancellationToken);

            return local with
            {
                Provedor = "OpenAI Responses API",
                Resumo = string.IsNullOrWhiteSpace(resumo) ? local.Resumo : resumo
            };
        }
        catch
        {
            return await _fallback.RecommendAsync(request, cancellationToken);
        }
    }

    private static string ExtrairTexto(string json)
    {
        using var document = JsonDocument.Parse(json);
        if (document.RootElement.TryGetProperty("output_text", out var outputText))
        {
            return outputText.GetString() ?? string.Empty;
        }

        return "Recomendacao gerada pela OpenAI Responses API com retorno estruturado pelo sistema.";
    }
}

