using System;
using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// Specifies the media-coating member attribute of the media-col collection.
    /// See: PWG 5101.1
    /// </summary>
    public readonly record struct MediaCoating(string Value, bool IsValue = true) : ISmartEnum
    {
        /// <summary>
        /// No coating is applied to the media.
        /// See: PWG 5101.1
        /// </summary>
        public static readonly MediaCoating None = new("none");

        /// <summary>
        /// A glossy coating is applied to the media.
        /// See: PWG 5101.1
        /// </summary>
        public static readonly MediaCoating Glossy = new("glossy");

        /// <summary>
        /// A high-gloss coating is applied to the media.
        /// See: PWG 5101.1
        /// </summary>
        public static readonly MediaCoating HighGloss = new("high-gloss");

        /// <summary>
        /// A semi-gloss coating is applied to the media.
        /// See: PWG 5101.1
        /// </summary>
        public static readonly MediaCoating SemiGloss = new("semi-gloss");

        /// <summary>
        /// A satin coating is applied to the media.
        /// See: PWG 5101.1
        /// </summary>
        public static readonly MediaCoating Satin = new("satin");

        /// <summary>
        /// A matte coating is applied to the media.
        /// See: PWG 5101.1
        /// </summary>
        public static readonly MediaCoating Matte = new("matte");

        public override string ToString() => Value;
        public static implicit operator string(MediaCoating bin) => bin.Value;
        public static implicit operator MediaCoating(string value) => new(value);
    }
}
