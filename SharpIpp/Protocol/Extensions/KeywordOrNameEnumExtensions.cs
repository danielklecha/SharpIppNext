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
        if (value is null)
            throw new System.ArgumentNullException(nameof(value));

        return value.IsKeyword ? Tag.Keyword : Tag.NameWithoutLanguage;
    }

}
