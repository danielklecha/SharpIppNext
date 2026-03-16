using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models;
public readonly record struct MediaRecycled(string Value)
{
    public static readonly MediaRecycled None = new("none");
    public static readonly MediaRecycled Standard = new("standard");

    public override string ToString() => Value;
    public static implicit operator string(MediaRecycled bin) => bin.Value;
    public static explicit operator MediaRecycled(string value) => new(value);
}
