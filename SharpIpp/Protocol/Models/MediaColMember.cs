namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies member attribute names supported by <code>media-col-supported</code>.
/// See: PWG 5100.7-2023 Section 6.9.39
/// </summary>
public readonly record struct MediaColMember(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>The media-back-coating member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaBackCoating = new("media-back-coating");
    /// <summary>The media-bottom-margin member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaBottomMargin = new("media-bottom-margin");
    /// <summary>The media-color member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaColor = new("media-color");
    /// <summary>The media-front-coating member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaFrontCoating = new("media-front-coating");
    /// <summary>The media-grain member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaGrain = new("media-grain");
    /// <summary>The media-hole-count member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaHoleCount = new("media-hole-count");
    /// <summary>The media-info member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaInfo = new("media-info");
    /// <summary>The media-key member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaKey = new("media-key");
    /// <summary>The media-left-margin member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaLeftMargin = new("media-left-margin");
    /// <summary>The media-order-count member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaOrderCount = new("media-order-count");
    /// <summary>The media-pre-printed member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaPrePrinted = new("media-pre-printed");
    /// <summary>The media-recycled member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaRecycled = new("media-recycled");
    /// <summary>The media-right-margin member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaRightMargin = new("media-right-margin");
    /// <summary>The media-size member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaSize = new("media-size");
    /// <summary>The media-size-name member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaSizeName = new("media-size-name");
    /// <summary>The media-source member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaSource = new("media-source");
    /// <summary>The media-source-properties member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaSourceProperties = new("media-source-properties");
    /// <summary>The media-thickness member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaThickness = new("media-thickness");
    /// <summary>The media-tooth member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaTooth = new("media-tooth");
    /// <summary>The media-top-margin member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaTopMargin = new("media-top-margin");
    /// <summary>The media-type member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaType = new("media-type");
    /// <summary>The media-weight-metric member attribute. See: PWG 5101.1</summary>
    public static readonly MediaColMember MediaWeightMetric = new("media-weight-metric");

    public override string ToString() => Value;
    public static implicit operator string(MediaColMember value) => value.Value;
    public static implicit operator MediaColMember(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
