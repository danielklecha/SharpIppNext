using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;
/// <summary>
/// PWG 5100.5-2024 Section 5.2.1
/// </summary>
public class GetDocumentsRequest : IppRequest<GetDocumentsOperationAttributes>, IIppJobRequest
{
}
