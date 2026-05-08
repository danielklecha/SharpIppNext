namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>output-attributes</code>.
/// See: PWG 5100.18-2015 Section 6.2.8
/// </summary>
public readonly record struct OutputAttributesMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly OutputAttributesMember OutputBin = new("output-bin");
    public static readonly OutputAttributesMember OutputDevice = new("output-device");

    public override string ToString() => Value;
    public static implicit operator string(OutputAttributesMember value) => value.Value;
    public static explicit operator OutputAttributesMember(string value) => new(value);
}
