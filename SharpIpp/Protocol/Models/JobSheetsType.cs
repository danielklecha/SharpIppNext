namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the job-sheets-type.
/// See: PWG 5100.1-2022 Section 6.10
/// </summary>
public readonly record struct JobSheetsType(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// No job sheet is printed.
    /// See: PWG 5100.1-2022 Section 6.10
    /// </summary>
    public static readonly JobSheetsType None = new("none");

    /// <summary>
    /// One or more site-specific standard job sheets are printed.
    /// See: PWG 5100.1-2022 Section 6.10
    /// </summary>
    public static readonly JobSheetsType Standard = new("standard");

    /// <summary>
    /// An extended standard job sheet is printed.
    /// See: PWG 5100.1-2022 Section 6.10
    /// </summary>
    public static readonly JobSheetsType StandardX = new("standard-x");

    /// <summary>
    /// A job sheet is printed to indicate the start of the job.
    /// See: PWG 5100.1-2022 Section 6.10
    /// </summary>
    public static readonly JobSheetsType JobStartSheet = new("job-start-sheet");

    /// <summary>
    /// A job sheet is printed to indicate the end of the job.
    /// See: PWG 5100.1-2022 Section 6.10
    /// </summary>
    public static readonly JobSheetsType JobEndSheet = new("job-end-sheet");

    /// <summary>
    /// Job sheets are printed to indicate both the start and end of the job.
    /// See: PWG 5100.1-2022 Section 6.10
    /// </summary>
    public static readonly JobSheetsType JobBothSheets = new("job-both-sheets");

    /// <summary>
    /// The first input page in the document data is printed as the job sheet.
    /// See: PWG 5100.1-2022 Section 6.10
    /// </summary>
    public static readonly JobSheetsType FirstPrintStreamPage = new("first-print-stream-page");

    public override string ToString() => Value;
    public static implicit operator string(JobSheetsType bin) => bin.Value;
    public static explicit operator JobSheetsType(string value) => new(value);
}
