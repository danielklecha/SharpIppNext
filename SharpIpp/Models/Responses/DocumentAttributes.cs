using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;
/// <summary>
/// PWG 5100.5-2024 Section 6.2 / Section 8.3-8.5
/// Document Status attributes returned in Document Creation responses.
/// </summary>
public class DocumentAttributes : IIppCollection
{
    public bool IsValue { get; set; } = true;
    /// <summary>
    /// The document-number IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2.4
    /// </summary>
    /// <code>document-number</code>
    public int? DocumentNumber { get; set; }
    /// <summary>
    /// The document-state IPP attribute.
    /// See: PWG 5100.5-2024
    /// </summary>
    /// <code>document-state</code>
    public DocumentState? DocumentState { get; set; }
    /// <summary>
    /// The document-state-reasons IPP attribute.
    /// See: pwg5100.13 - IPP Driver Replacement Extensions v2.0 Section 9.1
    /// </summary>
    /// <code>document-state-reasons</code>
    public DocumentStateReason[]? DocumentStateReasons { get; set; }
    /// <summary>
    /// The document-state-message IPP attribute.
    /// See: pwg5100.18 - IPP Shared Infrastructure Extensions v1.1
    /// </summary>
    /// <code>document-state-message</code>
    public string? DocumentStateMessage { get; set; }
    /// <summary>
    /// The print-content-optimize IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.3.3
    /// </summary>
    /// <code>print-content-optimize</code>
    public PrintContentOptimize? PrintContentOptimize { get; set; }
    /// <summary>
    /// The attributes-charset IPP attribute.
    /// See: RFC 8011 Section 5.3.19
    /// </summary>
    /// <code>attributes-charset</code>
    public string? AttributesCharset { get; set; }
    /// <summary>
    /// The attributes-natural-language IPP attribute.
    /// See: RFC 8011 Section 5.3.20
    /// </summary>
    /// <code>attributes-natural-language</code>
    public string? AttributesNaturalLanguage { get; set; }

    /// <summary>
    /// The current-page-order IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2 and PWG 5100.3-2001.
    /// </summary>
    /// <code>current-page-order</code>
    public CurrentPageOrder? CurrentPageOrder { get; set; }

    /// <summary>
    /// The date-time-at-completed IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>date-time-at-completed</code>
    public DateTimeOffset? DateTimeAtCompleted { get; set; }
    /// <summary>
    /// The date-time-at-creation IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>date-time-at-creation</code>
    public DateTimeOffset? DateTimeAtCreation { get; set; }
    /// <summary>
    /// The date-time-at-processing IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>date-time-at-processing</code>
    public DateTimeOffset? DateTimeAtProcessing { get; set; }
    /// <summary>
    /// The detailed-status-messages IPP attribute.
    /// See: pwg5100.11 - IPP Enterprise Printing Extensions v2.0 Section 14.1
    /// </summary>
    /// <code>detailed-status-messages</code>
    public string[]? DetailedStatusMessages { get; set; }
    /// <summary>
    /// The document-access-errors IPP attribute.
    /// See: pwg5100.5-2024
    /// </summary>
    /// <code>document-access-errors</code>
    public string[]? DocumentAccessErrors { get; set; }
    /// <summary>
    /// The document-charset IPP attribute.
    /// See: pwg5100.5-2024 Section 6.2.1
    /// </summary>
    /// <code>document-charset</code>
    public string? DocumentCharset { get; set; }
    /// <summary>
    /// The document-format IPP attribute.
    /// See: RFC 8011
    /// </summary>
    /// <code>document-format</code>
    public string? DocumentFormat { get; set; }
    /// <summary>
    /// The document-format-detected IPP attribute.
    /// See: pwg5100.5-2024 Section 6.2.2
    /// </summary>
    /// <code>document-format-detected</code>
    public string? DocumentFormatDetected { get; set; }

    /// <summary>
    /// The document-format-details IPP attribute.
    /// DEPRECATED.
    /// See: PWG 5100.7-2023 Section 6.2.1
    /// </summary>
    /// <code>document-format-details</code>
    public DocumentFormatDetails? DocumentFormatDetails { get; set; }

    /// <summary>
    /// The document-format-details-detected IPP attribute.
    /// DEPRECATED.
    /// See: PWG 5100.7-2023 Section 6.2.2
    /// </summary>
    /// <code>document-format-details-detected</code>
    public DocumentFormatDetails? DocumentFormatDetailsDetected { get; set; }

    /// <summary>
    /// The errors-count IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.2.3
    /// </summary>
    /// <code>errors-count</code>
    public int? ErrorsCount { get; set; }

    /// <summary>
    /// The warnings-count IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.2.5
    /// </summary>
    /// <code>warnings-count</code>
    public int? WarningsCount { get; set; }

