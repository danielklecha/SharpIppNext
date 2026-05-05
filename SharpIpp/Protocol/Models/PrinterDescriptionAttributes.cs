using System;

namespace SharpIpp.Protocol.Models;

public class PrinterDescriptionAttributes
{
    /// <summary>
    /// printer-uri-supported
    /// See: RFC 8011 Section 5.4.1
    /// </summary>
    public Uri[]? PrinterUriSupported { get; set; }

    /// <summary>
    /// uri-security-supported
    /// See: RFC 8011 Section 5.4.3
    /// </summary>
    public UriSecurity[]? UriSecuritySupported { get; set; }

    /// <summary>
    /// uri-authentication-supported
    /// See: RFC 8011 Section 5.4.2
    /// </summary>
    public UriAuthentication[]? UriAuthenticationSupported { get; set; }

    /// <summary>
    /// printer-name
    /// See: RFC 8011 Section 5.4.4
    /// </summary>
    public string? PrinterName { get; set; }

    /// <summary>
    /// printer-location
    /// See: RFC 8011 Section 5.4.5
    /// </summary>
    public string? PrinterLocation { get; set; }

    /// <summary>
    /// printer-info
    /// See: RFC 8011 Section 5.4.6
    /// </summary>
    public string? PrinterInfo { get; set; }

    /// <summary>
    /// printer-more-info
    /// See: RFC 8011 Section 5.4.7
    /// </summary>
    public string? PrinterMoreInfo { get; set; }

    /// <summary>
    /// printer-driver-installer
    /// See: RFC 8011 Section 5.4.8
    /// </summary>
    public string? PrinterDriverInstaller { get; set; }

    /// <summary>
    /// printer-make-and-model
    /// See: RFC 8011 Section 5.4.9
    /// </summary>
    public string? PrinterMakeAndModel { get; set; }

    /// <summary>
    /// printer-more-info-manufacturer
    /// See: RFC 8011 Section 5.4.10
    /// </summary>
    public string? PrinterMoreInfoManufacturer { get; set; }

    /// <summary>
    /// printer-state
    /// See: RFC 8011 Section 5.4.11
    /// </summary>
    public PrinterState? PrinterState { get; set; }

    /// <summary>
    /// printer-state-reasons
    /// See: RFC 8011 Section 5.4.12
    /// </summary>
    public PrinterStateReason[]? PrinterStateReasons { get; set; }

    /// <summary>
    /// printer-state-message
    /// See: RFC 8011 Section 5.4.13
    /// </summary>
    public string? PrinterStateMessage { get; set; }

    /// <summary>
    /// printer-state-change-time
    /// See: RFC 8011 Section 5.4.14
    /// </summary>
    public int? PrinterStateChangeTime { get; set; }

    /// <summary>
    /// printer-state-change-date-time
    /// See: RFC 8011 Section 5.4.15
    /// </summary>
    public DateTimeOffset? PrinterStateChangeDateTime { get; set; }

    /// <summary>
    /// printer-detailed-status-messages
    /// See: PWG 5100.7-2023 Section 6.10.1
    /// </summary>
    public string[]? PrinterDetailedStatusMessages { get; set; }

    /// <summary>
    /// ipp-versions-supported
    /// See: RFC 8011 Section 5.4.16
    /// </summary>
    public IppVersion[]? IppVersionsSupported { get; set; }

    /// <summary>
    /// operations-supported
    /// See: RFC 8011 Section 5.4.17
    /// </summary>
    public IppOperation[]? OperationsSupported { get; set; }

    /// <summary>
    /// multiple-document-jobs-supported
    /// See: RFC 8011 Section 5.4.16
    /// </summary>
    public bool? MultipleDocumentJobsSupported { get; set; }

    /// <summary>
    /// multiple-document-handling-default
    /// See: RFC 2911 Section 4.2.4
    /// </summary>
    public MultipleDocumentHandling? MultipleDocumentHandlingDefault { get; set; }

    /// <summary>
    /// multiple-document-handling-supported
    /// See: RFC 2911 Section 4.2.4
    /// </summary>
    public MultipleDocumentHandling[]? MultipleDocumentHandlingSupported { get; set; }

    /// <summary>
    /// charset-configured
    /// See: RFC 8011 Section 5.4.17
    /// </summary>
    public string? CharsetConfigured { get; set; }

    /// <summary>
    /// charset-supported
    /// See: RFC 8011 Section 5.4.18
    /// </summary>
    public string[]? CharsetSupported { get; set; }

    /// <summary>
    /// natural-language-configured
    /// See: RFC 8011 Section 5.4.19
    /// </summary>
    public string? NaturalLanguageConfigured { get; set; }

    /// <summary>
    /// generated-natural-language-supported
    /// See: RFC 8011 Section 5.4.20
    /// </summary>
    public string[]? GeneratedNaturalLanguageSupported { get; set; }

    /// <summary>
    /// client-info-supported
    /// See: PWG 5100.7-2023 Section 6.9.5
    /// </summary>
    public string[]? ClientInfoSupported { get; set; }

    /// <summary>
    /// max-client-info-supported
    /// See: PWG 5100.7-2023 Section 6.9.41
    /// </summary>
    public int? MaxClientInfoSupported { get; set; }

    /// <summary>
    /// document-charset-default
    /// See: PWG 5100.7-2023 Section 6.9.16
    /// </summary>
    public string? DocumentCharsetDefault { get; set; }

    /// <summary>
    /// document-charset-supported
    /// See: PWG 5100.7-2023 Section 6.9.17
    /// </summary>
    public string[]? DocumentCharsetSupported { get; set; }

    /// <summary>
    /// document-format-details-supported
    /// See: PWG 5100.7-2023 Section 6.9.20
    /// </summary>
    public string[]? DocumentFormatDetailsSupported { get; set; }

    /// <summary>
    /// document-natural-language-default
    /// See: PWG 5100.7-2023 Section 6.9.48
    /// </summary>
    public string? DocumentNaturalLanguageDefault { get; set; }

    /// <summary>
    /// document-natural-language-supported
    /// See: PWG 5100.7-2023 Section 6.9.49
    /// </summary>
    public string[]? DocumentNaturalLanguageSupported { get; set; }

    /// <summary>
    /// job-ids-supported
    /// See: PWG 5100.7-2023 Section 6.9.26
    /// </summary>
    public bool? JobIdsSupported { get; set; }

    /// <summary>
    /// job-mandatory-attributes-supported
    /// See: PWG 5100.7-2023 Section 6.9.28
    /// </summary>
    public bool? JobMandatoryAttributesSupported { get; set; }

    /// <summary>
    /// job-sheets-col-default
    /// See: PWG 5100.7-2023 Section 6.9.29
    /// </summary>
    public JobSheetsCol? JobSheetsColDefault { get; set; }

    /// <summary>
    /// job-sheets-col-supported
    /// See: PWG 5100.7-2023 Section 6.9.30
    /// </summary>
    public JobSheetsColMember[]? JobSheetsColSupported { get; set; }

    /// <summary>
    /// document-format-default
    /// See: RFC 8011 Section 5.4.21
    /// </summary>
    public string? DocumentFormatDefault { get; set; }

    /// <summary>
    /// document-format-supported
    /// See: RFC 8011 Section 5.4.22
    /// </summary>
    public string[]? DocumentFormatSupported { get; set; }

    /// <summary>
    /// printer-is-accepting-jobs
    /// See: RFC 8011 Section 5.4.23
    /// </summary>
    public bool? PrinterIsAcceptingJobs { get; set; }

    /// <summary>
    /// queued-job-count
    /// See: RFC 8011 Section 5.4.24
    /// </summary>
    public int? QueuedJobCount { get; set; }

    /// <summary>
    /// printer-message-from-operator
    /// See: RFC 8011 Section 5.4.25
    /// </summary>
    public string? PrinterMessageFromOperator { get; set; }

    /// <summary>
    /// color-supported
    /// See: RFC 8011 Section 5.4.26
    /// </summary>
    public bool? ColorSupported { get; set; }

