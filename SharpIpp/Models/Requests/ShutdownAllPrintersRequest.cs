namespace SharpIpp.Models.Requests;

/// <summary>
/// Shutdown-All-Printers operation.
/// See: PWG 5100.22-2025 Section 6.3.16
/// </summary>
public class ShutdownAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
