namespace SharpIpp.Protocol.Models;

/// <summary>
/// PWG 5100.5-2024 Section 6.3
/// Document Template attributes that override Job Template attributes for a specific Document.
/// </summary>
public class DocumentTemplateAttributes : IIppCollection
{
    /// <inheritdoc />
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    /// <summary>
    /// The <c>copies</c> Document Template attribute.
    /// See: RFC 8011 Section 5.2.5
    /// </summary>
    public int? Copies { get; set; }

    /// <summary>
    /// The <c>cover-back</c> Document Template attribute.
    /// See: PWG 5100.5-2024 Section 6.3 and PWG 5100.3-2023 Section 5.2.1
    /// </summary>
    public Cover? CoverBack { get; set; }

    /// <summary>
    /// The <c>cover-front</c> Document Template attribute.
    /// See: PWG 5100.5-2024 Section 6.3 and PWG 5100.3-2023 Section 5.2.1
    /// </summary>
    public Cover? CoverFront { get; set; }

    /// <summary>
    /// The <c>finishings</c> Document Template attribute.
    /// See: RFC 8011 Section 5.2.6
    /// </summary>
    public Finishings[]? Finishings { get; set; }

    /// <summary>
    /// The <c>finishings-col</c> Document Template attribute.
    /// See: PWG 5100.1-2022 Section 5.2
    /// </summary>
    public FinishingsCol[]? FinishingsCol { get; set; }

    /// <summary>
    /// The <c>force-front-side</c> Document Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.2
    /// </summary>
    public int[]? ForceFrontSide { get; set; }

    /// <summary>
    /// The <c>imposition-template</c> Document Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.4
    /// </summary>
    public ImpositionTemplate? ImpositionTemplate { get; set; }

    /// <summary>
    /// The <c>media</c> Document Template attribute.
    /// See: RFC 8011 Section 5.2.11
    /// </summary>
    public Media? Media { get; set; }

    /// <summary>
    /// The <c>media-col</c> Document Template attribute.
    /// See: PWG 5100.7-2023
    /// </summary>
    public MediaCol? MediaCol { get; set; }

    /// <summary>
    /// The <c>media-input-tray-check</c> Document Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.13
    /// </summary>
    public MediaInputTrayCheck? MediaInputTrayCheck { get; set; }

    /// <summary>
    /// The <c>number-up</c> Document Template attribute.
    /// See: RFC 8011 Section 5.2.9
    /// </summary>
    public int? NumberUp { get; set; }

    /// <summary>
    /// The <c>orientation-requested</c> Document Template attribute.
    /// See: RFC 8011 Section 5.2.10
    /// </summary>
    public Orientation? OrientationRequested { get; set; }

    /// <summary>
    /// The <c>output-bin</c> Document Template attribute.
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public OutputBin? OutputBin { get; set; }

    /// <summary>
    /// The <c>page-delivery</c> Document Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.14
    /// </summary>
    public PageDelivery? PageDelivery { get; set; }

    /// <summary>
    /// The <c>page-order-received</c> Document Template attribute.
    /// OBSOLETE in PWG 5100.3-2023 but still part of PWG 5100.5-2024 Section 6.3.
    /// </summary>
    public PageOrderReceived? PageOrderReceived { get; set; }

    /// <summary>
    /// The <c>page-ranges</c> Document Template attribute.
    /// See: RFC 8011 Section 5.2.7
    /// </summary>
    public Range[]? PageRanges { get; set; }

    /// <summary>
    /// The <c>presentation-direction-number-up</c> Document Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.15
    /// </summary>
    public PresentationDirectionNumberUp? PresentationDirectionNumberUp { get; set; }

    /// <summary>
    /// The <c>print-quality</c> Document Template attribute.
    /// See: RFC 8011 Section 5.2.13
    /// </summary>
    public PrintQuality? PrintQuality { get; set; }

    /// <summary>
    /// The <c>printer-resolution</c> Document Template attribute.
    /// See: RFC 8011 Section 5.2.12
    /// </summary>
    public Resolution? PrinterResolution { get; set; }

