namespace SharpIpp.Models.Requests;

/// <summary>
/// Disable-All-Printers operation.
/// See: PWG 5100.22-2025 Section 6.3.5
/// </summary>
public class DisableAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
