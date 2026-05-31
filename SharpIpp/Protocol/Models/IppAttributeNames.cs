using System;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Unified registry of all IPP and PWG attribute names.
    /// </summary>
    public static class IppAttributeNames
    {
    /// <summary>
    /// The accuracy units supported by the 3D Printer.
    /// See: PWG 5100.21-2019 Section 8.3.1
    /// </summary>
        public const string AccuracyUnitsSupported = "accuracy-units-supported";

        /// <summary>
        /// The attributes-charset attribute.
        /// </summary>
        public const string AttributesCharset = "attributes-charset";

        /// <summary>
        /// The attributes-natural-language attribute.
        /// </summary>
        public const string AttributesNaturalLanguage = "attributes-natural-language";

        /// <summary>
        /// The baling-type-supported attribute.
        /// </summary>
        public const string BalingTypeSupported = "baling-type-supported";

        /// <summary>
        /// The baling-when-supported attribute.
        /// </summary>
        public const string BalingWhenSupported = "baling-when-supported";

        /// <summary>
        /// The binding-reference-edge-supported attribute.
        /// </summary>
        public const string BindingReferenceEdgeSupported = "binding-reference-edge-supported";

        /// <summary>
        /// The binding-type-supported attribute.
        /// </summary>
        public const string BindingTypeSupported = "binding-type-supported";

        /// <summary>
        /// The chamber-humidity attribute.
        /// </summary>
        public const string ChamberHumidity = "chamber-humidity";

        /// <summary>
        /// The chamber-humidity-actual attribute.
        /// </summary>
        public const string ChamberHumidityActual = "chamber-humidity-actual";

        /// <summary>
        /// The chamber-humidity-current attribute.
        /// </summary>
        public const string ChamberHumidityCurrent = "chamber-humidity-current";

        /// <summary>
        /// The chamber-humidity-default attribute.
        /// </summary>
        public const string ChamberHumidityDefault = "chamber-humidity-default";

        /// <summary>
        /// The chamber-humidity-supported attribute.
        /// </summary>
        public const string ChamberHumiditySupported = "chamber-humidity-supported";

        /// <summary>
        /// The chamber-temperature attribute.
        /// </summary>
        public const string ChamberTemperature = "chamber-temperature";

        /// <summary>
        /// The chamber-temperature-actual attribute.
        /// </summary>
        public const string ChamberTemperatureActual = "chamber-temperature-actual";

        /// <summary>
        /// The chamber-temperature-current attribute.
        /// </summary>
        public const string ChamberTemperatureCurrent = "chamber-temperature-current";

        /// <summary>
        /// The chamber-temperature-default attribute.
        /// </summary>
        public const string ChamberTemperatureDefault = "chamber-temperature-default";

        /// <summary>
        /// The chamber-temperature-supported attribute.
        /// </summary>
        public const string ChamberTemperatureSupported = "chamber-temperature-supported";

        /// <summary>
        /// The charge-info-message attribute.
        /// </summary>
        public const string ChargeInfoMessage = "charge-info-message";

        /// <summary>
        /// charset-configured
        /// See: PWG 5100.22-2025 Section 7.2.1
        /// </summary>
        public const string CharsetConfigured = "charset-configured";

        /// <summary>
        /// charset-supported
        /// See: PWG 5100.22-2025 Section 7.2.2
        /// </summary>
        public const string CharsetSupported = "charset-supported";

        /// <summary>
        /// The client-info attribute.
        /// </summary>
        public const string ClientInfo = "client-info";

        /// <summary>
        /// The client-info-supported attribute.
        /// </summary>
        public const string ClientInfoSupported = "client-info-supported";

        /// <summary>
        /// The coating-sides-supported attribute.
        /// </summary>
        public const string CoatingSidesSupported = "coating-sides-supported";

        /// <summary>
        /// The coating-type-supported attribute.
        /// </summary>
        public const string CoatingTypeSupported = "coating-type-supported";

        /// <summary>
        /// The color-supported attribute.
        /// </summary>
        public const string ColorSupported = "color-supported";

        /// <summary>
        /// The compression attribute.
        /// </summary>
        public const string Compression = "compression";

        /// <summary>
        /// The compression-accepted attribute.
        /// </summary>
        public const string CompressionAccepted = "compression-accepted";

        /// <summary>
        /// The compression-default attribute.
        /// </summary>
        public const string CompressionDefault = "compression-default";

        /// <summary>
        /// The compression-supported attribute.
        /// </summary>
        public const string CompressionSupported = "compression-supported";

        /// <summary>
        /// The confirmation-sheet-print attribute.
        /// </summary>
        public const string ConfirmationSheetPrint = "confirmation-sheet-print";

        /// <summary>
        /// The confirmation-sheet-print-default attribute.
        /// </summary>
        public const string ConfirmationSheetPrintDefault = "confirmation-sheet-print-default";

        /// <summary>
        /// The copies attribute.
        /// </summary>
        public const string Copies = "copies";

        /// <summary>
        /// The copies-actual attribute.
        /// </summary>
        public const string CopiesActual = "copies-actual";

        /// <summary>
        /// The copies-default attribute.
        /// </summary>
        public const string CopiesDefault = "copies-default";

        /// <summary>
        /// The copies-supported attribute.
        /// </summary>
        public const string CopiesSupported = "copies-supported";

        /// <summary>
        /// The cover-back attribute.
        /// </summary>
        public const string CoverBack = "cover-back";

        /// <summary>
        /// The cover-back-actual attribute.
        /// </summary>
        public const string CoverBackActual = "cover-back-actual";

        /// <summary>
        /// The cover-back-default attribute.
        /// </summary>
        public const string CoverBackDefault = "cover-back-default";

        /// <summary>
        /// The cover-back-supported attribute.
        /// </summary>
        public const string CoverBackSupported = "cover-back-supported";

        /// <summary>
        /// The cover-front attribute.
        /// </summary>
        public const string CoverFront = "cover-front";

        /// <summary>
        /// The cover-front-actual attribute.
        /// </summary>
        public const string CoverFrontActual = "cover-front-actual";

        /// <summary>
        /// The cover-front-default attribute.
        /// </summary>
        public const string CoverFrontDefault = "cover-front-default";

        /// <summary>
        /// The cover-front-supported attribute.
        /// </summary>
        public const string CoverFrontSupported = "cover-front-supported";

        /// <summary>
        /// The cover-sheet-info attribute.
        /// </summary>
        public const string CoverSheetInfo = "cover-sheet-info";

        /// <summary>
        /// The cover-sheet-info-default attribute.
        /// </summary>
        public const string CoverSheetInfoDefault = "cover-sheet-info-default";

        /// <summary>
        /// The cover-sheet-info-supported attribute.
        /// </summary>
        public const string CoverSheetInfoSupported = "cover-sheet-info-supported";

        /// <summary>
        /// The cover-type-supported attribute.
        /// </summary>
        public const string CoverTypeSupported = "cover-type-supported";

        /// <summary>
        /// The covering-name-supported attribute.
        /// </summary>
        public const string CoveringNameSupported = "covering-name-supported";

        /// <summary>
        /// The current-page-order attribute.
        /// </summary>
        public const string CurrentPageOrder = "current-page-order";

        /// <summary>
        /// The date-time-at-completed attribute.
        /// </summary>
        public const string DateTimeAtCompleted = "date-time-at-completed";

        /// <summary>
        /// The date-time-at-completed-estimated attribute.
        /// </summary>
        public const string DateTimeAtCompletedEstimated = "date-time-at-completed-estimated";

        /// <summary>
        /// The date-time-at-creation attribute.
        /// </summary>
        public const string DateTimeAtCreation = "date-time-at-creation";

        /// <summary>
        /// The date-time-at-processing attribute.
        /// </summary>
        public const string DateTimeAtProcessing = "date-time-at-processing";

        /// <summary>
        /// The date-time-at-processing-estimated attribute.
        /// </summary>
        public const string DateTimeAtProcessingEstimated = "date-time-at-processing-estimated";

        /// <summary>
        /// The destination-accesses attribute.
        /// </summary>
        public const string DestinationAccesses = "destination-accesses";

        /// <summary>
        /// The destination-accesses-supported attribute.
        /// </summary>
        public const string DestinationAccessesSupported = "destination-accesses-supported";

        /// <summary>
        /// The destination-statuses attribute.
        /// </summary>
        public const string DestinationStatuses = "destination-statuses";

        /// <summary>
        /// The destination-uri-ready attribute.
        /// </summary>
        public const string DestinationUriReady = "destination-uri-ready";

        /// <summary>
        /// The destination-uri-schemes-supported attribute.
        /// </summary>
        public const string DestinationUriSchemesSupported = "destination-uri-schemes-supported";

        /// <summary>
        /// The destination-uris attribute.
        /// </summary>
        public const string DestinationUris = "destination-uris";

        /// <summary>
        /// The destination-uris-supported attribute.
        /// </summary>
        public const string DestinationUrisSupported = "destination-uris-supported";

        /// <summary>
        /// The detailed-status-message attribute.
        /// </summary>
        public const string DetailedStatusMessage = "detailed-status-message";

        /// <summary>
        /// The detailed-status-messages attribute.
        /// </summary>
        public const string DetailedStatusMessages = "detailed-status-messages";

        /// <summary>
        /// The document-access attribute.
        /// </summary>
        public const string DocumentAccess = "document-access";

        /// <summary>
        /// The document-access-error attribute.
        /// </summary>
        public const string DocumentAccessError = "document-access-error";

        /// <summary>
        /// The document-access-errors attribute.
        /// </summary>
        public const string DocumentAccessErrors = "document-access-errors";

        /// <summary>
        /// The document-access-supported attribute.
        /// </summary>
        public const string DocumentAccessSupported = "document-access-supported";

        /// <summary>
        /// The document-charset attribute.
        /// </summary>
        public const string DocumentCharset = "document-charset";

        /// <summary>
        /// The document-charset-default attribute.
        /// </summary>
        public const string DocumentCharsetDefault = "document-charset-default";

        /// <summary>
        /// The document-charset-supported attribute.
        /// </summary>
        public const string DocumentCharsetSupported = "document-charset-supported";

        /// <summary>
        /// The document-completed attribute.
        /// </summary>
        public const string DocumentCompleted = "document-completed";

        /// <summary>
        /// The document-config-changed attribute.
        /// </summary>
        public const string DocumentConfigChanged = "document-config-changed";

        /// <summary>
        /// The document-created attribute.
        /// </summary>
        public const string DocumentCreated = "document-created";

        /// <summary>
        /// The document-creation-attributes-supported Printer Description attribute name.
        /// See: PWG 5100.5-2024 Section 6.5.1
        /// </summary>
        public const string DocumentCreationAttributesSupported = "document-creation-attributes-supported";

        /// <summary>
        /// The document-data-get-interval attribute.
        /// </summary>
        public const string DocumentDataGetInterval = "document-data-get-interval";

        /// <summary>
        /// The document-data-wait attribute.
        /// </summary>
        public const string DocumentDataWait = "document-data-wait";

        /// <summary>
        /// The document-description attribute.
        /// </summary>
        public const string DocumentDescription = "document-description";

        /// <summary>
        /// The document-digital-signature attribute.
        /// </summary>
        [Obsolete("The 'document-digital-signature' attribute is deprecated. See PWG 5100.7-2023 Section 6.2.1.")]
        public const string DocumentDigitalSignature = "document-digital-signature";

        /// <summary>
        /// The document-format attribute.
        /// </summary>
        public const string DocumentFormat = "document-format";

        /// <summary>
        /// The document-format-accepted attribute.
        /// </summary>
        public const string DocumentFormatAccepted = "document-format-accepted";

        /// <summary>
        /// The document-format-default attribute.
        /// </summary>
        public const string DocumentFormatDefault = "document-format-default";

        /// <summary>
        /// The document-format-details attribute.
        /// </summary>
        public const string DocumentFormatDetails = "document-format-details";

        /// <summary>
        /// The document-format-details-detected attribute.
        /// </summary>
        public const string DocumentFormatDetailsDetected = "document-format-details-detected";

        /// <summary>
        /// The document-format-details-supported attribute.
        /// </summary>
        public const string DocumentFormatDetailsSupported = "document-format-details-supported";

        /// <summary>
        /// The document-format-detected attribute.
        /// </summary>
        public const string DocumentFormatDetected = "document-format-detected";

        /// <summary>
        /// The document-format-ready attribute.
        /// </summary>
        public const string DocumentFormatReady = "document-format-ready";

        /// <summary>
        /// document-format-supported
        /// See: PWG 5100.22-2025 Section 7.2.3
        /// </summary>
        public const string DocumentFormatSupported = "document-format-supported";

        /// <summary>
        /// The document-format-version attribute.
        /// </summary>
        [Obsolete("The 'document-format-version' attribute is deprecated. See PWG 5100.7-2023 Section 6.2.1.")]
        public const string DocumentFormatVersion = "document-format-version";

        /// <summary>
        /// The document-format-version-detected attribute.
        /// </summary>
        [Obsolete("The 'document-format-version-detected' attribute is deprecated. See PWG 5100.7-2023 Section 6.2.1.")]
        public const string DocumentFormatVersionDetected = "document-format-version-detected";

        /// <summary>
        /// The document-job-id attribute.
        /// </summary>
        public const string DocumentJobId = "document-job-id";

        /// <summary>
        /// The document-job-uri attribute.
        /// </summary>
        public const string DocumentJobUri = "document-job-uri";

        /// <summary>
        /// The document-message attribute.
        /// </summary>
        public const string DocumentMessage = "document-message";

        /// <summary>
        /// Arbitrary metadata associated with the document.
        /// See: PWG 5100.13-2023 Section 6.3.1
        /// </summary>
        public const string DocumentMetadata = "document-metadata";

        /// <summary>
        /// The document-name attribute.
        /// </summary>
        public const string DocumentName = "document-name";

        /// <summary>
        /// The document-natural-language attribute.
        /// </summary>
        public const string DocumentNaturalLanguage = "document-natural-language";

        /// <summary>
        /// The document-natural-language-default attribute.
        /// </summary>
        public const string DocumentNaturalLanguageDefault = "document-natural-language-default";

        /// <summary>
        /// The document-natural-language-supported attribute.
        /// </summary>
        public const string DocumentNaturalLanguageSupported = "document-natural-language-supported";

        /// <summary>
        /// The document-number attribute.
        /// </summary>
        public const string DocumentNumber = "document-number";

        /// <summary>
        /// The document-overrides attribute.
        /// </summary>
        public const string DocumentOverrides = "document-overrides";

        /// <summary>
        /// The document-password attribute.
        /// </summary>
        public const string DocumentPassword = "document-password";

        /// <summary>
        /// The document-password-supported attribute.
        /// </summary>
        public const string DocumentPasswordSupported = "document-password-supported";

        /// <summary>
        /// The document-printer-uri attribute.
        /// </summary>
        public const string DocumentPrinterUri = "document-printer-uri";

        /// <summary>
        /// The document-resource-ids attribute.
        /// </summary>
        public const string DocumentResourceIds = "document-resource-ids";

        /// <summary>
        /// The document-state attribute.
        /// </summary>
        public const string DocumentState = "document-state";

        /// <summary>
        /// The document-state-changed attribute.
        /// </summary>
        public const string DocumentStateChanged = "document-state-changed";

        /// <summary>
        /// The document-state-message attribute.
        /// </summary>
        public const string DocumentStateMessage = "document-state-message";

        /// <summary>
        /// The document-state-reasons attribute.
        /// </summary>
        public const string DocumentStateReasons = "document-state-reasons";

        /// <summary>
        /// The document-stopped attribute.
        /// </summary>
        public const string DocumentStopped = "document-stopped";

        /// <summary>
        /// The document-template attribute.
        /// </summary>
        public const string DocumentTemplate = "document-template";

        /// <summary>
        /// The document-uri attribute.
        /// </summary>
        public const string DocumentUri = "document-uri";

        /// <summary>
        /// The errors-count attribute.
        /// </summary>
        public const string ErrorsCount = "errors-count";

        /// <summary>
        /// The fetch-document-attributes-supported attribute.
        /// </summary>
        public const string FetchDocumentAttributesSupported = "fetch-document-attributes-supported";

        /// <summary>
        /// The fetch-status-code attribute.
        /// </summary>
        public const string FetchStatusCode = "fetch-status-code";

        /// <summary>
        /// The fetch-status-message attribute.
        /// </summary>
        public const string FetchStatusMessage = "fetch-status-message";

        /// <summary>
        /// The finishing-template-supported attribute.
        /// </summary>
        public const string FinishingTemplateSupported = "finishing-template-supported";

        /// <summary>
        /// The finishings attribute.
        /// </summary>
        public const string Finishings = "finishings";

        /// <summary>
        /// The finishings-actual attribute.
        /// </summary>
        public const string FinishingsActual = "finishings-actual";

        /// <summary>
        /// The finishings-col attribute.
        /// </summary>
        public const string FinishingsCol = "finishings-col";

        /// <summary>
        /// The finishings-col-actual attribute.
        /// </summary>
        public const string FinishingsColActual = "finishings-col-actual";

        /// <summary>
        /// The finishings-col-database attribute.
        /// </summary>
        public const string FinishingsColDatabase = "finishings-col-database";

        /// <summary>
        /// The finishings-col-default attribute.
        /// </summary>
        public const string FinishingsColDefault = "finishings-col-default";

        /// <summary>
        /// The finishings-col-ready attribute.
        /// </summary>
        public const string FinishingsColReady = "finishings-col-ready";

        /// <summary>
        /// The finishings-col-supported attribute.
        /// </summary>
        public const string FinishingsColSupported = "finishings-col-supported";

        /// <summary>
        /// The finishings-default attribute.
        /// </summary>
        public const string FinishingsDefault = "finishings-default";

        /// <summary>
        /// The finishings-supported attribute.
        /// </summary>
        public const string FinishingsSupported = "finishings-supported";

        /// <summary>
        /// The first-index attribute.
        /// </summary>
        public const string FirstIndex = "first-index";

        /// <summary>
        /// The first-printer-name attribute.
        /// </summary>
        public const string FirstPrinterName = "first-printer-name";

        /// <summary>
        /// The folding-direction-supported attribute.
        /// </summary>
        public const string FoldingDirectionSupported = "folding-direction-supported";

        /// <summary>
        /// The folding-offset-supported attribute.
        /// </summary>
        public const string FoldingOffsetSupported = "folding-offset-supported";

        /// <summary>
        /// The folding-reference-edge-supported attribute.
        /// </summary>
        public const string FoldingReferenceEdgeSupported = "folding-reference-edge-supported";

        /// <summary>
        /// The force-front-side attribute.
        /// </summary>
        public const string ForceFrontSide = "force-front-side";

        /// <summary>
        /// The force-front-side-supported attribute.
        /// </summary>
        public const string ForceFrontSideSupported = "force-front-side-supported";

        /// <summary>
        /// The from-name-supported attribute.
        /// </summary>
        public const string FromNameSupported = "from-name-supported";

        /// <summary>
        /// generated-natural-language-supported
        /// See: PWG 5100.22-2025 Section 7.2.9
        /// </summary>
        public const string GeneratedNaturalLanguageSupported = "generated-natural-language-supported";

        /// <summary>
        /// The identify-actions attribute.
        /// </summary>
        public const string IdentifyActions = "identify-actions";

        /// <summary>
        /// The image-orientation attribute.
        /// </summary>
        public const string ImageOrientation = "image-orientation";

        /// <summary>
        /// The image-orientation-default attribute.
        /// </summary>
        public const string ImageOrientationDefault = "image-orientation-default";

        /// <summary>
        /// The image-orientation-supported attribute.
        /// </summary>
        public const string ImageOrientationSupported = "image-orientation-supported";

        /// <summary>
        /// The imposition-template attribute.
        /// </summary>
        public const string ImpositionTemplate = "imposition-template";

        /// <summary>
        /// The imposition-template-actual attribute.
        /// </summary>
        public const string ImpositionTemplateActual = "imposition-template-actual";

        /// <summary>
        /// The imposition-template-default attribute.
        /// </summary>
        public const string ImpositionTemplateDefault = "imposition-template-default";

        /// <summary>
        /// The imposition-template-supported attribute.
        /// </summary>
        public const string ImpositionTemplateSupported = "imposition-template-supported";

        /// <summary>
        /// The impressions attribute.
        /// </summary>
        public const string Impressions = "impressions";

        /// <summary>
        /// The impressions-completed attribute.
        /// </summary>
        public const string ImpressionsCompleted = "impressions-completed";

        /// <summary>
        /// The input-attributes attribute.
        /// </summary>
        public const string InputAttributes = "input-attributes";

        /// <summary>
        /// The input-attributes-actual attribute.
        /// </summary>
        public const string InputAttributesActual = "input-attributes-actual";

        /// <summary>
        /// The input-attributes-default attribute.
        /// </summary>
        public const string InputAttributesDefault = "input-attributes-default";

        /// <summary>
        /// The input-attributes-supported attribute.
        /// </summary>
        public const string InputAttributesSupported = "input-attributes-supported";

    /// The input-auto-exposure attribute.
        public const string InputAutoExposure = "input-auto-exposure";

    /// The input-auto-scaling attribute.
        public const string InputAutoScaling = "input-auto-scaling";

    /// The input-auto-skew-correction attribute.
        public const string InputAutoSkewCorrection = "input-auto-skew-correction";

    /// The input-brightness attribute.
        public const string InputBrightness = "input-brightness";

    /// The input-color-mode attribute.
        public const string InputColorMode = "input-color-mode";

        /// <summary>
        /// The input-color-mode-supported attribute.
        /// </summary>
        public const string InputColorModeSupported = "input-color-mode-supported";

    /// The input-content-type attribute.
        public const string InputContentType = "input-content-type";

        /// <summary>
        /// The input-content-type-supported attribute.
        /// </summary>
        public const string InputContentTypeSupported = "input-content-type-supported";

    /// The input-contrast attribute.
        public const string InputContrast = "input-contrast";

    /// The input-film-scan-mode attribute.
        public const string InputFilmScanMode = "input-film-scan-mode";

        /// <summary>
        /// The input-film-scan-mode-supported attribute.
        /// </summary>
        public const string InputFilmScanModeSupported = "input-film-scan-mode-supported";

    /// The input-images-to-transfer attribute.
        public const string InputImagesToTransfer = "input-images-to-transfer";

    /// The input-media attribute.
        public const string InputMedia = "input-media";

        /// <summary>
        /// The input-media-supported attribute.
        /// </summary>
        public const string InputMediaSupported = "input-media-supported";

    /// The input-orientation-requested attribute.
        public const string InputOrientationRequested = "input-orientation-requested";

        /// <summary>
        /// The input-orientation-requested-supported attribute.
        /// </summary>
        public const string InputOrientationRequestedSupported = "input-orientation-requested-supported";

    /// The input-quality attribute.
        public const string InputQuality = "input-quality";

        /// <summary>
        /// The input-quality-supported attribute.
        /// </summary>
        public const string InputQualitySupported = "input-quality-supported";

    /// The input-resolution attribute.
        public const string InputResolution = "input-resolution";

        /// <summary>
        /// The input-resolution-supported attribute.
        /// </summary>
        public const string InputResolutionSupported = "input-resolution-supported";

    /// The input-scaling-height attribute.
        public const string InputScalingHeight = "input-scaling-height";

    /// The input-scaling-width attribute.
        public const string InputScalingWidth = "input-scaling-width";

    /// The input-sharpness attribute.
        public const string InputSharpness = "input-sharpness";

    /// The input-sides attribute.
        public const string InputSides = "input-sides";

        /// <summary>
        /// The input-sides-supported attribute.
        /// </summary>
        public const string InputSidesSupported = "input-sides-supported";

    /// The input-source attribute.
        public const string InputSource = "input-source";

        /// <summary>
        /// The input-source-supported attribute.
        /// </summary>
        public const string InputSourceSupported = "input-source-supported";

        /// <summary>
        /// The insert-count-supported attribute.
        /// </summary>
        public const string InsertCountSupported = "insert-count-supported";

        /// <summary>
        /// The insert-sheet attribute.
        /// </summary>
        public const string InsertSheet = "insert-sheet";

        /// <summary>
        /// The insert-sheet-actual attribute.
        /// </summary>
        public const string InsertSheetActual = "insert-sheet-actual";

        /// <summary>
        /// The insert-sheet-default attribute.
        /// </summary>
        public const string InsertSheetDefault = "insert-sheet-default";

        /// <summary>
        /// The insert-sheet-supported attribute.
        /// </summary>
        public const string InsertSheetSupported = "insert-sheet-supported";

        /// <summary>
        /// The ipp-attribute-fidelity attribute.
        /// </summary>
        public const string IppAttributeFidelity = "ipp-attribute-fidelity";

        /// <summary>
        /// ipp-features-supported
        /// See: PWG 5100.22-2025 Section 7.2.5
        /// </summary>
        public const string IppFeaturesSupported = "ipp-features-supported";

        /// <summary>
        /// The IPP client event lifetime in seconds.
        /// See: PWG 5100.22-2025 Section 7.1.25
        /// </summary>
        public const string IppGetEventLife = "ippget-event-life";

        /// <summary>
        /// ipp-versions-supported
        /// See: PWG 5100.22-2025 Section 7.2.6
        /// </summary>
        public const string IppVersionsSupported = "ipp-versions-supported";

        /// <summary>
        /// The job-account-id attribute.
        /// </summary>
        public const string JobAccountId = "job-account-id";

        /// <summary>
        /// The job-account-id-actual attribute.
        /// </summary>
        public const string JobAccountIdActual = "job-account-id-actual";

        /// <summary>
        /// The job-account-id-default attribute.
        /// </summary>
        public const string JobAccountIdDefault = "job-account-id-default";

        /// <summary>
        /// The job-account-id-supported attribute.
        /// </summary>
        public const string JobAccountIdSupported = "job-account-id-supported";

        /// <summary>
        /// The job-account-type attribute.
        /// </summary>
        public const string JobAccountType = "job-account-type";

        /// <summary>
        /// The job-account-type-default attribute.
        /// </summary>
        public const string JobAccountTypeDefault = "job-account-type-default";

        /// <summary>
        /// The job-account-type-supported attribute.
        /// </summary>
        public const string JobAccountTypeSupported = "job-account-type-supported";

        /// <summary>
        /// The job-accounting-output-bin-supported attribute.
        /// </summary>
        public const string JobAccountingOutputBinSupported = "job-accounting-output-bin-supported";

        /// <summary>
        /// The job-accounting-sheets attribute.
        /// </summary>
        public const string JobAccountingSheets = "job-accounting-sheets";

        /// <summary>
        /// The job-accounting-sheets-actual attribute.
        /// </summary>
        public const string JobAccountingSheetsActual = "job-accounting-sheets-actual";

        /// <summary>
        /// The job-accounting-sheets-default attribute.
        /// </summary>
        public const string JobAccountingSheetsDefault = "job-accounting-sheets-default";

        /// <summary>
        /// The job-accounting-sheets-supported attribute.
        /// </summary>
        public const string JobAccountingSheetsSupported = "job-accounting-sheets-supported";

        /// <summary>
        /// The job-accounting-sheets-type-supported attribute.
        /// </summary>
        public const string JobAccountingSheetsTypeSupported = "job-accounting-sheets-type-supported";

        /// <summary>
        /// The job-accounting-user-id attribute.
        /// </summary>
        public const string JobAccountingUserId = "job-accounting-user-id";

        /// <summary>
        /// The job-accounting-user-id-actual attribute.
        /// </summary>
        public const string JobAccountingUserIdActual = "job-accounting-user-id-actual";

        /// <summary>
        /// The job-accounting-user-id-default attribute.
        /// </summary>
        public const string JobAccountingUserIdDefault = "job-accounting-user-id-default";

        /// <summary>
        /// The job-accounting-user-id-supported attribute.
        /// </summary>
        public const string JobAccountingUserIdSupported = "job-accounting-user-id-supported";

        /// <summary>
        /// The job-authorization-uri attribute.
        /// </summary>
        public const string JobAuthorizationUri = "job-authorization-uri";

        /// <summary>
        /// The job-authorization-uri-supported attribute.
        /// </summary>
        public const string JobAuthorizationUriSupported = "job-authorization-uri-supported";

        /// <summary>
        /// The job-cancel-after attribute.
        /// </summary>
        public const string JobCancelAfter = "job-cancel-after";

        /// <summary>
        /// The job-cancel-after-default attribute.
        /// </summary>
        public const string JobCancelAfterDefault = "job-cancel-after-default";

        /// <summary>
        /// The job-cancel-after-supported attribute.
        /// </summary>
        public const string JobCancelAfterSupported = "job-cancel-after-supported";

        /// <summary>
        /// The job-charge-info attribute.
        /// </summary>
        public const string JobChargeInfo = "job-charge-info";

        /// <summary>
        /// The job-charge-info-uri attribute.
        /// </summary>
        public const string JobChargeInfoUri = "job-charge-info-uri";

        /// <summary>
        /// The job-complete-before attribute.
        /// </summary>
        public const string JobCompleteBefore = "job-complete-before";

        /// <summary>
        /// The job-complete-before-supported attribute.
        /// </summary>
        public const string JobCompleteBeforeSupported = "job-complete-before-supported";

        /// <summary>
        /// The job-complete-before-time attribute.
        /// </summary>
        public const string JobCompleteBeforeTime = "job-complete-before-time";

        /// <summary>
        /// The job-complete-before-time-supported attribute.
        /// </summary>
        public const string JobCompleteBeforeTimeSupported = "job-complete-before-time-supported";

        /// <summary>
        /// The job-constraints-supported attribute.
        /// </summary>
        public const string JobConstraintsSupported = "job-constraints-supported";

        /// <summary>
        /// The job-copies attribute.
        /// </summary>
        public const string JobCopies = "job-copies";

        /// <summary>
        /// The job-copies-actual attribute.
        /// </summary>
        public const string JobCopiesActual = "job-copies-actual";

        /// <summary>
        /// The job-cover-back attribute.
        /// </summary>
        public const string JobCoverBack = "job-cover-back";

        /// <summary>
        /// The job-cover-front attribute.
        /// </summary>
        public const string JobCoverFront = "job-cover-front";

        /// <summary>
        /// The job-creation-attributes-supported attribute.
        /// </summary>
        public const string JobCreationAttributesSupported = "job-creation-attributes-supported";

        /// <summary>
        /// The job-delay-output-until attribute.
        /// </summary>
        public const string JobDelayOutputUntil = "job-delay-output-until";

        /// <summary>
        /// The job-delay-output-until-default attribute.
        /// </summary>
        public const string JobDelayOutputUntilDefault = "job-delay-output-until-default";

        /// <summary>
        /// The job-delay-output-until-supported attribute.
        /// </summary>
        public const string JobDelayOutputUntilSupported = "job-delay-output-until-supported";

        /// <summary>
        /// The job-delay-output-until-time attribute.
        /// </summary>
        public const string JobDelayOutputUntilTime = "job-delay-output-until-time";

        /// <summary>
        /// The job-delay-output-until-time-supported attribute.
        /// </summary>
        public const string JobDelayOutputUntilTimeSupported = "job-delay-output-until-time-supported";

        /// <summary>
        /// The job-destination-spooling-supported attribute.
        /// </summary>
        public const string JobDestinationSpoolingSupported = "job-destination-spooling-supported";

        /// <summary>
        /// The job-detailed-status-messages attribute.
        /// </summary>
        public const string JobDetailedStatusMessages = "job-detailed-status-messages";

        /// <summary>
        /// The job-document-access-errors attribute.
        /// </summary>
        public const string JobDocumentAccessErrors = "job-document-access-errors";

        /// <summary>
        /// The job-error-action attribute.
        /// </summary>
        public const string JobErrorAction = "job-error-action";

        /// <summary>
        /// The job-error-sheet attribute.
        /// </summary>
        public const string JobErrorSheet = "job-error-sheet";

        /// <summary>
        /// The job-error-sheet-actual attribute.
        /// </summary>
        public const string JobErrorSheetActual = "job-error-sheet-actual";

        /// <summary>
        /// The job-error-sheet-default attribute.
        /// </summary>
        public const string JobErrorSheetDefault = "job-error-sheet-default";

        /// <summary>
        /// The job-error-sheet-supported attribute.
        /// </summary>
        public const string JobErrorSheetSupported = "job-error-sheet-supported";

        /// <summary>
        /// The job-error-sheet-type-supported attribute.
        /// </summary>
        public const string JobErrorSheetTypeSupported = "job-error-sheet-type-supported";

        /// <summary>
        /// The job-error-sheet-when-supported attribute.
        /// </summary>
        public const string JobErrorSheetWhenSupported = "job-error-sheet-when-supported";

        /// <summary>
        /// The job-finishings attribute.
        /// </summary>
        public const string JobFinishings = "job-finishings";

        /// <summary>
        /// The job-finishings-col attribute.
        /// </summary>
        public const string JobFinishingsCol = "job-finishings-col";

        /// <summary>
        /// The job-history-attributes-configured attribute.
        /// </summary>
        public const string JobHistoryAttributesConfigured = "job-history-attributes-configured";

        /// <summary>
        /// The job-history-attributes-supported attribute.
        /// </summary>
        public const string JobHistoryAttributesSupported = "job-history-attributes-supported";

        /// <summary>
        /// The job-history-interval-configured attribute.
        /// </summary>
        public const string JobHistoryIntervalConfigured = "job-history-interval-configured";

        /// <summary>
        /// The job-history-interval-supported attribute.
        /// </summary>
        public const string JobHistoryIntervalSupported = "job-history-interval-supported";

        /// <summary>
        /// The job-hold-until attribute.
        /// </summary>
        public const string JobHoldUntil = "job-hold-until";

        /// <summary>
        /// The job-hold-until-actual attribute.
        /// </summary>
        public const string JobHoldUntilActual = "job-hold-until-actual";

        /// <summary>
        /// The job-hold-until-default attribute.
        /// </summary>
        public const string JobHoldUntilDefault = "job-hold-until-default";

        /// <summary>
        /// The job-hold-until-supported attribute.
        /// </summary>
        public const string JobHoldUntilSupported = "job-hold-until-supported";

        /// <summary>
        /// The job-hold-until-time attribute.
        /// </summary>
        public const string JobHoldUntilTime = "job-hold-until-time";

        /// <summary>
        /// The job-hold-until-time-supported attribute.
        /// </summary>
        public const string JobHoldUntilTimeSupported = "job-hold-until-time-supported";

        /// <summary>
        /// The job-id attribute.
        /// </summary>
        public const string JobId = "job-id";

        /// <summary>
        /// The job-ids attribute.
        /// </summary>
        public const string JobIds = "job-ids";

        /// <summary>
        /// The job-ids-supported attribute.
        /// </summary>
        public const string JobIdsSupported = "job-ids-supported";

        /// <summary>
        /// The job-impressions attribute.
        /// </summary>
        public const string JobImpressions = "job-impressions";

        /// <summary>
        /// The job-impressions-col attribute.
        /// </summary>
        public const string JobImpressionsCol = "job-impressions-col";

        /// <summary>
        /// The job-impressions-completed attribute.
        /// </summary>
        public const string JobImpressionsCompleted = "job-impressions-completed";

        /// <summary>
        /// The job-impressions-completed-col attribute.
        /// </summary>
        public const string JobImpressionsCompletedCol = "job-impressions-completed-col";

        /// <summary>
        /// The job-impressions-estimated attribute.
        /// </summary>
        public const string JobImpressionsEstimated = "job-impressions-estimated";

        /// <summary>
        /// The job-impressions-supported attribute.
        /// </summary>
        public const string JobImpressionsSupported = "job-impressions-supported";

        /// <summary>
        /// The job-k-octets attribute.
        /// </summary>
        public const string JobKOctets = "job-k-octets";

        /// <summary>
        /// The job-k-octets-completed attribute.
        /// </summary>
        public const string JobKOctetsCompleted = "job-k-octets-completed";

        /// <summary>
        /// The job-k-octets-processed attribute.
        /// </summary>
        public const string JobKOctetsProcessed = "job-k-octets-processed";

        /// <summary>
        /// The job-k-octets-supported attribute.
        /// </summary>
        public const string JobKOctetsSupported = "job-k-octets-supported";

        /// <summary>
        /// The job-mandatory-attributes attribute.
        /// </summary>
        public const string JobMandatoryAttributes = "job-mandatory-attributes";

        /// <summary>
        /// The job-mandatory-attributes-supported attribute.
        /// </summary>
        public const string JobMandatoryAttributesSupported = "job-mandatory-attributes-supported";

        /// <summary>
        /// The job-media-sheets attribute.
        /// </summary>
        public const string JobMediaSheets = "job-media-sheets";

        /// <summary>
        /// The job-media-sheets-col attribute.
        /// </summary>
        public const string JobMediaSheetsCol = "job-media-sheets-col";

        /// <summary>
        /// The job-media-sheets-completed attribute.
        /// </summary>
        public const string JobMediaSheetsCompleted = "job-media-sheets-completed";

        /// <summary>
        /// The job-media-sheets-completed-col attribute.
        /// </summary>
        public const string JobMediaSheetsCompletedCol = "job-media-sheets-completed-col";

        /// <summary>
        /// The job-media-sheets-supported attribute.
        /// </summary>
        public const string JobMediaSheetsSupported = "job-media-sheets-supported";

        /// <summary>
        /// The job-message-from-operator attribute.
        /// </summary>
        public const string JobMessageFromOperator = "job-message-from-operator";

        /// <summary>
        /// The job-message-to-operator attribute.
        /// </summary>
        public const string JobMessageToOperator = "job-message-to-operator";

        /// <summary>
        /// The job-message-to-operator-actual attribute.
        /// </summary>
        public const string JobMessageToOperatorActual = "job-message-to-operator-actual";

        /// <summary>
        /// The job-message-to-operator-supported attribute.
        /// </summary>
        public const string JobMessageToOperatorSupported = "job-message-to-operator-supported";

        /// <summary>
        /// The job-more-info attribute.
        /// </summary>
        public const string JobMoreInfo = "job-more-info";

        /// <summary>
        /// The job-name attribute.
        /// </summary>
        public const string JobName = "job-name";

        /// <summary>
        /// The job-originating-user-name attribute.
        /// </summary>
        public const string JobOriginatingUserName = "job-originating-user-name";

        /// <summary>
        /// The job-pages attribute.
        /// </summary>
        public const string JobPages = "job-pages";

        /// <summary>
        /// The job-pages-col attribute.
        /// </summary>
        public const string JobPagesCol = "job-pages-col";

        /// <summary>
        /// The job-pages-completed attribute.
        /// </summary>
        public const string JobPagesCompleted = "job-pages-completed";

        /// <summary>
        /// The job-pages-completed-col attribute.
        /// </summary>
        public const string JobPagesCompletedCol = "job-pages-completed-col";

        /// <summary>
        /// The job-pages-completed-current-copy attribute.
        /// </summary>
        public const string JobPagesCompletedCurrentCopy = "job-pages-completed-current-copy";

        /// <summary>
        /// The job-pages-per-set attribute.
        /// </summary>
        public const string JobPagesPerSet = "job-pages-per-set";

        /// <summary>
        /// The job-pages-per-set-supported attribute.
        /// </summary>
        public const string JobPagesPerSetSupported = "job-pages-per-set-supported";

        /// <summary>
        /// The job-password attribute.
        /// </summary>
        public const string JobPassword = "job-password";

        /// <summary>
        /// The job-password-encryption attribute.
        /// </summary>
        public const string JobPasswordEncryption = "job-password-encryption";

        /// <summary>
        /// The job-password-encryption-supported attribute.
        /// </summary>
        public const string JobPasswordEncryptionSupported = "job-password-encryption-supported";

        /// <summary>
        /// The job-password-length-supported attribute.
        /// </summary>
        public const string JobPasswordLengthSupported = "job-password-length-supported";

        /// <summary>
        /// The job-password-supported attribute.
        /// </summary>
        public const string JobPasswordSupported = "job-password-supported";

        /// <summary>
        /// The job-phone-number attribute.
        /// </summary>
        public const string JobPhoneNumber = "job-phone-number";

        /// <summary>
        /// The job-phone-number-default attribute.
        /// </summary>
        public const string JobPhoneNumberDefault = "job-phone-number-default";

        /// <summary>
        /// The job-phone-number-scheme-supported attribute.
        /// </summary>
        public const string JobPhoneNumberSchemeSupported = "job-phone-number-scheme-supported";

        /// <summary>
        /// The job-phone-number-supported attribute.
        /// </summary>
        public const string JobPhoneNumberSupported = "job-phone-number-supported";

        /// <summary>
        /// The job-presets-supported attribute.
        /// </summary>
        public const string JobPresetsSupported = "job-presets-supported";

        /// <summary>
        /// The job-printer-up-time attribute.
        /// </summary>
        public const string JobPrinterUpTime = "job-printer-up-time";

        /// <summary>
        /// The job-printer-uri attribute.
        /// </summary>
        public const string JobPrinterUri = "job-printer-uri";

        /// <summary>
        /// The job-priority attribute.
        /// </summary>
        public const string JobPriority = "job-priority";

        /// <summary>
        /// The job-priority-actual attribute.
        /// </summary>
        public const string JobPriorityActual = "job-priority-actual";

        /// <summary>
        /// The job-priority-default attribute.
        /// </summary>
        public const string JobPriorityDefault = "job-priority-default";

        /// <summary>
        /// The job-priority-supported attribute.
        /// </summary>
        public const string JobPrioritySupported = "job-priority-supported";

        /// <summary>
        /// The job-processing-time attribute.
        /// </summary>
        public const string JobProcessingTime = "job-processing-time";

        /// <summary>
        /// The job-recipient-name attribute.
        /// </summary>
        public const string JobRecipientName = "job-recipient-name";

        /// <summary>
        /// The job-recipient-name-supported attribute.
        /// </summary>
        public const string JobRecipientNameSupported = "job-recipient-name-supported";

        /// <summary>
        /// The job-release-action attribute.
        /// </summary>
        public const string JobReleaseAction = "job-release-action";

        /// <summary>
        /// The job-resolvers-supported attribute.
        /// </summary>
        public const string JobResolversSupported = "job-resolvers-supported";

        /// <summary>
        /// The job-resource-ids attribute.
        /// </summary>
        public const string JobResourceIds = "job-resource-ids";

        /// <summary>
        /// The job-retain-until attribute.
        /// </summary>
        public const string JobRetainUntil = "job-retain-until";

        /// <summary>
        /// The job-retain-until-default attribute.
        /// </summary>
        public const string JobRetainUntilDefault = "job-retain-until-default";

        /// <summary>
        /// The job-retain-until-interval attribute.
        /// </summary>
        public const string JobRetainUntilInterval = "job-retain-until-interval";

        /// <summary>
        /// The job-retain-until-interval-default attribute.
        /// </summary>
        public const string JobRetainUntilIntervalDefault = "job-retain-until-interval-default";

        /// <summary>
        /// The job-retain-until-interval-supported attribute.
        /// </summary>
        public const string JobRetainUntilIntervalSupported = "job-retain-until-interval-supported";

        /// <summary>
        /// The job-retain-until-supported attribute.
        /// </summary>
        public const string JobRetainUntilSupported = "job-retain-until-supported";

        /// <summary>
        /// The job-retain-until-time attribute.
        /// </summary>
        public const string JobRetainUntilTime = "job-retain-until-time";

        /// <summary>
        /// The job-retain-until-time-supported attribute.
        /// </summary>
        public const string JobRetainUntilTimeSupported = "job-retain-until-time-supported";

        /// <summary>
        /// The job-save-disposition attribute.
        /// </summary>
        public const string JobSaveDisposition = "job-save-disposition";

        /// <summary>
        /// The job-save-disposition-default attribute.
        /// </summary>
        public const string JobSaveDispositionDefault = "job-save-disposition-default";

        /// <summary>
        /// The job-save-disposition-supported attribute.
        /// </summary>
        public const string JobSaveDispositionSupported = "job-save-disposition-supported";

        /// <summary>
        /// The job-sheet-message attribute.
        /// </summary>
        public const string JobSheetMessage = "job-sheet-message";

        /// <summary>
        /// The job-sheet-message-actual attribute.
        /// </summary>
        public const string JobSheetMessageActual = "job-sheet-message-actual";

        /// <summary>
        /// The job-sheet-message-supported attribute.
        /// </summary>
        public const string JobSheetMessageSupported = "job-sheet-message-supported";

        /// <summary>
        /// The job-sheets attribute.
        /// </summary>
        public const string JobSheets = "job-sheets";

        /// <summary>
        /// The job-sheets-actual attribute.
        /// </summary>
        public const string JobSheetsActual = "job-sheets-actual";

        /// <summary>
        /// The job-sheets-col attribute.
        /// </summary>
        public const string JobSheetsCol = "job-sheets-col";

        /// <summary>
        /// The job-sheets-col-default attribute.
        /// </summary>
        public const string JobSheetsColDefault = "job-sheets-col-default";

        /// <summary>
        /// The job-sheets-col-supported attribute.
        /// </summary>
        public const string JobSheetsColSupported = "job-sheets-col-supported";

        /// <summary>
        /// The job-sheets-default attribute.
        /// </summary>
        public const string JobSheetsDefault = "job-sheets-default";

        /// <summary>
        /// The job-sheets-supported attribute.
        /// </summary>
        public const string JobSheetsSupported = "job-sheets-supported";

        /// <summary>
        /// The job-spooling-supported attribute.
        /// </summary>
        public const string JobSpoolingSupported = "job-spooling-supported";

        /// <summary>
        /// The job-state attribute.
        /// </summary>
        public const string JobState = "job-state";

        /// <summary>
        /// The job-state-message attribute.
        /// </summary>
        public const string JobStateMessage = "job-state-message";

        /// <summary>
        /// The job-state-reasons attribute.
        /// </summary>
        public const string JobStateReasons = "job-state-reasons";

        /// <summary>
        /// The job-storage attribute.
        /// </summary>
        public const string JobStorage = "job-storage";

        /// <summary>
        /// The job-triggers-supported attribute.
        /// </summary>
        public const string JobTriggersSupported = "job-triggers-supported";

        /// <summary>
        /// The job-uri attribute.
        /// </summary>
        public const string JobUri = "job-uri";

        /// <summary>
        /// The jpeg-k-octets-supported attribute.
        /// </summary>
        public const string JpegKOctetsSupported = "jpeg-k-octets-supported";

        /// <summary>
        /// The jpeg-x-dimension-supported attribute.
        /// </summary>
        public const string JpegXDimensionSupported = "jpeg-x-dimension-supported";

        /// <summary>
        /// The jpeg-y-dimension-supported attribute.
        /// </summary>
        public const string JpegYDimensionSupported = "jpeg-y-dimension-supported";

        /// <summary>
        /// The k-octets attribute.
        /// </summary>
        public const string KOctets = "k-octets";

        /// <summary>
        /// The k-octets-processed attribute.
        /// </summary>
        public const string KOctetsProcessed = "k-octets-processed";

        /// <summary>
        /// The laminating-sides-supported attribute.
        /// </summary>
        public const string LaminatingSidesSupported = "laminating-sides-supported";

        /// <summary>
        /// The laminating-type-supported attribute.
        /// </summary>
        public const string LaminatingTypeSupported = "laminating-type-supported";

        /// <summary>
        /// The last-document attribute.
        /// </summary>
        public const string LastDocument = "last-document";

        /// <summary>
        /// The limit attribute.
        /// </summary>
        public const string Limit = "limit";

        /// <summary>
        /// The logo-uri-formats-supported attribute.
        /// </summary>
        public const string LogoUriFormatsSupported = "logo-uri-formats-supported";

        /// <summary>
        /// The logo-uri-schemes-supported attribute.
        /// </summary>
        public const string LogoUriSchemesSupported = "logo-uri-schemes-supported";

        /// <summary>
        /// The material-amount-units-supported attribute.
        /// </summary>
        public const string MaterialAmountUnitsSupported = "material-amount-units-supported";

        /// <summary>
        /// The material-diameter-supported attribute.
        /// </summary>
        public const string MaterialDiameterSupported = "material-diameter-supported";

        /// <summary>
        /// The material-nozzle-diameter-supported attribute.
        /// </summary>
        public const string MaterialNozzleDiameterSupported = "material-nozzle-diameter-supported";

        /// <summary>
        /// The material-purpose-supported attribute.
        /// </summary>
        public const string MaterialPurposeSupported = "material-purpose-supported";

        /// <summary>
        /// The material-rate-supported attribute.
        /// </summary>
        public const string MaterialRateSupported = "material-rate-supported";

        /// <summary>
        /// The material-rate-units-supported attribute.
        /// </summary>
        public const string MaterialRateUnitsSupported = "material-rate-units-supported";

        /// <summary>
        /// The material-shell-thickness-supported attribute.
        /// </summary>
        public const string MaterialShellThicknessSupported = "material-shell-thickness-supported";

        /// <summary>
        /// The material-temperature-supported attribute.
        /// </summary>
        public const string MaterialTemperatureSupported = "material-temperature-supported";

        /// <summary>
        /// The material-type-supported attribute.
        /// </summary>
        public const string MaterialTypeSupported = "material-type-supported";

        /// <summary>
        /// The materials-col attribute.
        /// </summary>
        public const string MaterialsCol = "materials-col";

        /// <summary>
        /// The materials-col-actual attribute.
        /// </summary>
        public const string MaterialsColActual = "materials-col-actual";

        /// <summary>
        /// The materials-col-database attribute.
        /// </summary>
        public const string MaterialsColDatabase = "materials-col-database";

        /// <summary>
        /// The materials-col-default attribute.
        /// </summary>
        public const string MaterialsColDefault = "materials-col-default";

        /// <summary>
        /// The materials-col-ready attribute.
        /// </summary>
        public const string MaterialsColReady = "materials-col-ready";

        /// <summary>
        /// The materials-col-supported attribute.
        /// </summary>
        public const string MaterialsColSupported = "materials-col-supported";

        /// <summary>
        /// The max-client-info-supported attribute.
        /// </summary>
        public const string MaxClientInfoSupported = "max-client-info-supported";

        /// <summary>
        /// The max-materials-col-supported attribute.
        /// </summary>
        public const string MaxMaterialsColSupported = "max-materials-col-supported";

        /// <summary>
        /// The max-page-ranges-supported attribute.
        /// </summary>
        public const string MaxPageRangesSupported = "max-page-ranges-supported";

        /// <summary>
        /// The media attribute.
        /// </summary>
        public const string Media = "media";

        /// <summary>
        /// The media-actual attribute.
        /// </summary>
        public const string MediaActual = "media-actual";

        /// <summary>
        /// The media-back-coating-supported attribute.
        /// </summary>
        public const string MediaBackCoatingSupported = "media-back-coating-supported";

        /// <summary>
        /// The media-bottom-margin-supported attribute.
        /// </summary>
        public const string MediaBottomMarginSupported = "media-bottom-margin-supported";

        /// <summary>
        /// The media-col attribute.
        /// </summary>
        public const string MediaCol = "media-col";

        /// <summary>
        /// The media-col-actual attribute.
        /// </summary>
        public const string MediaColActual = "media-col-actual";

        /// <summary>
        /// The media-col-database attribute.
        /// </summary>
        public const string MediaColDatabase = "media-col-database";

        /// <summary>
        /// The media-col-default attribute.
        /// </summary>
        public const string MediaColDefault = "media-col-default";

        /// <summary>
        /// The media-col-ready attribute.
        /// </summary>
        public const string MediaColReady = "media-col-ready";

        /// <summary>
        /// The media-col-supported attribute.
        /// </summary>
        public const string MediaColSupported = "media-col-supported";

        /// <summary>
        /// The media-color-supported attribute.
        /// </summary>
        public const string MediaColorSupported = "media-color-supported";

        /// <summary>
        /// The media-default attribute.
        /// </summary>
        public const string MediaDefault = "media-default";

        /// <summary>
        /// The media-front-coating-supported attribute.
        /// </summary>
        public const string MediaFrontCoatingSupported = "media-front-coating-supported";

        /// <summary>
        /// The media-grain-supported attribute.
        /// </summary>
        public const string MediaGrainSupported = "media-grain-supported";

        /// <summary>
        /// The media-hole-count-supported attribute.
        /// </summary>
        public const string MediaHoleCountSupported = "media-hole-count-supported";

        /// <summary>
        /// The media-input-tray-check attribute.
        /// </summary>
        public const string MediaInputTrayCheck = "media-input-tray-check";

        /// <summary>
        /// The media-input-tray-check-actual attribute.
        /// </summary>
        public const string MediaInputTrayCheckActual = "media-input-tray-check-actual";

        /// <summary>
        /// The media-key-supported attribute.
        /// </summary>
        public const string MediaKeySupported = "media-key-supported";

        /// <summary>
        /// The media-left-margin-supported attribute.
        /// </summary>
        public const string MediaLeftMarginSupported = "media-left-margin-supported";

        /// <summary>
        /// The media-order-count-supported attribute.
        /// </summary>
        public const string MediaOrderCountSupported = "media-order-count-supported";

        /// <summary>
        /// The media-pre-printed-supported attribute.
        /// </summary>
        public const string MediaPrePrintedSupported = "media-pre-printed-supported";

        /// <summary>
        /// The media-ready attribute.
        /// </summary>
        public const string MediaReady = "media-ready";

        /// <summary>
        /// The media-recycled-supported attribute.
        /// </summary>
        public const string MediaRecycledSupported = "media-recycled-supported";

        /// <summary>
        /// The media-right-margin-supported attribute.
        /// </summary>
        public const string MediaRightMarginSupported = "media-right-margin-supported";

        /// <summary>
        /// The media-sheets attribute.
        /// </summary>
        public const string MediaSheets = "media-sheets";

        /// <summary>
        /// The media-sheets-completed attribute.
        /// </summary>
        public const string MediaSheetsCompleted = "media-sheets-completed";

        /// <summary>
        /// The media-size-supported attribute.
        /// </summary>
        public const string MediaSizeSupported = "media-size-supported";

        /// <summary>
        /// The media-source attribute.
        /// </summary>
        public const string MediaSource = "media-source";

        /// <summary>
        /// The media-source-feed-direction attribute.
        /// </summary>
        public const string MediaSourceFeedDirection = "media-source-feed-direction";

        /// <summary>
        /// The media-source-feed-orientation attribute.
        /// </summary>
        public const string MediaSourceFeedOrientation = "media-source-feed-orientation";

        /// <summary>
        /// The media-source-properties attribute.
        /// </summary>
        public const string MediaSourceProperties = "media-source-properties";

        /// <summary>
        /// The media-source-supported attribute.
        /// </summary>
        public const string MediaSourceSupported = "media-source-supported";

        /// <summary>
        /// The media-supported attribute.
        /// </summary>
        public const string MediaSupported = "media-supported";

        /// <summary>
        /// The media-thickness attribute.
        /// </summary>
        public const string MediaThickness = "media-thickness";

        /// <summary>
        /// The media-thickness-supported attribute.
        /// </summary>
        public const string MediaThicknessSupported = "media-thickness-supported";

        /// <summary>
        /// The media-tooth-supported attribute.
        /// </summary>
        public const string MediaToothSupported = "media-tooth-supported";

        /// <summary>
        /// The media-top-margin-supported attribute.
        /// </summary>
        public const string MediaTopMarginSupported = "media-top-margin-supported";

        /// <summary>
        /// The media-type-supported attribute.
        /// </summary>
        public const string MediaTypeSupported = "media-type-supported";

        /// <summary>
        /// The media-weight-metric-supported attribute.
        /// </summary>
        public const string MediaWeightMetricSupported = "media-weight-metric-supported";

        /// <summary>
        /// The message attribute.
        /// </summary>
        public const string Message = "message";

        /// <summary>
        /// The message-supported attribute.
        /// </summary>
        public const string MessageSupported = "message-supported";

        /// <summary>
        /// The more-info attribute.
        /// </summary>
        public const string MoreInfo = "more-info";

        /// <summary>
        /// The multiple-destination-uris-supported attribute.
        /// </summary>
        public const string MultipleDestinationUrisSupported = "multiple-destination-uris-supported";

        /// <summary>
        /// The multiple-document-handling attribute.
        /// </summary>
        public const string MultipleDocumentHandling = "multiple-document-handling";

        /// <summary>
        /// The multiple-document-handling-actual attribute.
        /// </summary>
        public const string MultipleDocumentHandlingActual = "multiple-document-handling-actual";

        /// <summary>
        /// The multiple-document-handling-default attribute.
        /// </summary>
        public const string MultipleDocumentHandlingDefault = "multiple-document-handling-default";

        /// <summary>
        /// The multiple-document-handling-supported attribute.
        /// </summary>
        public const string MultipleDocumentHandlingSupported = "multiple-document-handling-supported";

        /// <summary>
        /// The multiple-document-jobs-supported attribute.
        /// </summary>
        public const string MultipleDocumentJobsSupported = "multiple-document-jobs-supported";

        /// <summary>
        /// multiple-document-printers-supported
        /// See: PWG 5100.22-2025 Section 7.2.7
        /// </summary>
        public const string MultipleDocumentPrintersSupported = "multiple-document-printers-supported";

        /// <summary>
        /// The multiple-object-handling attribute.
        /// </summary>
        public const string MultipleObjectHandling = "multiple-object-handling";

        /// <summary>
        /// The multiple-object-handling-actual attribute.
        /// </summary>
        public const string MultipleObjectHandlingActual3d = "multiple-object-handling-actual";

        /// <summary>
        /// The multiple-object-handling-default attribute.
        /// </summary>
        public const string MultipleObjectHandlingDefault = "multiple-object-handling-default";

        /// <summary>
        /// The multiple-object-handling-supported attribute.
        /// </summary>
        public const string MultipleObjectHandlingSupported = "multiple-object-handling-supported";

        /// <summary>
        /// The multiple-operation-time-out attribute.
        /// </summary>
        public const string MultipleOperationTimeOut = "multiple-operation-time-out";

        /// <summary>
        /// The multiple-operation-time-out-action attribute.
        /// </summary>
        public const string MultipleOperationTimeOutAction = "multiple-operation-time-out-action";

        /// <summary>
        /// The my-jobs attribute.
        /// </summary>
        public const string MyJobs = "my-jobs";

        /// <summary>
        /// natural-language-configured
        /// See: PWG 5100.22-2025 Section 7.2.8
        /// </summary>
        public const string NaturalLanguageConfigured = "natural-language-configured";

        /// <summary>
        /// notify-attributes-supported
        /// See: PWG 5100.22-2025 Section 7.2.10
        /// </summary>
        public const string NotifyAttributesSupported = "notify-attributes-supported";

        /// <summary>
        /// notify-events-default
        /// See: PWG 5100.22-2025 Section 7.2.11
        /// </summary>
        public const string NotifyEventsDefault = "notify-events-default";

        /// <summary>
        /// notify-events-supported
        /// See: PWG 5100.22-2025 Section 7.2.12
        /// </summary>
        public const string NotifyEventsSupported = "notify-events-supported";

        /// <summary>
        /// notify-lease-duration-default
        /// See: PWG 5100.22-2025 Section 7.2.13
        /// </summary>
        public const string NotifyLeaseDurationDefault = "notify-lease-duration-default";

        /// <summary>
        /// notify-lease-duration-supported
        /// See: PWG 5100.22-2025 Section 7.2.14
        /// </summary>
        public const string NotifyLeaseDurationSupported = "notify-lease-duration-supported";

        /// <summary>
        /// notify-max-events-supported
        /// See: PWG 5100.22-2025 Section 7.2.15
        /// </summary>
        public const string NotifyMaxEventsSupported = "notify-max-events-supported";

        /// <summary>
        /// notify-printer-ids
        /// See: PWG 5100.22-2025 Section 7.1.1
        /// </summary>
        public const string NotifyPrinterIds = "notify-printer-ids";

        /// <summary>
        /// notify-pull-method — the pull delivery method for Get-Notifications requests; must be "ippget".
        /// See: RFC 3996 Section 5.1
        /// </summary>
        public const string NotifyPullMethod = "notify-pull-method";

        /// <summary>
        /// notify-pull-method-supported
        /// See: PWG 5100.22-2025 Section 7.2.16
        /// </summary>
        public const string NotifyPullMethodSupported = "notify-pull-method-supported";

        /// <summary>
        /// notify-resource-id
        /// See: PWG 5100.22-2025 Section 7.1.2
        /// </summary>
        public const string NotifyResourceId = "notify-resource-id";

        /// <summary>
        /// notify-schemes-supported
        /// See: PWG 5100.22-2025 Section 7.2.17
        /// </summary>
        public const string NotifySchemesSupported = "notify-schemes-supported";

        /// <summary>
        /// notify-subscription-id — identifies the subscription for subscription-targeted operations
        /// such as Cancel-Subscription, Get-Subscription-Attributes, and Renew-Subscription.
        /// See: RFC 3995 Section 5.1
        /// </summary>
        public const string NotifySubscriptionId = "notify-subscription-id";

        /// <summary>
        /// The notified system up time.
        /// See: PWG 5100.22-2025 Section 7.11.2
        /// </summary>
        public const string NotifySystemUpTime = "notify-system-up-time";

        /// <summary>
        /// The notified system URI.
        /// See: PWG 5100.22-2025 Sections 7.10.2 and 7.11.3
        /// </summary>
        public const string NotifySystemUri = "notify-system-uri";

        /// <summary>
        /// The number-of-documents attribute.
        /// </summary>
        public const string NumberOfDocuments = "number-of-documents";

        /// <summary>
        /// The number-of-intervening-jobs attribute.
        /// </summary>
        public const string NumberOfInterveningJobs = "number-of-intervening-jobs";

        /// <summary>
        /// The number-of-retries attribute.
        /// </summary>
        public const string NumberOfRetries = "number-of-retries";

        /// <summary>
        /// The number-of-retries-default attribute.
        /// </summary>
        public const string NumberOfRetriesDefault = "number-of-retries-default";

        /// <summary>
        /// The number-of-retries-supported attribute.
        /// </summary>
        public const string NumberOfRetriesSupported = "number-of-retries-supported";

        /// <summary>
        /// The number-up attribute.
        /// </summary>
        public const string NumberUp = "number-up";

        /// <summary>
        /// The number-up-actual attribute.
        /// </summary>
        public const string NumberUpActual = "number-up-actual";

        /// <summary>
        /// The number-up-default attribute.
        /// </summary>
        public const string NumberUpDefault = "number-up-default";

        /// <summary>
        /// The number-up-supported attribute.
        /// </summary>
        public const string NumberUpSupported = "number-up-supported";

        /// <summary>
        /// operations-supported
        /// See: PWG 5100.22-2025 Section 7.2.18
        /// </summary>
        public const string OperationsSupported = "operations-supported";

        /// <summary>
        /// The organization-name-supported attribute.
        /// </summary>
        public const string OrganizationNameSupported = "organization-name-supported";

        /// <summary>
        /// The orientation-requested attribute.
        /// </summary>
        public const string OrientationRequested = "orientation-requested";

        /// <summary>
        /// The orientation-requested-actual attribute.
        /// </summary>
        public const string OrientationRequestedActual = "orientation-requested-actual";

        /// <summary>
        /// The orientation-requested-default attribute.
        /// </summary>
        public const string OrientationRequestedDefault = "orientation-requested-default";

        /// <summary>
        /// The orientation-requested-supported attribute.
        /// </summary>
        public const string OrientationRequestedSupported = "orientation-requested-supported";

        /// <summary>
        /// The output-attributes attribute.
        /// </summary>
        public const string OutputAttributes = "output-attributes";

        /// <summary>
        /// The output-attributes-default attribute.
        /// </summary>
        public const string OutputAttributesDefault = "output-attributes-default";

        /// <summary>
        /// The output-attributes-supported attribute.
        /// </summary>
        public const string OutputAttributesSupported = "output-attributes-supported";

        /// <summary>
        /// The output-bin attribute.
        /// </summary>
        public const string OutputBin = "output-bin";

        /// <summary>
        /// The output-bin-actual attribute.
        /// </summary>
        public const string OutputBinActual = "output-bin-actual";

        /// <summary>
        /// The output-bin-default attribute.
        /// </summary>
        public const string OutputBinDefault = "output-bin-default";

        /// <summary>
        /// The output-bin-supported attribute.
        /// </summary>
        public const string OutputBinSupported = "output-bin-supported";

        /// <summary>
        /// The output-device attribute.
        /// </summary>
        public const string OutputDevice = "output-device";

        /// <summary>
        /// The output-device-assigned attribute.
        /// </summary>
        public const string OutputDeviceAssigned = "output-device-assigned";

        /// <summary>
        /// The output-device-document-state attribute.
        /// </summary>
        public const string OutputDeviceDocumentState = "output-device-document-state";

        /// <summary>
        /// The output-device-document-state-message attribute.
        /// </summary>
        public const string OutputDeviceDocumentStateMessage = "output-device-document-state-message";

        /// <summary>
        /// The output-device-document-state-reasons attribute.
        /// </summary>
        public const string OutputDeviceDocumentStateReasons = "output-device-document-state-reasons";

        /// <summary>
        /// The output-device-job-state attribute.
        /// </summary>
        public const string OutputDeviceJobState = "output-device-job-state";

        /// <summary>
        /// The output-device-job-state-message attribute.
        /// </summary>
        public const string OutputDeviceJobStateMessage = "output-device-job-state-message";

        /// <summary>
        /// The output-device-job-state-reasons attribute.
        /// </summary>
        public const string OutputDeviceJobStateReasons = "output-device-job-state-reasons";

        /// <summary>
        /// The output-device-job-states attribute.
        /// </summary>
        public const string OutputDeviceJobStates = "output-device-job-states";

        /// <summary>
        /// The output-device-supported attribute.
        /// </summary>
        public const string OutputDeviceSupported = "output-device-supported";

        /// <summary>
        /// The output-device-uuid attribute.
        /// </summary>
        public const string OutputDeviceUuid = "output-device-uuid";

        /// <summary>
        /// The output-device-uuid-assigned attribute.
        /// </summary>
        public const string OutputDeviceUuidAssigned = "output-device-uuid-assigned";

        /// <summary>
        /// The output-device-uuid-supported attribute.
        /// </summary>
        public const string OutputDeviceUuidSupported = "output-device-uuid-supported";

        /// <summary>
        /// The output-device-x509-certificate attribute.
        /// </summary>
        public const string OutputDeviceX509Certificate = "output-device-x509-certificate";

        /// <summary>
        /// The output-device-x509-request attribute.
        /// </summary>
        public const string OutputDeviceX509Request = "output-device-x509-request";

        /// <summary>
        /// output-device-x509-type-supported
        /// See: PWG 5100.22-2025 Section 7.2.19
        /// </summary>
        public const string OutputDeviceX509TypeSupported = "output-device-x509-type-supported";

        /// <summary>
        /// The overrides attribute.
        /// </summary>
        public const string Overrides = "overrides";

        /// <summary>
        /// The overrides-actual attribute.
        /// </summary>
        public const string OverridesActual = "overrides-actual";

        /// <summary>
        /// The overrides-supported attribute.
        /// </summary>
        public const string OverridesSupported = "overrides-supported";

        /// <summary>
        /// The page-delivery attribute.
        /// </summary>
        public const string PageDelivery = "page-delivery";

        /// <summary>
        /// The page-delivery-actual attribute.
        /// </summary>
        public const string PageDeliveryActual = "page-delivery-actual";

        /// <summary>
        /// The page-delivery-default attribute.
        /// </summary>
        public const string PageDeliveryDefault = "page-delivery-default";

        /// <summary>
        /// The page-delivery-supported attribute.
        /// </summary>
        public const string PageDeliverySupported = "page-delivery-supported";

        /// <summary>
        /// The page-order-received attribute.
        /// </summary>
        public const string PageOrderReceived = "page-order-received";

        /// <summary>
        /// The page-order-received-actual attribute.
        /// </summary>
        public const string PageOrderReceivedActual = "page-order-received-actual";

        /// <summary>
        /// The page-overrides attribute.
        /// </summary>
        public const string PageOverrides = "page-overrides";

        /// <summary>
        /// The page-ranges attribute.
        /// </summary>
        public const string PageRanges = "page-ranges";

        /// <summary>
        /// The page-ranges-actual attribute.
        /// </summary>
        public const string PageRangesActual = "page-ranges-actual";

        /// <summary>
        /// The page-ranges-supported attribute.
        /// </summary>
        public const string PageRangesSupported = "page-ranges-supported";

        /// <summary>
        /// The pages attribute.
        /// </summary>
        public const string Pages = "pages";

        /// <summary>
        /// The pages-completed attribute.
        /// </summary>
        public const string PagesCompleted = "pages-completed";

        /// <summary>
        /// The pages-completed-current-copy attribute.
        /// </summary>
        public const string PagesCompletedCurrentCopy = "pages-completed-current-copy";

        /// <summary>
        /// The pages-per-minute attribute.
        /// </summary>
        public const string PagesPerMinute = "pages-per-minute";

        /// <summary>
        /// The pages-per-minute-color attribute.
        /// </summary>
        public const string PagesPerMinuteColor = "pages-per-minute-color";

        /// <summary>
        /// The pages-per-subset attribute.
        /// </summary>
        public const string PagesPerSubset = "pages-per-subset";

        /// <summary>
        /// The pdf-features-supported attribute.
        /// </summary>
        public const string PdfFeaturesSupported = "pdf-features-supported";

        /// <summary>
        /// The pdf-k-octets-supported attribute.
        /// </summary>
        public const string PdfKOctetsSupported = "pdf-k-octets-supported";

        /// <summary>
        /// The pdf-versions-supported attribute.
        /// </summary>
        public const string PdfVersionsSupported = "pdf-versions-supported";

        /// <summary>
        /// The pdl-init-file attribute.
        /// </summary>
        public const string PdlInitFile = "pdl-init-file";

        /// <summary>
        /// The pdl-init-file-default attribute.
        /// </summary>
        public const string PdlInitFileDefault = "pdl-init-file-default";

        /// <summary>
        /// The pdl-init-file-location attribute.
        /// </summary>
        public const string PdlInitFileLocation = "pdl-init-file-location";

        /// <summary>
        /// The pdl-init-file-name attribute.
        /// </summary>
        public const string PdlInitFileName = "pdl-init-file-name";

        /// <summary>
        /// The pdl-init-file-supported attribute.
        /// </summary>
        public const string PdlInitFileSupported = "pdl-init-file-supported";

        /// <summary>
        /// The pdl-override-supported attribute.
        /// </summary>
        public const string PdlOverrideSupported = "pdl-override-supported";

        /// <summary>
        /// The platform-shape attribute.
        /// </summary>
        public const string PlatformShape = "platform-shape";

        /// <summary>
        /// The platform-temperature attribute.
        /// </summary>
        public const string PlatformTemperature = "platform-temperature";

        /// <summary>
        /// The platform-temperature-actual attribute.
        /// </summary>
        public const string PlatformTemperatureActual = "platform-temperature-actual";

        /// <summary>
        /// The platform-temperature-default attribute.
        /// </summary>
        public const string PlatformTemperatureDefault = "platform-temperature-default";

        /// <summary>
        /// The platform-temperature-supported attribute.
        /// </summary>
        public const string PlatformTemperatureSupported = "platform-temperature-supported";

        /// <summary>
        /// power-calendar-policy-col
        /// See: PWG 5100.22-2025 Section 7.2.20
        /// </summary>
        public const string PowerCalendarPolicyCol = "power-calendar-policy-col";

        /// <summary>
        /// power-event-policy-col
        /// See: PWG 5100.22-2025 Section 7.2.21
        /// </summary>
        public const string PowerEventPolicyCol = "power-event-policy-col";

        /// <summary>
        /// Power log collection.
        /// See: PWG 5100.22-2025 Section 7.3.1
        /// </summary>
        public const string PowerLogCol = "power-log-col";

        /// <summary>
        /// Power state capabilities collection.
        /// See: PWG 5100.22-2025 Section 7.3.2
        /// </summary>
        public const string PowerStateCapabilitiesCol = "power-state-capabilities-col";

        /// <summary>
        /// Power state counters collection.
        /// See: PWG 5100.22-2025 Section 7.3.3
        /// </summary>
        public const string PowerStateCountersCol = "power-state-counters-col";

        /// <summary>
        /// Power state monitor collection.
        /// See: PWG 5100.22-2025 Section 7.3.4
        /// </summary>
        public const string PowerStateMonitorCol = "power-state-monitor-col";

        /// <summary>
        /// Power state transitions collection.
        /// See: PWG 5100.22-2025 Section 7.3.5
        /// </summary>
        public const string PowerStateTransitionsCol = "power-state-transitions-col";

        /// <summary>
        /// power-timeout-policy-col
        /// See: PWG 5100.22-2025 Section 7.2.22
        /// </summary>
        public const string PowerTimeoutPolicyCol = "power-timeout-policy-col";

        /// <summary>
        /// The predecessor-job-id attribute.
        /// </summary>
        public const string PredecessorJobId = "predecessor-job-id";

        /// <summary>
        /// The preferred-attributes attribute.
        /// </summary>
        public const string PreferredAttributes = "preferred-attributes";

        /// <summary>
        /// The presentation-direction-number-up attribute.
        /// </summary>
        public const string PresentationDirectionNumberUp = "presentation-direction-number-up";

        /// <summary>
        /// The presentation-direction-number-up-actual attribute.
        /// </summary>
        public const string PresentationDirectionNumberUpActual = "presentation-direction-number-up-actual";

        /// <summary>
        /// The presentation-direction-number-up-default attribute.
        /// </summary>
        public const string PresentationDirectionNumberUpDefault = "presentation-direction-number-up-default";

        /// <summary>
        /// The presentation-direction-number-up-supported attribute.
        /// </summary>
        public const string PresentationDirectionNumberUpSupported = "presentation-direction-number-up-supported";

        /// <summary>
        /// The print-accuracy attribute.
        /// </summary>
        public const string PrintAccuracy = "print-accuracy";

        /// <summary>
        /// The print-accuracy-actual attribute.
        /// </summary>
        public const string PrintAccuracyActual3d = "print-accuracy-actual";

        /// <summary>
        /// The print-accuracy-default attribute.
        /// </summary>
        public const string PrintAccuracyDefault = "print-accuracy-default";

        /// <summary>
        /// The print-accuracy-supported attribute.
        /// </summary>
        public const string PrintAccuracySupported = "print-accuracy-supported";

        /// <summary>
        /// The print-base attribute.
        /// </summary>
        public const string PrintBase = "print-base";

        /// <summary>
        /// The print-base-actual attribute.
        /// </summary>
        public const string PrintBaseActual3d = "print-base-actual";

        /// <summary>
        /// The print-base-default attribute.
        /// </summary>
        public const string PrintBaseDefault = "print-base-default";

        /// <summary>
        /// The print-base-supported attribute.
        /// </summary>
        public const string PrintBaseSupported = "print-base-supported";

        /// <summary>
        /// The print-color-mode attribute.
        /// </summary>
        public const string PrintColorMode = "print-color-mode";

        /// <summary>
        /// The print-color-mode-default attribute.
        /// </summary>
        public const string PrintColorModeDefault = "print-color-mode-default";

        /// <summary>
        /// The print-color-mode-icc-profiles attribute.
        /// </summary>
        public const string PrintColorModeIccProfiles = "print-color-mode-icc-profiles";

        /// <summary>
        /// The print-color-mode-supported attribute.
        /// </summary>
        public const string PrintColorModeSupported = "print-color-mode-supported";

        /// <summary>
        /// The print-content-optimize attribute.
        /// </summary>
        public const string PrintContentOptimize = "print-content-optimize";

        /// <summary>
        /// The print-content-optimize-actual attribute.
        /// </summary>
        public const string PrintContentOptimizeActual = "print-content-optimize-actual";

        /// <summary>
        /// The print-content-optimize-default attribute.
        /// </summary>
        public const string PrintContentOptimizeDefault = "print-content-optimize-default";

        /// <summary>
        /// The print-content-optimize-supported attribute.
        /// </summary>
        public const string PrintContentOptimizeSupported = "print-content-optimize-supported";

        /// <summary>
        /// The print-objects attribute.
        /// </summary>
        public const string PrintObjects = "print-objects";

        /// <summary>
        /// The print-objects-actual attribute.
        /// </summary>
        public const string PrintObjectsActual3d = "print-objects-actual";

        /// <summary>
        /// The print-objects-supported attribute.
        /// </summary>
        public const string PrintObjectsSupported = "print-objects-supported";

        /// <summary>
        /// The print-quality attribute.
        /// </summary>
        public const string PrintQuality = "print-quality";

        /// <summary>
        /// The print-quality-actual attribute.
        /// </summary>
        public const string PrintQualityActual = "print-quality-actual";

        /// <summary>
        /// The print-quality-default attribute.
        /// </summary>
        public const string PrintQualityDefault = "print-quality-default";

        /// <summary>
        /// The print-quality-supported attribute.
        /// </summary>
        public const string PrintQualitySupported = "print-quality-supported";

        /// <summary>
        /// The print-rendering-intent attribute.
        /// </summary>
        public const string PrintRenderingIntent = "print-rendering-intent";

        /// <summary>
        /// The print-scaling attribute.
        /// </summary>
        public const string PrintScaling = "print-scaling";

        /// <summary>
        /// The print-scaling-default attribute.
        /// </summary>
        public const string PrintScalingDefault = "print-scaling-default";

        /// <summary>
        /// The print-scaling-supported attribute.
        /// </summary>
        public const string PrintScalingSupported = "print-scaling-supported";

        /// <summary>
        /// The print-supports attribute.
        /// </summary>
        public const string PrintSupports = "print-supports";

        /// <summary>
        /// The print-supports-actual attribute.
        /// </summary>
        public const string PrintSupportsActual3d = "print-supports-actual";

        /// <summary>
        /// The print-supports-default attribute.
        /// </summary>
        public const string PrintSupportsDefault = "print-supports-default";

        /// <summary>
        /// The print-supports-supported attribute.
        /// </summary>
        public const string PrintSupportsSupported = "print-supports-supported";

        /// <summary>
        /// The printer-alert attribute.
        /// </summary>
        public const string PrinterAlert = "printer-alert";

        /// <summary>
        /// The printer-alert-description attribute.
        /// </summary>
        public const string PrinterAlertDescription = "printer-alert-description";

    /// <summary>
    /// The URI(s) of camera image(s) for the 3D Printer.
    /// See: PWG 5100.21-2019 Section 8.4.3
    /// </summary>
        public const string PrinterCameraImageUri = "printer-camera-image-uri";

        /// <summary>
        /// The printer-charge-info attribute.
        /// </summary>
        public const string PrinterChargeInfo = "printer-charge-info";

        /// <summary>
        /// The printer-charge-info-uri attribute.
        /// </summary>
        public const string PrinterChargeInfoUri = "printer-charge-info-uri";

        /// <summary>
        /// The printer-config-change-date-time attribute.
        /// </summary>
        public const string PrinterConfigChangeDateTime = "printer-config-change-date-time";

        /// <summary>
        /// The printer-config-change-time attribute.
        /// </summary>
        public const string PrinterConfigChangeTime = "printer-config-change-time";

        /// <summary>
        /// The printer-config-changes attribute.
        /// </summary>
        public const string PrinterConfigChanges = "printer-config-changes";

        /// <summary>
        /// The printer-contact-col attribute.
        /// </summary>
        public const string PrinterContactCol = "printer-contact-col";

        /// <summary>
        /// printer-creation-attributes-supported
        /// See: PWG 5100.22-2025 Section 7.2.23
        /// </summary>
        public const string PrinterCreationAttributesSupported = "printer-creation-attributes-supported";

        /// <summary>
        /// The printer-current-time attribute.
        /// </summary>
        public const string PrinterCurrentTime = "printer-current-time";

        /// <summary>
        /// The printer-detailed-status-messages attribute.
        /// </summary>
        public const string PrinterDetailedStatusMessages = "printer-detailed-status-messages";

        /// <summary>
        /// The printer-driver-installer attribute.
        /// </summary>
        public const string PrinterDriverInstaller = "printer-driver-installer";

        /// <summary>
        /// The printer-fax-log-uri attribute.
        /// </summary>
        public const string PrinterFaxLogUri = "printer-fax-log-uri";

        /// <summary>
        /// The printer-fax-modem-info attribute.
        /// </summary>
        public const string PrinterFaxModemInfo = "printer-fax-modem-info";

        /// <summary>
        /// The printer-fax-modem-name attribute.
        /// </summary>
        public const string PrinterFaxModemName = "printer-fax-modem-name";

        /// <summary>
        /// The printer-fax-modem-number attribute.
        /// </summary>
        public const string PrinterFaxModemNumber = "printer-fax-modem-number";

        /// <summary>
        /// The printer-finisher attribute.
        /// </summary>
        public const string PrinterFinisher = "printer-finisher";

        /// <summary>
        /// The printer-finisher-description attribute.
        /// </summary>
        public const string PrinterFinisherDescription = "printer-finisher-description";

        /// <summary>
        /// The printer-finisher-supplies attribute.
        /// </summary>
        public const string PrinterFinisherSupplies = "printer-finisher-supplies";

        /// <summary>
        /// The printer-finisher-supplies-description attribute.
        /// </summary>
        public const string PrinterFinisherSuppliesDescription = "printer-finisher-supplies-description";

        /// <summary>
        /// The printer-geo-location attribute.
        /// </summary>
        public const string PrinterGeoLocation = "printer-geo-location";

        /// <summary>
        /// The printer-icc-profiles attribute.
        /// </summary>
        public const string PrinterIccProfiles = "printer-icc-profiles";

        /// <summary>
        /// The printer-id attribute.
        /// </summary>
        public const string PrinterId = "printer-id";

        /// <summary>
        /// The printer-ids attribute.
        /// </summary>
        public const string PrinterIds = "printer-ids";

        /// <summary>
        /// The printer-impressions-completed attribute.
        /// </summary>
        public const string PrinterImpressionsCompleted = "printer-impressions-completed";

        /// <summary>
        /// The printer-impressions-completed-col attribute.
        /// </summary>
        public const string PrinterImpressionsCompletedCol = "printer-impressions-completed-col";

        /// <summary>
        /// The printer-info attribute.
        /// </summary>
        public const string PrinterInfo = "printer-info";

        /// <summary>
        /// The printer-input-tray attribute.
        /// </summary>
        public const string PrinterInputTray = "printer-input-tray";

        /// <summary>
        /// The printer-is-accepting-jobs attribute.
        /// </summary>
        public const string PrinterIsAcceptingJobs = "printer-is-accepting-jobs";

        /// <summary>
        /// The printer-location attribute.
        /// </summary>
        public const string PrinterLocation = "printer-location";

        /// <summary>
        /// The printer-make-and-model attribute.
        /// </summary>
        public const string PrinterMakeAndModel = "printer-make-and-model";

        /// <summary>
        /// The printer-mandatory-job-attributes attribute.
        /// </summary>
        public const string PrinterMandatoryJobAttributes = "printer-mandatory-job-attributes";

        /// <summary>
        /// The printer-media-sheets-completed attribute.
        /// </summary>
        public const string PrinterMediaSheetsCompleted = "printer-media-sheets-completed";

        /// <summary>
        /// The printer-media-sheets-completed-col attribute.
        /// </summary>
        public const string PrinterMediaSheetsCompletedCol = "printer-media-sheets-completed-col";

        /// <summary>
        /// The printer-message-from-operator attribute.
        /// </summary>
        public const string PrinterMessageFromOperator = "printer-message-from-operator";

        /// <summary>
        /// The printer-mode-configured attribute.
        /// </summary>
        public const string PrinterModeConfigured = "printer-mode-configured";

        /// <summary>
        /// The printer-mode-supported attribute.
        /// </summary>
        public const string PrinterModeSupported = "printer-mode-supported";

        /// <summary>
        /// The printer-more-info attribute.
        /// </summary>
        public const string PrinterMoreInfo = "printer-more-info";

        /// <summary>
        /// The printer-more-info-manufacturer attribute.
        /// </summary>
        public const string PrinterMoreInfoManufacturer = "printer-more-info-manufacturer";

        /// <summary>
        /// The printer-name attribute.
        /// </summary>
        public const string PrinterName = "printer-name";

        /// <summary>
        /// The printer-output-tray attribute.
        /// </summary>
        public const string PrinterOutputTray = "printer-output-tray";

        /// <summary>
        /// The printer-pages-completed attribute.
        /// </summary>
        public const string PrinterPagesCompleted = "printer-pages-completed";

        /// <summary>
        /// The printer-pages-completed-col attribute.
        /// </summary>
        public const string PrinterPagesCompletedCol = "printer-pages-completed-col";

        /// <summary>
        /// The printer-requested-client-type attribute.
        /// </summary>
        public const string PrinterRequestedClientType = "printer-requested-client-type";

        /// <summary>
        /// The printer-requested-job-attributes attribute.
        /// </summary>
        public const string PrinterRequestedJobAttributes = "printer-requested-job-attributes";

        /// <summary>
        /// The printer-resolution attribute.
        /// </summary>
        public const string PrinterResolution = "printer-resolution";

        /// <summary>
        /// The printer-resolution-actual attribute.
        /// </summary>
        public const string PrinterResolutionActual = "printer-resolution-actual";

        /// <summary>
        /// The printer-resolution-default attribute.
        /// </summary>
        public const string PrinterResolutionDefault = "printer-resolution-default";

        /// <summary>
        /// The printer-resolution-supported attribute.
        /// </summary>
        public const string PrinterResolutionSupported = "printer-resolution-supported";

    /// <summary>
    /// The list of resource IDs currently allocated to this Printer (Printer Status attribute).
    /// Defined in PWG 5100.22-2025 Sections 6.1.1 and 6.1.2 (response Group 3: Printer Attributes).
    /// </summary>
        public const string PrinterResourceIds = "printer-resource-ids";

    /// <summary>
    /// The type(s) of print service provided by the Printer.
    /// As operation attribute (1setOf keyword): PWG 5100.22-2025 Section 7.1.9.
    /// As Printer Status attribute (keyword): PWG 5100.22-2025 Section 7.7.9.
    /// </summary>
        public const string PrinterServiceType = "printer-service-type";

        /// <summary>
        /// printer-service-type-supported
        /// See: PWG 5100.22-2025 Section 7.2.24
        /// </summary>
        public const string PrinterServiceTypeSupported = "printer-service-type-supported";

        /// <summary>
        /// The printer-state attribute.
        /// </summary>
        public const string PrinterState = "printer-state";

        /// <summary>
        /// The printer-state-change-date-time attribute.
        /// </summary>
        public const string PrinterStateChangeDateTime = "printer-state-change-date-time";

        /// <summary>
        /// The printer-state-change-time attribute.
        /// </summary>
        public const string PrinterStateChangeTime = "printer-state-change-time";

        /// <summary>
        /// The printer-state-message attribute.
        /// </summary>
        public const string PrinterStateMessage = "printer-state-message";

        /// <summary>
        /// The printer-state-reasons attribute.
        /// </summary>
        public const string PrinterStateReasons = "printer-state-reasons";

        /// <summary>
        /// The printer-static-resource-directory-uri attribute.
        /// </summary>
        public const string PrinterStaticResourceDirectoryUri = "printer-static-resource-directory-uri";

        /// <summary>
        /// The printer-static-resource-k-octets-free attribute.
        /// </summary>
        public const string PrinterStaticResourceKOctetsFree = "printer-static-resource-k-octets-free";

        /// <summary>
        /// The printer-static-resource-k-octets-supported attribute.
        /// </summary>
        public const string PrinterStaticResourceKOctetsSupported = "printer-static-resource-k-octets-supported";

        /// <summary>
        /// The printer-supply attribute.
        /// </summary>
        public const string PrinterSupply = "printer-supply";

        /// <summary>
        /// The printer-supply-description attribute.
        /// </summary>
        public const string PrinterSupplyDescription = "printer-supply-description";

        /// <summary>
        /// The printer-type attribute.
        /// </summary>
        public const string PrinterType = "printer-type";

        /// <summary>
        /// The printer-type-mask attribute.
        /// </summary>
        public const string PrinterTypeMask = "printer-type-mask";

        /// <summary>
        /// The printer-uuid attribute.
        /// </summary>
        public const string PrinterUUID = "printer-uuid";

        /// <summary>
        /// The printer-up-time attribute.
        /// </summary>
        public const string PrinterUpTime = "printer-up-time";

        /// <summary>
        /// The printer-uri attribute.
        /// </summary>
        public const string PrinterUri = "printer-uri";

        /// <summary>
        /// The printer-uri-supported attribute.
        /// </summary>
        public const string PrinterUriSupported = "printer-uri-supported";

        /// <summary>
        /// The printer-volume-supported attribute.
        /// </summary>
        public const string PrinterVolumeSupported = "printer-volume-supported";

    /// <summary>
    /// The URI(s) of the Printer object, as requested by the client (operation attribute).
    /// See: PWG 5100.22-2025 Section 7.1.10
    /// </summary>
        public const string PrinterXriRequested = "printer-xri-requested";

    /// <summary>
    /// The XRI(s) of the Printer object, as requested by the client (operation attribute).
    /// See: PWG 5100.22-2025 Section 7.1.10
    /// </summary>
        public const string PrinterXriSupported = "printer-xri-supported";

        /// <summary>
        /// The proof-copies attribute.
        /// </summary>
        public const string ProofCopies = "proof-copies";

        /// <summary>
        /// The proof-print attribute.
        /// </summary>
        public const string ProofPrint = "proof-print";

        /// <summary>
        /// The punching-hole-diameter-configured attribute.
        /// </summary>
        public const string PunchingHoleDiameterConfigured = "punching-hole-diameter-configured";

        /// <summary>
        /// The punching-locations-supported attribute.
        /// </summary>
        public const string PunchingLocationsSupported = "punching-locations-supported";

        /// <summary>
        /// The punching-offset-supported attribute.
        /// </summary>
        public const string PunchingOffsetSupported = "punching-offset-supported";

        /// <summary>
        /// The punching-reference-edge-supported attribute.
        /// </summary>
        public const string PunchingReferenceEdgeSupported = "punching-reference-edge-supported";

        /// <summary>
        /// The queued-job-count attribute.
        /// </summary>
        public const string QueuedJobCount = "queued-job-count";

        /// <summary>
        /// The reference-uri-schemes-supported attribute.
        /// </summary>
        public const string ReferenceUriSchemesSupported = "reference-uri-schemes-supported";

        /// <summary>
        /// The requested-attributes attribute.
        /// </summary>
        public const string RequestedAttributes = "requested-attributes";

        /// <summary>
        /// The requesting-user-name attribute.
        /// </summary>
        public const string RequestingUserName = "requesting-user-name";

        /// <summary>
        /// The requesting-user-uri attribute.
        /// </summary>
        public const string RequestingUserUri = "requesting-user-uri";

        /// <summary>
        /// The requesting-user-vcard attribute.
        /// </summary>
        public const string RequestingUserVcard = "requesting-user-vcard";

        /// <summary>
        /// Resource data URI
        /// See: PWG 5100.22-2025 Section 7.9.4
        /// </summary>
        public const string ResourceDataUri = "resource-data-uri";

        /// <summary>
        /// Resource date-time at canceled
        /// See: PWG 5100.22-2025 Section 7.9.1
        /// </summary>
        public const string ResourceDateTimeAtCanceled = "date-time-at-canceled";

        /// <summary>
        /// Resource date-time at creation
        /// See: PWG 5100.22-2025 Section 7.9.2
        /// </summary>
        public const string ResourceDateTimeAtCreation = "date-time-at-creation";

        /// <summary>
        /// Resource date-time at installed
        /// See: PWG 5100.22-2025 Section 7.9.3
        /// </summary>
        public const string ResourceDateTimeAtInstalled = "date-time-at-installed";

        /// <summary>
        /// Resource format (MIME media type).
        /// See: PWG 5100.22-2025 Section 7.9.1
        /// </summary>
        public const string ResourceFormat = "resource-format";

        /// <summary>
        /// resource-format-accepted
        /// See: PWG 5100.22-2025 Section 7.1.12
        /// </summary>
        public const string ResourceFormatAccepted = "resource-format-accepted";

        /// <summary>
        /// resource-format-supported
        /// See: PWG 5100.22-2025 Section 7.2.25
        /// </summary>
        public const string ResourceFormatSupported = "resource-format-supported";

        /// <summary>
        /// Resource formats supported
        /// See: PWG 5100.22-2025 Section 7.9.2
        /// </summary>
        public const string ResourceFormats = "resource-formats";

        /// <summary>
        /// The unique identifier for a resource (singular).
        /// See: PWG 5100.22-2025 Section 7.9.6
        /// </summary>
        public const string ResourceId = "resource-id";

        /// <summary>
        /// The list of resource identifiers (operation attribute).
        /// See: PWG 5100.22-2025 Section 7.1.15
        /// </summary>
        public const string ResourceIds = "resource-ids";

        /// <summary>
        /// Resource info
        /// See: PWG 5100.22-2025 Section 7.9.4
        /// </summary>
        public const string ResourceInfo = "resource-info";

        /// <summary>
        /// Resource size in kilobytes
        /// See: PWG 5100.22-2025 Section 7.9.7
        /// </summary>
        public const string ResourceKOctets = "resource-k-octets";

        /// <summary>
        /// Resource name
        /// See: PWG 5100.22-2025 Section 7.9.5
        /// </summary>
        public const string ResourceName = "resource-name";

        /// <summary>
        /// Resource natural language
        /// See: PWG 5100.22-2025 Section 7.9.8
        /// </summary>
        public const string ResourceNaturalLanguage = "resource-natural-language";

        /// <summary>
        /// Resource patches
        /// See: PWG 5100.22-2025 Section 7.9.9
        /// </summary>
        public const string ResourcePatches = "resource-patches";

        /// <summary>
        /// resource-settable-attributes-supported
        /// See: PWG 5100.22-2025 Section 7.2.27
        /// </summary>
        public const string ResourceSettableAttributesSupported = "resource-settable-attributes-supported";

        /// <summary>
        /// Resource signature
        /// See: PWG 5100.22-2025 Section 7.9.10
        /// </summary>
        public const string ResourceSignature = "resource-signature";

        /// <summary>
        /// Resource state
        /// See: PWG 5100.22-2025 Section 7.5.1
        /// </summary>
        public const string ResourceState = "resource-state";

        /// <summary>
        /// Resource state message
        /// See: PWG 5100.22-2025 Section 7.9.12
        /// </summary>
        public const string ResourceStateMessage = "resource-state-message";

        /// <summary>
        /// Resource state reasons
        /// See: PWG 5100.22-2025 Section 7.5.2
        /// </summary>
        public const string ResourceStateReasons = "resource-state-reasons";

        /// <summary>
        /// resource-types (for write operations)
        /// See: PWG 5100.22-2025 Section 7.1.20
        /// </summary>
        public const string ResourceStates = "resource-states";

        /// <summary>
        /// Resource string version
        /// See: PWG 5100.22-2025 Section 7.9.14
        /// </summary>
        public const string ResourceStringVersion = "resource-string-version";

        /// <summary>
        /// Resource time at canceled
        /// See: PWG 5100.22-2025 Section 7.9.1
        /// </summary>
        public const string ResourceTimeAtCanceled = "time-at-canceled";

        /// <summary>
        /// Resource time at creation
        /// See: PWG 5100.22-2025 Section 7.9.2
        /// </summary>
        public const string ResourceTimeAtCreation = "time-at-creation";

        /// <summary>
        /// Resource time at installed
        /// See: PWG 5100.22-2025 Section 7.9.3
        /// </summary>
        public const string ResourceTimeAtInstalled = "time-at-installed";

        /// <summary>
        /// Resource type
        /// See: PWG 5100.22-2025 Section 7.9.11
        /// </summary>
        public const string ResourceType = "resource-type";

        /// <summary>
        /// resource-type-supported
        /// See: PWG 5100.22-2025 Section 7.2.26
        /// </summary>
        public const string ResourceTypeSupported = "resource-type-supported";

        /// <summary>
        /// resource-types
        /// See: PWG 5100.22-2025 Section 7.1.23
        /// </summary>
        public const string ResourceTypes = "resource-types";

        /// <summary>
        /// Resource use count
        /// See: PWG 5100.22-2025 Section 7.9.16
        /// </summary>
        public const string ResourceUseCount = "resource-use-count";

        /// <summary>
        /// The UUID for a resource.
        /// See: PWG 5100.22-2025 Section 7.9.13
        /// </summary>
        public const string ResourceUuid = "resource-uuid";

        /// <summary>
        /// Resource version
        /// See: PWG 5100.22-2025 Section 7.9.10
        /// </summary>
        public const string ResourceVersion = "resource-version";

        /// <summary>
        /// restart-get-interval
        /// See: PWG 5100.22-2025 Section 7.1.25
        /// </summary>
        public const string RestartGetInterval = "restart-get-interval";

        /// <summary>
        /// The retry-interval attribute.
        /// </summary>
        public const string RetryInterval = "retry-interval";

        /// <summary>
        /// The retry-interval-default attribute.
        /// </summary>
        public const string RetryIntervalDefault = "retry-interval-default";

        /// <summary>
        /// The retry-interval-supported attribute.
        /// </summary>
        public const string RetryIntervalSupported = "retry-interval-supported";

        /// <summary>
        /// The retry-time-out attribute.
        /// </summary>
        public const string RetryTimeOut = "retry-time-out";

        /// <summary>
        /// The retry-time-out-default attribute.
        /// </summary>
        public const string RetryTimeOutDefault = "retry-time-out-default";

        /// <summary>
        /// The retry-time-out-supported attribute.
        /// </summary>
        public const string RetryTimeOutSupported = "retry-time-out-supported";

        /// <summary>
        /// The save-disposition attribute.
        /// </summary>
        public const string SaveDisposition = "save-disposition";

        /// <summary>
        /// The save-disposition-supported attribute.
        /// </summary>
        public const string SaveDispositionSupported = "save-disposition-supported";

        /// <summary>
        /// The save-info attribute.
        /// </summary>
        public const string SaveInfo = "save-info";

        /// <summary>
        /// The save-info-supported attribute.
        /// </summary>
        public const string SaveInfoSupported = "save-info-supported";

        /// <summary>
        /// The save-location attribute.
        /// </summary>
        public const string SaveLocation = "save-location";

        /// <summary>
        /// The save-location-supported attribute.
        /// </summary>
        public const string SaveLocationSupported = "save-location-supported";

        /// <summary>
        /// The save-name attribute.
        /// </summary>
        public const string SaveName = "save-name";

        /// <summary>
        /// The save-document-format attribute.
        /// </summary>
        public const string SaveDocumentFormat = "save-document-format";

        /// <summary>
        /// The separator-sheets attribute.
        /// </summary>
        public const string SeparatorSheets = "separator-sheets";

        /// <summary>
        /// The separator-sheets-actual attribute.
        /// </summary>
        public const string SeparatorSheetsActual = "separator-sheets-actual";

        /// <summary>
        /// The separator-sheets-default attribute.
        /// </summary>
        public const string SeparatorSheetsDefault = "separator-sheets-default";

        /// <summary>
        /// The separator-sheets-supported attribute.
        /// </summary>
        public const string SeparatorSheetsSupported = "separator-sheets-supported";

        /// <summary>
        /// The separator-sheets-type-supported attribute.
        /// </summary>
        public const string SeparatorSheetsTypeSupported = "separator-sheets-type-supported";

        /// <summary>
        /// The sheet-collate attribute.
        /// </summary>
        public const string SheetCollate = "sheet-collate";

        /// <summary>
        /// The sides attribute.
        /// </summary>
        public const string Sides = "sides";

        /// <summary>
        /// The sides-actual attribute.
        /// </summary>
        public const string SidesActual = "sides-actual";

        /// <summary>
        /// The sides-default attribute.
        /// </summary>
        public const string SidesDefault = "sides-default";

        /// <summary>
        /// The sides-supported attribute.
        /// </summary>
        public const string SidesSupported = "sides-supported";

        /// <summary>
        /// The status-message attribute.
        /// </summary>
        public const string StatusMessage = "status-message";

        /// <summary>
        /// The stitching-angle-supported attribute.
        /// </summary>
        public const string StitchingAngleSupported = "stitching-angle-supported";

        /// <summary>
        /// The stitching-locations-supported attribute.
        /// </summary>
        public const string StitchingLocationsSupported = "stitching-locations-supported";

        /// <summary>
        /// The stitching-method-supported attribute.
        /// </summary>
        public const string StitchingMethodSupported = "stitching-method-supported";

        /// <summary>
        /// The stitching-offset-supported attribute.
        /// </summary>
        public const string StitchingOffsetSupported = "stitching-offset-supported";

        /// <summary>
        /// The stitching-reference-edge-supported attribute.
        /// </summary>
        public const string StitchingReferenceEdgeSupported = "stitching-reference-edge-supported";

        /// <summary>
        /// The subject-supported attribute.
        /// </summary>
        public const string SubjectSupported = "subject-supported";

        /// <summary>
        /// System asset tag
        /// See: PWG 5100.22-2025 Section 7.2.28
        /// </summary>
        public const string SystemAssetTag = "system-asset-tag";

        /// <summary>
        /// System config change date-time.
        /// See: PWG 5100.22-2025 Section 7.3.8
        /// </summary>
        public const string SystemConfigChangeDateTime = "system-config-change-date-time";

        /// <summary>
        /// System config change time.
        /// See: PWG 5100.22-2025 Section 7.3.9
        /// </summary>
        public const string SystemConfigChangeTime = "system-config-change-time";

        /// <summary>
        /// The number of times the System configuration has changed.
        /// See: PWG 5100.22-2025 Section 7.3.2
        /// </summary>
        public const string SystemConfigChanges = "system-config-changes";

        /// <summary>
        /// The number of configured printers.
        /// See: PWG 5100.22-2025 Section 7.3.3
        /// </summary>
        public const string SystemConfiguredPrinters = "system-configured-printers";

        /// <summary>
        /// The number of configured resources.
        /// See: PWG 5100.22-2025 Section 7.3.4
        /// </summary>
        public const string SystemConfiguredResources = "system-configured-resources";

        /// <summary>
        /// System contact list
        /// See: PWG 5100.22-2025 Section 7.2.29
        /// </summary>
        public const string SystemContactCol = "system-contact-col";

        /// <summary>
        /// System current time
        /// See: PWG 5100.22-2025 Section 7.2.30
        /// </summary>
        public const string SystemCurrentTime = "system-current-time";

        /// <summary>
        /// System default printer id
        /// See: PWG 5100.22-2025 Section 7.2.31
        /// </summary>
        public const string SystemDefaultPrinterId = "system-default-printer-id";

        /// <summary>
        /// System DNS-SD name
        /// See: PWG 5100.22-2025 Section 7.2.32
        /// </summary>
        public const string SystemDnsSdName = "system-dns-sd-name";

        /// <summary>
        /// System firmware names
        /// See: PWG 5100.22-2025 Section 7.3.11
        /// </summary>
        public const string SystemFirmwareName = "system-firmware-name";

        /// <summary>
        /// System firmware patches
        /// See: PWG 5100.22-2025 Section 7.3.12
        /// </summary>
        public const string SystemFirmwarePatches = "system-firmware-patches";

        /// <summary>
        /// System firmware string version
        /// See: PWG 5100.22-2025 Section 7.3.13
        /// </summary>
        public const string SystemFirmwareStringVersion = "system-firmware-string-version";

        /// <summary>
        /// System firmware version
        /// See: PWG 5100.22-2025 Section 7.3.14
        /// </summary>
        public const string SystemFirmwareVersion = "system-firmware-version";

        /// <summary>
        /// System geo location
        /// See: PWG 5100.22-2025 Section 7.2.33
        /// </summary>
        public const string SystemGeoLocation = "system-geo-location";

        /// <summary>
        /// Total system impressions completed.
        /// See: PWG 5100.22-2025 Section 7.3.20
        /// </summary>
        public const string SystemImpressionsCompleted = "system-impressions-completed";

        /// <summary>
        /// Total system impressions completed (colon
        /// and corresponding color detail),
        /// See: PWG 5100.22-2025 Section 7.3.21
        /// </summary>
        public const string SystemImpressionsCompletedCol = "system-impressions-completed-col";

        /// <summary>
        /// System info
        /// See: PWG 5100.22-2025 Section 7.2.34
        /// </summary>
        public const string SystemInfo = "system-info";

        /// <summary>
        /// System location
        /// See: PWG 5100.22-2025 Section 7.2.35
        /// </summary>
        public const string SystemLocation = "system-location";

        /// <summary>
        /// System make and model
        /// See: PWG 5100.22-2025 Section 7.2.38
        /// </summary>
        public const string SystemMakeAndModel = "system-make-and-model";

        /// <summary>
        /// System mandatory printer attributes
        /// See: PWG 5100.22-2025 Section 7.2.36
        /// </summary>
        public const string SystemMandatoryPrinterAttributes = "system-mandatory-printer-attributes";

        /// <summary>
        /// System mandatory registration attributes
        /// See: PWG 5100.22-2025 Section 7.2.37
        /// </summary>
        public const string SystemMandatoryRegistrationAttributes = "system-mandatory-registration-attributes";

        /// <summary>
        /// Total system media sheets completed.
        /// See: PWG 5100.22-2025 Section 7.3.22
        /// </summary>
        public const string SystemMediaSheetsCompleted = "system-media-sheets-completed";

        /// <summary>
        /// Total system media sheets completed color variants.
        /// See: PWG 5100.22-2025 Section 7.3.23
        /// </summary>
        public const string SystemMediaSheetsCompletedCol = "system-media-sheets-completed-col";

        /// <summary>
        /// System message from operator
        /// See: PWG 5100.22-2025 Section 7.2.39
        /// </summary>
        public const string SystemMessageFromOperator = "system-message-from-operator";

        /// <summary>
        /// System name
        /// See: PWG 5100.22-2025 Section 7.2.40
        /// </summary>
        public const string SystemName = "system-name";

        /// <summary>
        /// Total system pages completed.
        /// See: PWG 5100.22-2025 Section 7.3.24
        /// </summary>
        public const string SystemPagesCompleted = "system-pages-completed";

        /// <summary>
        /// Total system pages completed (color variants).
        /// See: PWG 5100.22-2025 Section 7.3.25
        /// </summary>
        public const string SystemPagesCompletedCol = "system-pages-completed-col";

        /// <summary>
        /// The system-resident-application-name attribute.
        /// </summary>
        public const string SystemResidentApplicationName = "system-resident-application-name";

        /// <summary>
        /// The system-resident-application-patches attribute.
        /// </summary>
        public const string SystemResidentApplicationPatches = "system-resident-application-patches";

        /// <summary>
        /// The system-resident-application-string-version attribute.
        /// </summary>
        public const string SystemResidentApplicationStringVersion = "system-resident-application-string-version";

        /// <summary>
        /// The system-resident-application-version attribute.
        /// </summary>
        public const string SystemResidentApplicationVersion = "system-resident-application-version";

        /// <summary>
        /// System serial number.
        /// See: PWG 5100.22-2025 Section 7.3.19
        /// </summary>
        public const string SystemSerialNumber = "system-serial-number";

        /// <summary>
        /// System service contact list
        /// See: PWG 5100.22-2025 Section 7.2.41
        /// </summary>
        public const string SystemServiceContactCol = "system-service-contact-col";

        /// <summary>
        /// System settable attributes supported
        /// See: PWG 5100.22-2025 Section 7.2.42
        /// </summary>
        public const string SystemSettableAttributesSupported = "system-settable-attributes-supported";

        /// <summary>
        /// The current state of the System object.
        /// See: PWG 5100.22-2025 Section 7.3.26
        /// </summary>
        public const string SystemState = "system-state";

        /// <summary>
        /// System state change date-time.
        /// See: PWG 5100.22-2025 Section 7.3.27
        /// </summary>
        public const string SystemStateChangeDateTime = "system-state-change-date-time";

        /// <summary>
        /// System state change time.
        /// See: PWG 5100.22-2025 Section 7.3.28
        /// </summary>
        public const string SystemStateChangeTime = "system-state-change-time";

        /// <summary>
        /// System state message.
        /// See: PWG 5100.22-2025 Section 7.3.29
        /// </summary>
        public const string SystemStateMessage = "system-state-message";

        /// <summary>
        /// System state reasons.
        /// See: PWG 5100.22-2025 Section 7.3.30
        /// </summary>
        public const string SystemStateReasons = "system-state-reasons";

        /// <summary>
        /// The system strings languages supported.
        /// See: PWG 5100.22-2025 Section 7.3.5
        /// </summary>
        public const string SystemStringsLanguagesSupported = "system-strings-languages-supported";

        /// <summary>
        /// URI for system strings.
        /// See: PWG 5100.22-2025 Section 7.3.6
        /// </summary>
        public const string SystemStringsUri = "system-strings-uri";

        /// <summary>
        /// System time source configured
        /// See: PWG 5100.22-2025 Section 7.3.26
        /// </summary>
        public const string SystemTimeSourceConfigured = "system-time-source-configured";

        /// <summary>
        /// System up time.
        /// See: PWG 5100.22-2025 Section 7.3.31
        /// </summary>
        public const string SystemUpTime = "system-up-time";

        /// <summary>
        /// The URI of the target System object.
        /// See: PWG 5100.22-2025 Section 7.1.26
        /// </summary>
        public const string SystemUri = "system-uri";

        /// <summary>
        /// The system-user-application-name attribute.
        /// </summary>
        public const string SystemUserApplicationName = "system-user-application-name";

        /// <summary>
        /// The system-user-application-patches attribute.
        /// </summary>
        public const string SystemUserApplicationPatches = "system-user-application-patches";

        /// <summary>
        /// The system-user-application-string-version attribute.
        /// </summary>
        public const string SystemUserApplicationStringVersion = "system-user-application-string-version";

        /// <summary>
        /// The system-user-application-version attribute.
        /// </summary>
        public const string SystemUserApplicationVersion = "system-user-application-version";

        /// <summary>
        /// System UUID.
        /// See: PWG 5100.22-2025 Section 7.3.32
        /// </summary>
        public const string SystemUuid = "system-uuid";

        /// <summary>
        /// System XRI supported
        /// See: PWG 5100.22-2025 Section 7.2.45
        /// </summary>
        public const string SystemXriSupported = "system-xri-supported";

        /// <summary>
        /// The time-at-completed attribute.
        /// </summary>
        public const string TimeAtCompleted = "time-at-completed";

        /// <summary>
        /// The time-at-completed-estimated attribute.
        /// </summary>
        public const string TimeAtCompletedEstimated = "time-at-completed-estimated";

        /// <summary>
        /// The time-at-creation attribute.
        /// </summary>
        public const string TimeAtCreation = "time-at-creation";

        /// <summary>
        /// The time-at-processing attribute.
        /// </summary>
        public const string TimeAtProcessing = "time-at-processing";

        /// <summary>
        /// The time-at-processing-estimated attribute.
        /// </summary>
        public const string TimeAtProcessingEstimated = "time-at-processing-estimated";

        /// <summary>
        /// The to-name-supported attribute.
        /// </summary>
        public const string ToNameSupported = "to-name-supported";

        /// <summary>
        /// The trimming-offset-supported attribute.
        /// </summary>
        public const string TrimmingOffsetSupported = "trimming-offset-supported";

        /// <summary>
        /// The trimming-reference-edge-supported attribute.
        /// </summary>
        public const string TrimmingReferenceEdgeSupported = "trimming-reference-edge-supported";

        /// <summary>
        /// The trimming-type-supported attribute.
        /// </summary>
        public const string TrimmingTypeSupported = "trimming-type-supported";

        /// <summary>
        /// The trimming-when-supported attribute.
        /// </summary>
        public const string TrimmingWhenSupported = "trimming-when-supported";

        /// <summary>
        /// The uri-authentication-supported attribute.
        /// </summary>
        public const string UriAuthenticationSupported = "uri-authentication-supported";

        /// <summary>
        /// The uri-security-supported attribute.
        /// </summary>
        public const string UriSecuritySupported = "uri-security-supported";

        /// <summary>
        /// The user-defined-values-supported attribute.
        /// </summary>
        public const string UserDefinedValuesSupported = "user-defined-values-supported";

        /// <summary>
        /// The warnings-count attribute.
        /// </summary>
        public const string WarningsCount = "warnings-count";

        /// <summary>
        /// The which-jobs attribute.
        /// </summary>
        public const string WhichJobs = "which-jobs";

        /// <summary>
        /// The which-jobs-supported attribute.
        /// </summary>
        public const string WhichJobsSupported = "which-jobs-supported";

        /// <summary>
        /// which-printers
        /// See: PWG 5100.22-2025 Section 7.1.27
        /// </summary>
        public const string WhichPrinters = "which-printers";

        /// <summary>
        /// The x-image-position attribute.
        /// </summary>
        public const string XImagePosition = "x-image-position";

        /// <summary>
        /// The x-image-position-actual attribute.
        /// </summary>
        public const string XImagePositionActual = "x-image-position-actual";

        /// <summary>
        /// The x-image-position-default attribute.
        /// </summary>
        public const string XImagePositionDefault = "x-image-position-default";

        /// <summary>
        /// The x-image-position-supported attribute.
        /// </summary>
        public const string XImagePositionSupported = "x-image-position-supported";

        /// <summary>
        /// The x-image-shift attribute.
        /// </summary>
        public const string XImageShift = "x-image-shift";

        /// <summary>
        /// The x-image-shift-actual attribute.
        /// </summary>
        public const string XImageShiftActual = "x-image-shift-actual";

        /// <summary>
        /// The x-image-shift-default attribute.
        /// </summary>
        public const string XImageShiftDefault = "x-image-shift-default";

        /// <summary>
        /// The x-image-shift-supported attribute.
        /// </summary>
        public const string XImageShiftSupported = "x-image-shift-supported";

        /// <summary>
        /// The x-side1-image-offset-supported attribute.
        /// </summary>
        public const string XSide1ImageOffsetSupported = "x-side1-image-offset-supported";

        /// <summary>
        /// The x-side1-image-shift attribute.
        /// </summary>
        public const string XSide1ImageShift = "x-side1-image-shift";

        /// <summary>
        /// The x-side1-image-shift-actual attribute.
        /// </summary>
        public const string XSide1ImageShiftActual = "x-side1-image-shift-actual";

        /// <summary>
        /// The x-side1-image-shift-default attribute.
        /// </summary>
        public const string XSide1ImageShiftDefault = "x-side1-image-shift-default";

        /// <summary>
        /// The x-side2-image-offset-supported attribute.
        /// </summary>
        public const string XSide2ImageOffsetSupported = "x-side2-image-offset-supported";

        /// <summary>
        /// The x-side2-image-shift attribute.
        /// </summary>
        public const string XSide2ImageShift = "x-side2-image-shift";

        /// <summary>
        /// The x-side2-image-shift-actual attribute.
        /// </summary>
        public const string XSide2ImageShiftActual = "x-side2-image-shift-actual";

        /// <summary>
        /// The x-side2-image-shift-default attribute.
        /// </summary>
        public const string XSide2ImageShiftDefault = "x-side2-image-shift-default";

        /// <summary>
        /// XRI authentication supported
        /// See: PWG 5100.22-2025 Section 7.3.38
        /// </summary>
        public const string XriAuthenticationSupported = "xri-authentication-supported";

        /// <summary>
        /// XRI security supported
        /// See: PWG 5100.22-2025 Section 7.3.39
        /// </summary>
        public const string XriSecuritySupported = "xri-security-supported";

        /// <summary>
        /// XRI URI scheme supported
        /// See: PWG 5100.22-2025 Section 7.3.40
        /// </summary>
        public const string XriUriSchemeSupported = "xri-uri-scheme-supported";

        /// <summary>
        /// The y-image-position attribute.
        /// </summary>
        public const string YImagePosition = "y-image-position";

        /// <summary>
        /// The y-image-position-actual attribute.
        /// </summary>
        public const string YImagePositionActual = "y-image-position-actual";

        /// <summary>
        /// The y-image-position-default attribute.
        /// </summary>
        public const string YImagePositionDefault = "y-image-position-default";

        /// <summary>
        /// The y-image-position-supported attribute.
        /// </summary>
        public const string YImagePositionSupported = "y-image-position-supported";

        /// <summary>
        /// The y-image-shift attribute.
        /// </summary>
        public const string YImageShift = "y-image-shift";

        /// <summary>
        /// The y-image-shift-actual attribute.
        /// </summary>
        public const string YImageShiftActual = "y-image-shift-actual";

        /// <summary>
        /// The y-image-shift-default attribute.
        /// </summary>
        public const string YImageShiftDefault = "y-image-shift-default";

        /// <summary>
        /// The y-image-shift-supported attribute.
        /// </summary>
        public const string YImageShiftSupported = "y-image-shift-supported";

        /// <summary>
        /// The y-side1-image-offset-supported attribute.
        /// </summary>
        public const string YSide1ImageOffsetSupported = "y-side1-image-offset-supported";

        /// <summary>
        /// The y-side1-image-shift attribute.
        /// </summary>
        public const string YSide1ImageShift = "y-side1-image-shift";

        /// <summary>
        /// The y-side1-image-shift-actual attribute.
        /// </summary>
        public const string YSide1ImageShiftActual = "y-side1-image-shift-actual";

        /// <summary>
        /// The y-side1-image-shift-default attribute.
        /// </summary>
        public const string YSide1ImageShiftDefault = "y-side1-image-shift-default";

        /// <summary>
        /// The y-side2-image-offset-supported attribute.
        /// </summary>
        public const string YSide2ImageOffsetSupported = "y-side2-image-offset-supported";

        /// <summary>
        /// The y-side2-image-shift attribute.
        /// </summary>
        public const string YSide2ImageShift = "y-side2-image-shift";

        /// <summary>
        /// The y-side2-image-shift-actual attribute.
        /// </summary>
        public const string YSide2ImageShiftActual = "y-side2-image-shift-actual";

        /// <summary>
        /// The y-side2-image-shift-default attribute.
        /// </summary>
        public const string YSide2ImageShiftDefault = "y-side2-image-shift-default";
    }
}
