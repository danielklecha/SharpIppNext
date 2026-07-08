namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>printer-mode-configured</c> and <c>printer-mode-supported</c> attributes.
/// See: PWG 5100.18-2025 Section 7.4.4
/// </summary>
public readonly record struct PrinterMode(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The Printer operates in fax mode.
    /// See: PWG 5100.18-2025 Section 7.4.4
    /// </summary>
    public static readonly PrinterMode Fax = new("fax");

    /// <summary>
    /// The Printer operates in IPP print mode.
    /// See: PWG 5100.18-2025 Section 7.4.4
    /// </summary>
    public static readonly PrinterMode Ipp = new("ipp");

    /// <summary>
    /// The Printer operates in scan mode.
    /// See: PWG 5100.18-2025 Section 7.4.4
    /// </summary>
    public static readonly PrinterMode Scan = new("scan");

    /// <summary>
    /// The Printer operates in storage mode.
    /// See: PWG 5100.18-2025 Section 7.4.4
    /// </summary>
    public static readonly PrinterMode Storage = new("storage");

    public override string ToString() => Value;
    public static implicit operator string(PrinterMode value) => value.Value;
    public static implicit operator PrinterMode(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
