using System;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>job-save-disposition</c> collection.
/// See: PWG 5100.11
/// </summary>
public class JobSaveDisposition : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;
    public SaveDisposition? SaveDisposition { get; set; }
    public SaveInfo[]? SaveInfo { get; set; }
    public Uri? SaveLocation { get; set; }
}