    /// <summary>
    /// reference-uri-schemes-supported
    /// See: RFC 8011 Section 5.4.27
    /// </summary>
    public UriScheme[]? ReferenceUriSchemesSupported { get; set; }

    /// <summary>
    /// pdl-override-supported
    /// See: RFC 8011 Section 5.4.28
    /// </summary>
    public PdlOverride? PdlOverrideSupported { get; set; }

    /// <summary>
    /// overrides-supported
    /// See: PWG 5100.6-2003 Section 4.1.7
    /// </summary>
    public OverrideSupported[]? OverridesSupported { get; set; }

    /// <summary>
    /// printer-up-time
    /// See: RFC 8011 Section 5.4.29
    /// </summary>
    public int? PrinterUpTime { get; set; }

    /// <summary>
    /// printer-current-time
    /// See: RFC 8011 Section 5.4.30
    /// </summary>
    public DateTimeOffset? PrinterCurrentTime { get; set; }

    /// <summary>
    /// printer-config-change-time
    /// See: RFC 8011 Section 5.4.31
    /// </summary>
    public int? PrinterConfigChangeTime { get; set; }

    /// <summary>
    /// printer-config-change-date-time
    /// See: RFC 8011 Section 5.4.31
    /// </summary>
    public DateTimeOffset? PrinterConfigChangeDateTime { get; set; }

    /// <summary>
    /// printer-config-changes
    /// See: PWG 5100.22-2025 Section 7.7.1
    /// </summary>
    public int? PrinterConfigChanges { get; set; }

    /// <summary>
    /// printer-contact-col
    /// See: PWG 5100.22-2025 Section 7.6.1
    /// </summary>
    public SystemContact[]? PrinterContactCol { get; set; }

    /// <summary>
    /// printer-geo-location
    /// See: PWG 5100.22-2025 Section 7.1.7
    /// </summary>
    public Uri? PrinterGeoLocation { get; set; }

    /// <summary>
    /// printer-ids
    /// See: PWG 5100.22-2025 Section 7.1.6
    /// </summary>
    public int[]? PrinterIds { get; set; }

    /// <summary>
    /// printer-impressions-completed
    /// See: PWG 5100.22-2025 Section 7.7.3
    /// </summary>
    public int? PrinterImpressionsCompleted { get; set; }

    /// <summary>
    /// printer-impressions-completed-col
    /// See: PWG 5100.22-2025 Section 7.7.4
    /// </summary>
    public int? PrinterImpressionsCompletedCol { get; set; }

    /// <summary>
    /// printer-media-sheets-completed
    /// See: PWG 5100.22-2025 Section 7.7.5
    /// </summary>
    public int? PrinterMediaSheetsCompleted { get; set; }

    /// <summary>
    /// printer-media-sheets-completed-col
    /// See: PWG 5100.22-2025 Section 7.7.6
    /// </summary>
    public int? PrinterMediaSheetsCompletedCol { get; set; }

    /// <summary>
    /// printer-pages-completed
    /// See: PWG 5100.22-2025 Section 7.7.7
    /// </summary>
    public int? PrinterPagesCompleted { get; set; }

    /// <summary>
    /// printer-pages-completed-col
    /// See: PWG 5100.22-2025 Section 7.7.8
    /// </summary>
    public int? PrinterPagesCompletedCol { get; set; }

    /// <summary>
    /// multiple-operation-time-out
    /// See: RFC 8011 Section 5.4.31
    /// </summary>
    public int? MultipleOperationTimeOut { get; set; }

    /// <summary>
    /// compression-supported
    /// See: RFC 8011 Section 5.4.32
    /// </summary>
    public Compression[]? CompressionSupported { get; set; }

    /// <summary>
    /// compression-default
    /// See: RFC 8011 Section 5.4.32
    /// </summary>
    public Compression? CompressionDefault { get; set; }

    /// <summary>
    /// job-k-octets-supported
    /// See: RFC 8011 Section 5.4.33
    /// </summary>
    public Range? JobKOctetsSupported { get; set; }

    /// <summary>
    /// jpeg-k-octets-supported
    /// See: PWG 5100.13-2023 Section 6.5.12
    /// </summary>
    public Range? JpegKOctetsSupported { get; set; }

    /// <summary>
    /// pdf-k-octets-supported
    /// See: PWG 5100.13-2023 Section 6.5.20
    /// </summary>
    public Range? PdfKOctetsSupported { get; set; }

    /// <summary>
    /// job-impressions-supported
    /// See: RFC 8011 Section 5.4.34
    /// </summary>
    public Range? JobImpressionsSupported { get; set; }

    /// <summary>
    /// job-media-sheets-supported
    /// See: RFC 8011 Section 5.4.35
    /// </summary>
    public Range? JobMediaSheetsSupported { get; set; }

    /// <summary>
    /// job-sheets-default
    /// See: RFC 2911 Section 4.2.3
    /// </summary>
    public JobSheets? JobSheetsDefault { get; set; }

    /// <summary>
    /// job-sheets-supported
    /// See: RFC 2911 Section 4.2.3
    /// </summary>
    public JobSheets[]? JobSheetsSupported { get; set; }

    /// <summary>
    /// number-up-default
    /// See: RFC 2911 Section 4.2.7
    /// </summary>
    public int? NumberUpDefault { get; set; }

    /// <summary>
    /// number-up-supported
    /// See: RFC 2911 Section 4.2.7
    /// </summary>
    public Range[]? NumberUpSupported { get; set; }

    /// <summary>
    /// pages-per-minute
    /// See: RFC 8011 Section 5.4.36
    /// </summary>
    public int? PagesPerMinute { get; set; }

    /// <summary>
    /// pages-per-minute-color
    /// See: RFC 8011 Section 5.4.37
    /// </summary>
    public int? PagesPerMinuteColor { get; set; }

    /// <summary>
    /// print-scaling-default
    /// See: PWG 5100.13-2023 Section 6.5.29
    /// </summary>
    public PrintScaling? PrintScalingDefault { get; set; }

    /// <summary>
    /// print-scaling-supported
    /// See: PWG 5100.13-2023 Section 6.5.30
    /// </summary>
    public PrintScaling[]? PrintScalingSupported { get; set; }

    /// <summary>
    /// media-default
    /// See: RFC 8011 Section 5.2.11
    /// </summary>
    public Media? MediaDefault { get; set; }

    /// <summary>
    /// media-supported
    /// See: RFC 8011 Section 5.2.11
    /// </summary>
    public Media[]? MediaSupported { get; set; }

    /// <summary>
    /// media-ready
    /// See: RFC 2911 Section 4.2.11
    /// </summary>
    public Media[]? MediaReady { get; set; }

    /// <summary>
    /// sides-default
    /// See: RFC 8011 Section 5.2.8
    /// </summary>
    public Sides? SidesDefault { get; set; }

    /// <summary>
    /// sides-supported
    /// See: RFC 8011 Section 5.2.8
    /// </summary>
    public Sides[]? SidesSupported { get; set; }

    /// <summary>
    /// finishings-default
    /// See: RFC 8011 Section 5.2.6
    /// </summary>
    public Finishings? FinishingsDefault { get; set; }

    /// <summary>
    /// finishings-supported
    /// See: RFC 8011 Section 5.2.6
    /// </summary>
    public Finishings[]? FinishingsSupported { get; set; }

    /// <summary>
    /// printer-resolution-default
    /// See: RFC 8011 Section 5.2.12
    /// </summary>
    public Resolution? PrinterResolutionDefault { get; set; }

    /// <summary>
    /// printer-resolution-supported
    /// See: RFC 8011 Section 5.2.12
    /// </summary>
    public Resolution[]? PrinterResolutionSupported { get; set; }

    /// <summary>
    /// print-quality-default
    /// See: RFC 8011 Section 5.2.13
    /// </summary>
    public PrintQuality? PrintQualityDefault { get; set; }

    /// <summary>
    /// print-quality-supported
    /// See: RFC 8011 Section 5.2.13
    /// </summary>
    public PrintQuality[]? PrintQualitySupported { get; set; }

