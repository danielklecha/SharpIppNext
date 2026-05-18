namespace SharpIpp.Models.Requests;

/// <summary>
/// Pause-All-Printers operation.
/// See: PWG 5100.22-2025 Section 6.3.10
/// </summary>
public class PauseAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
