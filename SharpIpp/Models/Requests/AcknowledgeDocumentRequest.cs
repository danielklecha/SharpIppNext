using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Acknowledge-Document operation.
/// See: PWG 5100.18-2025 Section 5.1
/// </summary>
[IppRequest(IppOperation.AcknowledgeDocument)]
public class AcknowledgeDocumentRequest : IppRequest<AcknowledgeDocumentOperationAttributes>, IIppJobRequest
{
}
