namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies when output is trimmed.
/// See: PWG 5100.1-2022 Section 6.31
/// </summary>
public readonly record struct TrimmingWhen(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>Trimming occurs after each sheet. See: PWG 5100.1-2022 Section 6.31</summary>
    public static readonly TrimmingWhen AfterSheets = new("after-sheets");
    /// <summary>Trimming occurs after each document. See: PWG 5100.1-2022 Section 6.31</summary>
    public static readonly TrimmingWhen AfterDocuments = new("after-documents");
    /// <summary>Trimming occurs after each set of copies. See: PWG 5100.1-2022 Section 6.31</summary>
    public static readonly TrimmingWhen AfterSets = new("after-sets");
    /// <summary>Trimming occurs after the entire job. See: PWG 5100.1-2022 Section 6.31</summary>
    public static readonly TrimmingWhen AfterJob = new("after-job");

    public override string ToString() => Value;
    public static implicit operator string(TrimmingWhen bin) => bin.Value;
    public static implicit operator TrimmingWhen(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
