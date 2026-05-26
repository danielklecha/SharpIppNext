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
public class PrinterAlertProfileTests : MapperTestBase
{

    [TestMethod]
    public void Map_PrinterAlert_ToByteArray_MapsCorrectly()
    {
        // Arrange
        var alert = new PrinterAlert { Code = "jam", Severity = "critical" };
        var expectedString = "code=jam;severity=critical";
        var expectedBytes = Encoding.UTF8.GetBytes(expectedString);

        // Act
        var result = _mapper.Map<PrinterAlert, byte[]>(alert);

        // Assert
        result.Should().BeEquivalentTo(expectedBytes);
    }

    [TestMethod]
    public void Map_ByteArray_ToPrinterAlert_MapsCorrectly()
    {
        // Arrange
        var alertString = "code=jam;severity=critical";
        var bytes = Encoding.UTF8.GetBytes(alertString);

        // Act
        var result = _mapper.Map<byte[], PrinterAlert>(bytes);

        // Assert
        result.Should().NotBeNull();
        result.Code.Should().Be("jam");
        result.Severity.Should().Be("critical");
    }

    [TestMethod]
    public void Map_PrinterAlert_ToOctetString_MapsCorrectly()
    {
        // Arrange
        var alert = new PrinterAlert { Code = "jam", Severity = "critical" };
        var expectedString = "code=jam;severity=critical";
        var expectedBytes = Encoding.UTF8.GetBytes(expectedString);

        // Act
        var result = _mapper.Map<PrinterAlert, OctetString>(alert);

        // Assert
        result.Should().NotBeNull();
        result.Value.Should().BeEquivalentTo(expectedBytes);
    }

    [TestMethod]
    public void Map_OctetString_ToPrinterAlert_MapsCorrectly()
    {
        // Arrange
        var alertString = "code=jam;severity=critical";
        var bytes = Encoding.UTF8.GetBytes(alertString);
        var octetString = new OctetString(bytes);

        // Act
        var result = _mapper.Map<OctetString, PrinterAlert>(octetString);

        // Assert
        result.Should().NotBeNull();
        result.Code.Should().Be("jam");
        result.Severity.Should().Be("critical");
    }
}
