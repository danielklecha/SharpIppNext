namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the job-sheets-type.
/// See: PWG 5100.1-2022 Section 6.10
/// </summary>
public readonly record struct JobSheetsType(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// See: PWG 5100.3-2023 Sections 5.2.6.1 and 5.2.9.1
    /// </summary>
    public static readonly JobSheetsType None = new("none");

    public static readonly JobSheetsType Standard = new("standard");
    public static readonly JobSheetsType StandardX = new("standard-x");
    public static readonly JobSheetsType JobStartSheet = new("job-start-sheet");
    public static readonly JobSheetsType JobEndSheet = new("job-end-sheet");
    public static readonly JobSheetsType JobBothSheets = new("job-both-sheets");
    public static readonly JobSheetsType FirstPrintStreamPage = new("first-print-stream-page");

    public override string ToString() => Value;
    public static implicit operator string(JobSheetsType bin) => bin.Value;
    public static explicit operator JobSheetsType(string value) => new(value);
}
