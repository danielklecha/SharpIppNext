namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of trimming to perform.
/// See: PWG 5100.1-2022 Section 6.30
/// </summary>
public enum TrimmingType
{
    DrawLine,
    Full,
    Partial,
    Perforate,
    Score,
    Tab
}
