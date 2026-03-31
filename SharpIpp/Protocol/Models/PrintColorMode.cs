using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models;
public readonly record struct PrintColorMode(string Value, bool IsValue = true) : ISmartEnum 
{
    public static readonly PrintColorMode Auto = new("auto");
    public static readonly PrintColorMode AutoMonochrome = new("auto-monochrome");
    public static readonly PrintColorMode BiLevel = new("bi-level");
    public static readonly PrintColorMode Color = new("color");
    public static readonly PrintColorMode Highlight = new("highlight");
    public static readonly PrintColorMode Monochrome = new("monochrome");
    public static readonly PrintColorMode ProcessBiLevel = new("process-bi-level");
    public static readonly PrintColorMode ProcessMonochrome = new("process-monochrome");

    public override string ToString() => Value;
    public static implicit operator string(PrintColorMode bin) => bin.Value;
    public static explicit operator PrintColorMode(string value) => new(value);
}
