namespace SharpIpp.Protocol.Models
{
    /// <summary>
    ///     This REQUIRED Printer attribute identifies the set of supported
    ///     compression algorithms for document data.  Compression only applies
    ///     to the document data; compression does not apply to the encoding of
    ///     the IPP operation itself.  The supported values are used to validate
    ///     the client supplied "compression" operation attributes in Print-Job,
    ///     Send-Document, and Send-URI requests.
    ///     https://tools.ietf.org/html/rfc2911#section-4.4.32
    /// </summary>
    public enum Compression
    {
        Unsupported,

        /// <summary>
        ///     no compression is used.
        /// </summary>
        None,

        /// <summary>
        ///     ZIP public domain inflate/deflate) compression technology
        ///     in RFC 1951
        /// </summary>
        Deflate,

        /// <summary>
        ///     GNU zip compression technology described in RFC 1952
        /// </summary>
        Gzip,
    }
}
