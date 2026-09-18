using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Fetch-Document operation.
/// See: PWG 5100.18-2025 Section 5.5
/// </summary>
[IppRequest(IppOperation.FetchDocument)]
public class FetchDocumentRequest : IppRequest<FetchDocumentOperationAttributes>, IIppJobRequest
{
}
