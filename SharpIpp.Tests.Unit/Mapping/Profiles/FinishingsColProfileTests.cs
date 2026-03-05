using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class FinishingsColProfileTests
{
    private readonly IMapper _mapper;

    public FinishingsColProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_FinishingsCol_WithOutOfBandCondition_WhenCount1_Length1_IsOutOfBand()
    {
        // Assert: src.Count == 1, first.Length == 1, and Tag == IsOutOfBand (e.g., NoValue)
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "finishings-col", new[] { new IppAttribute(Tag.NoValue, "finishings-col", NoValue.Instance) } }
        };

        var result = _mapper.Map<FinishingsCol>(dict);

        // Should return a FinishingsCol with IsNoValue set to true
        result.IsNoValue.Should().BeTrue();
        // Sub-properties remain at their defaults (null) since only IsNoValue is set.
        result.FinishingTemplate.Should().BeNull();
        result.Stitching.Should().BeNull();
        result.Binding.Should().BeNull();
    }
}
