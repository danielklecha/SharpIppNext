namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for configured resource entries in System status.
/// See: PWG 5100.22-2025 Section 7.3.10
/// </summary>
public class SystemConfiguredResource : IIppCollection
{
    bool IIppCollection.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((IIppCollection)this).IsValue;

    public string? ResourceFormat { get; set; }
    public int? ResourceId { get; set; }
    public string? ResourceInfo { get; set; }
    public string? ResourceName { get; set; }
    public ResourceState? ResourceState { get; set; }
    public ResourceStateReason[]? ResourceStateReasons { get; set; }
    public string? ResourceType { get; set; }
}