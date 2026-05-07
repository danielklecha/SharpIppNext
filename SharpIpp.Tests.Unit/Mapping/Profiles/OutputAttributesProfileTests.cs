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
public class OutputAttributesProfileTests
{
    private readonly IMapper _mapper;

    public OutputAttributesProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

[TestMethod]
    public void Map_OutputAttributes_NoiseRemoval_UsesIntegerSyntax()
    {
        // Arrange
        var outputAttributes = new OutputAttributes
        {
            NoiseRemoval = 80,
            OutputCompressionQualityFactor = 65
        };

        // Act
        var serialized = _mapper.Map<IEnumerable<IppAttribute>>(outputAttributes).ToList();
        var parsed = _mapper.Map<OutputAttributes>(serialized.ToIppDictionary());

        // Assert
        serialized.Should().Contain(a => a.Name == "noise-removal" && a.Tag == Tag.Integer && a.Value!.Equals(80));
        parsed.NoiseRemoval.Should().Be(80);
        parsed.OutputCompressionQualityFactor.Should().Be(65);
    }
}
