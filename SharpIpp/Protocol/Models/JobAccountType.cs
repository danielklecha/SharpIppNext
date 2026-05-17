namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>job-account-type</c> keyword values.
/// See: PWG 5100.11-2024 Section 5.3.5
/// </summary>
public readonly record struct JobAccountType(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// A general-purpose account type.
    /// See: PWG 5100.11-2024 Section 5.3.5
    /// </summary>
    public static readonly JobAccountType General = new("general");

    /// <summary>
    /// A group account type.
    /// See: PWG 5100.11-2024 Section 5.3.5
    /// </summary>
    public static readonly JobAccountType Group = new("group");

    /// <summary>
    /// No account type is specified.
    /// See: PWG 5100.11-2024 Section 5.3.5
    /// </summary>
    public static readonly JobAccountType None = new("none");

    public override string ToString() => Value;
    public static implicit operator string(JobAccountType value) => value.Value;
    public static explicit operator JobAccountType(string value) => new(value);
}
