using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrinterFinisherTests
{
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
        finisher.Type.Should().Be("stitcher");
        finisher.MaxCapacity.Should().Be(100);
        finisher.Extensions.Should().ContainKey("x-custom").WhoseValue.Should().Be("value");

        // 2. Change via properties
        finisher.Type = "folder";
        finisher.Extensions.Should().ContainKey("x-custom").WhoseValue.Should().Be("value");
        finisher.Extensions.Should().NotContainKey("type");

        // 3. Clear extensions
        finisher.Extensions = null;
        finisher.Extensions.Should().BeNull();
        finisher.Type.Should().Be("folder");
    }

    [TestMethod]
    public void PrinterFinisherSupply_Properties_And_Extensions_ShouldSynchronizeWithDictionary()
    {
        var supply = new PrinterFinisherSupply
        {
            Class = "supplies",
            Max = 500,
            Extensions = new Dictionary<string, string> { { "x-custom-supply", "value2" } }
        };

        // 1. Check properties are correct
        supply.Class.Should().Be("supplies");
        supply.Max.Should().Be(500);
        supply.Extensions.Should().ContainKey("x-custom-supply").WhoseValue.Should().Be("value2");

        // 2. Change via properties
        supply.Class = "consumable";
        supply.Extensions.Should().ContainKey("x-custom-supply").WhoseValue.Should().Be("value2");
        supply.Extensions.Should().NotContainKey("class");

        // 3. Clear extensions
        supply.Extensions = null;
        supply.Extensions.Should().BeNull();
        supply.Class.Should().Be("consumable");
    }
}