    /// <summary>
    /// job-priority-default
    /// See: RFC 8011 Section 5.2.1
    /// </summary>
    public int? JobPriorityDefault { get; set; }

    /// <summary>
    /// job-priority-supported
    /// See: RFC 8011 Section 5.2.1
    /// </summary>
    public int? JobPrioritySupported { get; set; }

    /// <summary>
    /// copies-default
    /// See: RFC 8011 Section 5.2.5
    /// </summary>
    public int? CopiesDefault { get; set; }

    /// <summary>
    /// copies-supported
    /// See: RFC 8011 Section 5.2.5
    /// </summary>
    public Range? CopiesSupported { get; set; }

    /// <summary>
    /// orientation-requested-default
    /// See: RFC 8011 Section 5.2.10
    /// </summary>
    public Orientation? OrientationRequestedDefault { get; set; }

    /// <summary>
    /// orientation-requested-supported
    /// See: RFC 8011 Section 5.2.10
    /// </summary>
    public Orientation[]? OrientationRequestedSupported { get; set; }

    /// <summary>
    /// page-ranges-supported
    /// See: RFC 8011 Section 5.2.7
    /// </summary>
    public bool? PageRangesSupported { get; set; }

    /// <summary>
    /// job-hold-until-supported
    /// See: RFC 8011 Section 5.2.2
    /// </summary>
    public JobHoldUntil[]? JobHoldUntilSupported { get; set; }

    /// <summary>
    /// job-hold-until-default
    /// See: RFC 8011 Section 5.2.2
    /// </summary>
    public JobHoldUntil? JobHoldUntilDefault { get; set; }

    /// <summary>
    /// job-hold-until-time-supported
    /// See: PWG 5100.7-2023 Section 6.9.21
    /// </summary>
    public bool? JobHoldUntilTimeSupported { get; set; }

    /// <summary>
    /// job-delay-output-until-default
    /// See: PWG 5100.7-2023 Section 6.9.14
    /// </summary>
    public JobHoldUntil? JobDelayOutputUntilDefault { get; set; }

    /// <summary>
    /// job-delay-output-until-supported
    /// See: PWG 5100.7-2023 Section 6.9.15
    /// </summary>
    public JobHoldUntil[]? JobDelayOutputUntilSupported { get; set; }

    /// <summary>
    /// job-delay-output-until-time-supported
    /// See: PWG 5100.7-2023 Section 6.9.16
    /// </summary>
    public Range? JobDelayOutputUntilTimeSupported { get; set; }

    /// <summary>
    /// job-history-attributes-configured
    /// See: PWG 5100.7-2023 Section 6.9.17
    /// </summary>
    public string[]? JobHistoryAttributesConfigured { get; set; }

    /// <summary>
    /// job-history-attributes-supported
    /// See: PWG 5100.7-2023 Section 6.9.18
    /// </summary>
    public string[]? JobHistoryAttributesSupported { get; set; }

    /// <summary>
    /// job-history-interval-configured
    /// See: PWG 5100.7-2023 Section 6.9.19
    /// </summary>
    public int? JobHistoryIntervalConfigured { get; set; }

    /// <summary>
    /// job-history-interval-supported
    /// See: PWG 5100.7-2023 Section 6.9.20
    /// </summary>
    public Range? JobHistoryIntervalSupported { get; set; }

    /// <summary>
    /// job-retain-until-default
    /// See: PWG 5100.7-2023 Section 6.9.24
    /// </summary>
    public JobHoldUntil? JobRetainUntilDefault { get; set; }

    /// <summary>
    /// job-retain-until-interval-default
    /// See: PWG 5100.7-2023 Section 6.9.25
    /// </summary>
    public int? JobRetainUntilIntervalDefault { get; set; }

    /// <summary>
    /// job-retain-until-interval-supported
    /// See: PWG 5100.7-2023 Section 6.9.26
    /// </summary>
    public Range? JobRetainUntilIntervalSupported { get; set; }

    /// <summary>
    /// job-retain-until-supported
    /// See: PWG 5100.7-2023 Section 6.9.27
    /// </summary>
    public JobHoldUntil[]? JobRetainUntilSupported { get; set; }

    /// <summary>
    /// job-retain-until-time-supported
    /// See: PWG 5100.7-2023 Section 6.9.28
    /// </summary>
    public bool? JobRetainUntilTimeSupported { get; set; }

    /// <summary>
    /// output-bin-default
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public OutputBin? OutputBinDefault { get; set; }

    /// <summary>
    /// output-bin-supported
    /// See: PWG 5100.2-2001 Section 2.1
    /// </summary>
    public OutputBin[]? OutputBinSupported { get; set; }

    /// <summary>
    /// media-col-default
    /// See: PWG 5100.7-2023
    /// </summary>
    public MediaCol? MediaColDefault { get; set; }

    /// <summary>
    /// media-col-database
    /// See: PWG 5100.7-2023 Section 6.9.36
    /// </summary>
    public MediaCol[]? MediaColDatabase { get; set; }

    /// <summary>
    /// media-col-ready
    /// See: PWG 5100.7-2023 Section 6.9.38
    /// </summary>
    public MediaCol[]? MediaColReady { get; set; }

    /// <summary>
    /// media-col-supported
    /// See: PWG 5100.7-2023 Section 6.9.39
    /// </summary>
    public MediaColMember[]? MediaColSupported { get; set; }

    /// <summary>
    /// media-size-supported
    /// See: PWG 5100.7-2023 Section 6.9.50
    /// </summary>
    public MediaSizeSupported[]? MediaSizeSupported { get; set; }

    /// <summary>
    /// media-key-supported
    /// See: PWG 5100.7-2023 Section 6.9.44
    /// </summary>
    public MediaKey[]? MediaKeySupported { get; set; }

    /// <summary>
    /// media-source-supported
    /// See: PWG 5100.7-2023 Section 6.9.51
    /// </summary>
    public MediaSource[]? MediaSourceSupported { get; set; }

    /// <summary>
    /// media-type-supported
    /// See: PWG 5100.7-2023 Section 6.9.55
    /// </summary>
    public MediaType[]? MediaTypeSupported { get; set; }

    /// <summary>
    /// media-back-coating-supported
    /// See: PWG 5100.7-2023 Section 6.9.34
    /// </summary>
    public MediaCoating[]? MediaBackCoatingSupported { get; set; }

    /// <summary>
    /// media-front-coating-supported
    /// See: PWG 5100.7-2023 Section 6.9.41
    /// </summary>
    public MediaCoating[]? MediaFrontCoatingSupported { get; set; }

    /// <summary>
    /// media-color-supported
    /// See: PWG 5100.7-2023 Section 6.9.40
    /// </summary>
    public MediaColor[]? MediaColorSupported { get; set; }

    /// <summary>
    /// media-grain-supported
    /// See: PWG 5100.7-2023 Section 6.9.42
    /// </summary>
    public MediaGrain[]? MediaGrainSupported { get; set; }

    /// <summary>
    /// media-tooth-supported
    /// See: PWG 5100.7-2023 Section 6.9.53
    /// </summary>
    public MediaTooth[]? MediaToothSupported { get; set; }

    /// <summary>
    /// media-pre-printed-supported
    /// See: PWG 5100.7-2023 Section 6.9.47
    /// </summary>
    public MediaPrePrinted[]? MediaPrePrintedSupported { get; set; }

    /// <summary>
    /// media-recycled-supported
    /// See: PWG 5100.7-2023 Section 6.9.48
    /// </summary>
    public MediaRecycled[]? MediaRecycledSupported { get; set; }

    /// <summary>
    /// media-hole-count-supported
    /// See: PWG 5100.7-2023 Section 6.9.43
    /// </summary>
    public Range[]? MediaHoleCountSupported { get; set; }

    /// <summary>
    /// media-order-count-supported
    /// See: PWG 5100.7-2023 Section 6.9.46
    /// </summary>
    public Range[]? MediaOrderCountSupported { get; set; }

