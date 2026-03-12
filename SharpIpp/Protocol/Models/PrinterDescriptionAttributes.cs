using System;

namespace SharpIpp.Protocol.Models
{
    public class PrinterDescriptionAttributes
    {
        /// <summary>
        ///     printer-uri-supported (RFC 8011 Section 5.4.1)
        /// </summary>
        public string[]? PrinterUriSupported { get; set; }

        /// <summary>
        ///     uri-security-supported (RFC 8011 Section 5.4.3)
        /// </summary>
        public UriSecurity[]? UriSecuritySupported { get; set; }

        /// <summary>
        ///     uri-authentication-supported (RFC 8011 Section 5.4.2)
        /// </summary>
        public UriAuthentication[]? UriAuthenticationSupported { get; set; }

        /// <summary>
        ///     printer-name (RFC 8011 Section 5.4.4)
        /// </summary>
        public string? PrinterName { get; set; }

        /// <summary>
        ///     printer-location (RFC 8011 Section 5.4.5)
        /// </summary>
        public string? PrinterLocation { get; set; }

        /// <summary>
        ///     printer-info (RFC 8011 Section 5.4.6)
        /// </summary>
        public string? PrinterInfo { get; set; }

        /// <summary>
        ///     printer-more-info (RFC 8011 Section 5.4.7)
        /// </summary>
        public string? PrinterMoreInfo { get; set; }

        /// <summary>
        ///     printer-driver-installer (RFC 8011 Section 5.4.8)
        /// </summary>
        public string? PrinterDriverInstaller { get; set; }

        /// <summary>
        ///     printer-make-and-model (RFC 8011 Section 5.4.9)
        /// </summary>
        public string? PrinterMakeAndModel { get; set; }

        /// <summary>
        ///     printer-more-info-manufacturer (RFC 8011 Section 5.4.10)
        /// </summary>
        public string? PrinterMoreInfoManufacturer { get; set; }

        /// <summary>
        ///     printer-state (RFC 8011 Section 5.4.11)
        /// </summary>
        public PrinterState? PrinterState { get; set; }

        /// <summary>
        ///     printer-state-reasons (RFC 8011 Section 5.4.12)
        /// </summary>
        public PrinterStateReason[]? PrinterStateReasons { get; set; }

        /// <summary>
        ///     printer-state-message (RFC 8011 Section 5.4.13)
        /// </summary>
        public string? PrinterStateMessage { get; set; }

        /// <summary>
        ///     ipp-versions-supported (RFC 8011 Section 5.4.14)
        /// </summary>
        public IppVersion[]? IppVersionsSupported { get; set; }

        /// <summary>
        ///     operations-supported (RFC 8011 Section 5.4.15)
        /// </summary>
        public IppOperation[]? OperationsSupported { get; set; }

        /// <summary>
        ///     multiple-document-jobs-supported (RFC 8011 Section 5.4.16)
        /// </summary>
        public bool? MultipleDocumentJobsSupported { get; set; }

        /// <summary>
        ///     charset-configured (RFC 8011 Section 5.4.17)
        /// </summary>
        public string? CharsetConfigured { get; set; }

        /// <summary>
        ///     charset-supported (RFC 8011 Section 5.4.18)
        /// </summary>
        public string[]? CharsetSupported { get; set; }

        /// <summary>
        ///     natural-language-configured (RFC 8011 Section 5.4.19)
        /// </summary>
        public string? NaturalLanguageConfigured { get; set; }

        /// <summary>
        ///     generated-natural-language-supported (RFC 8011 Section 5.4.20)
        /// </summary>
        public string[]? GeneratedNaturalLanguageSupported { get; set; }

        /// <summary>
        ///     document-format-default (RFC 8011 Section 5.4.21)
        /// </summary>
        public string? DocumentFormatDefault { get; set; }

        /// <summary>
        ///     document-format-supported (RFC 8011 Section 5.4.22)
        /// </summary>
        public string[]? DocumentFormatSupported { get; set; }

        /// <summary>
        ///     printer-is-accepting-jobs (RFC 8011 Section 5.4.23)
        /// </summary>
        public bool? PrinterIsAcceptingJobs { get; set; }

        /// <summary>
        ///     queued-job-count (RFC 8011 Section 5.4.24)
        /// </summary>
        public int? QueuedJobCount { get; set; }

