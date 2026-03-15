using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;
/// <summary>
/// PWG 5100.5-2024 Section 5.1.2.2
/// </summary>
public class GetDocumentAttributesResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// The document-attributes IPP attribute.
    /// See: pwg5100.18 - IPP Shared Infrastructure Extensions v1.1
    /// </summary>
    /// <code>document-attributes</code>
    public DocumentAttributes? DocumentAttributes { get; set; }
}
