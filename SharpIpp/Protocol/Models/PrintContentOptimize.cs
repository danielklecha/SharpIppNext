namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the print-content-optimize attribute, which controls how the Printer optimizes output for the type of content.
/// See: PWG 5100.1-2022 Section 6.20
/// </summary>
public readonly record struct PrintContentOptimize(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// The Printer automatically selects the optimization based on the document content.
    /// See: PWG 5100.1-2022 Section 6.20
    /// </summary>
    public static readonly PrintContentOptimize Auto = new("auto");

    /// <summary>
    /// The Printer optimizes output for graphic (vector) content.
    /// See: PWG 5100.1-2022 Section 6.20
    /// </summary>
    public static readonly PrintContentOptimize Graphic = new("graphic");

    /// <summary>
    /// The Printer optimizes output for photographic content.
    /// See: PWG 5100.1-2022 Section 6.20
    /// </summary>
    public static readonly PrintContentOptimize Photo = new("photo");

    /// <summary>
    /// The Printer optimizes output for text content.
    /// See: PWG 5100.1-2022 Section 6.20
    /// </summary>
    public static readonly PrintContentOptimize Text = new("text");

    /// <summary>
    /// The Printer optimizes output for mixed text and graphic content.
    /// See: PWG 5100.1-2022 Section 6.20
    /// </summary>
    public static readonly PrintContentOptimize TextAndGraphic = new("text-and-graphic");

    public override string ToString() => Value;
    public static implicit operator string(PrintContentOptimize bin) => bin.Value;
    public static implicit operator PrintContentOptimize(string value) => new(value);
}
