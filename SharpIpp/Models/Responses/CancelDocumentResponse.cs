using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using SharpIpp.Models.Requests;

namespace SharpIpp.Models.Responses;
/// <summary>
/// PWG 5100.5-2024 Section 5.1.1.2
/// </summary>
[IppResponse(IppOperation.CancelDocument)]
public class CancelDocumentResponse : IppResponse<OperationAttributes>
{
}
