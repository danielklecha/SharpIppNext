using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies known attribute names for <code>document-creation-attributes-supported</code>.
/// See: PWG 5100.5-2024 Section 6.5.1
/// </summary>
public readonly record struct DocumentCreationAttribute(string Value, bool IsValue = true) : ISmartEnum 
{
    // Operation attributes used with Send-Document and Send-URI.
    /// <summary>The compression attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute Compression = new("compression");
    /// <summary>The document-format attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute DocumentFormat = new("document-format");
    /// <summary>The document-format-details attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    [Obsolete("The 'document-format-details' attribute is deprecated. See PWG 5100.7-2023 Section 6.2.1.")]
    public static readonly DocumentCreationAttribute DocumentFormatDetails = new("document-format-details");
    /// <summary>The document-natural-language attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute DocumentNaturalLanguage = new("document-natural-language");
    /// <summary>The document-charset attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute DocumentCharset = new("document-charset");
    /// <summary>The document-message attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute DocumentMessage = new("document-message");
    /// <summary>The document-name attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute DocumentName = new("document-name");
    /// <summary>The last-document attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute LastDocument = new("last-document");
    /// <summary>The document-uri attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute DocumentUri = new("document-uri");
    /// <summary>The document-access attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    [Obsolete("The 'document-access' attribute is deprecated in favor of URI authentication. See PWG 5100.18-2025 Section 7.1.5.")]
    public static readonly DocumentCreationAttribute DocumentAccess = new("document-access");

    // Document Template attributes accepted in Document Creation requests.
    /// <summary>The copies attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute Copies = new("copies");
    /// <summary>The cover-back attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute CoverBack = new("cover-back");
    /// <summary>The cover-front attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute CoverFront = new("cover-front");
    /// <summary>The finishings attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute Finishings = new("finishings");
    /// <summary>The finishings-col attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute FinishingsCol = new("finishings-col");
    /// <summary>The force-front-side attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute ForceFrontSide = new("force-front-side");
    /// <summary>The imposition-template attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute ImpositionTemplate = new("imposition-template");
    /// <summary>The media attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute Media = new("media");
    /// <summary>The media-col attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute MediaCol = new("media-col");
    /// <summary>The media-input-tray-check attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute MediaInputTrayCheck = new("media-input-tray-check");
    /// <summary>The number-up attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute NumberUp = new("number-up");
    /// <summary>The orientation-requested attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute OrientationRequested = new("orientation-requested");
    /// <summary>The output-bin attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute OutputBin = new("output-bin");
    /// <summary>The page-delivery attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute PageDelivery = new("page-delivery");
    /// <summary>The page-order-received attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute PageOrderReceived = new("page-order-received");
    /// <summary>The page-ranges attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute PageRanges = new("page-ranges");
    /// <summary>The presentation-direction-number-up attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute PresentationDirectionNumberUp = new("presentation-direction-number-up");
    /// <summary>The print-quality attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute PrintQuality = new("print-quality");
    /// <summary>The printer-resolution attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute PrinterResolution = new("printer-resolution");
    /// <summary>The sides attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute Sides = new("sides");
    /// <summary>The x-image-position attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute XImagePosition = new("x-image-position");
    /// <summary>The x-image-shift attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute XImageShift = new("x-image-shift");
    /// <summary>The x-side1-image-shift attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute XSide1ImageShift = new("x-side1-image-shift");
    /// <summary>The x-side2-image-shift attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute XSide2ImageShift = new("x-side2-image-shift");
    /// <summary>The y-image-position attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute YImagePosition = new("y-image-position");
    /// <summary>The y-image-shift attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute YImageShift = new("y-image-shift");
    /// <summary>The y-side1-image-shift attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute YSide1ImageShift = new("y-side1-image-shift");
    /// <summary>The y-side2-image-shift attribute. See: PWG 5100.5-2024 Section 6.5.1</summary>
    public static readonly DocumentCreationAttribute YSide2ImageShift = new("y-side2-image-shift");

    public override string ToString() => Value;
    public static implicit operator string(DocumentCreationAttribute value) => value.Value;
    public static implicit operator DocumentCreationAttribute(string value) => new(value);
}
