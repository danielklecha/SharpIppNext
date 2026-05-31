using SharpIpp.Validation;
using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>save-info</c> collection.
/// See: PWG 5100.11
/// </summary>
public class SaveInfo : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;
    public Uri? SaveLocation { get; set; }
    
    [ByteRange(1, 255)]
    public string? SaveName { get; set; }

    [ByteRange(1, 255)]
    public string? SaveDocumentFormat { get; set; }
}
