using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Next-Document-Data operation.
/// See: PWG 5100.17-2014 Section 6.1
/// </summary>
[IppRequest(IppOperation.GetNextDocumentData)]
public class GetNextDocumentDataRequest : IppRequest<GetNextDocumentDataOperationAttributes>, IIppJobRequest
{
}
