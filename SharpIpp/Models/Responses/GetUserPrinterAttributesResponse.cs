using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
namespace SharpIpp.Models.Responses;

/// <summary>
/// Get-User-Printer-Attributes response.
/// See: PWG 5100.11-2024 Section 5.1.2
/// </summary>
[IppResponse(IppOperation.GetUserPrinterAttributes)]
public class GetUserPrinterAttributesResponse : GetPrinterAttributesResponse
{
}
