namespace SharpIpp.Protocol.Models;

/// <summary>
/// This REQUIRED Printer attribute identifies the set of supported
/// compression algorithms for document data.
/// See: RFC 2911 Section 4.4.32
/// </summary>
public readonly record struct Compression(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// no compression is used.
    /// </summary>
    public static readonly Compression None = new("none");

    /// <summary>
    /// ZIP public domain inflate/deflate) compression technology
    /// in RFC 1951
    /// </summary>
    public static readonly Compression Deflate = new("deflate");

    /// <summary>
    /// GNU zip compression technology described in RFC 1952
    /// </summary>
    public static readonly Compression Gzip = new("gzip");

    /// <summary>
    /// UNIX compression technology in RFC 1977
    /// See: RFC 2911 Section 4.4.32
    /// </summary>
    public static readonly Compression Compress = new("compress");

    public override string ToString() => Value;
    public static implicit operator string(Compression bin) => bin.Value;
    public static explicit operator Compression(string value) => new(value);
}
