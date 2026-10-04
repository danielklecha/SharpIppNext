using SharpIpp.Mapping;
using System;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;
/// <summary>
/// Document Status and Description attributes for a Document object.
/// Defined in PWG 5100.5-2024 Sections 6.1-6.2 and extended by PWG 5100.18-2025.
/// </summary>
[IppAttribute("document-attributes")]
public class DocumentAttributes : IIppCollection
{
    /// <inheritdoc />
    /// <summary>
    /// The document-number IPP attribute.
    /// Type: integer(1:MAX)
    /// See: PWG 5100.5-2024 Section 6.2.4
    /// </summary>
    /// <code>document-number</code>
    [IppAttribute(IppAttributeNames.DocumentNumber, Tag.Integer)]
    public IppValue<int>? DocumentNumber { get; set; }
    /// <summary>
    /// The document-state IPP attribute.
    /// See: PWG 5100.5-2024
    /// </summary>
    /// <code>document-state</code>
    [IppAttribute(IppAttributeNames.DocumentState, Tag.Enum)]
    public IppValue<DocumentState>? DocumentState { get; set; }
    /// <summary>
    /// The document-state-reasons IPP attribute.
    /// See: pwg5100.13 - IPP Driver Replacement Extensions v2.0 Section 9.1
    /// </summary>
    /// <code>document-state-reasons</code>
    [IppAttribute(IppAttributeNames.DocumentStateReasons, Tag.Keyword)]
    public IppValue<DocumentStateReason[]>? DocumentStateReasons { get; set; }
    /// <summary>
    /// The document-state-message IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2
    /// </summary>
    /// <code>document-state-message</code>
    [IppAttribute(IppAttributeNames.DocumentStateMessage, Tag.TextWithoutLanguage)]
    public StringWithLanguage? DocumentStateMessage { get; set; }
    /// <summary>
    /// The print-content-optimize IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.3.3
    /// </summary>
    /// <code>print-content-optimize</code>
    [IppAttribute(IppAttributeNames.PrintContentOptimize, Tag.Keyword)]
    public IppValue<PrintContentOptimize>? PrintContentOptimize { get; set; }
    /// <summary>
    /// The attributes-charset IPP attribute.
    /// See: RFC 8011 Section 5.3.19
    /// </summary>
    /// <code>attributes-charset</code>
    [IppAttribute(IppAttributeNames.AttributesCharset, Tag.Charset)]
    public IppValue<Charset>? AttributesCharset { get; set; }
    /// <summary>
    /// The attributes-natural-language IPP attribute.
    /// See: RFC 8011 Section 5.3.20
    /// </summary>
    /// <code>attributes-natural-language</code>
    [IppAttribute(IppAttributeNames.AttributesNaturalLanguage, Tag.NaturalLanguage)]
    public IppValue<NaturalLanguage>? AttributesNaturalLanguage { get; set; }

    /// <summary>
    /// The current-page-order IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2 and PWG 5100.3-2001.
    /// </summary>
    /// <code>current-page-order</code>
    [IppAttribute(IppAttributeNames.CurrentPageOrder, Tag.Keyword)]
    public IppValue<CurrentPageOrder>? CurrentPageOrder { get; set; }

    /// <summary>
    /// The date-time-at-completed IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>date-time-at-completed</code>
    [IppAttribute(IppAttributeNames.DateTimeAtCompleted, Tag.DateTime)]
    public IppValue<DateTimeOffset>? DateTimeAtCompleted { get; set; }
    /// <summary>
    /// The date-time-at-creation IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>date-time-at-creation</code>
    [IppAttribute(IppAttributeNames.DateTimeAtCreation, Tag.DateTime)]
    public IppValue<DateTimeOffset>? DateTimeAtCreation { get; set; }
    /// <summary>
    /// The date-time-at-processing IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>date-time-at-processing</code>
    [IppAttribute(IppAttributeNames.DateTimeAtProcessing, Tag.DateTime)]
    public IppValue<DateTimeOffset>? DateTimeAtProcessing { get; set; }
    /// <summary>
    /// The detailed-status-messages IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2 Table 4
    /// </summary>
    /// <code>detailed-status-messages</code>
    [IppAttribute(IppAttributeNames.DetailedStatusMessages, Tag.TextWithoutLanguage)]
    public IppValue<string[]>? DetailedStatusMessages { get; set; }
    /// <summary>
    /// The document-access-errors IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2 Table 4
    /// </summary>
    /// <code>document-access-errors</code>
    [IppAttribute(IppAttributeNames.DocumentAccessErrors, Tag.TextWithoutLanguage)]
    public IppValue<string[]>? DocumentAccessErrors { get; set; }
    /// <summary>
    /// The document-charset IPP attribute.
    /// See: pwg5100.5-2024 Section 6.2.1
    /// </summary>
    /// <code>document-charset</code>
    [IppAttribute(IppAttributeNames.DocumentCharset, Tag.Charset)]
    public IppValue<Charset>? DocumentCharset { get; set; }
    /// <summary>
    /// The document-format IPP attribute.
    /// See: RFC 8011
    /// </summary>
    /// <code>document-format</code>
    [IppAttribute(IppAttributeNames.DocumentFormat, Tag.MimeMediaType)]
    public IppValue<DocumentFormat>? DocumentFormat { get; set; }
    /// <summary>
    /// The document-format-detected IPP attribute.
    /// See: pwg5100.5-2024 Section 6.2.2
    /// </summary>
    /// <code>document-format-detected</code>
    [IppAttribute(IppAttributeNames.DocumentFormatDetected, Tag.MimeMediaType)]
    public IppValue<DocumentFormat>? DocumentFormatDetected { get; set; }

