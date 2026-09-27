using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Attributes describing a Subscription object.
    /// See: RFC 3995
    /// See: RFC 3996
    /// </summary>
    [IppAttribute]
    public class SubscriptionDescriptionAttributes
    {
        /// <summary>
        /// notify-subscription-id
        /// See: RFC 3995 Section 5.4.1
        /// </summary>
        [IppAttribute(IppAttributeNames.NotifySubscriptionId, Tag.Integer)]
        public IppValue<int>? NotifySubscriptionId { get; set; }

        /// <summary>
        /// notify-pull-method
        /// See: RFC 3996 Section 5.1
        /// </summary>
        [IppAttribute(IppAttributeNames.NotifyPullMethod, Tag.Keyword)]
        public IppValue<NotifyPullMethod>? NotifyPullMethod { get; set; }

        /// <summary>
        /// notify-events
        /// See: RFC 3995 Section 5.3.3
        /// </summary>
        [IppAttribute(IppAttributeNames.NotifyEvents, Tag.Keyword)]
        public IppValue<NotifyEvent[]>? NotifyEvents { get; set; }

        /// <summary>
        /// notify-lease-duration
        /// See: RFC 3995 Section 5.3.6
        /// </summary>
        [IppAttribute(IppAttributeNames.NotifyLeaseDuration, Tag.Integer)]
        public IppValue<int>? NotifyLeaseDuration { get; set; }

        /// <summary>
        /// notify-recipient-uri
        /// See: RFC 3995 Section 5.3.1
        /// </summary>
        [IppAttribute(IppAttributeNames.NotifyRecipientUri, Tag.Uri)]
        public IppValue<Uri>? NotifyRecipientUri { get; set; }

        /// <summary>
        /// notify-user-data
        /// See: RFC 3995 Section 5.3.5
        /// </summary>
        [IppAttribute(IppAttributeNames.NotifyUserData, Tag.OctetStringWithAnUnspecifiedFormat)]
        public IppValue<OctetString>? NotifyUserData { get; set; }

        /// <summary>
        /// notify-subscriber-user-name
        /// See: RFC 3995 Section 5.4.6
        /// </summary>
        [IppAttribute(IppAttributeNames.NotifySubscriberUserName, Tag.NameWithoutLanguage)]
        public IppValue<string>? NotifySubscriberUserName { get; set; }

        /// <summary>
        /// notify-charset
        /// See: RFC 3995 Section 5.3.7
        /// </summary>
        [IppAttribute(IppAttributeNames.NotifyCharset, Tag.Charset)]
        public IppValue<string>? NotifyCharset { get; set; }

        /// <summary>
        /// notify-natural-language
        /// See: RFC 3995 Section 5.3.8
        /// </summary>
        [IppAttribute(IppAttributeNames.NotifyNaturalLanguage, Tag.NaturalLanguage)]
        public IppValue<string>? NotifyNaturalLanguage { get; set; }
    }
}
