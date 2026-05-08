namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies member attribute names supported by <code>insert-sheet-supported</code>.
/// See: PWG 5100.3-2023 Section 5.3.13
/// </summary>
public readonly record struct InsertSheetMember(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly InsertSheetMember InsertAfterPageNumber = new("insert-after-page-number");
    public static readonly InsertSheetMember InsertCount = new("insert-count");
    public static readonly InsertSheetMember Media = new("media");
    public static readonly InsertSheetMember MediaCol = new("media-col");

    public override string ToString() => Value;
    public static implicit operator string(InsertSheetMember value) => value.Value;
    public static explicit operator InsertSheetMember(string value) => new(value);
}
