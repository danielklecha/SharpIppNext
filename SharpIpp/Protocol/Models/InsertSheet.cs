using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// See: PWG 5100.3-2023 Section 5.2.6
/// </summary>
public class InsertSheet : IIppCollection
{
    /// <inheritdoc />
    public bool IsNoValue { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? InsertAfterPageNumber { get; set; }

    /// <summary>
    /// integer(0:MAX)
    /// </summary>
    public int? InsertCount { get; set; }

    /// <summary>
    /// keyword | name(MAX)
    /// </summary>
    public Media? Media { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public MediaCol? MediaCol { get; set; }
}
