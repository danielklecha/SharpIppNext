using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the media-recycled member attribute of the media-col collection, indicating whether the media contains recycled content.
/// See: PWG 5101.1
/// </summary>
public readonly record struct MediaRecycled(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// The media contains no recycled content.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaRecycled None = new("none");

    /// <summary>
    /// The media contains standard recycled content.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaRecycled Standard = new("standard");

    public override string ToString() => Value;
    public static implicit operator string(MediaRecycled bin) => bin.Value;
    public static explicit operator MediaRecycled(string value) => new(value);
}
