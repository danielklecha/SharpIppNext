using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-User-Printer-Attributes operation.
/// See: PWG 5100.11-2024 Section 5.1
/// </summary>
[IppRequest(IppOperation.GetUserPrinterAttributes)]
public class GetUserPrinterAttributesRequest : IppRequest<GetUserPrinterAttributesOperationAttributes>, IIppPrinterRequest
{
}
