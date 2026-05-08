namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the configured time source for the System object.
/// See: PWG 5100.22-2025 Section 7.3.26
/// </summary>
public readonly record struct SystemTimeSourceConfigured(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly SystemTimeSourceConfigured None = new("none");
    public static readonly SystemTimeSourceConfigured Ntp = new("ntp");
    public static readonly SystemTimeSourceConfigured Sntp = new("sntp");

    public override string ToString() => Value;
    public static implicit operator string(SystemTimeSourceConfigured value) => value.Value;
    public static explicit operator SystemTimeSourceConfigured(string value) => new(value);
}
