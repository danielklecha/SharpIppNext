using System;
using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>job-save-disposition</c> collection.
/// See: PWG 5100.11
/// </summary>
[IppAttribute(IppAttributeNames.JobSaveDisposition)]
public class JobSaveDisposition : IIppCollection
{
    public IppValue<SaveDisposition>? SaveDisposition { get; set; }
    public IppValue<SaveInfo[]>? SaveInfo { get; set; }
    public IppValue<Uri>? SaveLocation { get; set; }
}
