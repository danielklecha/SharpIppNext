using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrinterFinisherSupplyTests
{
    [TestMethod]
    public void Parse_WithKnownAndExtensionElements_ShouldParse()
    {
        var raw = "class=consumed; type=staples; unit=items; max=5000; level=2500; color=silver; index=1; deviceindex=1; vendor=x;";

        var parsed = PrinterFinisherSupply.Parse(raw);

        parsed.Should().NotBeNull();
        parsed.Class.Should().Be(FinisherSupplyClass.Consumed);
        parsed.Type.Should().Be(FinisherSupplyType.Staples);
        parsed.Unit.Should().Be(CapacityUnit.Items);
        parsed.Max.Should().Be(5000);
        parsed.Level.Should().Be(2500);
        parsed.Color.Should().Be("silver");
        parsed.Index.Should().Be(1);
        parsed.DeviceIndex.Should().Be(1);
        parsed.Extensions.Should().ContainKey("vendor").WhoseValue.Should().Be("x");
    }

    [TestMethod]
    public void Parse_NullOrWhiteSpace_ShouldThrow()
    {
        Action act1 = () => PrinterFinisherSupply.Parse(null!);
        act1.Should().Throw<ArgumentNullException>();

        Action act2 = () => PrinterFinisherSupply.Parse("");
        act2.Should().Throw<FormatException>();

        Action act3 = () => PrinterFinisherSupply.Parse("   ");
        act3.Should().Throw<FormatException>();
    }

    [TestMethod]
    public void TryParse_ValidInput_ShouldReturnTrueAndPopulateResult()
    {
        var raw = "class=consumed; type=staples;";

        var success = PrinterFinisherSupply.TryParse(raw, out var parsed);

        success.Should().BeTrue();
        parsed.Should().NotBeNull();
        parsed!.Class.Should().Be(FinisherSupplyClass.Consumed);
        parsed.Type.Should().Be(FinisherSupplyType.Staples);
    }

    [TestMethod]
    public void TryParse_NullOrWhiteSpace_ShouldReturnFalse()
    {
        PrinterFinisherSupply.TryParse(null, out var r1).Should().BeFalse();
        r1.Should().BeNull();

        PrinterFinisherSupply.TryParse("", out var r2).Should().BeFalse();
        r2.Should().BeNull();

        PrinterFinisherSupply.TryParse("   ", out var r3).Should().BeFalse();
        r3.Should().BeNull();
    }

    [TestMethod]
    public void ToString_WithPopulatedModel_ShouldFollowDefinedOrderWithTrailingSemicolon()
    {
        var supply = new PrinterFinisherSupply
        {
            Class = FinisherSupplyClass.Consumed,
            Type = FinisherSupplyType.Staples,
            Unit = CapacityUnit.Items,
            Max = 5000,
            Level = 2500,
            Color = "silver",
            Index = 1,
            DeviceIndex = 1,
            Extensions = new Dictionary<string, string> { { "vendor", "x" } }
        };

        var raw = supply.ToString();

        raw.Should().Be("class=consumed; type=staples; unit=items; max=5000; level=2500; color=silver; index=1; deviceIndex=1; vendor=x;");
    }

    [TestMethod]
    public void ToString_EmptyModel_ShouldReturnEmptyString()
    {
        var supply = new PrinterFinisherSupply();
        supply.ToString().Should().Be(string.Empty);
    }

    [TestMethod]
    public void PrinterFinisherSupply_Properties_And_Extensions_ShouldSynchronizeWithDictionary()
    {
        var supply = new PrinterFinisherSupply
        {
            Class = (FinisherSupplyClass?)"supplies",
            Max = 500,
            Extensions = new Dictionary<string, string> { { "x-custom-supply", "value2" } }
        };

        // 1. Check properties are correct
        supply.Class.Should().Be((FinisherSupplyClass?)"supplies");
        supply.Max.Should().Be(500);
        supply.Extensions.Should().ContainKey("x-custom-supply").WhoseValue.Should().Be("value2");

        // 2. Change via properties
        supply.Class = (FinisherSupplyClass?)"consumable";
        supply.Extensions.Should().ContainKey("x-custom-supply").WhoseValue.Should().Be("value2");
        supply.Extensions.Should().NotContainKey("class");

        // 3. Clear extensions
        supply.Extensions = null;
        supply.Extensions.Should().BeNull();
        supply.Class.Should().Be((FinisherSupplyClass?)"consumable");
    }

    [TestMethod]
    public void Parse_MissingEquals_ShouldSkip()
    {
        var raw = "class=consumed;invalidSegment;vendor=x";

        var parsed = PrinterFinisherSupply.Parse(raw);

        parsed.Class.Should().Be(FinisherSupplyClass.Consumed);
        parsed.Extensions.Should().ContainKey("vendor").WhoseValue.Should().Be("x");
    }

    [TestMethod]
    public void Parse_WhitespaceSegments_ShouldBeSkipped()
    {
        var raw = "class=consumed;  ; ;vendor=x;";

        var parsed = PrinterFinisherSupply.Parse(raw);

        parsed.Class.Should().Be(FinisherSupplyClass.Consumed);
        parsed.Extensions.Should().ContainKey("vendor").WhoseValue.Should().Be("x");
    }

    [TestMethod]
    public void Parse_InvalidInts_ShouldStoreInDictionaryButReturnNullFromTypedProperty()
    {
        var raw = "class=consumed;max=abc;level=def;index=ghi;deviceindex=jkl";
        var parsed = PrinterFinisherSupply.Parse(raw);

        parsed.Max.Should().BeNull();
        parsed.Level.Should().BeNull();
        parsed.Index.Should().BeNull();
        parsed.DeviceIndex.Should().BeNull();

        parsed.Extensions.Should().BeNull();
        parsed.Dictionary.Should().ContainKey("max").WhoseValue.Should().Be("abc");
        parsed.Dictionary.Should().ContainKey("level").WhoseValue.Should().Be("def");
        parsed.Dictionary.Should().ContainKey("index").WhoseValue.Should().Be("ghi");
        parsed.Dictionary.Should().ContainKey("deviceindex").WhoseValue.Should().Be("jkl");
    }

    [TestMethod]
    public void Properties_GetAndSet_ShouldWorkCorrectly()
    {
        var supply = new PrinterFinisherSupply
        {
            Class = FinisherSupplyClass.Consumed,
            Type = FinisherSupplyType.Staples,
            Unit = CapacityUnit.Items,
            Max = 100,
            Level = 50,
            Color = "blue",
            Index = 2,
            DeviceIndex = 3
        };

        supply.Class.Should().Be(FinisherSupplyClass.Consumed);
        supply.Type.Should().Be(FinisherSupplyType.Staples);
        supply.Unit.Should().Be(CapacityUnit.Items);
        supply.Max.Should().Be(100);
        supply.Level.Should().Be(50);
        supply.Color.Should().Be("blue");
        supply.Index.Should().Be(2);
        supply.DeviceIndex.Should().Be(3);

        supply.Class = null;
        supply.Type = null;
        supply.Unit = null;
        supply.Max = null;
        supply.Level = null;
        supply.Color = null;
        supply.Index = null;
        supply.DeviceIndex = null;

        supply.Class.Should().BeNull();
        supply.Type.Should().BeNull();
        supply.Unit.Should().BeNull();
        supply.Max.Should().BeNull();
        supply.Level.Should().BeNull();
        supply.Color.Should().BeNull();
        supply.Index.Should().BeNull();
        supply.DeviceIndex.Should().BeNull();
    }
}

