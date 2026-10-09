using FluentAssertions;
using Moq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace SharpIpp.Tests.Unit.Exceptions;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppResponseExceptionTests
{
    [TestMethod]
    public void Constructor_WithResponseMessage_ShouldSetProperties()
    {
        // Arrange
        var responseMessage = Mock.Of<IIppResponseMessage>();

        // Act
        var exception = new IppResponseException( responseMessage );

        // Assert
        exception.ResponseMessage.Should().Be( responseMessage );
        exception.Message.Should().Be( $"Exception of type '{typeof( IppResponseException )}' was thrown." );
    }

    [TestMethod]
    public void Constructor_WithMessageAndResponseMessage_ShouldSetProperties()
    {
        // Arrange
        var message = "Test message";
        var responseMessage = Mock.Of<IIppResponseMessage>();

        // Act
        var exception = new IppResponseException( message, responseMessage );

        // Assert
        exception.Message.Should().Be( message );
        exception.ResponseMessage.Should().Be( responseMessage );
    }

    [TestMethod]
    public void Constructor_WithMessageAndInnerExceptionAndResponseMessage_ShouldSetProperties()
    {
        // Arrange
        var message = "Test message";
        var innerException = new Exception( "Inner exception" );
        var responseMessage = Mock.Of<IIppResponseMessage>();

        // Act
        var exception = new IppResponseException( message, innerException, responseMessage );

        // Assert
        exception.Message.Should().Be( message );
        exception.InnerException.Should().Be( innerException );
        exception.ResponseMessage.Should().Be( responseMessage );
    }

    [TestMethod]
    public void Constructor_WithMessage_ShouldSetMessage()
    {
        // Arrange
        var message = "Test error";

        // Act
        var exception = new IppResponseException( message );

        // Assert
        exception.Message.Should().Be( message );
        exception.ResponseMessage.Should().BeNull();
        exception.InnerException.Should().BeNull();
    }

    [TestMethod]
    public void Constructor_WithMessageAndInnerException_ShouldSetProperties()
    {
        // Arrange
        var message = "Test error";
        var innerException = new InvalidOperationException( "Inner" );

        // Act
        var exception = new IppResponseException( message, innerException );

        // Assert
        exception.Message.Should().Be( message );
        exception.InnerException.Should().Be( innerException );
        exception.ResponseMessage.Should().BeNull();
    }

    [TestMethod]
    public void ToString_WhenResponseMessageIsNull_ShouldNotIncludeResponseMessage()
    {
        // Arrange
        var exception = new IppResponseException( "Test message" );

        // Act
        var result = exception.ToString();

        // Assert
        result.Should().NotContain( nameof( IppResponseException.ResponseMessage ) );
        result.Should().Contain( "Test message" );
    }

    [TestMethod]
    public void ToString_ShouldIncludeResponseMessage()
    {
        // Arrange
        var responseMessage = Mock.Of<IIppResponseMessage>();
        var exception = new IppResponseException( responseMessage );

        // Act
        var result = exception.ToString();

        // Assert
        result.Should().Contain( nameof( IppResponseException.ResponseMessage ) );
        result.Should().Contain( responseMessage.ToString() );
    }
}
