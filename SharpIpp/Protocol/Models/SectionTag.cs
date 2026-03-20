namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// See:  Section 3.5.1
    /// </summary>
    public enum SectionTag : byte
    {
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved = 0x00,

        /// <summary>
        /// operation-attributes-tag
        /// </summary>
        OperationAttributesTag = 0x01,

        /// <summary>
        /// job-attributes-tag
        /// </summary>
        JobAttributesTag = 0x02,

        /// <summary>
        /// end-of-attributes-tag
        /// </summary>
        EndOfAttributesTag = 0x03,

        /// <summary>
        /// printer-attributes-tag
        /// </summary>
        PrinterAttributesTag = 0x04,

        /// <summary>
        /// unsupported-attributes-tag
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
        SystemAttributesTag = 0x0A,

        /// <summary>
        /// Future extension group tags (0x0B-0x0F) as per RFC 8010 Section 3.5.1.
        /// Parsed to allow forward-compatible decoding; currently not surfaced on the public model.
        /// </summary>
        Future0B = 0x0B,
        Future0C = 0x0C,
        Future0D = 0x0D,
        Future0E = 0x0E,
        Future0F = 0x0F,
    }
}
