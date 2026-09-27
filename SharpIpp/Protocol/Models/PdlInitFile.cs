using System;
using SharpIpp.Mapping;
using SharpIpp.Validation;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>pdl-init-file</c> collection.
/// See: PWG 5100.11
/// </summary>
[IppAttribute(IppAttributeNames.PdlInitFile)]
public class PdlInitFile : IIppCollection
{
    public IppValue<Uri>? PdlInitFileLocation { get; set; }

    [ByteRange(1, 255)]
    public IppValue<string>? PdlInitFileName { get; set; }
}
