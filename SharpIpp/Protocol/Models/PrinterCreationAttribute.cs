namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>printer-creation-attributes-supported</code>.
/// See: PWG 5100.22-2025 Section 7.2.35
/// </summary>
public readonly record struct PrinterCreationAttribute(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly PrinterCreationAttribute PrinterName = new("printer-name");
    public static readonly PrinterCreationAttribute PrinterLocation = new("printer-location");
    public static readonly PrinterCreationAttribute PrinterInfo = new("printer-info");

    public override string ToString() => Value;
    public static implicit operator string(PrinterCreationAttribute value) => value.Value;
    public static explicit operator PrinterCreationAttribute(string value) => new(value);
}
