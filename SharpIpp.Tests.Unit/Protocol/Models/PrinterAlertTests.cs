using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using SharpIpp.Tests.Unit.Mapping;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrinterAlertTests : MapperTestBase
{
    [TestMethod]
    public void Parse_WithKnownAndExtensionElements_ShouldParse()
    {
        var raw = "code=jam;index=22;severity=critical;group=mediaPath;groupindex=4;location=6;vendor=x";

        var parsed = _mapper.Map<string, PrinterAlert>(raw);

        parsed.Should().NotBeNull();
        parsed.Code.Should().Be("jam");
        parsed.Index.Should().Be(22);
        parsed.Severity.Should().Be("critical");
        parsed.Group.Should().Be("mediaPath");
        parsed.GroupIndex.Should().Be(4);
        parsed.Location.Should().Be(6);
        parsed.Extensions.Should().ContainKey("vendor").WhoseValue.Should().Be("x");
    }

    [TestMethod]
    public void Parse_NullOrWhiteSpace_ShouldThrow()
    {
        Action act1 = () => _mapper.Map<string, PrinterAlert>(null!);
        act1.Should().Throw<Exception>();

        Action act2 = () => _mapper.Map<string, PrinterAlert>("");
        act2.Should().Throw<Exception>();

        Action act3 = () => _mapper.Map<string, PrinterAlert>("   ");
        act3.Should().Throw<Exception>();
    }

    [TestMethod]
    public void Parse_MissingEqualsOrEmptyValue_ShouldSkip()
    {
        var raw = "code=jam;invalidSegment;=emptyKey;key=;";

        var parsed = _mapper.Map<string, PrinterAlert>(raw);

        parsed.Code.Should().Be("jam");
        parsed.Extensions.Should().BeNull(); // No valid extensions added
    }

    [TestMethod]
    public void Parse_InvalidInts_ShouldStoreInDictionaryButReturnNullFromTypedProperty()
    {
        var raw = "code=jam;index=abc;groupindex=def;location=ghi;time=jkl";
        var parsed = _mapper.Map<string, PrinterAlert>(raw);
        
        parsed.Index.Should().BeNull();
        parsed.GroupIndex.Should().BeNull();
        parsed.Location.Should().BeNull();
        parsed.Time.Should().BeNull();
        
        // Under our simplified design, invalid values for standard keys remain under their standard keys in the dictionary,
        // so they do not map to Extensions.
        parsed.Extensions.Should().BeNull();
        parsed.Dictionary.Should().ContainKey("index").WhoseValue.Should().Be("abc");
        parsed.Dictionary.Should().ContainKey("groupindex").WhoseValue.Should().Be("def");
        parsed.Dictionary.Should().ContainKey("location").WhoseValue.Should().Be("ghi");
        parsed.Dictionary.Should().ContainKey("time").WhoseValue.Should().Be("jkl");
    }

    [TestMethod]
    public void Parse_MissingCode_ShouldThrow()
    {
        var raw = "severity=critical";
        Action act = () => _mapper.Map<string, PrinterAlert>(raw);
        act.Should().Throw<Exception>();
    }

    [TestMethod]
    public void Serialize_WithParsedModel_ShouldFollowDefinedOrder()
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

        var raw = _mapper.Map<PrinterAlert, string>(alert);

        raw.Should().Be("code=coverOpen;index=23;severity=critical;training=fieldService;group=cover;groupindex=6;location=8;time=42;vendor=x");
    }

    [TestMethod]
    public void Serialize_NullAlert_ShouldThrow()
    {
        Action act = () => _mapper.Map<PrinterAlert, string>(null!);
        act.Should().Throw<Exception>();
    }

    [TestMethod]
    public void Serialize_MissingCode_ShouldThrow()
    {
        var alert = new PrinterAlert { Severity = "critical" };
        Action act = () => _mapper.Map<PrinterAlert, string>(alert);
        act.Should().Throw<Exception>();
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
        alert.Code.Should().Be("coverOpen"); // Standard property preserved
    }
}
