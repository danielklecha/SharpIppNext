using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Cancel-Resource operation.
/// See: PWG 5100.22-2025 Section 6.2.1
/// </summary>
[IppRequest(IppOperation.CancelResource)]
public class CancelResourceRequest : IppRequest<CancelResourceOperationAttributes>, IIppSystemRequest
{
}
