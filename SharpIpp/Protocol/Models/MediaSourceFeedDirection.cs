namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the feed direction of the media source.
/// See: PWG 5100.1-2022 Section 5.3.3
/// </summary>
public readonly record struct MediaSourceFeedDirection(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// Media is fed long-edge first.
    /// See: PWG 5100.1-2022 Section 5.3.3
    /// </summary>
    public static readonly MediaSourceFeedDirection LongEdgeFirst = new("long-edge-first");

    /// <summary>
    /// Media is fed short-edge first.
    /// See: PWG 5100.1-2022 Section 5.3.3
    /// </summary>
    public static readonly MediaSourceFeedDirection ShortEdgeFirst = new("short-edge-first");

    public override string ToString() => Value;
    public static implicit operator string(MediaSourceFeedDirection bin) => bin.Value;
    public static implicit operator MediaSourceFeedDirection(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
