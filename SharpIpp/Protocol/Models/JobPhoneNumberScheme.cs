namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>job-phone-number-scheme-supported</c> attribute.
/// See: PWG 5100.3-2023 Section 5.3.26
/// </summary>
public readonly record struct JobPhoneNumberScheme(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// See: PWG 5100.3-2023 Section 5.3.26
    /// </summary>
    public static readonly JobPhoneNumberScheme Tel = new("tel");

    /// <summary>
    /// See: RFC 2806 (optional in implementation)
    /// </summary>
    public static readonly JobPhoneNumberScheme Fax = new("fax");

    public override string ToString() => Value;
    public static implicit operator string(JobPhoneNumberScheme bin) => bin.Value;
    public static explicit operator JobPhoneNumberScheme(string value) => new(value);
}
