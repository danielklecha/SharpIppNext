namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of X.509 certificate supported for an output device.
/// See: PWG 5100.22-2025 Section 7.2.19
/// </summary>
public readonly record struct X509Type(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>An internal X.509 certificate. See: PWG 5100.22-2025 Section 7.2.14</summary>
    public static readonly X509Type Internal = new("internal");
    /// <summary>An output device X.509 certificate. See: PWG 5100.22-2025 Section 7.2.14</summary>
    public static readonly X509Type OutputDevice = new("output-device");
    /// <summary>A resource X.509 certificate. See: PWG 5100.22-2025 Section 7.2.14</summary>
    public static readonly X509Type Resource = new("resource");

    public override string ToString() => Value;
    public static implicit operator string(X509Type value) => value.Value;
    public static implicit operator X509Type(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
