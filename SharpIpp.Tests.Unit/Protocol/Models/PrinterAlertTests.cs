using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrinterAlertTests
{
    [TestMethod]
    public void Parse_WithKnownAndExtensionElements_ShouldParse()
    {
        var raw = "code=jam;index=22;severity=critical;training=fieldService;group=mediaPath;groupindex=4;location=6;time=42;vendor=x";

        var parsed = PrinterAlert.Parse(raw);

        parsed.Should().NotBeNull();
        parsed.Code.Should().Be("jam");
        parsed.Index.Should().Be(22);
        parsed.Severity.Should().Be("critical");
        parsed.Training.Should().Be("fieldService");
        parsed.Group.Should().Be("mediaPath");
        parsed.GroupIndex.Should().Be(4);
        parsed.Location.Should().Be(6);
        parsed.Time.Should().Be(42);
        parsed.Extensions.Should().ContainKey("vendor").WhoseValue.Should().Be("x");
    }

    [TestMethod]
    public void Parse_NullOrWhiteSpace_ShouldThrow()
    {
        Action act1 = () => PrinterAlert.Parse(null!);
        act1.Should().Throw<ArgumentNullException>();

        Action act2 = () => PrinterAlert.Parse("");
        act2.Should().Throw<FormatException>();

        Action act3 = () => PrinterAlert.Parse("   ");
        act3.Should().Throw<FormatException>();
    }

    [TestMethod]
    public void TryParse_ValidInput_ShouldReturnTrueAndPopulateResult()
    {
        var raw = "code=jam;severity=critical";

        var success = PrinterAlert.TryParse(raw, out var parsed);

        success.Should().BeTrue();
        parsed.Should().NotBeNull();
        parsed!.Code.Should().Be("jam");
        parsed.Severity.Should().Be("critical");
    }

    [TestMethod]
    public void TryParse_NullOrWhiteSpace_ShouldReturnFalse()
    {
        PrinterAlert.TryParse(null, out var r1).Should().BeFalse();
        r1.Should().BeNull();

        PrinterAlert.TryParse("", out var r2).Should().BeFalse();
        r2.Should().BeNull();

        PrinterAlert.TryParse("   ", out var r3).Should().BeFalse();
        r3.Should().BeNull();
    }

    [TestMethod]
    public void Parse_MissingEqualsOrEmptyValue_ShouldSkip()
    {
        var raw = "code=jam;invalidSegment;=emptyKey;key=;";

        var parsed = PrinterAlert.Parse(raw);

        parsed.Code.Should().Be("jam");
        parsed.Extensions.Should().BeNull();
    }

    [TestMethod]
    public void Parse_WhitespaceAndEmptyValues_ShouldBeSkipped()
    {
        var raw = "code=jam;  ;  =val;key=  ; ;";

        var parsed = PrinterAlert.Parse(raw);

        parsed.Code.Should().Be("jam");
        parsed.Extensions.Should().BeNull();
    }

    [TestMethod]
    public void Parse_InvalidInts_ShouldStoreInDictionaryButReturnNullFromTypedProperty()
    {
        var raw = "code=jam;index=abc;groupindex=def;location=ghi;time=jkl";
        var parsed = PrinterAlert.Parse(raw);
        
        parsed.Index.Should().BeNull();
        parsed.GroupIndex.Should().BeNull();
        parsed.Location.Should().BeNull();
        parsed.Time.Should().BeNull();
        
        parsed.Extensions.Should().BeNull();
        parsed.Dictionary.Should().ContainKey("index").WhoseValue.Should().Be("abc");
        parsed.Dictionary.Should().ContainKey("groupindex").WhoseValue.Should().Be("def");
        parsed.Dictionary.Should().ContainKey("location").WhoseValue.Should().Be("ghi");
        parsed.Dictionary.Should().ContainKey("time").WhoseValue.Should().Be("jkl");
    }

    [TestMethod]
    public void Parse_MissingCode_ShouldNotThrow()
    {
        var raw = "severity=critical";
        var parsed = PrinterAlert.Parse(raw);
        parsed.Code.Should().BeNull();
        parsed.Severity.Should().Be("critical");
    }

    [TestMethod]
    public void Parse_RawCodeWithoutEquals_ShouldParseAsCode()
    {
        var raw = "other";
        var parsed = PrinterAlert.Parse(raw);
        parsed.Code.Should().Be("other");
    }

    [TestMethod]
    public void Parse_RawCodeWithoutEqualsAndAdditionalElements_ShouldParseCorrectly()
    {
        var raw = "other;severity=critical";
        var parsed = PrinterAlert.Parse(raw);
        parsed.Code.Should().Be("other");
        parsed.Severity.Should().Be("critical");
    }

    [TestMethod]
    public void ToString_WithPopulatedModel_ShouldFollowDefinedOrder()
    {
        var alert = new PrinterAlert
        {
            Code = "coverOpen",
            Index = 23,
            Severity = "critical",
            Training = "fieldService",
            Group = "cover",
            GroupIndex = 6,
            Location = 8,
            Time = 42,
            Extensions = new Dictionary<string, string> { { "vendor", "x" } }
        };

        var raw = alert.ToString();

        raw.Should().Be("code=coverOpen;index=23;severity=critical;training=fieldService;group=cover;groupindex=6;location=8;time=42;vendor=x");
    }

    [TestMethod]
    public void ToString_MissingCode_ShouldNotThrow()
    {
        var alert = new PrinterAlert { Severity = "critical" };
        var raw = alert.ToString();
        raw.Should().Be("severity=critical");
    }

    [TestMethod]
    public void ToString_EmptyModel_ShouldReturnEmptyString()
    {
        var alert = new PrinterAlert();
        alert.ToString().Should().Be(string.Empty);
    }

    [TestMethod]
    public void Properties_And_Extensions_ShouldSynchronizeWithDictionary()
    {
        var alert = new PrinterAlert
        {
            Code = "jam",
            Index = 42,
            Severity = "critical",
            Extensions = new Dictionary<string, string> { { "x-custom", "hello" } }
        };

        // 1. Check properties are correct
        alert.Code.Should().Be("jam");
        alert.Index.Should().Be(42);
        alert.Severity.Should().Be("critical");
        alert.Extensions.Should().ContainKey("x-custom").WhoseValue.Should().Be("hello");

        // 2. Change via properties, check Extensions is unchanged but dictionary is updated
        alert.Code = "coverOpen";
        alert.Extensions.Should().ContainKey("x-custom").WhoseValue.Should().Be("hello");
        alert.Extensions.Should().NotContainKey("code");

        // 3. Clear extensions
        alert.Extensions = null;
        alert.Extensions.Should().BeNull();
        alert.Code.Should().Be("coverOpen");
    }
}
