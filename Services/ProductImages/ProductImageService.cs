using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WebApplication1.Helpers;

namespace WebApplication1.Services.ProductImages;

public sealed class ProductImageService : IProductImageService
{
    private const long MaxFileSize = 5 * 1024 * 1024;
    private const int MinSide = 600;
    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    };

    private readonly IWebHostEnvironment _environment;

    public ProductImageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<ProductImageSaveResult> SaveAsync(IFormFile image, int lojistaId, string productSlug, CancellationToken cancellationToken)
    {
        if (image.Length == 0)
        {
            return new ProductImageSaveResult(false, null, "Envie uma imagem do produto.");
        }

        if (image.Length > MaxFileSize)
        {
            return new ProductImageSaveResult(false, null, "A imagem deve ter no máximo 5 MB.");
        }

        var extension = Path.GetExtension(image.FileName);
        if (!AllowedExtensions.Contains(extension))
        {
            return new ProductImageSaveResult(false, null, "Use uma imagem JPG, PNG ou WebP.");
        }

        var dimensions = await TryReadDimensionsAsync(image, cancellationToken);
        if (dimensions == null)
        {
            return new ProductImageSaveResult(false, null, "Não foi possível validar a imagem enviada.");
        }

        if (Math.Min(dimensions.Value.Width, dimensions.Value.Height) < MinSide)
        {
            return new ProductImageSaveResult(false, null, "A imagem precisa ter pelo menos 600 px no menor lado.");
        }

        var folder = Path.Combine(_environment.WebRootPath, "images", "produtos", "uploads", lojistaId.ToString());
        Directory.CreateDirectory(folder);

        var safeSlug = SlugHelper.Generate(productSlug);
        var fileName = $"{safeSlug}-{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
        var fullPath = Path.Combine(folder, fileName);

        await using var stream = File.Create(fullPath);
        await image.CopyToAsync(stream, cancellationToken);

        return new ProductImageSaveResult(true, $"/images/produtos/uploads/{lojistaId}/{fileName}", null);
    }

    private static async Task<(int Width, int Height)?> TryReadDimensionsAsync(IFormFile image, CancellationToken cancellationToken)
    {
        await using var stream = image.OpenReadStream();
        using var memory = new MemoryStream();
        await stream.CopyToAsync(memory, cancellationToken);
        var bytes = memory.ToArray();

        return TryReadPng(bytes) ?? TryReadJpeg(bytes) ?? TryReadWebp(bytes);
    }

    private static (int Width, int Height)? TryReadPng(byte[] bytes)
    {
        if (bytes.Length < 24 ||
            bytes[0] != 0x89 ||
            bytes[1] != 0x50 ||
            bytes[2] != 0x4E ||
            bytes[3] != 0x47)
        {
            return null;
        }

        return (ReadBigEndianInt32(bytes, 16), ReadBigEndianInt32(bytes, 20));
    }

    private static (int Width, int Height)? TryReadJpeg(byte[] bytes)
    {
        if (bytes.Length < 4 || bytes[0] != 0xFF || bytes[1] != 0xD8)
        {
            return null;
        }

        var index = 2;
        while (index + 9 < bytes.Length)
        {
            if (bytes[index] != 0xFF)
            {
                index++;
                continue;
            }

            var marker = bytes[index + 1];
            var length = (bytes[index + 2] << 8) + bytes[index + 3];
            if (length < 2 || index + length + 2 > bytes.Length)
            {
                return null;
            }

            if (marker is >= 0xC0 and <= 0xC3 or >= 0xC5 and <= 0xC7 or >= 0xC9 and <= 0xCB or >= 0xCD and <= 0xCF)
            {
                var height = (bytes[index + 5] << 8) + bytes[index + 6];
                var width = (bytes[index + 7] << 8) + bytes[index + 8];
                return (width, height);
            }

            index += length + 2;
        }

        return null;
    }

    private static (int Width, int Height)? TryReadWebp(byte[] bytes)
    {
        if (bytes.Length < 30 ||
            bytes[0] != 0x52 ||
            bytes[1] != 0x49 ||
            bytes[2] != 0x46 ||
            bytes[3] != 0x46 ||
            bytes[8] != 0x57 ||
            bytes[9] != 0x45 ||
            bytes[10] != 0x42 ||
            bytes[11] != 0x50)
        {
            return null;
        }

        var chunk = System.Text.Encoding.ASCII.GetString(bytes, 12, 4);
        if (chunk == "VP8X" && bytes.Length >= 30)
        {
            var width = 1 + bytes[24] + (bytes[25] << 8) + (bytes[26] << 16);
            var height = 1 + bytes[27] + (bytes[28] << 8) + (bytes[29] << 16);
            return (width, height);
        }

        return null;
    }

    private static int ReadBigEndianInt32(byte[] bytes, int offset)
    {
        return (bytes[offset] << 24) +
               (bytes[offset + 1] << 16) +
               (bytes[offset + 2] << 8) +
               bytes[offset + 3];
    }
}
