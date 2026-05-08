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
public class CoverSheetInfoProfileTests : MapperTestBase
{

    [TestMethod]
    public void Map_CoverSheetInfo_To_Attributes_Should_Include_TextFields()
    {
        // Arrange
        var coverSheetInfo = new CoverSheetInfo
        {
            FromName = "from",
            Logo = "logo",
            Message = "message",
            OrganizationName = "org",
            Subject = "subject",
            ToName = "to"
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(coverSheetInfo).ToList();

        // Assert
        result.Should().Contain(a => a.Name == "logo" && a.Tag == Tag.TextWithoutLanguage && a.Value!.Equals("logo"));
        result.Should().Contain(a => a.Name == "message" && a.Tag == Tag.TextWithoutLanguage && a.Value!.Equals("message"));
        result.Should().Contain(a => a.Name == "organization-name" && a.Tag == Tag.TextWithoutLanguage && a.Value!.Equals("org"));
    }
}
