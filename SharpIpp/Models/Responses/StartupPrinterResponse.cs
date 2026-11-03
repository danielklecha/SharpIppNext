using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Startup-Printer Response.
/// See: PWG 5100.15-2013 Section 4.2
/// </summary>
[IppResponse(IppOperation.StartupPrinter)]
public class StartupPrinterResponse : IppResponse<OperationAttributes> { }
