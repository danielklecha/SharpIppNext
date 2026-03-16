namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the job-sheets-type.
/// See: PWG 5100.1-2022 Section 6.10
/// </summary>
public readonly record struct JobSheetsType(string Value)
{
    public static readonly JobSheetsType Standard = new("standard");
    public static readonly JobSheetsType StandardX = new("standard-x");

    public override string ToString() => Value;
    public static implicit operator string(JobSheetsType bin) => bin.Value;
    public static explicit operator JobSheetsType(string value) => new(value);
}
