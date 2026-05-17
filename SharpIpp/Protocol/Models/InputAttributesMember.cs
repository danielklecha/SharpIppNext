namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>input-attributes</code>.
/// See: PWG 5100.15-2014 Section 7.1.1
/// </summary>
public readonly record struct InputAttributesMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    /// <summary>The input-color-mode member attribute. See: PWG 5100.15-2013 Section 7.1.1.5</summary>
    public static readonly InputAttributesMember InputColorMode = new("input-color-mode");
    /// <summary>The input-content-type member attribute. See: PWG 5100.15-2013 Section 7.1.1.6</summary>
    public static readonly InputAttributesMember InputContentType = new("input-content-type");
    /// <summary>The input-film-scan-mode member attribute. See: PWG 5100.15-2013 Section 7.1.1.8</summary>
    public static readonly InputAttributesMember InputFilmScanMode = new("input-film-scan-mode");
    /// <summary>The input-media member attribute. See: PWG 5100.15-2013</summary>
    public static readonly InputAttributesMember InputMedia = new("input-media");
    /// <summary>The input-orientation-requested member attribute. See: PWG 5100.15-2013</summary>
    public static readonly InputAttributesMember InputOrientationRequested = new("input-orientation-requested");
    /// <summary>The input-quality member attribute. See: PWG 5100.15-2013</summary>
    public static readonly InputAttributesMember InputQuality = new("input-quality");
    /// <summary>The input-resolution member attribute. See: PWG 5100.15-2013</summary>
    public static readonly InputAttributesMember InputResolution = new("input-resolution");
    /// <summary>The input-sides member attribute. See: PWG 5100.15-2013</summary>
    public static readonly InputAttributesMember InputSides = new("input-sides");
    /// <summary>The input-source member attribute. See: PWG 5100.15-2013 Section 7.1.1.19</summary>
    public static readonly InputAttributesMember InputSource = new("input-source");

    public override string ToString() => Value;
    public static implicit operator string(InputAttributesMember value) => value.Value;
    public static explicit operator InputAttributesMember(string value) => new(value);
}
