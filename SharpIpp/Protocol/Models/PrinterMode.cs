namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>printer-mode-configured</c> and <c>printer-mode-supported</c> attributes.
/// See: PWG 5100.18-2025 Section 7.4.4
/// </summary>
public readonly record struct PrinterMode(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly PrinterMode Fax = new("fax");
    public static readonly PrinterMode Ipp = new("ipp");
    public static readonly PrinterMode Scan = new("scan");
    public static readonly PrinterMode Storage = new("storage");

    public override string ToString() => Value;
    public static implicit operator string(PrinterMode value) => value.Value;
    public static explicit operator PrinterMode(string value) => new(value);
}