        /// <summary>
        ///     printer-message-from-operator (RFC 8011 Section 5.4.25)
        /// </summary>
        public string? PrinterMessageFromOperator { get; set; }

        /// <summary>
        ///     color-supported (RFC 8011 Section 5.4.26)
        /// </summary>
        public bool? ColorSupported { get; set; }

        /// <summary>
        ///     reference-uri-schemes-supported (RFC 8011 Section 5.4.27)
        /// </summary>
        public UriScheme[]? ReferenceUriSchemesSupported { get; set; }

        /// <summary>
        ///     pdl-override-supported (RFC 8011 Section 5.4.28)
        /// </summary>
        public PdlOverride? PdlOverrideSupported { get; set; }

        /// <summary>
        ///     printer-up-time (RFC 8011 Section 5.4.29)
        /// </summary>
        public int? PrinterUpTime { get; set; }

        /// <summary>
        ///     printer-current-time (RFC 8011 Section 5.4.30)
        /// </summary>
        public DateTimeOffset? PrinterCurrentTime { get; set; }

        /// <summary>
        ///     multiple-operation-time-out (RFC 8011 Section 5.4.31)
        /// </summary>
        public int? MultipleOperationTimeOut { get; set; }

        /// <summary>
        ///     compression-supported (RFC 8011 Section 5.4.32)
        /// </summary>
        public Compression[]? CompressionSupported { get; set; }

        /// <summary>
        ///     job-k-octets-supported (RFC 8011 Section 5.4.33)
        /// </summary>
        public Range? JobKOctetsSupported { get; set; }

        /// <summary>
        ///     jpeg-k-octets-supported (PWG 5100.13-2023 Section 6.5.12)
        /// </summary>
        public Range? JpegKOctetsSupported { get; set; }

        /// <summary>
        ///     pdf-k-octets-supported (PWG 5100.13-2023 Section 6.5.20)
        /// </summary>
        public Range? PdfKOctetsSupported { get; set; }

        /// <summary>
        ///     job-impressions-supported (RFC 8011 Section 5.4.34)
        /// </summary>
        public Range? JobImpressionsSupported { get; set; }

        /// <summary>
        ///     job-media-sheets-supported (RFC 8011 Section 5.4.35)
        /// </summary>
        public Range? JobMediaSheetsSupported { get; set; }

        /// <summary>
        ///     pages-per-minute (RFC 8011 Section 5.4.36)
        /// </summary>
        public int? PagesPerMinute { get; set; }

        /// <summary>
        ///     pages-per-minute-color (RFC 8011 Section 5.4.37)
        /// </summary>
        public int? PagesPerMinuteColor { get; set; }

        /// <summary>
        ///     print-scaling-default (PWG 5100.13-2023 Section 6.5.29)
        /// </summary>
        public PrintScaling? PrintScalingDefault { get; set; }

        /// <summary>
        ///     print-scaling-supported (PWG 5100.13-2023 Section 6.5.30)
        /// </summary>
        public PrintScaling[]? PrintScalingSupported { get; set; }

        /// <summary>
        ///     media-default (RFC 8011 Section 5.2.11)
        /// </summary>
        public string? MediaDefault { get; set; }

        /// <summary>
        ///     media-supported (RFC 8011 Section 5.2.11)
        /// </summary>
        public string[]? MediaSupported { get; set; }

        /// <summary>
        ///     sides-default (RFC 8011 Section 5.2.8)
        /// </summary>
        public Sides? SidesDefault { get; set; }

        /// <summary>
        ///     sides-supported (RFC 8011 Section 5.2.8)
        /// </summary>
        public Sides[]? SidesSupported { get; set; }

        /// <summary>
        ///     finishings-default (RFC 8011 Section 5.2.6)
        /// </summary>
        public Finishings? FinishingsDefault { get; set; }

        /// <summary>
        ///     finishings-supported (RFC 8011 Section 5.2.6)
        /// </summary>
        public Finishings[]? FinishingsSupported { get; set; }

        /// <summary>
        ///     printer-resolution-default (RFC 8011 Section 5.2.12)
        /// </summary>
        public Resolution? PrinterResolutionDefault { get; set; }

        /// <summary>
        ///     printer-resolution-supported (RFC 8011 Section 5.2.12)
        /// </summary>
        public Resolution[]? PrinterResolutionSupported { get; set; }

