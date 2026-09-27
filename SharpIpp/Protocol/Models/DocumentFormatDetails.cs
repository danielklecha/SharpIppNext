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

    /// <summary>
    /// name(MAX)
    /// </summary>
    [IppAttribute(Tag = Tag.NameWithoutLanguage)]
    public IppValue<string>? DocumentSourceApplicationName { get; set; }

    /// <summary>
    /// text(127)
    /// </summary>
    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? DocumentSourceApplicationVersion { get; set; }

    /// <summary>
    /// name(40)
    /// </summary>
    [IppAttribute(Tag = Tag.NameWithoutLanguage)]
    public IppValue<string>? DocumentSourceOsName { get; set; }

    /// <summary>
    /// text(40)
    /// </summary>
    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? DocumentSourceOsVersion { get; set; }
}
