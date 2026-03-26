using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents the <c>cover-front</c> / <c>cover-back</c> collection value.
/// See: PWG 5100.3-2023 Section 5.2.1
/// Deprecated in: PWG 5100.3-2023 Section 5.2.1
/// </summary>
public class Cover : IIppCollection
{
    /// <inheritdoc />
    bool IIppCollection.IsNoValue { get; set; }

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
