namespace SharpIpp.Models.Requests;

/// <summary>
/// Shutdown-One-Printer operation.
/// See: PWG 5100.22-2025 Section 6.1.7
/// </summary>
public class ShutdownOnePrinterRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
