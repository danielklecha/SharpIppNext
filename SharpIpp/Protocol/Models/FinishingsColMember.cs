namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies member attribute names supported by <code>finishings-col-supported</code>.
/// See: PWG 5100.1-2022 Section 6.12
/// </summary>
public readonly record struct FinishingsColMember(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>The finishing-template member attribute. See: PWG 5100.1-2022 Section 6.12</summary>
    public static readonly FinishingsColMember FinishingTemplate = new("finishing-template");
    /// <summary>The baling member attribute. See: PWG 5100.1-2022 Section 6.12</summary>
    public static readonly FinishingsColMember Baling = new("baling");
    /// <summary>The binding member attribute. See: PWG 5100.1-2022 Section 6.12</summary>
    public static readonly FinishingsColMember Binding = new("binding");
    /// <summary>The coating member attribute. See: PWG 5100.1-2022 Section 6.12</summary>
    public static readonly FinishingsColMember Coating = new("coating");
    /// <summary>The covering member attribute. See: PWG 5100.1-2022 Section 6.12</summary>
    public static readonly FinishingsColMember Covering = new("covering");
    /// <summary>The folding member attribute. See: PWG 5100.1-2022 Section 6.12</summary>
    public static readonly FinishingsColMember Folding = new("folding");
    /// <summary>The laminating member attribute. See: PWG 5100.1-2022 Section 6.12</summary>
    public static readonly FinishingsColMember Laminating = new("laminating");
    /// <summary>The punching member attribute. See: PWG 5100.1-2022 Section 6.12</summary>
    public static readonly FinishingsColMember Punching = new("punching");
    /// <summary>The stitching member attribute. See: PWG 5100.1-2022 Section 6.12</summary>
    public static readonly FinishingsColMember Stitching = new("stitching");
    /// <summary>The trimming member attribute. See: PWG 5100.1-2022 Section 6.12</summary>
    public static readonly FinishingsColMember Trimming = new("trimming");

    public override string ToString() => Value;
    public static implicit operator string(FinishingsColMember value) => value.Value;
    public static explicit operator FinishingsColMember(string value) => new(value);
}
