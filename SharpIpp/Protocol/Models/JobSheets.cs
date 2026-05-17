namespace SharpIpp.Protocol.Models;

/// <summary>
/// This attribute determines which job start/end sheet(s), if any, MUST
/// be printed with a job.
/// See: RFC 2911 Section 4.2.3
/// See: RFC 8011 Section 5.2.3
/// </summary>
public readonly record struct JobSheets(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// No job sheet is printed.
    /// See: RFC 8011 Section 5.2.3
    /// </summary>
    public static readonly JobSheets None = new("none");

    /// <summary>
    /// One or more site specific standard job sheets are printed.
    /// See: RFC 8011 Section 5.2.3
    /// </summary>
    public static readonly JobSheets Standard = new("standard");

    /// <summary>
    /// A Job Sheet is printed to indicate the start of the Job.
    /// See: PWG 5100.3-2023 Section 4.1
    /// </summary>
    public static readonly JobSheets JobStartSheet = new("job-start-sheet");

    /// <summary>
    /// A Job Sheet is printed to indicate the end of the Job.
    /// See: PWG 5100.3-2023 Section 4.1
    /// </summary>
    public static readonly JobSheets JobEndSheet = new("job-end-sheet");

    /// <summary>
    /// Job Sheets are printed to indicate the start and end of all output associated with the Job.
    /// See: PWG 5100.3-2023 Section 4.1
    /// </summary>
    public static readonly JobSheets JobBothSheets = new("job-both-sheets");

    /// <summary>
    /// The first Input Page in the Document Data is printed as the Job Sheet and the Printer's standard Job Sheet is suppressed.
    /// See: PWG 5100.3-2023 Section 4.1
    /// </summary>
    public static readonly JobSheets FirstPrintStreamPage = new("first-print-stream-page");

    public override string ToString() => Value;
    public static implicit operator string(JobSheets bin) => bin.Value;
    public static explicit operator JobSheets(string value) => new(value);
}