    /// <summary>
    /// The print-content-optimize-actual IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.2.4
    /// </summary>
    /// <code>print-content-optimize-actual</code>
    public PrintContentOptimize[]? PrintContentOptimizeActual { get; set; }
    /// <summary>
    /// The document-job-id IPP attribute.
    /// See: PWG 5100.18-2025
    /// </summary>
    /// <code>document-job-id</code>
    public int? DocumentJobId { get; set; }
    /// <summary>
    /// The document-job-uri IPP attribute.
    /// See: pwg5100.5-2024
    /// </summary>
    /// <code>document-job-uri</code>
    public string? DocumentJobUri { get; set; }
    /// <summary>
    /// The document-message IPP attribute.
    /// See: pwg5100.5-2024 Section 6.2.3
    /// </summary>
    /// <code>document-message</code>
    public string? DocumentMessage { get; set; }
    /// <summary>
    /// The document-name IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.1.1
    /// </summary>
    /// <code>document-name</code>
    public string? DocumentName { get; set; }

    /// <summary>
    /// The document-resource-ids IPP attribute.
    /// See: PWG 5100.22-2025 Section 7.4.1
    /// </summary>
    /// <code>document-resource-ids</code>
    public int[]? DocumentResourceIds { get; set; }
    /// <summary>
    /// The document-natural-language IPP attribute.
    /// See: pwg5100.5-2024
    /// </summary>
    /// <code>document-natural-language</code>
    public string? DocumentNaturalLanguage { get; set; }
    /// <summary>
    /// The document-printer-uri IPP attribute.
    /// See: PWG 5100.18-2025
    /// </summary>
    /// <code>document-printer-uri</code>
    public string? DocumentPrinterUri { get; set; }
    /// <summary>
    /// The document-uri IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.1.2
    /// </summary>
    /// <code>document-uri</code>
    public string? DocumentUri { get; set; }
    /// <summary>
    /// The impressions IPP attribute.
    /// See: pwg5100.1-2022 Section 5.2.1
    /// </summary>
    /// <code>impressions</code>
    public int? Impressions { get; set; }
    /// <summary>
    /// The impressions-completed IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>impressions-completed</code>
    public int? ImpressionsCompleted { get; set; }
    /// <summary>
    /// The k-octets IPP attribute.
    /// See: pwg5100.13 - IPP Driver Replacement Extensions v2.0
    /// </summary>
    /// <code>k-octets</code>
    public int? KOctets { get; set; }
    /// <summary>
    /// The k-octets-processed IPP attribute.
    /// See: PWG 5100.5-2024
    /// </summary>
    /// <code>k-octets-processed</code>
    public int? KOctetsProcessed { get; set; }
    /// <summary>
    /// The last-document IPP attribute.
    /// See: PWG 5100.5-2024 Section 6.2.5
    /// </summary>
    /// <code>last-document</code>
    public bool? LastDocument { get; set; }
    /// <summary>
    /// The media-sheets IPP attribute.
    /// See: pwg5100.1-2022 Section 2.2
    /// </summary>
    /// <code>media-sheets</code>
    public int? MediaSheets { get; set; }
    /// <summary>
    /// The media-sheets-completed IPP attribute.
    /// See: PWG 5100.5-2024
    /// </summary>
    /// <code>media-sheets-completed</code>
    public int? MediaSheetsCompleted { get; set; }
    /// <summary>
    /// The more-info IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>more-info</code>
    public string? MoreInfo { get; set; }
    /// <summary>
    /// The output-device-assigned IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.2.2
    /// </summary>
    /// <code>output-device-assigned</code>
    public string? OutputDeviceAssigned { get; set; }
    /// <summary>
    /// The printer-up-time IPP attribute.
    /// See: pwg5100.13 - IPP Driver Replacement Extensions v2.0 Section 6.6.5
    /// </summary>
    /// <code>printer-up-time</code>
    public int? PrinterUpTime { get; set; }
    /// <summary>
    /// The time-at-completed IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>time-at-completed</code>
    public int? TimeAtCompleted { get; set; }
    /// <summary>
    /// The time-at-creation IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>time-at-creation</code>
    public int? TimeAtCreation { get; set; }
    /// <summary>
    /// The time-at-processing IPP attribute.
    /// See: pwg5100.15 - IPP FaxOut Service Section 7.4.18
    /// </summary>
    /// <code>time-at-processing</code>
    public int? TimeAtProcessing { get; set; }
    /// <summary>
    /// The pages IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.4.1
    /// </summary>
    /// <code>pages</code>
    public int? Pages { get; set; }
    /// <summary>
    /// The pages-completed IPP attribute.
    /// See: PWG 5100.7-2023 Section 6.5.1
    /// </summary>
    /// <code>pages-completed</code>
    public int? PagesCompleted { get; set; }

    /// <summary>
    /// The input-attributes-actual IPP attribute.
    /// See: PWG 5100.15-2013 Section 7.5.1
    /// </summary>
    /// <code>input-attributes-actual</code>
    public DocumentTemplateAttributes? InputAttributesActual { get; set; }
}
