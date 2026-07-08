namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the orientation of media feed.
/// See: PWG 5100.13-2023 Section 6.6.9
/// </summary>
public readonly record struct FeedOrientation(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly FeedOrientation LongEdgeFirst = new("long-edge-first");
    public static readonly FeedOrientation ShortEdgeFirst = new("short-edge-first");

    public override string ToString() => Value;
    public static implicit operator string(FeedOrientation bin) => bin.Value;
    public static implicit operator FeedOrientation(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
