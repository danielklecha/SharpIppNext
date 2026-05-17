namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// IPP attribute group delimiter tags used to separate attribute groups in an IPP message.
    /// See: RFC 8010 Section 3.5.1
    /// </summary>
    public enum SectionTag : byte
    {
        /// <summary>
        /// Reserved delimiter tag (0x00).
        /// See: RFC 8010 Section 3.5.1
        /// </summary>
        Reserved = 0x00,

        /// <summary>
        /// operation-attributes-tag — marks the start of the operation attributes group.
        /// See: RFC 8010 Section 3.5.1
        /// </summary>
        OperationAttributesTag = 0x01,

        /// <summary>
        /// job-attributes-tag — marks the start of the job attributes group.
        /// See: RFC 8010 Section 3.5.1
        /// </summary>
        JobAttributesTag = 0x02,

        /// <summary>
        /// end-of-attributes-tag — marks the end of all attribute groups.
        /// See: RFC 8010 Section 3.5.1
        /// </summary>
        EndOfAttributesTag = 0x03,

        /// <summary>
        /// printer-attributes-tag — marks the start of the printer attributes group.
        /// See: RFC 8010 Section 3.5.1
        /// </summary>
        PrinterAttributesTag = 0x04,

        /// <summary>
        /// unsupported-attributes-tag — marks the start of the unsupported attributes group.
        /// See: RFC 8010 Section 3.5.1
        /// </summary>
        UnsupportedAttributesTag = 0x05,

        /// <summary>
        /// subscription-attributes-tag — marks the start of the subscription attributes group.
        /// See: RFC 3995 Section 7.1
        /// </summary>
        SubscriptionAttributesTag = 0x06,

        /// <summary>
        /// event-notification-attributes-tag — marks the start of the event notification attributes group.
        /// See: RFC 3995 Section 7.1
        /// </summary>
        EventNotificationAttributesTag = 0x07,

        /// <summary>
        /// resource-attributes-tag — marks the start of the resource attributes group.
        /// See: PWG 5100.22-2025 Section 8.1
        /// </summary>
        ResourceAttributesTag = 0x08,

        /// <summary>
        /// document-attributes-tag — marks the start of the document attributes group.
        /// See: PWG 5100.5-2024 Section 8.1
        /// </summary>
        DocumentAttributesTag = 0x09,

        /// <summary>
        /// system-attributes-tag — marks the start of the system attributes group.
        /// See: PWG 5100.22-2025 Section 8.1
        /// </summary>
        SystemAttributesTag = 0x0A,

        /// <summary>
        /// Future extension group tag 0x0B, reserved for forward-compatible decoding.
        /// See: RFC 8010 Section 3.5.1
        /// </summary>
        Future0B = 0x0B,

        /// <summary>
        /// Future extension group tag 0x0C, reserved for forward-compatible decoding.
        /// See: RFC 8010 Section 3.5.1
        /// </summary>
        Future0C = 0x0C,

        /// <summary>
        /// Future extension group tag 0x0D, reserved for forward-compatible decoding.
        /// See: RFC 8010 Section 3.5.1
        /// </summary>
        Future0D = 0x0D,

        /// <summary>
        /// Future extension group tag 0x0E, reserved for forward-compatible decoding.
        /// See: RFC 8010 Section 3.5.1
        /// </summary>
        Future0E = 0x0E,

        /// <summary>
        /// Future extension group tag 0x0F, reserved for forward-compatible decoding.
        /// See: RFC 8010 Section 3.5.1
        /// </summary>
        Future0F = 0x0F,
    }
}
