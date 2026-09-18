using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Set-Printer-Attributes operation response.
/// See: RFC 3380 Section 4.1
/// </summary>
[IppResponse]
public class SetPrinterAttributesResponse : IppResponse<OperationAttributes>
{
}
