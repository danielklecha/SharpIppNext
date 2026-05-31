namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the <c>pwg-raster-document-sheet-back</c> attribute.
/// See: PWG 5102.4-2012 Section 10.2
/// </summary>
public readonly record struct PwgRasterDocumentSheetBack(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>
    /// Backside coordinate system is the same as the frontside coordinate system.
    /// See: PWG 5102.4-2012 Section 10.2
    /// </summary>
    public static readonly PwgRasterDocumentSheetBack Normal = new("normal");

    /// <summary>
    /// Backside coordinate system is rotated 180 degrees compared to the frontside.
    /// See: PWG 5102.4-2012 Section 10.2
    /// </summary>
    public static readonly PwgRasterDocumentSheetBack Rotated = new("rotated");

    /// <summary>
    /// Backside coordinate system requires manual tumbling.
    /// See: PWG 5102.4-2012 Section 10.2
    /// </summary>
    public static readonly PwgRasterDocumentSheetBack ManualTumble = new("manual-tumble");

    /// <summary>
    /// Backside coordinate system is flipped compared to the frontside.
    /// See: PWG 5102.4-2012 Section 10.2
    /// </summary>
    public static readonly PwgRasterDocumentSheetBack Flipped = new("flipped");

    public override string ToString() => Value;
    public static implicit operator string(PwgRasterDocumentSheetBack value) => value.Value;
    public static implicit operator PwgRasterDocumentSheetBack(string value) => new(value);
}
