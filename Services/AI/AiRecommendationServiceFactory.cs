namespace WebApplication1.Services.AI;

public sealed class AiRecommendationServiceFactory : IAiRecommendationServiceFactory
{
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;

    public AiRecommendationServiceFactory(IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }

    public IAiRecommendationService Create()
    {
        var apiKey = _configuration["OpenAI:ApiKey"] ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        if (!string.IsNullOrWhiteSpace(apiKey))
        {
            return _serviceProvider.GetRequiredService<OpenAiRecommendationService>();
        }

        return _serviceProvider.GetRequiredService<LocalAiRecommendationService>();
    }
}

