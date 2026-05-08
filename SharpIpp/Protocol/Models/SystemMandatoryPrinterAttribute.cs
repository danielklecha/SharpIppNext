namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>system-mandatory-printer-attributes</code>.
/// See: PWG 5100.22-2025 Section 7.2.36
/// </summary>
public readonly record struct SystemMandatoryPrinterAttribute(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly SystemMandatoryPrinterAttribute PrinterName = new("printer-name");
    public static readonly SystemMandatoryPrinterAttribute PrinterLocation = new("printer-location");
    public static readonly SystemMandatoryPrinterAttribute PrinterInfo = new("printer-info");

    public override string ToString() => Value;
    public static implicit operator string(SystemMandatoryPrinterAttribute value) => value.Value;
    public static explicit operator SystemMandatoryPrinterAttribute(string value) => new(value);
}
