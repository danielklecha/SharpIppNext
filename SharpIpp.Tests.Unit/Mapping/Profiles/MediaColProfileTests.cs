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

    public static IEnumerable<object[]> MapMediaColData
    {
        get
        {
            yield return new object[]
            {
                new Dictionary<string, IppAttribute[]>
                {
                    { "media-bottom-margin", new[] { new IppAttribute(Tag.Integer, "media-bottom-margin", 100) } }
                },
                new Action<MediaCol>(result =>
                {
                    result.MediaBottomMargin.Should().Be(100);
                    result.MediaLeftMargin.Should().BeNull();
                }),
                "Count 1, Length 1, Not OutOfBand"
            };

            yield return new object[]
            {
                new Dictionary<string, IppAttribute[]>
                {
                    { "media-bottom-margin", new[]
                        {
                            new IppAttribute(Tag.Integer, "media-bottom-margin", 10),
                            new IppAttribute(Tag.Integer, "media-bottom-margin", 20)
                        }
                    }
                },
                new Action<MediaCol>(result =>
                {
                    result.MediaBottomMargin.Should().Be(10);
                    result.MediaLeftMargin.Should().BeNull();
                }),
                "Count 1, Length 2"
            };

            yield return new object[]
            {
                new Dictionary<string, IppAttribute[]>
                {
                    { "media-bottom-margin", new[] { new IppAttribute(Tag.Integer, "media-bottom-margin", 100) } },
                    { "media-left-margin", new[] { new IppAttribute(Tag.Integer, "media-left-margin", 200) } }
                },
                new Action<MediaCol>(result =>
                {
                    result.MediaBottomMargin.Should().Be(100);
                    result.MediaLeftMargin.Should().Be(200);
                }),
                "Count > 1"
            };

            yield return new object[]
            {
                new Dictionary<string, IppAttribute[]>
                {
                    { "media-bottom-margin", Array.Empty<IppAttribute>() }
                },
                new Action<MediaCol>(result =>
                {
                    result.MediaBottomMargin.Should().BeNull();
                    result.MediaLeftMargin.Should().BeNull();
                }),
                "Array is empty"
            };

            yield return new object[]
            {
                new Dictionary<string, IppAttribute[]>
                {
                    { "media-col", new[] { new IppAttribute(Tag.NoValue, "media-col", NoValue.Instance) } }
                },
                new Action<MediaCol>(result =>
                {
                    ((IIppCollection)result).IsNoValue.Should().BeTrue();
                    result.MediaBottomMargin.Should().BeNull();
                    result.MediaLeftMargin.Should().BeNull();
                }),
                "Count 1, Length 1, IsOutOfBand"
            };
        }
    }

    [TestMethod]
    [DynamicData(nameof(MapMediaColData))]
    public void Map_MediaCol(Dictionary<string, IppAttribute[]> dict, Action<MediaCol> assert, string description)
    {
        var result = _mapper.Map<MediaCol>(dict);
        assert(result);
    }
}
