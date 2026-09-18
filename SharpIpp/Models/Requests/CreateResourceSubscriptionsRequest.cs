using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Create-Resource-Subscriptions operation.
/// See: PWG 5100.22-2025 Section 6.2.2
/// </summary>
[IppRequest(IppOperation.CreateResourceSubscriptions)]
public class CreateResourceSubscriptionsRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
