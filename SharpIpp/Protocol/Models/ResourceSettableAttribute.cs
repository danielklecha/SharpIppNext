namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>resource-settable-attributes-supported</code>.
/// See: PWG 5100.22-2025 Section 7.2.40
/// </summary>
public readonly record struct ResourceSettableAttribute(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>The resource-info attribute. See: PWG 5100.22-2025 Section 7.2.40</summary>
    public static readonly ResourceSettableAttribute ResourceInfo = new("resource-info");
    /// <summary>The resource-name attribute. See: PWG 5100.22-2025 Section 7.2.40</summary>
    public static readonly ResourceSettableAttribute ResourceName = new("resource-name");

    public override string ToString() => Value;
    public static implicit operator string(ResourceSettableAttribute value) => value.Value;
    public static explicit operator ResourceSettableAttribute(string value) => new(value);
}
