using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Specifies the media-grain member attribute of the media-col collection, indicating the grain direction of the media.
/// See: PWG 5101.1
/// </summary>
public readonly record struct MediaGrain(string Value, bool IsValue = true) : ISmartEnum 
{
    /// <summary>
    /// The grain of the media runs in the X (horizontal) direction.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaGrain XDirection = new("x-direction");

    /// <summary>
    /// The grain of the media runs in the Y (vertical) direction.
    /// See: PWG 5101.1
    /// </summary>
    public static readonly MediaGrain YDirection = new("y-direction");

    public override string ToString() => Value;
    public static implicit operator string(MediaGrain bin) => bin.Value;
    public static implicit operator MediaGrain(string value) => new(value);
}
