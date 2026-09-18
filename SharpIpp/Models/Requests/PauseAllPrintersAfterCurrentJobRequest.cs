using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Pause-All-Printers-After-Current-Job operation.
/// See: PWG 5100.22-2025 Section 6.3.11
/// </summary>
[IppRequest(IppOperation.PauseAllPrintersAfterCurrentJob)]
public class PauseAllPrintersAfterCurrentJobRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
