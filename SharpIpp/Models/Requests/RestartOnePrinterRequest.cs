using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Restart-One-Printer operation.
/// See: PWG 5100.22-2025 Section 6.1.6
/// </summary>
[IppRequest(IppOperation.RestartOnePrinter)]
public class RestartOnePrinterRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
