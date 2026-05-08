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
        dst.OperationAttributes.AttributesCharset.Should().Be("utf-8");
        dst.OperationAttributes.AttributesNaturalLanguage.Should().Be("en");
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
                    new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-16"),
                    new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "fr"),
                    new IppAttribute(Tag.Keyword, JobAttribute.Compression, "gzip"),
                    new IppAttribute(Tag.Integer, JobAttribute.DocumentDataGetInterval, 10),
                    new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, true),
                    new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, 5)
                }
            }
        };

        // Act
        var dst = _mapper.Map<GetNextDocumentDataResponse>(src);

        // Assert
        dst.OperationAttributes.Should().NotBeNull();
        dst.OperationAttributes.AttributesCharset.Should().Be("utf-16");
        dst.OperationAttributes.AttributesNaturalLanguage.Should().Be("fr");
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
                AttributesCharset = "utf-16",
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
        attrs.Any(a => a.Name == JobAttribute.AttributesCharset && (string)a.Value == "utf-16").Should().BeTrue();
        attrs.Any(a => a.Name == JobAttribute.AttributesNaturalLanguage && (string)a.Value == "fr").Should().BeTrue();
        attrs.Any(a => a.Name == JobAttribute.Compression && (string)a.Value == "gzip").Should().BeTrue();
        attrs.Any(a => a.Name == JobAttribute.DocumentDataGetInterval && (int)a.Value == 10).Should().BeTrue();
        attrs.Any(a => a.Name == JobAttribute.LastDocument && (bool)a.Value == true).Should().BeTrue();
        attrs.Any(a => a.Name == DocumentAttribute.DocumentNumber && (int)a.Value == 5).Should().BeTrue();
    }
}
