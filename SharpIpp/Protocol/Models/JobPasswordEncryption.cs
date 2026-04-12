namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>job-password-encryption</c> keyword values.
/// See: PWG 5100.11-2024 Section 5.2.3
/// </summary>
public readonly record struct JobPasswordEncryption(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly JobPasswordEncryption None = new("none");
    /// <summary>
    /// Obsolete legacy compatibility value.
    /// See: IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.
    /// </summary>
    public static readonly JobPasswordEncryption Md2 = new("md2");
    /// <summary>
    /// Obsolete legacy compatibility value.
    /// See: IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.
    /// </summary>
    public static readonly JobPasswordEncryption Md4 = new("md4");
    /// <summary>
    /// Obsolete legacy compatibility value.
    /// See: IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.
    /// </summary>
    public static readonly JobPasswordEncryption Md5 = new("md5");
    /// <summary>
    /// Obsolete legacy compatibility value.
    /// See: IPP Enterprise Printing Extensions v2.0 (PWG 5100.11-2024) Section 9.2.
    /// </summary>
    public static readonly JobPasswordEncryption Sha = new("sha");
    public static readonly JobPasswordEncryption Sha2224 = new("sha2-224");
    public static readonly JobPasswordEncryption Sha2256 = new("sha2-256");
    public static readonly JobPasswordEncryption Sha2384 = new("sha2-384");
    public static readonly JobPasswordEncryption Sha2512 = new("sha2-512");
    public static readonly JobPasswordEncryption Sha2512224 = new("sha2-512_224");
    public static readonly JobPasswordEncryption Sha2512256 = new("sha2-512_256");
    public static readonly JobPasswordEncryption Sha3224 = new("sha3-224");
    public static readonly JobPasswordEncryption Sha3256 = new("sha3-256");
    public static readonly JobPasswordEncryption Sha3384 = new("sha3-384");
    public static readonly JobPasswordEncryption Sha3512 = new("sha3-512");
    public static readonly JobPasswordEncryption Sha3512224 = new("sha3-512_224");
    public static readonly JobPasswordEncryption Sha3512256 = new("sha3-512_256");
    public static readonly JobPasswordEncryption Shake256 = new("shake-256");
    public static readonly JobPasswordEncryption Shake512 = new("shake-512");

    public override string ToString() => Value;
    public static implicit operator string(JobPasswordEncryption value) => value.Value;
    public static explicit operator JobPasswordEncryption(string value) => new(value);
}
