using System;
using System.ComponentModel.DataAnnotations;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Document Template attributes that override Job Template attributes for a specific Document.
/// See: RFC 8011
/// See: PWG 5100.1-2022
/// See: PWG 5100.2-2001
/// See: PWG 5100.3-2023
/// See: PWG 5100.5-2024
/// See: PWG 5100.7-2023
/// See: PWG 5100.8-2003
/// See: PWG 5100.11-2024
/// See: PWG 5100.15-2013
/// </summary>
public class DocumentTemplateAttributes : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// The <c>copies</c> Document Template attribute.
    /// See: RFC 8011 Section 5.2.5
    /// </summary>
    [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue)]
    public int? Copies { get; set; }

    /// <summary>
    /// [DEPRECATED] The <c>cover-back</c> Document Template attribute. Deprecated per PWG 5100.5-2024 Section 14.1 to match changes in PWG 5100.3-2023.
    /// See: PWG 5100.5-2024 Section 14.1 and PWG 5100.3-2023 Section 5.2.1
    /// </summary>
    [Obsolete("The 'cover-back' attribute is deprecated. See PWG 5100.3-2023 Section 5.2.1.")]
    public Cover? CoverBack { get; set; }

    /// <summary>
    /// [DEPRECATED] The <c>cover-front</c> Document Template attribute. Deprecated per PWG 5100.5-2024 Section 14.1 to match changes in PWG 5100.3-2023.
    /// See: PWG 5100.5-2024 Section 14.1 and PWG 5100.3-2023 Section 5.2.1
    /// </summary>
    [Obsolete("The 'cover-front' attribute is deprecated. See PWG 5100.3-2023 Section 5.2.1.")]
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
    /// [DEPRECATED] The <c>media-input-tray-check</c> Document Template attribute. Deprecated per PWG 5100.5-2024 Section 14.1 to match changes in PWG 5100.3-2023.
    /// See: PWG 5100.5-2024 Section 14.1 and PWG 5100.3-2023 Section 5.2.13
    /// </summary>
    [Obsolete("The 'media-input-tray-check' attribute is deprecated. See PWG 5100.3-2023 Section 5.2.13.")]
    public MediaInputTrayCheck? MediaInputTrayCheck { get; set; }

    /// <summary>
    /// The <c>number-up</c> Document Template attribute.
    /// See: RFC 8011 Section 5.2.9
    /// </summary>
    [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue)]
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
    /// See: PWG 5100.5-2024 Section 6.3
    /// </summary>
    [Obsolete("The 'page-order-received' attribute is obsolete. See PWG 5100.3-2023 Section 5.2.14.")]
    public PageOrderReceived? PageOrderReceived { get; set; }

    /// <summary>
    /// The <c>page-ranges</c> Document Template attribute.
    /// See: RFC 8011 Section 5.2.7
    /// </summary>
    public Range[]? PageRanges { get; set; }

    /// <summary>
    /// [DEPRECATED] The <c>presentation-direction-number-up</c> Document Template attribute. Deprecated per PWG 5100.5-2024 Section 14.1 to match changes in PWG 5100.3-2023.
    /// See: PWG 5100.5-2024 Section 14.1 and PWG 5100.3-2023 Section 5.2.15
    /// </summary>
    [Obsolete("The 'presentation-direction-number-up' attribute is deprecated. See PWG 5100.3-2023 Section 5.2.15.")]
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
    public InputColorMode? InputColorMode { get; set; }

    /// <summary>
    /// The <c>input-content-type</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.6
    /// </summary>
    public InputContentType? InputContentType { get; set; }

    /// <summary>
    /// The <c>input-contrast</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.7
    /// </summary>
    [System.ComponentModel.DataAnnotations.Range(-100, 100)]
    public int? InputContrast { get; set; }

    /// <summary>
    /// The <c>input-film-scan-mode</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.8
    /// </summary>
    public InputFilmScanMode? InputFilmScanMode { get; set; }

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
    [System.ComponentModel.DataAnnotations.Range(1, 1000)]
    public int? InputScalingHeight { get; set; }

    /// <summary>
    /// The <c>input-scaling-width</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.15
    /// </summary>
    [System.ComponentModel.DataAnnotations.Range(1, 1000)]
    public int? InputScalingWidth { get; set; }

    /// <summary>
    /// The <c>input-sharpness</c> input attribute.
    /// See: PWG 5100.15-2013 Section 7.1.1.17
    /// </summary>
    [System.ComponentModel.DataAnnotations.Range(-100, 100)]
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
    public InputSource? InputSource { get; set; }

    /// <summary>
    /// The <c>document-charset</c> Document Template attribute. Specifies the charset of the document data.
    /// See: PWG 5100.5-2024 Section 6.5.1
    /// </summary>
    public Charset? DocumentCharset { get; set; }



    /// <summary>
    /// The <c>document-format</c> Document Template attribute. Specifies the MIME media type of the document data.
    /// See: PWG 5100.5-2024 Section 6.5.3
    /// </summary>
    public DocumentFormat? DocumentFormat { get; set; }

    /// <summary>
    /// The <c>document-format-details</c> Document Template attribute. Provides detailed information about the document format.
    /// See: PWG 5100.5-2024 Section 6.5.4
    /// </summary>
    [Obsolete("The 'document-format-details' attribute is deprecated. See PWG 5100.7-2023 Section 6.2.1.")]
    public DocumentFormatDetails? DocumentFormatDetails { get; set; }

    /// <summary>
    /// The <c>document-message</c> Document Template attribute. A message from the client to the Document object.
    /// See: PWG 5100.5-2024 Section 6.5.5
    /// </summary>
    public string? DocumentMessage { get; set; }

    /// <summary>
    /// The <c>document-metadata</c> Document Template attribute. Arbitrary metadata associated with the document.
    /// See: PWG 5100.5-2024 Section 6.5.6
    /// </summary>
    [Metadata]
    public DocumentMetadata? DocumentMetadata { get; set; }

    /// <summary>
    /// The <c>document-name</c> Document Template attribute. The name of the document.
    /// See: PWG 5100.5-2024 Section 6.5.7
    /// </summary>
    public string? DocumentName { get; set; }

    /// <summary>
    /// The <c>document-natural-language</c> Document Template attribute. The natural language of the document content.
    /// See: PWG 5100.5-2024 Section 6.5.8
    /// </summary>
    public NaturalLanguage? DocumentNaturalLanguage { get; set; }



    /// <summary>
    /// The <c>document-password</c> Document Template attribute. A password required to access the document (maximum 1023 octets).
    /// See: PWG 5100.5-2024 Section 6.5.10
    /// </summary>
    [ByteRange(1, 1023)]
    public OctetString? DocumentPassword { get; set; }

    /// <summary>
    /// The <c>document-uri</c> Document Template attribute. A URI that references the document data.
    /// See: PWG 5100.5-2024 Section 6.5.11
    /// </summary>
    public Uri? DocumentUri { get; set; }

    /// <summary>
    /// The <c>last-document</c> Document Template attribute. Indicates whether this is the last document in a multi-document job.
    /// See: PWG 5100.5-2024 Section 6.5.12
    /// </summary>
    public bool? LastDocument { get; set; }

    /// <summary>
    /// The <c>job-password</c> Document Template attribute. A password for the job associated with this document.
    /// See: PWG 5100.5-2024 Section 6.5.13
    /// </summary>
    public OctetString? JobPassword { get; set; }

    /// <summary>
    /// The <c>job-password-encryption</c> Document Template attribute. Specifies the encryption algorithm used for the job password.
    /// See: PWG 5100.5-2024 Section 6.5.14
    /// </summary>
    public JobPasswordEncryption? JobPasswordEncryption { get; set; }
}
