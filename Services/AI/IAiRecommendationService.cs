using WebApplication1.Dtos.Api;

namespace WebApplication1.Services.AI;

public interface IAiRecommendationService
{
    Task<AiRecommendationResponse> RecommendAsync(AiRecommendationRequest request, CancellationToken cancellationToken);
}

