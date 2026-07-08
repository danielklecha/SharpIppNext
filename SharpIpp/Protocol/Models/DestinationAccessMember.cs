namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>destination-accesses</code>.
/// See: PWG 5100.17-2014 Section 8.1.2
/// </summary>
public readonly record struct DestinationAccessMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// The access PIN for the destination.
    /// See: PWG 5100.17-2014 Section 8.1.2
    /// </summary>
    public static readonly DestinationAccessMember AccessPin = new("access-pin");

    /// <summary>
    /// The access user name for the destination.
    /// See: PWG 5100.17-2014 Section 8.1.2
    /// </summary>
    public static readonly DestinationAccessMember AccessUserName = new("access-user-name");

    /// <summary>
    /// The access password for the destination.
    /// See: PWG 5100.17-2014 Section 8.1.2
    /// </summary>
    public static readonly DestinationAccessMember AccessPassword = new("access-password");

    /// <summary>
    /// The URI of the destination.
    /// See: PWG 5100.17-2014 Section 8.1.2
    /// </summary>
    public static readonly DestinationAccessMember DestinationUri = new("destination-uri");

    /// <summary>
    /// The network type of the destination.
    /// See: PWG 5100.17-2014 Section 8.1.2
    /// </summary>
    public static readonly DestinationAccessMember NetworkType = new("network-type");

    /// <summary>
    /// Whether it is acceptable to forward the destination URI.
    /// See: PWG 5100.17-2014 Section 8.1.2
    /// </summary>
    public static readonly DestinationAccessMember OkToForwardUri = new("ok-to-forward-uri");

    public override string ToString() => Value;
    public static implicit operator string(DestinationAccessMember value) => value.Value;
    public static implicit operator DestinationAccessMember(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
