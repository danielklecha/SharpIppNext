namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of X.509 certificate supported for an output device.
/// See: PWG 5100.22-2025 Section 7.2.14
/// </summary>
public readonly record struct X509Type(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly X509Type Internal = new("internal");
    public static readonly X509Type OutputDevice = new("output-device");
    public static readonly X509Type Resource = new("resource");

    public override string ToString() => Value;
    public static implicit operator string(X509Type value) => value.Value;
    public static explicit operator X509Type(string value) => new(value);
}
