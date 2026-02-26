using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Models.Responses;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Models.Responses;

[TestClass]
[ExcludeFromCodeCoverage]
public class OperationAttributesTests
{
    [TestMethod]
    public void AttributesCharset_Set_ShouldUpdateValue()
    {
        // Arrange
        var operationAttributes = new OperationAttributes();
        var expectedValue = "utf-16";

        // Act
        operationAttributes.AttributesCharset = expectedValue;

        // Assert
        operationAttributes.AttributesCharset.Should().Be(expectedValue);
    }

    [TestMethod]
    public void AttributesNaturalLanguage_Set_ShouldUpdateValue()
    {
        // Arrange
        var operationAttributes = new OperationAttributes();
        var expectedValue = "pl-PL";

        // Act
        operationAttributes.AttributesNaturalLanguage = expectedValue;

        // Assert
        operationAttributes.AttributesNaturalLanguage.Should().Be(expectedValue);
    }
}
