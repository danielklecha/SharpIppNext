using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Acknowledge-Identify-Printer operation.
/// See: PWG 5100.18-2025 Section 5.2
/// </summary>
[IppRequest(IppOperation.AcknowledgeIdentifyPrinter)]
public class AcknowledgeIdentifyPrinterRequest : IppRequest<AcknowledgeIdentifyPrinterOperationAttributes>, IIppPrinterRequest
{
}
