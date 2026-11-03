using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Disable-Printer Response.
/// See: PWG 5100.15-2013 Section 4.2
/// </summary>
[IppResponse(IppOperation.DisablePrinter)]
public class DisablePrinterResponse : IppResponse<OperationAttributes> { }
