using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Validation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Validation;

[TestClass]
[ExcludeFromCodeCoverage]
public class ValidationExceptionTests
{
    [TestMethod]
    public void Constructor_WithMessage_ShouldSetMessage()
    {
        // Arrange
        var message = "Validation failed";

        // Act
        var exception = new ValidationException(message);

        // Assert
        exception.Message.Should().Be(message);
        exception.InnerException.Should().BeNull();
    }

    [TestMethod]
    public void Constructor_WithMessageAndInnerException_ShouldSetMessageAndInnerException()
    {
        // Arrange
        var message = "Validation failed";
        var innerException = new InvalidOperationException("Inner error");

        // Act
        var exception = new ValidationException(message, innerException);

        // Assert
        exception.Message.Should().Be(message);
        exception.InnerException.Should().BeSameAs(innerException);
    }
}