        /// <summary>
        ///     print-quality-default (RFC 8011 Section 5.2.13)
        /// </summary>
        public PrintQuality? PrintQualityDefault { get; set; }

        /// <summary>
        ///     print-quality-supported (RFC 8011 Section 5.2.13)
        /// </summary>
        public PrintQuality[]? PrintQualitySupported { get; set; }

        /// <summary>
        ///     job-priority-default (RFC 8011 Section 5.2.1)
        /// </summary>
        public int? JobPriorityDefault { get; set; }

        /// <summary>
        ///     job-priority-supported (RFC 8011 Section 5.2.1)
        /// </summary>
        public int? JobPrioritySupported { get; set; }

        /// <summary>
        ///     copies-default (RFC 8011 Section 5.2.5)
        /// </summary>
        public int? CopiesDefault { get; set; }

        /// <summary>
        ///     copies-supported (RFC 8011 Section 5.2.5)
        /// </summary>
        public Range? CopiesSupported { get; set; }

        /// <summary>
        ///     orientation-requested-default (RFC 8011 Section 5.2.10)
        /// </summary>
        public Orientation? OrientationRequestedDefault { get; set; }

        /// <summary>
        ///     orientation-requested-supported (RFC 8011 Section 5.2.10)
        /// </summary>
        public Orientation[]? OrientationRequestedSupported { get; set; }

        /// <summary>
        ///     page-ranges-supported (RFC 8011 Section 5.2.7)
        /// </summary>
        public bool? PageRangesSupported { get; set; }

        /// <summary>
        ///     job-hold-until-supported (RFC 8011 Section 5.2.2)
        /// </summary>
        public JobHoldUntil[]? JobHoldUntilSupported { get; set; }

        /// <summary>
        ///     job-hold-until-default (RFC 8011 Section 5.2.2)
        /// </summary>
        public JobHoldUntil? JobHoldUntilDefault { get; set; }

        /// <summary>
        ///     output-bin-default (PWG 5100.2-2001 Section 2.1)
        /// </summary>
        public string? OutputBinDefault { get; set; }

        /// <summary>
        ///     output-bin-supported (PWG 5100.2-2001 Section 2.1)
        /// </summary>
        public string[]? OutputBinSupported { get; set; }

        /// <summary>
        ///     media-col-default (PWG 5100.7-2023)
        /// </summary>
        public MediaCol? MediaColDefault { get; set; }

        /// <summary>
        ///     print-color-mode-default (PWG 5100.13-2023 Section 6.5.23)
        /// </summary>
        public PrintColorMode? PrintColorModeDefault { get; set; }

        /// <summary>
        ///     print-color-mode-supported (PWG 5100.13-2023 Section 6.5.25)
        /// </summary>
        public PrintColorMode[]? PrintColorModeSupported { get; set; }

        /// <summary>
        ///     which-jobs-supported (RFC 8011 Section 4.2.6.1)
        /// </summary>
        public WhichJobs[]? WhichJobsSupported { get; set; }

        /// <summary>
        ///     printer-uuid (PWG 5100.13-2023 Section 6.6.14)
        /// </summary>
        public string? PrinterUUID { get; set; }

        /// <summary>
        ///     document-creation-attributes-supported (PWG 5100.5-2024 Section 6.5.1)
        /// </summary>
        public string[]? DocumentCreationAttributesSupported { get; set; }

        /// <summary>
        ///     job-account-id-default (PWG 5100.7-2023 Section 6.9.7)
        /// </summary>
        public string? JobAccountIdDefault { get; set; }

        /// <summary>
        ///     job-account-id-supported (PWG 5100.7-2023 Section 6.9.8)
        /// </summary>
        public bool? JobAccountIdSupported { get; set; }

        /// <summary>
        ///     job-accounting-user-id-default (PWG 5100.7-2023 Section 6.9.9)
        /// </summary>
        public string? JobAccountingUserIdDefault { get; set; }

        /// <summary>
        ///     job-accounting-user-id-supported (PWG 5100.7-2023 Section 6.9.10)
        /// </summary>
        public bool? JobAccountingUserIdSupported { get; set; }

        /// <summary>
        ///     job-cancel-after-default (PWG 5100.7-2023 Section 6.9.11)
        /// </summary>
        public int? JobCancelAfterDefault { get; set; }