    /// <summary>
    /// media-thickness-supported
    /// See: PWG 5100.7-2023 Section 6.9.52
    /// </summary>
    public Range[]? MediaThicknessSupported { get; set; }

    /// <summary>
    /// media-weight-metric-supported
    /// See: PWG 5100.7-2023 Section 6.9.56
    /// </summary>
    public Range[]? MediaWeightMetricSupported { get; set; }

    /// <summary>
    /// media-bottom-margin-supported
    /// See: PWG 5100.7-2023 Section 6.9.35
    /// </summary>
    public int[]? MediaBottomMarginSupported { get; set; }

    /// <summary>
    /// media-left-margin-supported
    /// See: PWG 5100.7-2023 Section 6.9.45
    /// </summary>
    public int[]? MediaLeftMarginSupported { get; set; }

    /// <summary>
    /// media-right-margin-supported
    /// See: PWG 5100.7-2023 Section 6.9.49
    /// </summary>
    public int[]? MediaRightMarginSupported { get; set; }

    /// <summary>
    /// media-top-margin-supported
    /// See: PWG 5100.7-2023 Section 6.9.54
    /// </summary>
    public int[]? MediaTopMarginSupported { get; set; }

    /// <summary>
    /// print-color-mode-default
    /// See: PWG 5100.13-2023 Section 6.5.23
    /// </summary>
    public PrintColorMode? PrintColorModeDefault { get; set; }

    /// <summary>
    /// print-color-mode-supported
    /// See: PWG 5100.13-2023 Section 6.5.25
    /// </summary>
    public PrintColorMode[]? PrintColorModeSupported { get; set; }

    /// <summary>
    /// which-jobs-supported
    /// See: RFC 8011 Section 4.2.6.1
    /// </summary>
    public WhichJobs[]? WhichJobsSupported { get; set; }

    /// <summary>
    /// printer-uuid
    /// See: PWG 5100.13-2023 Section 6.6.14
    /// </summary>
    public string? PrinterUUID { get; set; }

    /// <summary>
    /// pdf-versions-supported
    /// See: RFC 8011 Section 5.4.38
    /// </summary>
    public PdfVersion[]? PdfVersionsSupported { get; set; }

    /// <summary>
    /// ipp-features-supported
    /// See: RFC 8011 Section 5.4.39
    /// </summary>
    public IppFeature[]? IppFeaturesSupported { get; set; }

    /// <summary>
    /// document-creation-attributes-supported
    /// See: PWG 5100.5-2024 Section 6.5.1
    /// </summary>
    public DocumentCreationAttribute[]? DocumentCreationAttributesSupported { get; set; }

    /// <summary>
    /// job-account-id-default
    /// See: PWG 5100.7-2023 Section 6.9.7
    /// </summary>
    public string? JobAccountIdDefault { get; set; }

    /// <summary>
    /// job-account-id-supported
    /// See: PWG 5100.7-2023 Section 6.9.8
    /// </summary>
    public bool? JobAccountIdSupported { get; set; }

    /// <summary>
    /// job-accounting-user-id-default
    /// See: PWG 5100.7-2023 Section 6.9.9
    /// </summary>
    public string? JobAccountingUserIdDefault { get; set; }

    /// <summary>
    /// job-accounting-user-id-supported
    /// See: PWG 5100.7-2023 Section 6.9.10
    /// </summary>
    public bool? JobAccountingUserIdSupported { get; set; }

    /// <summary>
    /// job-cancel-after-default
    /// See: PWG 5100.7-2023 Section 6.9.11
    /// </summary>
    public int? JobCancelAfterDefault { get; set; }

    /// <summary>
    /// job-cancel-after-supported
    /// See: PWG 5100.7-2023 Section 6.9.12
    /// </summary>
    public Range? JobCancelAfterSupported { get; set; }

    /// <summary>
    /// job-spooling-supported
    /// See: PWG 5100.7-2023 Section 6.9.31
    /// </summary>
    public JobSpooling? JobSpoolingSupported { get; set; }

    /// <summary>
    /// max-page-ranges-supported
    /// See: PWG 5100.7-2023 Section 6.9.33
    /// </summary>
    public int? MaxPageRangesSupported { get; set; }

    /// <summary>
    /// print-content-optimize-default
    /// See: PWG 5100.7-2023 Section 6.9.58
    /// </summary>
    public PrintContentOptimize? PrintContentOptimizeDefault { get; set; }

    /// <summary>
    /// print-content-optimize-supported
    /// See: PWG 5100.7-2023 Section 6.9.59
    /// </summary>
    public PrintContentOptimize[]? PrintContentOptimizeSupported { get; set; }

    /// <summary>
    /// output-device-supported
    /// See: PWG 5100.7-2023 Section 6.9.57
    /// </summary>
    public OutputDevice[]? OutputDeviceSupported { get; set; }

    /// <summary>
    /// job-creation-attributes-supported
    /// See: PWG 5100.7-2023 Section 6.9.13
    /// </summary>
    public JobCreationAttribute[]? JobCreationAttributesSupported { get; set; }

    /// <summary>
    /// printer-requested-client-type
    /// See: PWG 5100.7-2023 Section 6.9.60
    /// </summary>
    public ClientType[]? PrinterRequestedClientType { get; set; }

    /// <summary>
    /// printer-service-type
    /// See: PWG 5100.22-2025 Section 7.7.9
    /// </summary>
    public PrinterServiceType[]? PrinterServiceType { get; set; }

    /// <summary>
    /// finishing-template-supported
    /// See: PWG 5100.1-2022 Section 6.8
    /// </summary>
    public FinishingTemplate[]? FinishingTemplateSupported { get; set; }

    /// <summary>
    /// finishings-col-supported
    /// See: PWG 5100.1-2022 Section 6.12
    /// </summary>
    public FinishingsColMember[]? FinishingsColSupported { get; set; }

    /// <summary>
    /// finishings-col-default
    /// See: PWG 5100.1-2022 Section 6.10
    /// </summary>
    public FinishingsCol[]? FinishingsColDefault { get; set; }

    /// <summary>
    /// finishings-col-ready
    /// See: PWG 5100.1-2022 Section 6.11
    /// </summary>
    public FinishingsCol[]? FinishingsColReady { get; set; }

    /// <summary>
    /// job-pages-per-set-supported
    /// See: PWG 5100.1-2022 Section 6.18
    /// </summary>
    public bool? JobPagesPerSetSupported { get; set; }

    /// <summary>
    /// punching-hole-diameter-configured
    /// See: PWG 5100.1-2022 Section 6.19
    /// </summary>
    public int? PunchingHoleDiameterConfigured { get; set; }

    /// <summary>
    /// baling-type-supported
    /// See: PWG 5100.1-2022 Section 6.1
    /// </summary>
    public BalingType[]? BalingTypeSupported { get; set; }

    /// <summary>
    /// baling-when-supported
    /// See: PWG 5100.1-2022 Section 6.2
    /// </summary>
    public BalingWhen[]? BalingWhenSupported { get; set; }

    /// <summary>
    /// binding-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.3
    /// </summary>
    public FinishingReferenceEdge[]? BindingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// binding-type-supported
    /// See: PWG 5100.1-2022 Section 6.4
    /// </summary>
    public BindingType[]? BindingTypeSupported { get; set; }

    /// <summary>
    /// coating-sides-supported
    /// See: PWG 5100.1-2022 Section 6.5
    /// </summary>
    public CoatingSides[]? CoatingSidesSupported { get; set; }

    /// <summary>
    /// coating-type-supported
    /// See: PWG 5100.1-2022 Section 6.6
    /// </summary>
    public CoatingType[]? CoatingTypeSupported { get; set; }

    /// <summary>
    /// covering-name-supported
    /// See: PWG 5100.1-2022 Section 6.7
    /// </summary>
    public CoveringName[]? CoveringNameSupported { get; set; }

    /// <summary>
    /// finishings-col-database
    /// See: PWG 5100.1-2022 Section 6.9
    /// </summary>
    public FinishingsCol[]? FinishingsColDatabase { get; set; }

