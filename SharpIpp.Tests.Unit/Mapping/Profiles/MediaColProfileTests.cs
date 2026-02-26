using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class MediaColProfileTests
{
    private readonly IMapper _mapper;

    public MediaColProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_MediaCol_WithOutOfBandCondition_WhenCount1_Length1_NotOutOfBand()
    {
        // Assert: src.Count == 1, first.Length == 1, but Tag != IsOutOfBand
        // It should map to a typical MediaCol instead of falling into the IsOutOfBand shortcut.
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "media-bottom-margin", new[] { new IppAttribute(Tag.Integer, "media-bottom-margin", 100) } }
        };

        var result = _mapper.Map<MediaCol>(dict);

        result.MediaBottomMargin.Should().Be(100);
        
        // This confirms it did NOT construct the "NoValue" version
        result.MediaLeftMargin.Should().BeNull();
    }

    [TestMethod]
    public void Map_MediaCol_WithOutOfBandCondition_WhenCount1_Length2()
    {
        // Assert: src.Count == 1, but first.Length > 1
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "media-bottom-margin", new[] 
                { 
                    new IppAttribute(Tag.Integer, "media-bottom-margin", 10),
                    new IppAttribute(Tag.Integer, "media-bottom-margin", 20)
                } 
            }
        };

        // Even though mapping multi-value to `int?` will just take the first value via MapFromDicNullable,
        // it verifies we don't crash and don't take the `no-value` shortcut path.
        var result = _mapper.Map<MediaCol>(dict);

        result.MediaBottomMargin.Should().Be(10); 
        result.MediaLeftMargin.Should().BeNull();
    }

    [TestMethod]
    public void Map_MediaCol_WithOutOfBandCondition_WhenCountGreaterThan1()
    {
        // Assert: src.Count > 1
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "media-bottom-margin", new[] { new IppAttribute(Tag.Integer, "media-bottom-margin", 100) } },
            { "media-left-margin", new[] { new IppAttribute(Tag.Integer, "media-left-margin", 200) } }
        };

        var result = _mapper.Map<MediaCol>(dict);

        result.MediaBottomMargin.Should().Be(100);
        result.MediaLeftMargin.Should().Be(200);
    }

    [TestMethod]
    public void Map_MediaCol_WithOutOfBandCondition_WhenArrayIsEmpty()
    {
        // Assert: src.Count == 1, but first.Length == 0 (empty array)
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "media-bottom-margin", Array.Empty<IppAttribute>() }
        };

        var result = _mapper.Map<MediaCol>(dict);

        result.MediaBottomMargin.Should().BeNull();
        result.MediaLeftMargin.Should().BeNull();
    }

    [TestMethod]
    public void Map_MediaCol_WithOutOfBandCondition_WhenCount1_Length1_IsOutOfBand()
    {
        // Assert: src.Count == 1, first.Length == 1, and Tag == IsOutOfBand (e.g., NoValue)
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "media-col", new[] { new IppAttribute(Tag.NoValue, "media-col", NoValue.Instance) } }
        };

        var result = _mapper.Map<MediaCol>(dict);

        // Should return a MediaCol with all properties set to NoValue
        result.MediaBottomMargin.Should().Be(NoValue.GetNoValue<int>());
        result.MediaSource.Should().Be(NoValue.GetNoValue<MediaSource>());
        result.MediaBackCoating.Should().Be(NoValue.GetNoValue<MediaCoating>());
        // And nullable fields should NOT be null, since the shortcut explicitly sets them to the NoValue representations.
    }
}
