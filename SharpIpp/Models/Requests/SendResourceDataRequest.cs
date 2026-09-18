using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Send-Resource-Data operation.
/// See: PWG 5100.22-2025 Section 6.2.5
/// </summary>
[IppRequest(IppOperation.SendResourceData)]
public class SendResourceDataRequest : IppRequest<SendResourceDataOperationAttributes>, IIppSystemRequest
{
}
