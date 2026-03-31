namespace SharpIpp.Protocol.Models;

/// <summary>
/// The <c>job-storage</c> collection.
/// See: PWG 5100.11-2024 Section 5.2.5
/// </summary>
public class JobStorage : IIppCollection
{
    public bool IsValue { get; set; } = true;
    public string? JobStorageAccess { get; set; }
    public string? JobStorageDisposition { get; set; }
    public string? JobStorageGroup { get; set; }
}
