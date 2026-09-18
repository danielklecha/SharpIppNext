using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>document-access</c> collection.
/// DEPRECATED.
/// See: PWG 5100.18-2025 Section 7.1.5
/// </summary>
[Obsolete("The 'document-access' attribute is deprecated in favor of URI authentication. See PWG 5100.18-2025 Section 7.1.5.")]
[IppAttribute(IppAttributeNames.DocumentAccess)]
public class DocumentAccess : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public string? AccessOAuthToken { get; set; }

    public Uri? AccessOAuthUri { get; set; }

    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public string? AccessPassword { get; set; }

    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public string? AccessPin { get; set; }

    public string? AccessUserName { get; set; }

    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public string? AccessX509Certificate { get; set; }
}
