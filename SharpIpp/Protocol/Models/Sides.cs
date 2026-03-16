namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies how pages are imposed onto media sheets.
/// </summary>
public readonly record struct Sides(string Value)
{
    /// <summary>
    /// 'one-sided': imposes each consecutive print-stream page upon the
    /// same side of consecutive media sheets.
    /// </summary>
    public static readonly Sides OneSided = new("one-sided");

    /// <summary>
    /// 'two-sided-long-edge': imposes each consecutive pair of print-
    /// stream pages upon front and back sides of consecutive media
    /// sheets, such that the orientation of each pair of print-stream
    /// pages on the medium would be correct for the reader as if for
    /// binding on the long edge.
    /// </summary>
    public static readonly Sides TwoSidedLongEdge = new("two-sided-long-edge");

    /// <summary>
    /// 'two-sided-short-edge': imposes each consecutive pair of print-
    /// stream pages upon front and back sides of consecutive media
    /// sheets, such that the orientation of each pair of print-stream
    /// pages on the medium would be correct for the reader as if for
    /// binding on the short edge.
    /// </summary>
    public static readonly Sides TwoSidedShortEdge = new("two-sided-short-edge");

    public override string ToString() => Value;
    public static implicit operator string(Sides bin) => bin.Value;
    public static explicit operator Sides(string value) => new(value);
}
