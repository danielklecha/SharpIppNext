namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the supported member attribute names for <code>destination-uris</code>.
/// See: PWG 5100.15-2013 Section 7.4.5
/// </summary>
public readonly record struct DestinationUrisMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The URI of the destination (fax, scan, email, etc.).
    /// See: PWG 5100.15-2013 Section 7.2.3.1
    /// </summary>
    public static readonly DestinationUrisMember DestinationUri = new("destination-uri");

    /// <summary>
    /// Additional numbers to dial after the destination has connected.
    /// See: PWG 5100.15-2013 Section 7.2.3.2
    /// </summary>
    public static readonly DestinationUrisMember PostDialString = new("post-dial-string");

    /// <summary>
    /// Additional numbers to dial before the destination number is dialed.
    /// See: PWG 5100.15-2013 Section 7.2.3.3
    /// </summary>
    public static readonly DestinationUrisMember PreDialString = new("pre-dial-string");

    /// <summary>
    /// Additional numbers (typically an extension) to dial after any post-dial-string.
    /// See: PWG 5100.15-2013 Section 7.2.3.4
    /// </summary>
    public static readonly DestinationUrisMember T33Subaddress = new("t33-subaddress");

    public override string ToString() => Value;
    public static implicit operator string(DestinationUrisMember value) => value.Value;
    public static implicit operator DestinationUrisMember(string value) => new(value);
}
