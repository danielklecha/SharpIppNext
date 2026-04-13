using SharpIpp.Models.Responses;

namespace SharpIpp.Models.Requests;

/// <summary>
/// Update-Document-Status operation.
/// See: PWG 5100.18-2025 Section 5.8
/// </summary>
public class UpdateDocumentStatusRequest : IppRequest<UpdateDocumentStatusOperationAttributes>, IIppJobRequest
{
    /// <summary>
    /// Document status attributes supplied by the Proxy.
    /// See: PWG 5100.18-2025 Section 5.8.1
    /// </summary>
    public DocumentAttributes? DocumentAttributes { get; set; }
}
