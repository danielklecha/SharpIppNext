using SharpIpp.Mapping;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;
/// <summary>
/// PWG 5100.5-2024 Section 5.1.2.2
/// </summary>
[IppResponse]
public class GetDocumentAttributesResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// See: PWG 5100.5-2024 Section 5.1.2.2
    /// </summary>
    public IppValue<DocumentAttributes>? DocumentAttributes { get; set; }
}
