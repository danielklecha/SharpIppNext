using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
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
}