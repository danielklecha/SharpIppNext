namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies member attribute names supported by <code>media-col-supported</code>.
/// See: PWG 5100.7-2023 Section 6.9.39
/// </summary>
public readonly record struct MediaColMember(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly MediaColMember MediaBackCoating = new("media-back-coating");
    public static readonly MediaColMember MediaBottomMargin = new("media-bottom-margin");
    public static readonly MediaColMember MediaColor = new("media-color");
    public static readonly MediaColMember MediaFrontCoating = new("media-front-coating");
    public static readonly MediaColMember MediaGrain = new("media-grain");
    public static readonly MediaColMember MediaHoleCount = new("media-hole-count");
    public static readonly MediaColMember MediaInfo = new("media-info");
    public static readonly MediaColMember MediaKey = new("media-key");
    public static readonly MediaColMember MediaLeftMargin = new("media-left-margin");
    public static readonly MediaColMember MediaOrderCount = new("media-order-count");
    public static readonly MediaColMember MediaPrePrinted = new("media-pre-printed");
    public static readonly MediaColMember MediaRecycled = new("media-recycled");
    public static readonly MediaColMember MediaRightMargin = new("media-right-margin");
    public static readonly MediaColMember MediaSize = new("media-size");
    public static readonly MediaColMember MediaSizeName = new("media-size-name");
    public static readonly MediaColMember MediaSource = new("media-source");
    public static readonly MediaColMember MediaSourceProperties = new("media-source-properties");
    public static readonly MediaColMember MediaThickness = new("media-thickness");
    public static readonly MediaColMember MediaTooth = new("media-tooth");
    public static readonly MediaColMember MediaTopMargin = new("media-top-margin");
    public static readonly MediaColMember MediaType = new("media-type");
    public static readonly MediaColMember MediaWeightMetric = new("media-weight-metric");

    public override string ToString() => Value;
    public static implicit operator string(MediaColMember value) => value.Value;
    public static explicit operator MediaColMember(string value) => new(value);
}
