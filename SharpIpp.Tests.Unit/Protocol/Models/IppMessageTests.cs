using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppMessageTests
{
    [TestMethod]
    public void IppResponseMessage_ToString_ShouldReturnFormattedString()
    {
        // Arrange
        var response = new IppResponseMessage
        {
            Version = new IppVersion(1, 1),
            StatusCode = IppStatusCode.SuccessfulOk,
            RequestId = 123
        };

        // Act
        var result = response.ToString();

        // Assert
        result.Should().Be("Version: 1.1, StatusCode: SuccessfulOk, RequestId: 123");
    }

    [TestMethod]
    public void IppRequestMessage_ToString_ShouldReturnFormattedString()
    {
        // Arrange
        var request = new IppRequestMessage
        {
            Version = new IppVersion(2, 0),
            IppOperation = IppOperation.GetPrinterAttributes,
            RequestId = 456
        };

        // Act
        var result = request.ToString();

        // Assert
        result.Should().Be("Version: 2.0, Operation: GetPrinterAttributes, RequestId: 456");
    }
}
