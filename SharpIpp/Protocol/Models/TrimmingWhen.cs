namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies when output is trimmed.
/// See: PWG 5100.1-2022 Section 6.31
/// </summary>
public enum TrimmingWhen
{
    AfterSheets,
    AfterDocuments,
    AfterSets,
    AfterJob
}
