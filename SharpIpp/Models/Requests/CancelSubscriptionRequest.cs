namespace SharpIpp.Models.Requests;

/// <summary>
/// Cancel-Subscription operation.
/// See: PWG 5100.22-2025 Section 8.1
/// See: RFC 3995 Section 6.1
/// </summary>
public class CancelSubscriptionRequest : IppRequest<SystemOperationAttributes>, IIppSystemRequest
{
}