        /// <summary>
        ///     job-cancel-after-supported (PWG 5100.7-2023 Section 6.9.12)
        /// </summary>
        public Range? JobCancelAfterSupported { get; set; }

        /// <summary>
        ///     job-spooling-supported (PWG 5100.7-2023 Section 6.9.31)
        /// </summary>
        public JobSpooling? JobSpoolingSupported { get; set; }

        /// <summary>
        ///     max-page-ranges-supported (PWG 5100.7-2023 Section 6.9.33)
        /// </summary>
        public int? MaxPageRangesSupported { get; set; }

        /// <summary>
        ///     print-content-optimize-default (PWG 5100.7-2023 Section 6.9.58)
        /// </summary>
        public PrintContentOptimize? PrintContentOptimizeDefault { get; set; }

        /// <summary>
        ///     print-content-optimize-supported (PWG 5100.7-2023 Section 6.9.59)
        /// </summary>
        public PrintContentOptimize[]? PrintContentOptimizeSupported { get; set; }

        /// <summary>
        ///     output-device-supported (PWG 5100.7-2023 Section 6.9.57)
        /// </summary>
        public string[]? OutputDeviceSupported { get; set; }

        /// <summary>
        ///     job-creation-attributes-supported (PWG 5100.7-2023 Section 6.9.13)
        /// </summary>
        public string[]? JobCreationAttributesSupported { get; set; }

        /// <summary>
        ///     finishing-template-supported (PWG 5100.1-2022 Section 6.8)
        /// </summary>
        public string[]? FinishingTemplateSupported { get; set; }

        /// <summary>
        ///     finishings-col-supported (PWG 5100.1-2022 Section 6.12)
        /// </summary>
        public string[]? FinishingsColSupported { get; set; }

        /// <summary>
        ///     finishings-col-default (PWG 5100.1-2022 Section 6.10)
        /// </summary>
        public FinishingsCol? FinishingsColDefault { get; set; }

        /// <summary>
        ///     finishings-col-ready (PWG 5100.1-2022 Section 6.11)
        /// </summary>
        public FinishingsCol[]? FinishingsColReady { get; set; }

        /// <summary>
        ///     job-pages-per-set-supported (PWG 5100.1-2022 Section 6.18)
        /// </summary>
        public bool? JobPagesPerSetSupported { get; set; }

        /// <summary>
        ///     punching-hole-diameter-configured (PWG 5100.1-2022 Section 6.19)
        /// </summary>
        public int? PunchingHoleDiameterConfigured { get; set; }

        /// <summary>
        ///     baling-type-supported (PWG 5100.1-2022 Section 6.1)
        /// </summary>
        public BalingType[]? BalingTypeSupported { get; set; }

        /// <summary>
        ///     baling-when-supported (PWG 5100.1-2022 Section 6.2)
        /// </summary>
        public BalingWhen[]? BalingWhenSupported { get; set; }

        /// <summary>
        ///     binding-reference-edge-supported (PWG 5100.1-2022 Section 6.3)
        /// </summary>
        public FinishingReferenceEdge[]? BindingReferenceEdgeSupported { get; set; }

        /// <summary>
        ///     binding-type-supported (PWG 5100.1-2022 Section 6.4)
        /// </summary>
        public BindingType[]? BindingTypeSupported { get; set; }

        /// <summary>
        ///     coating-sides-supported (PWG 5100.1-2022 Section 6.5)
        /// </summary>
        public CoatingSides[]? CoatingSidesSupported { get; set; }

        /// <summary>
        ///     coating-type-supported (PWG 5100.1-2022 Section 6.6)
        /// </summary>
        public CoatingType[]? CoatingTypeSupported { get; set; }

        /// <summary>
        ///     covering-name-supported (PWG 5100.1-2022 Section 6.7)
        /// </summary>
        public CoveringName[]? CoveringNameSupported { get; set; }

        /// <summary>
        ///     finishings-col-database (PWG 5100.1-2022 Section 6.9)
        /// </summary>
        public FinishingsCol[]? FinishingsColDatabase { get; set; }

        /// <summary>
        ///     folding-direction-supported (PWG 5100.1-2022 Section 6.13)
        /// </summary>
        public FoldingDirection[]? FoldingDirectionSupported { get; set; }