    /// <summary>
    /// The document-format-ready IPP attribute.
    /// See: PWG 5100.18-2025 Section 7.2.1
    /// </summary>
    /// <code>document-format-ready</code>
    [IppAttribute(IppAttributeNames.DocumentFormatReady, Tag.MimeMediaType)]
    public IppValue<string[]>? DocumentFormatReady { get; set; }

    /// <summary>
    /// The output-device-document-state IPP attribute.
    /// See: PWG 5100.18-2025 Section 7.2.2
    /// </summary>
    /// <code>output-device-document-state</code>
    [IppAttribute(IppAttributeNames.OutputDeviceDocumentState, Tag.Enum)]
    public IppValue<DocumentState>? OutputDeviceDocumentState { get; set; }

    /// <summary>
    /// The output-device-document-state-message IPP attribute.
    /// See: PWG 5100.18-2025 Section 7.2.3
    /// </summary>
    /// <code>output-device-document-state-message</code>
    [IppAttribute(IppAttributeNames.OutputDeviceDocumentStateMessage, Tag.TextWithoutLanguage)]
    public StringWithLanguage? OutputDeviceDocumentStateMessage { get; set; }

    /// <summary>
    /// The output-device-document-state-reasons IPP attribute.
    /// See: PWG 5100.18-2025 Section 7.2.4
    /// </summary>
    /// <code>output-device-document-state-reasons</code>
    [IppAttribute(IppAttributeNames.OutputDeviceDocumentStateReasons, Tag.Keyword)]
    public IppValue<DocumentStateReason[]>? OutputDeviceDocumentStateReasons { get; set; }

    /// <summary>
    /// The document-format-details IPP attribute.
    /// DEPRECATED.
    /// See: PWG 5100.7-2023 Section 6.2.1
    /// </summary>
    /// <code>document-format-details</code>
    [Obsolete("The 'document-format-details' attribute is deprecated. See PWG 5100.7-2023 Section 6.2.1.")]
    [IppAttribute(IppAttributeNames.DocumentFormatDetails)]
    public IppValue<DocumentFormatDetails>? DocumentFormatDetails { get; set; }

    /// <summary>
    /// The document-format-details-detected IPP attribute.
    /// DEPRECATED.
    /// See: PWG 5100.7-2023 Section 6.2.2
    /// </summary>
    /// <code>document-format-details-detected</code>
    [Obsolete("The 'document-format-details-detected' attribute is deprecated. See PWG 5100.7-2023 Section 6.2.2.")]
    [IppAttribute(IppAttributeNames.DocumentFormatDetailsDetected)]
    public IppValue<DocumentFormatDetails>? DocumentFormatDetailsDetected { get; set; }

    /// <summary>
    /// The document-digital-signature IPP attribute.
    /// DEPRECATED.
    /// See: PWG 5100.7-2023 Section 6.2.1
    /// </summary>
    /// <code>document-digital-signature</code>
    [Obsolete("The 'document-digital-signature' attribute is deprecated. See PWG 5100.7-2023 Section 6.2.1.")]
    [IppAttribute(IppAttributeNames.DocumentDigitalSignature, Tag.Keyword)]
    public IppValue<DocumentDigitalSignature>? DocumentDigitalSignature { get; set; }

