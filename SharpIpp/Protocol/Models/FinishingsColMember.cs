namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies member attribute names supported by <code>finishings-col-supported</code>.
/// See: PWG 5100.1-2022 Section 6.12
/// </summary>
public readonly record struct FinishingsColMember(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly FinishingsColMember FinishingTemplate = new("finishing-template");
    public static readonly FinishingsColMember Baling = new("baling");
    public static readonly FinishingsColMember Binding = new("binding");
    public static readonly FinishingsColMember Coating = new("coating");
    public static readonly FinishingsColMember Covering = new("covering");
    public static readonly FinishingsColMember Folding = new("folding");
    public static readonly FinishingsColMember Laminating = new("laminating");
    public static readonly FinishingsColMember Punching = new("punching");
    public static readonly FinishingsColMember Stitching = new("stitching");
    public static readonly FinishingsColMember Trimming = new("trimming");

    public override string ToString() => Value;
    public static implicit operator string(FinishingsColMember value) => value.Value;
    public static explicit operator FinishingsColMember(string value) => new(value);
}