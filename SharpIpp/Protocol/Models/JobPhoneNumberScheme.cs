namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the job-phone-number-scheme-supported attribute.
/// </summary>
public readonly record struct JobPhoneNumberScheme(string Value)
{
    public static readonly JobPhoneNumberScheme Tel = new("tel");
    public static readonly JobPhoneNumberScheme Uri = new("uri");
    public static readonly JobPhoneNumberScheme Fax = new("fax");

    public override string ToString() => Value;
    public static implicit operator string(JobPhoneNumberScheme bin) => bin.Value;
    public static explicit operator JobPhoneNumberScheme(string value) => new(value);
}
