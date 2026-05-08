using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Models.Requests;
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;


[TestClass]
[ExcludeFromCodeCoverage]
public class SeparatorSheetsProfileTests : MapperTestBase
{

[TestMethod]
    [DataRow(true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, DisplayName = "None present")]
    public void Map_Dictionary_To_SeparatorSheets_Coverage(bool h1, bool h2, bool h3)
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>();
        if (h1) dict.Add("media", new[] { new IppAttribute(Tag.Keyword, "media", "iso_a4_210x297mm") });
        if (h2) dict.Add("separator-sheets-type", new[] { new IppAttribute(Tag.Keyword, "separator-sheets-type", "slip-sheets") });
        if (h3) dict.Add("media-col", new[] { new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance), new IppAttribute(Tag.MemberAttrName, "", "media-color"), new IppAttribute(Tag.Keyword, "", "blue"), new IppAttribute(Tag.EndCollection, "", NoValue.Instance) });

        // Act
        var result = _mapper.Map<SeparatorSheets>(dict);

        // Assert
        if (h1) result.Media.Should().Be((Media)"iso_a4_210x297mm"); else result.Media.Should().BeNull();
        if (h2) result.SeparatorSheetsType.Should().Contain(SeparatorSheetsType.SlipSheets); else result.SeparatorSheetsType.Should().BeNull();
        if (h3) result.MediaCol.Should().NotBeNull(); else result.MediaCol.Should().BeNull();
    }

[TestMethod]
    [DataRow(true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, DisplayName = "None present")]
    public void Map_SeparatorSheets_To_Attributes_Coverage(bool h1, bool h2, bool h3)
    {
        // Arrange
        var sheets = new SeparatorSheets
        {
            Media = h1 ? (Media)"iso_a4_210x297mm" : null,
            SeparatorSheetsType = h2 ? new[] { SeparatorSheetsType.SlipSheets } : null,
            MediaCol = h3 ? new MediaCol { MediaColor = (MediaColor)"blue" } : null
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(sheets).ToList();

        // Assert
        if (h1) result.Should().Contain(a => a.Name == "media"); else result.Should().NotContain(a => a.Name == "media");
        if (h2) result.Should().Contain(a => a.Name == "separator-sheets-type"); else result.Should().NotContain(a => a.Name == "separator-sheets-type");
        if (h3) result.Should().Contain(a => a.Name == "media-col"); else result.Should().NotContain(a => a.Name == "media-col");
    }
}
