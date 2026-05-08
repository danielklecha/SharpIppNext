namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>system-settable-attributes-supported</code>.
/// See: PWG 5100.22-2025 Section 7.2.42
/// </summary>
public readonly record struct SystemSettableAttribute(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly SystemSettableAttribute SystemDnsSdName = new("system-dns-sd-name");
    public static readonly SystemSettableAttribute SystemInfo = new("system-info");
    public static readonly SystemSettableAttribute SystemLocation = new("system-location");
    public static readonly SystemSettableAttribute SystemName = new("system-name");
    public static readonly SystemSettableAttribute SystemGeoLocation = new("system-geo-location");

    public override string ToString() => Value;
    public static implicit operator string(SystemSettableAttribute value) => value.Value;
    public static explicit operator SystemSettableAttribute(string value) => new(value);
}
