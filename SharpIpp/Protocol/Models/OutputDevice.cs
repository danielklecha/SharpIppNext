namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the output-device.
/// See: PWG 5100.7-2023 Section 6.3.2
///
/// PWG defines this as name(127), so there is no global fixed value set.
/// </summary>
public readonly record struct OutputDevice(string Value, bool IsValue = true) : ISmartEnum 
{
    public override string ToString() => Value;
    public static implicit operator string(OutputDevice device) => device.Value;
    public static explicit operator OutputDevice(string value) => new(value);
}
