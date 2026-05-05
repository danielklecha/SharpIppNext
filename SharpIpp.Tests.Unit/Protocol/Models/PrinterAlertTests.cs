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
    public void TryParse_Relaxed_WithKnownAndExtensionElements_ShouldParse()
    {
        var raw = "code=jam;index=22;severity=critical;group=mediaPath;groupindex=4;location=6;vendor=x";

        var ok = PrinterAlert.TryParse(raw, out var parsed, PrinterAlertParseOptions.Relaxed);

        ok.Should().BeTrue();
        parsed.Should().NotBeNull();
        parsed!.Code.Should().Be("jam");
        parsed.Index.Should().Be(22);
        parsed.Severity.Should().Be("critical");
        parsed.Group.Should().Be("mediaPath");
        parsed.GroupIndex.Should().Be(4);
        parsed.Location.Should().Be(6);
        parsed.Extensions.Should().ContainKey("vendor").WhoseValue.Should().Be("x");
    }

    [TestMethod]
    public void TryParse_Strict_WithControlCharacter_ShouldFail()
    {
        var raw = "code=jam;severity=critical\u0001";

        var ok = PrinterAlert.TryParse(raw, out var _, PrinterAlertParseOptions.Strict);

        ok.Should().BeFalse();
    }

    [TestMethod]
    public void TryParse_Strict_WithUnknownElement_ShouldFail()
    {
        var raw = "code=jam;vendor=x";

        var ok = PrinterAlert.TryParse(raw, out var _, PrinterAlertParseOptions.Strict);

        ok.Should().BeFalse();
    }

    [TestMethod]
    public void TryParse_NullOrWhiteSpace_ShouldReturnFalse()
    {
        PrinterAlert.TryParse(null!, out _).Should().BeFalse();
        PrinterAlert.TryParse("", out _).Should().BeFalse();
        PrinterAlert.TryParse("   ", out _).Should().BeFalse();
    }

    [TestMethod]
    public void TryParse_Relaxed_MissingEqualsOrEmptyValue_ShouldSkip()
    {
        var raw = "code=jam;invalidSegment;=emptyKey;key=;";

        var ok = PrinterAlert.TryParse(raw, out var parsed, PrinterAlertParseOptions.Relaxed);

        ok.Should().BeTrue();
        parsed!.Code.Should().Be("jam");
        parsed.Extensions.Should().BeNull(); // No valid extensions added
    }

    [TestMethod]
    public void TryParse_Strict_MissingEqualsOrEmptyValue_ShouldFail()
    {
        PrinterAlert.TryParse("code=jam;invalidSegment", out _, PrinterAlertParseOptions.Strict).Should().BeFalse();
        PrinterAlert.TryParse("code=jam;=emptyKey", out _, PrinterAlertParseOptions.Strict).Should().BeFalse();
        PrinterAlert.TryParse("code=jam;key=", out _, PrinterAlertParseOptions.Strict).Should().BeFalse();
    }

    [TestMethod]
    public void TryParse_Strict_NonAsciiLetters_ShouldFail()
    {
        PrinterAlert.TryParse("code=jam1", out _, PrinterAlertParseOptions.Strict).Should().BeFalse();
        PrinterAlert.TryParse("code=jam;severity=crit1", out _, PrinterAlertParseOptions.Strict).Should().BeFalse();
        PrinterAlert.TryParse("code=jam;training=train1", out _, PrinterAlertParseOptions.Strict).Should().BeFalse();
        PrinterAlert.TryParse("code=jam;group=grp1", out _, PrinterAlertParseOptions.Strict).Should().BeFalse();
    }

    [TestMethod]
    public void TryParse_Relaxed_NonAsciiLetters_ShouldParse()
    {
        var raw = "code=jam1;severity=crit1;training=train1;group=grp1";
        PrinterAlert.TryParse(raw, out var parsed, PrinterAlertParseOptions.Relaxed).Should().BeTrue();
        parsed!.Code.Should().Be("jam1");
        parsed.Severity.Should().Be("crit1");
        parsed.Training.Should().Be("train1");
        parsed.Group.Should().Be("grp1");
    }

    [TestMethod]
    public void TryParse_Strict_InvalidInts_ShouldFail()
    {
        PrinterAlert.TryParse("code=jam;index=abc", out _, PrinterAlertParseOptions.Strict).Should().BeFalse();
        PrinterAlert.TryParse("code=jam;groupindex=abc", out _, PrinterAlertParseOptions.Strict).Should().BeFalse();
        PrinterAlert.TryParse("code=jam;location=abc", out _, PrinterAlertParseOptions.Strict).Should().BeFalse();
        PrinterAlert.TryParse("code=jam;time=abc", out _, PrinterAlertParseOptions.Strict).Should().BeFalse();
    }

    [TestMethod]
    public void TryParse_Relaxed_InvalidInts_ShouldMapToExtensions()
    {
        var raw = "code=jam;index=abc;groupindex=def;location=ghi;time=jkl";
        PrinterAlert.TryParse(raw, out var parsed, PrinterAlertParseOptions.Relaxed).Should().BeTrue();
        
        parsed!.Index.Should().BeNull();
        parsed.GroupIndex.Should().BeNull();
        parsed.Location.Should().BeNull();
        parsed.Time.Should().BeNull();
        
        parsed.Extensions.Should().NotBeNull();
        parsed.Extensions.Should().ContainKey("index").WhoseValue.Should().Be("abc");
        parsed.Extensions.Should().ContainKey("groupindex").WhoseValue.Should().Be("def");
        parsed.Extensions.Should().ContainKey("location").WhoseValue.Should().Be("ghi");
        parsed.Extensions.Should().ContainKey("time").WhoseValue.Should().Be("jkl");
    }

    [TestMethod]
    public void TryParse_RequireCodeFalse_MissingCode_ShouldParse()
    {
        var options = new PrinterAlertParseOptions { RequireCode = false };
        var raw = "severity=critical";
        PrinterAlert.TryParse(raw, out var parsed, options).Should().BeTrue();
        parsed!.Code.Should().BeNull();
        parsed.Severity.Should().Be("critical");
    }

    [TestMethod]
    public void TryParse_RequireCodeTrue_MissingCode_ShouldFail()
    {
        var options = new PrinterAlertParseOptions { RequireCode = true };
        var raw = "severity=critical";
        PrinterAlert.TryParse(raw, out _, options).Should().BeFalse();
    }

    [TestMethod]
    public void Parse_ValidString_ShouldReturnAlert()
    {
        var raw = "code=jam;severity=critical";
        var parsed = PrinterAlert.Parse(raw);
        parsed.Should().NotBeNull();
        parsed.Code.Should().Be("jam");
        parsed.Severity.Should().Be("critical");
    }

    [TestMethod]
    public void Parse_InvalidString_ShouldThrowFormatException()
    {
        var raw = "severity=critical"; // Missing code
        Action act = () => PrinterAlert.Parse(raw);
        act.Should().Throw<FormatException>().WithMessage($"Invalid printer-alert value: '{raw}'");
    }

    [TestMethod]
    public void Serialize_WithParsedModel_ShouldFollowAbnfOrder()
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

        var raw = PrinterAlert.Serialize(alert);

        raw.Should().Be("code=coverOpen;index=23;severity=critical;training=fieldService;group=cover;groupindex=6;location=8;time=42;vendor=x");
    }

    [TestMethod]
    public void Serialize_NullAlert_ShouldThrowArgumentNullException()
    {
        Action act = () => PrinterAlert.Serialize(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void Serialize_MissingCode_ShouldThrowFormatException()
    {
        var alert = new PrinterAlert { Severity = "critical" };
        Action act = () => PrinterAlert.Serialize(alert);
        act.Should().Throw<FormatException>().WithMessage("The 'code' element is required for printer-alert serialization.");
    }

    [TestMethod]
    public void TryParse_WithEmptySegment_ShouldSkip()
    {
        var raw = "code=jam; ;severity=critical";
        var ok = PrinterAlert.TryParse(raw, out var parsed, PrinterAlertParseOptions.Relaxed);
        ok.Should().BeTrue();
        parsed!.Code.Should().Be("jam");
        parsed.Severity.Should().Be("critical");
    }

    [TestMethod]
    public void TryParse_WithTime_ShouldParse()
    {
        var raw = "code=jam;time=123";
        var ok = PrinterAlert.TryParse(raw, out var parsed, PrinterAlertParseOptions.Relaxed);
        ok.Should().BeTrue();
        parsed!.Time.Should().Be(123);
    }

    [TestMethod]
    public void TryParse_WithOverflowInt_Strict_ShouldFail()
    {
        var raw = "code=jam;index=9999999999";
        var ok = PrinterAlert.TryParse(raw, out _, PrinterAlertParseOptions.Strict);
        ok.Should().BeFalse();
    }

    [TestMethod]
    public void TryParse_WithOverflowInt_Relaxed_ShouldMapToExtensions()
    {
        var raw = "code=jam;index=9999999999";
        var ok = PrinterAlert.TryParse(raw, out var parsed, PrinterAlertParseOptions.Relaxed);
        ok.Should().BeTrue();
        parsed!.Index.Should().BeNull();
        parsed.Extensions.Should().ContainKey("index").WhoseValue.Should().Be("9999999999");
    }

    [TestMethod]
    [DataRow("abc", true)]
    [DataRow("ABC", true)]
    [DataRow("AZaz", true)]
    [DataRow("", false)]
    [DataRow("123", false)]
    [DataRow("a1", false)]
    [DataRow("@", false)]
    [DataRow("[", false)]
    [DataRow("`", false)]
    [DataRow("{", false)]
    public void IsAsciiLetters_ShouldValidate(string value, bool expected)
    {
        PrinterAlert.IsAsciiLetters(value).Should().Be(expected);
    }
}