    /// <summary>
    /// folding-direction-supported
    /// See: PWG 5100.1-2022 Section 6.13
    /// </summary>
    public FoldingDirection[]? FoldingDirectionSupported { get; set; }

    /// <summary>
    /// folding-offset-supported
    /// See: PWG 5100.1-2022 Section 6.14
    /// </summary>
    public Range[]? FoldingOffsetSupported { get; set; }

    /// <summary>
    /// folding-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.15
    /// </summary>
    public FinishingReferenceEdge[]? FoldingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// laminating-sides-supported
    /// See: PWG 5100.1-2022 Section 6.16
    /// </summary>
    public CoatingSides[]? LaminatingSidesSupported { get; set; }

    /// <summary>
    /// laminating-type-supported
    /// See: PWG 5100.1-2022 Section 6.17
    /// </summary>
    public LaminatingType[]? LaminatingTypeSupported { get; set; }

    /// <summary>
    /// punching-locations-supported
    /// See: PWG 5100.1-2022 Section 6.20
    /// </summary>
    public Range[]? PunchingLocationsSupported { get; set; }

    /// <summary>
    /// punching-offset-supported
    /// See: PWG 5100.1-2022 Section 6.21
    /// </summary>
    public Range[]? PunchingOffsetSupported { get; set; }

    /// <summary>
    /// punching-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.22
    /// </summary>
    public FinishingReferenceEdge[]? PunchingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// stitching-angle-supported
    /// See: PWG 5100.1-2022 Section 6.23
    /// </summary>
    public Range[]? StitchingAngleSupported { get; set; }

    /// <summary>
    /// stitching-locations-supported
    /// See: PWG 5100.1-2022 Section 6.24
    /// </summary>
    public Range[]? StitchingLocationsSupported { get; set; }

    /// <summary>
    /// stitching-method-supported
    /// See: PWG 5100.1-2022 Section 6.25
    /// </summary>
    public StitchingMethod[]? StitchingMethodSupported { get; set; }

    /// <summary>
    /// stitching-offset-supported
    /// See: PWG 5100.1-2022 Section 6.26
    /// </summary>
    public Range[]? StitchingOffsetSupported { get; set; }

    /// <summary>
    /// stitching-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.27
    /// </summary>
    public FinishingReferenceEdge[]? StitchingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// trimming-offset-supported
    /// See: PWG 5100.1-2022 Section 6.28
    /// </summary>
    public Range[]? TrimmingOffsetSupported { get; set; }

    /// <summary>
    /// trimming-reference-edge-supported
    /// See: PWG 5100.1-2022 Section 6.29
    /// </summary>
    public FinishingReferenceEdge[]? TrimmingReferenceEdgeSupported { get; set; }

    /// <summary>
    /// trimming-type-supported
    /// See: PWG 5100.1-2022 Section 6.30
    /// </summary>
    public TrimmingType[]? TrimmingTypeSupported { get; set; }

    /// <summary>
    /// trimming-when-supported
    /// See: PWG 5100.1-2022 Section 6.31
    /// </summary>
    public TrimmingWhen[]? TrimmingWhenSupported { get; set; }

    /// <summary>
    /// printer-finisher
    /// See: PWG 5100.1-2022 Section 7.1
    /// </summary>
    public PrinterFinisher[]? PrinterFinisher { get; set; }

    /// <summary>
    /// printer-finisher-description
    /// See: PWG 5100.1-2022 Section 7.2
    /// </summary>
    public string[]? PrinterFinisherDescription { get; set; }

    /// <summary>
    /// printer-finisher-supplies
    /// See: PWG 5100.1-2022 Section 7.3
    /// </summary>
    public PrinterFinisherSupply[]? PrinterFinisherSupplies { get; set; }

    /// <summary>
    /// printer-finisher-supplies-description
    /// See: PWG 5100.1-2022 Section 7.4
    /// </summary>
    public string[]? PrinterFinisherSuppliesDescription { get; set; }

    /// <summary>
    /// cover-back-default
    /// See: PWG 5100.3-2023 Section 5.3.1
    /// </summary>
    public Cover? CoverBackDefault { get; set; }
    /// <summary>
    /// cover-back-supported
    /// See: PWG 5100.3-2023 Section 5.3.2
    /// </summary>
    public CoverMember[]? CoverBackSupported { get; set; }

    /// <summary>
    /// cover-front-default
    /// See: PWG 5100.3-2023 Section 5.3.3
    /// </summary>
    public Cover? CoverFrontDefault { get; set; }

    /// <summary>
    /// cover-front-supported
    /// See: PWG 5100.3-2023 Section 5.3.4
    /// </summary>
    public CoverMember[]? CoverFrontSupported { get; set; }

    /// <summary>
    /// cover-type-supported
    /// See: PWG 5100.3-2023 Section 5.3.5
    /// </summary>
    public CoverType[]? CoverTypeSupported { get; set; }

    /// <summary>
    /// force-front-side-supported
    /// See: PWG 5100.3-2023 Section 5.3.6
    /// </summary>
    public Range? ForceFrontSideSupported { get; set; }

    /// <summary>
    /// image-orientation-default
    /// See: PWG 5100.3-2023 Section 5.3.7
    /// </summary>
    public Orientation? ImageOrientationDefault { get; set; }

    /// <summary>
    /// image-orientation-supported
    /// See: PWG 5100.3-2023 Section 5.3.8
    /// </summary>
    public Orientation[]? ImageOrientationSupported { get; set; }

    /// <summary>
    /// imposition-template-default
    /// See: PWG 5100.3-2023 Section 5.3.9
    /// </summary>
    public ImpositionTemplate? ImpositionTemplateDefault { get; set; }

    /// <summary>
    /// imposition-template-supported
    /// See: PWG 5100.3-2023 Section 5.3.10
    /// </summary>
    public ImpositionTemplate[]? ImpositionTemplateSupported { get; set; }

    /// <summary>
    /// insert-count-supported
    /// See: PWG 5100.3-2023 Section 5.3.11
    /// </summary>
    public Range? InsertCountSupported { get; set; }

    /// <summary>
    /// insert-sheet-default
    /// See: PWG 5100.3-2023 Section 5.3.12
    /// </summary>
    public InsertSheet[]? InsertSheetDefault { get; set; }

    /// <summary>
    /// insert-sheet-supported
    /// See: PWG 5100.3-2023 Section 5.3.13
    /// </summary>
    public InsertSheetMember[]? InsertSheetSupported { get; set; }

    /// <summary>
    /// job-accounting-output-bin-supported
    /// See: PWG 5100.3-2023 Section 5.3.14
    /// </summary>
    public OutputBin[]? JobAccountingOutputBinSupported { get; set; }

    /// <summary>
    /// job-accounting-sheets-default
    /// See: PWG 5100.3-2023 Section 5.3.15
    /// </summary>
    public JobAccountingSheets? JobAccountingSheetsDefault { get; set; }

    /// <summary>
    /// job-accounting-sheets-supported
    /// See: PWG 5100.3-2023 Section 5.3.16
    /// </summary>
    public JobAccountingSheetsMember[]? JobAccountingSheetsSupported { get; set; }

    /// <summary>
    /// job-accounting-sheets-type-supported
    /// See: PWG 5100.3-2023 Section 5.3.17
    /// </summary>
    public JobSheetsType[]? JobAccountingSheetsTypeSupported { get; set; }

    /// <summary>
    /// job-complete-before-supported
    /// See: PWG 5100.3-2023 Section 5.3.18
    /// </summary>
    public JobHoldUntil[]? JobCompleteBeforeSupported { get; set; }

    /// <summary>
    /// job-complete-before-time-supported
    /// See: PWG 5100.3-2023 Section 5.3.19
    /// </summary>
    public bool? JobCompleteBeforeTimeSupported { get; set; }

    /// <summary>
    /// job-error-sheet-default
    /// See: PWG 5100.3-2023 Section 5.3.20
    /// </summary>
    public JobErrorSheet? JobErrorSheetDefault { get; set; }

