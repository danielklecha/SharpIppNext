using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Startup-One-Printer operation.
/// See: PWG 5100.22-2025 Section 6.1.8
/// </summary>
[IppRequest(IppOperation.StartupOnePrinter)]
public class StartupOnePrinterRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
