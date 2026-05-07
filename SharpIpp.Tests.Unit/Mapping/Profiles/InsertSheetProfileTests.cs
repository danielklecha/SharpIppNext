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
public class InsertSheetProfileTests
{
    private readonly IMapper _mapper;

    public InsertSheetProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

[TestMethod]
    [DataRow(true, true, true, true, DisplayName = "All present")]
    [DataRow(true, false, false, false, DisplayName = "Only InsertAfterPageNumber")]
    [DataRow(false, true, false, false, DisplayName = "Only InsertCount")]
    [DataRow(false, false, true, false, DisplayName = "Only Media")]
    [DataRow(false, false, false, true, DisplayName = "Only MediaCol")]
    [DataRow(false, false, false, false, DisplayName = "None present")]
    public void Map_Dictionary_To_InsertSheet_Coverage(bool h1, bool h2, bool h3, bool h4)
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>();
        if (h1) dict.Add("insert-after-page-number", new[] { new IppAttribute(Tag.Integer, "insert-after-page-number", 1) });
        if (h2) dict.Add("insert-count", new[] { new IppAttribute(Tag.Integer, "insert-count", 2) });
        if (h3) dict.Add("media", new[] { new IppAttribute(Tag.Keyword, "media", "iso_a4_210x297mm") });
        if (h4) dict.Add("media-col", new[] { new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance), new IppAttribute(Tag.MemberAttrName, "", "media-color"), new IppAttribute(Tag.Keyword, "", "blue"), new IppAttribute(Tag.EndCollection, "", NoValue.Instance) });

        // Act
        var result = _mapper.Map<InsertSheet>(dict);

        // Assert
        if (h1) result.InsertAfterPageNumber.Should().Be(1); else result.InsertAfterPageNumber.Should().BeNull();
        if (h2) result.InsertCount.Should().Be(2); else result.InsertCount.Should().BeNull();
        if (h3) result.Media.Should().Be((Media)"iso_a4_210x297mm"); else result.Media.Should().BeNull();
        if (h4) result.MediaCol.Should().NotBeNull(); else result.MediaCol.Should().BeNull();
    }

[TestMethod]
    [DataRow(true, true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, false, DisplayName = "None present")]
    public void Map_InsertSheet_To_Attributes_Coverage(bool h1, bool h2, bool h3, bool h4)
    {
        // Arrange
        var sheet = new InsertSheet
        {
            InsertAfterPageNumber = h1 ? 1 : null,
            InsertCount = h2 ? 2 : null,
            Media = h3 ? (Media)"iso_a4_210x297mm" : null,
            MediaCol = h4 ? new MediaCol { MediaColor = (MediaColor)"blue" } : null
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(sheet).ToList();

        // Assert
        if (h1) result.Should().Contain(a => a.Name == "insert-after-page-number"); else result.Should().NotContain(a => a.Name == "insert-after-page-number");
        if (h2) result.Should().Contain(a => a.Name == "insert-count"); else result.Should().NotContain(a => a.Name == "insert-count");
        if (h3) result.Should().Contain(a => a.Name == "media"); else result.Should().NotContain(a => a.Name == "media");
        if (h4) result.Should().Contain(a => a.Name == "media-col"); else result.Should().NotContain(a => a.Name == "media-col");
    }
}
