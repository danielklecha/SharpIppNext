using SharpIpp.Mapping;
using SharpIpp.Validation;
using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>save-info</c> collection.
/// See: PWG 5100.11
/// </summary>
[IppAttribute(IppAttributeNames.SaveInfo)]
public class SaveInfo : IIppCollection
{
    public IppValue<Uri>? SaveLocation { get; set; }
    
    [ByteRange(1, 255)]
    public IppValue<string>? SaveName { get; set; }

    [ByteRange(1, 255)]
    [IppAttribute(IppAttributeNames.SaveDocumentFormat, Tag = Tag.MimeMediaType)]
    public IppValue<string>? SaveDocumentFormat { get; set; }
}
