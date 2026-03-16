using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents the <c>separator-sheets</c> collection.
/// See: PWG 5100.3-2023 Section 5.2.16
/// </summary>
public class SeparatorSheets : IIppCollection
{
    /// <inheritdoc />
    public bool IsNoValue { get; set; }

    /// <summary>
    /// keyword | name(MAX)
    /// </summary>
    public Media? Media { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public MediaCol? MediaCol { get; set; }

    /// <summary>
    /// 1setOf (type2 keyword | name(MAX))
    /// </summary>
    public SeparatorSheetsType[]? SeparatorSheetsType { get; set; }
}
