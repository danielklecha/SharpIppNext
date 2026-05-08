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
public class JobErrorSheetProfileTests : MapperTestBase
{

[TestMethod]
    [DataRow(true, true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, false, DisplayName = "None present")]
    public void Map_Dictionary_To_JobErrorSheet_Coverage(bool h1, bool h2, bool h3, bool h4)
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>();
        if (h1) dict.Add("job-error-sheet-type", new[] { new IppAttribute(Tag.Keyword, "job-error-sheet-type", "standard") });
        if (h2) dict.Add("job-error-sheet-when", new[] { new IppAttribute(Tag.Keyword, "job-error-sheet-when", "on-error") });
        if (h3) dict.Add("media", new[] { new IppAttribute(Tag.Keyword, "media", "iso_a4_210x297mm") });
        if (h4) dict.Add("media-col", new[] { new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance), new IppAttribute(Tag.MemberAttrName, "", "media-color"), new IppAttribute(Tag.Keyword, "", "blue"), new IppAttribute(Tag.EndCollection, "", NoValue.Instance) });

        // Act
        var result = _mapper.Map<JobErrorSheet>(dict);

        // Assert
        if (h1) result.JobErrorSheetType.Should().Be(JobSheetsType.Standard); else result.JobErrorSheetType.Should().BeNull();
        if (h2) result.JobErrorSheetWhen.Should().Be(JobErrorSheetWhen.OnError); else result.JobErrorSheetWhen.Should().BeNull();
        if (h3) result.Media.Should().Be((Media)"iso_a4_210x297mm"); else result.Media.Should().BeNull();
        if (h4) result.MediaCol.Should().NotBeNull(); else result.MediaCol.Should().BeNull();
    }

[TestMethod]
    [DataRow(true, true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, false, DisplayName = "None present")]
    public void Map_JobErrorSheet_To_Attributes_Coverage(bool h1, bool h2, bool h3, bool h4)
    {
        // Arrange
        var sheet = new JobErrorSheet
        {
            JobErrorSheetType = h1 ? JobSheetsType.Standard : null,
            JobErrorSheetWhen = h2 ? JobErrorSheetWhen.OnError : null,
            Media = h3 ? (Media)"iso_a4_210x297mm" : null,
            MediaCol = h4 ? new MediaCol { MediaColor = (MediaColor)"blue" } : null
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(sheet).ToList();

        // Assert
        if (h1) result.Should().Contain(a => a.Name == "job-error-sheet-type"); else result.Should().NotContain(a => a.Name == "job-error-sheet-type");
        if (h2) result.Should().Contain(a => a.Name == "job-error-sheet-when"); else result.Should().NotContain(a => a.Name == "job-error-sheet-when");
        if (h3) result.Should().Contain(a => a.Name == "media"); else result.Should().NotContain(a => a.Name == "media");
        if (h4) result.Should().Contain(a => a.Name == "media-col"); else result.Should().NotContain(a => a.Name == "media-col");
    }
}
