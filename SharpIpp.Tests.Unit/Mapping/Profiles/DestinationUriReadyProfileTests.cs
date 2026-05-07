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
public class DestinationUriReadyProfileTests
{
    private readonly IMapper _mapper;

    public DestinationUriReadyProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_DestinationUriReady_RoundTripsWithNestedDestinationAttributes()
    {
        // Arrange
        var destinationUriReady = new DestinationUriReady
        {
            DestinationUri = "https://example.test/upload",
            DestinationName = "Inbox",
            DestinationIsDirectory = true,
            DestinationAttributesSupported = ["job-name"],
            DestinationMandatoryAccessAttributes = ["access-user-name"],
            DestinationAttributes =
            [
                new Dictionary<string, IppAttribute[]>
                {
                    { "job-name", [new IppAttribute(Tag.NameWithoutLanguage, "job-name", "Scanned document")] }
                }
            ]
        };

        // Act
        var serialized = _mapper.Map<IEnumerable<IppAttribute>>(destinationUriReady).ToList();
        var parsed = _mapper.Map<DestinationUriReady>(serialized.ToIppDictionary());

        // Assert
        serialized.Should().Contain(x => x.Tag == Tag.Uri && x.Name == "destination-uri" && x.Value!.Equals("https://example.test/upload"));
        serialized.Should().Contain(x => x.Tag == Tag.BegCollection && x.Name == "destination-attributes");
        parsed.DestinationUri.Should().Be("https://example.test/upload");
        parsed.DestinationName.Should().Be("Inbox");
        parsed.DestinationIsDirectory.Should().BeTrue();
        parsed.DestinationAttributesSupported.Should().BeEquivalentTo("job-name");
        parsed.DestinationMandatoryAccessAttributes.Should().BeEquivalentTo("access-user-name");
        parsed.DestinationAttributes.Should().NotBeNull();
        parsed.DestinationAttributes!.Should().ContainSingle();
        parsed.DestinationAttributes[0].Should().ContainKey("job-name");
    }
}
