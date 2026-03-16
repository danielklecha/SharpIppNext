using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using Range = SharpIpp.Protocol.Models.Range;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class MediaSizeSupportedProfileTests
{
    private readonly IMapper _mapper;

    public MediaSizeSupportedProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_MediaSizeSupported_ToAttributes_UsesIntegerTag_ForSingleValueRanges()
    {
        // Arrange
        var mediaSizeSupported = new MediaSizeSupported
        {
            XDimension = new Range(21000, 21000),
            YDimension = new Range(29700, 29700)
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(mediaSizeSupported).ToList();

        // Assert
        result.Should().ContainEquivalentOf(new IppAttribute(Tag.Integer, "x-dimension", 21000));
        result.Should().ContainEquivalentOf(new IppAttribute(Tag.Integer, "y-dimension", 29700));
    }

    [TestMethod]
    public void Map_MediaSizeSupported_ToAttributes_UsesRangeOfIntegerTag_ForRangeValues()
    {
        // Arrange
        var mediaSizeSupported = new MediaSizeSupported
        {
            XDimension = new Range(100, 200),
            YDimension = new Range(300, 400)
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(mediaSizeSupported).ToList();

        // Assert
        result.Should().ContainEquivalentOf(new IppAttribute(Tag.RangeOfInteger, "x-dimension", new Range(100, 200)));
        result.Should().ContainEquivalentOf(new IppAttribute(Tag.RangeOfInteger, "y-dimension", new Range(300, 400)));
    }
}
