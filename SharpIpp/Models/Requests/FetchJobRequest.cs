namespace SharpIpp.Models.Requests;

/// <summary>
/// Fetch-Job operation.
/// See: PWG 5100.18-2025 Section 5.6
/// </summary>
public class FetchJobRequest : IppRequest<FetchJobOperationAttributes>, IIppJobRequest
{
}
