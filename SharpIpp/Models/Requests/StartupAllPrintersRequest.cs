namespace SharpIpp.Models.Requests;

/// <summary>
/// Startup-All-Printers operation.
/// See: PWG 5100.22-2025 Section 6.3.17
/// </summary>
public class StartupAllPrintersRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
