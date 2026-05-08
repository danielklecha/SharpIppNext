namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies member attribute names supported by <code>job-sheets-col-supported</code>.
/// See: PWG 5100.7-2023 Section 6.9.30
/// </summary>
public readonly record struct JobSheetsColMember(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly JobSheetsColMember JobSheets = new("job-sheets");
    public static readonly JobSheetsColMember Media = new("media");
    public static readonly JobSheetsColMember MediaCol = new("media-col");

    public override string ToString() => Value;
    public static implicit operator string(JobSheetsColMember value) => value.Value;
    public static explicit operator JobSheetsColMember(string value) => new(value);
}
