using Microsoft.AspNetCore.Http;

namespace WebApplication1.Services.ProductImages;

public interface IProductImageService
{
    Task<ProductImageSaveResult> SaveAsync(IFormFile image, int lojistaId, string productSlug, CancellationToken cancellationToken);
}

public sealed record ProductImageSaveResult(bool Success, string? Url, string? ErrorMessage);
