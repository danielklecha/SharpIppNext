using System;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Attributes describing a Subscription object.
    /// See: RFC 3995
    /// See: RFC 3996
    /// </summary>
    public class SubscriptionDescriptionAttributes
    {
        /// <summary>
        /// notify-subscription-id
        /// See: RFC 3995 Section 5.4.1
        /// </summary>
        public int? NotifySubscriptionId { get; set; }

        /// <summary>
        /// notify-pull-method
        /// See: RFC 3996 Section 5.1
        /// </summary>
        public NotifyPullMethod? NotifyPullMethod { get; set; }

        /// <summary>
        /// notify-events
        /// See: RFC 3995 Section 5.3.3
        /// </summary>
        public NotifyEvent[]? NotifyEvents { get; set; }

        /// <summary>
        /// notify-lease-duration
        /// See: RFC 3995 Section 5.3.6
        /// </summary>
        public int? NotifyLeaseDuration { get; set; }

        /// <summary>
        /// notify-recipient-uri
        /// See: RFC 3995 Section 5.3.1
        /// </summary>
        public Uri? NotifyRecipientUri { get; set; }

        /// <summary>
        /// notify-user-data
        /// See: RFC 3995 Section 5.3.5
        /// </summary>
        public OctetString? NotifyUserData { get; set; }

        /// <summary>
        /// notify-subscriber-user-name
        /// See: RFC 3995 Section 5.4.6
        /// </summary>
        public string? NotifySubscriberUserName { get; set; }

        /// <summary>
        /// notify-charset
        /// See: RFC 3995 Section 5.3.7
        /// </summary>
        public string? NotifyCharset { get; set; }

        /// <summary>
        /// notify-natural-language
        /// See: RFC 3995 Section 5.3.8
        /// </summary>
        public string? NotifyNaturalLanguage { get; set; }
    }
}
