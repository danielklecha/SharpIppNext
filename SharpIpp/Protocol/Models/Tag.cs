namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// IPP attribute value tag codes used to identify the syntax of an attribute value.
    /// See: RFC 8010 Section 3.5.2
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "<Pending>")]
    public enum Tag : byte
    {
        /// <summary>
        /// unsupported — the attribute is not supported by the Printer.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        Unsupported = 0x10,

        /// <summary>
        /// unknown — the attribute is supported but the value is unknown.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        Unknown = 0x12,

        /// <summary>
        /// no-value — the attribute is supported but has no value.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        NoValue = 0x13,

        /// <summary>
        /// Unassigned integer tag 0x20.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        IntegerUnassigned20 = 0x20,

        /// <summary>
        /// integer — a 32-bit signed integer value.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        Integer = 0x21,

        /// <summary>
        /// boolean — a boolean value (0x00 = false, 0x01 = true).
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        Boolean = 0x22,

        /// <summary>
        /// enum — a 32-bit signed integer value used as an enumeration.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        Enum = 0x23,

        /// <summary>
        /// Unassigned integer tag 0x24.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        IntegerUnassigned24 = 0x24,

        /// <summary>
        /// Unassigned integer tag 0x25.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        IntegerUnassigned25 = 0x25,

        /// <summary>
        /// Unassigned integer tag 0x26.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        IntegerUnassigned26 = 0x26,

        /// <summary>
        /// Unassigned integer tag 0x27.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        IntegerUnassigned27 = 0x27,

        /// <summary>
        /// Unassigned integer tag 0x28.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        IntegerUnassigned28 = 0x28,

        /// <summary>
        /// Unassigned integer tag 0x29.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        IntegerUnassigned29 = 0x29,

        /// <summary>
        /// Unassigned integer tag 0x2A.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        IntegerUnassigned2A = 0x2A,

        /// <summary>
        /// Unassigned integer tag 0x2B.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        IntegerUnassigned2B = 0x2B,

        /// <summary>
        /// Unassigned integer tag 0x2C.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        IntegerUnassigned2C = 0x2C,

        /// <summary>
        /// Unassigned integer tag 0x2D.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        IntegerUnassigned2D = 0x2D,

        /// <summary>
        /// Unassigned integer tag 0x2E.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        IntegerUnassigned2E = 0x2E,

        /// <summary>
        /// Unassigned integer tag 0x2F.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        IntegerUnassigned2F = 0x2F,

        /// <summary>
        /// octetString with an unspecified format.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        OctetStringWithAnUnspecifiedFormat = 0x30,

        /// <summary>
        /// dateTime — a date and time value encoded as an SNMP DateAndTime.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        DateTime = 0x31,

        /// <summary>
        /// resolution — a printer resolution value (cross-feed, feed, units).
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        Resolution = 0x32,

        /// <summary>
        /// rangeOfInteger — a range of integers (lower bound, upper bound).
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        RangeOfInteger = 0x33,

        /// <summary>
        /// begCollection — marks the beginning of a collection attribute value.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        BegCollection = 0x34,

        /// <summary>
        /// textWithLanguage — a text string with an associated natural language tag.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        TextWithLanguage = 0x35,

        /// <summary>
        /// nameWithLanguage — a name string with an associated natural language tag.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        NameWithLanguage = 0x36,

        /// <summary>
        /// endCollection — marks the end of a collection attribute value.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        EndCollection = 0x37,

        /// <summary>
        /// Unassigned octetString tag 0x38.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        OctetStringUnassigned38 = 0x38,

        /// <summary>
        /// Unassigned octetString tag 0x39.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        OctetStringUnassigned39 = 0x39,

        /// <summary>
        /// Unassigned octetString tag 0x3A.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        OctetStringUnassigned3A = 0x3a,

        /// <summary>
        /// Unassigned octetString tag 0x3B.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        OctetStringUnassigned3B = 0x3b,

        /// <summary>
        /// Unassigned octetString tag 0x3C.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        OctetStringUnassigned3C = 0x3c,

        /// <summary>
        /// Unassigned octetString tag 0x3D.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        OctetStringUnassigned3D = 0x3d,

        /// <summary>
        /// Unassigned octetString tag 0x3E.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        OctetStringUnassigned3E = 0x3e,

        /// <summary>
        /// Unassigned octetString tag 0x3F.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        OctetStringUnassigned3F = 0x3f,

        /// <summary>
        /// Unassigned string tag 0x40.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned40 = 0x40,

        /// <summary>
        /// textWithoutLanguage — a text string without an associated natural language tag.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        TextWithoutLanguage = 0x41,

        /// <summary>
        /// nameWithoutLanguage — a name string without an associated natural language tag.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        NameWithoutLanguage = 0x42,

        /// <summary>
        /// Unassigned string tag 0x43.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned43 = 0x43,

        /// <summary>
        /// keyword — a keyword string value.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        Keyword = 0x44,

        /// <summary>
        /// uri — a URI value.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        Uri = 0x45,

        /// <summary>
        /// uriScheme — a URI scheme value (e.g., "ipp", "http").
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        UriScheme = 0x46,

        /// <summary>
        /// charset — a charset name value (e.g., "utf-8").
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        Charset = 0x47,

        /// <summary>
        /// naturalLanguage — a natural language tag value (e.g., "en-us").
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        NaturalLanguage = 0x48,

        /// <summary>
        /// mimeMediaType — a MIME media type value (e.g., "application/pdf").
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        MimeMediaType = 0x49,

        /// <summary>
        /// memberAttrName — the name of a collection member attribute.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        MemberAttrName = 0x4a,

        /// <summary>
        /// Unassigned string tag 0x4B.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned4B = 0x4b,

        /// <summary>
        /// Unassigned string tag 0x4C.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned4C = 0x4c,

        /// <summary>
        /// Unassigned string tag 0x4D.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned4D = 0x4d,

        /// <summary>
        /// Unassigned string tag 0x4E.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned4E = 0x4e,

        /// <summary>
        /// Unassigned string tag 0x4F.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned4F = 0x4f,

        /// <summary>
        /// Unassigned string tag 0x50.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned50 = 0x50,

        /// <summary>
        /// Unassigned string tag 0x51.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned51 = 0x51,

        /// <summary>
        /// Unassigned string tag 0x52.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned52 = 0x52,

        /// <summary>
        /// Unassigned string tag 0x53.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned53 = 0x53,

        /// <summary>
        /// Unassigned string tag 0x54.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned54 = 0x54,

        /// <summary>
        /// Unassigned string tag 0x55.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned55 = 0x55,

        /// <summary>
        /// Unassigned string tag 0x56.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned56 = 0x56,

        /// <summary>
        /// Unassigned string tag 0x57.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned57 = 0x57,

        /// <summary>
        /// Unassigned string tag 0x58.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned58 = 0x58,

        /// <summary>
        /// Unassigned string tag 0x59.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned59 = 0x59,

        /// <summary>
        /// Unassigned string tag 0x5A.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned5A = 0x5a,

        /// <summary>
        /// Unassigned string tag 0x5B.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned5B = 0x5b,

        /// <summary>
        /// Unassigned string tag 0x5C.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned5C = 0x5c,

        /// <summary>
        /// Unassigned string tag 0x5D.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned5D = 0x5d,

        /// <summary>
        /// Unassigned string tag 0x5E.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned5E = 0x5e,

        /// <summary>
        /// Unassigned string tag 0x5F.
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        StringUnassigned5F = 0x5f,

        /// <summary>
        /// Extended tag indicator; value length begins with a 4-byte extended tag id (RFC 8010 Section 3.5.2)
        /// See: RFC 8010 Section 3.5.2
        /// </summary>
        Extended = 0x7f,
    }
}
