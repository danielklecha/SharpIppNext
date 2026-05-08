namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies attribute names for <code>job-creation-attributes-supported</code>.
/// See: PWG 5100.7-2023 and PWG 5100.11-2024.
/// </summary>
public readonly record struct JobCreationAttribute(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    // Operation Attributes (RFC 8011)
    public static readonly JobCreationAttribute IppAttributeFidelity = new("ipp-attribute-fidelity");
    public static readonly JobCreationAttribute JobName = new("job-name");
    public static readonly JobCreationAttribute JobKOctets = new("job-k-octets");
    public static readonly JobCreationAttribute JobImpressions = new("job-impressions");
    public static readonly JobCreationAttribute JobMediaSheets = new("job-media-sheets");

    // Operation Attributes (PWG 5100.11)
    public static readonly JobCreationAttribute JobPassword = new("job-password");
    public static readonly JobCreationAttribute JobPasswordEncryption = new("job-password-encryption");
    public static readonly JobCreationAttribute JobReleaseAction = new("job-release-action");
    public static readonly JobCreationAttribute JobStorage = new("job-storage");

    // Job Template Attributes (RFC 8011)
    public static readonly JobCreationAttribute JobPriority = new("job-priority");
    public static readonly JobCreationAttribute JobHoldUntil = new("job-hold-until");
    public static readonly JobCreationAttribute JobSheets = new("job-sheets");
    public static readonly JobCreationAttribute MultipleDocumentHandling = new("multiple-document-handling");
    public static readonly JobCreationAttribute Copies = new("copies");
    public static readonly JobCreationAttribute Finishings = new("finishings");
    public static readonly JobCreationAttribute PageRanges = new("page-ranges");
    public static readonly JobCreationAttribute Sides = new("sides");
    public static readonly JobCreationAttribute NumberUp = new("number-up");
    public static readonly JobCreationAttribute OrientationRequested = new("orientation-requested");
    public static readonly JobCreationAttribute Media = new("media");
    public static readonly JobCreationAttribute PrinterResolution = new("printer-resolution");
    public static readonly JobCreationAttribute PrintQuality = new("print-quality");

    // Job Template Attributes (PWG 5100.7)
    public static readonly JobCreationAttribute JobAccountId = new("job-account-id");
    public static readonly JobCreationAttribute JobAccountingUserId = new("job-accounting-user-id");
    public static readonly JobCreationAttribute JobCancelAfter = new("job-cancel-after");
    public static readonly JobCreationAttribute JobDelayOutputUntil = new("job-delay-output-until");
    public static readonly JobCreationAttribute JobDelayOutputUntilTime = new("job-delay-output-until-time");
    public static readonly JobCreationAttribute JobHoldUntilTime = new("job-hold-until-time");
    public static readonly JobCreationAttribute JobRetainUntil = new("job-retain-until");
    public static readonly JobCreationAttribute JobRetainUntilInterval = new("job-retain-until-interval");
    public static readonly JobCreationAttribute JobRetainUntilTime = new("job-retain-until-time");
    public static readonly JobCreationAttribute JobSheetMessage = new("job-sheet-message");
    public static readonly JobCreationAttribute OutputDevice = new("output-device");
    public static readonly JobCreationAttribute PrintContentOptimize = new("print-content-optimize");
    public static readonly JobCreationAttribute JobSheetsCol = new("job-sheets-col");
    public static readonly JobCreationAttribute MediaCol = new("media-col");

    // Job Template Attributes (PWG 5100.11)
    public static readonly JobCreationAttribute JobAccountType = new("job-account-type");
    public static readonly JobCreationAttribute ProofCopies = new("proof-copies");
    public static readonly JobCreationAttribute ProofPrint = new("proof-print");

    // Job Template Attributes (PWG 5100.3)
    public static readonly JobCreationAttribute CoverBack = new("cover-back");
    public static readonly JobCreationAttribute CoverFront = new("cover-front");
    public static readonly JobCreationAttribute ForceFrontSide = new("force-front-side");
    public static readonly JobCreationAttribute ImageOrientation = new("image-orientation");
    public static readonly JobCreationAttribute ImpositionTemplate = new("imposition-template");
    public static readonly JobCreationAttribute InsertSheet = new("insert-sheet");
    public static readonly JobCreationAttribute JobAccountingSheets = new("job-accounting-sheets");
    public static readonly JobCreationAttribute JobCompleteBefore = new("job-complete-before");
    public static readonly JobCreationAttribute JobCompleteBeforeTime = new("job-complete-before-time");
    public static readonly JobCreationAttribute JobErrorSheet = new("job-error-sheet");
    public static readonly JobCreationAttribute JobMessageToOperator = new("job-message-to-operator");
    public static readonly JobCreationAttribute JobPhoneNumber = new("job-phone-number");
    public static readonly JobCreationAttribute JobRecipientName = new("job-recipient-name");
    public static readonly JobCreationAttribute MediaInputTrayCheck = new("media-input-tray-check");
    public static readonly JobCreationAttribute PageDelivery = new("page-delivery");
    public static readonly JobCreationAttribute PresentationDirectionNumberUp = new("presentation-direction-number-up");
    public static readonly JobCreationAttribute SeparatorSheets = new("separator-sheets");
    public static readonly JobCreationAttribute XImagePosition = new("x-image-position");
    public static readonly JobCreationAttribute XImageShift = new("x-image-shift");
    public static readonly JobCreationAttribute XSide1ImageShift = new("x-side1-image-shift");
    public static readonly JobCreationAttribute XSide2ImageShift = new("x-side2-image-shift");
    public static readonly JobCreationAttribute YImagePosition = new("y-image-position");
    public static readonly JobCreationAttribute YImageShift = new("y-image-shift");
    public static readonly JobCreationAttribute YSide1ImageShift = new("y-side1-image-shift");
    public static readonly JobCreationAttribute YSide2ImageShift = new("y-side2-image-shift");

    // Job Template Attributes (PWG 5100.13)
    public static readonly JobCreationAttribute PrintScaling = new("print-scaling");
    public static readonly JobCreationAttribute PrintColorMode = new("print-color-mode");
    public static readonly JobCreationAttribute PrintRenderingIntent = new("print-rendering-intent");
    public static readonly JobCreationAttribute JobErrorAction = new("job-error-action");

    // Job Template Attributes (PWG 5100.21)
    public static readonly JobCreationAttribute MaterialsCol = new("materials-col");
    public static readonly JobCreationAttribute MultipleObjectHandling = new("multiple-object-handling");
    public static readonly JobCreationAttribute PlatformTemperature = new("platform-temperature");
    public static readonly JobCreationAttribute PrintAccuracy = new("print-accuracy");
    public static readonly JobCreationAttribute PrintBase = new("print-base");
    public static readonly JobCreationAttribute PrintObjects = new("print-objects");
    public static readonly JobCreationAttribute PrintSupports = new("print-supports");

    // Job Template Attributes (PWG 5100.6)
    public static readonly JobCreationAttribute Overrides = new("overrides");

    // Job Template Attributes (PWG 5100.1)
    public static readonly JobCreationAttribute FinishingsCol = new("finishings-col");
    public static readonly JobCreationAttribute JobPagesPerSet = new("job-pages-per-set");

    public override string ToString() => Value;
    public static implicit operator string(JobCreationAttribute value) => value.Value;
    public static explicit operator JobCreationAttribute(string value) => new(value);
}
