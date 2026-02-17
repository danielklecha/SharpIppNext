using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Models;

[TestClass]
public class OperationAttributesTests
{
    [TestMethod]
    public void GetIppAttributes_ShouldThrowArgumentNullException_WhenAttributesCharsetIsNull()
    {
        // Arrange
        var operationAttributes = new OperationAttributes
        {
            AttributesCharset = null,
            AttributesNaturalLanguage = "en-us",
            PrinterUri = new Uri("ipp://printer")
        };
        var mapperMock = new Mock<IMapperApplier>();

        // Act
        Action act = () => operationAttributes.GetIppAttributes(mapperMock.Object).ToList();

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(nameof(OperationAttributes.AttributesCharset));
    }

    [TestMethod]
    public void GetIppAttributes_ShouldThrowArgumentNullException_WhenAttributesNaturalLanguageIsNull()
    {
        // Arrange
        var operationAttributes = new OperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = null,
            PrinterUri = new Uri("ipp://printer")
        };
        var mapperMock = new Mock<IMapperApplier>();

        // Act
        Action act = () => operationAttributes.GetIppAttributes(mapperMock.Object).ToList();

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName(nameof(OperationAttributes.AttributesNaturalLanguage));
    }

    [TestMethod]
    public void GetIppAttributes_ShouldReturnAttributes_WhenAllPropertiesAreSet()
    {
        // Arrange
        var operationAttributes = new OperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en-us",
            PrinterUri = new Uri("ipp://printer"),
            RequestingUserName = "user"
        };
        var mapperMock = new Mock<IMapperApplier>();

        // Act
        var attributes = operationAttributes.GetIppAttributes(mapperMock.Object).ToList();

        // Assert
        attributes.Should().Contain(x => x.Name == JobAttribute.AttributesCharset && (string)x.Value == "utf-8");
        attributes.Should().Contain(x => x.Name == JobAttribute.AttributesNaturalLanguage && (string)x.Value == "en-us");
        attributes.Should().Contain(x => x.Name == JobAttribute.PrinterUri && (string)x.Value == "ipp://printer/");
        attributes.Should().Contain(x => x.Name == JobAttribute.RequestingUserName && (string)x.Value == "user");
    }
}
