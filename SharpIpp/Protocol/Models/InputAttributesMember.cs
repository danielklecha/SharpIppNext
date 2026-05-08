namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the member names for <code>input-attributes</code>.
/// See: PWG 5100.18-2015 Section 6.2.7
/// </summary>
public readonly record struct InputAttributesMember(string Value, bool IsMarked = true, bool IsValue = true) : IMarkedSmartEnum
{
    public static readonly InputAttributesMember InputColorMode = new("input-color-mode");
    public static readonly InputAttributesMember InputContentType = new("input-content-type");
    public static readonly InputAttributesMember InputFilmScanMode = new("input-film-scan-mode");
    public static readonly InputAttributesMember InputMedia = new("input-media");
    public static readonly InputAttributesMember InputOrientationRequested = new("input-orientation-requested");
    public static readonly InputAttributesMember InputQuality = new("input-quality");
    public static readonly InputAttributesMember InputResolution = new("input-resolution");
    public static readonly InputAttributesMember InputSides = new("input-sides");
    public static readonly InputAttributesMember InputSource = new("input-source");

    public override string ToString() => Value;
    public static implicit operator string(InputAttributesMember value) => value.Value;
    public static explicit operator InputAttributesMember(string value) => new(value);
}
