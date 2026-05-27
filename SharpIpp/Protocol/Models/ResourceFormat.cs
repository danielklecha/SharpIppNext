namespace SharpIpp.Protocol.Models;

/// <summary>
/// Identifies the format of the resource.
/// See: PWG 5100.22-2025 Section 6.2.2
/// </summary>
public readonly record struct ResourceFormat(string Value, bool IsValue = true) : ISmartEnum
{
    public override string ToString() => Value;
    public static implicit operator string(ResourceFormat bin) => bin.Value;
    public static implicit operator ResourceFormat(string value) => new(value);
}
