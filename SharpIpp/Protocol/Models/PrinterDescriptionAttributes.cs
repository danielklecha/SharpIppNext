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
    public IppValue<Uri[]>? PrinterUriSupported { get; set; }

    /// <summary>
    /// uri-security-supported
    /// See: RFC 8011 Section 5.4.3
    /// </summary>
    [IppAttribute(IppAttributeNames.UriSecuritySupported, Tag.Keyword)]
    public IppValue<UriSecurity[]>? UriSecuritySupported { get; set; }

    /// <summary>
    /// uri-authentication-supported
    /// See: RFC 8011 Section 5.4.2
    /// </summary>
    [IppAttribute(IppAttributeNames.UriAuthenticationSupported, Tag.Keyword)]
    public IppValue<UriAuthentication[]>? UriAuthenticationSupported { get; set; }

    /// <summary>
    /// printer-name
    /// See: RFC 8011 Section 5.4.4
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterName, Tag.NameWithoutLanguage)]
    public IppValue<string>? PrinterName { get; set; }

    /// <summary>
    /// printer-location
    /// See: RFC 8011 Section 5.4.5
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterLocation, Tag.TextWithoutLanguage)]
    public IppValue<string>? PrinterLocation { get; set; }

    /// <summary>
    /// printer-info
    /// See: RFC 8011 Section 5.4.6
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterInfo, Tag.TextWithoutLanguage)]
    public IppValue<string>? PrinterInfo { get; set; }

    /// <summary>
    /// printer-more-info
    /// See: RFC 8011 Section 5.4.7
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMoreInfo, Tag.Uri)]
    public IppValue<Uri>? PrinterMoreInfo { get; set; }

    /// <summary>
    /// printer-driver-installer
    /// See: RFC 8011 Section 5.4.8
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterDriverInstaller, Tag.Uri)]
    public IppValue<Uri>? PrinterDriverInstaller { get; set; }

    /// <summary>
    /// printer-make-and-model
    /// See: RFC 8011 Section 5.4.9
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMakeAndModel, Tag.TextWithoutLanguage)]
    public IppValue<string>? PrinterMakeAndModel { get; set; }

    /// <summary>
    /// printer-more-info-manufacturer
    /// See: RFC 8011 Section 5.4.10
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMoreInfoManufacturer, Tag.Uri)]
    public IppValue<Uri>? PrinterMoreInfoManufacturer { get; set; }

    /// <summary>
    /// printer-state
    /// See: RFC 8011 Section 5.4.11
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterState, Tag.Enum)]
    public IppValue<PrinterState>? PrinterState { get; set; }

    /// <summary>
    /// printer-state-reasons
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStateReasons, Tag.Keyword)]
    public IppValue<PrinterStateReason[]>? PrinterStateReasons { get; set; }

    /// <summary>
    /// printer-state-message
    /// See: RFC 8011 Section 5.4.13
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStateMessage, Tag.TextWithoutLanguage)]
    public IppValue<string>? PrinterStateMessage { get; set; }

    /// <summary>
    /// printer-state-change-time
    /// See: RFC 8011 Section 5.4.14
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStateChangeTime, Tag.Integer)]
    public IppValue<int>? PrinterStateChangeTime { get; set; }

    /// <summary>
    /// printer-state-change-date-time
    /// See: RFC 8011 Section 5.4.15
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStateChangeDateTime, Tag.DateTime)]
    public IppValue<DateTimeOffset>? PrinterStateChangeDateTime { get; set; }

    /// <summary>
    /// printer-detailed-status-messages
    /// See: PWG 5100.7-2023 Section 6.10.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterDetailedStatusMessages, Tag.TextWithoutLanguage)]
    public IppValue<string[]>? PrinterDetailedStatusMessages { get; set; }

    /// <summary>
    /// ipp-versions-supported
    /// See: RFC 8011 Section 5.4.16
    /// </summary>
    [IppAttribute(IppAttributeNames.IppVersionsSupported, Tag.Keyword)]
    public IppValue<IppVersion[]>? IppVersionsSupported { get; set; }

    /// <summary>
    /// operations-supported
    /// See: RFC 8011 Section 5.4.17
    /// </summary>
    [IppAttribute(IppAttributeNames.OperationsSupported, Tag.Enum)]
    public IppValue<IppOperation[]>? OperationsSupported { get; set; }

    /// <summary>
    /// multiple-document-jobs-supported
    /// See: RFC 8011 Section 5.4.16
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleDocumentJobsSupported, Tag.Boolean)]
    public IppValue<bool>? MultipleDocumentJobsSupported { get; set; }

    /// <summary>
    /// multiple-document-handling-default
    /// See: RFC 2911 Section 4.2.4
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleDocumentHandlingDefault, Tag.Keyword)]
    public IppValue<MultipleDocumentHandling>? MultipleDocumentHandlingDefault { get; set; }

    /// <summary>
    /// multiple-document-handling-supported
    /// See: RFC 2911 Section 4.2.4
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleDocumentHandlingSupported, Tag.Keyword)]
    public IppValue<MultipleDocumentHandling[]>? MultipleDocumentHandlingSupported { get; set; }

    /// <summary>
    /// charset-configured
    /// See: RFC 8011 Section 5.4.17
    /// </summary>
    [IppAttribute(IppAttributeNames.CharsetConfigured, Tag.Charset)]
    public IppValue<string>? CharsetConfigured { get; set; }

    /// <summary>
    /// charset-supported
    /// See: RFC 8011 Section 5.4.18
    /// </summary>
    [IppAttribute(IppAttributeNames.CharsetSupported, Tag.Charset)]
    public IppValue<string[]>? CharsetSupported { get; set; }

    /// <summary>
    /// natural-language-configured
    /// See: RFC 8011 Section 5.4.19
    /// </summary>
    [IppAttribute(IppAttributeNames.NaturalLanguageConfigured, Tag.NaturalLanguage)]
    public IppValue<NaturalLanguage>? NaturalLanguageConfigured { get; set; }

    /// <summary>
    /// generated-natural-language-supported
    /// See: RFC 8011 Section 5.4.20
    /// </summary>
    [IppAttribute(IppAttributeNames.GeneratedNaturalLanguageSupported, Tag.NaturalLanguage)]
    public IppValue<NaturalLanguage[]>? GeneratedNaturalLanguageSupported { get; set; }

    /// <summary>
    /// client-info-supported
    /// See: PWG 5100.7-2023 Section 6.9.5
    /// </summary>
    [IppAttribute(IppAttributeNames.ClientInfoSupported, Tag.Keyword)]
    public IppValue<ClientInfoMember[]>? ClientInfoSupported { get; set; }

    /// <summary>
    /// max-client-info-supported
    /// See: PWG 5100.7-2023 Section 6.9.41
    /// </summary>
    [IppAttribute(IppAttributeNames.MaxClientInfoSupported, Tag.Integer)]
    public IppValue<int>? MaxClientInfoSupported { get; set; }

    /// <summary>
    /// document-charset-default
    /// See: PWG 5100.7-2023 Section 6.9.16
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentCharsetDefault, Tag.Charset)]
    public IppValue<Charset>? DocumentCharsetDefault { get; set; }

    /// <summary>
    /// document-charset-supported
    /// See: PWG 5100.7-2023 Section 6.9.17
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentCharsetSupported, Tag.Charset)]
    public IppValue<Charset[]>? DocumentCharsetSupported { get; set; }

    /// <summary>
    /// document-format-details-supported
    /// See: PWG 5100.7-2023 Section 6.9.20
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentFormatDetailsSupported, Tag.Keyword)]
    public IppValue<DocumentFormatDetail[]>? DocumentFormatDetailsSupported { get; set; }

    /// <summary>
    /// document-natural-language-default
    /// See: PWG 5100.7-2023 Section 6.9.48
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentNaturalLanguageDefault, Tag.NaturalLanguage)]
    public IppValue<NaturalLanguage>? DocumentNaturalLanguageDefault { get; set; }

    /// <summary>
    /// document-natural-language-supported
    /// See: PWG 5100.7-2023 Section 6.9.49
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentNaturalLanguageSupported, Tag.NaturalLanguage)]
    public IppValue<NaturalLanguage[]>? DocumentNaturalLanguageSupported { get; set; }

    /// <summary>
    /// job-ids-supported
    /// See: PWG 5100.7-2023 Section 6.9.26
    /// </summary>
    [IppAttribute(IppAttributeNames.JobIdsSupported, Tag.Boolean)]
    public IppValue<bool>? JobIdsSupported { get; set; }

    /// <summary>
    /// job-mandatory-attributes-supported
    /// See: PWG 5100.7-2023 Section 6.9.28
    /// </summary>
    [IppAttribute(IppAttributeNames.JobMandatoryAttributesSupported, Tag.Boolean)]
    public IppValue<bool>? JobMandatoryAttributesSupported { get; set; }

    /// <summary>
    /// job-sheets-col-default
    /// See: PWG 5100.7-2023 Section 6.9.29
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSheetsColDefault)]
    public IppValue<JobSheetsCol>? JobSheetsColDefault { get; set; }

    /// <summary>
    /// job-sheets-col-supported
    /// See: PWG 5100.7-2023 Section 6.9.30
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSheetsColSupported, Tag.Keyword)]
    public IppValue<JobSheetsColMember[]>? JobSheetsColSupported { get; set; }

    /// <summary>
    /// document-format-default
    /// See: RFC 8011 Section 5.4.21
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentFormatDefault, Tag.MimeMediaType)]
    public IppValue<DocumentFormat>? DocumentFormatDefault { get; set; }

    /// <summary>
    /// document-format-supported
    /// See: RFC 8011 Section 5.4.22
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentFormatSupported, Tag.MimeMediaType)]
    public IppValue<string[]>? DocumentFormatSupported { get; set; }

    /// <summary>
    /// printer-is-accepting-jobs
    /// See: RFC 8011 Section 5.4.23
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterIsAcceptingJobs, Tag.Boolean)]
    public IppValue<bool>? PrinterIsAcceptingJobs { get; set; }

    /// <summary>
    /// queued-job-count
    /// See: RFC 8011 Section 5.4.24
    /// </summary>
    [IppAttribute(IppAttributeNames.QueuedJobCount, Tag.Integer)]
    public IppValue<int>? QueuedJobCount { get; set; }

    /// <summary>
    /// printer-message-from-operator
    /// See: RFC 8011 Section 5.4.25
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMessageFromOperator, Tag.TextWithoutLanguage)]
    public IppValue<string>? PrinterMessageFromOperator { get; set; }

    /// <summary>
    /// color-supported
    /// See: RFC 8011 Section 5.4.26
    /// </summary>
    [IppAttribute(IppAttributeNames.ColorSupported, Tag.Boolean)]
    public IppValue<bool>? ColorSupported { get; set; }

    /// <summary>
    /// reference-uri-schemes-supported
    /// See: RFC 8011 Section 5.4.27
    /// </summary>
    [IppAttribute(IppAttributeNames.ReferenceUriSchemesSupported, Tag.UriScheme)]
    public IppValue<UriScheme[]>? ReferenceUriSchemesSupported { get; set; }

    /// <summary>
    /// pdl-override-supported
    /// See: RFC 8011 Section 5.4.28
    /// </summary>
    [IppAttribute(IppAttributeNames.PdlOverrideSupported, Tag.Keyword)]
    public IppValue<PdlOverride>? PdlOverrideSupported { get; set; }

    /// <summary>
    /// overrides-supported
    /// See: PWG 5100.6-2003 Section 4.1.7
    /// </summary>
    [IppAttribute(IppAttributeNames.OverridesSupported, Tag.Keyword)]
    public IppValue<OverrideSupported[]>? OverridesSupported { get; set; }

    /// <summary>
    /// printer-up-time
    /// See: RFC 8011 Section 5.4.29
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterUpTime, Tag.Integer)]
    public IppValue<int>? PrinterUpTime { get; set; }

    /// <summary>
    /// printer-current-time
    /// See: RFC 8011 Section 5.4.30
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterCurrentTime, Tag.DateTime)]
    public IppValue<DateTimeOffset>? PrinterCurrentTime { get; set; }

    /// <summary>
    /// printer-config-change-time
    /// See: RFC 8011 Section 5.4.31
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterConfigChangeTime, Tag.Integer)]
    public IppValue<int>? PrinterConfigChangeTime { get; set; }

    /// <summary>
    /// printer-config-change-date-time
    /// See: RFC 8011 Section 5.4.31
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterConfigChangeDateTime, Tag.DateTime)]
    public IppValue<DateTimeOffset>? PrinterConfigChangeDateTime { get; set; }

    /// <summary>
    /// printer-config-changes
    /// See: PWG 5100.22-2025 Section 7.7.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterConfigChanges, Tag.Integer)]
    public IppValue<int>? PrinterConfigChanges { get; set; }

    /// <summary>
    /// printer-contact-col
    /// See: PWG 5100.22-2025 Section 7.6.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterContactCol)]
    public IppValue<SystemContact[]>? PrinterContactCol { get; set; }

    /// <summary>
    /// printer-geo-location
    /// See: PWG 5100.22-2025 Section 7.1.7
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterGeoLocation, Tag.Uri)]
    public IppValue<Uri>? PrinterGeoLocation { get; set; }

    /// <summary>
    /// printer-ids
    /// See: PWG 5100.22-2025 Section 7.1.6
    /// </summary>
    [Range(1, 65535)]
    [IppAttribute(IppAttributeNames.PrinterIds, Tag.Integer)]
    public IppValue<int[]>? PrinterIds { get; set; }

    /// <summary>
    /// printer-impressions-completed
    /// See: PWG 5100.22-2025 Section 7.7.3
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterImpressionsCompleted, Tag.Integer)]
    public IppValue<int>? PrinterImpressionsCompleted { get; set; }

    /// <summary>
    /// printer-impressions-completed-col
    /// See: PWG 5100.22-2025 Section 7.7.4
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterImpressionsCompletedCol, Tag.Integer)]
    public IppValue<int>? PrinterImpressionsCompletedCol { get; set; }

    /// <summary>
    /// printer-media-sheets-completed
    /// See: PWG 5100.22-2025 Section 7.7.5
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMediaSheetsCompleted, Tag.Integer)]
    public IppValue<int>? PrinterMediaSheetsCompleted { get; set; }

    /// <summary>
    /// printer-media-sheets-completed-col
    /// See: PWG 5100.22-2025 Section 7.7.6
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMediaSheetsCompletedCol, Tag.Integer)]
    public IppValue<int>? PrinterMediaSheetsCompletedCol { get; set; }

    /// <summary>
    /// printer-pages-completed
    /// See: PWG 5100.22-2025 Section 7.7.7
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterPagesCompleted, Tag.Integer)]
    public IppValue<int>? PrinterPagesCompleted { get; set; }

    /// <summary>
    /// printer-pages-completed-col
    /// See: PWG 5100.22-2025 Section 7.7.8
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterPagesCompletedCol, Tag.Integer)]
    public IppValue<int>? PrinterPagesCompletedCol { get; set; }

    /// <summary>
    /// multiple-operation-time-out
    /// Type: integer(1:MAX) (Rec: 60-240)
    /// See: RFC 8011 Section 5.4.31
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleOperationTimeOut, Tag.Integer)]
    public IppValue<int>? MultipleOperationTimeOut { get; set; }

    /// <summary>
    /// multiple-operation-time-out-action
    /// See: PWG 5100.13-2023 Section 6.5.19
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleOperationTimeOutAction, Tag.Keyword)]
    public IppValue<MultipleOperationTimeOutAction>? MultipleOperationTimeOutAction { get; set; }

    /// <summary>
    /// compression-supported
    /// See: RFC 8011 Section 5.4.32
    /// </summary>
    [IppAttribute(IppAttributeNames.CompressionSupported, Tag.Keyword)]
    public IppValue<Compression[]>? CompressionSupported { get; set; }

    /// <summary>
    /// compression-default
    /// See: RFC 8011 Section 5.4.32
    /// </summary>
    [IppAttribute(IppAttributeNames.CompressionDefault, Tag.Keyword)]
    public IppValue<Compression>? CompressionDefault { get; set; }

    /// <summary>
    /// job-k-octets-supported
    /// See: RFC 8011 Section 5.4.33
    /// </summary>
    [IppAttribute(IppAttributeNames.JobKOctetsSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? JobKOctetsSupported { get; set; }

    /// <summary>
    /// jpeg-k-octets-supported
    /// See: PWG 5100.13-2023 Section 6.5.12
    /// </summary>
    [IppAttribute(IppAttributeNames.JpegKOctetsSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? JpegKOctetsSupported { get; set; }

    /// <summary>
    /// pdf-k-octets-supported
    /// See: PWG 5100.13-2023 Section 6.5.20
    /// </summary>
    [IppAttribute(IppAttributeNames.PdfKOctetsSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? PdfKOctetsSupported { get; set; }

    /// <summary>
    /// job-impressions-supported
    /// See: RFC 8011 Section 5.4.34
    /// </summary>
    [IppAttribute(IppAttributeNames.JobImpressionsSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? JobImpressionsSupported { get; set; }

    /// <summary>
    /// job-media-sheets-supported
    /// See: RFC 8011 Section 5.4.35
    /// </summary>
    [IppAttribute(IppAttributeNames.JobMediaSheetsSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? JobMediaSheetsSupported { get; set; }

    /// <summary>
    /// job-sheets-default
    /// See: RFC 2911 Section 4.2.3
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSheetsDefault, Tag.Keyword)]
    public IppValue<JobSheets>? JobSheetsDefault { get; set; }

    /// <summary>
    /// job-sheets-supported
    /// See: RFC 2911 Section 4.2.3
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSheetsSupported, Tag.Keyword)]
    public IppValue<JobSheets[]>? JobSheetsSupported { get; set; }

    /// <summary>
    /// number-up-default
    /// See: RFC 2911 Section 4.2.7
    /// </summary>
    [IppAttribute(IppAttributeNames.NumberUpDefault, Tag.Integer)]
    public IppValue<int>? NumberUpDefault { get; set; }

    /// <summary>
    /// number-up-supported
    /// See: RFC 2911 Section 4.2.7
    /// </summary>
    [IppAttribute(IppAttributeNames.NumberUpSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? NumberUpSupported { get; set; }

    /// <summary>
    /// pages-per-minute
    /// Type: integer(0:MAX)
    /// See: RFC 8011 Section 5.4.36
    /// </summary>
    [IppAttribute(IppAttributeNames.PagesPerMinute, Tag.Integer)]
    public IppValue<int>? PagesPerMinute { get; set; }

    /// <summary>
    /// pages-per-minute-color
    /// See: RFC 8011 Section 5.4.37
    /// </summary>
    [IppAttribute(IppAttributeNames.PagesPerMinuteColor, Tag.Integer)]
    public IppValue<int>? PagesPerMinuteColor { get; set; }

    /// <summary>
    /// print-scaling-default
    /// See: PWG 5100.13-2023 Section 6.5.29
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintScalingDefault, Tag.Keyword)]
    public IppValue<PrintScaling>? PrintScalingDefault { get; set; }

    /// <summary>
    /// print-scaling-supported
    /// See: PWG 5100.13-2023 Section 6.5.30
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintScalingSupported, Tag.Keyword)]
    public IppValue<PrintScaling[]>? PrintScalingSupported { get; set; }

    /// <summary>
    /// media-default
    /// See: RFC 8011 Section 5.2.11
    /// </summary>
    [IppAttribute()]
    public IppValue<Media>? MediaDefault { get; set; }

    /// <summary>
    /// media-supported
    /// See: RFC 8011 Section 5.2.11
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaSupported)]
    public IppValue<Media[]>? MediaSupported { get; set; }

    /// <summary>
    /// media-ready
    /// See: RFC 2911 Section 4.2.11
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaReady)]
    public IppValue<Media[]>? MediaReady { get; set; }

    /// <summary>
    /// sides-default
    /// See: RFC 8011 Section 5.2.8
    /// </summary>
    [IppAttribute(IppAttributeNames.SidesDefault, Tag.Keyword)]
    public IppValue<Sides>? SidesDefault { get; set; }

    /// <summary>
    /// sides-supported
    /// See: RFC 8011 Section 5.2.8
    /// </summary>
    [IppAttribute(IppAttributeNames.SidesSupported, Tag.Keyword)]
    public IppValue<Sides[]>? SidesSupported { get; set; }

    /// <summary>
    /// finishings-default
    /// See: RFC 8011 Section 5.2.6
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingsDefault, Tag.Enum)]
    public IppValue<Finishings>? FinishingsDefault { get; set; }

    /// <summary>
    /// finishings-supported
    /// See: RFC 8011 Section 5.2.6
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingsSupported, Tag.Enum)]
    public IppValue<Finishings[]>? FinishingsSupported { get; set; }

    /// <summary>
    /// printer-resolution-default
    /// See: RFC 8011 Section 5.2.12
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterResolutionDefault, Tag.Resolution)]
    public IppValue<Resolution>? PrinterResolutionDefault { get; set; }

    /// <summary>
    /// printer-resolution-supported
    /// See: RFC 8011 Section 5.2.12
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterResolutionSupported, Tag.Resolution)]
    public IppValue<Resolution[]>? PrinterResolutionSupported { get; set; }

    /// <summary>
    /// print-quality-default
    /// See: RFC 8011 Section 5.2.13
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintQualityDefault, Tag.Enum)]
    public IppValue<PrintQuality>? PrintQualityDefault { get; set; }

    /// <summary>
    /// print-quality-supported
    /// See: RFC 8011 Section 5.2.13
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintQualitySupported, Tag.Enum)]
    public IppValue<PrintQuality[]>? PrintQualitySupported { get; set; }

    /// <summary>
    /// job-priority-default
    /// See: RFC 8011 Section 5.2.1
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPriorityDefault, Tag.Integer)]
    public IppValue<int>? JobPriorityDefault { get; set; }

    /// <summary>
    /// job-priority-supported
    /// See: RFC 8011 Section 5.2.1
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPrioritySupported, Tag.Integer)]
    public IppValue<int>? JobPrioritySupported { get; set; }

    /// <summary>
    /// copies-default
    /// See: RFC 8011 Section 5.2.5
    /// </summary>
    [IppAttribute(IppAttributeNames.CopiesDefault, Tag.Integer)]
    public IppValue<int>? CopiesDefault { get; set; }

    /// <summary>
    /// copies-supported
    /// See: RFC 8011 Section 5.2.5
    /// </summary>
    [IppAttribute(IppAttributeNames.CopiesSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? CopiesSupported { get; set; }

    /// <summary>
    /// orientation-requested-default
    /// See: RFC 8011 Section 5.2.10
    /// </summary>
    [IppAttribute(IppAttributeNames.OrientationRequestedDefault, Tag.Enum)]
    public IppValue<Orientation>? OrientationRequestedDefault { get; set; }

    /// <summary>
    /// orientation-requested-supported
    /// See: RFC 8011 Section 5.2.10
    /// </summary>
    [IppAttribute(IppAttributeNames.OrientationRequestedSupported, Tag.Enum)]
    public IppValue<Orientation[]>? OrientationRequestedSupported { get; set; }

    /// <summary>
    /// page-ranges-supported
    /// See: RFC 8011 Section 5.2.7
    /// </summary>
    [IppAttribute(IppAttributeNames.PageRangesSupported, Tag.Boolean)]
    public IppValue<bool>? PageRangesSupported { get; set; }

    /// <summary>
    /// job-hold-until-supported
    /// See: RFC 8011 Section 5.2.2
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHoldUntilSupported, Tag.Keyword)]
    public IppValue<JobHoldUntil[]>? JobHoldUntilSupported { get; set; }

    /// <summary>
    /// job-hold-until-default
    /// See: RFC 8011 Section 5.2.2
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHoldUntilDefault, Tag.Keyword)]
    public IppValue<JobHoldUntil>? JobHoldUntilDefault { get; set; }

    /// <summary>
    /// job-hold-until-time-supported
    /// See: PWG 5100.7-2023 Section 6.9.21
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHoldUntilTimeSupported, Tag.Boolean)]
    public IppValue<bool>? JobHoldUntilTimeSupported { get; set; }

    /// <summary>
    /// job-delay-output-until-default
    /// See: PWG 5100.7-2023 Section 6.9.14
    /// </summary>
    [IppAttribute(IppAttributeNames.JobDelayOutputUntilDefault, Tag.Keyword)]
    public IppValue<JobHoldUntil>? JobDelayOutputUntilDefault { get; set; }

    /// <summary>
    /// job-delay-output-until-supported
    /// See: PWG 5100.7-2023 Section 6.9.15
    /// </summary>
    [IppAttribute(IppAttributeNames.JobDelayOutputUntilSupported, Tag.Keyword)]
    public IppValue<JobHoldUntil[]>? JobDelayOutputUntilSupported { get; set; }

    /// <summary>
    /// job-delay-output-until-time-supported
    /// See: PWG 5100.7-2023 Section 6.9.16
    /// </summary>
    [IppAttribute(IppAttributeNames.JobDelayOutputUntilTimeSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? JobDelayOutputUntilTimeSupported { get; set; }

    /// <summary>
    /// job-history-attributes-configured
    /// See: PWG 5100.7-2023 Section 6.9.17
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHistoryAttributesConfigured, Tag.Keyword)]
    public IppValue<JobHistoryAttribute[]>? JobHistoryAttributesConfigured { get; set; }

    /// <summary>
    /// job-history-attributes-supported
    /// See: PWG 5100.7-2023 Section 6.9.18
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHistoryAttributesSupported, Tag.Keyword)]
    public IppValue<JobHistoryAttribute[]>? JobHistoryAttributesSupported { get; set; }

    /// <summary>
    /// job-history-interval-configured
    /// See: PWG 5100.7-2023 Section 6.9.19
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHistoryIntervalConfigured, Tag.Integer)]
    public IppValue<int>? JobHistoryIntervalConfigured { get; set; }

    /// <summary>
    /// job-history-interval-supported
    /// See: PWG 5100.7-2023 Section 6.9.20
    /// </summary>
    [IppAttribute(IppAttributeNames.JobHistoryIntervalSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? JobHistoryIntervalSupported { get; set; }

    /// <summary>
    /// job-retain-until-default
    /// See: PWG 5100.7-2023 Section 6.9.24
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRetainUntilDefault, Tag.Keyword)]
    public IppValue<JobHoldUntil>? JobRetainUntilDefault { get; set; }

    /// <summary>
    /// job-retain-until-interval-default
    /// See: PWG 5100.7-2023 Section 6.9.25
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRetainUntilIntervalDefault, Tag.Integer)]
    public IppValue<int>? JobRetainUntilIntervalDefault { get; set; }

    /// <summary>
    /// job-retain-until-interval-supported
    /// See: PWG 5100.7-2023 Section 6.9.26
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRetainUntilIntervalSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? JobRetainUntilIntervalSupported { get; set; }

    /// <summary>
    /// job-retain-until-supported
    /// See: PWG 5100.7-2023 Section 6.9.27
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRetainUntilSupported, Tag.Keyword)]
    public IppValue<JobHoldUntil[]>? JobRetainUntilSupported { get; set; }

    /// <summary>
    /// job-retain-until-time-supported
    /// See: PWG 5100.7-2023 Section 6.9.28
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRetainUntilTimeSupported, Tag.Boolean)]
    public IppValue<bool>? JobRetainUntilTimeSupported { get; set; }

    /// <summary>
    /// output-bin-default
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    [IppAttribute()]
    public IppValue<OutputBin>? OutputBinDefault { get; set; }

    /// <summary>
    /// output-bin-supported
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputBinSupported)]
    public IppValue<OutputBin[]>? OutputBinSupported { get; set; }

    /// <summary>
    /// media-col-default
    /// See: PWG 5100.7-2023
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaColDefault)]
    public IppValue<MediaCol>? MediaColDefault { get; set; }

    /// <summary>
    /// media-col-database
    /// See: PWG 5100.7-2023 Section 6.9.36
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaColDatabase)]
    public IppValue<MediaCol[]>? MediaColDatabase { get; set; }

    /// <summary>
    /// media-col-ready
    /// See: PWG 5100.7-2023 Section 6.9.38
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaColReady)]
    public IppValue<MediaCol[]>? MediaColReady { get; set; }

    /// <summary>
    /// media-col-supported
    /// See: PWG 5100.7-2023 Section 6.9.39
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaColSupported, Tag.Keyword)]
    public IppValue<MediaColMember[]>? MediaColSupported { get; set; }

    /// <summary>
    /// media-size-supported
    /// See: PWG 5100.7-2023 Section 6.9.50
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaSizeSupported)]
    public IppValue<MediaSizeSupported[]>? MediaSizeSupported { get; set; }

    /// <summary>
    /// media-key-supported
    /// See: PWG 5100.7-2023 Section 6.9.44
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaKeySupported)]
    public IppValue<MediaKey[]>? MediaKeySupported { get; set; }

    /// <summary>
    /// media-source-supported
    /// See: PWG 5100.7-2023 Section 6.9.51
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaSourceSupported, Tag.Keyword)]
    public IppValue<MediaSource[]>? MediaSourceSupported { get; set; }

    /// <summary>
    /// media-type-supported
    /// See: PWG 5100.7-2023 Section 6.9.55
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaTypeSupported, Tag.Keyword)]
    public IppValue<MediaType[]>? MediaTypeSupported { get; set; }

    /// <summary>
    /// media-back-coating-supported
    /// See: PWG 5100.7-2023 Section 6.9.34
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaBackCoatingSupported, Tag.Keyword)]
    public IppValue<MediaCoating[]>? MediaBackCoatingSupported { get; set; }

    /// <summary>
    /// media-front-coating-supported
    /// See: PWG 5100.7-2023 Section 6.9.41
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaFrontCoatingSupported, Tag.Keyword)]
    public IppValue<MediaCoating[]>? MediaFrontCoatingSupported { get; set; }

    /// <summary>
    /// media-color-supported
    /// See: PWG 5100.7-2023 Section 6.9.40
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaColorSupported, Tag.Keyword)]
    public IppValue<MediaColor[]>? MediaColorSupported { get; set; }

    /// <summary>
    /// media-grain-supported
    /// See: PWG 5100.7-2023 Section 6.9.42
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaGrainSupported, Tag.Keyword)]
    public IppValue<MediaGrain[]>? MediaGrainSupported { get; set; }

    /// <summary>
    /// media-tooth-supported
    /// See: PWG 5100.7-2023 Section 6.9.53
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaToothSupported, Tag.Keyword)]
    public IppValue<MediaTooth[]>? MediaToothSupported { get; set; }

    /// <summary>
    /// media-pre-printed-supported
    /// See: PWG 5100.7-2023 Section 6.9.47
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaPrePrintedSupported, Tag.Keyword)]
    public IppValue<MediaPrePrinted[]>? MediaPrePrintedSupported { get; set; }

    /// <summary>
    /// media-recycled-supported
    /// See: PWG 5100.7-2023 Section 6.9.48
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaRecycledSupported, Tag.Keyword)]
    public IppValue<MediaRecycled[]>? MediaRecycledSupported { get; set; }

    /// <summary>
    /// media-hole-count-supported
    /// See: PWG 5100.7-2023 Section 6.9.43
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaHoleCountSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? MediaHoleCountSupported { get; set; }

    /// <summary>
    /// media-order-count-supported
    /// See: PWG 5100.7-2023 Section 6.9.46
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaOrderCountSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? MediaOrderCountSupported { get; set; }

    /// <summary>
    /// media-thickness-supported
    /// See: PWG 5100.7-2023 Section 6.9.52
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaThicknessSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? MediaThicknessSupported { get; set; }

    /// <summary>
    /// media-weight-metric-supported
    /// See: PWG 5100.7-2023 Section 6.9.56
    /// </summary>
    [IppAttribute(IppAttributeNames.MediaWeightMetricSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? MediaWeightMetricSupported { get; set; }

    /// <summary>
    /// media-bottom-margin-supported
    /// See: PWG 5100.7-2023 Section 6.9.35
    /// </summary>
    [Range(0, int.MaxValue)]
    [IppAttribute(IppAttributeNames.MediaBottomMarginSupported, Tag.Integer)]
    public IppValue<int[]>? MediaBottomMarginSupported { get; set; }

    /// <summary>
    /// media-left-margin-supported
    /// See: PWG 5100.7-2023 Section 6.9.45
    /// </summary>
    [Range(0, int.MaxValue)]
    [IppAttribute(IppAttributeNames.MediaLeftMarginSupported, Tag.Integer)]
    public IppValue<int[]>? MediaLeftMarginSupported { get; set; }

    /// <summary>
    /// media-right-margin-supported
    /// See: PWG 5100.7-2023 Section 6.9.49
    /// </summary>
    [Range(0, int.MaxValue)]
    [IppAttribute(IppAttributeNames.MediaRightMarginSupported, Tag.Integer)]
    public IppValue<int[]>? MediaRightMarginSupported { get; set; }

    /// <summary>
    /// media-top-margin-supported
    /// See: PWG 5100.7-2023 Section 6.9.54
    /// </summary>
    [Range(0, int.MaxValue)]
    [IppAttribute(IppAttributeNames.MediaTopMarginSupported, Tag.Integer)]
    public IppValue<int[]>? MediaTopMarginSupported { get; set; }

    /// <summary>
    /// print-color-mode-default
    /// See: PWG 5100.13-2023 Section 6.5.23
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintColorModeDefault, Tag.Keyword)]
    public IppValue<PrintColorMode>? PrintColorModeDefault { get; set; }

    /// <summary>
    /// print-color-mode-supported
    /// See: PWG 5100.13-2023 Section 6.5.25
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintColorModeSupported, Tag.Keyword)]
    public IppValue<PrintColorMode[]>? PrintColorModeSupported { get; set; }

    /// <summary>
    /// which-jobs-supported
    /// See: RFC 8011 Section 4.2.6.1
    /// </summary>
    [IppAttribute(IppAttributeNames.WhichJobsSupported, Tag.Keyword)]
    public IppValue<WhichJobs[]>? WhichJobsSupported { get; set; }

    /// <summary>
    /// printer-uuid
    /// See: PWG 5100.13-2023 Section 6.6.14
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterUUID, Tag.Uri)]
    public IppValue<string>? PrinterUUID { get; set; }

    /// <summary>
    /// pdf-versions-supported
    /// See: RFC 8011 Section 5.4.38
    /// </summary>
    [IppAttribute(IppAttributeNames.PdfVersionsSupported, Tag.Keyword)]
    public IppValue<PdfVersion[]>? PdfVersionsSupported { get; set; }

    /// <summary>
    /// ipp-features-supported
    /// See: RFC 8011 Section 5.4.39
    /// </summary>
    [IppAttribute(IppAttributeNames.IppFeaturesSupported, Tag.Keyword)]
    public IppValue<IppFeature[]>? IppFeaturesSupported { get; set; }

    /// <summary>
    /// document-creation-attributes-supported
    /// See: PWG 5100.5-2024 Section 6.5.1
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentCreationAttributesSupported, Tag.Keyword)]
    public IppValue<DocumentCreationAttribute[]>? DocumentCreationAttributesSupported { get; set; }

    /// <summary>
    /// job-account-id-default
    /// See: PWG 5100.7-2023 Section 6.9.7
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountIdDefault, Tag.NameWithoutLanguage)]
    public IppValue<string>? JobAccountIdDefault { get; set; }

    /// <summary>
    /// job-account-id-supported
    /// See: PWG 5100.7-2023 Section 6.9.8
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountIdSupported, Tag.Boolean)]
    public IppValue<bool>? JobAccountIdSupported { get; set; }

    /// <summary>
    /// job-accounting-user-id-default
    /// See: PWG 5100.7-2023 Section 6.9.9
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountingUserIdDefault, Tag.NameWithoutLanguage)]
    public IppValue<string>? JobAccountingUserIdDefault { get; set; }

    /// <summary>
    /// job-accounting-user-id-supported
    /// See: PWG 5100.7-2023 Section 6.9.10
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountingUserIdSupported, Tag.Boolean)]
    public IppValue<bool>? JobAccountingUserIdSupported { get; set; }

    /// <summary>
    /// job-cancel-after-default
    /// See: PWG 5100.7-2023 Section 6.9.11
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCancelAfterDefault, Tag.Integer)]
    public IppValue<int>? JobCancelAfterDefault { get; set; }

    /// <summary>
    /// job-cancel-after-supported
    /// See: PWG 5100.7-2023 Section 6.9.12
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCancelAfterSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? JobCancelAfterSupported { get; set; }

    /// <summary>
    /// job-spooling-supported
    /// See: PWG 5100.7-2023 Section 6.9.31
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSpoolingSupported, Tag.Keyword)]
    public IppValue<JobSpooling>? JobSpoolingSupported { get; set; }

    /// <summary>
    /// max-page-ranges-supported
    /// See: PWG 5100.7-2023 Section 6.9.33
    /// </summary>
    [IppAttribute(IppAttributeNames.MaxPageRangesSupported, Tag.Integer)]
    public IppValue<int>? MaxPageRangesSupported { get; set; }

    /// <summary>
    /// print-content-optimize-default
    /// See: PWG 5100.7-2023 Section 6.9.58
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintContentOptimizeDefault, Tag.Keyword)]
    public IppValue<PrintContentOptimize>? PrintContentOptimizeDefault { get; set; }

    /// <summary>
    /// print-content-optimize-supported
    /// See: PWG 5100.7-2023 Section 6.9.59
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintContentOptimizeSupported, Tag.Keyword)]
    public IppValue<PrintContentOptimize[]>? PrintContentOptimizeSupported { get; set; }

    /// <summary>
    /// output-device-supported
    /// See: PWG 5100.7-2023 Section 6.9.57
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceSupported, Tag.NameWithoutLanguage)]
    public IppValue<OutputDevice[]>? OutputDeviceSupported { get; set; }

    /// <summary>
    /// job-creation-attributes-supported
    /// See: PWG 5100.7-2023 Section 6.9.13
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCreationAttributesSupported, Tag.Keyword)]
    public IppValue<JobCreationAttribute[]>? JobCreationAttributesSupported { get; set; }

    /// <summary>
    /// printer-requested-client-type
    /// See: PWG 5100.7-2023 Section 6.9.60
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterRequestedClientType, Tag.Enum)]
    public IppValue<ClientType[]>? PrinterRequestedClientType { get; set; }

    /// <summary>
    /// printer-service-type
    /// See: PWG 5100.22-2025 Section 7.7.9
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterServiceType, Tag.Keyword)]
    public IppValue<PrinterServiceType[]>? PrinterServiceType { get; set; }

    /// <summary>
    /// finishing-template-supported
    /// See: PWG 5100.1-2022 Section 6.8
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingTemplateSupported)]
    public IppValue<FinishingTemplate[]>? FinishingTemplateSupported { get; set; }

    /// <summary>
    /// finishings-col-supported
    /// See: PWG 5100.1-2022 Section 6.12
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingsColSupported, Tag.Keyword)]
    public IppValue<FinishingsColMember[]>? FinishingsColSupported { get; set; }

    /// <summary>
    /// finishings-col-default
    /// See: PWG 5100.1-2022 Section 6.10
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingsColDefault)]
    public IppValue<FinishingsCol[]>? FinishingsColDefault { get; set; }

    /// <summary>
    /// finishings-col-ready
    /// See: PWG 5100.1-2022 Section 6.11
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingsColReady)]
    public IppValue<FinishingsCol[]>? FinishingsColReady { get; set; }

    /// <summary>
    /// job-pages-per-set-supported
    /// See: PWG 5100.1-2022 Section 6.18
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPagesPerSetSupported, Tag.Boolean)]
    public IppValue<bool>? JobPagesPerSetSupported { get; set; }

    /// <summary>
    /// punching-hole-diameter-configured
    /// See: PWG 5100.1-2022 Section 6.19
    /// </summary>
    [IppAttribute(IppAttributeNames.PunchingHoleDiameterConfigured, Tag.Integer)]
    public IppValue<int>? PunchingHoleDiameterConfigured { get; set; }

    /// <summary>
    /// baling-type-supported
    /// See: PWG 5100.1-2022 Section 6.1
    /// </summary>
    [IppAttribute(IppAttributeNames.BalingTypeSupported)]
    public IppValue<BalingType[]>? BalingTypeSupported { get; set; }

    /// <summary>
    /// baling-when-supported
    /// See: PWG 5100.1-2022 Section 6.2
    /// </summary>
    [IppAttribute(IppAttributeNames.BalingWhenSupported, Tag.Keyword)]
    public IppValue<BalingWhen[]>? BalingWhenSupported { get; set; }

    /// <summary>
    /// binding-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.3
    /// </summary>
    [IppAttribute(IppAttributeNames.BindingReferenceEdgeSupported, Tag.Keyword)]
    public IppValue<FinishingReferenceEdge[]>? BindingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// binding-type-supported
    /// See: PWG 5100.1-2022 Section 6.4
    /// </summary>
    [IppAttribute(IppAttributeNames.BindingTypeSupported)]
    public IppValue<BindingType[]>? BindingTypeSupported { get; set; }

    /// <summary>
    /// coating-sides-supported
    /// See: PWG 5100.1-2022 Section 6.5
    /// </summary>
    [IppAttribute(IppAttributeNames.CoatingSidesSupported, Tag.Keyword)]
    public IppValue<CoatingSides[]>? CoatingSidesSupported { get; set; }

    /// <summary>
    /// coating-type-supported
    /// See: PWG 5100.1-2022 Section 6.6
    /// </summary>
    [IppAttribute(IppAttributeNames.CoatingTypeSupported)]
    public IppValue<CoatingType[]>? CoatingTypeSupported { get; set; }

    /// <summary>
    /// covering-name-supported
    /// See: PWG 5100.1-2022 Section 6.7
    /// </summary>
    [IppAttribute(IppAttributeNames.CoveringNameSupported)]
    public IppValue<CoveringName[]>? CoveringNameSupported { get; set; }

    /// <summary>
    /// finishings-col-database
    /// See: PWG 5100.1-2022 Section 6.9
    /// </summary>
    [IppAttribute(IppAttributeNames.FinishingsColDatabase)]
    public IppValue<FinishingsCol[]>? FinishingsColDatabase { get; set; }

    /// <summary>
    /// folding-direction-supported
    /// See: PWG 5100.1-2022 Section 6.13
    /// </summary>
    [IppAttribute(IppAttributeNames.FoldingDirectionSupported, Tag.Keyword)]
    public IppValue<FoldingDirection[]>? FoldingDirectionSupported { get; set; }

    /// <summary>
    /// folding-offset-supported
    /// See: PWG 5100.1-2022 Section 6.14
    /// </summary>
    [IppAttribute(IppAttributeNames.FoldingOffsetSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? FoldingOffsetSupported { get; set; }

    /// <summary>
    /// folding-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.15
    /// </summary>
    [IppAttribute(IppAttributeNames.FoldingReferenceEdgeSupported, Tag.Keyword)]
    public IppValue<FinishingReferenceEdge[]>? FoldingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// laminating-sides-supported
    /// See: PWG 5100.1-2022 Section 6.16
    /// </summary>
    [IppAttribute(IppAttributeNames.LaminatingSidesSupported, Tag.Keyword)]
    public IppValue<CoatingSides[]>? LaminatingSidesSupported { get; set; }

    /// <summary>
    /// laminating-type-supported
    /// See: PWG 5100.1-2022 Section 6.17
    /// </summary>
    [IppAttribute(IppAttributeNames.LaminatingTypeSupported)]
    public IppValue<LaminatingType[]>? LaminatingTypeSupported { get; set; }

    /// <summary>
    /// punching-locations-supported
    /// See: PWG 5100.1-2022 Section 6.20
    /// </summary>
    [IppAttribute(IppAttributeNames.PunchingLocationsSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? PunchingLocationsSupported { get; set; }

    /// <summary>
    /// punching-offset-supported
    /// See: PWG 5100.1-2022 Section 6.21
    /// </summary>
    [IppAttribute(IppAttributeNames.PunchingOffsetSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? PunchingOffsetSupported { get; set; }

    /// <summary>
    /// punching-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.22
    /// </summary>
    [IppAttribute(IppAttributeNames.PunchingReferenceEdgeSupported, Tag.Keyword)]
    public IppValue<FinishingReferenceEdge[]>? PunchingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// stitching-angle-supported
    /// See: PWG 5100.1-2022 Section 6.23
    /// </summary>
    [IppAttribute(IppAttributeNames.StitchingAngleSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? StitchingAngleSupported { get; set; }

    /// <summary>
    /// stitching-locations-supported
    /// See: PWG 5100.1-2022 Section 6.24
    /// </summary>
    [IppAttribute(IppAttributeNames.StitchingLocationsSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? StitchingLocationsSupported { get; set; }

    /// <summary>
    /// stitching-method-supported
    /// See: PWG 5100.1-2022 Section 6.25
    /// </summary>
    [IppAttribute(IppAttributeNames.StitchingMethodSupported, Tag.Keyword)]
    public IppValue<StitchingMethod[]>? StitchingMethodSupported { get; set; }

    /// <summary>
    /// stitching-offset-supported
    /// See: PWG 5100.1-2022 Section 6.26
    /// </summary>
    [IppAttribute(IppAttributeNames.StitchingOffsetSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? StitchingOffsetSupported { get; set; }

    /// <summary>
    /// stitching-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.27
    /// </summary>
    [IppAttribute(IppAttributeNames.StitchingReferenceEdgeSupported, Tag.Keyword)]
    public IppValue<FinishingReferenceEdge[]>? StitchingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// trimming-offset-supported
    /// See: PWG 5100.1-2022 Section 6.28
    /// </summary>
    [IppAttribute(IppAttributeNames.TrimmingOffsetSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? TrimmingOffsetSupported { get; set; }

    /// <summary>
    /// trimming-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.29
    /// </summary>
    [IppAttribute(IppAttributeNames.TrimmingReferenceEdgeSupported, Tag.Keyword)]
    public IppValue<FinishingReferenceEdge[]>? TrimmingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// trimming-type-supported
    /// See: PWG 5100.1-2022 Section 6.30
    /// </summary>
    [IppAttribute(IppAttributeNames.TrimmingTypeSupported)]
    public IppValue<TrimmingType[]>? TrimmingTypeSupported { get; set; }

    /// <summary>
    /// trimming-when-supported
    /// See: PWG 5100.1-2022 Section 6.31
    /// </summary>
    [IppAttribute(IppAttributeNames.TrimmingWhenSupported, Tag.Keyword)]
    public IppValue<TrimmingWhen[]>? TrimmingWhenSupported { get; set; }

    /// <summary>
    /// printer-finisher
    /// See: PWG 5100.1-2022 Section 7.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFinisher, Tag.OctetStringWithAnUnspecifiedFormat)]
    public IppValue<PrinterFinisher[]>? PrinterFinisher { get; set; }

    /// <summary>
    /// printer-finisher-description
    /// See: PWG 5100.1-2022 Section 7.2
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFinisherDescription, Tag.TextWithoutLanguage)]
    public IppValue<string[]>? PrinterFinisherDescription { get; set; }

    /// <summary>
    /// printer-finisher-supplies
    /// See: PWG 5100.1-2022 Section 7.3
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFinisherSupplies, Tag.OctetStringWithAnUnspecifiedFormat)]
    public IppValue<PrinterFinisherSupply[]>? PrinterFinisherSupplies { get; set; }

    /// <summary>
    /// printer-finisher-supplies-description
    /// See: PWG 5100.1-2022 Section 7.4
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFinisherSuppliesDescription, Tag.TextWithoutLanguage)]
    public IppValue<string[]>? PrinterFinisherSuppliesDescription { get; set; }

    /// <summary>
    /// cover-back-default
    /// See: PWG 5100.3-2023 Section 5.3.1
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverBackDefault)]
    public IppValue<Cover>? CoverBackDefault { get; set; }
    /// <summary>
    /// cover-back-supported
    /// See: PWG 5100.3-2023 Section 5.3.2
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverBackSupported, Tag.Keyword)]
    public IppValue<CoverMember[]>? CoverBackSupported { get; set; }

    /// <summary>
    /// cover-front-default
    /// See: PWG 5100.3-2023 Section 5.3.3
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverFrontDefault)]
    public IppValue<Cover>? CoverFrontDefault { get; set; }

    /// <summary>
    /// cover-front-supported
    /// See: PWG 5100.3-2023 Section 5.3.4
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverFrontSupported, Tag.Keyword)]
    public IppValue<CoverMember[]>? CoverFrontSupported { get; set; }

    /// <summary>
    /// cover-type-supported
    /// See: PWG 5100.3-2023 Section 5.3.5
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverTypeSupported, Tag.Keyword)]
    public IppValue<CoverType[]>? CoverTypeSupported { get; set; }

    /// <summary>
    /// force-front-side-supported
    /// See: PWG 5100.3-2023 Section 5.3.6
    /// </summary>
    [IppAttribute(IppAttributeNames.ForceFrontSideSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? ForceFrontSideSupported { get; set; }

    /// <summary>
    /// image-orientation-default
    /// See: PWG 5100.3-2023 Section 5.3.7
    /// </summary>
    [IppAttribute(IppAttributeNames.ImageOrientationDefault, Tag.Enum)]
    public IppValue<Orientation>? ImageOrientationDefault { get; set; }

    /// <summary>
    /// image-orientation-supported
    /// See: PWG 5100.3-2023 Section 5.3.8
    /// </summary>
    [IppAttribute(IppAttributeNames.ImageOrientationSupported, Tag.Enum)]
    public IppValue<Orientation[]>? ImageOrientationSupported { get; set; }

    /// <summary>
    /// imposition-template-default
    /// See: PWG 5100.3-2023 Section 5.3.9
    /// </summary>
    [IppAttribute()]
    public IppValue<ImpositionTemplate>? ImpositionTemplateDefault { get; set; }

    /// <summary>
    /// imposition-template-supported
    /// See: PWG 5100.3-2023 Section 5.3.10
    /// </summary>
    [IppAttribute(IppAttributeNames.ImpositionTemplateSupported)]
    public IppValue<ImpositionTemplate[]>? ImpositionTemplateSupported { get; set; }

    /// <summary>
    /// insert-count-supported
    /// See: PWG 5100.3-2023 Section 5.3.11
    /// </summary>
    [IppAttribute(IppAttributeNames.InsertCountSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? InsertCountSupported { get; set; }

    /// <summary>
    /// insert-sheet-default
    /// See: PWG 5100.3-2023 Section 5.3.12
    /// </summary>
    [IppAttribute(IppAttributeNames.InsertSheetDefault)]
    public IppValue<InsertSheet[]>? InsertSheetDefault { get; set; }

    /// <summary>
    /// insert-sheet-supported
    /// See: PWG 5100.3-2023 Section 5.3.13
    /// </summary>
    [IppAttribute(IppAttributeNames.InsertSheetSupported, Tag.Keyword)]
    public IppValue<InsertSheetMember[]>? InsertSheetSupported { get; set; }

    /// <summary>
    /// job-accounting-output-bin-supported
    /// See: PWG 5100.3-2023 Section 5.3.14
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountingOutputBinSupported)]
    public IppValue<OutputBin[]>? JobAccountingOutputBinSupported { get; set; }

    /// <summary>
    /// job-accounting-sheets-default
    /// See: PWG 5100.3-2023 Section 5.3.15
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountingSheetsDefault)]
    public IppValue<JobAccountingSheets>? JobAccountingSheetsDefault { get; set; }

    /// <summary>
    /// job-accounting-sheets-supported
    /// See: PWG 5100.3-2023 Section 5.3.16
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountingSheetsSupported, Tag.Keyword)]
    public IppValue<JobAccountingSheetsMember[]>? JobAccountingSheetsSupported { get; set; }

    /// <summary>
    /// job-accounting-sheets-type-supported
    /// See: PWG 5100.3-2023 Section 5.3.17
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountingSheetsTypeSupported, Tag.Keyword)]
    public IppValue<JobAccountingSheetsType[]>? JobAccountingSheetsTypeSupported { get; set; }

    /// <summary>
    /// job-complete-before-supported
    /// See: PWG 5100.3-2023 Section 5.3.18
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCompleteBeforeSupported, Tag.Keyword)]
    public IppValue<JobCompleteBefore[]>? JobCompleteBeforeSupported { get; set; }

    /// <summary>
    /// job-complete-before-time-supported
    /// See: PWG 5100.3-2023 Section 5.3.19
    /// </summary>
    [IppAttribute(IppAttributeNames.JobCompleteBeforeTimeSupported, Tag.Boolean)]
    public IppValue<bool>? JobCompleteBeforeTimeSupported { get; set; }

    /// <summary>
    /// job-error-sheet-default
    /// See: PWG 5100.3-2023 Section 5.3.20
    /// </summary>
    [IppAttribute(IppAttributeNames.JobErrorSheetDefault)]
    public IppValue<JobErrorSheet>? JobErrorSheetDefault { get; set; }

    /// <summary>
    /// job-error-sheet-supported
    /// See: PWG 5100.3-2023 Section 5.3.21
    /// </summary>
    [IppAttribute(IppAttributeNames.JobErrorSheetSupported, Tag.Keyword)]
    public IppValue<JobErrorSheetMember[]>? JobErrorSheetSupported { get; set; }

    /// <summary>
    /// job-error-sheet-type-supported
    /// See: PWG 5100.3-2023 Section 5.3.22
    /// </summary>
    [IppAttribute(IppAttributeNames.JobErrorSheetTypeSupported, Tag.Keyword)]
    public IppValue<JobErrorSheetType[]>? JobErrorSheetTypeSupported { get; set; }

    /// <summary>
    /// job-error-sheet-when-supported
    /// See: PWG 5100.3-2023 Section 5.3.23
    /// </summary>
    [IppAttribute(IppAttributeNames.JobErrorSheetWhenSupported, Tag.Keyword)]
    public IppValue<JobErrorSheetWhen[]>? JobErrorSheetWhenSupported { get; set; }

    /// <summary>
    /// job-message-to-operator-supported
    /// See: PWG 5100.3-2023 Section 5.3.24
    /// </summary>
    [IppAttribute(IppAttributeNames.JobMessageToOperatorSupported, Tag.Boolean)]
    public IppValue<bool>? JobMessageToOperatorSupported { get; set; }

    /// <summary>
    /// job-phone-number-default
    /// See: PWG 5100.3-2023 Section 5.3.25
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPhoneNumberDefault, Tag.Keyword)]
    public IppValue<string>? JobPhoneNumberDefault { get; set; }

    /// <summary>
    /// job-phone-number-scheme-supported
    /// See: PWG 5100.3-2023 Section 5.3.26
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPhoneNumberSchemeSupported, Tag.Keyword)]
    public IppValue<JobPhoneNumberScheme[]>? JobPhoneNumberSchemeSupported { get; set; }

    /// <summary>
    /// job-phone-number-supported
    /// See: PWG 5100.3-2023 Section 5.3.27
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPhoneNumberSupported, Tag.Boolean)]
    public IppValue<bool>? JobPhoneNumberSupported { get; set; }

    /// <summary>
    /// job-recipient-name-supported
    /// See: PWG 5100.3-2023 Section 5.3.28
    /// </summary>
    [IppAttribute(IppAttributeNames.JobRecipientNameSupported, Tag.Boolean)]
    public IppValue<bool>? JobRecipientNameSupported { get; set; }

    /// <summary>
    /// job-sheet-message-supported
    /// See: PWG 5100.3-2023
    /// </summary>
    [IppAttribute(IppAttributeNames.JobSheetMessageSupported, Tag.Boolean)]
    public IppValue<bool>? JobSheetMessageSupported { get; set; }

    /// <summary>
    /// page-delivery-default
    /// See: PWG 5100.3-2023 Section 4.2 / 11.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PageDeliveryDefault, Tag.Keyword)]
    public IppValue<PageDelivery>? PageDeliveryDefault { get; set; }

    /// <summary>
    /// page-delivery-supported
    /// See: PWG 5100.3-2023 Section 4.2 / 11.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PageDeliverySupported, Tag.Keyword)]
    public IppValue<PageDelivery[]>? PageDeliverySupported { get; set; }

    /// <summary>
    /// presentation-direction-number-up-default
    /// See: PWG 5100.3-2023 Section 5.3.29
    /// </summary>
    [IppAttribute(IppAttributeNames.PresentationDirectionNumberUpDefault, Tag.Keyword)]
    public IppValue<PresentationDirectionNumberUp>? PresentationDirectionNumberUpDefault { get; set; }

    /// <summary>
    /// presentation-direction-number-up-supported
    /// See: PWG 5100.3-2023 Section 5.3.30
    /// </summary>
    [IppAttribute(IppAttributeNames.PresentationDirectionNumberUpSupported, Tag.Keyword)]
    public IppValue<PresentationDirectionNumberUp[]>? PresentationDirectionNumberUpSupported { get; set; }

    /// <summary>
    /// separator-sheets-default
    /// See: PWG 5100.3-2023 Section 5.3.31
    /// </summary>
    [IppAttribute(IppAttributeNames.SeparatorSheetsDefault)]
    public IppValue<SeparatorSheets>? SeparatorSheetsDefault { get; set; }

    /// <summary>
    /// separator-sheets-supported
    /// See: PWG 5100.3-2023 Section 5.3.32
    /// </summary>
    [IppAttribute(IppAttributeNames.SeparatorSheetsSupported, Tag.Keyword)]
    public IppValue<SeparatorSheetsMember[]>? SeparatorSheetsSupported { get; set; }

    /// <summary>
    /// separator-sheets-type-supported
    /// See: PWG 5100.3-2023 Section 5.3.33
    /// </summary>
    [IppAttribute(IppAttributeNames.SeparatorSheetsTypeSupported, Tag.Keyword)]
    public IppValue<SeparatorSheetsType[]>? SeparatorSheetsTypeSupported { get; set; }

    /// <summary>
    /// x-image-position-default
    /// See: PWG 5100.3-2023 Section 5.3.34
    /// </summary>
    [IppAttribute(IppAttributeNames.XImagePositionDefault, Tag.Keyword)]
    public IppValue<XImagePosition>? XImagePositionDefault { get; set; }

    /// <summary>
    /// x-image-position-supported
    /// See: PWG 5100.3-2023 Section 5.3.35
    /// </summary>
    [IppAttribute(IppAttributeNames.XImagePositionSupported, Tag.Keyword)]
    public IppValue<XImagePosition[]>? XImagePositionSupported { get; set; }

    /// <summary>
    /// x-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.36
    /// </summary>
    [IppAttribute(IppAttributeNames.XImageShiftDefault, Tag.Integer)]
    public IppValue<int>? XImageShiftDefault { get; set; }

    /// <summary>
    /// x-image-shift-supported
    /// See: PWG 5100.3-2023 Section 5.3.37
    /// </summary>
    [IppAttribute(IppAttributeNames.XImageShiftSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? XImageShiftSupported { get; set; }

    /// <summary>
    /// x-side1-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.38
    /// </summary>
    [IppAttribute(IppAttributeNames.XSide1ImageShiftDefault, Tag.Integer)]
    public IppValue<int>? XSide1ImageShiftDefault { get; set; }

    /// <summary>
    /// x-side2-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.39
    /// </summary>
    [IppAttribute(IppAttributeNames.XSide2ImageShiftDefault, Tag.Integer)]
    public IppValue<int>? XSide2ImageShiftDefault { get; set; }

    /// <summary>
    /// y-image-position-default
    /// See: PWG 5100.3-2023 Section 5.3.40
    /// </summary>
    [IppAttribute(IppAttributeNames.YImagePositionDefault, Tag.Keyword)]
    public IppValue<YImagePosition>? YImagePositionDefault { get; set; }

    /// <summary>
    /// y-image-position-supported
    /// See: PWG 5100.3-2023 Section 5.3.41
    /// </summary>
    [IppAttribute(IppAttributeNames.YImagePositionSupported, Tag.Keyword)]
    public IppValue<YImagePosition[]>? YImagePositionSupported { get; set; }

    /// <summary>
    /// y-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.42
    /// </summary>
    [IppAttribute(IppAttributeNames.YImageShiftDefault, Tag.Integer)]
    public IppValue<int>? YImageShiftDefault { get; set; }

    /// <summary>
    /// y-image-shift-supported
    /// See: PWG 5100.3-2023 Section 5.3.43
    /// </summary>
    [IppAttribute(IppAttributeNames.YImageShiftSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? YImageShiftSupported { get; set; }

    /// <summary>
    /// y-side1-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.44
    /// </summary>
    [IppAttribute(IppAttributeNames.YSide1ImageShiftDefault, Tag.Integer)]
    public IppValue<int>? YSide1ImageShiftDefault { get; set; }

    /// <summary>
    /// y-side2-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.45
    /// </summary>
    [IppAttribute(IppAttributeNames.YSide2ImageShiftDefault, Tag.Integer)]
    public IppValue<int>? YSide2ImageShiftDefault { get; set; }

    /// <summary>
    /// job-account-type-default
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountTypeDefault, Tag.Keyword)]
    public IppValue<JobAccountType>? JobAccountTypeDefault { get; set; }

    /// <summary>
    /// job-account-type-supported
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAccountTypeSupported, Tag.Keyword)]
    public IppValue<JobAccountType[]>? JobAccountTypeSupported { get; set; }

    /// <summary>
    /// job-password-encryption-supported
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPasswordEncryptionSupported, Tag.Keyword)]
    public IppValue<JobPasswordEncryption[]>? JobPasswordEncryptionSupported { get; set; }

    /// <summary>
    /// job-authorization-uri-supported
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.JobAuthorizationUriSupported, Tag.Boolean)]
    public IppValue<bool>? JobAuthorizationUriSupported { get; set; }

    /// <summary>
    /// printer-charge-info
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterChargeInfo, Tag.TextWithoutLanguage)]
    public IppValue<string>? PrinterChargeInfo { get; set; }

    /// <summary>
    /// printer-charge-info-uri
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterChargeInfoUri, Tag.Uri)]
    public IppValue<Uri>? PrinterChargeInfoUri { get; set; }

    /// <summary>
    /// printer-mandatory-job-attributes
    /// See: PWG 5100.11-2024
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterMandatoryJobAttributes, Tag.Keyword)]
    public IppValue<PrinterMandatoryJobAttribute[]>? PrinterMandatoryJobAttributes { get; set; }

    /// <summary>
    /// printer-requested-job-attributes
    /// See: PWG 5100.16-2020
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterRequestedJobAttributes, Tag.Keyword)]
    public IppValue<PrinterRequestedJobAttribute[]>? PrinterRequestedJobAttributes { get; set; }

    /// <summary>
    /// Structured parser model for printer-alert values.
    /// See: PWG 5100.9-2009 Section 5.2.2
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterAlert, Tag.OctetStringWithAnUnspecifiedFormat)]
    public IppValue<PrinterAlert[]>? PrinterAlert { get; set; }

    /// <summary>
    /// printer-alert-description
    /// See: PWG 5100.9-2009 Section 5.3
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterAlertDescription, Tag.TextWithoutLanguage)]
    public IppValue<string[]>? PrinterAlertDescription { get; set; }

    /// <summary>
    /// printer-supply
    /// See: PWG 5100.13-2023 Section 6.6.11
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterSupply)]
    public IppValue<PrinterSupply[]>? PrinterSupply { get; set; }

    /// <summary>
    /// printer-input-tray
    /// See: PWG 5100.13-2023 Section 6.6.9
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterInputTray)]
    public IppValue<PrinterInputTray[]>? PrinterInputTray { get; set; }

    /// <summary>
    /// printer-output-tray
    /// See: PWG 5100.13-2023 Section 6.6.10
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterOutputTray)]
    public IppValue<PrinterOutputTray[]>? PrinterOutputTray { get; set; }

    /// <summary>
    /// job-constraints-supported
    /// See: PWG 5100.13-2023 Section 6.5.5
    /// </summary>
    [IppAttribute(IppAttributeNames.JobConstraintsSupported)]
    public IppValue<JobConstraintsSupported[]>? JobConstraintsSupported { get; set; }

    /// <summary>
    /// job-presets-supported
    /// See: PWG 5100.13-2023 Section 6.5.8
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPresetsSupported)]
    public IppValue<JobPresetsSupported[]>? JobPresetsSupported { get; set; }

    /// <summary>
    /// job-resolvers-supported
    /// See: PWG 5100.13-2023 Section 6.5.9
    /// </summary>
    [IppAttribute(IppAttributeNames.JobResolversSupported)]
    public IppValue<JobResolversSupported[]>? JobResolversSupported { get; set; }

    /// <summary>
    /// job-triggers-supported
    /// See: PWG 5100.13-2023 Section 6.5.10
    /// </summary>
    [IppAttribute(IppAttributeNames.JobTriggersSupported)]
    public IppValue<JobTriggersSupported[]>? JobTriggersSupported { get; set; }

    /// <summary>
    /// print-color-mode-icc-profiles
    /// See: PWG 5100.13-2023 Section 6.5.24
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintColorModeIccProfiles)]
    public IppValue<PrintColorModeIccProfile[]>? PrintColorModeIccProfile { get; set; }

    /// <summary>
    /// printer-icc-profiles
    /// See: PWG 5100.13-2023 Section 6.5.34
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterIccProfiles)]
    public IppValue<PrinterIccProfile[]>? PrinterIccProfile { get; set; }

    /// <summary>
    /// printer-supply-description
    /// See: RFC 8011 Section 5.4.44
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterSupplyDescription, Tag.TextWithoutLanguage)]
    public IppValue<string[]>? PrinterSupplyDescription { get; set; }

    /// <summary>
    /// output-device-uuid-supported
    /// See: PWG 5100.18-2025
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputDeviceUuidSupported, Tag.Uri)]
    public IppValue<string[]>? OutputDeviceUuidSupported { get; set; }

    /// <summary>
    /// document-access-supported
    /// See: PWG 5100.18-2025 Section 7.4.1
    /// </summary>
    [IppAttribute(IppAttributeNames.DocumentAccessSupported, Tag.Keyword)]
    public IppValue<DocumentAccessMember[]>? DocumentAccessSupported { get; set; }

    /// <summary>
    /// fetch-document-attributes-supported
    /// See: PWG 5100.18-2025 Section 7.4.2
    /// </summary>
    [IppAttribute(IppAttributeNames.FetchDocumentAttributesSupported, Tag.Keyword)]
    public IppValue<FetchDocumentAttribute[]>? FetchDocumentAttributesSupported { get; set; }

    /// <summary>
    /// printer-mode-configured
    /// See: PWG 5100.18-2025 Section 7.4.4
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterModeConfigured, Tag.Keyword)]
    public IppValue<PrinterMode>? PrinterModeConfigured { get; set; }

    /// <summary>
    /// printer-mode-supported
    /// See: PWG 5100.18-2025 Section 7.4.5
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterModeSupported, Tag.Keyword)]
    public IppValue<PrinterMode[]>? PrinterModeSupported { get; set; }

    /// <summary>
    /// printer-static-resource-directory-uri
    /// See: PWG 5100.18-2025 Section 7.4.6
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStaticResourceDirectoryUri, Tag.Uri)]
    public IppValue<Uri>? PrinterStaticResourceDirectoryUri { get; set; }

    /// <summary>
    /// printer-static-resource-k-octets-supported
    /// See: PWG 5100.18-2025 Section 7.4.7
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStaticResourceKOctetsSupported, Tag.Integer)]
    public IppValue<int>? PrinterStaticResourceKOctetsSupported { get; set; }

    /// <summary>
    /// printer-static-resource-k-octets-free
    /// See: PWG 5100.18-2025 Section 7.5.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterStaticResourceKOctetsFree, Tag.Integer)]
    public IppValue<int>? PrinterStaticResourceKOctetsFree { get; set; }

    /// <summary>
    /// accuracy-units-supported
    /// See: PWG 5100.21-2019 Section 8.1
    /// </summary>
    [IppAttribute(IppAttributeNames.AccuracyUnitsSupported, Tag.Keyword)]
    public IppValue<AccuracyUnits[]>? AccuracyUnitsSupported { get; set; }

    /// <summary>
    /// chamber-humidity-default
    /// Type: integer(0:100)
    /// See: PWG 5100.21-2019 Section 8.3.2
    /// </summary>
    [IppAttribute(IppAttributeNames.ChamberHumidityDefault, Tag.Integer)]
    public IppValue<int>? ChamberHumidityDefault { get; set; }

    /// <summary>
    /// chamber-humidity-supported
    /// See: PWG 5100.21-2019 Section 8.3.3
    /// </summary>
    [IppAttribute(IppAttributeNames.ChamberHumiditySupported, Tag.Boolean)]
    public IppValue<bool>? ChamberHumiditySupported { get; set; }

    /// <summary>
    /// chamber-temperature-default
    /// Type: integer(-273:MAX)
    /// See: PWG 5100.21-2019 Section 8.3.4
    /// </summary>
    [IppAttribute(IppAttributeNames.ChamberTemperatureDefault, Tag.Integer)]
    public IppValue<int>? ChamberTemperatureDefault { get; set; }

    /// <summary>
    /// chamber-temperature-supported
    /// See: PWG 5100.21-2019 Section 8.3.5
    /// </summary>
    [IppAttribute(IppAttributeNames.ChamberTemperatureSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? ChamberTemperatureSupported { get; set; }

    /// <summary>
    /// material-amount-units-supported
    /// See: PWG 5100.21-2019 Section 8.3.6
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialAmountUnitsSupported, Tag.Keyword)]
    public IppValue<MaterialAmountUnits[]>? MaterialAmountUnitsSupported { get; set; }

    /// <summary>
    /// material-diameter-supported
    /// See: PWG 5100.21-2019 Section 8.3.7
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialDiameterSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? MaterialDiameterSupported { get; set; }

    /// <summary>
    /// material-nozzle-diameter-supported
    /// See: PWG 5100.21-2019 Section 8.3.8
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialNozzleDiameterSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? MaterialNozzleDiameterSupported { get; set; }

    /// <summary>
    /// material-purpose-supported
    /// See: PWG 5100.21-2019 Section 8.3.9
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialPurposeSupported, Tag.Keyword)]
    public IppValue<MaterialPurpose[]>? MaterialPurposeSupported { get; set; }

    /// <summary>
    /// material-rate-supported
    /// See: PWG 5100.21-2019 Section 8.3.10
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialRateSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? MaterialRateSupported { get; set; }

    /// <summary>
    /// material-rate-units-supported
    /// See: PWG 5100.21-2019 Section 8.3.11
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialRateUnitsSupported, Tag.Keyword)]
    public IppValue<MaterialRateUnits[]>? MaterialRateUnitsSupported { get; set; }

    /// <summary>
    /// material-shell-thickness-supported
    /// See: PWG 5100.21-2019 Section 8.3.12
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialShellThicknessSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? MaterialShellThicknessSupported { get; set; }

    /// <summary>
    /// material-temperature-supported
    /// See: PWG 5100.21-2019 Section 8.3.13
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialTemperatureSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? MaterialTemperatureSupported { get; set; }

    /// <summary>
    /// material-type-supported
    /// See: PWG 5100.21-2019 Section 8.3.14
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialTypeSupported, Tag.Keyword)]
    public IppValue<MaterialType[]>? MaterialTypeSupported { get; set; }

    /// <summary>
    /// materials-col-database
    /// See: PWG 5100.21-2019 Section 8.3.15
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialsColDatabase)]
    public IppValue<Material[]>? MaterialsColDatabase { get; set; }

    /// <summary>
    /// materials-col-default
    /// See: PWG 5100.21-2019 Section 8.3.16
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialsColDefault)]
    public IppValue<Material[]>? MaterialsColDefault { get; set; }

    /// <summary>
    /// materials-col-ready
    /// See: PWG 5100.21-2019 Section 8.3.17
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialsColReady)]
    public IppValue<Material[]>? MaterialsColReady { get; set; }

    /// <summary>
    /// materials-col-supported
    /// See: PWG 5100.21-2019 Section 8.3.18
    /// </summary>
    [IppAttribute(IppAttributeNames.MaterialsColSupported, Tag.Keyword)]
    public IppValue<MaterialsColMember[]>? MaterialsColSupported { get; set; }

    /// <summary>
    /// max-materials-col-supported
    /// See: PWG 5100.21-2019 Section 8.3.19
    /// </summary>
    [IppAttribute(IppAttributeNames.MaxMaterialsColSupported, Tag.Integer)]
    public IppValue<int>? MaxMaterialsColSupported { get; set; }

    /// <summary>
    /// multiple-object-handling-default
    /// See: PWG 5100.21-2019 Section 8.3.20
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleObjectHandlingDefault, Tag.Keyword)]
    public IppValue<MultipleObjectHandling>? MultipleObjectHandlingDefault { get; set; }

    /// <summary>
    /// multiple-object-handling-supported
    /// See: PWG 5100.21-2019 Section 8.3.21
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleObjectHandlingSupported, Tag.Keyword)]
    public IppValue<MultipleObjectHandling[]>? MultipleObjectHandlingSupported { get; set; }

    /// <summary>
    /// pdf-features-supported
    /// See: PWG 5100.21-2019 Section 8.3.22
    /// </summary>
    [IppAttribute(IppAttributeNames.PdfFeaturesSupported, Tag.Keyword)]
    public IppValue<PdfFeature[]>? PdfFeaturesSupported { get; set; }

    /// <summary>
    /// platform-shape
    /// See: PWG 5100.21-2019 Section 8.3.23
    /// </summary>
    [IppAttribute(IppAttributeNames.PlatformShape, Tag.Keyword)]
    public IppValue<PlatformShape>? PlatformShape { get; set; }

    /// <summary>
    /// repertoire-supported
    /// See: PWG 5101.2-2004 Section 8
    /// </summary>
    [IppAttribute(IppAttributeNames.RepertoireSupported)]
    public IppValue<Repertoire[]>? RepertoireSupported { get; set; }

    /// <summary>
    /// pwg-raster-document-resolution-supported
    /// See: PWG 5102.4-2012 Section 10.1
    /// </summary>
    [IppAttribute(IppAttributeNames.PwgRasterDocumentResolutionSupported, Tag.Resolution)]
    public IppValue<Resolution[]>? PwgRasterDocumentResolutionSupported { get; set; }

    /// <summary>
    /// pwg-raster-document-sheet-back
    /// See: PWG 5102.4-2012 Section 10.2
    /// </summary>
    [IppAttribute(IppAttributeNames.PwgRasterDocumentSheetBack, Tag.Keyword)]
    public IppValue<PwgRasterDocumentSheetBack>? PwgRasterDocumentSheetBack { get; set; }

    /// <summary>
    /// pwg-raster-document-type-supported
    /// See: PWG 5102.4-2012 Section 10.3
    /// </summary>
    [IppAttribute(IppAttributeNames.PwgRasterDocumentTypeSupported, Tag.Keyword)]
    public IppValue<string[]>? PwgRasterDocumentTypeSupported { get; set; }

    /// <summary>
    /// printer-device-id
    /// See: PWG 5107.2-2010 Section 5.2
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterDeviceId, Tag.TextWithoutLanguage)]
    public IppValue<string>? PrinterDeviceId { get; set; }

    /// <summary>
    /// platform-temperature-default
    /// Type: integer(-273:MAX)
    /// See: PWG 5100.21-2019 Section 8.3.24
    /// </summary>
    [IppAttribute(IppAttributeNames.PlatformTemperatureDefault, Tag.Integer)]
    public IppValue<int>? PlatformTemperatureDefault { get; set; }

    /// <summary>
    /// platform-temperature-supported
    /// See: PWG 5100.21-2019 Section 8.3.25
    /// </summary>
    [IppAttribute(IppAttributeNames.PlatformTemperatureSupported, Tag.RangeOfInteger)]
    public IppValue<Range[]>? PlatformTemperatureSupported { get; set; }

    /// <summary>
    /// print-accuracy-default
    /// See: PWG 5100.21-2019 Section 8.3.26
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintAccuracyDefault)]
    public IppValue<PrintAccuracy>? PrintAccuracyDefault { get; set; }

    /// <summary>
    /// print-accuracy-supported
    /// See: PWG 5100.21-2019 Section 8.3.27
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintAccuracySupported)]
    public IppValue<PrintAccuracy>? PrintAccuracySupported { get; set; }

    /// <summary>
    /// print-base-default
    /// See: PWG 5100.21-2019 Section 8.3.28
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintBaseDefault, Tag.Keyword)]
    public IppValue<PrintBase>? PrintBaseDefault { get; set; }

    /// <summary>
    /// print-base-supported
    /// See: PWG 5100.21-2019 Section 8.3.29
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintBaseSupported, Tag.Keyword)]
    public IppValue<PrintBase[]>? PrintBaseSupported { get; set; }

    /// <summary>
    /// print-objects-supported
    /// See: PWG 5100.21-2019 Section 8.3.30
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintObjectsSupported, Tag.Keyword)]
    public IppValue<PrintObjectsMember[]>? PrintObjectsSupported { get; set; }

    /// <summary>
    /// print-supports-default
    /// See: PWG 5100.21-2019 Section 8.3.31
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintSupportsDefault, Tag.Keyword)]
    public IppValue<PrintSupports>? PrintSupportsDefault { get; set; }

    /// <summary>
    /// print-supports-supported
    /// See: PWG 5100.21-2019 Section 8.3.32
    /// </summary>
    [IppAttribute(IppAttributeNames.PrintSupportsSupported, Tag.Keyword)]
    public IppValue<PrintSupports[]>? PrintSupportsSupported { get; set; }

    /// <summary>
    /// printer-volume-supported
    /// See: PWG 5100.21-2019 Section 8.3.33
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterVolumeSupported)]
    public IppValue<PrinterVolumeSupported>? PrinterVolumeSupported { get; set; }

    /// <summary>
    /// chamber-humidity-current
    /// Type: integer(0:100)
    /// See: PWG 5100.21-2019 Section 8.4.1
    /// </summary>
    [IppAttribute(IppAttributeNames.ChamberHumidityCurrent, Tag.Integer)]
    public IppValue<int>? ChamberHumidityCurrent { get; set; }

    /// <summary>
    /// chamber-temperature-current
    /// Type: integer(-273:MAX)
    /// See: PWG 5100.21-2019 Section 8.4.2
    /// </summary>
    [IppAttribute(IppAttributeNames.ChamberTemperatureCurrent, Tag.Integer)]
    public IppValue<int>? ChamberTemperatureCurrent { get; set; }

    /// <summary>
    /// printer-camera-image-uri
    /// See: PWG 5100.21-2019 Section 8.17
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterCameraImageUri, Tag.Uri)]
    public IppValue<Uri[]>? PrinterCameraImageUri { get; set; }

    /// <summary>
    /// printer-resource-ids
    /// See: PWG 5100.22-2025 Section 6.1.2
    /// </summary>
    [Range(1, int.MaxValue)]
    [IppAttribute(IppAttributeNames.PrinterResourceIds, Tag.Integer)]
    public IppValue<int[]>? PrinterResourceIds { get; set; }

    /// <summary>
    /// confirmation-sheet-print-default
    /// See: PWG 5100.15-2013 Section 7.4.1
    /// </summary>
    [IppAttribute(IppAttributeNames.ConfirmationSheetPrintDefault, Tag.Boolean)]
    public IppValue<bool>? ConfirmationSheetPrintDefault { get; set; }

    /// <summary>
    /// cover-sheet-info-default
    /// See: PWG 5100.15-2013 Section 7.4.2
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverSheetInfoDefault)]
    public IppValue<CoverSheetInfo>? CoverSheetInfoDefault { get; set; }

    /// <summary>
    /// cover-sheet-info-supported
    /// See: PWG 5100.15-2013 Section 7.4.3
    /// </summary>
    [IppAttribute(IppAttributeNames.CoverSheetInfoSupported, Tag.Keyword)]
    public IppValue<CoverSheetInfoMember[]>? CoverSheetInfoSupported { get; set; }

    /// <summary>
    /// destination-accesses-supported
    /// See: PWG 5100.17-2014 Section 8.3.1
    /// </summary>
    [IppAttribute(IppAttributeNames.DestinationAccessesSupported, Tag.Keyword)]
    public IppValue<DestinationAccessMember[]>? DestinationAccessesSupported { get; set; }

    /// <summary>
    /// destination-uri-ready
    /// See: PWG 5100.17-2014 Section 8.3.3
    /// </summary>
    [IppAttribute(IppAttributeNames.DestinationUriReady)]
    public IppValue<DestinationUriReady[]>? DestinationUriReady { get; set; }

    /// <summary>
    /// destination-uri-schemes-supported
    /// See: PWG 5100.15-2013 Section 7.4.4
    /// </summary>
    [IppAttribute(IppAttributeNames.DestinationUriSchemesSupported, Tag.UriScheme)]
    public IppValue<UriScheme[]>? DestinationUriSchemesSupported { get; set; }

    /// <summary>
    /// destination-uris-supported
    /// See: PWG 5100.15-2013 Section 7.4.5
    /// </summary>
    [IppAttribute(IppAttributeNames.DestinationUrisSupported, Tag.Keyword)]
    public IppValue<DestinationUrisMember[]>? DestinationUrisSupported { get; set; }

    /// <summary>
    /// from-name-supported
    /// See: PWG 5100.15-2013 Section 7.4.6
    /// </summary>
    [IppAttribute(IppAttributeNames.FromNameSupported, Tag.Integer)]
    public IppValue<int>? FromNameSupported { get; set; }

    /// <summary>
    /// input-attributes-default
    /// See: PWG 5100.15-2013 Section 7.4.7
    /// </summary>
    [IppAttribute(IppAttributeNames.InputAttributesDefault)]
    public IppValue<DocumentTemplateAttributes>? InputAttributesDefault { get; set; }

    /// <summary>
    /// input-attributes-supported
    /// See: PWG 5100.15-2013 Section 7.4.8
    /// </summary>
    [IppAttribute(IppAttributeNames.InputAttributesSupported, Tag.Keyword)]
    public IppValue<InputAttributesMember[]>? InputAttributesSupported { get; set; }

    /// <summary>
    /// input-color-mode-supported
    /// See: PWG 5100.15-2013 Section 7.4.9
    /// </summary>
    [IppAttribute(IppAttributeNames.InputColorModeSupported, Tag.Keyword)]
    public IppValue<InputColorMode[]>? InputColorModeSupported { get; set; }

    /// <summary>
    /// input-content-type-supported
    /// See: PWG 5100.15-2013 Section 7.4.10
    /// </summary>
    [IppAttribute(IppAttributeNames.InputContentTypeSupported, Tag.Keyword)]
    public IppValue<InputContentType[]>? InputContentTypeSupported { get; set; }

    /// <summary>
    /// input-film-scan-mode-supported
    /// See: PWG 5100.15-2013 Section 7.4.11
    /// </summary>
    [IppAttribute(IppAttributeNames.InputFilmScanModeSupported, Tag.Keyword)]
    public IppValue<InputFilmScanMode[]>? InputFilmScanModeSupported { get; set; }

    /// <summary>
    /// input-media-supported
    /// See: PWG 5100.15-2013 Section 7.4.12
    /// </summary>
    [IppAttribute(IppAttributeNames.InputMediaSupported)]
    public IppValue<Media[]>? InputMediaSupported { get; set; }

    /// <summary>
    /// input-orientation-requested-supported
    /// See: PWG 5100.15-2013 Section 7.4.13
    /// </summary>
    [IppAttribute(IppAttributeNames.InputOrientationRequestedSupported, Tag.Enum)]
    public IppValue<Orientation[]>? InputOrientationRequestedSupported { get; set; }

    /// <summary>
    /// input-quality-supported
    /// See: PWG 5100.15-2013 Section 7.4.14
    /// </summary>
    [IppAttribute(IppAttributeNames.InputQualitySupported, Tag.Enum)]
    public IppValue<PrintQuality[]>? InputQualitySupported { get; set; }

    /// <summary>
    /// input-resolution-supported
    /// See: PWG 5100.15-2013 Section 7.4.15
    /// </summary>
    [IppAttribute(IppAttributeNames.InputResolutionSupported, Tag.Resolution)]
    public IppValue<Resolution[]>? InputResolutionSupported { get; set; }

    /// <summary>
    /// input-sides-supported
    /// See: PWG 5100.15-2013 Section 7.4.17
    /// </summary>
    [IppAttribute(IppAttributeNames.InputSidesSupported, Tag.Keyword)]
    public IppValue<Sides[]>? InputSidesSupported { get; set; }

    /// <summary>
    /// input-source-supported
    /// See: PWG 5100.15-2013 Section 7.4.18
    /// </summary>
    [IppAttribute(IppAttributeNames.InputSourceSupported, Tag.Keyword)]
    public IppValue<InputSource[]>? InputSourceSupported { get; set; }

    /// <summary>
    /// logo-uri-formats-supported
    /// See: PWG 5100.15-2013 Section 7.4.19
    /// </summary>
    [IppAttribute(IppAttributeNames.LogoUriFormatsSupported, Tag.MimeMediaType)]
    public IppValue<string[]>? LogoUriFormatsSupported { get; set; }

    /// <summary>
    /// logo-uri-schemes-supported
    /// See: PWG 5100.15-2013 Section 7.4.20
    /// </summary>
    [IppAttribute(IppAttributeNames.LogoUriSchemesSupported, Tag.UriScheme)]
    public IppValue<UriScheme[]>? LogoUriSchemesSupported { get; set; }

    /// <summary>
    /// message-supported
    /// See: PWG 5100.15-2013 Section 7.4.21
    /// </summary>
    [IppAttribute(IppAttributeNames.MessageSupported, Tag.Integer)]
    public IppValue<int>? MessageSupported { get; set; }

    /// <summary>
    /// multiple-destination-uris-supported
    /// See: PWG 5100.15-2013 Section 7.4.22
    /// </summary>
    [IppAttribute(IppAttributeNames.MultipleDestinationUrisSupported, Tag.Boolean)]
    public IppValue<bool>? MultipleDestinationUrisSupported { get; set; }

    /// <summary>
    /// number-of-retries-default
    /// See: PWG 5100.15-2013 Section 7.4.23
    /// </summary>
    [IppAttribute(IppAttributeNames.NumberOfRetriesDefault, Tag.Integer)]
    public IppValue<int>? NumberOfRetriesDefault { get; set; }

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
    public IppValue<int>? OrganizationNameSupported { get; set; }

    /// <summary>
    /// job-destination-spooling-supported
    /// See: PWG 5100.17-2014 Section 8.3.4
    /// </summary>
    [IppAttribute(IppAttributeNames.JobDestinationSpoolingSupported, Tag.Keyword)]
    public IppValue<JobSpooling>? JobDestinationSpoolingSupported { get; set; }

    /// <summary>
    /// output-attributes-default
    /// See: PWG 5100.17-2014 Section 8.3.5
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputAttributesDefault)]
    public IppValue<OutputAttributes>? OutputAttributesDefault { get; set; }

    /// <summary>
    /// output-attributes-supported
    /// See: PWG 5100.17-2014 Section 8.3.6
    /// </summary>
    [IppAttribute(IppAttributeNames.OutputAttributesSupported, Tag.Keyword)]
    public IppValue<OutputAttributesMember[]>? OutputAttributesSupported { get; set; }

    /// <summary>
    /// printer-fax-log-uri
    /// See: PWG 5100.15-2013 Section 7.4.26
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFaxLogUri, Tag.Uri)]
    public IppValue<Uri>? PrinterFaxLogUri { get; set; }

    /// <summary>
    /// printer-fax-modem-info
    /// See: PWG 5100.15-2013 Section 7.4.27
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFaxModemInfo, Tag.TextWithoutLanguage)]
    public IppValue<string[]>? PrinterFaxModemInfo { get; set; }

    /// <summary>
    /// printer-fax-modem-name
    /// See: PWG 5100.15-2013 Section 7.4.28
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFaxModemName, Tag.NameWithoutLanguage)]
    public IppValue<string[]>? PrinterFaxModemName { get; set; }

    /// <summary>
    /// printer-fax-modem-number
    /// See: PWG 5100.15-2013 Section 7.4.29
    /// </summary>
    [IppAttribute(IppAttributeNames.PrinterFaxModemNumber, Tag.Uri)]
    public IppValue<Uri[]>? PrinterFaxModemNumber { get; set; }

    /// <summary>
    /// retry-interval-default
    /// Type: integer(1:MAX)
    /// See: PWG 5100.15-2013 Section 7.4.30
    /// </summary>
    [IppAttribute(IppAttributeNames.RetryIntervalDefault, Tag.Integer)]
    public IppValue<int>? RetryIntervalDefault { get; set; }

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
    public IppValue<int>? RetryTimeOutDefault { get; set; }

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
    public IppValue<int>? SubjectSupported { get; set; }

    /// <summary>
    /// to-name-supported
    /// See: PWG 5100.15-2013 Section 7.4.35
    /// </summary>
    [IppAttribute(IppAttributeNames.ToNameSupported, Tag.Integer)]
    public IppValue<int>? ToNameSupported { get; set; }

    /// <summary>
    /// jpeg-x-dimension-supported
    /// Type: rangeOfInteger(0:65535)
    /// See: RFC 8011 Section 5.4.38
    /// </summary>
    [IppAttribute(IppAttributeNames.JpegXDimensionSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? JpegXDimensionSupported { get; set; }

    /// <summary>
    /// jpeg-y-dimension-supported
    /// Type: rangeOfInteger(1:65535)
    /// See: RFC 8011 Section 5.4.39
    /// </summary>
    [IppAttribute(IppAttributeNames.JpegYDimensionSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? JpegYDimensionSupported { get; set; }

    /// <summary>
    /// job-password-supported
    /// Type: integer(0:255)
    /// See: PWG 5100.11-2024 Section 7.2.1
    /// </summary>
    [Range(0, 255)]
    [IppAttribute(IppAttributeNames.JobPasswordSupported, Tag.Integer)]
    public IppValue<int>? JobPasswordSupported { get; set; }

    /// <summary>
    /// job-password-length-supported
    /// Type: rangeOfInteger(4:1020)
    /// See: PWG 5100.11-2024 Section 7.2.2
    /// </summary>
    [IppAttribute(IppAttributeNames.JobPasswordLengthSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? JobPasswordLengthSupported { get; set; }

    /// <summary>
    /// document-password-supported
    /// Type: integer(0:1023) (Valid: 0 or 255-1023)
    /// See: PWG 5100.11-2024 Section 7.2.4
    /// </summary>
    [Range(0, 0, 255, 1023)]
    [IppAttribute(IppAttributeNames.DocumentPasswordSupported, Tag.Integer)]
    public IppValue<int>? DocumentPasswordSupported { get; set; }

    /// <summary>
    /// x-side1-image-offset-supported
    /// See: PWG 5100.3-2023
    /// </summary>
    [Obsolete("The 'x-side1-image-offset-supported' attribute is obsolete. See PWG 5100.3-2023 Section 12.")]
    [IppAttribute(IppAttributeNames.XSide1ImageOffsetSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? XSide1ImageOffsetSupported { get; set; }

    /// <summary>
    /// x-side2-image-offset-supported
    /// See: PWG 5100.3-2023
    /// </summary>
    [Obsolete("The 'x-side2-image-offset-supported' attribute is obsolete. See PWG 5100.3-2023 Section 12.")]
    [IppAttribute(IppAttributeNames.XSide2ImageOffsetSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? XSide2ImageOffsetSupported { get; set; }

    /// <summary>
    /// y-side1-image-offset-supported
    /// See: PWG 5100.3-2023
    /// </summary>
    [Obsolete("The 'y-side1-image-offset-supported' attribute is obsolete. See PWG 5100.3-2023 Section 12.")]
    [IppAttribute(IppAttributeNames.YSide1ImageOffsetSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? YSide1ImageOffsetSupported { get; set; }

    /// <summary>
    /// y-side2-image-offset-supported
    /// See: PWG 5100.3-2023
    /// </summary>
    [Obsolete("The 'y-side2-image-offset-supported' attribute is obsolete. See PWG 5100.3-2023 Section 12.")]
    [IppAttribute(IppAttributeNames.YSide2ImageOffsetSupported, Tag.RangeOfInteger)]
    public IppValue<Range>? YSide2ImageOffsetSupported { get; set; }

    /// <summary>
    /// user-defined-values-supported
    /// See: PWG 5100.3-2023
    /// </summary>
    [Obsolete("The 'user-defined-values-supported' attribute is obsolete. See PWG 5100.3-2023 Section 12.")]
    [IppAttribute(IppAttributeNames.UserDefinedValuesSupported, Tag.Keyword)]
    public IppValue<string[]>? UserDefinedValuesSupported { get; set; }

    /// <summary>
    /// pdl-init-file-supported
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'pdl-init-file-supported' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.PdlInitFileSupported, Tag.Keyword)]
    public IppValue<string[]>? PdlInitFileSupported { get; set; }

    /// <summary>
    /// pdl-init-file-default
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'pdl-init-file-default' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.PdlInitFileDefault)]
    public IppValue<PdlInitFile>? PdlInitFileDefault { get; set; }

    /// <summary>
    /// job-save-disposition-supported
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'job-save-disposition-supported' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.JobSaveDispositionSupported, Tag.Keyword)]
    public IppValue<string[]>? JobSaveDispositionSupported { get; set; }

    /// <summary>
    /// job-save-disposition-default
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'job-save-disposition-default' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.JobSaveDispositionDefault)]
    public IppValue<JobSaveDisposition>? JobSaveDispositionDefault { get; set; }

    /// <summary>
    /// save-disposition-supported
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'save-disposition-supported' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.SaveDispositionSupported, Tag.Keyword)]
    public IppValue<SaveDisposition[]>? SaveDispositionSupported { get; set; }

    /// <summary>
    /// save-info-supported
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'save-info-supported' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.SaveInfoSupported, Tag.Keyword)]
    public IppValue<string[]>? SaveInfoSupported { get; set; }

    /// <summary>
    /// save-location-supported
    /// See: PWG 5100.11
    /// </summary>
    [Obsolete("The 'save-location-supported' attribute is obsolete. See PWG 5100.11-2024 Section 9.1.")]
    [IppAttribute(IppAttributeNames.SaveLocationSupported, Tag.Uri)]
    public IppValue<Uri[]>? SaveLocationSupported { get; set; }

    /// <summary>
    /// The pages-per-subset-supported Printer Description attribute.
    /// See: PWG 5100.8-2003 Section 4.2
    /// </summary>
    [Obsolete("The 'pages-per-subset-supported' attribute is obsolete. See PWG 5100.13-2023 Section 7.1.")]
    [IppAttribute(IppAttributeNames.PagesPerSubsetSupported, Tag.Boolean)]
    public IppValue<bool>? PagesPerSubsetSupported { get; set; }
}