    /// <summary>
    /// The <c>sides</c> Document Template attribute.
    /// See: RFC 8011 Section 5.2.8
    /// </summary>
    public Sides? Sides { get; set; }

    /// <summary>
    /// The <c>x-image-position</c> Document Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.17
    /// </summary>
    public XImagePosition? XImagePosition { get; set; }

    /// <summary>
    /// The <c>x-image-shift</c> Document Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.18
    /// </summary>
    public int? XImageShift { get; set; }

    /// <summary>
    /// The <c>x-side1-image-shift</c> Document Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.19
    /// </summary>
    public int? XSide1ImageShift { get; set; }

    /// <summary>
    /// The <c>x-side2-image-shift</c> Document Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.20
    /// </summary>
    public int? XSide2ImageShift { get; set; }

    /// <summary>
    /// The <c>y-image-position</c> Document Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.21
    /// </summary>
    public YImagePosition? YImagePosition { get; set; }

    /// <summary>
    /// The <c>y-image-shift</c> Document Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.22
    /// </summary>
    public int? YImageShift { get; set; }

    /// <summary>
    /// The <c>y-side1-image-shift</c> Document Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.23
    /// </summary>
    public int? YSide1ImageShift { get; set; }

    /// <summary>
    /// The <c>y-side2-image-shift</c> Document Template attribute.
    /// See: PWG 5100.3-2023 Section 5.2.24
    /// </summary>
    public int? YSide2ImageShift { get; set; }

    /// <summary>
    /// The <c>input-auto-exposure</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.1
    /// </summary>
    public bool? InputAutoExposure { get; set; }

    /// <summary>
    /// The <c>input-auto-scaling</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.2
    /// </summary>
    public bool? InputAutoScaling { get; set; }

    /// <summary>
    /// The <c>input-auto-skew-correction</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.3
    /// </summary>
    public bool? InputAutoSkewCorrection { get; set; }

    /// <summary>
    /// The <c>input-brightness</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.4
    /// </summary>
    public int? InputBrightness { get; set; }

    /// <summary>
    /// The <c>input-color-mode</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.5
    /// </summary>
    public string? InputColorMode { get; set; }

    /// <summary>
    /// The <c>input-content-type</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.6
    /// </summary>
    public string? InputContentType { get; set; }

    /// <summary>
    /// The <c>input-contrast</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.7
    /// </summary>
    public int? InputContrast { get; set; }

    /// <summary>
    /// The <c>input-film-scan-mode</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.8
    /// </summary>
    public string? InputFilmScanMode { get; set; }

    /// <summary>
    /// The <c>input-images-to-transfer</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.9
    /// </summary>
    public int? InputImagesToTransfer { get; set; }

    /// <summary>
    /// The <c>input-media</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.10
    /// </summary>
    public Media? InputMedia { get; set; }

    /// <summary>
    /// The <c>input-orientation-requested</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.11
    /// </summary>
    public Orientation? InputOrientationRequested { get; set; }

    /// <summary>
    /// The <c>input-quality</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.12
    /// </summary>
    public PrintQuality? InputQuality { get; set; }

    /// <summary>
    /// The <c>input-resolution</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.13
    /// </summary>
    public Resolution? InputResolution { get; set; }

    /// <summary>
    /// The <c>input-scaling-height</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.14
    /// </summary>
    public int? InputScalingHeight { get; set; }

    /// <summary>
    /// The <c>input-scaling-width</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.15
    /// </summary>
    public int? InputScalingWidth { get; set; }

    /// <summary>
    /// The <c>input-sharpness</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.17
    /// </summary>
    public int? InputSharpness { get; set; }

    /// <summary>
    /// The <c>input-sides</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.18
    /// </summary>
    public Sides? InputSides { get; set; }

    /// <summary>
    /// The <c>input-source</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.19
    /// </summary>
    public string? InputSource { get; set; }
}
