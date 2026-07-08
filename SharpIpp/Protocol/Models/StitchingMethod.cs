namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of stitching to use.
/// See: PWG 5100.1-2022 Section 5.2.9.3
/// </summary>
public readonly record struct StitchingMethod(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// The Printer automatically selects the stitching method.
    /// See: PWG 5100.1-2022 Section 5.2.9.3
    /// </summary>
    public static readonly StitchingMethod Auto = new("auto");

    /// <summary>
    /// Crimp stitching (staple-free binding using a crimp).
    /// See: PWG 5100.1-2022 Section 5.2.9.3
    /// </summary>
    public static readonly StitchingMethod Crimp = new("crimp");

    /// <summary>
    /// Wire stitching (traditional staple binding).
    /// See: PWG 5100.1-2022 Section 5.2.9.3
    /// </summary>
    public static readonly StitchingMethod Wire = new("wire");

    public override string ToString() => Value;
    public static implicit operator string(StitchingMethod bin) => bin.Value;
    public static implicit operator StitchingMethod(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
