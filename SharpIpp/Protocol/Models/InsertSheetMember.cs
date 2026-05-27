namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies member attribute names supported by <code>insert-sheet-supported</code>.
/// See: PWG 5100.3-2023 Section 5.3.13
/// </summary>
public readonly record struct InsertSheetMember(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>The insert-after-page-number member attribute. See: PWG 5100.3-2023 Section 5.3.13</summary>
    public static readonly InsertSheetMember InsertAfterPageNumber = new("insert-after-page-number");
    /// <summary>The insert-count member attribute. See: PWG 5100.3-2023 Section 5.3.13</summary>
    public static readonly InsertSheetMember InsertCount = new("insert-count");
    /// <summary>The media member attribute. See: PWG 5100.3-2023 Section 5.3.13</summary>
    public static readonly InsertSheetMember Media = new("media");
    /// <summary>The media-col member attribute. See: PWG 5100.3-2023 Section 5.3.13</summary>
    public static readonly InsertSheetMember MediaCol = new("media-col");

    public override string ToString() => Value;
    public static implicit operator string(InsertSheetMember value) => value.Value;
    public static implicit operator InsertSheetMember(string value) => new(value);
}
