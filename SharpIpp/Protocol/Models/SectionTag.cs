namespace SharpIpp.Protocol.Models
{
    /// <summary>
    ///     https://tools.ietf.org/html/rfc8010#section-3.5.1
    /// </summary>
    public enum SectionTag : byte
    {
        /// <summary>
        ///     Reserved
        /// </summary>
        Reserved = 0x00,

        /// <summary>
        ///     operation-attributes-tag
        /// </summary>
        OperationAttributesTag = 0x01,

        /// <summary>
        ///     job-attributes-tag
        /// </summary>
        JobAttributesTag = 0x02,

        /// <summary>
        ///     end-of-attributes-tag
        /// </summary>
        EndOfAttributesTag = 0x03,

        /// <summary>
        ///     printer-attributes-tag
        /// </summary>
        PrinterAttributesTag = 0x04,

        /// <summary>
        ///     unsupported-attributes-tag
        /// </summary>
        UnsupportedAttributesTag = 0x05,

        /// <summary>
        /// subscription-attributes-tag
        /// </summary>
        SubscriptionAttributesTag = 0x06,

        /// <summary>
        /// event-notification-attributes-tag
        /// </summary>
        EventNotificationAttributesTag = 0x07,

        /// <summary>
        /// resource-attributes-tag
        /// </summary>
        ResourceAttributesTag = 0x08,

        /// <summary>
        /// document-attributes-tag
        /// </summary>
        DocumentAttributesTag = 0x09,

        /// <summary>
        /// system-attributes-tag
        /// </summary>
        SystemAttributesTag = 0x0A
    }
}
