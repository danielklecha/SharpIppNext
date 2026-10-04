using SharpIpp.Mapping;

namespace SharpIpp.Protocol.Models;

/// <summary>
/// Member attributes for configured resource entries in System status.
/// See: PWG 5100.22-2025 Section 7.3.10
/// </summary>
[IppAttribute(IppAttributeNames.SystemConfiguredResources)]
public class SystemConfiguredResource : IIppCollection
{

    [IppAttribute(IppAttributeNames.ResourceFormat, Tag = Tag.MimeMediaType)]
    public IppValue<ResourceFormat>? ResourceFormat { get; set; }

    public IppValue<int>? ResourceId { get; set; }

    [IppAttribute(IppAttributeNames.ResourceInfo, Tag = Tag.TextWithoutLanguage)]
    public StringWithLanguage? ResourceInfo { get; set; }

    public IppValue<string>? ResourceName { get; set; }
    public IppValue<ResourceState>? ResourceState { get; set; }
    public IppValue<ResourceStateReason[]>? ResourceStateReasons { get; set; }
    public IppValue<ResourceType>? ResourceType { get; set; }
}
