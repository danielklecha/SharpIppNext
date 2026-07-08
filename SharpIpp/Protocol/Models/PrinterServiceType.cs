namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies known keyword values for <code>printer-service-type</code>.
/// See: PWG 5100.22-2025 Section 7.7.9.
/// </summary>
public readonly record struct PrinterServiceType(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// The Printer provides copy services.
    /// See: PWG 5100.22-2025 Section 7.7.9
    /// </summary>
    public static readonly PrinterServiceType Copy = new("copy");

    /// <summary>
    /// The Printer provides inbound fax services.
    /// See: PWG 5100.22-2025 Section 7.7.9
    /// </summary>
    public static readonly PrinterServiceType FaxIn = new("faxin");

    /// <summary>
    /// The Printer provides outbound fax services.
    /// See: PWG 5100.22-2025 Section 7.7.9
    /// </summary>
    public static readonly PrinterServiceType FaxOut = new("faxout");

    /// <summary>
    /// The Printer provides print services.
    /// See: PWG 5100.22-2025 Section 7.7.9
    /// </summary>
    public static readonly PrinterServiceType Print = new("print");

    /// <summary>
    /// The Printer provides 3D print services.
    /// See: PWG 5100.22-2025 Section 7.7.9
    /// </summary>
    public static readonly PrinterServiceType Print3D = new("print3d");

    /// <summary>
    /// The Printer provides scan services.
    /// See: PWG 5100.22-2025 Section 7.7.9
    /// </summary>
    public static readonly PrinterServiceType Scan = new("scan");

    public override string ToString() => Value;
    public static implicit operator string(PrinterServiceType value) => value.Value;
    public static implicit operator PrinterServiceType(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
