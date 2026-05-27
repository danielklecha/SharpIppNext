namespace SharpIpp.Protocol.Models;

/// <summary>
/// Identifies the charset (coded character set and encoding method).
/// </summary>
public readonly record struct Charset(string Value, bool IsValue = true) : ISmartEnum
{
    /// <summary>
    /// UTF-8 encoding.
    /// </summary>
    public static readonly Charset Utf8 = new("utf-8");

    /// <summary>
    /// US-ASCII encoding.
    /// </summary>
    public static readonly Charset UsAscii = new("us-ascii");

    public override string ToString() => Value;
    public static implicit operator string(Charset bin) => bin.Value;
    public static implicit operator Charset(string value) => new(value);
}
