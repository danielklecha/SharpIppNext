namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the job spooling.
/// See: PWG 5100.1-2022 Section 6.11
/// </summary>
public readonly record struct JobSpooling(string Value)
{
    public static readonly JobSpooling Spool = new("spool");
    public static readonly JobSpooling Stream = new("stream");
    public static readonly JobSpooling Automatic = new("automatic");

    public override string ToString() => Value;
    public static implicit operator string(JobSpooling bin) => bin.Value;
    public static explicit operator JobSpooling(string value) => new(value);
}
