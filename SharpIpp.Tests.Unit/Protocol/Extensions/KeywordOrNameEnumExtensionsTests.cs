using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Protocol.Extensions;

[TestClass]
[ExcludeFromCodeCoverage]
public class KeywordOrNameEnumExtensionsTests
{
    [TestMethod]
    [DataRow("iso_a4_210x297mm", true, Tag.Keyword)]
    [DataRow("Accounting Team", false, Tag.NameWithoutLanguage)]
    public void ToIppTag_Media_ShouldReturnExpectedTag(string value, bool isKeyword, Tag expected)
    {
        var media = new Media(value, isKeyword);
        media.ToIppTag().Should().Be(expected);
    }

    [TestMethod]
    public void ToIppTag_MediaSingleArg_DefaultsToKeyword()
    {
        var media = new Media("iso_a4_210x297mm");
        media.ToIppTag().Should().Be(Tag.Keyword);
    }

    [TestMethod]
    [DataRow("vendor-bin-42", true, Tag.Keyword)]
    [DataRow("custom-finisher-bin", false, Tag.NameWithoutLanguage)]
    public void ToIppTag_OutputBin_ShouldReturnExpectedTag(string value, bool isKeyword, Tag expected)
    {
        var bin = new OutputBin(value, isKeyword);
        bin.ToIppTag().Should().Be(expected);
    }

    [TestMethod]
    public void ToIppTag_JobStorageAccess_ShouldReturnExpectedTag()
    {
        JobStorageAccess.Group.ToIppTag().Should().Be(Tag.Keyword);
        new JobStorageAccess("custom-group", false).ToIppTag().Should().Be(Tag.NameWithoutLanguage);
    }

    [TestMethod]
    public void ToIppTag_Null_ThrowsArgumentNullException()
    {
        IKeywordOrNameEnum? value = null;
        var act = () => value!.ToIppTag();
        act.Should().Throw<System.ArgumentNullException>();
    }
}
