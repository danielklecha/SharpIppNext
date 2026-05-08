namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies member attribute names supported by <code>job-accounting-sheets-supported</code>.
/// See: PWG 5100.3-2023 Section 5.3.16
/// </summary>
public readonly record struct JobAccountingSheetsMember(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly JobAccountingSheetsMember JobAccountingOutputBin = new("job-accounting-output-bin");
    public static readonly JobAccountingSheetsMember JobAccountingSheetsType = new("job-accounting-sheets-type");
    public static readonly JobAccountingSheetsMember Media = new("media");
    public static readonly JobAccountingSheetsMember MediaCol = new("media-col");

    public override string ToString() => Value;
    public static implicit operator string(JobAccountingSheetsMember value) => value.Value;
    public static explicit operator JobAccountingSheetsMember(string value) => new(value);
}
