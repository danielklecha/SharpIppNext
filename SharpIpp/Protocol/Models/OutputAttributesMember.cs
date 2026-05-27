namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>output-attributes</code>.
/// See: PWG 5100.17-2014 Section 8.1.7
/// </summary>
public readonly record struct OutputAttributesMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>The output-bin member attribute. See: PWG 5100.17-2014 Section 8.1.7</summary>
    public static readonly OutputAttributesMember OutputBin = new("output-bin");
    /// <summary>The output-device member attribute. See: PWG 5100.17-2014 Section 8.1.7</summary>
    public static readonly OutputAttributesMember OutputDevice = new("output-device");

    public override string ToString() => Value;
    public static implicit operator string(OutputAttributesMember value) => value.Value;
    public static implicit operator OutputAttributesMember(string value) => new(value);
}
