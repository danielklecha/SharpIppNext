namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies whether the Printer's Page Description Language (PDL) override is supported.
/// See: RFC 8011 Section 5.4.28
/// </summary>
public readonly record struct PdlOverride(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// The Printer attempts to override PDL instructions with IPP attribute values.
    /// See: RFC 8011 Section 5.4.28
    /// </summary>
    public static readonly PdlOverride Attempted = new("attempted");

    /// <summary>
    /// The Printer does not attempt to override PDL instructions with IPP attribute values.
    /// See: RFC 8011 Section 5.4.28
    /// </summary>
    public static readonly PdlOverride NotAttempted = new("not-attempted");

    /// <summary>
    /// The Printer guarantees that IPP attribute values override PDL instructions.
    /// See: RFC 8011 Section 5.4.28
    /// </summary>
    public static readonly PdlOverride Guaranteed = new("guaranteed");

    public override string ToString() => Value;
    public static implicit operator string(PdlOverride bin) => bin.Value;
    public static implicit operator PdlOverride(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
