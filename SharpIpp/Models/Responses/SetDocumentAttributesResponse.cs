using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Models.Requests;

namespace SharpIpp.Models.Responses;
/// <summary>
/// PWG 5100.5-2024 Section 5.1.3.2
/// </summary>
[IppResponse(IppOperation.SetDocumentAttributes)]
public class SetDocumentAttributesResponse : IppResponse<OperationAttributes>
{
}
