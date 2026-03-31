using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles.Responses;

[TestClass]
[ExcludeFromCodeCoverage]
public class GetResourcesResponseProfileTests
{
    private readonly IMapper _mapper;

    public GetResourcesResponseProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_IppResponseMessageToGetResourcesResponse_LeavesResourcesAttributesNullWhenEmpty()
    {
        // Arrange
        var src = new IppResponseMessage
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk
        };

        // Act
        var dst = _mapper.Map<GetResourcesResponse>(src);

        // Assert
        dst.ResourcesAttributes.Should().BeNull();
    }

    [TestMethod]
    public void Map_IppResponseMessageToGetResourcesResponse_MapsResourcesAttributesWhenPresent()
    {
        // Arrange
        var src = new IppResponseMessage
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk,
            ResourceAttributes =
            {
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Integer, SystemAttribute.ResourceId, 7),
                    new IppAttribute(Tag.NameWithoutLanguage, SystemAttribute.ResourceName, "TrayProfile")
                }
            }
        };

        // Act
        var dst = _mapper.Map<GetResourcesResponse>(src);

        // Assert
        dst.ResourcesAttributes.Should().NotBeNull();
        dst.ResourcesAttributes!.Should().HaveCount(1);
        dst.ResourcesAttributes![0].ResourceId.Should().Be(7);
        dst.ResourcesAttributes![0].ResourceName.Should().Be("TrayProfile");
    }

    [TestMethod]
    public void Map_GetResourcesResponseToIppResponseMessage_LeavesResourceAttributesEmptyWhenNull()
    {
        // Arrange
        var src = new GetResourcesResponse
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk,
            ResourcesAttributes = null
        };

        // Act
        var dst = _mapper.Map<IppResponseMessage>(src);

        // Assert
        dst.ResourceAttributes.Should().BeEmpty();
    }

    [TestMethod]
    public void Map_GetResourcesResponseToIppResponseMessage_MapsResourceAttributesWhenPresent()
    {
        // Arrange
        var src = new GetResourcesResponse
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk,
            ResourcesAttributes =
            [
                new ResourceDescriptionAttributes
                {
                    ResourceId = 9,
                    ResourceType = "printer-resource"
                }
            ]
        };

        // Act
        var dst = _mapper.Map<IppResponseMessage>(src);

        // Assert
        dst.ResourceAttributes.Should().HaveCount(1);
        var firstResource = dst.ResourceAttributes.First();
        firstResource.Should().ContainSingle(a => a.Name == SystemAttribute.ResourceId && (int)a.Value == 9);
        firstResource.Should().ContainSingle(a => a.Name == SystemAttribute.ResourceType && (string)a.Value == "printer-resource");
    }
}
