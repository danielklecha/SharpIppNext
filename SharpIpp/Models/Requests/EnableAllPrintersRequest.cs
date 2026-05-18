namespace SharpIpp.Models.Requests;

/// <summary>
/// Enable-All-Printers operation.
/// See: PWG 5100.22-2025 Section 6.3.6
/// </summary>
public class EnableAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
