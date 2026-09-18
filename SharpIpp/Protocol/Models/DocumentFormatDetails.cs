using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// "document-format-details" member attributes.
/// DEPRECATED.
/// See: PWG 5100.7-2023 Section 6.1.2
/// </summary>
[IppAttribute(IppAttributeNames.DocumentFormatDetails)]
[Obsolete("See PWG 5100.7-2023 Section 6.1.2.")]
public class DocumentFormatDetails : IIppCollection
{
    /// <inheritdoc />
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    /// <summary>
    /// name(MAX)
    /// </summary>
    [IppAttribute(Tag = Tag.NameWithoutLanguage)]
    public string? DocumentSourceApplicationName { get; set; }

    /// <summary>
    /// text(127)
    /// </summary>
    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public string? DocumentSourceApplicationVersion { get; set; }

    /// <summary>
    /// name(40)
    /// </summary>
    [IppAttribute(Tag = Tag.NameWithoutLanguage)]
    public string? DocumentSourceOsName { get; set; }

    /// <summary>
    /// text(40)
    /// </summary>
    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public string? DocumentSourceOsVersion { get; set; }
}
