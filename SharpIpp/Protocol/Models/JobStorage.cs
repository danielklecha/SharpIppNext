using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>job-storage</c> collection.
/// See: PWG 5100.11-2024 Section 5.2.5
/// </summary>
[IppAttribute(IppAttributeNames.JobStorage)]
public class JobStorage : IIppCollection
{
    public IppValue<JobStorageAccess>? JobStorageAccess { get; set; }
    public IppValue<JobStorageDisposition>? JobStorageDisposition { get; set; }
    public IppValue<string>? JobStorageGroup { get; set; }
}
