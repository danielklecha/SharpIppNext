using Moq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;

namespace SharpIppTests.Exceptions;

[TestClass]
[ExcludeFromCodeCoverage]
public class IppRequestExceptionTests
{
    [TestMethod]
    public void Constructor_WithRequestMessage_ShouldSetProperties()
    {
        // Arrange
        var requestMessage = Mock.Of<IIppRequestMessage>();

        // Act
        var exception = new IppRequestException( requestMessage );

        // Assert
        exception.RequestMessage.Should().Be( requestMessage );
        exception.Message.Should().Be( $"Exception of type '{typeof( IppRequestException )}' was thrown." );
    }

    [TestMethod]
    public void Constructor_WithMessageAndRequestMessageAndStatusCode_ShouldSetProperties()
    {
        // Arrange
        var message = "Test message";
        var requestMessage = Mock.Of<IIppRequestMessage>();
        var statusCode = IppStatusCode.ClientErrorBadRequest;

        // Act
        var exception = new IppRequestException( message, requestMessage, statusCode );

        // Assert
        exception.Message.Should().Be( message );
        exception.RequestMessage.Should().Be( requestMessage );
        exception.StatusCode.Should().Be( statusCode );
    }

    [TestMethod]
    public void Constructor_WithMessageAndInnerExceptionAndRequestMessageAndStatusCode_ShouldSetProperties()
    {
        // Arrange
        var message = "Test message";
        var innerException = new Exception( "Inner exception" );
        var requestMessage = Mock.Of<IIppRequestMessage>();
        var statusCode = IppStatusCode.ServerErrorInternalError;

        // Act
        var exception = new IppRequestException( message, innerException, requestMessage, statusCode );

        // Assert
        exception.Message.Should().Be( message );
        exception.InnerException.Should().Be( innerException );
        exception.RequestMessage.Should().Be( requestMessage );
        exception.StatusCode.Should().Be( statusCode );
    }

    [TestMethod]
    public void ToString_ShouldIncludeRequestMessage()
    {
        // Arrange
        var requestMessage = Mock.Of<IIppRequestMessage>();
        var exception = new IppRequestException( requestMessage );

        // Act
        var result = exception.ToString();

        // Assert
        result.Should().Contain( nameof( IppRequestException.RequestMessage ) );
        result.Should().Contain( requestMessage.ToString() );
    }


}
