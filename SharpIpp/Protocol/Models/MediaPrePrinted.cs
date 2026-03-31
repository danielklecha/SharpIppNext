using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models;
public readonly record struct MediaPrePrinted(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly MediaPrePrinted Blank = new("blank");
    public static readonly MediaPrePrinted PrePrinted = new("pre-printed");
    public static readonly MediaPrePrinted LetterHead = new("letter-head");

    public override string ToString() => Value;
    public static implicit operator string(MediaPrePrinted bin) => bin.Value;
    public static explicit operator MediaPrePrinted(string value) => new(value);
}
