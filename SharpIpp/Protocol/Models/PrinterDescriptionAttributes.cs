using SharpIpp.Mapping;
using SharpIpp.Protocol;
using System;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Attributes describing the capabilities and configuration of a Printer object.
/// See: RFC 8011
/// See: PWG 5100.1-2022
/// See: PWG 5100.2-2001
/// See: PWG 5100.3-2023
/// See: PWG 5100.7-2023
/// See: PWG 5100.9-2009
/// See: PWG 5100.11-2024
/// See: PWG 5100.13-2023
/// See: PWG 5100.15-2013
/// See: PWG 5100.17-2014
/// See: PWG 5100.18-2025
/// See: PWG 5100.21-2019
/// See: PWG 5100.22-2025
/// </summary>
public class PrinterDescriptionAttributes
{
    /// <summary>
    /// printer-uri-supported
    /// See: RFC 8011 Section 5.4.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterUriSupported, Tag.Uri)]
    public Uri[]? PrinterUriSupported { get; set; }

    /// <summary>
    /// uri-security-supported
    /// See: RFC 8011 Section 5.4.3
    /// </summary>
    [IppAttribute(IppAttributeNames.UriSecuritySupported, Tag.Keyword)]
    public UriSecurity[]? UriSecuritySupported { get; set; }

    /// <summary>
    /// uri-authentication-supported
    /// See: RFC 8011 Section 5.4.2
    /// </summary>
    [IppAttribute(IppAttributeNames.UriAuthenticationSupported, Tag.Keyword)]
    public UriAuthentication[]? UriAuthenticationSupported { get; set; }

