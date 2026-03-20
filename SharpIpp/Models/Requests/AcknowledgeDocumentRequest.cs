namespace SharpIpp.Models.Requests;

/// <summary>
/// Acknowledge-Document operation.
/// See: PWG 5100.18-2025 Section 5.1
/// </summary>
public class AcknowledgeDocumentRequest : IppRequest<AcknowledgeDocumentOperationAttributes>, IIppJobRequest
{
}
