using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Printer-Resources operation.
/// See: PWG 5100.22-2025 Section 6.1.5
/// </summary>
[IppRequest(IppOperation.GetPrinterResources)]
public class GetPrinterResourcesRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
