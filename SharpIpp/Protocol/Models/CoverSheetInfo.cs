using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>cover-sheet-info</c> collection.
/// See: PWG 5100.15-2013 Section 7.4.8
/// </summary>
[IppAttribute(IppAttributeNames.CoverSheetInfo)]
public class CoverSheetInfo : IIppCollection
{
    public IppValue<string>? FromName { get; set; }

    [IppAttribute("logo", Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? Logo { get; set; }

    [IppAttribute("message", Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? Message { get; set; }

    [IppAttribute("organization-name", Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? OrganizationName { get; set; }

    [IppAttribute("subject", Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? Subject { get; set; }

    public IppValue<string>? ToName { get; set; }
}
