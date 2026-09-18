using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Create-Printer operation.
/// See: PWG 5100.22-2025 Section 6.3.1
/// </summary>
[IppRequest(IppOperation.CreatePrinter)]
public class CreatePrinterRequest : IppRequest<CreatePrinterOperationAttributes>, IIppSystemRequest
{
}
