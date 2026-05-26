namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>input-attributes</code>.
/// See: PWG 5100.15-2014 Section 7.1.1
/// </summary>
public readonly record struct InputAttributesMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>The input-color-mode member attribute. See: PWG 5100.15-2013 Section 7.1.1.5</summary>
    public static readonly InputAttributesMember InputColorMode = new(IppAttributeNames.InputColorMode);
    /// <summary>The input-content-type member attribute. See: PWG 5100.15-2013 Section 7.1.1.6</summary>
    public static readonly InputAttributesMember InputContentType = new(IppAttributeNames.InputContentType);
    /// <summary>The input-film-scan-mode member attribute. See: PWG 5100.15-2013 Section 7.1.1.8</summary>
    public static readonly InputAttributesMember InputFilmScanMode = new(IppAttributeNames.InputFilmScanMode);
    /// <summary>The input-media member attribute. See: PWG 5100.15-2013</summary>
    public static readonly InputAttributesMember InputMedia = new(IppAttributeNames.InputMedia);
    /// <summary>The input-orientation-requested member attribute. See: PWG 5100.15-2013</summary>
    public static readonly InputAttributesMember InputOrientationRequested = new(IppAttributeNames.InputOrientationRequested);
    /// <summary>The input-quality member attribute. See: PWG 5100.15-2013</summary>
    public static readonly InputAttributesMember InputQuality = new(IppAttributeNames.InputQuality);
    /// <summary>The input-resolution member attribute. See: PWG 5100.15-2013</summary>
    public static readonly InputAttributesMember InputResolution = new(IppAttributeNames.InputResolution);
    /// <summary>The input-sides member attribute. See: PWG 5100.15-2013</summary>
    public static readonly InputAttributesMember InputSides = new(IppAttributeNames.InputSides);
    /// <summary>The input-source member attribute. See: PWG 5100.15-2013 Section 7.1.1.19</summary>
    public static readonly InputAttributesMember InputSource = new(IppAttributeNames.InputSource);

    public override string ToString() => Value;
    public static implicit operator string(InputAttributesMember value) => value.Value;
    public static explicit operator InputAttributesMember(string value) => new(value);
}
