using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Shutdown-One-Printer operation.
/// See: PWG 5100.22-2025 Section 6.1.7
/// </summary>
[IppRequest(IppOperation.ShutdownOnePrinter)]
public class ShutdownOnePrinterRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
