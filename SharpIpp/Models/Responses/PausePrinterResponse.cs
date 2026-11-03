using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;
/// <summary>
/// Pause-Printer Response
/// See: RFC 2911 Section 3.2.7
/// </summary>
[IppResponse(IppOperation.PausePrinter)]
public class PausePrinterResponse : IppResponse<OperationAttributes>
{
}
