using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents the <c>cover-front</c> / <c>cover-back</c> collection value.
/// See: PWG 5100.3-2023 Section 5.2.1
/// </summary>
[Obsolete("See PWG 5100.3-2023 Section 5.2.1.")]
public class Cover : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public CoverType? CoverType { get; set; }

    /// <summary>
    /// keyword | name(MAX)
    /// </summary>
    public Media? Media { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public MediaCol? MediaCol { get; set; }
}