        /// <summary>
        ///     folding-offset-supported (PWG 5100.1-2022 Section 6.14)
        /// </summary>
        public Range[]? FoldingOffsetSupported { get; set; }

        /// <summary>
        ///     folding-reference-edge-supported (PWG 5100.1-2022 Section 6.15)
        /// </summary>
        public FinishingReferenceEdge[]? FoldingReferenceEdgeSupported { get; set; }

        /// <summary>
        ///     laminating-sides-supported (PWG 5100.1-2022 Section 6.16)
        /// </summary>
        public CoatingSides[]? LaminatingSidesSupported { get; set; }

        /// <summary>
        ///     laminating-type-supported (PWG 5100.1-2022 Section 6.17)
        /// </summary>
        public LaminatingType[]? LaminatingTypeSupported { get; set; }

        /// <summary>
        ///     punching-locations-supported (PWG 5100.1-2022 Section 6.20)
        /// </summary>
        public Range[]? PunchingLocationsSupported { get; set; }

        /// <summary>
        ///     punching-offset-supported (PWG 5100.1-2022 Section 6.21)
        /// </summary>
        public Range[]? PunchingOffsetSupported { get; set; }

        /// <summary>
        ///     punching-reference-edge-supported (PWG 5100.1-2022 Section 6.22)
        /// </summary>
        public FinishingReferenceEdge[]? PunchingReferenceEdgeSupported { get; set; }

        /// <summary>
        ///     stitching-angle-supported (PWG 5100.1-2022 Section 6.23)
        /// </summary>
        public Range[]? StitchingAngleSupported { get; set; }

        /// <summary>
        ///     stitching-locations-supported (PWG 5100.1-2022 Section 6.24)
        /// </summary>
        public Range[]? StitchingLocationsSupported { get; set; }

        /// <summary>
        ///     stitching-method-supported (PWG 5100.1-2022 Section 6.25)
        /// </summary>
        public StitchingMethod[]? StitchingMethodSupported { get; set; }

        /// <summary>
        ///     stitching-offset-supported (PWG 5100.1-2022 Section 6.26)
        /// </summary>
        public Range[]? StitchingOffsetSupported { get; set; }

        /// <summary>
        ///     stitching-reference-edge-supported (PWG 5100.1-2022 Section 6.27)
        /// </summary>
        public FinishingReferenceEdge[]? StitchingReferenceEdgeSupported { get; set; }

        /// <summary>
        ///     trimming-offset-supported (PWG 5100.1-2022 Section 6.28)
        /// </summary>
        public Range[]? TrimmingOffsetSupported { get; set; }

        /// <summary>
        ///     trimming-reference-edge-supported (PWG 5100.1-2022 Section 6.29)
        /// </summary>
        public FinishingReferenceEdge[]? TrimmingReferenceEdgeSupported { get; set; }

        /// <summary>
        ///     trimming-type-supported (PWG 5100.1-2022 Section 6.30)
        /// </summary>
        public TrimmingType[]? TrimmingTypeSupported { get; set; }

        /// <summary>
        ///     trimming-when-supported (PWG 5100.1-2022 Section 6.31)
        /// </summary>
        public TrimmingWhen[]? TrimmingWhenSupported { get; set; }

        /// <summary>
        ///     printer-finisher (PWG 5100.1-2022 Section 7.1)
        /// </summary>
        public string[]? PrinterFinisher { get; set; }

        /// <summary>
        ///     printer-finisher-description (PWG 5100.1-2022 Section 7.2)
        /// </summary>
        public string[]? PrinterFinisherDescription { get; set; }

        /// <summary>
        ///     printer-finisher-supplies (PWG 5100.1-2022 Section 7.3)
        /// </summary>
        public string[]? PrinterFinisherSupplies { get; set; }

        /// <summary>
        ///     printer-finisher-supplies-description (PWG 5100.1-2022 Section 7.4)
        /// </summary>
        public string[]? PrinterFinisherSuppliesDescription { get; set; }

        /// <summary>
        ///     cover-back-default (PWG 5100.3-2023 Section 5.3.1)
        /// </summary>
        public Cover? CoverBackDefault { get; set; }

        /// <summary>
        ///     cover-back-supported (PWG 5100.3-2023 Section 5.3.2)
        /// </summary>
        public string[]? CoverBackSupported { get; set; }

