using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for configured printer entries in System status.
/// See: PWG 5100.22-2025 Section 7.3.9
/// </summary>
[IppAttribute(IppAttributeNames.SystemConfiguredPrinters)]
public class SystemConfiguredPrinter : IIppCollection
{

    /// <summary>
    /// The printer-id IPP attribute.
    /// Type: integer(1:65535)
    /// See: PWG 5100.22-2025 Section 7.1.5
    /// </summary>
    public IppValue<int>? PrinterId { get; set; }

    [IppAttribute(IppAttributeNames.PrinterInfo, Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? PrinterInfo { get; set; }

    public IppValue<bool>? PrinterIsAcceptingJobs { get; set; }
    public IppValue<string>? PrinterName { get; set; }
    public IppValue<PrinterServiceType>? PrinterServiceType { get; set; }
    public IppValue<PrinterState>? PrinterState { get; set; }
    public IppValue<PrinterStateReason[]>? PrinterStateReasons { get; set; }
    public IppValue<SystemXri[]>? PrinterXriSupported { get; set; }
}