    /// <summary>
    /// job-error-sheet-supported
    /// See: PWG 5100.3-2023 Section 5.3.21
    /// </summary>
    public JobErrorSheetMember[]? JobErrorSheetSupported { get; set; }

    /// <summary>
    /// job-error-sheet-type-supported
    /// See: PWG 5100.3-2023 Section 5.3.22
    /// </summary>
    public JobSheetsType[]? JobErrorSheetTypeSupported { get; set; }

    /// <summary>
    /// job-error-sheet-when-supported
    /// See: PWG 5100.3-2023 Section 5.3.23
    /// </summary>
    public JobErrorSheetWhen[]? JobErrorSheetWhenSupported { get; set; }

    /// <summary>
    /// job-message-to-operator-supported
    /// See: PWG 5100.3-2023 Section 5.3.24
    /// </summary>
    public bool? JobMessageToOperatorSupported { get; set; }

    /// <summary>
    /// job-phone-number-default
    /// See: PWG 5100.3-2023 Section 5.3.25
    /// </summary>
    public string? JobPhoneNumberDefault { get; set; }

    /// <summary>
    /// job-phone-number-scheme-supported
    /// See: PWG 5100.3-2023 Section 5.3.26
    /// </summary>
    public JobPhoneNumberScheme[]? JobPhoneNumberSchemeSupported { get; set; }

    /// <summary>
    /// job-phone-number-supported
    /// See: PWG 5100.3-2023 Section 5.3.27
    /// </summary>
    public bool? JobPhoneNumberSupported { get; set; }

    /// <summary>
    /// job-recipient-name-supported
    /// See: PWG 5100.3-2023 Section 5.3.28
    /// </summary>
    public bool? JobRecipientNameSupported { get; set; }

    /// <summary>
    /// job-sheet-message-supported
    /// See: PWG 5100.3-2023
    /// </summary>
    public bool? JobSheetMessageSupported { get; set; }

    /// <summary>
    /// page-delivery-default
    /// See: PWG 5100.3-2023 Section 4.2 / 11.1
    /// </summary>
    public PageDelivery? PageDeliveryDefault { get; set; }

    /// <summary>
    /// page-delivery-supported
    /// See: PWG 5100.3-2023 Section 4.2 / 11.1
    /// </summary>
    public PageDelivery[]? PageDeliverySupported { get; set; }

    /// <summary>
    /// presentation-direction-number-up-default
    /// See: PWG 5100.3-2023 Section 5.3.29
    /// </summary>
    public PresentationDirectionNumberUp? PresentationDirectionNumberUpDefault { get; set; }

    /// <summary>
    /// presentation-direction-number-up-supported
    /// See: PWG 5100.3-2023 Section 5.3.30
    /// </summary>
    public PresentationDirectionNumberUp[]? PresentationDirectionNumberUpSupported { get; set; }

    /// <summary>
    /// separator-sheets-default
    /// See: PWG 5100.3-2023 Section 5.3.31
    /// </summary>
    public SeparatorSheets? SeparatorSheetsDefault { get; set; }

    /// <summary>
    /// separator-sheets-supported
    /// See: PWG 5100.3-2023 Section 5.3.32
    /// </summary>
    public SeparatorSheetsMember[]? SeparatorSheetsSupported { get; set; }

    /// <summary>
    /// separator-sheets-type-supported
    /// See: PWG 5100.3-2023 Section 5.3.33
    /// </summary>
    public SeparatorSheetsType[]? SeparatorSheetsTypeSupported { get; set; }

    /// <summary>
    /// x-image-position-default
    /// See: PWG 5100.3-2023 Section 5.3.34
    /// </summary>
    public XImagePosition? XImagePositionDefault { get; set; }

    /// <summary>
    /// x-image-position-supported
    /// See: PWG 5100.3-2023 Section 5.3.35
    /// </summary>
    public XImagePosition[]? XImagePositionSupported { get; set; }

    /// <summary>
    /// x-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.36
    /// </summary>
    public int? XImageShiftDefault { get; set; }

    /// <summary>
    /// x-image-shift-supported
    /// See: PWG 5100.3-2023 Section 5.3.37
    /// </summary>
    public Range? XImageShiftSupported { get; set; }

    /// <summary>
    /// x-side1-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.38
    /// </summary>
    public int? XSide1ImageShiftDefault { get; set; }

    /// <summary>
    /// x-side2-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.39
    /// </summary>
    public int? XSide2ImageShiftDefault { get; set; }

    /// <summary>
    /// y-image-position-default
    /// See: PWG 5100.3-2023 Section 5.3.40
    /// </summary>
    public YImagePosition? YImagePositionDefault { get; set; }

    /// <summary>
    /// y-image-position-supported
    /// See: PWG 5100.3-2023 Section 5.3.41
    /// </summary>
    public YImagePosition[]? YImagePositionSupported { get; set; }

    /// <summary>
    /// y-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.42
    /// </summary>
    public int? YImageShiftDefault { get; set; }

    /// <summary>
    /// y-image-shift-supported
    /// See: PWG 5100.3-2023 Section 5.3.43
    /// </summary>
    public Range? YImageShiftSupported { get; set; }

    /// <summary>
    /// y-side1-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.44
    /// </summary>
    public int? YSide1ImageShiftDefault { get; set; }

    /// <summary>
    /// y-side2-image-shift-default
    /// See: PWG 5100.3-2023 Section 5.3.45
    /// </summary>
    public int? YSide2ImageShiftDefault { get; set; }

    /// <summary>
    /// job-account-type-default
    /// See: PWG 5100.11-2024
    /// </summary>
    public JobAccountType? JobAccountTypeDefault { get; set; }

    /// <summary>
    /// job-account-type-supported
    /// See: PWG 5100.11-2024
    /// </summary>
    public JobAccountType[]? JobAccountTypeSupported { get; set; }

    /// <summary>
    /// job-password-encryption-supported
    /// See: PWG 5100.11-2024
    /// </summary>
    public JobPasswordEncryption[]? JobPasswordEncryptionSupported { get; set; }

    /// <summary>
    /// job-authorization-uri-supported
    /// See: PWG 5100.11-2024
    /// </summary>
    public bool? JobAuthorizationUriSupported { get; set; }

    /// <summary>
    /// printer-charge-info
    /// See: PWG 5100.11-2024
    /// </summary>
    public string? PrinterChargeInfo { get; set; }

    /// <summary>
    /// printer-charge-info-uri
    /// See: PWG 5100.11-2024
    /// </summary>
    public string? PrinterChargeInfoUri { get; set; }

    /// <summary>
    /// printer-mandatory-job-attributes
    /// See: PWG 5100.11-2024
    /// </summary>
    public string[]? PrinterMandatoryJobAttributes { get; set; }

    /// <summary>
    /// printer-requested-job-attributes
    /// See: PWG 5100.16-2020
    /// </summary>
    public string[]? PrinterRequestedJobAttributes { get; set; }

    /// <summary>
    /// Structured parser model for printer-alert values.
    /// See: PWG 5100.9-2009 Section 5.2.2
    /// </summary>
    public PrinterAlert[]? PrinterAlert { get; set; }

    /// <summary>
    /// printer-alert-description
    /// See: RFC 8011 Section 5.4.42
    /// </summary>
    public string[]? PrinterAlertDescription { get; set; }

    /// <summary>
    /// printer-supply
    /// See: RFC 8011 Section 5.4.43
    /// </summary>
    public string[]? PrinterSupply { get; set; }

    /// <summary>
    /// printer-supply-description
    /// See: RFC 8011 Section 5.4.44
    /// </summary>
    public string[]? PrinterSupplyDescription { get; set; }

    /// <summary>
    /// output-device-uuid-supported
    /// See: PWG 5100.18-2025
    /// </summary>
    public string[]? OutputDeviceUuidSupported { get; set; }

    /// <summary>
    /// document-access-supported
    /// See: PWG 5100.18-2025 Section 7.4.1
    /// </summary>
    public string[]? DocumentAccessSupported { get; set; }