    /// <summary>
    /// The document-format-version IPP attribute.
    /// DEPRECATED.
    /// See: PWG 5100.7-2023 Section 6.2.1
    /// </summary>
    /// <code>document-format-version</code>
    [Obsolete("The 'document-format-version' attribute is deprecated. See PWG 5100.7-2023 Section 6.2.1.")]
    [IppAttribute(IppAttributeNames.DocumentFormatVersion, Tag.TextWithoutLanguage)]
    public StringWithLanguage? DocumentFormatVersion { get; set; }

    /// <summary>
    /// The document-format-version-detected IPP attribute.
    /// DEPRECATED.
    /// See: PWG 5100.7-2023 Section 6.2.1
    /// </summary>
    /// <code>document-format-version-detected</code>
    [Obsolete("The 'document-format-version-detected' attribute is deprecated. See PWG 5100.7-2023 Section 6.2.1.")]
    [IppAttribute(IppAttributeNames.DocumentFormatVersionDetected, Tag.TextWithoutLanguage)]
    public StringWithLanguage? DocumentFormatVersionDetected { get; set; }

    /// <summary>
    /// The errors-count IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.2.3
    /// </summary>
    /// <code>errors-count</code>
    [IppAttribute(IppAttributeNames.ErrorsCount, Tag.Integer)]
    public IppValue<int>? ErrorsCount { get; set; }

    /// <summary>
    /// The warnings-count IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.2.5
    /// </summary>
    /// <code>warnings-count</code>
    [IppAttribute(IppAttributeNames.WarningsCount, Tag.Integer)]
    public IppValue<int>? WarningsCount { get; set; }

    /// <summary>
    /// The print-content-optimize-actual IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.2.4
    /// </summary>
    /// <code>print-content-optimize-actual</code>
    [IppAttribute(IppAttributeNames.PrintContentOptimizeActual, Tag.Keyword)]
    public IppValue<PrintContentOptimize[]>? PrintContentOptimizeActual { get; set; }
    /// <summary>
    /// The document-job-id IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2
    /// </summary>
    /// <code>document-job-id</code>
    [IppAttribute(IppAttributeNames.DocumentJobId, Tag.Integer)]
    public IppValue<int>? DocumentJobId { get; set; }
    /// <summary>
    /// The document-job-uri IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2
    /// </summary>
    /// <code>document-job-uri</code>
    [IppAttribute(IppAttributeNames.DocumentJobUri, Tag.Uri)]
    public IppValue<Uri>? DocumentJobUri { get; set; }
    /// <summary>
    /// The document-message IPP attribute.
    /// See: pwg5100.5-2024 Section 6.2.3
    /// </summary>
    /// <code>document-message</code>
    [IppAttribute(IppAttributeNames.DocumentMessage, Tag.TextWithoutLanguage)]
    public StringWithLanguage? DocumentMessage { get; set; }
    /// <summary>
    /// The document-name IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.1.1
    /// </summary>
    /// <code>document-name</code>
    [IppAttribute(IppAttributeNames.DocumentName, Tag.NameWithoutLanguage)]
    public StringWithLanguage? DocumentName { get; set; }

