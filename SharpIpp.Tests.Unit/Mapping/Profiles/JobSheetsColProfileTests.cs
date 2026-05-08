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
public class JobSheetsColProfileTests : MapperTestBase
{

[TestMethod]
    public void Map_Dictionary_To_JobSheetsCol_Coverage()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "job-sheets", [new IppAttribute(Tag.Keyword, "job-sheets", "standard")] },
            { "media", [new IppAttribute(Tag.Keyword, "media", "iso_a4_210x297mm")] },
            { "media-col", [
                new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance),
                new IppAttribute(Tag.MemberAttrName, "", "media-color"),
                new IppAttribute(Tag.Keyword, "", "blue"),
                new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
            ] },
        };

        // Act
        var result = _mapper.Map<JobSheetsCol>(dict);

        // Assert
        result.JobSheets.Should().Be(JobSheets.Standard);
        result.Media.Should().Be((Media)"iso_a4_210x297mm");
        result.MediaCol.Should().NotBeNull();
    }

[TestMethod]
    public void Map_JobSheetsCol_To_Attributes_Coverage()
    {
        // Arrange
        var sheets = new JobSheetsCol
        {
            JobSheets = JobSheets.Standard,
            Media = (Media)"iso_a4_210x297mm",
            MediaCol = new MediaCol { MediaColor = (MediaColor)"blue" }
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(sheets).ToList();

        // Assert
        result.Should().Contain(a => a.Name == "job-sheets");
        result.Should().Contain(a => a.Name == "media");
        result.Should().Contain(a => a.Name == "media-col");
    }
}
