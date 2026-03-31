namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies known attribute names for <code>document-creation-attributes-supported</code>.
/// See: PWG 5100.5-2024 Section 6.5.1
/// </summary>
public readonly record struct DocumentCreationAttribute(string Value, bool IsValue = true) : ISmartEnum 
{
    // Operation attributes used with Send-Document and Send-URI.
    public static readonly DocumentCreationAttribute Compression = new("compression");
    public static readonly DocumentCreationAttribute DocumentFormat = new("document-format");
    public static readonly DocumentCreationAttribute DocumentFormatDetails = new("document-format-details");
    public static readonly DocumentCreationAttribute DocumentNaturalLanguage = new("document-natural-language");
    public static readonly DocumentCreationAttribute DocumentCharset = new("document-charset");
    public static readonly DocumentCreationAttribute DocumentMessage = new("document-message");
    public static readonly DocumentCreationAttribute DocumentName = new("document-name");
    public static readonly DocumentCreationAttribute LastDocument = new("last-document");
    public static readonly DocumentCreationAttribute DocumentUri = new("document-uri");
    public static readonly DocumentCreationAttribute DocumentAccess = new("document-access");

    // Document Template attributes accepted in Document Creation requests.
    public static readonly DocumentCreationAttribute Copies = new("copies");
    public static readonly DocumentCreationAttribute CoverBack = new("cover-back");
    public static readonly DocumentCreationAttribute CoverFront = new("cover-front");
    public static readonly DocumentCreationAttribute Finishings = new("finishings");
    public static readonly DocumentCreationAttribute FinishingsCol = new("finishings-col");
    public static readonly DocumentCreationAttribute ForceFrontSide = new("force-front-side");
    public static readonly DocumentCreationAttribute ImpositionTemplate = new("imposition-template");
    public static readonly DocumentCreationAttribute Media = new("media");
    public static readonly DocumentCreationAttribute MediaCol = new("media-col");
    public static readonly DocumentCreationAttribute MediaInputTrayCheck = new("media-input-tray-check");
    public static readonly DocumentCreationAttribute NumberUp = new("number-up");
    public static readonly DocumentCreationAttribute OrientationRequested = new("orientation-requested");
    public static readonly DocumentCreationAttribute OutputBin = new("output-bin");
    public static readonly DocumentCreationAttribute PageDelivery = new("page-delivery");
    public static readonly DocumentCreationAttribute PageRanges = new("page-ranges");
    public static readonly DocumentCreationAttribute PresentationDirectionNumberUp = new("presentation-direction-number-up");
    public static readonly DocumentCreationAttribute PrintQuality = new("print-quality");
    public static readonly DocumentCreationAttribute PrinterResolution = new("printer-resolution");
    public static readonly DocumentCreationAttribute Sides = new("sides");
    public static readonly DocumentCreationAttribute XImagePosition = new("x-image-position");
    public static readonly DocumentCreationAttribute XImageShift = new("x-image-shift");
    public static readonly DocumentCreationAttribute XSide1ImageShift = new("x-side1-image-shift");
    public static readonly DocumentCreationAttribute XSide2ImageShift = new("x-side2-image-shift");
    public static readonly DocumentCreationAttribute YImagePosition = new("y-image-position");
    public static readonly DocumentCreationAttribute YImageShift = new("y-image-shift");
    public static readonly DocumentCreationAttribute YSide1ImageShift = new("y-side1-image-shift");
    public static readonly DocumentCreationAttribute YSide2ImageShift = new("y-side2-image-shift");

    public override string ToString() => Value;
    public static implicit operator string(DocumentCreationAttribute value) => value.Value;
    public static explicit operator DocumentCreationAttribute(string value) => new(value);
}