using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class OutputBinTests
{
    [TestMethod]
    public void Constructor_StoresKeywordFlag()
    {
        var keywordBin = new OutputBin("vendor-bin-42", true);
        var nameBin = new OutputBin("Accounting Team", false);

        keywordBin.IsMarked.Should().BeTrue();
        nameBin.IsMarked.Should().BeFalse();
    }

    [TestMethod]
    public void FactoryMethods_CreateExpectedValues()
    {
        OutputBin.Stacker(2).Value.Should().Be("stacker-2");
        OutputBin.Stacker(2).IsMarked.Should().BeTrue();
        OutputBin.Mailbox(3).Value.Should().Be("mailbox-3");
        OutputBin.Mailbox(3).IsMarked.Should().BeTrue();
        OutputBin.Tray(4).Value.Should().Be("tray-4");
        OutputBin.Tray(4).IsMarked.Should().BeTrue();
    }

    [TestMethod]
    public void ExplicitCast_CreatesKeywordValue()
    {
        var outputBin = (OutputBin)"face-down";

        outputBin.Value.Should().Be("face-down");
        outputBin.IsMarked.Should().BeTrue();
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
