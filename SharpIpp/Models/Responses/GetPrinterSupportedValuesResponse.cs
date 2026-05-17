using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Get-Printer-Supported-Values Response.
/// See: RFC 3380 Section 4.3
/// </summary>
public class GetPrinterSupportedValuesResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// The printer-attributes IPP attribute.
    /// See: RFC 3380 Section 4.3
    /// </summary>
    public PrinterDescriptionAttributes? PrinterAttributes { get; set; }
}
