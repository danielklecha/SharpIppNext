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

    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? AccessOAuthToken { get; set; }

    public IppValue<Uri>? AccessOAuthUri { get; set; }

    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? AccessPassword { get; set; }

    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? AccessPin { get; set; }

    public IppValue<string>? AccessUserName { get; set; }

    [IppAttribute(Tag = Tag.TextWithoutLanguage)]
    public IppValue<string>? AccessX509Certificate { get; set; }
}
