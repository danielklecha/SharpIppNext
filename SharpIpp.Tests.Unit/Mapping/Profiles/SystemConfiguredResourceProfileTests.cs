using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class SystemConfiguredResourceProfileTests : MapperTestBase
{

    [TestMethod]
    public void Map_Dictionary_To_SystemConfiguredResource_ShouldMapKeywordSmartEnumResourceType()
    {
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "resource-format", [new IppAttribute(Tag.MimeMediaType, "resource-format", "application/pdf")] },
            { "resource-id", [new IppAttribute(Tag.Integer, "resource-id", 202)] },
            { "resource-info", [new IppAttribute(Tag.TextWithoutLanguage, "resource-info", "configured-resource-info")] },
            { "resource-name", [new IppAttribute(Tag.NameWithoutLanguage, "resource-name", "configured-resource")] },
            { "resource-state", [new IppAttribute(Tag.Enum, "resource-state", (int)ResourceState.Available)] },
            { "resource-state-reasons", [new IppAttribute(Tag.Keyword, "resource-state-reasons", "none")] },
            { "resource-type", [new IppAttribute(Tag.Keyword, "resource-type", "x-vendor-resource")] }
        };

        var result = _mapper.Map<SystemConfiguredResource>(dict);

        result.ResourceFormat.Should().Be("application/pdf");
        result.ResourceId.Should().Be(202);
        result.ResourceState.Should().Be(ResourceState.Available);
        result.ResourceStateReasons.Should().Contain(ResourceStateReason.None);
        result.ResourceType.Should().Be((ResourceType)"x-vendor-resource");
    }

    [TestMethod]
    public void Map_SystemConfiguredResource_To_Attributes_ShouldSerializeKeywordSmartEnumResourceType()
    {
        var src = new SystemConfiguredResource
        {
            ResourceFormat = "application/pdf",
            ResourceId = 202,
            ResourceInfo = "configured-resource-info",
            ResourceName = "configured-resource",
            ResourceState = ResourceState.Available,
            ResourceStateReasons = new[] { ResourceStateReason.None },
            ResourceType = (ResourceType)"x-vendor-resource"
        };

        var result = _mapper.Map<IEnumerable<IppAttribute>>(src).ToList();

        result.Should().Contain(a => a.Name == "resource-type" && a.Tag == Tag.Keyword && a.Value != null && a.Value.ToString() == "x-vendor-resource");
    }
}