    /// <summary>
    /// The document-resource-ids IPP attribute.
    /// See: PWG 5100.22-2025 Section 7.4.1
    /// </summary>
    /// <code>document-resource-ids</code>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.DocumentResourceIds, Tag.Integer)]
    public IppValue<int[]>? DocumentResourceIds { get; set; }
    /// <summary>
    /// The document-natural-language IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2
    /// </summary>
    /// <code>document-natural-language</code>
    [IppAttribute(IppAttributeNames.DocumentNaturalLanguage, Tag.NaturalLanguage)]
    public IppValue<NaturalLanguage>? DocumentNaturalLanguage { get; set; }
    /// <summary>
    /// The document-printer-uri IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2
    /// </summary>
    /// <code>document-printer-uri</code>
    [IppAttribute(IppAttributeNames.DocumentPrinterUri, Tag.Uri)]
    public IppValue<Uri>? DocumentPrinterUri { get; set; }
    /// <summary>
    /// The document-uri IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.1.2
    /// </summary>
    /// <code>document-uri</code>
    [IppAttribute(IppAttributeNames.DocumentUri, Tag.Uri)]
    public IppValue<Uri>? DocumentUri { get; set; }
    /// <summary>
    /// The impressions IPP attribute.
    /// See: pwg5100.1-2022 Section 5.2.1
    /// </summary>
    /// <code>impressions</code>
    [IppAttribute(IppAttributeNames.Impressions, Tag.Integer)]
    public IppValue<int>? Impressions { get; set; }
    /// <summary>
    /// The impressions-completed IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>impressions-completed</code>
    [IppAttribute(IppAttributeNames.ImpressionsCompleted, Tag.Integer)]
    public IppValue<int>? ImpressionsCompleted { get; set; }
    /// <summary>
    /// The k-octets IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2
    /// </summary>
    /// <code>k-octets</code>
    [IppAttribute(IppAttributeNames.KOctets, Tag.Integer)]
    public IppValue<int>? KOctets { get; set; }
    /// <summary>
    /// The k-octets-processed IPP attribute.
    /// See: PWG 5100.5-2024
    /// </summary>
    /// <code>k-octets-processed</code>
    [IppAttribute(IppAttributeNames.KOctetsProcessed, Tag.Integer)]
    public IppValue<int>? KOctetsProcessed { get; set; }
    /// <summary>
    /// The last-document IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2.5
    /// </summary>
    /// <code>last-document</code>
    [IppAttribute(IppAttributeNames.LastDocument, Tag.Boolean)]
    public IppValue<bool>? LastDocument { get; set; }
    /// <summary>
    /// The media-sheets IPP attribute.
    /// See: RFC 8011 Section 5.3.17.3
    /// See: PWG 5100.5-2024 Section 6.2
    /// </summary>
    /// <code>media-sheets</code>
    [IppAttribute(IppAttributeNames.MediaSheets, Tag.Integer)]
    public IppValue<int>? MediaSheets { get; set; }
    /// <summary>
    /// The media-sheets-completed IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2
    /// </summary>
    /// <code>media-sheets-completed</code>
    [IppAttribute(IppAttributeNames.MediaSheetsCompleted, Tag.Integer)]
    public IppValue<int>? MediaSheetsCompleted { get; set; }
    /// <summary>
    /// The more-info IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>more-info</code>
    [IppAttribute(IppAttributeNames.MoreInfo, Tag.Uri)]
    public IppValue<Uri>? MoreInfo { get; set; }
    /// <summary>
    /// The output-device-assigned IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.2.2
    /// </summary>
    /// <code>output-device-assigned</code>
    [IppAttribute(IppAttributeNames.OutputDeviceAssigned, Tag.NameWithoutLanguage)]
    public StringWithLanguage? OutputDeviceAssigned { get; set; }
    /// <summary>
    /// The printer-up-time IPP attribute.
    /// See: pwg5100.13 - IPP Driver Replacement Extensions v2.0 Section 6.6.5
    /// </summary>
    /// <code>printer-up-time</code>
    [IppAttribute(IppAttributeNames.PrinterUpTime, Tag.Integer)]
    public IppValue<int>? PrinterUpTime { get; set; }
    /// <summary>
    /// The time-at-completed IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>time-at-completed</code>
    [IppAttribute(IppAttributeNames.TimeAtCompleted, Tag.Integer)]
    public IppValue<int>? TimeAtCompleted { get; set; }
    /// <summary>
    /// The time-at-creation IPP attribute.
    /// Type: integer(MIN:MAX)
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>time-at-creation</code>
    [IppAttribute(IppAttributeNames.TimeAtCreation, Tag.Integer)]
    public IppValue<int>? TimeAtCreation { get; set; }
    /// <summary>
    /// The time-at-processing IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>time-at-processing</code>
    [IppAttribute(IppAttributeNames.TimeAtProcessing, Tag.Integer)]
    public IppValue<int>? TimeAtProcessing { get; set; }
    /// <summary>
    /// The pages IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.4.1
    /// </summary>
    /// <code>pages</code>
    [IppAttribute(IppAttributeNames.Pages, Tag.Integer)]
    public IppValue<int>? Pages { get; set; }
    /// <summary>
    /// The pages-completed IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.5.1
    /// </summary>
    /// <code>pages-completed</code>
    [IppAttribute(IppAttributeNames.PagesCompleted, Tag.Integer)]
    public IppValue<int>? PagesCompleted { get; set; }

    /// <summary>
    /// The input-attributes-actual IPP attribute.
    /// See: PWG 5100.15-2013 Section 7.5.1
    /// </summary>
    /// <code>input-attributes-actual</code>
    [IppAttribute(IppAttributeNames.InputAttributesActual)]
    public IppValue<DocumentTemplateAttributes>? InputAttributesActual { get; set; }



    /// <summary>
    /// Arbitrary metadata associated with the document (1setOf octetString).
    /// See: PWG 5100.13-2023 Section 6.3.1
    /// </summary>
    /// <code>document-metadata</code>
    [Metadata]
    [IppAttribute(IppAttributeNames.DocumentMetadata, Tag.OctetStringWithAnUnspecifiedFormat)]
    public IppValue<DocumentMetadata>? DocumentMetadata { get; set; }


}
