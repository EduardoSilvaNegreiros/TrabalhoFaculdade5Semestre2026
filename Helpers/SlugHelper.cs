using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace WebApplication1.Helpers;

public static partial class SlugHelper
{
    public static string Generate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return "produto";
        }

        var normalized = value.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder(normalized.Length);

        foreach (var character in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(char.ToLowerInvariant(character));
            }
        }

        var slug = InvalidSlugCharsRegex().Replace(builder.ToString().Normalize(NormalizationForm.FormC), "-");
        slug = DuplicateDashRegex().Replace(slug, "-").Trim('-');

        return string.IsNullOrWhiteSpace(slug) ? "produto" : slug;
    }

    [GeneratedRegex("[^a-z0-9]+")]
    private static partial Regex InvalidSlugCharsRegex();

    [GeneratedRegex("-+")]
    private static partial Regex DuplicateDashRegex();
}
