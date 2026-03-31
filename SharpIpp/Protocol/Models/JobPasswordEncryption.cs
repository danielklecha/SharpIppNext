namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>job-password-encryption</c> keyword values.
/// See: PWG 5100.11-2024 Section 5.2.3
/// </summary>
public readonly record struct JobPasswordEncryption(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly JobPasswordEncryption None = new("none");
    public static readonly JobPasswordEncryption Sha2256 = new("sha2-256");
    public static readonly JobPasswordEncryption Sha2384 = new("sha2-384");
    public static readonly JobPasswordEncryption Sha2512 = new("sha2-512");
    public static readonly JobPasswordEncryption Sha3256 = new("sha3-256");
    public static readonly JobPasswordEncryption Sha3384 = new("sha3-384");
    public static readonly JobPasswordEncryption Sha3512 = new("sha3-512");

    public override string ToString() => Value;
    public static implicit operator string(JobPasswordEncryption value) => value.Value;
    public static explicit operator JobPasswordEncryption(string value) => new(value);
}
