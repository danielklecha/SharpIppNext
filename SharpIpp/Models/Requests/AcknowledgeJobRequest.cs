namespace SharpIpp.Models.Requests;

/// <summary>
/// Acknowledge-Job operation.
/// See: PWG 5100.18-2025 Section 5.3
/// </summary>
public class AcknowledgeJobRequest : IppRequest<AcknowledgeJobOperationAttributes>, IIppJobRequest
{
}
