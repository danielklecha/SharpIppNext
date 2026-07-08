namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the configured time source for the System object.
/// See: PWG 5100.22-2025 Section 7.3.26
/// </summary>
public readonly record struct SystemTimeSourceConfigured(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>No time source is configured. See: PWG 5100.22-2025 Section 7.3.26</summary>
    public static readonly SystemTimeSourceConfigured None = new("none");
    /// <summary>NTP (Network Time Protocol) is used as the time source. See: PWG 5100.22-2025 Section 7.3.26</summary>
    public static readonly SystemTimeSourceConfigured Ntp = new("ntp");
    /// <summary>SNTP (Simple Network Time Protocol) is used as the time source. See: PWG 5100.22-2025 Section 7.3.26</summary>
    public static readonly SystemTimeSourceConfigured Sntp = new("sntp");

    public override string ToString() => Value;
    public static implicit operator string(SystemTimeSourceConfigured value) => value.Value;
    public static implicit operator SystemTimeSourceConfigured(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
