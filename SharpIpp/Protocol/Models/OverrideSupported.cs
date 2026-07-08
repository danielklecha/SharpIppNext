namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies known keyword values for <c>overrides-supported</c>.
/// See: PWG 5100.6-2003 Section 4.1.7
/// </summary>
public readonly record struct OverrideSupported(string Value, bool IsValue = true) : ISmartEnum
{
    // Required member attributes.

    /// <summary>The pages member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported Pages = new("pages");
    /// <summary>The document-numbers member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported DocumentNumbers = new("document-numbers");
    /// <summary>The document-copies member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported DocumentCopies = new("document-copies");

    // Common Job Template override members.

    /// <summary>The finishings member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported Finishings = new("finishings");
    /// <summary>The finishings-col member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported FinishingsCol = new("finishings-col");
    /// <summary>The force-front-side member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported ForceFrontSide = new("force-front-side");
    /// <summary>The imposition-template member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported ImpositionTemplate = new("imposition-template");
    /// <summary>The media member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported Media = new("media");
    /// <summary>The media-col member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported MediaCol = new("media-col");
    /// <summary>The media-type member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported MediaType = new("media-type");
    /// <summary>The number-up member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported NumberUp = new("number-up");
    /// <summary>The orientation-requested member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported OrientationRequested = new("orientation-requested");
    /// <summary>The output-device member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported OutputDevice = new("output-device");
    /// <summary>The presentation-direction-number-up member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported PresentationDirectionNumberUp = new("presentation-direction-number-up");
    /// <summary>The print-quality member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported PrintQuality = new("print-quality");
    /// <summary>The printer-resolution member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported PrinterResolution = new("printer-resolution");
    /// <summary>The sides member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported Sides = new("sides");
    /// <summary>The x-image-position member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported XImagePosition = new("x-image-position");
    /// <summary>The x-image-shift member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported XImageShift = new("x-image-shift");
    /// <summary>The x-side1-image-shift member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported XSide1ImageShift = new("x-side1-image-shift");
    /// <summary>The x-side2-image-shift member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported XSide2ImageShift = new("x-side2-image-shift");
    /// <summary>The y-image-position member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported YImagePosition = new("y-image-position");
    /// <summary>The y-image-shift member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported YImageShift = new("y-image-shift");
    /// <summary>The y-side1-image-shift member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported YSide1ImageShift = new("y-side1-image-shift");
    /// <summary>The y-side2-image-shift member attribute is supported in overrides. See: PWG 5100.6-2003 Section 4.1.7</summary>
    public static readonly OverrideSupported YSide2ImageShift = new("y-side2-image-shift");

    public override string ToString() => Value;
    public static implicit operator string(OverrideSupported value) => value.Value;
    public static implicit operator OverrideSupported(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
