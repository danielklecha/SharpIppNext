using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models;
public readonly record struct MediaGrain(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly MediaGrain XDirection = new("x-direction");
    public static readonly MediaGrain YDirection = new("y-direction");

    public override string ToString() => Value;
    public static implicit operator string(MediaGrain bin) => bin.Value;
    public static explicit operator MediaGrain(string value) => new(value);
}
