namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of baling.
/// See: PWG 5100.1-2022 Section 5.2.1.1
/// </summary>
public readonly record struct BalingType(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum 
{
    /// <summary>
    /// Baling using a band or strap.
    /// See: PWG 5100.1-2022 Section 5.2.1.1
    /// </summary>
    public static readonly BalingType Band = new("band");

    /// <summary>
    /// Baling using shrink wrap.
    /// See: PWG 5100.1-2022 Section 5.2.1.1
    /// </summary>
    public static readonly BalingType ShrinkWrap = new("shrink-wrap");

    /// <summary>
    /// Baling using a wrap.
    /// See: PWG 5100.1-2022 Section 5.2.1.1
    /// </summary>
    public static readonly BalingType Wrap = new("wrap");

    public override string ToString() => Value;
    public static implicit operator string(BalingType bin) => bin.Value;
    public static implicit operator BalingType(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
