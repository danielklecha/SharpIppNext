namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the finishing-template attribute.
/// See: PWG 5100.1
/// </summary>
public readonly record struct FinishingTemplate(string Value)
{
    public static readonly FinishingTemplate Staple = new("staple");
    public static readonly FinishingTemplate Punch = new("punch");
    public static readonly FinishingTemplate Bind = new("bind");
    public static readonly FinishingTemplate Trim = new("trim");
    public static readonly FinishingTemplate Fold = new("fold");
    public static readonly FinishingTemplate Bale = new("bale");
    public static readonly FinishingTemplate Laminate = new("laminate");
    public static readonly FinishingTemplate Coat = new("coat");
    public static readonly FinishingTemplate JogOffset = new("jog-offset");

    public override string ToString() => Value;
    public static implicit operator string(FinishingTemplate bin) => bin.Value;
    public static explicit operator FinishingTemplate(string value) => new(value);
}
