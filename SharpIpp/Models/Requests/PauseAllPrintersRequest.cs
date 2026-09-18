using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Pause-All-Printers operation.
/// See: PWG 5100.22-2025 Section 6.3.10
/// </summary>
[IppRequest(IppOperation.PauseAllPrinters)]
public class PauseAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
