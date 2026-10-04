using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol.Extensions;

/// <summary>
/// Extension methods for <see cref="IKeywordOrNameEnum"/>.
/// </summary>
public static class KeywordOrNameEnumExtensions
{
    /// <summary>
    /// Returns the IPP tag based on whether the value is an IPP keyword or name.
    /// </summary>
    /// <param name="value">The keyword-or-name value.</param>
    /// <returns><see cref="Tag.Keyword"/> when <see cref="IKeywordOrNameEnum.IsKeyword"/> is true; otherwise <see cref="Tag.NameWithoutLanguage"/>.</returns>
    public static Tag ToIppTag(this IKeywordOrNameEnum value)
    {
        return value.IsKeyword ? Tag.Keyword : Tag.NameWithoutLanguage;
    }

    /// <summary>
    /// Gets a value indicating whether this value represents an IPP name rather than a keyword.
    /// </summary>
    /// <param name="value">The keyword-or-name value.</param>
    /// <returns><see langword="true"/> when the value is an IPP name; otherwise <see langword="false"/>.</returns>
    public static bool IsName(this IKeywordOrNameEnum value) => !value.IsKeyword;
}
