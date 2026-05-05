namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>job-storage</c> collection.
/// See: PWG 5100.11-2024 Section 5.2.5
/// </summary>
public class JobStorage : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;
    public string? JobStorageAccess { get; set; }
    public string? JobStorageDisposition { get; set; }
    public string? JobStorageGroup { get; set; }
}
