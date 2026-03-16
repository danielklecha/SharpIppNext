using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models;
public readonly record struct MediaTooth(string Value)
{
    public static readonly MediaTooth Antique = new("antique");
    public static readonly MediaTooth Calendared = new("calendared");
    public static readonly MediaTooth Coarse = new("coarse");
    public static readonly MediaTooth Fine = new("fine");
    public static readonly MediaTooth Linen = new("linen");
    public static readonly MediaTooth Medium = new("medium");
    public static readonly MediaTooth Smooth = new("smooth");
    public static readonly MediaTooth Stipple = new("stipple");
    public static readonly MediaTooth Uncalendared = new("uncalendared");
    public static readonly MediaTooth Vellum = new("vellum");

    public override string ToString() => Value;
    public static implicit operator string(MediaTooth bin) => bin.Value;
    public static explicit operator MediaTooth(string value) => new(value);
}
