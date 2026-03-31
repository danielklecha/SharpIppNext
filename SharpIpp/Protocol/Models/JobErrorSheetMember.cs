namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies member attribute names supported by <code>job-error-sheet-supported</code>.
/// See: PWG 5100.3-2023 Section 5.3.21
/// </summary>
public readonly record struct JobErrorSheetMember(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly JobErrorSheetMember JobErrorSheetType = new("job-error-sheet-type");
    public static readonly JobErrorSheetMember JobErrorSheetWhen = new("job-error-sheet-when");
    public static readonly JobErrorSheetMember Media = new("media");
    public static readonly JobErrorSheetMember MediaCol = new("media-col");

    public override string ToString() => Value;
    public static implicit operator string(JobErrorSheetMember value) => value.Value;
    public static explicit operator JobErrorSheetMember(string value) => new(value);
}