    /// <summary>
    /// fetch-document-attributes-supported
    /// See: PWG 5100.18-2025 Section 7.4.2
    /// </summary>
    public string[]? FetchDocumentAttributesSupported { get; set; }

    /// <summary>
    /// printer-mode-configured
    /// See: PWG 5100.18-2025 Section 7.4.4
    /// </summary>
    public string? PrinterModeConfigured { get; set; }

    /// <summary>
    /// printer-mode-supported
    /// See: PWG 5100.18-2025 Section 7.4.5
    /// </summary>
    public string[]? PrinterModeSupported { get; set; }

    /// <summary>
    /// printer-static-resource-directory-uri
    /// See: PWG 5100.18-2025 Section 7.4.6
    /// </summary>
    public Uri? PrinterStaticResourceDirectoryUri { get; set; }

    /// <summary>
    /// printer-static-resource-k-octets-supported
    /// See: PWG 5100.18-2025 Section 7.4.7
    /// </summary>
    public int? PrinterStaticResourceKOctetsSupported { get; set; }

    /// <summary>
    /// printer-static-resource-k-octets-free
    /// See: PWG 5100.18-2025 Section 7.5.1
    /// </summary>
    public int? PrinterStaticResourceKOctetsFree { get; set; }

    /// <summary>
    /// accuracy-units-supported
    /// See: PWG 5100.21-2019 Section 8.1
    /// </summary>
    public string[]? AccuracyUnitsSupported { get; set; }

    /// <summary>
    /// chamber-humidity-default
    /// See: PWG 5100.21-2019 Section 8.3.2
    /// </summary>
    public int? ChamberHumidityDefault { get; set; }

    /// <summary>
    /// chamber-humidity-supported
    /// See: PWG 5100.21-2019 Section 8.3.3
    /// </summary>
    public bool? ChamberHumiditySupported { get; set; }

    /// <summary>
    /// chamber-temperature-default
    /// See: PWG 5100.21-2019 Section 8.3.4
    /// </summary>
    public int? ChamberTemperatureDefault { get; set; }

    /// <summary>
    /// chamber-temperature-supported
    /// See: PWG 5100.21-2019 Section 8.3.5
    /// </summary>
    public Range[]? ChamberTemperatureSupported { get; set; }

    /// <summary>
    /// material-amount-units-supported
    /// See: PWG 5100.21-2019 Section 8.3.6
    /// </summary>
    public string[]? MaterialAmountUnitsSupported { get; set; }

    /// <summary>
    /// material-diameter-supported
    /// See: PWG 5100.21-2019 Section 8.3.7
    /// </summary>
    public Range[]? MaterialDiameterSupported { get; set; }

    /// <summary>
    /// material-nozzle-diameter-supported
    /// See: PWG 5100.21-2019 Section 8.3.8
    /// </summary>
    public Range[]? MaterialNozzleDiameterSupported { get; set; }

    /// <summary>
    /// material-purpose-supported
    /// See: PWG 5100.21-2019 Section 8.3.9
    /// </summary>
    public string[]? MaterialPurposeSupported { get; set; }

    /// <summary>
    /// material-rate-supported
    /// See: PWG 5100.21-2019 Section 8.3.10
    /// </summary>
    public Range[]? MaterialRateSupported { get; set; }

    /// <summary>
    /// material-rate-units-supported
    /// See: PWG 5100.21-2019 Section 8.3.11
    /// </summary>
    public string[]? MaterialRateUnitsSupported { get; set; }

    /// <summary>
    /// material-shell-thickness-supported
    /// See: PWG 5100.21-2019 Section 8.3.12
    /// </summary>
    public Range[]? MaterialShellThicknessSupported { get; set; }

    /// <summary>
    /// material-temperature-supported
    /// See: PWG 5100.21-2019 Section 8.3.13
    /// </summary>
    public Range[]? MaterialTemperatureSupported { get; set; }

    /// <summary>
    /// material-type-supported
    /// See: PWG 5100.21-2019 Section 8.3.14
    /// </summary>
    public string[]? MaterialTypeSupported { get; set; }

    /// <summary>
    /// materials-col-database
    /// See: PWG 5100.21-2019 Section 8.3.15
    /// </summary>
    public Material[]? MaterialsColDatabase { get; set; }

    /// <summary>
    /// materials-col-default
    /// See: PWG 5100.21-2019 Section 8.3.16
    /// </summary>
    public Material[]? MaterialsColDefault { get; set; }

    /// <summary>
    /// materials-col-ready
    /// See: PWG 5100.21-2019 Section 8.3.17
    /// </summary>
    public Material[]? MaterialsColReady { get; set; }

    /// <summary>
    /// materials-col-supported
    /// See: PWG 5100.21-2019 Section 8.3.18
    /// </summary>
    public string[]? MaterialsColSupported { get; set; }

    /// <summary>
    /// max-materials-col-supported
    /// See: PWG 5100.21-2019 Section 8.3.19
    /// </summary>
    public int? MaxMaterialsColSupported { get; set; }

    /// <summary>
    /// multiple-object-handling-default
    /// See: PWG 5100.21-2019 Section 8.3.20
    /// </summary>
    public string? MultipleObjectHandlingDefault { get; set; }

    /// <summary>
    /// multiple-object-handling-supported
    /// See: PWG 5100.21-2019 Section 8.3.21
    /// </summary>
    public string[]? MultipleObjectHandlingSupported { get; set; }

    /// <summary>
    /// pdf-features-supported
    /// See: PWG 5100.21-2019 Section 8.3.22
    /// </summary>
    public string[]? PdfFeaturesSupported { get; set; }

    /// <summary>
    /// platform-shape
    /// See: PWG 5100.21-2019 Section 8.3.23
    /// </summary>
    public string? PlatformShape { get; set; }

    /// <summary>
    /// platform-temperature-default
    /// See: PWG 5100.21-2019 Section 8.3.24
    /// </summary>
    public int? PlatformTemperatureDefault { get; set; }

    /// <summary>
    /// platform-temperature-supported
    /// See: PWG 5100.21-2019 Section 8.3.25
    /// </summary>
    public Range[]? PlatformTemperatureSupported { get; set; }

    /// <summary>
    /// print-accuracy-default
    /// See: PWG 5100.21-2019 Section 8.3.26
    /// </summary>
    public PrintAccuracy? PrintAccuracyDefault { get; set; }

    /// <summary>
    /// print-accuracy-supported
    /// See: PWG 5100.21-2019 Section 8.3.27
    /// </summary>
    public PrintAccuracy? PrintAccuracySupported { get; set; }

    /// <summary>
    /// print-base-default
    /// See: PWG 5100.21-2019 Section 8.3.28
    /// </summary>
    public string? PrintBaseDefault { get; set; }

    /// <summary>
    /// print-base-supported
    /// See: PWG 5100.21-2019 Section 8.3.29
    /// </summary>
    public string[]? PrintBaseSupported { get; set; }

    /// <summary>
    /// print-objects-supported
    /// See: PWG 5100.21-2019 Section 8.3.30
    /// </summary>
    public string[]? PrintObjectsSupported { get; set; }

    /// <summary>
    /// print-supports-default
    /// See: PWG 5100.21-2019 Section 8.3.31
    /// </summary>
    public string? PrintSupportsDefault { get; set; }

    /// <summary>
    /// print-supports-supported
    /// See: PWG 5100.21-2019 Section 8.3.32
    /// </summary>
    public string[]? PrintSupportsSupported { get; set; }

    /// <summary>
    /// printer-volume-supported
    /// See: PWG 5100.21-2019 Section 8.3.33
    /// </summary>
    public PrinterVolumeSupported? PrinterVolumeSupported { get; set; }

    /// <summary>
    /// chamber-humidity-current
    /// See: PWG 5100.21-2019 Section 8.4.1
    /// </summary>
    public int? ChamberHumidityCurrent { get; set; }

    /// <summary>
    /// chamber-temperature-current
    /// See: PWG 5100.21-2019 Section 8.4.2
    /// </summary>
    public int? ChamberTemperatureCurrent { get; set; }

