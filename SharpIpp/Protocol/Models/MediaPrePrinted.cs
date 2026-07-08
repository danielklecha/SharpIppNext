using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the media-pre-printed member attribute of the media-col collection, indicating whether the media has pre-printed content.
/// See: PWG 5101.1
/// </summary>
public readonly record struct MediaPrePrinted(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// The media has no pre-printed content.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaPrePrinted Blank = new("blank");

    /// <summary>
    /// The media has pre-printed content (e.g., forms or logos).
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaPrePrinted PrePrinted = new("pre-printed");

    /// <summary>
    /// The media has a pre-printed letterhead.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaPrePrinted LetterHead = new("letter-head");

    public override string ToString() => Value;
    public static implicit operator string(MediaPrePrinted bin) => bin.Value;
    public static implicit operator MediaPrePrinted(string value) => value is null ? throw new System.ArgumentNullException(nameof(value)) : new(value);
}
