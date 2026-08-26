using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrinterFinisherTests
{
    [TestMethod]
    public void Parse_WithKnownAndExtensionElements_ShouldParse()
    {
        var raw = "type=stitcher; unit=items; maxcapacity=500; capacity=250; index=1; presentonoff=on; status=0; vendor=x;";

        var parsed = PrinterFinisher.Parse(raw);

        parsed.Should().NotBeNull();
        parsed.Type.Should().Be(FinisherType.Stitcher);
        parsed.Unit.Should().Be(CapacityUnit.Items);
        parsed.MaxCapacity.Should().Be(500);
        parsed.Capacity.Should().Be(250);
        parsed.Index.Should().Be(1);
        parsed.PresentOnOff.Should().Be(PresentOnOff.On);
        parsed.Status.Should().Be(0);
        parsed.Extensions.Should().ContainKey("vendor").WhoseValue.Should().Be("x");
    }

    [TestMethod]
    public void Parse_NullOrWhiteSpace_ShouldThrow()
    {
        Action act1 = () => PrinterFinisher.Parse(null!);
        act1.Should().Throw<ArgumentNullException>();

        Action act2 = () => PrinterFinisher.Parse("");
        act2.Should().Throw<FormatException>();

        Action act3 = () => PrinterFinisher.Parse("   ");
        act3.Should().Throw<FormatException>();
    }

    [TestMethod]
    public void TryParse_ValidInput_ShouldReturnTrueAndPopulateResult()
    {
        var raw = "type=folder; unit=items;";

        var success = PrinterFinisher.TryParse(raw, out var parsed);

        success.Should().BeTrue();
        parsed.Should().NotBeNull();
        parsed!.Type.Should().Be(FinisherType.Folder);
        parsed.Unit.Should().Be(CapacityUnit.Items);
    }

    [TestMethod]
    public void TryParse_NullOrWhiteSpace_ShouldReturnFalse()
    {
        PrinterFinisher.TryParse(null, out var r1).Should().BeFalse();
        r1.Should().BeNull();

        PrinterFinisher.TryParse("", out var r2).Should().BeFalse();
        r2.Should().BeNull();

        PrinterFinisher.TryParse("   ", out var r3).Should().BeFalse();
        r3.Should().BeNull();
    }

    [TestMethod]
    public void ToString_WithPopulatedModel_ShouldFollowDefinedOrderWithTrailingSemicolon()
    {
        var finisher = new PrinterFinisher
        {
            Type = FinisherType.Stitcher,
            Unit = CapacityUnit.Items,
            MaxCapacity = 500,
            Index = 1,
            PresentOnOff = PresentOnOff.On,
            Status = 0,
            Capacity = 250,
            Extensions = new Dictionary<string, string> { { "vendor", "x" } }
        };

        var raw = finisher.ToString();

        raw.Should().Be("type=stitcher; unit=items; maxcapacity=500; index=1; presentonoff=on; status=0; capacity=250; vendor=x;");
    }

    [TestMethod]
    public void ToString_EmptyModel_ShouldReturnEmptyString()
    {
        var finisher = new PrinterFinisher();
        finisher.ToString().Should().Be(string.Empty);
    }

    [TestMethod]
    public void PrinterFinisher_Properties_And_Extensions_ShouldSynchronizeWithDictionary()
    {
        var finisher = new PrinterFinisher
        {
            Type = "stitcher",
            MaxCapacity = 100,
            Extensions = new Dictionary<string, string> { { "x-custom", "value" } }
        };

        // 1. Check properties are correct
        finisher.Type.Should().Be(FinisherType.Stitcher);
        finisher.MaxCapacity.Should().Be(100);
        finisher.Extensions.Should().ContainKey("x-custom").WhoseValue.Should().Be("value");

        // 2. Change via properties
        finisher.Type = FinisherType.Folder;
        finisher.Extensions.Should().ContainKey("x-custom").WhoseValue.Should().Be("value");
        finisher.Extensions.Should().NotContainKey("type");

        // 3. Clear extensions
        finisher.Extensions = null;
        finisher.Extensions.Should().BeNull();
        finisher.Type.Should().Be(FinisherType.Folder);
    }
}
