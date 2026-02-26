using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol.Extensions;

/// <summary>
///     Extension methods for <see cref="Tag" />.
/// </summary>
public static class TagExtensions
{
    /// <summary>
    ///     Checks if the tag is an out-of-band tag (unsupported, unknown, or no-value).
    /// </summary>
    /// <param name="tag">The tag to check.</param>
    /// <returns>True if the tag is out-of-band, otherwise false.</returns>
    public static bool IsOutOfBand(this Tag tag)
    {
        return tag == Tag.Unsupported || tag == Tag.Unknown || tag == Tag.NoValue;
    }
}
