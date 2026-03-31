using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models
{
    public readonly record struct MediaCoating(string Value, bool IsValue = true) : ISmartEnum
    {
        public static readonly MediaCoating None = new("none");
        public static readonly MediaCoating Glossy = new("glossy");
        public static readonly MediaCoating HighGloss = new("high-gloss");
        public static readonly MediaCoating SemiGloss = new("semi-gloss");
        public static readonly MediaCoating Satin = new("satin");
        public static readonly MediaCoating Matte = new("matte");

        public override string ToString() => Value;
        public static implicit operator string(MediaCoating bin) => bin.Value;
        public static explicit operator MediaCoating(string value) => new(value);
    }
}
