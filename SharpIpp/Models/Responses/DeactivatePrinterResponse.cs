using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Deactivate-Printer Response.
/// See: PWG 5100.15-2013 Section 4.2
/// </summary>
[IppResponse(IppOperation.DeactivatePrinter)]
public class DeactivatePrinterResponse : IppResponse<OperationAttributes> { }
