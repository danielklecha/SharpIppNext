namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>job-creation-attributes-supported</code>.
/// See: PWG 5100.7-2023 and PWG 5100.11-2024.
/// </summary>
public readonly record struct JobCreationAttribute(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    // Operation Attributes (RFC 8011)
    /// <summary>The ipp-attribute-fidelity attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute IppAttributeFidelity = new("ipp-attribute-fidelity");
    /// <summary>The job-name attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobName = new("job-name");
    /// <summary>The job-k-octets attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobKOctets = new("job-k-octets");
    /// <summary>The job-impressions attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobImpressions = new("job-impressions");
    /// <summary>The job-media-sheets attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobMediaSheets = new("job-media-sheets");

    // Operation Attributes (PWG 5100.11)
    /// <summary>The job-password attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobPassword = new("job-password");
    /// <summary>The job-password-encryption attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobPasswordEncryption = new("job-password-encryption");
    /// <summary>The job-release-action attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobReleaseAction = new("job-release-action");
    /// <summary>The job-storage attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobStorage = new("job-storage");

    // Job Template Attributes (RFC 8011)
    /// <summary>The job-priority attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobPriority = new("job-priority");
    /// <summary>The job-hold-until attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobHoldUntil = new("job-hold-until");
    /// <summary>The job-sheets attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobSheets = new("job-sheets");
    /// <summary>The multiple-document-handling attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute MultipleDocumentHandling = new("multiple-document-handling");
    /// <summary>The copies attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute Copies = new("copies");
    /// <summary>The finishings attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute Finishings = new("finishings");
    /// <summary>The page-ranges attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PageRanges = new("page-ranges");
    /// <summary>The sides attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute Sides = new("sides");
    /// <summary>The number-up attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute NumberUp = new("number-up");
    /// <summary>The orientation-requested attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute OrientationRequested = new("orientation-requested");
    /// <summary>The media attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute Media = new("media");
    /// <summary>The printer-resolution attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PrinterResolution = new("printer-resolution");
    /// <summary>The print-quality attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PrintQuality = new("print-quality");

    // Job Template Attributes (PWG 5100.7)
    /// <summary>The job-account-id attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobAccountId = new("job-account-id");
    /// <summary>The job-accounting-user-id attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobAccountingUserId = new("job-accounting-user-id");
    /// <summary>The job-cancel-after attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobCancelAfter = new("job-cancel-after");
    /// <summary>The job-delay-output-until attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobDelayOutputUntil = new("job-delay-output-until");
    /// <summary>The job-delay-output-until-time attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobDelayOutputUntilTime = new("job-delay-output-until-time");
    /// <summary>The job-hold-until-time attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobHoldUntilTime = new("job-hold-until-time");
    /// <summary>The job-retain-until attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobRetainUntil = new("job-retain-until");
    /// <summary>The job-retain-until-interval attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobRetainUntilInterval = new("job-retain-until-interval");
    /// <summary>The job-retain-until-time attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobRetainUntilTime = new("job-retain-until-time");
    /// <summary>The job-sheet-message attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobSheetMessage = new("job-sheet-message");
    /// <summary>The output-device attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute OutputDevice = new("output-device");
    /// <summary>The print-content-optimize attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PrintContentOptimize = new("print-content-optimize");
    /// <summary>The job-sheets-col attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobSheetsCol = new("job-sheets-col");
    /// <summary>The media-col attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute MediaCol = new("media-col");

    // Job Template Attributes (PWG 5100.11)
    /// <summary>The job-account-type attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobAccountType = new("job-account-type");
    /// <summary>The proof-copies attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute ProofCopies = new("proof-copies");
    /// <summary>The proof-print attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute ProofPrint = new("proof-print");

    // Job Template Attributes (PWG 5100.3)
    /// <summary>The cover-back attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute CoverBack = new("cover-back");
    /// <summary>The cover-front attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute CoverFront = new("cover-front");
    /// <summary>The force-front-side attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute ForceFrontSide = new("force-front-side");
    /// <summary>The image-orientation attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute ImageOrientation = new("image-orientation");
    /// <summary>The imposition-template attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute ImpositionTemplate = new("imposition-template");
    /// <summary>The insert-sheet attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute InsertSheet = new("insert-sheet");
    /// <summary>The job-accounting-sheets attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobAccountingSheets = new("job-accounting-sheets");
    /// <summary>The job-complete-before attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobCompleteBefore = new("job-complete-before");
    /// <summary>The job-complete-before-time attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobCompleteBeforeTime = new("job-complete-before-time");
    /// <summary>The job-error-sheet attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobErrorSheet = new("job-error-sheet");
    /// <summary>The job-message-to-operator attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobMessageToOperator = new("job-message-to-operator");
    /// <summary>The job-phone-number attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobPhoneNumber = new("job-phone-number");
    /// <summary>The job-recipient-name attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobRecipientName = new("job-recipient-name");
    /// <summary>The media-input-tray-check attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute MediaInputTrayCheck = new("media-input-tray-check");
    /// <summary>The page-delivery attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PageDelivery = new("page-delivery");
    /// <summary>The presentation-direction-number-up attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PresentationDirectionNumberUp = new("presentation-direction-number-up");
    /// <summary>The separator-sheets attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute SeparatorSheets = new("separator-sheets");
    /// <summary>The x-image-position attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute XImagePosition = new("x-image-position");
    /// <summary>The x-image-shift attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute XImageShift = new("x-image-shift");
    /// <summary>The x-side1-image-shift attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute XSide1ImageShift = new("x-side1-image-shift");
    /// <summary>The x-side2-image-shift attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute XSide2ImageShift = new("x-side2-image-shift");
    /// <summary>The y-image-position attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute YImagePosition = new("y-image-position");
    /// <summary>The y-image-shift attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute YImageShift = new("y-image-shift");
    /// <summary>The y-side1-image-shift attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute YSide1ImageShift = new("y-side1-image-shift");
    /// <summary>The y-side2-image-shift attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute YSide2ImageShift = new("y-side2-image-shift");

    // Job Template Attributes (PWG 5100.13)
    /// <summary>The print-scaling attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PrintScaling = new("print-scaling");
    /// <summary>The print-color-mode attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PrintColorMode = new("print-color-mode");
    /// <summary>The print-rendering-intent attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PrintRenderingIntent = new("print-rendering-intent");
    /// <summary>The job-error-action attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobErrorAction = new("job-error-action");

    // Job Template Attributes (PWG 5100.21)
    /// <summary>The materials-col attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute MaterialsCol = new("materials-col");
    /// <summary>The multiple-object-handling attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute MultipleObjectHandling = new("multiple-object-handling");
    /// <summary>The platform-temperature attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PlatformTemperature = new("platform-temperature");
    /// <summary>The print-accuracy attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PrintAccuracy = new("print-accuracy");
    /// <summary>The print-base attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PrintBase = new("print-base");
    /// <summary>The print-objects attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PrintObjects = new("print-objects");
    /// <summary>The print-supports attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute PrintSupports = new("print-supports");

    // Job Template Attributes (PWG 5100.6)
    /// <summary>The overrides attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute Overrides = new("overrides");

    // Job Template Attributes (PWG 5100.1)
    /// <summary>The finishings-col attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute FinishingsCol = new("finishings-col");
    /// <summary>The job-pages-per-set attribute. See: PWG 5100.7-2023</summary>
    public static readonly JobCreationAttribute JobPagesPerSet = new("job-pages-per-set");

    public override string ToString() => Value;
    public static implicit operator string(JobCreationAttribute value) => value.Value;
    public static explicit operator JobCreationAttribute(string value) => new(value);
}
