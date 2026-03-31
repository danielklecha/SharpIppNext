namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies member attribute names supported by <code>separator-sheets-supported</code>.
/// See: PWG 5100.3-2023 Section 5.3.32
/// </summary>
public readonly record struct SeparatorSheetsMember(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly SeparatorSheetsMember Media = new("media");
    public static readonly SeparatorSheetsMember MediaCol = new("media-col");
    public static readonly SeparatorSheetsMember SeparatorSheetsType = new("separator-sheets-type");

    public override string ToString() => Value;
    public static implicit operator string(SeparatorSheetsMember value) => value.Value;
    public static explicit operator SeparatorSheetsMember(string value) => new(value);
}