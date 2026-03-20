namespace SharpIpp.Models.Requests;

/// <summary>
/// Create-Printer operation attributes.
/// See: PWG 5100.22-2025 Section 6.3.1
/// </summary>
public class CreatePrinterOperationAttributes : SystemOperationAttributes
{
    /// <summary>
    /// The <c>resource-ids</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.15
    /// </summary>
    public int[]? ResourceIds { get; set; }

    /// <summary>
    /// The <c>printer-service-type</c> operation attribute specifying the type(s) of print service.
    /// See: PWG 5100.22-2025 Section 7.1.9
    /// </summary>
    public string[]? PrinterServiceType { get; set; }

    /// <summary>
    /// The <c>printer-xri-requested</c> operation attribute.
    /// See: PWG 5100.22-2025 Section 7.1.10
    /// </summary>
    public string[]? PrinterXriRequested { get; set; }
}
