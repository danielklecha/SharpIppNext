using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Models.Requests;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;


[TestClass]
[ExcludeFromCodeCoverage]
public class ResourceStatusAttributesProfileTests
{
    private readonly IMapper _mapper;

    public ResourceStatusAttributesProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

[TestMethod]
public void Map_Dictionary_To_ResourceStatusAttributes_ShouldMapResourceUuidAndTimes()
        {
            // Arrange
            var dict = new Dictionary<string, IppAttribute[]>
            {
                { "resource-id", [new IppAttribute(Tag.Integer, "resource-id", 55)] },
                { "resource-uuid", [new IppAttribute(Tag.Uri, "resource-uuid", "urn:uuid:123e4567-e89b-12d3-a456-426614174000")] },
                { "time-at-canceled", [new IppAttribute(Tag.Integer, "time-at-canceled", 8)] },
                { "time-at-creation", [new IppAttribute(Tag.Integer, "time-at-creation", 16)] },
                { "time-at-installed", [new IppAttribute(Tag.Integer, "time-at-installed", 24)] }
            };

            // Act
            var result = _mapper.Map<ResourceStatusAttributes>(dict);

            // Assert
            result.ResourceId.Should().Be(55);
            result.ResourceUuid.Should().Be(new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174000"));
            result.TimeAtCanceled.Should().Be(8);
            result.TimeAtCreation.Should().Be(16);
            result.TimeAtInstalled.Should().Be(24);
    }

[TestMethod]
    public void Map_ResourceStatusAttributes_To_Attributes_ShouldIncludeResourceUuidAndVersion()
    {
        // Arrange
        var src = new ResourceStatusAttributes
        {
            ResourceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174000"),
            ResourceVersion = "1.0.0",
            ResourceStringVersion = "1.0"
        };

        // Act
        var result = _mapper.Map<IDictionary<string, IppAttribute[]>>(src);

        // Assert
        result.Should().ContainKey("resource-uuid");
        result.Should().ContainKey("resource-version");
        result.Should().ContainKey("resource-string-version");
        result["resource-uuid"].Single().Value.Should().Be("urn:uuid:123e4567-e89b-12d3-a456-426614174000");
        result["resource-version"].Single().Value.Should().Be("1.0.0");
        result["resource-string-version"].Single().Value.Should().Be("1.0");
    }
}
