using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Represents the <c>separator-sheets</c> collection.
/// See: PWG 5100.3-2023 Section 5.2.16
/// </summary>
[IppAttribute(IppAttributeNames.SeparatorSheets)]
public class SeparatorSheets : IIppCollection
{
    /// <inheritdoc />

    /// <summary>
    /// keyword | name(MAX)
    /// </summary>
    public IppValue<Media>? Media { get; set; }

    /// <summary>
    /// collection
    /// </summary>
    public IppValue<MediaCol>? MediaCol { get; set; }

    /// <summary>
    /// 1setOf (type2 keyword | name(MAX))
    /// </summary>
    public IppValue<SeparatorSheetsType[]>? SeparatorSheetsType { get; set; }
}
