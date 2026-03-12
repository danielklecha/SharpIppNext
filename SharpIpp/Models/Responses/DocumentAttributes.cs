using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Models.Responses;

/// <summary>
///     PWG 5100.5-2024 Section 6.2 / Section 8.3-8.5
///     Document Status attributes returned in Document Creation responses.
/// </summary>
public class DocumentAttributes : IIppCollection
{
    public bool IsNoValue { get; set; }

    /// <summary>
    ///     The document-number attribute is the ordinal number of the Document within the Job.
    /// </summary>
    public int? DocumentNumber { get; set; }

    /// <summary>
    ///     The document-state attribute indicates the current state of the Document.
    /// </summary>
    public DocumentState? DocumentState { get; set; }

    /// <summary>
    ///     The document-state-reasons attribute provides additional information about the document's current state.
    /// </summary>
    public DocumentStateReason[]? DocumentStateReasons { get; set; }

    /// <summary>
    ///     The document-state-message attribute provides a localized description of the document-state and document-state-reasons.
    /// </summary>
    public string? DocumentStateMessage { get; set; }
    /// <summary>
    ///     The print-content-optimize attribute indicates the optimization for the print content.
    /// </summary>
    public PrintContentOptimize? PrintContentOptimize { get; set; }

    /// <summary>
    ///     The print-content-optimize-supported attribute identifies the supported optimizations for the print content.
    /// </summary>
    public PrintContentOptimize[]? PrintContentOptimizeSupported { get; set; }

    /// <summary>
    ///     The printer-state-reasons attribute provides additional information about the printer's current state.
    /// </summary>
    public PrinterStateReason[]? PrinterStateReasons { get; set; }

    /// <summary>
    ///     The attributes-charset attribute specifies the charset used for the attributes.
    /// </summary>
    public string? AttributesCharset { get; set; }

    /// <summary>
    ///     The attributes-natural-language attribute specifies the natural language used for the attributes.
    /// </summary>
    public string? AttributesNaturalLanguage { get; set; }

    /// <summary>
    ///     The current-page-order attribute identifies the order of pages.
    /// </summary>
    public CurrentPageOrder? CurrentPageOrder { get; set; }

    /// <summary>
    ///     The date-time-at-completed attribute indicates when the document moved to 'completed', 'canceled', or 'aborted' state.
    /// </summary>
    public DateTimeOffset? DateTimeAtCompleted { get; set; }

    /// <summary>
    ///     The date-time-at-creation attribute indicates when the document was created.
    /// </summary>
    public DateTimeOffset? DateTimeAtCreation { get; set; }

    /// <summary>
    ///     The date-time-at-processing attribute indicates when the document first began processing.
    /// </summary>
    public DateTimeOffset? DateTimeAtProcessing { get; set; }

    /// <summary>
    ///     The detailed-status-messages attribute provides more detail about the document's status.
    /// </summary>
    public string[]? DetailedStatusMessages { get; set; }

    /// <summary>
    ///     The document-access-errors attribute contains a list of errors occurred during document access.
    /// </summary>
    public string[]? DocumentAccessErrors { get; set; }

    /// <summary>
    ///     The document-charset attribute specifies the charset used in the document data.
    /// </summary>
    public string? DocumentCharset { get; set; }

    /// <summary>
    ///     The document-format attribute identify the format of the document data.
    /// </summary>
    public string? DocumentFormat { get; set; }

    /// <summary>
    ///     The document-format-detected attribute indicates the format of the document data as detected by the printer.
    /// </summary>
    public string? DocumentFormatDetected { get; set; }

    /// <summary>
    ///     The document-job-id attribute identifies the job to which the document belongs.
    /// </summary>
    public int? DocumentJobId { get; set; }

    /// <summary>
    ///     The document-job-uri attribute identifies the job to which the document belongs.
    /// </summary>
    public string? DocumentJobUri { get; set; }

    /// <summary>
    ///     The document-message attribute contains a message from the user or operator.
    /// </summary>
    public string? DocumentMessage { get; set; }

    /// <summary>
    ///     The document-name attribute specifies the name of the document.
    /// </summary>
    public string? DocumentName { get; set; }

    /// <summary>
    ///     The document-natural-language attribute specifies the natural language of the document.
    /// </summary>
    public string? DocumentNaturalLanguage { get; set; }

    /// <summary>
    ///     The document-printer-uri attribute identifies the printer that created the document.
    /// </summary>
    public string? DocumentPrinterUri { get; set; }

    /// <summary>
    ///     The document-uri attribute identifies the source location of the document data.
    /// </summary>
    public string? DocumentUri { get; set; }

    /// <summary>
    ///     The impressions attribute specifies the total size of the document in impressions.
    /// </summary>
    public int? Impressions { get; set; }

    /// <summary>
    ///     The impressions-completed attribute specifies the number of impressions completed.
    /// </summary>
    public int? ImpressionsCompleted { get; set; }

    /// <summary>
    ///     The k-octets attribute specifies the total size of the document in K octets.
    /// </summary>
    public int? KOctets { get; set; }

    /// <summary>
    ///     The k-octets-processed attribute specifies the number of K octets processed.
    /// </summary>
    public int? KOctetsProcessed { get; set; }

    /// <summary>
    ///     The last-document attribute indicates whether this is the last document in the job.
    /// </summary>
    public bool? LastDocument { get; set; }

    /// <summary>
    ///     The media-sheets attribute specifies the total size of the document in media sheets.
    /// </summary>
    public int? MediaSheets { get; set; }

    /// <summary>
    ///     The media-sheets-completed attribute specifies the number of media sheets completed.
    /// </summary>
    public int? MediaSheetsCompleted { get; set; }

    /// <summary>
    ///     The more-info attribute specifies a URI that contains more information about the document.
    /// </summary>
    public string? MoreInfo { get; set; }

    /// <summary>
    ///     The output-device-assigned attribute identifies the output device assigned to the document.
    /// </summary>
    public string? OutputDeviceAssigned { get; set; }

    /// <summary>
    ///     The printer-up-time attribute indicates the time at which the printer was started.
    /// </summary>
    public int? PrinterUpTime { get; set; }

    /// <summary>
    ///     The time-at-completed attribute indicates when the document moved to 'completed', 'canceled', or 'aborted' state.
    /// </summary>
    public int? TimeAtCompleted { get; set; }

    /// <summary>
    ///     The time-at-creation attribute indicates when the document was created.
    /// </summary>
    public int? TimeAtCreation { get; set; }

    /// <summary>
    ///     The time-at-processing attribute indicates when the document first began processing.
    /// </summary>
    public int? TimeAtProcessing { get; set; }

}