    /// <summary>
    /// printer-name
    /// See: RFC 8011 Section 5.4.4
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterName, Tag.NameWithoutLanguage)]
    public string? PrinterName { get; set; }

    /// <summary>
    /// printer-location
    /// See: RFC 8011 Section 5.4.5
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterLocation, Tag.TextWithoutLanguage)]
    public string? PrinterLocation { get; set; }

    /// <summary>
    /// printer-info
    /// See: RFC 8011 Section 5.4.6
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterInfo, Tag.TextWithoutLanguage)]
    public string? PrinterInfo { get; set; }

    /// <summary>
    /// printer-more-info
    /// See: RFC 8011 Section 5.4.7
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMoreInfo, Tag.Uri)]
    public Uri? PrinterMoreInfo { get; set; }

    /// <summary>
    /// printer-driver-installer
    /// See: RFC 8011 Section 5.4.8
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterDriverInstaller, Tag.Uri)]
    public Uri? PrinterDriverInstaller { get; set; }

    /// <summary>
    /// printer-make-and-model
    /// See: RFC 8011 Section 5.4.9
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMakeAndModel, Tag.TextWithoutLanguage)]
    public string? PrinterMakeAndModel { get; set; }

    /// <summary>
    /// printer-more-info-manufacturer
    /// See: RFC 8011 Section 5.4.10
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMoreInfoManufacturer, Tag.Uri)]
    public Uri? PrinterMoreInfoManufacturer { get; set; }

    /// <summary>
    /// printer-state
    /// See: RFC 8011 Section 5.4.11
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterState, Tag.Enum)]
    public PrinterState? PrinterState { get; set; }

    /// <summary>
    /// printer-state-reasons
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStateReasons, Tag.Keyword)]
    public PrinterStateReason[]? PrinterStateReasons { get; set; }

    /// <summary>
    /// printer-state-message
    /// See: RFC 8011 Section 5.4.13
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStateMessage, Tag.TextWithoutLanguage)]
    public string? PrinterStateMessage { get; set; }

    /// <summary>
    /// printer-state-change-time
    /// See: RFC 8011 Section 5.4.14
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStateChangeTime, Tag.Integer)]
    public int? PrinterStateChangeTime { get; set; }

    /// <summary>
    /// printer-state-change-date-time
    /// See: RFC 8011 Section 5.4.15
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStateChangeDateTime, Tag.DateTime)]
    public DateTimeOffset? PrinterStateChangeDateTime { get; set; }

    /// <summary>
    /// printer-detailed-status-messages
    /// See: PWG 5100.7-2023 Section 6.10.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterDetailedStatusMessages, Tag.TextWithoutLanguage)]
    public string[]? PrinterDetailedStatusMessages { get; set; }

    /// <summary>
    /// ipp-versions-supported
    /// See: RFC 8011 Section 5.4.16
    /// </summary>
    [IppAttribute(IppAttributeNames.IppVersionsSupported, Tag.Keyword)]
    public IppVersion[]? IppVersionsSupported { get; set; }

    /// <summary>
    /// operations-supported
    /// See: RFC 8011 Section 5.4.17
    /// </summary>
    [IppAttribute(IppAttributeNames.OperationsSupported, Tag.Enum)]
    public IppOperation[]? OperationsSupported { get; set; }

    /// <summary>
    /// multiple-document-jobs-supported
    /// See: RFC 8011 Section 5.4.16
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleDocumentJobsSupported, Tag.Boolean)]
    public bool? MultipleDocumentJobsSupported { get; set; }

    /// <summary>
    /// multiple-document-handling-default
    /// See: RFC 2911 Section 4.2.4
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleDocumentHandlingDefault, Tag.Keyword)]
    public MultipleDocumentHandling? MultipleDocumentHandlingDefault { get; set; }

    /// <summary>
    /// multiple-document-handling-supported
    /// See: RFC 2911 Section 4.2.4
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleDocumentHandlingSupported, Tag.Keyword)]
    public MultipleDocumentHandling[]? MultipleDocumentHandlingSupported { get; set; }

    /// <summary>
    /// charset-configured
    /// See: RFC 8011 Section 5.4.17
    /// </summary>
    [IppAttribute(IppAttributeNames.CharsetConfigured, Tag.Charset)]
    public string? CharsetConfigured { get; set; }

    /// <summary>
    /// charset-supported
    /// See: RFC 8011 Section 5.4.18
    /// </summary>
    [IppAttribute(IppAttributeNames.CharsetSupported, Tag.Charset)]
    public string[]? CharsetSupported { get; set; }

    /// <summary>
    /// natural-language-configured
    /// See: RFC 8011 Section 5.4.19
    /// </summary>
    [IppAttribute(IppAttributeNames.NaturalLanguageConfigured, Tag.NaturalLanguage)]
    public NaturalLanguage? NaturalLanguageConfigured { get; set; }

    /// <summary>
    /// generated-natural-language-supported
    /// See: RFC 8011 Section 5.4.20
    /// </summary>
    [IppAttribute(IppAttributeNames.GeneratedNaturalLanguageSupported, Tag.NaturalLanguage)]
    public NaturalLanguage[]? GeneratedNaturalLanguageSupported { get; set; }

    /// <summary>
    /// client-info-supported
    /// See: PWG 5100.7-2023 Section 6.9.5
    /// </summary>
    [IppAttribute(IppAttributeNames.ClientInfoSupported, Tag.Keyword)]
    public ClientInfoMember[]? ClientInfoSupported { get; set; }

    /// <summary>
    /// max-client-info-supported
    /// See: PWG 5100.7-2023 Section 6.9.41
    /// </summary>
    [IppAttribute(IppAttributeNames.MaxClientInfoSupported, Tag.Integer)]
    public int? MaxClientInfoSupported { get; set; }

    /// <summary>
    /// document-charset-default
    /// See: PWG 5100.7-2023 Section 6.9.16
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentCharsetDefault, Tag.Charset)]
    public Charset? DocumentCharsetDefault { get; set; }

    /// <summary>
    /// document-charset-supported
    /// See: PWG 5100.7-2023 Section 6.9.17
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentCharsetSupported, Tag.Charset)]
    public Charset[]? DocumentCharsetSupported { get; set; }

    /// <summary>
    /// document-format-details-supported
    /// See: PWG 5100.7-2023 Section 6.9.20
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentFormatDetailsSupported, Tag.Keyword)]
    public DocumentFormatDetail[]? DocumentFormatDetailsSupported { get; set; }

    /// <summary>
    /// document-natural-language-default
    /// See: PWG 5100.7-2023 Section 6.9.48
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentNaturalLanguageDefault, Tag.NaturalLanguage)]
    public NaturalLanguage? DocumentNaturalLanguageDefault { get; set; }

    /// <summary>
    /// document-natural-language-supported
    /// See: PWG 5100.7-2023 Section 6.9.49
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentNaturalLanguageSupported, Tag.NaturalLanguage)]
    public NaturalLanguage[]? DocumentNaturalLanguageSupported { get; set; }

    /// <summary>
    /// job-ids-supported
    /// See: PWG 5100.7-2023 Section 6.9.26
    /// </summary>
    [IppAttribute(IppAttributeNames.JobIdsSupported, Tag.Boolean)]
    public bool? JobIdsSupported { get; set; }

    /// <summary>
    /// job-mandatory-attributes-supported
    /// See: PWG 5100.7-2023 Section 6.9.28
    /// </summary>
    [IppAttribute(IppAttributeNames.JobMandatoryAttributesSupported, Tag.Boolean)]
    public bool? JobMandatoryAttributesSupported { get; set; }

    /// <summary>
    /// job-sheets-col-default
    /// See: PWG 5100.7-2023 Section 6.9.29
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSheetsColDefault)]
    public JobSheetsCol? JobSheetsColDefault { get; set; }

    /// <summary>
    /// job-sheets-col-supported
    /// See: PWG 5100.7-2023 Section 6.9.30
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSheetsColSupported, Tag.Keyword)]
    public JobSheetsColMember[]? JobSheetsColSupported { get; set; }

    /// <summary>
    /// document-format-default
    /// See: RFC 8011 Section 5.4.21
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentFormatDefault, Tag.MimeMediaType)]
    public DocumentFormat? DocumentFormatDefault { get; set; }

    /// <summary>
    /// document-format-supported
    /// See: RFC 8011 Section 5.4.22
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentFormatSupported, Tag.MimeMediaType)]
    public string[]? DocumentFormatSupported { get; set; }

    /// <summary>
    /// printer-is-accepting-jobs
    /// See: RFC 8011 Section 5.4.23
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterIsAcceptingJobs, Tag.Boolean)]
    public bool? PrinterIsAcceptingJobs { get; set; }

    /// <summary>
    /// queued-job-count
    /// See: RFC 8011 Section 5.4.24
    /// </summary>
    [IppAttribute(IppAttributeNames.QueuedJobCount, Tag.Integer)]
    public int? QueuedJobCount { get; set; }

    /// <summary>
    /// printer-message-from-operator
    /// See: RFC 8011 Section 5.4.25
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMessageFromOperator, Tag.TextWithoutLanguage)]
    public string? PrinterMessageFromOperator { get; set; }

    /// <summary>
    /// color-supported
    /// See: RFC 8011 Section 5.4.26
    /// </summary>
    [IppAttribute(IppAttributeNames.ColorSupported, Tag.Boolean)]
    public bool? ColorSupported { get; set; }

    /// <summary>
    /// reference-uri-schemes-supported
    /// See: RFC 8011 Section 5.4.27
    /// </summary>
    [IppAttribute(IppAttributeNames.ReferenceUriSchemesSupported, Tag.UriScheme)]
    public UriScheme[]? ReferenceUriSchemesSupported { get; set; }

    /// <summary>
    /// pdl-override-supported
    /// See: RFC 8011 Section 5.4.28
    /// </summary>
    [IppAttribute(IppAttributeNames.PdlOverrideSupported, Tag.Keyword)]
    public PdlOverride? PdlOverrideSupported { get; set; }

    /// <summary>
    /// overrides-supported
    /// See: PWG 5100.6-2003 Section 4.1.7
    /// </summary>
    [IppAttribute(IppAttributeNames.OverridesSupported, Tag.Keyword)]
    public OverrideSupported[]? OverridesSupported { get; set; }

    /// <summary>
    /// printer-up-time
    /// See: RFC 8011 Section 5.4.29
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterUpTime, Tag.Integer)]
    public int? PrinterUpTime { get; set; }

    /// <summary>
    /// printer-current-time
    /// See: RFC 8011 Section 5.4.30
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterCurrentTime, Tag.DateTime)]
    public DateTimeOffset? PrinterCurrentTime { get; set; }

    /// <summary>
    /// printer-config-change-time
    /// See: RFC 8011 Section 5.4.31
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterConfigChangeTime, Tag.Integer)]
    public int? PrinterConfigChangeTime { get; set; }

    /// <summary>
    /// printer-config-change-date-time
    /// See: RFC 8011 Section 5.4.31
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterConfigChangeDateTime, Tag.DateTime)]
    public DateTimeOffset? PrinterConfigChangeDateTime { get; set; }

    /// <summary>
    /// printer-config-changes
    /// See: PWG 5100.22-2025 Section 7.7.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterConfigChanges, Tag.Integer)]
    public int? PrinterConfigChanges { get; set; }

    /// <summary>
    /// printer-contact-col
    /// See: PWG 5100.22-2025 Section 7.6.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterContactCol)]
    public SystemContact[]? PrinterContactCol { get; set; }

    /// <summary>
    /// printer-geo-location
    /// See: PWG 5100.22-2025 Section 7.1.7
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterGeoLocation, Tag.Uri)]
    public Uri? PrinterGeoLocation { get; set; }

    /// <summary>
    /// printer-ids
    /// See: PWG 5100.22-2025 Section 7.1.6
    /// </summary>
    [Range(1, 65535)]
    [IppAttribute(IppAttributeNames.PrinterIds, Tag.Integer)]
    public int[]? PrinterIds { get; set; }

    /// <summary>
    /// printer-impressions-completed
    /// See: PWG 5100.22-2025 Section 7.7.3
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterImpressionsCompleted, Tag.Integer)]
    public int? PrinterImpressionsCompleted { get; set; }

    /// <summary>
    /// printer-impressions-completed-col
    /// See: PWG 5100.22-2025 Section 7.7.4
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterImpressionsCompletedCol, Tag.Integer)]
    public int? PrinterImpressionsCompletedCol { get; set; }

    /// <summary>
    /// printer-media-sheets-completed
    /// See: PWG 5100.22-2025 Section 7.7.5
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMediaSheetsCompleted, Tag.Integer)]
    public int? PrinterMediaSheetsCompleted { get; set; }

    /// <summary>
    /// printer-media-sheets-completed-col
    /// See: PWG 5100.22-2025 Section 7.7.6
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMediaSheetsCompletedCol, Tag.Integer)]
    public int? PrinterMediaSheetsCompletedCol { get; set; }

    /// <summary>
    /// printer-pages-completed
    /// See: PWG 5100.22-2025 Section 7.7.7
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterPagesCompleted, Tag.Integer)]
    public int? PrinterPagesCompleted { get; set; }

    /// <summary>
    /// printer-pages-completed-col
    /// See: PWG 5100.22-2025 Section 7.7.8
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterPagesCompletedCol, Tag.Integer)]
    public int? PrinterPagesCompletedCol { get; set; }

    /// <summary>
    /// multiple-operation-time-out
    /// Type: integer(1:MAX) (Rec: 60-240)
    /// See: RFC 8011 Section 5.4.31
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleOperationTimeOut, Tag.Integer)]
    public int? MultipleOperationTimeOut { get; set; }

    /// <summary>
    /// multiple-operation-time-out-action
    /// See: PWG 5100.13-2023 Section 6.5.19
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleOperationTimeOutAction, Tag.Keyword)]
    public MultipleOperationTimeOutAction? MultipleOperationTimeOutAction { get; set; }

    /// <summary>
    /// compression-supported
    /// See: RFC 8011 Section 5.4.32
    /// </summary>
    [IppAttribute(IppAttributeNames.CompressionSupported, Tag.Keyword)]
    public Compression[]? CompressionSupported { get; set; }

    /// <summary>
    /// compression-default
    /// See: RFC 8011 Section 5.4.32
    /// </summary>
    [IppAttribute(IppAttributeNames.CompressionDefault, Tag.Keyword)]
    public Compression? CompressionDefault { get; set; }

    /// <summary>
    /// job-k-octets-supported
    /// See: RFC 8011 Section 5.4.33
    /// </summary>
    [IppAttribute(IppAttributeNames.JobKOctetsSupported, Tag.RangeOfInteger)]
    public Range? JobKOctetsSupported { get; set; }

    /// <summary>
    /// jpeg-k-octets-supported
    /// See: PWG 5100.13-2023 Section 6.5.12
    /// </summary>
    [IppAttribute(IppAttributeNames.JpegKOctetsSupported, Tag.RangeOfInteger)]
    public Range? JpegKOctetsSupported { get; set; }

    /// <summary>
    /// pdf-k-octets-supported
    /// See: PWG 5100.13-2023 Section 6.5.20
    /// </summary>
    [IppAttribute(IppAttributeNames.PdfKOctetsSupported, Tag.RangeOfInteger)]
    public Range? PdfKOctetsSupported { get; set; }

    /// <summary>
    /// job-impressions-supported
    /// See: RFC 8011 Section 5.4.34
    /// </summary>
    [IppAttribute(IppAttributeNames.JobImpressionsSupported, Tag.RangeOfInteger)]
    public Range? JobImpressionsSupported { get; set; }

    /// <summary>
    /// job-media-sheets-supported
    /// See: RFC 8011 Section 5.4.35
    /// </summary>
    [IppAttribute(IppAttributeNames.JobMediaSheetsSupported, Tag.RangeOfInteger)]
    public Range? JobMediaSheetsSupported { get; set; }

    /// <summary>
    /// job-sheets-default
    /// See: RFC 2911 Section 4.2.3
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSheetsDefault, Tag.Keyword)]
    public JobSheets? JobSheetsDefault { get; set; }

    /// <summary>
    /// job-sheets-supported
    /// See: RFC 2911 Section 4.2.3
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSheetsSupported, Tag.Keyword)]
    public JobSheets[]? JobSheetsSupported { get; set; }

    /// <summary>
    /// number-up-default
    /// See: RFC 2911 Section 4.2.7
    /// </summary>
    [IppAttribute(IppAttributeNames.NumberUpDefault, Tag.Integer)]
    public int? NumberUpDefault { get; set; }

    /// <summary>
    /// number-up-supported
    /// See: RFC 2911 Section 4.2.7
    /// </summary>
    [IppAttribute(IppAttributeNames.NumberUpSupported, Tag.RangeOfInteger)]
    public Range[]? NumberUpSupported { get; set; }

    /// <summary>
    /// pages-per-minute
    /// Type: integer(0:MAX)
    /// See: RFC 8011 Section 5.4.36
    /// </summary>
    [IppAttribute(IppAttributeNames.PagesPerMinute, Tag.Integer)]
    public int? PagesPerMinute { get; set; }

    /// <summary>
    /// pages-per-minute-color
    /// See: RFC 8011 Section 5.4.37
    /// </summary>
    [IppAttribute(IppAttributeNames.PagesPerMinuteColor, Tag.Integer)]
    public int? PagesPerMinuteColor { get; set; }

    /// <summary>
    /// print-scaling-default
    /// See: PWG 5100.13-2023 Section 6.5.29
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintScalingDefault, Tag.Keyword)]
    public PrintScaling? PrintScalingDefault { get; set; }

    /// <summary>
    /// print-scaling-supported
    /// See: PWG 5100.13-2023 Section 6.5.30
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintScalingSupported, Tag.Keyword)]
    public PrintScaling[]? PrintScalingSupported { get; set; }

    /// <summary>
    /// media-default
    /// See: RFC 8011 Section 5.2.11
    /// </summary>
    [IppAttribute()]
    public Media? MediaDefault { get; set; }

    /// <summary>
    /// media-supported
    /// See: RFC 8011 Section 5.2.11
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaSupported)]
    public Media[]? MediaSupported { get; set; }

    /// <summary>
    /// media-ready
    /// See: RFC 2911 Section 4.2.11
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaReady)]
    public Media[]? MediaReady { get; set; }

    /// <summary>
    /// sides-default
    /// See: RFC 8011 Section 5.2.8
    /// </summary>
    [IppAttribute(IppAttributeNames.SidesDefault, Tag.Keyword)]
    public Sides? SidesDefault { get; set; }

    /// <summary>
    /// sides-supported
    /// See: RFC 8011 Section 5.2.8
    /// </summary>
    [IppAttribute(IppAttributeNames.SidesSupported, Tag.Keyword)]
    public Sides[]? SidesSupported { get; set; }

    /// <summary>
    /// finishings-default
    /// See: RFC 8011 Section 5.2.6
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingsDefault, Tag.Enum)]
    public Finishings? FinishingsDefault { get; set; }

    /// <summary>
    /// finishings-supported
    /// See: RFC 8011 Section 5.2.6
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingsSupported, Tag.Enum)]
    public Finishings[]? FinishingsSupported { get; set; }

    /// <summary>
    /// printer-resolution-default
    /// See: RFC 8011 Section 5.2.12
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterResolutionDefault, Tag.Resolution)]
    public Resolution? PrinterResolutionDefault { get; set; }

    /// <summary>
    /// printer-resolution-supported
    /// See: RFC 8011 Section 5.2.12
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterResolutionSupported, Tag.Resolution)]
    public Resolution[]? PrinterResolutionSupported { get; set; }

    /// <summary>
    /// print-quality-default
    /// See: RFC 8011 Section 5.2.13
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintQualityDefault, Tag.Enum)]
    public PrintQuality? PrintQualityDefault { get; set; }

    /// <summary>
    /// print-quality-supported
    /// See: RFC 8011 Section 5.2.13
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintQualitySupported, Tag.Enum)]
    public PrintQuality[]? PrintQualitySupported { get; set; }

    /// <summary>
    /// job-priority-default
    /// See: RFC 8011 Section 5.2.1
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPriorityDefault, Tag.Integer)]
    public int? JobPriorityDefault { get; set; }

    /// <summary>
    /// job-priority-supported
    /// See: RFC 8011 Section 5.2.1
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPrioritySupported, Tag.Integer)]
    public int? JobPrioritySupported { get; set; }

    /// <summary>
    /// copies-default
    /// See: RFC 8011 Section 5.2.5
    /// </summary>
    [IppAttribute(IppAttributeNames.CopiesDefault, Tag.Integer)]
    public int? CopiesDefault { get; set; }

    /// <summary>
    /// copies-supported
    /// See: RFC 8011 Section 5.2.5
    /// </summary>
    [IppAttribute(IppAttributeNames.CopiesSupported, Tag.RangeOfInteger)]
    public Range? CopiesSupported { get; set; }

    /// <summary>
    /// orientation-requested-default
    /// See: RFC 8011 Section 5.2.10
    /// </summary>
    [IppAttribute(IppAttributeNames.OrientationRequestedDefault, Tag.Enum)]
    public Orientation? OrientationRequestedDefault { get; set; }

    /// <summary>
    /// orientation-requested-supported
    /// See: RFC 8011 Section 5.2.10
    /// </summary>
    [IppAttribute(IppAttributeNames.OrientationRequestedSupported, Tag.Enum)]
    public Orientation[]? OrientationRequestedSupported { get; set; }

    /// <summary>
    /// page-ranges-supported
    /// See: RFC 8011 Section 5.2.7
    /// </summary>
    [IppAttribute(IppAttributeNames.PageRangesSupported, Tag.Boolean)]
    public bool? PageRangesSupported { get; set; }

    /// <summary>
    /// job-hold-until-supported
    /// See: RFC 8011 Section 5.2.2
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHoldUntilSupported, Tag.Keyword)]
    public JobHoldUntil[]? JobHoldUntilSupported { get; set; }

    /// <summary>
    /// job-hold-until-default
    /// See: RFC 8011 Section 5.2.2
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHoldUntilDefault, Tag.Keyword)]
    public JobHoldUntil? JobHoldUntilDefault { get; set; }

    /// <summary>
    /// job-hold-until-time-supported
    /// See: PWG 5100.7-2023 Section 6.9.21
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHoldUntilTimeSupported, Tag.Boolean)]
    public bool? JobHoldUntilTimeSupported { get; set; }

    /// <summary>
    /// job-delay-output-until-default
    /// See: PWG 5100.7-2023 Section 6.9.14
    /// </summary>
    [IppAttribute(IppAttributeNames.JobDelayOutputUntilDefault, Tag.Keyword)]
    public JobHoldUntil? JobDelayOutputUntilDefault { get; set; }

    /// <summary>
    /// job-delay-output-until-supported
    /// See: PWG 5100.7-2023 Section 6.9.15
    /// </summary>
    [IppAttribute(IppAttributeNames.JobDelayOutputUntilSupported, Tag.Keyword)]
    public JobHoldUntil[]? JobDelayOutputUntilSupported { get; set; }

    /// <summary>
    /// job-delay-output-until-time-supported
    /// See: PWG 5100.7-2023 Section 6.9.16
    /// </summary>
    [IppAttribute(IppAttributeNames.JobDelayOutputUntilTimeSupported, Tag.RangeOfInteger)]
    public Range? JobDelayOutputUntilTimeSupported { get; set; }

    /// <summary>
    /// job-history-attributes-configured
    /// See: PWG 5100.7-2023 Section 6.9.17
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHistoryAttributesConfigured, Tag.Keyword)]
    public JobHistoryAttribute[]? JobHistoryAttributesConfigured { get; set; }

    /// <summary>
    /// job-history-attributes-supported
    /// See: PWG 5100.7-2023 Section 6.9.18
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHistoryAttributesSupported, Tag.Keyword)]
    public JobHistoryAttribute[]? JobHistoryAttributesSupported { get; set; }

    /// <summary>
    /// job-history-interval-configured
    /// See: PWG 5100.7-2023 Section 6.9.19
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHistoryIntervalConfigured, Tag.Integer)]
    public int? JobHistoryIntervalConfigured { get; set; }

    /// <summary>
    /// job-history-interval-supported
    /// See: PWG 5100.7-2023 Section 6.9.20
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHistoryIntervalSupported, Tag.RangeOfInteger)]
    public Range? JobHistoryIntervalSupported { get; set; }

    /// <summary>
    /// job-retain-until-default
    /// See: PWG 5100.7-2023 Section 6.9.24
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRetainUntilDefault, Tag.Keyword)]
    public JobHoldUntil? JobRetainUntilDefault { get; set; }

    /// <summary>
    /// job-retain-until-interval-default
    /// See: PWG 5100.7-2023 Section 6.9.25
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRetainUntilIntervalDefault, Tag.Integer)]
    public int? JobRetainUntilIntervalDefault { get; set; }

    /// <summary>
    /// job-retain-until-interval-supported
    /// See: PWG 5100.7-2023 Section 6.9.26
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRetainUntilIntervalSupported, Tag.RangeOfInteger)]
    public Range? JobRetainUntilIntervalSupported { get; set; }

    /// <summary>
    /// job-retain-until-supported
    /// See: PWG 5100.7-2023 Section 6.9.27
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRetainUntilSupported, Tag.Keyword)]
    public JobHoldUntil[]? JobRetainUntilSupported { get; set; }

    /// <summary>
    /// job-retain-until-time-supported
    /// See: PWG 5100.7-2023 Section 6.9.28
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRetainUntilTimeSupported, Tag.Boolean)]
    public bool? JobRetainUntilTimeSupported { get; set; }

    /// <summary>
    /// output-bin-default
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    [IppAttribute()]
    public OutputBin? OutputBinDefault { get; set; }

    /// <summary>
    /// output-bin-supported
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputBinSupported)]
    public OutputBin[]? OutputBinSupported { get; set; }

    /// <summary>
    /// media-col-default
    /// See: PWG 5100.7-2023
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaColDefault)]
    public MediaCol? MediaColDefault { get; set; }

    /// <summary>
    /// media-col-database
    /// See: PWG 5100.7-2023 Section 6.9.36
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaColDatabase)]
    public MediaCol[]? MediaColDatabase { get; set; }

    /// <summary>
    /// media-col-ready
    /// See: PWG 5100.7-2023 Section 6.9.38
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaColReady)]
    public MediaCol[]? MediaColReady { get; set; }

    /// <summary>
    /// media-col-supported
    /// See: PWG 5100.7-2023 Section 6.9.39
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaColSupported, Tag.Keyword)]
    public MediaColMember[]? MediaColSupported { get; set; }

    /// <summary>
    /// media-size-supported
    /// See: PWG 5100.7-2023 Section 6.9.50
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaSizeSupported)]
    public MediaSizeSupported[]? MediaSizeSupported { get; set; }

    /// <summary>
    /// media-key-supported
    /// See: PWG 5100.7-2023 Section 6.9.44
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaKeySupported)]
    public MediaKey[]? MediaKeySupported { get; set; }

    /// <summary>
    /// media-source-supported
    /// See: PWG 5100.7-2023 Section 6.9.51
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaSourceSupported, Tag.Keyword)]
    public MediaSource[]? MediaSourceSupported { get; set; }

    /// <summary>
    /// media-type-supported
    /// See: PWG 5100.7-2023 Section 6.9.55
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaTypeSupported, Tag.Keyword)]
    public MediaType[]? MediaTypeSupported { get; set; }

    /// <summary>
    /// media-back-coating-supported
    /// See: PWG 5100.7-2023 Section 6.9.34
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaBackCoatingSupported, Tag.Keyword)]
    public MediaCoating[]? MediaBackCoatingSupported { get; set; }

    /// <summary>
    /// media-front-coating-supported
    /// See: PWG 5100.7-2023 Section 6.9.41
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaFrontCoatingSupported, Tag.Keyword)]
    public MediaCoating[]? MediaFrontCoatingSupported { get; set; }

    /// <summary>
    /// media-color-supported
    /// See: PWG 5100.7-2023 Section 6.9.40
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaColorSupported, Tag.Keyword)]
    public MediaColor[]? MediaColorSupported { get; set; }

    /// <summary>
    /// media-grain-supported
    /// See: PWG 5100.7-2023 Section 6.9.42
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaGrainSupported, Tag.Keyword)]
    public MediaGrain[]? MediaGrainSupported { get; set; }

    /// <summary>
    /// media-tooth-supported
    /// See: PWG 5100.7-2023 Section 6.9.53
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaToothSupported, Tag.Keyword)]
    public MediaTooth[]? MediaToothSupported { get; set; }

    /// <summary>
    /// media-pre-printed-supported
    /// See: PWG 5100.7-2023 Section 6.9.47
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaPrePrintedSupported, Tag.Keyword)]
    public MediaPrePrinted[]? MediaPrePrintedSupported { get; set; }

    /// <summary>
    /// media-recycled-supported
    /// See: PWG 5100.7-2023 Section 6.9.48
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaRecycledSupported, Tag.Keyword)]
    public MediaRecycled[]? MediaRecycledSupported { get; set; }

    /// <summary>
    /// media-hole-count-supported
    /// See: PWG 5100.7-2023 Section 6.9.43
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaHoleCountSupported, Tag.RangeOfInteger)]
    public Range[]? MediaHoleCountSupported { get; set; }

    /// <summary>
    /// media-order-count-supported
    /// See: PWG 5100.7-2023 Section 6.9.46
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaOrderCountSupported, Tag.RangeOfInteger)]
    public Range[]? MediaOrderCountSupported { get; set; }

    /// <summary>
    /// media-thickness-supported
    /// See: PWG 5100.7-2023 Section 6.9.52
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaThicknessSupported, Tag.RangeOfInteger)]
    public Range[]? MediaThicknessSupported { get; set; }

    /// <summary>
    /// media-weight-metric-supported
    /// See: PWG 5100.7-2023 Section 6.9.56
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaWeightMetricSupported, Tag.RangeOfInteger)]
    public Range[]? MediaWeightMetricSupported { get; set; }

    /// <summary>
    /// media-bottom-margin-supported
    /// See: PWG 5100.7-2023 Section 6.9.35
    /// </summary>
    [Range(0, int.MaxValue)]
    [IppAttribute(IppAttributeNames.MediaBottomMarginSupported, Tag.Integer)]
    public int[]? MediaBottomMarginSupported { get; set; }

    /// <summary>
    /// media-left-margin-supported
    /// See: PWG 5100.7-2023 Section 6.9.45
    /// </summary>
    [Range(0, int.MaxValue)]
    [IppAttribute(IppAttributeNames.MediaLeftMarginSupported, Tag.Integer)]
    public int[]? MediaLeftMarginSupported { get; set; }

    /// <summary>
    /// media-right-margin-supported
    /// See: PWG 5100.7-2023 Section 6.9.49
    /// </summary>
    [Range(0, int.MaxValue)]
    [IppAttribute(IppAttributeNames.MediaRightMarginSupported, Tag.Integer)]
    public int[]? MediaRightMarginSupported { get; set; }

    /// <summary>
    /// media-top-margin-supported
    /// See: PWG 5100.7-2023 Section 6.9.54
    /// </summary>
    [Range(0, int.MaxValue)]
    [IppAttribute(IppAttributeNames.MediaTopMarginSupported, Tag.Integer)]
    public int[]? MediaTopMarginSupported { get; set; }

    /// <summary>
    /// print-color-mode-default
    /// See: PWG 5100.13-2023 Section 6.5.23
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintColorModeDefault, Tag.Keyword)]
    public PrintColorMode? PrintColorModeDefault { get; set; }

    /// <summary>
    /// print-color-mode-supported
    /// See: PWG 5100.13-2023 Section 6.5.25
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintColorModeSupported, Tag.Keyword)]
    public PrintColorMode[]? PrintColorModeSupported { get; set; }

    /// <summary>
    /// which-jobs-supported
    /// See: RFC 8011 Section 4.2.6.1
    /// </summary>
    [IppAttribute(IppAttributeNames.WhichJobsSupported, Tag.Keyword)]
    public WhichJobs[]? WhichJobsSupported { get; set; }

    /// <summary>
    /// printer-uuid
    /// See: PWG 5100.13-2023 Section 6.6.14
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterUUID, Tag.Uri)]
    public string? PrinterUUID { get; set; }

    /// <summary>
    /// pdf-versions-supported
    /// See: RFC 8011 Section 5.4.38
    /// </summary>
    [IppAttribute(IppAttributeNames.PdfVersionsSupported, Tag.Keyword)]
    public PdfVersion[]? PdfVersionsSupported { get; set; }

    /// <summary>
    /// ipp-features-supported
    /// See: RFC 8011 Section 5.4.39
    /// </summary>
    [IppAttribute(IppAttributeNames.IppFeaturesSupported, Tag.Keyword)]
    public IppFeature[]? IppFeaturesSupported { get; set; }

    /// <summary>
    /// document-creation-attributes-supported
    /// See: PWG 5100.5-2024 Section 6.5.1
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentCreationAttributesSupported, Tag.Keyword)]
    public DocumentCreationAttribute[]? DocumentCreationAttributesSupported { get; set; }

    /// <summary>
    /// job-account-id-default
    /// See: PWG 5100.7-2023 Section 6.9.7
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountIdDefault, Tag.NameWithoutLanguage)]
    public string? JobAccountIdDefault { get; set; }

    /// <summary>
    /// job-account-id-supported
    /// See: PWG 5100.7-2023 Section 6.9.8
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountIdSupported, Tag.Boolean)]
    public bool? JobAccountIdSupported { get; set; }

    /// <summary>
    /// job-accounting-user-id-default
    /// See: PWG 5100.7-2023 Section 6.9.9
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountingUserIdDefault, Tag.NameWithoutLanguage)]
    public string? JobAccountingUserIdDefault { get; set; }

    /// <summary>
    /// job-accounting-user-id-supported
    /// See: PWG 5100.7-2023 Section 6.9.10
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountingUserIdSupported, Tag.Boolean)]
    public bool? JobAccountingUserIdSupported { get; set; }

    /// <summary>
    /// job-cancel-after-default
    /// See: PWG 5100.7-2023 Section 6.9.11
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCancelAfterDefault, Tag.Integer)]
    public int? JobCancelAfterDefault { get; set; }

    /// <summary>
    /// job-cancel-after-supported
    /// See: PWG 5100.7-2023 Section 6.9.12
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCancelAfterSupported, Tag.RangeOfInteger)]
    public Range? JobCancelAfterSupported { get; set; }

    /// <summary>
    /// job-spooling-supported
    /// See: PWG 5100.7-2023 Section 6.9.31
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSpoolingSupported, Tag.Keyword)]
    public JobSpooling? JobSpoolingSupported { get; set; }

    /// <summary>
    /// max-page-ranges-supported
    /// See: PWG 5100.7-2023 Section 6.9.33
    /// </summary>
    [IppAttribute(IppAttributeNames.MaxPageRangesSupported, Tag.Integer)]
    public int? MaxPageRangesSupported { get; set; }

    /// <summary>
    /// print-content-optimize-default
    /// See: PWG 5100.7-2023 Section 6.9.58
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintContentOptimizeDefault, Tag.Keyword)]
    public PrintContentOptimize? PrintContentOptimizeDefault { get; set; }

    /// <summary>
    /// print-content-optimize-supported
    /// See: PWG 5100.7-2023 Section 6.9.59
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintContentOptimizeSupported, Tag.Keyword)]
    public PrintContentOptimize[]? PrintContentOptimizeSupported { get; set; }

    /// <summary>
    /// output-device-supported
    /// See: PWG 5100.7-2023 Section 6.9.57
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceSupported, Tag.NameWithoutLanguage)]
    public OutputDevice[]? OutputDeviceSupported { get; set; }

    /// <summary>
    /// job-creation-attributes-supported
    /// See: PWG 5100.7-2023 Section 6.9.13
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCreationAttributesSupported, Tag.Keyword)]
    public JobCreationAttribute[]? JobCreationAttributesSupported { get; set; }

    /// <summary>
    /// printer-requested-client-type
    /// See: PWG 5100.7-2023 Section 6.9.60
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterRequestedClientType, Tag.Enum)]
    public ClientType[]? PrinterRequestedClientType { get; set; }

    /// <summary>
    /// printer-service-type
    /// See: PWG 5100.22-2025 Section 7.7.9
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterServiceType, Tag.Keyword)]
    public PrinterServiceType[]? PrinterServiceType { get; set; }

    /// <summary>
    /// finishing-template-supported
    /// See: PWG 5100.1-2022 Section 6.8
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingTemplateSupported)]
    public FinishingTemplate[]? FinishingTemplateSupported { get; set; }

    /// <summary>
    /// finishings-col-supported
    /// See: PWG 5100.1-2022 Section 6.12
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingsColSupported, Tag.Keyword)]
    public FinishingsColMember[]? FinishingsColSupported { get; set; }

    /// <summary>
    /// finishings-col-default
    /// See: PWG 5100.1-2022 Section 6.10
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingsColDefault)]
    public FinishingsCol[]? FinishingsColDefault { get; set; }

    /// <summary>
    /// finishings-col-ready
    /// See: PWG 5100.1-2022 Section 6.11
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingsColReady)]
    public FinishingsCol[]? FinishingsColReady { get; set; }

    /// <summary>
    /// job-pages-per-set-supported
    /// See: PWG 5100.1-2022 Section 6.18
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPagesPerSetSupported, Tag.Boolean)]
    public bool? JobPagesPerSetSupported { get; set; }

    /// <summary>
    /// punching-hole-diameter-configured
    /// See: PWG 5100.1-2022 Section 6.19
    /// </summary>
    [IppAttribute(IppAttributeNames.PunchingHoleDiameterConfigured, Tag.Integer)]
    public int? PunchingHoleDiameterConfigured { get; set; }

    /// <summary>
    /// baling-type-supported
    /// See: PWG 5100.1-2022 Section 6.1
    /// </summary>
    [IppAttribute(IppAttributeNames.BalingTypeSupported)]
    public BalingType[]? BalingTypeSupported { get; set; }

    /// <summary>
    /// baling-when-supported
    /// See: PWG 5100.1-2022 Section 6.2
    /// </summary>
    [IppAttribute(IppAttributeNames.BalingWhenSupported, Tag.Keyword)]
    public BalingWhen[]? BalingWhenSupported { get; set; }

    /// <summary>
    /// binding-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.3
    /// </summary>
    [IppAttribute(IppAttributeNames.BindingReferenceEdgeSupported, Tag.Keyword)]
    public FinishingReferenceEdge[]? BindingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// binding-type-supported
    /// See: PWG 5100.1-2022 Section 6.4
    /// </summary>
    [IppAttribute(IppAttributeNames.BindingTypeSupported)]
    public BindingType[]? BindingTypeSupported { get; set; }

    /// <summary>
    /// coating-sides-supported
    /// See: PWG 5100.1-2022 Section 6.5
    /// </summary>
    [IppAttribute(IppAttributeNames.CoatingSidesSupported, Tag.Keyword)]
    public CoatingSides[]? CoatingSidesSupported { get; set; }

    /// <summary>
    /// coating-type-supported
    /// See: PWG 5100.1-2022 Section 6.6
    /// </summary>
    [IppAttribute(IppAttributeNames.CoatingTypeSupported)]
    public CoatingType[]? CoatingTypeSupported { get; set; }

    /// <summary>
    /// covering-name-supported
    /// See: PWG 5100.1-2022 Section 6.7
    /// </summary>
    [IppAttribute(IppAttributeNames.CoveringNameSupported)]
    public CoveringName[]? CoveringNameSupported { get; set; }

    /// <summary>
    /// finishings-col-database
    /// See: PWG 5100.1-2022 Section 6.9
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingsColDatabase)]
    public FinishingsCol[]? FinishingsColDatabase { get; set; }

    /// <summary>
    /// folding-direction-supported
    /// See: PWG 5100.1-2022 Section 6.13
    /// </summary>
    [IppAttribute(IppAttributeNames.FoldingDirectionSupported, Tag.Keyword)]
    public FoldingDirection[]? FoldingDirectionSupported { get; set; }

    /// <summary>
    /// folding-offset-supported
    /// See: PWG 5100.1-2022 Section 6.14
    /// </summary>
    [IppAttribute(IppAttributeNames.FoldingOffsetSupported, Tag.RangeOfInteger)]
    public Range[]? FoldingOffsetSupported { get; set; }

    /// <summary>
    /// folding-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.15
    /// </summary>
    [IppAttribute(IppAttributeNames.FoldingReferenceEdgeSupported, Tag.Keyword)]
    public FinishingReferenceEdge[]? FoldingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// laminating-sides-supported
    /// See: PWG 5100.1-2022 Section 6.16
    /// </summary>
    [IppAttribute(IppAttributeNames.LaminatingSidesSupported, Tag.Keyword)]
    public CoatingSides[]? LaminatingSidesSupported { get; set; }

    /// <summary>
    /// laminating-type-supported
    /// See: PWG 5100.1-2022 Section 6.17
    /// </summary>
    [IppAttribute(IppAttributeNames.LaminatingTypeSupported)]
    public LaminatingType[]? LaminatingTypeSupported { get; set; }

    /// <summary>
    /// punching-locations-supported
    /// See: PWG 5100.1-2022 Section 6.20
    /// </summary>
    [IppAttribute(IppAttributeNames.PunchingLocationsSupported, Tag.RangeOfInteger)]
    public Range[]? PunchingLocationsSupported { get; set; }

    /// <summary>
    /// punching-offset-supported
    /// See: PWG 5100.1-2022 Section 6.21
    /// </summary>
    [IppAttribute(IppAttributeNames.PunchingOffsetSupported, Tag.RangeOfInteger)]
    public Range[]? PunchingOffsetSupported { get; set; }

    /// <summary>
    /// punching-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.22
    /// </summary>
    [IppAttribute(IppAttributeNames.PunchingReferenceEdgeSupported, Tag.Keyword)]
    public FinishingReferenceEdge[]? PunchingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// stitching-angle-supported
    /// See: PWG 5100.1-2022 Section 6.23
    /// </summary>
    [IppAttribute(IppAttributeNames.StitchingAngleSupported, Tag.RangeOfInteger)]
    public Range[]? StitchingAngleSupported { get; set; }

    /// <summary>
    /// stitching-locations-supported
    /// See: PWG 5100.1-2022 Section 6.24
    /// </summary>
    [IppAttribute(IppAttributeNames.StitchingLocationsSupported, Tag.RangeOfInteger)]
    public Range[]? StitchingLocationsSupported { get; set; }

    /// <summary>
    /// stitching-method-supported
    /// See: PWG 5100.1-2022 Section 6.25
    /// </summary>
    [IppAttribute(IppAttributeNames.StitchingMethodSupported, Tag.Keyword)]
    public StitchingMethod[]? StitchingMethodSupported { get; set; }

    /// <summary>
    /// stitching-offset-supported
    /// See: PWG 5100.1-2022 Section 6.26
    /// </summary>
    [IppAttribute(IppAttributeNames.StitchingOffsetSupported, Tag.RangeOfInteger)]
    public Range[]? StitchingOffsetSupported { get; set; }

    /// <summary>
    /// stitching-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.27
    /// </summary>
    [IppAttribute(IppAttributeNames.StitchingReferenceEdgeSupported, Tag.Keyword)]
    public FinishingReferenceEdge[]? StitchingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// trimming-offset-supported
    /// See: PWG 5100.1-2022 Section 6.28
    /// </summary>
    [IppAttribute(IppAttributeNames.TrimmingOffsetSupported, Tag.RangeOfInteger)]
    public Range[]? TrimmingOffsetSupported { get; set; }

    /// <summary>
    /// trimming-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.29
    /// </summary>
    [IppAttribute(IppAttributeNames.TrimmingReferenceEdgeSupported, Tag.Keyword)]
    public FinishingReferenceEdge[]? TrimmingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// trimming-type-supported
    /// See: PWG 5100.1-2022 Section 6.30
    /// </summary>
    [IppAttribute(IppAttributeNames.TrimmingTypeSupported)]
    public TrimmingType[]? TrimmingTypeSupported { get; set; }

    /// <summary>
    /// trimming-when-supported
    /// See: PWG 5100.1-2022 Section 6.31
    /// </summary>
    [IppAttribute(IppAttributeNames.TrimmingWhenSupported, Tag.Keyword)]
    public TrimmingWhen[]? TrimmingWhenSupported { get; set; }

    /// <summary>
    /// printer-finisher
    /// See: PWG 5100.1-2022 Section 7.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFinisher, Tag.OctetStringWithAnUnspecifiedFormat)]
    public PrinterFinisher[]? PrinterFinisher { get; set; }

    /// <summary>
    /// printer-finisher-description
    /// See: PWG 5100.1-2022 Section 7.2
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFinisherDescription, Tag.TextWithoutLanguage)]
    public string[]? PrinterFinisherDescription { get; set; }

    /// <summary>
    /// printer-finisher-supplies
    /// See: PWG 5100.1-2022 Section 7.3
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFinisherSupplies, Tag.OctetStringWithAnUnspecifiedFormat)]
    public PrinterFinisherSupply[]? PrinterFinisherSupplies { get; set; }

    /// <summary>
    /// printer-finisher-supplies-description
    /// See: PWG 5100.1-2022 Section 7.4
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFinisherSuppliesDescription, Tag.TextWithoutLanguage)]
    public string[]? PrinterFinisherSuppliesDescription { get; set; }

    /// <summary>
    /// cover-back-default
    /// See: PWG 5100.3-2023 Section 5.3.1
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverBackDefault)]
    public Cover? CoverBackDefault { get; set; }
    /// <summary>
    /// cover-back-supported
    /// See: PWG 5100.3-2023 Section 5.3.2
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverBackSupported, Tag.Keyword)]
    public CoverMember[]? CoverBackSupported { get; set; }

    /// <summary>
    /// cover-front-default
    /// See: PWG 5100.3-2023 Section 5.3.3
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverFrontDefault)]
    public Cover? CoverFrontDefault { get; set; }

    /// <summary>
    /// cover-front-supported
    /// See: PWG 5100.3-2023 Section 5.3.4
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverFrontSupported, Tag.Keyword)]
    public CoverMember[]? CoverFrontSupported { get; set; }

    /// <summary>
    /// cover-type-supported
    /// See: PWG 5100.3-2023 Section 5.3.5
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverTypeSupported, Tag.Keyword)]
    public CoverType[]? CoverTypeSupported { get; set; }

    /// <summary>
    /// force-front-side-supported
    /// See: PWG 5100.3-2023 Section 5.3.6
    /// </summary>
    [IppAttribute(IppAttributeNames.ForceFrontSideSupported, Tag.RangeOfInteger)]
    public Range? ForceFrontSideSupported { get; set; }

    /// <summary>
    /// image-orientation-default
    /// See: PWG 5100.3-2023 Section 5.3.7
    /// </summary>
    [IppAttribute(IppAttributeNames.ImageOrientationDefault, Tag.Enum)]
    public Orientation? ImageOrientationDefault { get; set; }

    /// <summary>
    /// image-orientation-supported
    /// See: PWG 5100.3-2023 Section 5.3.8
    /// </summary>
    [IppAttribute(IppAttributeNames.ImageOrientationSupported, Tag.Enum)]
    public Orientation[]? ImageOrientationSupported { get; set; }

    /// <summary>
    /// imposition-template-default
    /// See: PWG 5100.3-2023 Section 5.3.9
    /// </summary>
    [IppAttribute()]
    public ImpositionTemplate? ImpositionTemplateDefault { get; set; }

    /// <summary>
    /// imposition-template-supported
    /// See: PWG 5100.3-2023 Section 5.3.10
    /// </summary>
    [IppAttribute(IppAttributeNames.ImpositionTemplateSupported)]
    public ImpositionTemplate[]? ImpositionTemplateSupported { get; set; }

    /// <summary>
    /// insert-count-supported
    /// See: PWG 5100.3-2023 Section 5.3.11
    /// </summary>
    [IppAttribute(IppAttributeNames.InsertCountSupported, Tag.RangeOfInteger)]
    public Range? InsertCountSupported { get; set; }

    /// <summary>
    /// insert-sheet-default
    /// See: PWG 5100.3-2023 Section 5.3.12
    /// </summary>
    [IppAttribute(IppAttributeNames.InsertSheetDefault)]
    public InsertSheet[]? InsertSheetDefault { get; set; }

    /// <summary>
    /// insert-sheet-supported
    /// See: PWG 5100.3-2023 Section 5.3.13
    /// </summary>
    [IppAttribute(IppAttributeNames.InsertSheetSupported, Tag.Keyword)]
    public InsertSheetMember[]? InsertSheetSupported { get; set; }

    /// <summary>
    /// job-accounting-output-bin-supported
    /// See: PWG 5100.3-2023 Section 5.3.14
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountingOutputBinSupported)]
    public OutputBin[]? JobAccountingOutputBinSupported { get; set; }

    /// <summary>
    /// job-accounting-sheets-default
    /// See: PWG 5100.3-2023 Section 5.3.15
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountingSheetsDefault)]
    public JobAccountingSheets? JobAccountingSheetsDefault { get; set; }

    /// <summary>
    /// job-accounting-sheets-supported
    /// See: PWG 5100.3-2023 Section 5.3.16
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountingSheetsSupported, Tag.Keyword)]
    public JobAccountingSheetsMember[]? JobAccountingSheetsSupported { get; set; }

    /// <summary>
    /// job-accounting-sheets-type-supported
    /// See: PWG 5100.3-2023 Section 5.3.17
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountingSheetsTypeSupported, Tag.Keyword)]
    public JobAccountingSheetsType[]? JobAccountingSheetsTypeSupported { get; set; }

    /// <summary>
    /// job-complete-before-supported
    /// See: PWG 5100.3-2023 Section 5.3.18
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCompleteBeforeSupported, Tag.Keyword)]
    public JobCompleteBefore[]? JobCompleteBeforeSupported { get; set; }

    /// <summary>
    /// job-complete-before-time-supported
    /// See: PWG 5100.3-2023 Section 5.3.19
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCompleteBeforeTimeSupported, Tag.Boolean)]
    public bool? JobCompleteBeforeTimeSupported { get; set; }

    /// <summary>
    /// job-error-sheet-default
    /// See: PWG 5100.3-2023 Section 5.3.20
    /// </summary>
    [IppAttribute(IppAttributeNames.JobErrorSheetDefault)]
    public JobErrorSheet? JobErrorSheetDefault { get; set; }

    /// <summary>
    /// job-error-sheet-supported
    /// See: PWG 5100.3-2023 Section 5.3.21
    /// </summary>
    [IppAttribute(IppAttributeNames.JobErrorSheetSupported, Tag.Keyword)]
    public JobErrorSheetMember[]? JobErrorSheetSupported { get; set; }

    /// <summary>
    /// job-error-sheet-type-supported
    /// See: PWG 5100.3-2023 Section 5.3.22
    /// </summary>
    [IppAttribute(IppAttributeNames.JobErrorSheetTypeSupported, Tag.Keyword)]
    public JobErrorSheetType[]? JobErrorSheetTypeSupported { get; set; }

    /// <summary>
    /// job-error-sheet-when-supported
    /// See: PWG 5100.3-2023 Section 5.3.23
    /// </summary>
    [IppAttribute(IppAttributeNames.JobErrorSheetWhenSupported, Tag.Keyword)]
    public JobErrorSheetWhen[]? JobErrorSheetWhenSupported { get; set; }

    /// <summary>
    /// job-message-to-operator-supported
    /// See: PWG 5100.3-2023 Section 5.3.24
    /// </summary>
    [IppAttribute(IppAttributeNames.JobMessageToOperatorSupported, Tag.Boolean)]
    public bool? JobMessageToOperatorSupported { get; set; }

    /// <summary>
    /// job-phone-number-default
    /// See: PWG 5100.3-2023 Section 5.3.25
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPhoneNumberDefault, Tag.Keyword)]
    public string? JobPhoneNumberDefault { get; set; }

    /// <summary>
    /// job-phone-number-scheme-supported
    /// See: PWG 5100.3-2023 Section 5.3.26
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPhoneNumberSchemeSupported, Tag.Keyword)]
    public JobPhoneNumberScheme[]? JobPhoneNumberSchemeSupported { get; set; }

    /// <summary>
    /// job-phone-number-supported
    /// See: PWG 5100.3-2023 Section 5.3.27
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPhoneNumberSupported, Tag.Boolean)]
    public bool? JobPhoneNumberSupported { get; set; }

    /// <summary>
    /// job-recipient-name-supported
    /// See: PWG 5100.3-2023 Section 5.3.28
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRecipientNameSupported, Tag.Boolean)]
    public bool? JobRecipientNameSupported { get; set; }

    /// <summary>
    /// job-sheet-message-supported
    /// See: PWG 5100.3-2023
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSheetMessageSupported, Tag.Boolean)]
    public bool? JobSheetMessageSupported { get; set; }

    /// <summary>
    /// page-delivery-default
    /// See: PWG 5100.3-2023 Section 4.2 / 11.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PageDeliveryDefault, Tag.Keyword)]
    public PageDelivery? PageDeliveryDefault { get; set; }

    /// <summary>
    /// page-delivery-supported
    /// See: PWG 5100.3-2023 Section 4.2 / 11.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PageDeliverySupported, Tag.Keyword)]
    public PageDelivery[]? PageDeliverySupported { get; set; }

    /// <summary>
    /// presentation-direction-number-up-default
    /// See: PWG 5100.3-2023 Section 5.3.29
    /// </summary>
    [IppAttribute(IppAttributeNames.PresentationDirectionNumberUpDefault, Tag.Keyword)]
    public PresentationDirectionNumberUp? PresentationDirectionNumberUpDefault { get; set; }

    /// <summary>
    /// presentation-direction-number-up-supported
    /// See: PWG 5100.3-2023 Section 5.3.30
    /// </summary>
    [IppAttribute(IppAttributeNames.PresentationDirectionNumberUpSupported, Tag.Keyword)]
    public PresentationDirectionNumberUp[]? PresentationDirectionNumberUpSupported { get; set; }

    /// <summary>
    /// separator-sheets-default
    /// See: PWG 5100.3-2023 Section 5.3.31
    /// </summary>
    [IppAttribute(IppAttributeNames.SeparatorSheetsDefault)]
    public SeparatorSheets? SeparatorSheetsDefault { get; set; }

    /// <summary>
    /// separator-sheets-supported
    /// See: PWG 5100.3-2023 Section 5.3.32
    /// </summary>
    [IppAttribute(IppAttributeNames.SeparatorSheetsSupported, Tag.Keyword)]
    public SeparatorSheetsMember[]? SeparatorSheetsSupported { get; set; }

    /// <summary>
    /// separator-sheets-type-supported
    /// See: PWG 5100.3-2023 Section 5.3.33
    /// </summary>
    [IppAttribute(IppAttributeNames.SeparatorSheetsTypeSupported, Tag.Keyword)]
    public SeparatorSheetsType[]? SeparatorSheetsTypeSupported { get; set; }

    /// <summary>
    /// x-image-position-default
    /// See: PWG 5100.3-2023 Section 5.3.34
    /// </summary>
    [IppAttribute(IppAttributeNames.XImagePositionDefault, Tag.Keyword)]
    public XImagePosition? XImagePositionDefault { get; set; }

    /// <summary>
    /// x-image-position-supported
    /// See: PWG 5100.3-2023 Section 5.3.35
    /// </summary>
    [IppAttribute(IppAttributeNames.XImagePositionSupported, Tag.Keyword)]
    public XImagePosition[]? XImagePositionSupported { get; set; }

    /// <summary>
    /// x-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.36
    /// </summary>
    [IppAttribute(IppAttributeNames.XImageShiftDefault, Tag.Integer)]
    public int? XImageShiftDefault { get; set; }

    /// <summary>
    /// x-image-shift-supported
    /// See: PWG 5100.3-2023 Section 5.3.37
    /// </summary>
    [IppAttribute(IppAttributeNames.XImageShiftSupported, Tag.RangeOfInteger)]
    public Range? XImageShiftSupported { get; set; }

    /// <summary>
    /// x-side1-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.38
    /// </summary>
    [IppAttribute(IppAttributeNames.XSide1ImageShiftDefault, Tag.Integer)]
    public int? XSide1ImageShiftDefault { get; set; }

    /// <summary>
    /// x-side2-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.39
    /// </summary>
    [IppAttribute(IppAttributeNames.XSide2ImageShiftDefault, Tag.Integer)]
    public int? XSide2ImageShiftDefault { get; set; }

    /// <summary>
    /// y-image-position-default
    /// See: PWG 5100.3-2023 Section 5.3.40
    /// </summary>
    [IppAttribute(IppAttributeNames.YImagePositionDefault, Tag.Keyword)]
    public YImagePosition? YImagePositionDefault { get; set; }

    /// <summary>
    /// y-image-position-supported
    /// See: PWG 5100.3-2023 Section 5.3.41
    /// </summary>
    [IppAttribute(IppAttributeNames.YImagePositionSupported, Tag.Keyword)]
    public YImagePosition[]? YImagePositionSupported { get; set; }

    /// <summary>
    /// y-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.42
    /// </summary>
    [IppAttribute(IppAttributeNames.YImageShiftDefault, Tag.Integer)]
    public int? YImageShiftDefault { get; set; }

    /// <summary>
    /// y-image-shift-supported
    /// See: PWG 5100.3-2023 Section 5.3.43
    /// </summary>
    [IppAttribute(IppAttributeNames.YImageShiftSupported, Tag.RangeOfInteger)]
    public Range? YImageShiftSupported { get; set; }

    /// <summary>
    /// y-side1-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.44
    /// </summary>
    [IppAttribute(IppAttributeNames.YSide1ImageShiftDefault, Tag.Integer)]
    public int? YSide1ImageShiftDefault { get; set; }

    /// <summary>
    /// y-side2-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.45
    /// </summary>
    [IppAttribute(IppAttributeNames.YSide2ImageShiftDefault, Tag.Integer)]
    public int? YSide2ImageShiftDefault { get; set; }

    /// <summary>
    /// job-account-type-default
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountTypeDefault, Tag.Keyword)]
    public JobAccountType? JobAccountTypeDefault { get; set; }

    /// <summary>
    /// job-account-type-supported
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountTypeSupported, Tag.Keyword)]
    public JobAccountType[]? JobAccountTypeSupported { get; set; }

    /// <summary>
    /// job-password-encryption-supported
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPasswordEncryptionSupported, Tag.Keyword)]
    public JobPasswordEncryption[]? JobPasswordEncryptionSupported { get; set; }

    /// <summary>
    /// job-authorization-uri-supported
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAuthorizationUriSupported, Tag.Boolean)]
    public bool? JobAuthorizationUriSupported { get; set; }

    /// <summary>
    /// printer-charge-info
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterChargeInfo, Tag.TextWithoutLanguage)]
    public string? PrinterChargeInfo { get; set; }

    /// <summary>
    /// printer-charge-info-uri
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterChargeInfoUri, Tag.Uri)]
    public Uri? PrinterChargeInfoUri { get; set; }

    /// <summary>
    /// printer-mandatory-job-attributes
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMandatoryJobAttributes, Tag.Keyword)]
    public PrinterMandatoryJobAttribute[]? PrinterMandatoryJobAttributes { get; set; }

    /// <summary>
    /// printer-requested-job-attributes
    /// See: PWG 5100.16-2020
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterRequestedJobAttributes, Tag.Keyword)]
    public PrinterRequestedJobAttribute[]? PrinterRequestedJobAttributes { get; set; }

    /// <summary>
    /// Structured parser model for printer-alert values.
    /// See: PWG 5100.9-2009 Section 5.2.2
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterAlert, Tag.OctetStringWithAnUnspecifiedFormat)]
    public PrinterAlert[]? PrinterAlert { get; set; }

    /// <summary>
    /// printer-alert-description
    /// See: PWG 5100.9-2009 Section 5.3
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterAlertDescription, Tag.TextWithoutLanguage)]
    public string[]? PrinterAlertDescription { get; set; }

    /// <summary>
    /// printer-supply
    /// See: PWG 5100.13-2023 Section 6.6.11
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterSupply)]
    public PrinterSupply[]? PrinterSupply { get; set; }

    /// <summary>
    /// printer-input-tray
    /// See: PWG 5100.13-2023 Section 6.6.9
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterInputTray)]
    public PrinterInputTray[]? PrinterInputTray { get; set; }

    /// <summary>
    /// printer-output-tray
    /// See: PWG 5100.13-2023 Section 6.6.10
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterOutputTray)]
    public PrinterOutputTray[]? PrinterOutputTray { get; set; }

    /// <summary>
    /// job-constraints-supported
    /// See: PWG 5100.13-2023 Section 6.5.5
    /// </summary>
    [IppAttribute(IppAttributeNames.JobConstraintsSupported)]
    public JobConstraintsSupported[]? JobConstraintsSupported { get; set; }

    /// <summary>
    /// job-presets-supported
    /// See: PWG 5100.13-2023 Section 6.5.8
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPresetsSupported)]
    public JobPresetsSupported[]? JobPresetsSupported { get; set; }

    /// <summary>
    /// job-resolvers-supported
    /// See: PWG 5100.13-2023 Section 6.5.9
    /// </summary>
    [IppAttribute(IppAttributeNames.JobResolversSupported)]
    public JobResolversSupported[]? JobResolversSupported { get; set; }

    /// <summary>
    /// job-triggers-supported
    /// See: PWG 5100.13-2023 Section 6.5.10
    /// </summary>
    [IppAttribute(IppAttributeNames.JobTriggersSupported)]
    public JobTriggersSupported[]? JobTriggersSupported { get; set; }

    /// <summary>
    /// print-color-mode-icc-profiles
    /// See: PWG 5100.13-2023 Section 6.5.24
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintColorModeIccProfiles)]
    public PrintColorModeIccProfile[]? PrintColorModeIccProfile { get; set; }

    /// <summary>
    /// printer-icc-profiles
    /// See: PWG 5100.13-2023 Section 6.5.34
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterIccProfiles)]
    public PrinterIccProfile[]? PrinterIccProfile { get; set; }

    /// <summary>
    /// printer-supply-description
    /// See: RFC 8011 Section 5.4.44
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterSupplyDescription, Tag.TextWithoutLanguage)]
    public string[]? PrinterSupplyDescription { get; set; }

    /// <summary>
    /// output-device-uuid-supported
    /// See: PWG 5100.18-2025
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceUuidSupported, Tag.Uri)]
    public string[]? OutputDeviceUuidSupported { get; set; }

    /// <summary>
    /// document-access-supported
    /// See: PWG 5100.18-2025 Section 7.4.1
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentAccessSupported, Tag.Keyword)]
    public DocumentAccessMember[]? DocumentAccessSupported { get; set; }

    /// <summary>
    /// fetch-document-attributes-supported
    /// See: PWG 5100.18-2025 Section 7.4.2
    /// </summary>
    [IppAttribute(IppAttributeNames.FetchDocumentAttributesSupported, Tag.Keyword)]
    public FetchDocumentAttribute[]? FetchDocumentAttributesSupported { get; set; }

    /// <summary>
    /// printer-mode-configured
    /// See: PWG 5100.18-2025 Section 7.4.4
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterModeConfigured, Tag.Keyword)]
    public PrinterMode? PrinterModeConfigured { get; set; }

    /// <summary>
    /// printer-mode-supported
    /// See: PWG 5100.18-2025 Section 7.4.5
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterModeSupported, Tag.Keyword)]
    public PrinterMode[]? PrinterModeSupported { get; set; }

    /// <summary>
    /// printer-static-resource-directory-uri
    /// See: PWG 5100.18-2025 Section 7.4.6
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStaticResourceDirectoryUri, Tag.Uri)]
    public Uri? PrinterStaticResourceDirectoryUri { get; set; }

    /// <summary>
    /// printer-static-resource-k-octets-supported
    /// See: PWG 5100.18-2025 Section 7.4.7
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStaticResourceKOctetsSupported, Tag.Integer)]
    public int? PrinterStaticResourceKOctetsSupported { get; set; }

    /// <summary>
    /// printer-static-resource-k-octets-free
    /// See: PWG 5100.18-2025 Section 7.5.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStaticResourceKOctetsFree, Tag.Integer)]
    public int? PrinterStaticResourceKOctetsFree { get; set; }

    /// <summary>
    /// accuracy-units-supported
    /// See: PWG 5100.21-2019 Section 8.1
    /// </summary>
    [IppAttribute(IppAttributeNames.AccuracyUnitsSupported, Tag.Keyword)]
    public AccuracyUnits[]? AccuracyUnitsSupported { get; set; }

    /// <summary>
    /// chamber-humidity-default
    /// Type: integer(0:100)
    /// See: PWG 5100.21-2019 Section 8.3.2
    /// </summary>
    [IppAttribute(IppAttributeNames.ChamberHumidityDefault, Tag.Integer)]
    public int? ChamberHumidityDefault { get; set; }

    /// <summary>
    /// chamber-humidity-supported
    /// See: PWG 5100.21-2019 Section 8.3.3
    /// </summary>
    [IppAttribute(IppAttributeNames.ChamberHumiditySupported, Tag.Boolean)]
    public bool? ChamberHumiditySupported { get; set; }

    /// <summary>
    /// chamber-temperature-default
    /// Type: integer(-273:MAX)
    /// See: PWG 5100.21-2019 Section 8.3.4
    /// </summary>
    [IppAttribute(IppAttributeNames.ChamberTemperatureDefault, Tag.Integer)]
    public int? ChamberTemperatureDefault { get; set; }

    /// <summary>
    /// chamber-temperature-supported
    /// See: PWG 5100.21-2019 Section 8.3.5
    /// </summary>
    [IppAttribute(IppAttributeNames.ChamberTemperatureSupported, Tag.RangeOfInteger)]
    public Range[]? ChamberTemperatureSupported { get; set; }

    /// <summary>
    /// material-amount-units-supported
    /// See: PWG 5100.21-2019 Section 8.3.6
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialAmountUnitsSupported, Tag.Keyword)]
    public MaterialAmountUnits[]? MaterialAmountUnitsSupported { get; set; }

    /// <summary>
    /// material-diameter-supported
    /// See: PWG 5100.21-2019 Section 8.3.7
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialDiameterSupported, Tag.RangeOfInteger)]
    public Range[]? MaterialDiameterSupported { get; set; }

    /// <summary>
    /// material-nozzle-diameter-supported
    /// See: PWG 5100.21-2019 Section 8.3.8
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialNozzleDiameterSupported, Tag.RangeOfInteger)]
    public Range[]? MaterialNozzleDiameterSupported { get; set; }

    /// <summary>
    /// material-purpose-supported
    /// See: PWG 5100.21-2019 Section 8.3.9
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialPurposeSupported, Tag.Keyword)]
    public MaterialPurpose[]? MaterialPurposeSupported { get; set; }

    /// <summary>
    /// material-rate-supported
    /// See: PWG 5100.21-2019 Section 8.3.10
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialRateSupported, Tag.RangeOfInteger)]
    public Range[]? MaterialRateSupported { get; set; }

    /// <summary>
    /// material-rate-units-supported
    /// See: PWG 5100.21-2019 Section 8.3.11
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialRateUnitsSupported, Tag.Keyword)]
    public MaterialRateUnits[]? MaterialRateUnitsSupported { get; set; }

    /// <summary>
    /// material-shell-thickness-supported
    /// See: PWG 5100.21-2019 Section 8.3.12
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialShellThicknessSupported, Tag.RangeOfInteger)]
    public Range[]? MaterialShellThicknessSupported { get; set; }

    /// <summary>
    /// material-temperature-supported
    /// See: PWG 5100.21-2019 Section 8.3.13
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialTemperatureSupported, Tag.RangeOfInteger)]
    public Range[]? MaterialTemperatureSupported { get; set; }

    /// <summary>
    /// material-type-supported
    /// See: PWG 5100.21-2019 Section 8.3.14
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialTypeSupported, Tag.Keyword)]
    public MaterialType[]? MaterialTypeSupported { get; set; }

    /// <summary>
    /// materials-col-database
    /// See: PWG 5100.21-2019 Section 8.3.15
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialsColDatabase)]
    public Material[]? MaterialsColDatabase { get; set; }

    /// <summary>
    /// materials-col-default
    /// See: PWG 5100.21-2019 Section 8.3.16
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialsColDefault)]
    public Material[]? MaterialsColDefault { get; set; }

    /// <summary>
    /// materials-col-ready
    /// See: PWG 5100.21-2019 Section 8.3.17
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialsColReady)]
    public Material[]? MaterialsColReady { get; set; }

    /// <summary>
    /// materials-col-supported
    /// See: PWG 5100.21-2019 Section 8.3.18
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialsColSupported, Tag.Keyword)]
    public MaterialsColMember[]? MaterialsColSupported { get; set; }

    /// <summary>
    /// max-materials-col-supported
    /// See: PWG 5100.21-2019 Section 8.3.19
    /// </summary>
    [IppAttribute(IppAttributeNames.MaxMaterialsColSupported, Tag.Integer)]
    public int? MaxMaterialsColSupported { get; set; }

    /// <summary>
    /// multiple-object-handling-default
    /// See: PWG 5100.21-2019 Section 8.3.20
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleObjectHandlingDefault, Tag.Keyword)]
    public MultipleObjectHandling? MultipleObjectHandlingDefault { get; set; }

    /// <summary>
    /// multiple-object-handling-supported
    /// See: PWG 5100.21-2019 Section 8.3.21
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleObjectHandlingSupported, Tag.Keyword)]
    public MultipleObjectHandling[]? MultipleObjectHandlingSupported { get; set; }

    /// <summary>
    /// pdf-features-supported
    /// See: PWG 5100.21-2019 Section 8.3.22
    /// </summary>
    [IppAttribute(IppAttributeNames.PdfFeaturesSupported, Tag.Keyword)]
    public PdfFeature[]? PdfFeaturesSupported { get; set; }

    /// <summary>
    /// platform-shape
    /// See: PWG 5100.21-2019 Section 8.3.23
    /// </summary>
    [IppAttribute(IppAttributeNames.PlatformShape, Tag.Keyword)]
    public PlatformShape? PlatformShape { get; set; }

    /// <summary>
    /// repertoire-supported
    /// See: PWG 5101.2-2004 Section 8
    /// </summary>
    [IppAttribute(IppAttributeNames.RepertoireSupported)]
    public Repertoire[]? RepertoireSupported { get; set; }

    /// <summary>
    /// pwg-raster-document-resolution-supported
    /// See: PWG 5102.4-2012 Section 10.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PwgRasterDocumentResolutionSupported, Tag.Resolution)]
    public Resolution[]? PwgRasterDocumentResolutionSupported { get; set; }

    /// <summary>
    /// pwg-raster-document-sheet-back
    /// See: PWG 5102.4-2012 Section 10.2
    /// </summary>
    [IppAttribute(IppAttributeNames.PwgRasterDocumentSheetBack, Tag.Keyword)]
    public PwgRasterDocumentSheetBack? PwgRasterDocumentSheetBack { get; set; }

    /// <summary>
    /// pwg-raster-document-type-supported
    /// See: PWG 5102.4-2012 Section 10.3
    /// </summary>
    [IppAttribute(IppAttributeNames.PwgRasterDocumentTypeSupported, Tag.Keyword)]
    public string[]? PwgRasterDocumentTypeSupported { get; set; }

    /// <summary>
    /// printer-device-id
    /// See: PWG 5107.2-2010 Section 5.2
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterDeviceId, Tag.TextWithoutLanguage)]
    public string? PrinterDeviceId { get; set; }

    /// <summary>
    /// platform-temperature-default
    /// Type: integer(-273:MAX)
    /// See: PWG 5100.21-2019 Section 8.3.24
    /// </summary>
    [IppAttribute(IppAttributeNames.PlatformTemperatureDefault, Tag.Integer)]
    public int? PlatformTemperatureDefault { get; set; }

    /// <summary>
    /// platform-temperature-supported
    /// See: PWG 5100.21-2019 Section 8.3.25
    /// </summary>
    [IppAttribute(IppAttributeNames.PlatformTemperatureSupported, Tag.RangeOfInteger)]
    public Range[]? PlatformTemperatureSupported { get; set; }

    /// <summary>
    /// print-accuracy-default
    /// See: PWG 5100.21-2019 Section 8.3.26
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintAccuracyDefault)]
    public PrintAccuracy? PrintAccuracyDefault { get; set; }

    /// <summary>
    /// print-accuracy-supported
    /// See: PWG 5100.21-2019 Section 8.3.27
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintAccuracySupported)]
    public PrintAccuracy? PrintAccuracySupported { get; set; }

    /// <summary>
    /// print-base-default
    /// See: PWG 5100.21-2019 Section 8.3.28
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintBaseDefault, Tag.Keyword)]
    public PrintBase? PrintBaseDefault { get; set; }

    /// <summary>
    /// print-base-supported
    /// See: PWG 5100.21-2019 Section 8.3.29
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintBaseSupported, Tag.Keyword)]
    public PrintBase[]? PrintBaseSupported { get; set; }

    /// <summary>
    /// print-objects-supported
    /// See: PWG 5100.21-2019 Section 8.3.30
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintObjectsSupported, Tag.Keyword)]
    public PrintObjectsMember[]? PrintObjectsSupported { get; set; }

    /// <summary>
    /// print-supports-default
    /// See: PWG 5100.21-2019 Section 8.3.31
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintSupportsDefault, Tag.Keyword)]
    public PrintSupports? PrintSupportsDefault { get; set; }

    /// <summary>
    /// print-supports-supported
    /// See: PWG 5100.21-2019 Section 8.3.32
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintSupportsSupported, Tag.Keyword)]
    public PrintSupports[]? PrintSupportsSupported { get; set; }

    /// <summary>
    /// printer-volume-supported
    /// See: PWG 5100.21-2019 Section 8.3.33
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterVolumeSupported)]
    public PrinterVolumeSupported? PrinterVolumeSupported { get; set; }

    /// <summary>
    /// chamber-humidity-current
    /// Type: integer(0:100)
    /// See: PWG 5100.21-2019 Section 8.4.1
    /// </summary>
    [IppAttribute(IppAttributeNames.ChamberHumidityCurrent, Tag.Integer)]
    public int? ChamberHumidityCurrent { get; set; }

    /// <summary>
    /// chamber-temperature-current
    /// Type: integer(-273:MAX)
    /// See: PWG 5100.21-2019 Section 8.4.2
    /// </summary>
    [IppAttribute(IppAttributeNames.ChamberTemperatureCurrent, Tag.Integer)]
    public int? ChamberTemperatureCurrent { get; set; }

    /// <summary>
    /// printer-camera-image-uri
    /// See: PWG 5100.21-2019 Section 8.17
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterCameraImageUri, Tag.Uri)]
    public Uri[]? PrinterCameraImageUri { get; set; }

    /// <summary>
    /// printer-resource-ids
    /// See: PWG 5100.22-2025 Section 6.1.2
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.PrinterResourceIds, Tag.Integer)]
    public int[]? PrinterResourceIds { get; set; }

    /// <summary>
    /// confirmation-sheet-print-default
    /// See: PWG 5100.15-2013 Section 7.4.1
    /// </summary>
    [IppAttribute(IppAttributeNames.ConfirmationSheetPrintDefault, Tag.Boolean)]
    public bool? ConfirmationSheetPrintDefault { get; set; }

    /// <summary>
    /// cover-sheet-info-default
    /// See: PWG 5100.15-2013 Section 7.4.2
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverSheetInfoDefault)]
    public CoverSheetInfo? CoverSheetInfoDefault { get; set; }

    /// <summary>
    /// cover-sheet-info-supported
    /// See: PWG 5100.15-2013 Section 7.4.3
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverSheetInfoSupported, Tag.Keyword)]
    public CoverSheetInfoMember[]? CoverSheetInfoSupported { get; set; }

    /// <summary>
    /// destination-accesses-supported
    /// See: PWG 5100.17-2014 Section 8.3.1
    /// </summary>
    [IppAttribute(IppAttributeNames.DestinationAccessesSupported, Tag.Keyword)]
    public DestinationAccessMember[]? DestinationAccessesSupported { get; set; }

    /// <summary>
    /// destination-uri-ready
    /// See: PWG 5100.17-2014 Section 8.3.3
    /// </summary>
    [IppAttribute(IppAttributeNames.DestinationUriReady)]
    public DestinationUriReady[]? DestinationUriReady { get; set; }

    /// <summary>
    /// destination-uri-schemes-supported
    /// See: PWG 5100.15-2013 Section 7.4.4
    /// </summary>
    [IppAttribute(IppAttributeNames.DestinationUriSchemesSupported, Tag.UriScheme)]
    public UriScheme[]? DestinationUriSchemesSupported { get; set; }

    /// <summary>
    /// destination-uris-supported
    /// See: PWG 5100.15-2013 Section 7.4.5
    /// </summary>
    [IppAttribute(IppAttributeNames.DestinationUrisSupported, Tag.Keyword)]
    public DestinationUrisMember[]? DestinationUrisSupported { get; set; }

    /// <summary>
    /// from-name-supported
    /// See: PWG 5100.15-2013 Section 7.4.6
    /// </summary>
    [IppAttribute(IppAttributeNames.FromNameSupported, Tag.Integer)]
    public int? FromNameSupported { get; set; }

    /// <summary>
    /// input-attributes-default
    /// See: PWG 5100.15-2013 Section 7.4.7
    /// </summary>
    [IppAttribute(IppAttributeNames.InputAttributesDefault)]
    public DocumentTemplateAttributes? InputAttributesDefault { get; set; }

    /// <summary>
    /// input-attributes-supported
    /// See: PWG 5100.15-2013 Section 7.4.8
    /// </summary>
    [IppAttribute(IppAttributeNames.InputAttributesSupported, Tag.Keyword)]
    public InputAttributesMember[]? InputAttributesSupported { get; set; }

    /// <summary>
    /// input-color-mode-supported
    /// See: PWG 5100.15-2013 Section 7.4.9
    /// </summary>
    [IppAttribute(IppAttributeNames.InputColorModeSupported, Tag.Keyword)]
    public InputColorMode[]? InputColorModeSupported { get; set; }

    /// <summary>
    /// input-content-type-supported
    /// See: PWG 5100.15-2013 Section 7.4.10
    /// </summary>
    [IppAttribute(IppAttributeNames.InputContentTypeSupported, Tag.Keyword)]
    public InputContentType[]? InputContentTypeSupported { get; set; }

    /// <summary>
    /// input-film-scan-mode-supported
    /// See: PWG 5100.15-2013 Section 7.4.11
    /// </summary>
    [IppAttribute(IppAttributeNames.InputFilmScanModeSupported, Tag.Keyword)]
    public InputFilmScanMode[]? InputFilmScanModeSupported { get; set; }

    /// <summary>
    /// input-media-supported
    /// See: PWG 5100.15-2013 Section 7.4.12
    /// </summary>
    [IppAttribute(IppAttributeNames.InputMediaSupported)]
    public Media[]? InputMediaSupported { get; set; }

    /// <summary>
    /// input-orientation-requested-supported
    /// See: PWG 5100.15-2013 Section 7.4.13
    /// </summary>
    [IppAttribute(IppAttributeNames.InputOrientationRequestedSupported, Tag.Enum)]
    public Orientation[]? InputOrientationRequestedSupported { get; set; }

    /// <summary>
    /// input-quality-supported
    /// See: PWG 5100.15-2013 Section 7.4.14
    /// </summary>
    [IppAttribute(IppAttributeNames.InputQualitySupported, Tag.Enum)]
    public PrintQuality[]? InputQualitySupported { get; set; }

    /// <summary>
    /// input-resolution-supported
    /// See: PWG 5100.15-2013 Section 7.4.15
    /// </summary>
    [IppAttribute(IppAttributeNames.InputResolutionSupported, Tag.Resolution)]
    public Resolution[]? InputResolutionSupported { get; set; }

    /// <summary>
    /// input-sides-supported
    /// See: PWG 5100.15-2013 Section 7.4.17
    /// </summary>
    [IppAttribute(IppAttributeNames.InputSidesSupported, Tag.Keyword)]
    public Sides[]? InputSidesSupported { get; set; }

    /// <summary>
    /// input-source-supported
    /// See: PWG 5100.15-2013 Section 7.4.18
    /// </summary>
    [IppAttribute(IppAttributeNames.InputSourceSupported, Tag.Keyword)]
    public InputSource[]? InputSourceSupported { get; set; }

    /// <summary>
    /// logo-uri-formats-supported
    /// See: PWG 5100.15-2013 Section 7.4.19
    /// </summary>
    [IppAttribute(IppAttributeNames.LogoUriFormatsSupported, Tag.MimeMediaType)]
    public string[]? LogoUriFormatsSupported { get; set; }

    /// <summary>
    /// logo-uri-schemes-supported
    /// See: PWG 5100.15-2013 Section 7.4.20
    /// </summary>
    [IppAttribute(IppAttributeNames.LogoUriSchemesSupported, Tag.UriScheme)]
    public UriScheme[]? LogoUriSchemesSupported { get; set; }

    /// <summary>
    /// message-supported
    /// See: PWG 5100.15-2013 Section 7.4.21
    /// </summary>
    [IppAttribute(IppAttributeNames.MessageSupported, Tag.Integer)]
    public int? MessageSupported { get; set; }

    /// <summary>
    /// multiple-destination-uris-supported
    /// See: PWG 5100.15-2013 Section 7.4.22
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleDestinationUrisSupported, Tag.Boolean)]
    public bool? MultipleDestinationUrisSupported { get; set; }

    /// <summary>
    /// number-of-retries-default
    /// See: PWG 5100.15-2013 Section 7.4.23
    /// </summary>
    [IppAttribute(IppAttributeNames.NumberOfRetriesDefault, Tag.Integer)]
    public int? NumberOfRetriesDefault { get; set; }

    /// <summary>
    /// number-of-retries-supported
    /// See: PWG 5100.15-2013 Section 7.4.24
    /// </summary>
    [IppAttribute(IppAttributeNames.NumberOfRetriesSupported, Tag.RangeOfInteger)]
    public Protocol.Models.Range? NumberOfRetriesSupported { get; set; }

    /// <summary>
    /// organization-name-supported
    /// See: PWG 5100.15-2013 Section 7.4.25
    /// </summary>
    [IppAttribute(IppAttributeNames.OrganizationNameSupported, Tag.Integer)]
    public int? OrganizationNameSupported { get; set; }

    /// <summary>
    /// job-destination-spooling-supported
    /// See: PWG 5100.17-2014 Section 8.3.4
    /// </summary>
    [IppAttribute(IppAttributeNames.JobDestinationSpoolingSupported, Tag.Keyword)]
    public JobSpooling? JobDestinationSpoolingSupported { get; set; }

    /// <summary>
    /// output-attributes-default
    /// See: PWG 5100.17-2014 Section 8.3.5
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputAttributesDefault)]
    public OutputAttributes? OutputAttributesDefault { get; set; }

    /// <summary>
    /// output-attributes-supported
    /// See: PWG 5100.17-2014 Section 8.3.6
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputAttributesSupported, Tag.Keyword)]
    public OutputAttributesMember[]? OutputAttributesSupported { get; set; }

    /// <summary>
    /// printer-fax-log-uri
    /// See: PWG 5100.15-2013 Section 7.4.26
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFaxLogUri, Tag.Uri)]
    public Uri? PrinterFaxLogUri { get; set; }

    /// <summary>
    /// printer-fax-modem-info
    /// See: PWG 5100.15-2013 Section 7.4.27
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFaxModemInfo, Tag.TextWithoutLanguage)]
    public string[]? PrinterFaxModemInfo { get; set; }

    /// <summary>
    /// printer-fax-modem-name
    /// See: PWG 5100.15-2013 Section 7.4.28
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFaxModemName, Tag.NameWithoutLanguage)]
    public string[]? PrinterFaxModemName { get; set; }

    /// <summary>
    /// printer-fax-modem-number
    /// See: PWG 5100.15-2013 Section 7.4.29
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFaxModemNumber, Tag.Uri)]
    public Uri[]? PrinterFaxModemNumber { get; set; }

    /// <summary>
    /// retry-interval-default
    /// Type: integer(1:MAX)
    /// See: PWG 5100.15-2013 Section 7.4.30
    /// </summary>
    [IppAttribute(IppAttributeNames.RetryIntervalDefault, Tag.Integer)]
    public int? RetryIntervalDefault { get; set; }

    /// <summary>
    /// retry-interval-supported
    /// See: PWG 5100.15-2013 Section 7.4.31
    /// </summary>
    [IppAttribute(IppAttributeNames.RetryIntervalSupported, Tag.RangeOfInteger)]
    public Protocol.Models.Range? RetryIntervalSupported { get; set; }

    /// <summary>
    /// retry-time-out-default
    /// See: PWG 5100.15-2013 Section 7.4.32
    /// </summary>
    [IppAttribute(IppAttributeNames.RetryTimeOutDefault, Tag.Integer)]
    public int? RetryTimeOutDefault { get; set; }

    /// <summary>
    /// retry-time-out-supported
    /// See: PWG 5100.15-2013 Section 7.4.33
    /// </summary>
    [IppAttribute(IppAttributeNames.RetryTimeOutSupported, Tag.RangeOfInteger)]
    public Protocol.Models.Range? RetryTimeOutSupported { get; set; }

    /// <summary>
    /// subject-supported
    /// See: PWG 5100.15-2013 Section 7.4.34
    /// </summary>
    [IppAttribute(IppAttributeNames.SubjectSupported, Tag.Integer)]
    public int? SubjectSupported { get; set; }

    /// <summary>
    /// to-name-supported
    /// See: PWG 5100.15-2013 Section 7.4.35
    /// </summary>
    [IppAttribute(IppAttributeNames.ToNameSupported, Tag.Integer)]
    public int? ToNameSupported { get; set; }

    /// <summary>
    /// jpeg-x-dimension-supported
    /// Type: rangeOfInteger(0:65535)
    /// See: RFC 8011 Section 5.4.38
    /// </summary>
    [IppAttribute(IppAttributeNames.JpegXDimensionSupported, Tag.RangeOfInteger)]
    public Range? JpegXDimensionSupported { get; set; }

    /// <summary>
    /// jpeg-y-dimension-supported
    /// Type: rangeOfInteger(1:65535)
    /// See: RFC 8011 Section 5.4.39
    /// </summary>
    [IppAttribute(IppAttributeNames.JpegYDimensionSupported, Tag.RangeOfInteger)]
    public Range? JpegYDimensionSupported { get; set; }

    /// <summary>
    /// job-password-supported
    /// Type: integer(0:255)
    /// See: PWG 5100.11-2024 Section 7.2.1
    /// </summary>
    [Range(0, 255)]
    [IppAttribute(IppAttributeNames.JobPasswordSupported, Tag.Integer)]
    public int? JobPasswordSupported { get; set; }

    /// <summary>
    /// job-password-length-supported
    /// Type: rangeOfInteger(4:1020)
    /// See: PWG 5100.11-2024 Section 7.2.2
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPasswordLengthSupported, Tag.RangeOfInteger)]
    public Range? JobPasswordLengthSupported { get; set; }

    /// <summary>
    /// document-password-supported
    /// Type: integer(0:1023) (Valid: 0 or 255-1023)
    /// See: PWG 5100.11-2024 Section 7.2.4
    /// </summary>
    [Range(0, 0, 255, 1023)]
    [IppAttribute(IppAttributeNames.DocumentPasswordSupported, Tag.Integer)]
    public int? DocumentPasswordSupported { get; set; }

    /// <summary>
    /// x-side1-image-offset-supported
    /// See: PWG 5100.3-2023
    /// </summary>
    [Obsolete("The 'x-side1-image-offset-supported' attribute is obsolete. See PWG 5100.3-2023 Section 12.")]
    [IppAttribute(IppAttributeNames.XSide1ImageOffsetSupported, Tag.RangeOfInteger)]
    public Range? XSide1ImageOffsetSupported { get; set; }

    /// <summary>
    /// x-side2-image-offset-supported
    /// See: PWG 5100.3-2023
    /// </summary>
    [Obsolete("The 'x-side2-image-offset-supported' attribute is obsolete. See PWG 5100.3-2023 Section 12.")]
    [IppAttribute(IppAttributeNames.XSide2ImageOffsetSupported, Tag.RangeOfInteger)]
    public Range? XSide2ImageOffsetSupported { get; set; }

    /// <summary>
    /// y-side1-image-offset-supported
    /// See: PWG 5100.3-2023
    /// </summary>
    [Obsolete("The 'y-side1-image-offset-supported' attribute is obsolete. See PWG 5100.3-2023 Section 12.")]
    [IppAttribute(IppAttributeNames.YSide1ImageOffsetSupported, Tag.RangeOfInteger)]
    public Range? YSide1ImageOffsetSupported { get; set; }

    /// <summary>
    /// y-side2-image-offset-supported
    /// See: PWG 5100.3-2023
    /// </summary>
    [Obsolete("The 'y-side2-image-offset-supported' attribute is obsolete. See PWG 5100.3-2023 Section 12.")]
    [IppAttribute(IppAttributeNames.YSide2ImageOffsetSupported, Tag.RangeOfInteger)]
    public Range? YSide2ImageOffsetSupported { get; set; }

    /// <summary>
    /// user-defined-values-supported
    /// See: PWG 5100.3-2023
    /// </summary>
    [Obsolete("The 'user-defined-values-supported' attribute is obsolete. See PWG 5100.3-2023 Section 12.")]
    [IppAttribute(IppAttributeNames.UserDefinedValuesSupported, Tag.Keyword)]
    public string[]? UserDefinedValuesSupported { get; set; }

    /// <summary>
    /// pdl-init-file-supported
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'pdl-init-file-supported' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.PdlInitFileSupported, Tag.Keyword)]
    public string[]? PdlInitFileSupported { get; set; }

    /// <summary>
    /// pdl-init-file-default
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'pdl-init-file-default' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.PdlInitFileDefault)]
    public PdlInitFile? PdlInitFileDefault { get; set; }

    /// <summary>
    /// job-save-disposition-supported
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'job-save-disposition-supported' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.JobSaveDispositionSupported, Tag.Keyword)]
    public string[]? JobSaveDispositionSupported { get; set; }

    /// <summary>
    /// job-save-disposition-default
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'job-save-disposition-default' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.JobSaveDispositionDefault)]
    public JobSaveDisposition? JobSaveDispositionDefault { get; set; }

    /// <summary>
    /// save-disposition-supported
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'save-disposition-supported' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.SaveDispositionSupported, Tag.Keyword)]
    public SaveDisposition[]? SaveDispositionSupported { get; set; }

    /// <summary>
    /// save-info-supported
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'save-info-supported' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.SaveInfoSupported, Tag.Keyword)]
    public string[]? SaveInfoSupported { get; set; }

    /// <summary>
    /// save-location-supported
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'save-location-supported' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.SaveLocationSupported, Tag.Uri)]
    public Uri[]? SaveLocationSupported { get; set; }

    /// <summary>
    /// The pages-per-subset-supported Printer Description attribute.
    /// See: PWG 5100.8-2003 Section 4.2
    /// </summary>
    [Obsolete("The 'pages-per-subset-supported' attribute is obsolete. See PWG 5100.13-2023 Section 7.1.")]
    [IppAttribute(IppAttributeNames.PagesPerSubsetSupported, Tag.Boolean)]
    public bool? PagesPerSubsetSupported { get; set; }
}
