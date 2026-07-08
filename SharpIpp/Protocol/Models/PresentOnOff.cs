namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the presence or on/off state of a device or subunit.
/// See: PWG 5100.13-2023 Section 7.1
/// </summary>
public readonly record struct PresentOnOff(string Value, bool IsValue = true) : ISmartEnum
{
    public static readonly PresentOnOff Other = new("other");
    public static readonly PresentOnOff On = new("on");
    public static readonly PresentOnOff Off = new("off");
    public static readonly PresentOnOff NotPresent = new("notPresent");

    public override string ToString() => Value;
    public static implicit operator string(PresentOnOff bin) => bin.Value;
    public static implicit operator PresentOnOff(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
