namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies member attribute names supported by <code>job-error-sheet-supported</code>.
/// See: PWG 5100.3-2023 Section 5.3.21
/// </summary>
public readonly record struct JobErrorSheetMember(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>The job-error-sheet-type member attribute. See: PWG 5100.3-2023 Section 5.3.21</summary>
    public static readonly JobErrorSheetMember JobErrorSheetType = new("job-error-sheet-type");
    /// <summary>The job-error-sheet-when member attribute. See: PWG 5100.3-2023 Section 5.3.21</summary>
    public static readonly JobErrorSheetMember JobErrorSheetWhen = new("job-error-sheet-when");
    /// <summary>The media member attribute. See: PWG 5100.3-2023 Section 5.3.21</summary>
    public static readonly JobErrorSheetMember Media = new("media");
    /// <summary>The media-col member attribute. See: PWG 5100.3-2023 Section 5.3.21</summary>
    public static readonly JobErrorSheetMember MediaCol = new("media-col");

    public override string ToString() => Value;
    public static implicit operator string(JobErrorSheetMember value) => value.Value;
    public static explicit operator JobErrorSheetMember(string value) => new(value);
}