        /// <summary>
        ///     cover-front-default (PWG 5100.3-2023 Section 5.3.3)
        /// </summary>
        public Cover? CoverFrontDefault { get; set; }

        /// <summary>
        ///     cover-front-supported (PWG 5100.3-2023 Section 5.3.4)
        /// </summary>
        public string[]? CoverFrontSupported { get; set; }

        /// <summary>
        ///     cover-type-supported (PWG 5100.3-2023 Section 5.3.5)
        /// </summary>
        public CoverType[]? CoverTypeSupported { get; set; }

        /// <summary>
        ///     force-front-side-supported (PWG 5100.3-2023 Section 5.3.6)
        /// </summary>
        public Range? ForceFrontSideSupported { get; set; }

        /// <summary>
        ///     image-orientation-default (PWG 5100.3-2023 Section 5.3.7)
        /// </summary>
        public Orientation? ImageOrientationDefault { get; set; }

        /// <summary>
        ///     image-orientation-supported (PWG 5100.3-2023 Section 5.3.8)
        /// </summary>
        public Orientation[]? ImageOrientationSupported { get; set; }

        /// <summary>
        ///     imposition-template-default (PWG 5100.3-2023 Section 5.3.9)
        /// </summary>
        public string? ImpositionTemplateDefault { get; set; }

        /// <summary>
        ///     imposition-template-supported (PWG 5100.3-2023 Section 5.3.10)
        /// </summary>
        public string[]? ImpositionTemplateSupported { get; set; }

        /// <summary>
        ///     insert-count-supported (PWG 5100.3-2023 Section 5.3.11)
        /// </summary>
        public Range? InsertCountSupported { get; set; }

        /// <summary>
        ///     insert-sheet-default (PWG 5100.3-2023 Section 5.3.12)
        /// </summary>
        public InsertSheet[]? InsertSheetDefault { get; set; }

        /// <summary>
        ///     insert-sheet-supported (PWG 5100.3-2023 Section 5.3.13)
        /// </summary>
        public string[]? InsertSheetSupported { get; set; }

        /// <summary>
        ///     job-accounting-output-bin-supported (PWG 5100.3-2023 Section 5.3.14)
        /// </summary>
        public string[]? JobAccountingOutputBinSupported { get; set; }

        /// <summary>
        ///     job-accounting-sheets-default (PWG 5100.3-2023 Section 5.3.15)
        /// </summary>
        public JobAccountingSheets? JobAccountingSheetsDefault { get; set; }

        /// <summary>
        ///     job-accounting-sheets-supported (PWG 5100.3-2023 Section 5.3.16)
        /// </summary>
        public string[]? JobAccountingSheetsSupported { get; set; }

        /// <summary>
        ///     job-accounting-sheets-type-supported (PWG 5100.3-2023 Section 5.3.17)
        /// </summary>
        public JobSheetsType[]? JobAccountingSheetsTypeSupported { get; set; }

        /// <summary>
        ///     job-complete-before-supported (PWG 5100.3-2023 Section 5.3.18)
        /// </summary>
        public JobHoldUntil[]? JobCompleteBeforeSupported { get; set; }

        /// <summary>
        ///     job-complete-before-time-supported (PWG 5100.3-2023 Section 5.3.19)
        /// </summary>
        public bool? JobCompleteBeforeTimeSupported { get; set; }

        /// <summary>
        ///     job-error-sheet-default (PWG 5100.3-2023 Section 5.3.20)
        /// </summary>
        public JobErrorSheet? JobErrorSheetDefault { get; set; }

        /// <summary>
        ///     job-error-sheet-supported (PWG 5100.3-2023 Section 5.3.21)
        /// </summary>
        public string[]? JobErrorSheetSupported { get; set; }

        /// <summary>
        ///     job-error-sheet-type-supported (PWG 5100.3-2023 Section 5.3.22)
        /// </summary>
        public JobSheetsType[]? JobErrorSheetTypeSupported { get; set; }

        /// <summary>
        ///     job-error-sheet-when-supported (PWG 5100.3-2023 Section 5.3.23)
        /// </summary>
        public JobErrorSheetWhen[]? JobErrorSheetWhenSupported { get; set; }

        /// <summary>
        ///     job-message-to-operator-supported (PWG 5100.3-2023 Section 5.3.24)
        /// </summary>
        public bool? JobMessageToOperatorSupported { get; set; }

