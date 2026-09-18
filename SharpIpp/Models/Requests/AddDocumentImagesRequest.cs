using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Add-Document-Images operation.
/// See: PWG 5100.15-2013 Section 6.1
/// </summary>
[IppRequest(IppOperation.AddDocumentImages)]
public class AddDocumentImagesRequest : IppRequest<AddDocumentImagesOperationAttributes>, IIppJobRequest
{
}
