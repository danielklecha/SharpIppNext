using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SharpIpp.Tests.Models;

[TestClass]
[ExcludeFromCodeCoverage]
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

    [TestMethod]
    public void Create_ShouldReturnAttributes_WhenAllPropertiesAreSet()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { JobAttribute.AttributesCharset, new[] { new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8") } },
            { JobAttribute.AttributesNaturalLanguage, new[] { new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en-us") } },
            { JobAttribute.PrinterUri, new[] { new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://printer") } },
            { JobAttribute.RequestingUserName, new[] { new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "user") } }
        };
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(x => x.Map<string?>((object)"utf-8")).Returns("utf-8");
        mapperMock.Setup(x => x.Map<string?>((object)"en-us")).Returns("en-us");
        mapperMock.Setup(x => x.Map<string?>((object)"ipp://printer")).Returns("ipp://printer");
        mapperMock.Setup(x => x.Map<string?>((object)"user")).Returns("user");

        // Act
        var attributes = OperationAttributes.Create<OperationAttributes>(dict, mapperMock.Object);

        // Assert
        attributes.AttributesCharset.Should().Be("utf-8");
        attributes.AttributesNaturalLanguage.Should().Be("en-us");
        attributes.PrinterUri.Should().Be(new Uri("ipp://printer"));
        attributes.RequestingUserName.Should().Be("user");
    }

    [TestMethod]
    public void Create_ShouldNotSetPrinterUri_WhenPrinterUriIsInvalid()
    {
        // Arrange
         // space in URI is invalid
        var invalidUri = "http://invalid uri";
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { JobAttribute.PrinterUri, new[] { new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, invalidUri) } }
        };
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(x => x.Map<string?>((object)invalidUri)).Returns(invalidUri);

        // Act
        var attributes = OperationAttributes.Create<OperationAttributes>(dict, mapperMock.Object);

        // Assert
        attributes.PrinterUri.Should().BeNull();
    }

    [TestMethod]
    public void Create_ShouldNotSetPrinterUri_WhenPrinterUriIsNull()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>();
        var mapperMock = new Mock<IMapperApplier>();
        // When key is missing, MapFromDic passes NoValue.Instance
        mapperMock.Setup(x => x.Map<string?>(NoValue.Instance)).Returns((string?)null);

        // Act
        var attributes = OperationAttributes.Create<OperationAttributes>(dict, mapperMock.Object);

        // Assert
        attributes.PrinterUri.Should().BeNull();
    }
}
