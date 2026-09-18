using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Disable-All-Printers operation.
/// See: PWG 5100.22-2025 Section 6.3.5
/// </summary>
[IppRequest(IppOperation.DisableAllPrinters)]
public class DisableAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
