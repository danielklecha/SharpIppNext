using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
///     See: PWG 5100.3-2023 Section 5.2.1 / 5.2.2
/// </summary>
public class Cover : IIppCollection
{
    /// <inheritdoc />
    public bool IsNoValue { get; set; }

    /// <summary>
    /// type2 keyword | name(MAX)
    /// </summary>
    public CoverType? CoverType { get; set; }

    /// <summary>
    /// keyword | name(MAX)
    /// </summary>
    public string? Media { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public MediaCol? MediaCol { get; set; }
}