    /// <summary>
    /// printer-camera-image-uri
    /// See: PWG 5100.21-2019 Section 8.17
    /// </summary>
    public string[]? PrinterCameraImageUri { get; set; }

    /// <summary>
    /// printer-resource-ids
    /// See: PWG 5100.22-2025 Section 6.1.2
    /// </summary>
    public int[]? PrinterResourceIds { get; set; }

    /// <summary>
    /// confirmation-sheet-print-default
    /// See: PWG 5100.15-2013 Section 7.4.1
    /// </summary>
    public bool? ConfirmationSheetPrintDefault { get; set; }

    /// <summary>
    /// cover-sheet-info-default
    /// See: PWG 5100.15-2013 Section 7.4.2
    /// </summary>
    public CoverSheetInfo? CoverSheetInfoDefault { get; set; }

    /// <summary>
    /// cover-sheet-info-supported
    /// See: PWG 5100.15-2013 Section 7.4.3
    /// </summary>
    public string[]? CoverSheetInfoSupported { get; set; }

    /// <summary>
    /// destination-accesses-supported
    /// See: PWG 5100.17-2014 Section 8.3.1
    /// </summary>
    public string[]? DestinationAccessesSupported { get; set; }

    /// <summary>
    /// destination-uri-ready
    /// See: PWG 5100.17-2014 Section 8.3.3
    /// </summary>
    public DestinationUriReady[]? DestinationUriReady { get; set; }

    /// <summary>
    /// destination-uri-schemes-supported
    /// See: PWG 5100.15-2013 Section 7.4.4
    /// </summary>
    public UriScheme[]? DestinationUriSchemesSupported { get; set; }

    /// <summary>
    /// destination-uris-supported
    /// See: PWG 5100.15-2013 Section 7.4.5
    /// </summary>
    public string[]? DestinationUrisSupported { get; set; }

    /// <summary>
    /// from-name-supported
    /// See: PWG 5100.15-2013 Section 7.4.6
    /// </summary>
    public int? FromNameSupported { get; set; }

    /// <summary>
    /// input-attributes-default
    /// See: PWG 5100.15-2013 Section 7.4.7
    /// </summary>
    public DocumentTemplateAttributes? InputAttributesDefault { get; set; }

    /// <summary>
    /// input-attributes-supported
    /// See: PWG 5100.15-2013 Section 7.4.8
    /// </summary>
    public string[]? InputAttributesSupported { get; set; }

    /// <summary>
    /// input-color-mode-supported
    /// See: PWG 5100.15-2013 Section 7.4.9
    /// </summary>
    public string[]? InputColorModeSupported { get; set; }

    /// <summary>
    /// input-content-type-supported
    /// See: PWG 5100.15-2013 Section 7.4.10
    /// </summary>
    public string[]? InputContentTypeSupported { get; set; }

    /// <summary>
    /// input-film-scan-mode-supported
    /// See: PWG 5100.15-2013 Section 7.4.11
    /// </summary>
    public string[]? InputFilmScanModeSupported { get; set; }

    /// <summary>
    /// input-media-supported
    /// See: PWG 5100.15-2013 Section 7.4.12
    /// </summary>
    public Media[]? InputMediaSupported { get; set; }

    /// <summary>
    /// input-orientation-requested-supported
    /// See: PWG 5100.15-2013 Section 7.4.13
    /// </summary>
    public Orientation[]? InputOrientationRequestedSupported { get; set; }

    /// <summary>
    /// input-quality-supported
    /// See: PWG 5100.15-2013 Section 7.4.14
    /// </summary>
    public PrintQuality[]? InputQualitySupported { get; set; }

    /// <summary>
    /// input-resolution-supported
    /// See: PWG 5100.15-2013 Section 7.4.15
    /// </summary>
    public Resolution[]? InputResolutionSupported { get; set; }

    /// <summary>
    /// input-sides-supported
    /// See: PWG 5100.15-2013 Section 7.4.17
    /// </summary>
    public Sides[]? InputSidesSupported { get; set; }

    /// <summary>
    /// input-source-supported
    /// See: PWG 5100.15-2013 Section 7.4.18
    /// </summary>
    public string[]? InputSourceSupported { get; set; }

    /// <summary>
    /// logo-uri-formats-supported
    /// See: PWG 5100.15-2013 Section 7.4.19
    /// </summary>
    public string[]? LogoUriFormatsSupported { get; set; }

    /// <summary>
    /// logo-uri-schemes-supported
    /// See: PWG 5100.15-2013 Section 7.4.20
    /// </summary>
    public UriScheme[]? LogoUriSchemesSupported { get; set; }

    /// <summary>
    /// message-supported
    /// See: PWG 5100.15-2013 Section 7.4.21
    /// </summary>
    public int? MessageSupported { get; set; }

    /// <summary>
    /// multiple-destination-uris-supported
    /// See: PWG 5100.15-2013 Section 7.4.22
    /// </summary>
    public bool? MultipleDestinationUrisSupported { get; set; }

    /// <summary>
    /// number-of-retries-default
    /// See: PWG 5100.15-2013 Section 7.4.23
    /// </summary>
    public int? NumberOfRetriesDefault { get; set; }

    /// <summary>
    /// number-of-retries-supported
    /// See: PWG 5100.15-2013 Section 7.4.24
    /// </summary>
    public Protocol.Models.Range? NumberOfRetriesSupported { get; set; }

    /// <summary>
    /// organization-name-supported
    /// See: PWG 5100.15-2013 Section 7.4.25
    /// </summary>
    public int? OrganizationNameSupported { get; set; }

    /// <summary>
    /// job-destination-spooling-supported
    /// See: PWG 5100.17-2014 Section 8.3.4
    /// </summary>
    public JobSpooling? JobDestinationSpoolingSupported { get; set; }

    /// <summary>
    /// output-attributes-default
    /// See: PWG 5100.17-2014 Section 8.3.5
    /// </summary>
    public OutputAttributes? OutputAttributesDefault { get; set; }

    /// <summary>
    /// output-attributes-supported
    /// See: PWG 5100.17-2014 Section 8.3.6
    /// </summary>
    public string[]? OutputAttributesSupported { get; set; }

    /// <summary>
    /// printer-fax-log-uri
    /// See: PWG 5100.15-2013 Section 7.4.26
    /// </summary>
    public Uri? PrinterFaxLogUri { get; set; }

    /// <summary>
    /// printer-fax-modem-info
    /// See: PWG 5100.15-2013 Section 7.4.27
    /// </summary>
    public string[]? PrinterFaxModemInfo { get; set; }

    /// <summary>
    /// printer-fax-modem-name
    /// See: PWG 5100.15-2013 Section 7.4.28
    /// </summary>
    public string[]? PrinterFaxModemName { get; set; }

    /// <summary>
    /// printer-fax-modem-number
    /// See: PWG 5100.15-2013 Section 7.4.29
    /// </summary>
    public Uri[]? PrinterFaxModemNumber { get; set; }

    /// <summary>
    /// retry-interval-default
    /// See: PWG 5100.15-2013 Section 7.4.30
    /// </summary>
    public int? RetryIntervalDefault { get; set; }

    /// <summary>
    /// retry-interval-supported
    /// See: PWG 5100.15-2013 Section 7.4.31
    /// </summary>
    public Protocol.Models.Range? RetryIntervalSupported { get; set; }

    /// <summary>
    /// retry-time-out-default
    /// See: PWG 5100.15-2013 Section 7.4.32
    /// </summary>
    public int? RetryTimeOutDefault { get; set; }

    /// <summary>
    /// retry-time-out-supported
    /// See: PWG 5100.15-2013 Section 7.4.33
    /// </summary>
    public Protocol.Models.Range? RetryTimeOutSupported { get; set; }

    /// <summary>
    /// subject-supported
    /// See: PWG 5100.15-2013 Section 7.4.34
    /// </summary>
    public int? SubjectSupported { get; set; }

    /// <summary>
    /// to-name-supported
    /// See: PWG 5100.15-2013 Section 7.4.35
    /// </summary>
    public int? ToNameSupported { get; set; }
}
