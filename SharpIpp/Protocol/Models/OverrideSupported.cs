namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies known keyword values for <c>overrides-supported</c>.
/// See: PWG 5100.6-2003 Section 4.1.7
/// </summary>
public readonly record struct OverrideSupported(string Value, bool IsValue = true) : ISmartEnum
{
    // Required member attributes.
    public static readonly OverrideSupported Pages = new("pages");
    public static readonly OverrideSupported DocumentNumbers = new("document-numbers");
    public static readonly OverrideSupported DocumentCopies = new("document-copies");

    // Common Job Template override members.
    public static readonly OverrideSupported Finishings = new("finishings");
    public static readonly OverrideSupported FinishingsCol = new("finishings-col");
    public static readonly OverrideSupported ForceFrontSide = new("force-front-side");
    public static readonly OverrideSupported ImpositionTemplate = new("imposition-template");
    public static readonly OverrideSupported Media = new("media");
    public static readonly OverrideSupported MediaCol = new("media-col");
    public static readonly OverrideSupported MediaType = new("media-type");
    public static readonly OverrideSupported NumberUp = new("number-up");
    public static readonly OverrideSupported OrientationRequested = new("orientation-requested");
    public static readonly OverrideSupported OutputDevice = new("output-device");
    public static readonly OverrideSupported PresentationDirectionNumberUp = new("presentation-direction-number-up");
    public static readonly OverrideSupported PrintQuality = new("print-quality");
    public static readonly OverrideSupported PrinterResolution = new("printer-resolution");
    public static readonly OverrideSupported Sides = new("sides");
    public static readonly OverrideSupported XImagePosition = new("x-image-position");
    public static readonly OverrideSupported XImageShift = new("x-image-shift");
    public static readonly OverrideSupported XSide1ImageShift = new("x-side1-image-shift");
    public static readonly OverrideSupported XSide2ImageShift = new("x-side2-image-shift");
    public static readonly OverrideSupported YImagePosition = new("y-image-position");
    public static readonly OverrideSupported YImageShift = new("y-image-shift");
    public static readonly OverrideSupported YSide1ImageShift = new("y-side1-image-shift");
    public static readonly OverrideSupported YSide2ImageShift = new("y-side2-image-shift");

    public override string ToString() => Value;
    public static implicit operator string(OverrideSupported value) => value.Value;
    public static explicit operator OverrideSupported(string value) => new(value);
}