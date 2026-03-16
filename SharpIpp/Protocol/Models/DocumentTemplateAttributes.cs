namespace SharpIpp.Protocol.Models;

/// <summary>
/// PWG 5100.5-2024 Section 6.3
/// Document Template attributes that override Job Template attributes for a specific Document.
/// </summary>
public class DocumentTemplateAttributes : IIppCollection
{
    /// <inheritdoc />
    public bool IsNoValue { get; set; }

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
}
