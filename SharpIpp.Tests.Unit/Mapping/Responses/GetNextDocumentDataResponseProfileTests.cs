using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Models.Responses;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles.Responses;

[TestClass]
[ExcludeFromCodeCoverage]
public class GetNextDocumentDataResponseProfileTests : MapperTestBase
{

    [TestMethod]
    public void Map_IppResponseMessageToGetNextDocumentDataResponse_SetsDefaultOperationAttributesWhenAbsent()
    {
        // Arrange
        var src = new IppResponseMessage
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk,
            OperationAttributes = { new List<IppAttribute>() }
        };

        // Act
        var dst = _mapper.Map<GetNextDocumentDataResponse>(src);

        // Assert
        dst.OperationAttributes.Should().NotBeNull();
        dst.OperationAttributes.AttributesCharset.Should().Be((SharpIpp.Protocol.Models.Charset)"utf-8");
        ((string?)dst.OperationAttributes.AttributesNaturalLanguage).Should().Be("en");
    }

    [TestMethod]
    public void Map_IppResponseMessageToGetNextDocumentDataResponse_SetsOperationAttributesWhenPresent()
    {
        // Arrange
        var src = new IppResponseMessage
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk,
            OperationAttributes =
            {
                new List<IppAttribute>
                {
                    new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-16"),
                    new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "fr"),
                    new IppAttribute(Tag.Keyword, IppAttributeNames.Compression, "gzip"),
                    new IppAttribute(Tag.Integer, IppAttributeNames.DocumentDataGetInterval, 10),
                    new IppAttribute(Tag.Boolean, IppAttributeNames.LastDocument, true),
                    new IppAttribute(Tag.Integer, IppAttributeNames.DocumentNumber, 5)
                }
            }
        };

        // Act
        var dst = _mapper.Map<GetNextDocumentDataResponse>(src);

        // Assert
        dst.OperationAttributes.Should().NotBeNull();
        dst.OperationAttributes.AttributesCharset.Should().Be((SharpIpp.Protocol.Models.Charset)"utf-16");
        ((string?)dst.OperationAttributes.AttributesNaturalLanguage).Should().Be("fr");
        dst.OperationAttributes.Compression.Should().Be(Compression.Gzip);
        dst.OperationAttributes.DocumentDataGetInterval.Should().Be(10);
        dst.OperationAttributes.LastDocument.Should().Be(true);
        dst.OperationAttributes.DocumentNumber.Should().Be(5);
    }

    [TestMethod]
    public void Map_GetNextDocumentDataResponseToIppResponseMessage_SetsOperationAttributesWhenPresent()
    {
        // Arrange
        var src = new GetNextDocumentDataResponse
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk,
            OperationAttributes = new GetNextDocumentDataResponseOperationAttributes
            {
                AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-16",
                AttributesNaturalLanguage = "fr",
                Compression = Compression.Gzip,
                DocumentDataGetInterval = 10,
                LastDocument = true,
                DocumentNumber = 5
            }
        };

        // Act
        var dst = _mapper.Map<IppResponseMessage>(src);

        // Assert
        dst.OperationAttributes.Should().NotBeEmpty();
        var attrs = dst.OperationAttributes[0];
        attrs.Any(a => a.Name == IppAttributeNames.AttributesCharset && (string)a.Value == "utf-16").Should().BeTrue();
        attrs.Any(a => a.Name == IppAttributeNames.AttributesNaturalLanguage && (string)a.Value == "fr").Should().BeTrue();
        attrs.Any(a => a.Name == IppAttributeNames.Compression && (string)a.Value == "gzip").Should().BeTrue();
        attrs.Any(a => a.Name == IppAttributeNames.DocumentDataGetInterval && (int)a.Value == 10).Should().BeTrue();
        attrs.Any(a => a.Name == IppAttributeNames.LastDocument && (bool)a.Value == true).Should().BeTrue();
        attrs.Any(a => a.Name == IppAttributeNames.DocumentNumber && (int)a.Value == 5).Should().BeTrue();
    }
}
