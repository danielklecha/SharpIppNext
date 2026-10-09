using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Models.Responses;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppResponseTests
{
    private class TestResponse : IppResponse<OperationAttributes>
    {
    }

    private class TestCustomAttributesResponse : IppResponse<GetNextDocumentDataResponseOperationAttributes>
    {
    }

    [TestMethod]
    public void IIppResponse_OperationAttributes_Set_ShouldUpdateValue()
    {
        // Arrange
        var response = new TestResponse();
        IIppResponse ippResponse = response;
        var expected = new OperationAttributes();

        // Act
        ippResponse.OperationAttributes = expected;

        // Assert
        response.OperationAttributes.Should().BeSameAs(expected);
        ippResponse.OperationAttributes.Should().BeSameAs(expected);
    }

    [TestMethod]
    public void IIppResponse_OperationAttributes_SetNull_ShouldUpdateValueToNull()
    {
        // Arrange
        var response = new TestResponse
        {
            OperationAttributes = new OperationAttributes()
        };
        IIppResponse ippResponse = response;

        // Act
        ippResponse.OperationAttributes = null;

        // Assert
        response.OperationAttributes.Should().BeNull();
        ippResponse.OperationAttributes.Should().BeNull();
    }

    [TestMethod]
    public void IIppResponse_OperationAttributes_Set_WithCustomType_ShouldUpdateValue()
    {
        // Arrange
        var response = new TestCustomAttributesResponse();
        IIppResponse ippResponse = response;
        var expected = new GetNextDocumentDataResponseOperationAttributes();

        // Act
        ippResponse.OperationAttributes = expected;

        // Assert
        response.OperationAttributes.Should().BeSameAs(expected);
        ippResponse.OperationAttributes.Should().BeSameAs(expected);
    }

    [TestMethod]
    public void IIppResponse_OperationAttributes_Set_WithIncompatibleType_ShouldThrowInvalidCastException()
    {
        // Arrange
        var response = new TestCustomAttributesResponse();
        IIppResponse ippResponse = response;
        var incompatible = new OperationAttributes();

        // Act
        Action act = () => ippResponse.OperationAttributes = incompatible;

        // Assert
        act.Should().Throw<InvalidCastException>();
    }

    [TestMethod]
    public void IIppResponse_OperationAttributes_Get_ShouldReturnValue()
    {
        // Arrange
        var expected = new OperationAttributes();
        var response = new TestResponse
        {
            OperationAttributes = expected
        };
        IIppResponse ippResponse = response;

        // Act
        var actual = ippResponse.OperationAttributes;

        // Assert
        actual.Should().BeSameAs(expected);
    }

    [TestMethod]
    public void Properties_SetAndGet_ShouldWorkAsExpected()
    {
        // Arrange
        var response = new TestResponse();
        var version = new IppVersion(2, 0);
        var statusCode = IppStatusCode.SuccessfulOk;
        var requestId = 42;
        var opAttrs = new OperationAttributes();

        // Act
        response.Version = version;
        response.StatusCode = statusCode;
        response.RequestId = requestId;
        response.OperationAttributes = opAttrs;

        // Assert
        response.Version.Should().Be(version);
        response.StatusCode.Should().Be(statusCode);
        response.RequestId.Should().Be(requestId);
        response.OperationAttributes.Should().BeSameAs(opAttrs);
    }

    [TestMethod]
    public void ToString_ShouldReturnFormattedString()
    {
        // Arrange
        var response = new TestResponse
        {
            Version = new IppVersion(2, 0),
            StatusCode = IppStatusCode.SuccessfulOk,
            RequestId = 42
        };

        // Act
        var result = response.ToString();

        // Assert
        result.Should().Be("Version: 2.0, StatusCode: SuccessfulOk, RequestId: 42");
    }
}
