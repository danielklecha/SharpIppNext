using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for configured resource entries in System status.
/// See: PWG 5100.22-2025 Section 7.3.10
/// </summary>
[IppAttribute(IppAttributeNames.SystemConfiguredResources)]
public class SystemConfiguredResource : IIppCollection
{
    bool INoValueWritable.IsValue { get; set; } = true;
    bool INoValue.IsValue => ((INoValueWritable)this).IsValue;

    [IppAttribute(IppAttributeNames.ResourceFormat, Tag = Tag.MimeMediaType)]
    public ResourceFormat? ResourceFormat { get; set; }

    public int? ResourceId { get; set; }

    [IppAttribute(IppAttributeNames.ResourceInfo, Tag = Tag.TextWithoutLanguage)]
    public string? ResourceInfo { get; set; }

    public string? ResourceName { get; set; }
    public ResourceState? ResourceState { get; set; }
    public ResourceStateReason[]? ResourceStateReasons { get; set; }
    public ResourceType? ResourceType { get; set; }
}
