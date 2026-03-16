using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class CollectionProfilesTests
{
    private readonly IMapper _mapper;

    public CollectionProfilesTests()
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

    [TestMethod]
    public void Map_Dictionary_To_JobCounter_Coverage()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "blank", [new IppAttribute(Tag.Integer, "blank", 1)] },
            { "full-color", [new IppAttribute(Tag.Integer, "full-color", 2)] },
            { "monochrome-two-sided", [new IppAttribute(Tag.Integer, "monochrome-two-sided", 3)] },
        };

        // Act
        var result = _mapper.Map<JobCounter>(dict);

        // Assert
        result.Blank.Should().Be(1);
        result.FullColor.Should().Be(2);
        result.MonochromeTwoSided.Should().Be(3);
        result.HighlightColor.Should().BeNull();
    }

    [TestMethod]
    public void Map_JobCounter_To_Attributes_Coverage()
    {
        // Arrange
        var counter = new JobCounter
        {
            Blank = 1,
            FullColor = 2,
            MonochromeTwoSided = 3
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(counter).ToList();

        // Assert
        result.Should().Contain(a => a.Name == "blank");
        result.Should().Contain(a => a.Name == "full-color");
        result.Should().Contain(a => a.Name == "monochrome-two-sided");
        result.Should().NotContain(a => a.Name == "highlight-color");
    }

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
