using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
/// Create-Printer-Subscriptions Response.
/// See: RFC 3995 Section 5.1
/// See: PWG 5100.15-2013 Section 4.2
/// </summary>
public class CreatePrinterSubscriptionsResponse : IppResponse<OperationAttributes>
{
    /// <summary>
    /// The subscription-attributes IPP attributes.
    /// See: RFC 3995 Section 5.1
    /// </summary>
    public SubscriptionDescriptionAttributes[]? SubscriptionsAttributes { get; set; }
}
