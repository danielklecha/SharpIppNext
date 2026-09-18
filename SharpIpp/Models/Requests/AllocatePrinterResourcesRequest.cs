using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Allocate-Printer-Resources operation.
/// See: PWG 5100.22-2025 Section 6.1.1
/// </summary>
[IppRequest(IppOperation.AllocatePrinterResources)]
public class AllocatePrinterResourcesRequest : IppRequest<AllocatePrinterResourcesOperationAttributes>, IIppSystemRequest
{
}
