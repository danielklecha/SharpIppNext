namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>printer-creation-attributes-supported</code>.
/// See: PWG 5100.22-2025 Section 7.2.35
/// </summary>
public readonly record struct PrinterCreationAttribute(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>The printer-name attribute. See: PWG 5100.22-2025 Section 7.2.35</summary>
    public static readonly PrinterCreationAttribute PrinterName = new("printer-name");
    /// <summary>The printer-location attribute. See: PWG 5100.22-2025 Section 7.2.35</summary>
    public static readonly PrinterCreationAttribute PrinterLocation = new("printer-location");
    /// <summary>The printer-info attribute. See: PWG 5100.22-2025 Section 7.2.35</summary>
    public static readonly PrinterCreationAttribute PrinterInfo = new("printer-info");

    public override string ToString() => Value;
    public static implicit operator string(PrinterCreationAttribute value) => value.Value;
    public static implicit operator PrinterCreationAttribute(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
