namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies known attribute names for <code>job-creation-attributes-supported</code>.
/// See: PWG 5100.7-2023 Section 6.9.13
/// </summary>
public readonly record struct JobCreationAttribute(string Value, bool IsValue = true) : ISmartEnum 
{
    // Operation attributes used with Job Creation and Validate-Job requests.
    public static readonly JobCreationAttribute JobName = new("job-name");
    public static readonly JobCreationAttribute IppAttributeFidelity = new("ipp-attribute-fidelity");
    public static readonly JobCreationAttribute DocumentName = new("document-name");
    public static readonly JobCreationAttribute Compression = new("compression");
    public static readonly JobCreationAttribute DocumentFormat = new("document-format");
    public static readonly JobCreationAttribute DocumentNaturalLanguage = new("document-natural-language");
    public static readonly JobCreationAttribute JobKOctets = new("job-k-octets");
    public static readonly JobCreationAttribute JobImpressions = new("job-impressions");
    public static readonly JobCreationAttribute JobMediaSheets = new("job-media-sheets");
    public static readonly JobCreationAttribute RequestingUserName = new("requesting-user-name");

    // Core Job Template attributes (RFC 8011).
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
    public static readonly JobCreationAttribute Overrides = new("overrides");

    // Common PWG extension attributes accepted during Job Creation.
    public static readonly JobCreationAttribute FinishingsCol = new("finishings-col");
    public static readonly JobCreationAttribute MediaCol = new("media-col");
    public static readonly JobCreationAttribute OutputBin = new("output-bin");
    public static readonly JobCreationAttribute PrintScaling = new("print-scaling");
    public static readonly JobCreationAttribute PrintColorMode = new("print-color-mode");
    public static readonly JobCreationAttribute PrintContentOptimize = new("print-content-optimize");
    public static readonly JobCreationAttribute JobAccountId = new("job-account-id");
    public static readonly JobCreationAttribute JobAccountType = new("job-account-type");
    public static readonly JobCreationAttribute JobAccountingUserId = new("job-accounting-user-id");
    public static readonly JobCreationAttribute JobCancelAfter = new("job-cancel-after");
    public static readonly JobCreationAttribute JobDelayOutputUntil = new("job-delay-output-until");
    public static readonly JobCreationAttribute JobDelayOutputUntilTime = new("job-delay-output-until-time");
    public static readonly JobCreationAttribute JobErrorAction = new("job-error-action");
    public static readonly JobCreationAttribute JobErrorSheet = new("job-error-sheet");
    public static readonly JobCreationAttribute JobPhoneNumber = new("job-phone-number");
    public static readonly JobCreationAttribute JobPhoneNumberScheme = new("job-phone-number-scheme");
    public static readonly JobCreationAttribute JobRecipientName = new("job-recipient-name");
    public static readonly JobCreationAttribute JobSheetMessage = new("job-sheet-message");
    public static readonly JobCreationAttribute JobRetainUntil = new("job-retain-until");
    public static readonly JobCreationAttribute JobRetainUntilInterval = new("job-retain-until-interval");
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

    public override string ToString() => Value;
    public static implicit operator string(JobCreationAttribute value) => value.Value;
    public static explicit operator JobCreationAttribute(string value) => new(value);
}