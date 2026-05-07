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
public class CoverProfileTests
{
    private readonly IMapper _mapper;

    public CoverProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    [DataRow(true, true, true, DisplayName = "All present")]
    [DataRow(true, false, false, DisplayName = "Only CoverType")]
    [DataRow(false, true, false, DisplayName = "Only Media")]
    [DataRow(false, false, true, DisplayName = "Only MediaCol")]
    [DataRow(false, false, false, DisplayName = "None present")]
    public void Map_Dictionary_To_Cover_Coverage(bool includeCoverType, bool includeMedia, bool includeMediaCol)
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>();
        if (includeCoverType)
            dict.Add("cover-type", new[] { new IppAttribute(Tag.Keyword, "cover-type", "print-both") });
        if (includeMedia)
            dict.Add("media", new[] { new IppAttribute(Tag.Keyword, "media", "iso_a4_210x297mm") });
        if (includeMediaCol)
            dict.Add("media-col", new[] { new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance), new IppAttribute(Tag.MemberAttrName, "", "media-color"), new IppAttribute(Tag.Keyword, "", "blue"), new IppAttribute(Tag.EndCollection, "", NoValue.Instance) });

        // Act
        var result = _mapper.Map<Cover>(dict);

        // Assert
        if (includeCoverType) result.CoverType.Should().Be(CoverType.PrintBoth); else result.CoverType.Should().BeNull();
        if (includeMedia) result.Media.Should().Be((Media)"iso_a4_210x297mm"); else result.Media.Should().BeNull();
        if (includeMediaCol) result.MediaCol.Should().NotBeNull(); else result.MediaCol.Should().BeNull();
    }

    [TestMethod]
    [DataRow(true, true, true, DisplayName = "All present")]
    [DataRow(true, false, false, DisplayName = "Only CoverType")]
    [DataRow(false, true, false, DisplayName = "Only Media")]
    [DataRow(false, false, true, DisplayName = "Only MediaCol")]
    [DataRow(false, false, false, DisplayName = "None present")]
    public void Map_Cover_To_Attributes_Coverage(bool hasCoverType, bool hasMedia, bool hasMediaCol)
    {
        // Arrange
        var cover = new Cover
        {
            CoverType = hasCoverType ? CoverType.PrintBoth : null,
            Media = hasMedia ? (Media)"iso_a4_210x297mm" : null,
            MediaCol = hasMediaCol ? new MediaCol { MediaColor = (MediaColor)"blue" } : null
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(cover).ToList();

        // Assert
        if (hasCoverType) result.Should().Contain(a => a.Name == "cover-type"); else result.Should().NotContain(a => a.Name == "cover-type");
        if (hasMedia) result.Should().Contain(a => a.Name == "media"); else result.Should().NotContain(a => a.Name == "media");
        if (hasMediaCol) result.Should().Contain(a => a.Name == "media-col"); else result.Should().NotContain(a => a.Name == "media-col");
    }
}
