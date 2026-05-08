namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies known keyword values for <code>printer-service-type</code>.
/// See: PWG 5100.22-2025 Section 7.7.9.
/// </summary>
public readonly record struct PrinterServiceType(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly PrinterServiceType Copy = new("copy");
    public static readonly PrinterServiceType FaxIn = new("faxin");
    public static readonly PrinterServiceType FaxOut = new("faxout");
    public static readonly PrinterServiceType Print = new("print");
    public static readonly PrinterServiceType Print3D = new("print3d");
    public static readonly PrinterServiceType Scan = new("scan");

    public override string ToString() => Value;
    public static implicit operator string(PrinterServiceType value) => value.Value;
    public static explicit operator PrinterServiceType(string value) => new(value);
}
