namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of stitching to use.
/// See: PWG 5100.1-2022 Section 5.2.9.3
/// </summary>
public readonly record struct StitchingMethod(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly StitchingMethod Auto = new("auto");
    public static readonly StitchingMethod Crimp = new("crimp");
    public static readonly StitchingMethod Wire = new("wire");

    public override string ToString() => Value;
    public static implicit operator string(StitchingMethod bin) => bin.Value;
    public static explicit operator StitchingMethod(string value) => new(value);
}
