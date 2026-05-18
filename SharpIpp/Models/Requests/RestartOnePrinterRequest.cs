namespace SharpIpp.Models.Requests;

/// <summary>
/// Restart-One-Printer operation.
/// See: PWG 5100.22-2025 Section 6.1.6
/// </summary>
public class RestartOnePrinterRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
