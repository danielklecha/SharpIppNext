using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol.Extensions;

/// <summary>
/// Extension methods for <see cref="IMarkedSmartEnum"/>.
/// </summary>
public static class MarkedSmartEnumExtensions
{
    /// <summary>
    /// Returns the IPP tag based on whether the smart enum value is marked.
    /// </summary>
    /// <param name="value">The smart enum value.</param>
    /// <returns><see cref="Tag.Keyword"/> when marked; otherwise <see cref="Tag.NameWithoutLanguage"/>.</returns>
    public static Tag ToIppTag(this IMarkedSmartEnum value)
    {
        return value.IsMarked ? Tag.Keyword : Tag.NameWithoutLanguage;
    }
}
