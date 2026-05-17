using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the media-tooth member attribute of the media-col collection, indicating the surface texture of the media.
/// See: PWG 5101.1
/// </summary>
public readonly record struct MediaTooth(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// Antique surface texture.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaTooth Antique = new("antique");

    /// <summary>
    /// Calendared (smooth, machine-finished) surface texture.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaTooth Calendared = new("calendared");

    /// <summary>
    /// Coarse surface texture.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaTooth Coarse = new("coarse");

    /// <summary>
    /// Fine surface texture.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaTooth Fine = new("fine");

    /// <summary>
    /// Linen surface texture.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaTooth Linen = new("linen");

    /// <summary>
    /// Medium surface texture.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaTooth Medium = new("medium");

    /// <summary>
    /// Smooth surface texture.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaTooth Smooth = new("smooth");

    /// <summary>
    /// Stipple surface texture.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaTooth Stipple = new("stipple");

    /// <summary>
    /// Uncalendared (rough, unfinished) surface texture.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaTooth Uncalendared = new("uncalendared");

    /// <summary>
    /// Vellum surface texture.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaTooth Vellum = new("vellum");

    public override string ToString() => Value;
    public static implicit operator string(MediaTooth bin) => bin.Value;
    public static explicit operator MediaTooth(string value) => new(value);
}
