namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the type of separator sheets.
/// See: PWG 5100.3 Section 5.2.18
/// </summary>
public readonly record struct SeparatorSheetsType(string Value)
{
    public static readonly SeparatorSheetsType None = new("none");
    public static readonly SeparatorSheetsType SlipSheets = new("slip-sheets");
    public static readonly SeparatorSheetsType StartSheet = new("start-sheet");
    public static readonly SeparatorSheetsType EndSheet = new("end-sheet");
    public static readonly SeparatorSheetsType BothSheets = new("both-sheets");

    public override string ToString() => Value;
    public static implicit operator string(SeparatorSheetsType bin) => bin.Value;
    public static explicit operator SeparatorSheetsType(string value) => new(value);
}
