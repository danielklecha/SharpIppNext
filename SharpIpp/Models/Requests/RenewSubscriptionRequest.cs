using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Renew-Subscription operation.
/// See: PWG 5100.22-2025 Section 8.1
/// See: RFC 3995 Section 5.2
/// </summary>
[IppRequest(IppOperation.RenewSubscription)]
public class RenewSubscriptionRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
