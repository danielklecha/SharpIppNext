using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrinterFinisherProfileTests : MapperTestBase
{
    [TestMethod]
    public void Map_PrinterFinisher_ToOctetString_MapsCorrectly()
    {
        // Arrange
        var finisher = new PrinterFinisher
        {
            Type = "stitcher",
            Unit = "items",
            MaxCapacity = 100,
            Index = 1,
            PresentOnOff = "on",
            Status = 3,
            Capacity = 50
        };
        // Expectation: Serialize then UTF8 encode
        // The Serialize method in PrinterFinisherProfile appends a semicolon at the end if the builder is not empty.
        var expectedString = "type=stitcher; unit=items; maxcapacity=100; index=1; presentonoff=on; status=3; capacity=50;";
        var expectedBytes = Encoding.UTF8.GetBytes(expectedString);

        // Act
        var result = _mapper.Map<PrinterFinisher, OctetString>(finisher);

        // Assert
        result.Value.Should().BeEquivalentTo(expectedBytes);
    }

    [TestMethod]
    public void Map_OctetString_ToPrinterFinisher_MapsCorrectly()
    {
        // Arrange
        var raw = "type=stitcher; unit=items; maxcapacity=100; index=1; presentonoff=on; status=3; capacity=50;";
        var octetString = new OctetString(Encoding.UTF8.GetBytes(raw));

        // Act
        var result = _mapper.Map<OctetString, PrinterFinisher>(octetString);

        // Assert
        result.Should().BeEquivalentTo(new PrinterFinisher
        {
            Type = "stitcher",
            Unit = "items",
            MaxCapacity = 100,
            Index = 1,
            PresentOnOff = "on",
            Status = 3,
            Capacity = 50
        });
    }

    [TestMethod]
    public void Map_PrinterFinisher_ToByteArray_MapsCorrectly()
    {
        // Arrange
        var finisher = new PrinterFinisher { Type = "test", Unit = "unit" };
        var expectedString = "type=test; unit=unit;";
        var expectedBytes = Encoding.UTF8.GetBytes(expectedString);

        // Act
        var result = _mapper.Map<PrinterFinisher, byte[]>(finisher);

        // Assert
        result.Should().BeEquivalentTo(expectedBytes);
    }

    [TestMethod]
    public void Map_ByteArray_ToPrinterFinisher_MapsCorrectly()
    {
        // Arrange
        var raw = "type=stitcher; unit=items;";
        var bytes = Encoding.UTF8.GetBytes(raw);

        // Act
        var result = _mapper.Map<byte[], PrinterFinisher>(bytes);

        // Assert
        result.Should().BeEquivalentTo(new PrinterFinisher
        {
            Type = "stitcher",
            Unit = "items"
        });
    }

    [TestMethod]
    public void Map_PrinterFinisher_WithExtensions_ToOctetString_MapsCorrectly()
    {
        // Arrange
        var finisher = new PrinterFinisher
        {
            Type = "stitcher",
            Extensions = new Dictionary<string, string> { { "custom-key", "custom-value" } }
        };
        var expectedString = "type=stitcher; custom-key=custom-value;";
        var expectedBytes = Encoding.UTF8.GetBytes(expectedString);

        // Act
        var result = _mapper.Map<PrinterFinisher, OctetString>(finisher);

        // Assert
        result.Value.Should().BeEquivalentTo(expectedBytes);
    }

    [TestMethod]
    public void Map_OctetString_WithExtensions_ToPrinterFinisher_MapsCorrectly()
    {
        // Arrange
        var raw = "type=stitcher; custom-key=custom-value;";
        var octetString = new OctetString(Encoding.UTF8.GetBytes(raw));

        // Act
        var result = _mapper.Map<OctetString, PrinterFinisher>(octetString);

        // Assert
        result.Extensions.Should().ContainKey("custom-key").WhoseValue.Should().Be("custom-value");
    }

    [TestMethod]
    public void Map_PrinterFinisher_WithNullType_ToOctetString_OmitsTypeField()
    {
        // Arrange – Type is null, so append("type", null) should be a no-op
        var finisher = new PrinterFinisher { Type = null, Unit = "items" };
        var expectedString = "unit=items;";
        var expectedBytes = Encoding.UTF8.GetBytes(expectedString);

        // Act
        var result = _mapper.Map<PrinterFinisher, OctetString>(finisher);

        // Assert
        result.Value.Should().BeEquivalentTo(expectedBytes);
    }
}
