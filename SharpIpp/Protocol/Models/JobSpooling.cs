namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the job spooling support.
/// See: PWG 5100.7-2023 Section 6.9.31
/// </summary>
public enum JobSpooling
{
    Automatic,
    Spool,
    Stream
}
