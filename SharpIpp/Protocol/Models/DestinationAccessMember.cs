namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>destination-accesses</code>.
/// See: PWG 5100.18-2015 Section 6.2.3
/// </summary>
public readonly record struct DestinationAccessMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly DestinationAccessMember AccessPin = new("access-pin");
    public static readonly DestinationAccessMember AccessUserName = new("access-user-name");
    public static readonly DestinationAccessMember AccessPassword = new("access-password");
    public static readonly DestinationAccessMember DestinationUri = new("destination-uri");
    public static readonly DestinationAccessMember NetworkType = new("network-type");
    public static readonly DestinationAccessMember OkToForwardUri = new("ok-to-forward-uri");

    public override string ToString() => Value;
    public static implicit operator string(DestinationAccessMember value) => value.Value;
    public static explicit operator DestinationAccessMember(string value) => new(value);
}
