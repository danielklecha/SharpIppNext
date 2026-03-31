namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies whether the Printer's Page Description Language (PDL) override is supported.
/// See: RFC 8011 Section 5.4.28
/// </summary>
public readonly record struct PdlOverride(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly PdlOverride Attempted = new("attempted");
    public static readonly PdlOverride NotAttempted = new("not-attempted");
    public static readonly PdlOverride Guaranteed = new("guaranteed");

    public override string ToString() => Value;
    public static implicit operator string(PdlOverride bin) => bin.Value;
    public static explicit operator PdlOverride(string value) => new(value);
}
