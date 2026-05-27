namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>system-settable-attributes-supported</code>.
/// See: PWG 5100.22-2025 Section 7.2.42
/// </summary>
public readonly record struct SystemSettableAttribute(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>The system-dns-sd-name attribute. See: PWG 5100.22-2025 Section 7.2.42</summary>
    public static readonly SystemSettableAttribute SystemDnsSdName = new("system-dns-sd-name");
    /// <summary>The system-info attribute. See: PWG 5100.22-2025 Section 7.2.42</summary>
    public static readonly SystemSettableAttribute SystemInfo = new("system-info");
    /// <summary>The system-location attribute. See: PWG 5100.22-2025 Section 7.2.42</summary>
    public static readonly SystemSettableAttribute SystemLocation = new("system-location");
    /// <summary>The system-name attribute. See: PWG 5100.22-2025 Section 7.2.42</summary>
    public static readonly SystemSettableAttribute SystemName = new("system-name");
    /// <summary>The system-geo-location attribute. See: PWG 5100.22-2025 Section 7.2.42</summary>
    public static readonly SystemSettableAttribute SystemGeoLocation = new("system-geo-location");

    public override string ToString() => Value;
    public static implicit operator string(SystemSettableAttribute value) => value.Value;
    public static implicit operator SystemSettableAttribute(string value) => new(value);
}
