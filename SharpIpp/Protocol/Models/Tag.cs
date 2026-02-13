namespace SharpIpp.Protocol.Models
{
    /// <summary>
    ///     https://tools.ietf.org/html/rfc8010#section-3.5.2
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "<Pending>")]
    public enum Tag : byte
    {
        /// <summary>
        ///     unsupported
        /// </summary>
        Unsupported = 0x10,

        /// <summary>
        ///     unknown
        /// </summary>
        Unknown = 0x12,

        /// <summary>
        ///     no-value
        /// </summary>
        NoValue = 0x13,
        IntegerUnassigned20 = 0x20,

        /// <summary>
        ///     integer
        /// </summary>
        Integer = 0x21,

        /// <summary>
        ///     boolean
        /// </summary>
        Boolean = 0x22,

        /// <summary>
        ///     enum
        /// </summary>
        Enum = 0x23,
        IntegerUnassigned24 = 0x24,
        IntegerUnassigned25 = 0x25,
        IntegerUnassigned26 = 0x26,
        IntegerUnassigned27 = 0x27,
        IntegerUnassigned28 = 0x28,
        IntegerUnassigned29 = 0x29,
        IntegerUnassigned2A = 0x2A,
        IntegerUnassigned2B = 0x2B,
        IntegerUnassigned2C = 0x2C,
        IntegerUnassigned2D = 0x2D,
        IntegerUnassigned2E = 0x2E,
        IntegerUnassigned2F = 0x2F,

        /// <summary>
        ///     octetString with an unspecified format
        /// </summary>
        OctetStringWithAnUnspecifiedFormat = 0x30,

        /// <summary>
        ///     dateTime
        /// </summary>
        DateTime = 0x31,

        /// <summary>
        ///     resolution
        /// </summary>
        Resolution = 0x32,

        /// <summary>
        ///     rangeOfInteger
        /// </summary>
        RangeOfInteger = 0x33,

        /// <summary>
        ///     begCollection
        /// </summary>
        BegCollection = 0x34,

        /// <summary>
        ///     textWithLanguage
        /// </summary>
        TextWithLanguage = 0x35,

        /// <summary>
        ///     nameWithLanguage
        /// </summary>
        NameWithLanguage = 0x36,

        /// <summary>
        ///     endCollection
        /// </summary>
        EndCollection = 0x37,
        OctetStringUnassigned38 = 0x38,
        OctetStringUnassigned39 = 0x39,
        OctetStringUnassigned3A = 0x3a,
        OctetStringUnassigned3B = 0x3b,
        OctetStringUnassigned3C = 0x3c,
        OctetStringUnassigned3D = 0x3d,
        OctetStringUnassigned3E = 0x3e,
        OctetStringUnassigned3F = 0x3f,

        StringUnassigned40 = 0x40,

        /// <summary>
        ///     textWithoutLanguage
        /// </summary>
        TextWithoutLanguage = 0x41,

        /// <summary>
        ///     nameWithoutLanguage
        /// </summary>
        NameWithoutLanguage = 0x42,

        StringUnassigned43 = 0x43,

        /// <summary>
        ///     keyword
        /// </summary>
        Keyword = 0x44,

        /// <summary>
        ///     uri
        /// </summary>
        Uri = 0x45,

        /// <summary>
        ///     uriScheme
        /// </summary>
        UriScheme = 0x46,

        /// <summary>
        ///     charset
        /// </summary>
        Charset = 0x47,

        /// <summary>
        ///     naturalLanguage
        /// </summary>
        NaturalLanguage = 0x48,

        /// <summary>
        ///     mimeMediaType
        /// </summary>
        MimeMediaType = 0x49,

        /// <summary>
        ///     memberAttrName
        /// </summary>
        MemberAttrName = 0x4a,
        StringUnassigned4B = 0x4b,
        StringUnassigned4C = 0x4c,
        StringUnassigned4D = 0x4d,
        StringUnassigned4E = 0x4e,
        StringUnassigned4F = 0x4f,
        StringUnassigned50 = 0x50,
        StringUnassigned51 = 0x51,
        StringUnassigned52 = 0x52,
        StringUnassigned53 = 0x53,
        StringUnassigned54 = 0x54,
        StringUnassigned55 = 0x55,
        StringUnassigned56 = 0x56,
        StringUnassigned57 = 0x57,
        StringUnassigned58 = 0x58,
        StringUnassigned59 = 0x59,
        StringUnassigned5A = 0x5a,
        StringUnassigned5B = 0x5b,
        StringUnassigned5C = 0x5c,
        StringUnassigned5D = 0x5d,
        StringUnassigned5E = 0x5e,
        StringUnassigned5F = 0x5f,
    }
}
