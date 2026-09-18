using SharpIpp.Protocol.Models;
using SharpIpp.Mapping;
namespace SharpIpp.Models.Requests;

/// <summary>
/// Create-System-Subscriptions operation.
/// See: PWG 5100.22-2025 Section 6.3.3
/// </summary>
[IppRequest(IppOperation.CreateSystemSubscriptions)]
public class CreateSystemSubscriptionsRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
