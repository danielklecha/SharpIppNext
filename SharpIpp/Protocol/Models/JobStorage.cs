namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>job-storage</c> collection.
/// See: PWG 5100.11-2024 Section 5.2.5
/// </summary>
public class JobStorage : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;
    public JobStorageAccess? JobStorageAccess { get; set; }
    public JobStorageDisposition? JobStorageDisposition { get; set; }
    public string? JobStorageGroup { get; set; }
}
