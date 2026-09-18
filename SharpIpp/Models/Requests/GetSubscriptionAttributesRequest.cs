using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Get-Subscription-Attributes operation.
/// See: PWG 5100.22-2025 Section 8.1
/// See: RFC 3995 Section 5.7
/// </summary>
[IppRequest(IppOperation.GetSubscriptionAttributes)]
public class GetSubscriptionAttributesRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
