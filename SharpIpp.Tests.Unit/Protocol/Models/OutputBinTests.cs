using SharpIpp.Protocol.Models;
using SharpIpp.Protocol.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class OutputBinTests
{
    [TestMethod]
    [DataRow("top", true)]
    [DataRow("stacker-1", true)]
    [DataRow("stacker-0", true)]
    [DataRow("tray-10", true)]
    [DataRow("custom-bin", false)]
    [DataRow("", false)]
    [DataRow(null, false)]
    public void GetKeywordOrNameTag_WithDataRows_ReturnsExpectedResult(string? value, bool expected)
    {
        IsOutputBinKeyword(value).Should().Be(expected);
    }

    [TestMethod]
    [DataRow("top")]
    [DataRow("middle")]
    [DataRow("bottom")]
    [DataRow("side")]
    [DataRow("left")]
    [DataRow("right")]
    [DataRow("center")]
    [DataRow("rear")]
    [DataRow("face-up")]
    [DataRow("face-down")]
    [DataRow("large-capacity")]
    [DataRow("my-mailbox")]
    [DataRow("auto")]
    [DataRow("stacker-1")]
    [DataRow("stacker-0")]
    [DataRow("mailbox-5")]
    [DataRow("tray-10")]
    public void GetKeywordOrNameTag_WithKeywordValues_ReturnsTrue(string value)
    {
        IsOutputBinKeyword(value).Should().BeTrue();
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("mailroom-bin")]
    [DataRow("mailbox-a")]
    [DataRow("tray--1")]
    public void GetKeywordOrNameTag_WithNonKeywordValues_ReturnsFalse(string? value)
    {
        IsOutputBinKeyword(value).Should().BeFalse();
    }

    [TestMethod]
    public void GetKeywordOrNameTag_WithNamedBin_ReturnsNameWithoutLanguage()
    {
        var bin = new OutputBin("Accounting Team").Value;

        bin.GetKeywordOrNameTag(OutputBin.KeywordRegex).Should().Be(Tag.NameWithoutLanguage);
    }

    private static bool IsOutputBinKeyword(string? value)
    {
        return !string.IsNullOrWhiteSpace(value) && value.GetKeywordOrNameTag(OutputBin.KeywordRegex) == Tag.Keyword;
    }

    [TestMethod]
    public void FactoryMethods_CreateExpectedValues()
    {
        OutputBin.Stacker(2).Value.Should().Be("stacker-2");
        OutputBin.Mailbox(3).Value.Should().Be("mailbox-3");
        OutputBin.Tray(4).Value.Should().Be("tray-4");
    }

    [TestMethod]
    public void FactoryMethods_WithInvalidValue_Throws()
    {
        Action stacker = () => OutputBin.Stacker(0);
        Action mailbox = () => OutputBin.Mailbox(-1);
        Action tray = () => OutputBin.Tray(0);

        stacker.Should().Throw<ArgumentOutOfRangeException>();
        mailbox.Should().Throw<ArgumentOutOfRangeException>();
        tray.Should().Throw<ArgumentOutOfRangeException>();
    }
}
