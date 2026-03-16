namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies when output is trimmed.
/// See: PWG 5100.1-2022 Section 6.31
/// </summary>
public readonly record struct TrimmingWhen(string Value)
{
    public static readonly TrimmingWhen AfterSheets = new("after-sheets");
    public static readonly TrimmingWhen AfterDocuments = new("after-documents");
    public static readonly TrimmingWhen AfterSets = new("after-sets");
    public static readonly TrimmingWhen AfterJob = new("after-job");

    public override string ToString() => Value;
    public static implicit operator string(TrimmingWhen bin) => bin.Value;
    public static explicit operator TrimmingWhen(string value) => new(value);
}
