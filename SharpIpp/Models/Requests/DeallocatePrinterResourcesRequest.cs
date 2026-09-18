using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Deallocate-Printer-Resources operation.
/// See: PWG 5100.22-2025 Section 6.1.2
/// </summary>
[IppRequest(IppOperation.DeallocatePrinterResources)]
public class DeallocatePrinterResourcesRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
