using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol.Extensions;

/// <summary>
/// Extension methods for <see cref="IKeywordSmartEnum"/>.
/// </summary>
public static class KeywordSmartEnumExtensions
{
    /// <summary>
    /// Returns the IPP tag based on whether the smart enum value is a keyword.
    /// </summary>
    /// <param name="value">The smart enum value.</param>
    /// <returns><see cref="Tag.Keyword"/> when keyword-backed; otherwise <see cref="Tag.NameWithoutLanguage"/>.</returns>
    public static Tag ToIppTag(this IKeywordSmartEnum value)
    {
        return value.IsKeyword ? Tag.Keyword : Tag.NameWithoutLanguage;
    }
}