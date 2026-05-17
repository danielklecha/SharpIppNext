namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of separator sheets.
/// See: PWG 5100.3-2023 Section 5.2.16.1
/// </summary>
public readonly record struct SeparatorSheetsType(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// No separator sheets are added.
    /// See: PWG 5100.3-2023 Section 5.2.16.1
    /// </summary>
    public static readonly SeparatorSheetsType None = new("none");

    /// <summary>
    /// Slip sheets are inserted between sets of copies.
    /// See: PWG 5100.3-2023 Section 5.2.16.1
    /// </summary>
    public static readonly SeparatorSheetsType SlipSheets = new("slip-sheets");

    /// <summary>
    /// A separator sheet is inserted at the start of the job.
    /// See: PWG 5100.3-2023 Section 5.2.16.1
    /// </summary>
    public static readonly SeparatorSheetsType StartSheet = new("start-sheet");

    /// <summary>
    /// A separator sheet is inserted at the end of the job.
    /// See: PWG 5100.3-2023 Section 5.2.16.1
    /// </summary>
    public static readonly SeparatorSheetsType EndSheet = new("end-sheet");

    /// <summary>
    /// Separator sheets are inserted at both the start and end of the job.
    /// See: PWG 5100.3-2023 Section 5.2.16.1
    /// </summary>
    public static readonly SeparatorSheetsType BothSheets = new("both-sheets");

    public override string ToString() => Value;
    public static implicit operator string(SeparatorSheetsType bin) => bin.Value;
    public static explicit operator SeparatorSheetsType(string value) => new(value);
}
