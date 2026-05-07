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
public class JobAccountingSheetsProfileTests
{
    private readonly IMapper _mapper;

    public JobAccountingSheetsProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

[TestMethod]
    [DataRow(true, true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, false, DisplayName = "None present")]
    public void Map_Dictionary_To_JobAccountingSheets_Coverage(bool h1, bool h2, bool h3, bool h4)
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>();
        if (h1) dict.Add("job-accounting-output-bin", new[] { new IppAttribute(Tag.Keyword, "job-accounting-output-bin", "top") });
        if (h2) dict.Add("job-accounting-sheets-type", new[] { new IppAttribute(Tag.Keyword, "job-accounting-sheets-type", "standard") });
        if (h3) dict.Add("media", new[] { new IppAttribute(Tag.Keyword, "media", "iso_a4_210x297mm") });
        if (h4) dict.Add("media-col", new[] { new IppAttribute(Tag.BegCollection, "media-col", NoValue.Instance), new IppAttribute(Tag.MemberAttrName, "", "media-color"), new IppAttribute(Tag.Keyword, "", "blue"), new IppAttribute(Tag.EndCollection, "", NoValue.Instance) });

        // Act
        var result = _mapper.Map<JobAccountingSheets>(dict);

        // Assert
        if (h1) result.JobAccountingOutputBin.Should().Be((OutputBin)"top"); else result.JobAccountingOutputBin.Should().BeNull();
        if (h2) result.JobAccountingSheetsType.Should().Be(JobSheetsType.Standard); else result.JobAccountingSheetsType.Should().BeNull();
        if (h3) result.Media.Should().Be((Media)"iso_a4_210x297mm"); else result.Media.Should().BeNull();
        if (h4) result.MediaCol.Should().NotBeNull(); else result.MediaCol.Should().BeNull();
    }

[TestMethod]
    [DataRow(true, true, true, true, DisplayName = "All present")]
    [DataRow(false, false, false, false, DisplayName = "None present")]
    public void Map_JobAccountingSheets_To_Attributes_Coverage(bool h1, bool h2, bool h3, bool h4)
    {
        // Arrange
        var sheets = new JobAccountingSheets
        {
            JobAccountingOutputBin = h1 ? (OutputBin)"top" : null,
            JobAccountingSheetsType = h2 ? JobSheetsType.Standard : null,
            Media = h3 ? (Media)"iso_a4_210x297mm" : null,
            MediaCol = h4 ? new MediaCol { MediaColor = (MediaColor)"blue" } : null
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(sheets).ToList();

        // Assert
        if (h1) result.Should().Contain(a => a.Name == "job-accounting-output-bin"); else result.Should().NotContain(a => a.Name == "job-accounting-output-bin");
        if (h2) result.Should().Contain(a => a.Name == "job-accounting-sheets-type"); else result.Should().NotContain(a => a.Name == "job-accounting-sheets-type");
        if (h3) result.Should().Contain(a => a.Name == "media"); else result.Should().NotContain(a => a.Name == "media");
        if (h4) result.Should().Contain(a => a.Name == "media-col"); else result.Should().NotContain(a => a.Name == "media-col");
    }
}
