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
public class JobCounterProfileTests
{
    private readonly IMapper _mapper;

    public JobCounterProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

[TestMethod]
    public void Map_Dictionary_To_JobCounter_Coverage()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "blank", [new IppAttribute(Tag.Integer, "blank", 1)] },
            { "full-color", [new IppAttribute(Tag.Integer, "full-color", 2)] },
            { "monochrome-two-sided", [new IppAttribute(Tag.Integer, "monochrome-two-sided", 3)] },
        };

        // Act
        var result = _mapper.Map<JobCounter>(dict);

        // Assert
        result.Blank.Should().Be(1);
        result.FullColor.Should().Be(2);
        result.MonochromeTwoSided.Should().Be(3);
        result.HighlightColor.Should().BeNull();
    }
}
