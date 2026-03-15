using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Requests;
/// <summary>
/// PWG 5100.5-2024 Section 5.1.1
/// </summary>
public class CancelDocumentRequest : IppRequest<CancelDocumentOperationAttributes>, IIppJobRequest
{
}
