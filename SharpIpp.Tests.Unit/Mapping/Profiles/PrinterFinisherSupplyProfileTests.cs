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
public class PrinterFinisherSupplyProfileTests : MapperTestBase
{
    [TestMethod]
    public void Map_PrinterFinisherSupply_ToOctetString_MapsCorrectly()
    {
        // Arrange
        var supply = new PrinterFinisherSupply
        {
            Class = "supplyThatIsConsumed",
            Type = "staples",
            Unit = "items",
            Max = 500,
            Level = 100,
            Color = "silver",
            Index = 8,
            DeviceIndex = 3
        };
        // Expectation: Serialize then UTF8 encode
        var expectedString = "class=supplyThatIsConsumed; type=staples; unit=items; max=500; level=100; color=silver; index=8; deviceIndex=3;";
        var expectedBytes = Encoding.UTF8.GetBytes(expectedString);

        // Act
        var result = _mapper.Map<PrinterFinisherSupply, OctetString>(supply);

        // Assert
        result.Value.Should().BeEquivalentTo(expectedBytes);
    }

    [TestMethod]
    public void Map_OctetString_ToPrinterFinisherSupply_MapsCorrectly()
    {
        // Arrange
        var raw = "class=supplyThatIsConsumed; type=staples; unit=items; max=500; level=100; color=silver; index=8; deviceIndex=3;";
        var octetString = new OctetString(Encoding.UTF8.GetBytes(raw));

        // Act
        var result = _mapper.Map<OctetString, PrinterFinisherSupply>(octetString);

        // Assert
        result.Should().BeEquivalentTo(new PrinterFinisherSupply
        {
            Class = "supplyThatIsConsumed",
            Type = "staples",
            Unit = "items",
            Max = 500,
            Level = 100,
            Color = "silver",
            Index = 8,
            DeviceIndex = 3
        });
    }

    [TestMethod]
    public void Map_PrinterFinisherSupply_ToByteArray_MapsCorrectly()
    {
        // Arrange
        var supply = new PrinterFinisherSupply { Class = "test", Type = "test" };
        var expectedString = "class=test; type=test;";
        var expectedBytes = Encoding.UTF8.GetBytes(expectedString);

        // Act
        var result = _mapper.Map<PrinterFinisherSupply, byte[]>(supply);

        // Assert
        result.Should().BeEquivalentTo(expectedBytes);
    }

    [TestMethod]
    public void Map_PrinterFinisherSupply_WithNullType_ToOctetString_OmitsTypeField()
    {
        // Arrange – Type is null, so append("type", null) should be a no-op
        var supply = new PrinterFinisherSupply { Class = "supplyThatIsConsumed", Type = null, Unit = "items" };
        var expectedString = "class=supplyThatIsConsumed; unit=items;";
        var expectedBytes = Encoding.UTF8.GetBytes(expectedString);

        // Act
        var result = _mapper.Map<PrinterFinisherSupply, OctetString>(supply);

        // Assert
        result.Value.Should().BeEquivalentTo(expectedBytes);
    }
}
