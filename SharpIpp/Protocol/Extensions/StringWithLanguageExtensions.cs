using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol.Extensions;

/// <summary>
/// Extension methods for <see cref="StringWithLanguage"/>.
/// </summary>
public static class StringWithLanguageExtensions
{
    /// <summary>
    /// Resolves the IPP tag based on whether the string has a language tag or represents NoValue.
    /// </summary>
    /// <param name="value">The string with language instance.</param>
    /// <param name="suggestedTag">The base tag (e.g. <see cref="Tag.TextWithoutLanguage"/> or <see cref="Tag.NameWithoutLanguage"/>).</param>
    /// <returns>The resolved <see cref="Tag"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="suggestedTag"/> is not a Text or Name tag.</exception>
    public static Tag ToIppTag(this StringWithLanguage value, Tag suggestedTag = Tag.TextWithoutLanguage)
    {
        if (!value.IsValue)
            return Tag.NoValue;

        return suggestedTag switch
        {
            Tag.TextWithoutLanguage or Tag.TextWithLanguage =>
                value.HasLanguage ? Tag.TextWithLanguage : Tag.TextWithoutLanguage,

            Tag.NameWithoutLanguage or Tag.NameWithLanguage =>
                value.HasLanguage ? Tag.NameWithLanguage : Tag.NameWithoutLanguage,

            _ => throw new ArgumentException(
                $"Tag '{suggestedTag}' is not supported for {nameof(StringWithLanguage)}. Only Text and Name tags are supported.",
                nameof(suggestedTag))
        };
    }
}
