namespace SharpIpp.Protocol.Models;

/// <summary>
/// This attribute determines which job start/end sheet(s), if any, MUST
/// be printed with a job.
/// See: RFC 2911 Section 4.2.3
/// </summary>
public readonly record struct JobSheets(string Value)
{
    /// <summary>
    /// no job sheet is printed
    /// </summary>
    public static readonly JobSheets None = new("none");

    /// <summary>
    /// one or more site specific standard job sheets are printed
    /// </summary>
    public static readonly JobSheets Standard = new("standard");

    public override string ToString() => Value;
    public static implicit operator string(JobSheets bin) => bin.Value;
    public static explicit operator JobSheets(string value) => new(value);
}
