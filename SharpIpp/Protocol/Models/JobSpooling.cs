namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the job spooling behavior.
/// See: PWG 5100.1-2022 Section 6.11
/// </summary>
public readonly record struct JobSpooling(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// The Printer spools the entire job before processing it.
    /// See: PWG 5100.1-2022 Section 6.11
    /// </summary>
    public static readonly JobSpooling Spool = new("spool");

    /// <summary>
    /// The Printer processes the job as it receives the document data (streaming).
    /// See: PWG 5100.1-2022 Section 6.11
    /// </summary>
    public static readonly JobSpooling Stream = new("stream");

    /// <summary>
    /// The Printer automatically selects spool or stream based on available resources.
    /// See: PWG 5100.1-2022 Section 6.11
    /// </summary>
    public static readonly JobSpooling Automatic = new("automatic");

    public override string ToString() => Value;
    public static implicit operator string(JobSpooling bin) => bin.Value;
    public static explicit operator JobSpooling(string value) => new(value);
}
