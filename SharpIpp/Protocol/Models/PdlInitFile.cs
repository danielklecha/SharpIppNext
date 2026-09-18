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
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;
    public Uri? PdlInitFileLocation { get; set; }

    [ByteRange(1, 255)]
    public string? PdlInitFileName { get; set; }
}
