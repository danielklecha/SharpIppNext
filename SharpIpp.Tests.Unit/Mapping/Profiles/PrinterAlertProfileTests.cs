using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrinterAlertProfileTests
{
    private readonly IMapper _mapper;

    public PrinterAlertProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_PrinterAlert_ToByteArray_MapsCorrectly()
    {
        // Arrange
        var alert = new PrinterAlert { Code = "jam", Severity = "critical" };
        var expectedString = PrinterAlert.Serialize(alert);
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
}
