using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Allocate-Printer-Resources response.
/// See: PWG 5100.22-2025 Section 6.1.1
/// </summary>
public class AllocatePrinterResourcesResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// The complete list of resource IDs currently allocated to this Printer.
    /// See: PWG 5100.22-2025 Section 6.1.1.2 (Group 3: Printer Attributes)
    /// </summary>
    public int[]? PrinterResourceIds { get; set; }
}
