using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using Range = SharpIpp.Protocol.Models.Range;
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class MediaSizeProfileTests : MapperTestBase
{
    [TestMethod]
    public void Map_MediaSize_ToAttributes_UsesIntegerTag_ForSingleValueRanges()
    {
        // Arrange
        var mediaSize = new MediaSize
        {
            XDimension = 21000,
            YDimension = 29700
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(mediaSize).ToList();

        // Assert
        result.Should().ContainEquivalentOf(new IppAttribute(Tag.Integer, "x-dimension", 21000));
        result.Should().ContainEquivalentOf(new IppAttribute(Tag.Integer, "y-dimension", 29700));
    }

    [TestMethod]
    public void Map_MediaSize_ToAttributes_UsesRangeOfIntegerTag_ForRangeValues()
    {
        // Arrange
        var mediaSize = new MediaSize
        {
            XDimension = new Range(100, 200),
            YDimension = new Range(300, 400)
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(mediaSize).ToList();

        // Assert
        result.Should().ContainEquivalentOf(new IppAttribute(Tag.RangeOfInteger, "x-dimension", new Range(100, 200)));
        result.Should().ContainEquivalentOf(new IppAttribute(Tag.RangeOfInteger, "y-dimension", new Range(300, 400)));
    }

    [TestMethod]
    public void Map_MediaSize_FromAttributes_ReadsIntegerTags_AsRanges()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "x-dimension", new[] { new IppAttribute(Tag.Integer, "x-dimension", 21000) } },
            { "y-dimension", new[] { new IppAttribute(Tag.Integer, "y-dimension", 29700) } }
        };

        // Act
        var result = _mapper.Map<MediaSize>(dict);

        // Assert
        result.XDimension.Should().Be(new Range(21000, 21000));
        result.YDimension.Should().Be(new Range(29700, 29700));
    }

    [TestMethod]
    public void Map_MediaSize_FromAttributes_ReadsRangeOfIntegerTags_AsRanges()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "x-dimension", new[] { new IppAttribute(Tag.RangeOfInteger, "x-dimension", new Range(100, 200)) } },
            { "y-dimension", new[] { new IppAttribute(Tag.RangeOfInteger, "y-dimension", new Range(300, 400)) } }
        };

        // Act
        var result = _mapper.Map<MediaSize>(dict);

        // Assert
        result.XDimension.Should().Be(new Range(100, 200));
        result.YDimension.Should().Be(new Range(300, 400));
    }
}