        /// <summary>
        ///     job-phone-number-default (PWG 5100.3-2023 Section 5.3.25)
        /// </summary>
        public string? JobPhoneNumberDefault { get; set; }

        /// <summary>
        ///     job-phone-number-scheme-supported (PWG 5100.3-2023 Section 5.3.26)
        /// </summary>
        public string[]? JobPhoneNumberSchemeSupported { get; set; }

        /// <summary>
        ///     job-phone-number-supported (PWG 5100.3-2023 Section 5.3.27)
        /// </summary>
        public bool? JobPhoneNumberSupported { get; set; }

        /// <summary>
        ///     job-recipient-name-supported (PWG 5100.3-2023 Section 5.3.28)
        /// </summary>
        public bool? JobRecipientNameSupported { get; set; }

        /// <summary>
        ///     job-sheet-message-supported (PWG 5100.3-2023)
        /// </summary>
        public bool? JobSheetMessageSupported { get; set; }

        /// <summary>
        ///     presentation-direction-number-up-default (PWG 5100.3-2023 Section 5.3.29)
        /// </summary>
        public PresentationDirectionNumberUp? PresentationDirectionNumberUpDefault { get; set; }

        /// <summary>
        ///     presentation-direction-number-up-supported (PWG 5100.3-2023 Section 5.3.30)
        /// </summary>
        public PresentationDirectionNumberUp[]? PresentationDirectionNumberUpSupported { get; set; }

        /// <summary>
        ///     separator-sheets-default (PWG 5100.3-2023 Section 5.3.31)
        /// </summary>
        public SeparatorSheets? SeparatorSheetsDefault { get; set; }

        /// <summary>
        ///     separator-sheets-supported (PWG 5100.3-2023 Section 5.3.32)
        /// </summary>
        public string[]? SeparatorSheetsSupported { get; set; }

        /// <summary>
        ///     separator-sheets-type-supported (PWG 5100.3-2023 Section 5.3.33)
        /// </summary>
        public SeparatorSheetsType[]? SeparatorSheetsTypeSupported { get; set; }

        /// <summary>
        ///     x-image-position-default (PWG 5100.3-2023 Section 5.3.34)
        /// </summary>
        public XImagePosition? XImagePositionDefault { get; set; }

        /// <summary>
        ///     x-image-position-supported (PWG 5100.3-2023 Section 5.3.35)
        /// </summary>
        public XImagePosition[]? XImagePositionSupported { get; set; }

        /// <summary>
        ///     x-image-shift-default (PWG 5100.3-2023 Section 5.3.36)
        /// </summary>
        public int? XImageShiftDefault { get; set; }

        /// <summary>
        ///     x-image-shift-supported (PWG 5100.3-2023 Section 5.3.37)
        /// </summary>
        public Range? XImageShiftSupported { get; set; }

        /// <summary>
        ///     x-side1-image-shift-default (PWG 5100.3-2023 Section 5.3.38)
        /// </summary>
        public int? XSide1ImageShiftDefault { get; set; }

        /// <summary>
        ///     x-side2-image-shift-default (PWG 5100.3-2023 Section 5.3.39)
        /// </summary>
        public int? XSide2ImageShiftDefault { get; set; }

        /// <summary>
        ///     y-image-position-default (PWG 5100.3-2023 Section 5.3.40)
        /// </summary>
        public YImagePosition? YImagePositionDefault { get; set; }

        /// <summary>
        ///     y-image-position-supported (PWG 5100.3-2023 Section 5.3.41)
        /// </summary>
        public YImagePosition[]? YImagePositionSupported { get; set; }

        /// <summary>
        ///     y-image-shift-default (PWG 5100.3-2023 Section 5.3.42)
        /// </summary>
        public int? YImageShiftDefault { get; set; }

        /// <summary>
        ///     y-image-shift-supported (PWG 5100.3-2023 Section 5.3.43)
        /// </summary>
        public Range? YImageShiftSupported { get; set; }

        /// <summary>
        ///     y-side1-image-shift-default (PWG 5100.3-2023 Section 5.3.44)
        /// </summary>
        public int? YSide1ImageShiftDefault { get; set; }

        /// <summary>
        ///     y-side2-image-shift-default (PWG 5100.3-2023 Section 5.3.45)
        /// </summary>
        public int? YSide2ImageShiftDefault { get; set; }
    }
}
