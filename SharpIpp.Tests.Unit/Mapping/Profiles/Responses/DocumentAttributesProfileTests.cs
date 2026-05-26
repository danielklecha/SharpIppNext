using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles.Responses;

[TestClass]
[ExcludeFromCodeCoverage]
[Obsolete]
public class DocumentAttributesProfileTests : MapperTestBase
{
    [TestMethod]
    public void Map_DictionaryToDocumentAttributes_SetsObsoleteProperties()
    {
        // Arrange
        var src = new[]
        {
            new IppAttribute(Tag.Keyword, IppAttributeNames.DocumentDigitalSignature, "xmldsig"),
            new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DocumentFormatVersion, "1.2"),
            new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DocumentFormatVersionDetected, "1.2.3")
        }.ToIppDictionary();

        // Act
        var dst = _mapper.Map<DocumentAttributes>(src);

        // Assert
        dst.DocumentDigitalSignature.Should().Be(DocumentDigitalSignature.XmlDsig);
        dst.DocumentFormatVersion.Should().Be("1.2");
        dst.DocumentFormatVersionDetected.Should().Be("1.2.3");
    }

    [TestMethod]
    public void Map_DocumentAttributesToDictionary_SetsObsoleteProperties()
    {
        // Arrange
        var src = new DocumentAttributes
        {
            DocumentDigitalSignature = DocumentDigitalSignature.XmlDsig,
            DocumentFormatVersion = "1.2",
            DocumentFormatVersionDetected = "1.2.3"
        };

        // Act
        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>>(src);

        // Assert
        dst.Should().ContainKey(IppAttributeNames.DocumentDigitalSignature);
        dst[IppAttributeNames.DocumentDigitalSignature].Should().HaveCount(1);
        dst[IppAttributeNames.DocumentDigitalSignature][0].Tag.Should().Be(Tag.Keyword);
        dst[IppAttributeNames.DocumentDigitalSignature][0].Value.Should().Be("xmldsig");

        dst.Should().ContainKey(IppAttributeNames.DocumentFormatVersion);
        dst[IppAttributeNames.DocumentFormatVersion].Should().HaveCount(1);
        dst[IppAttributeNames.DocumentFormatVersion][0].Tag.Should().Be(Tag.TextWithoutLanguage);
        dst[IppAttributeNames.DocumentFormatVersion][0].Value.Should().Be("1.2");

        dst.Should().ContainKey(IppAttributeNames.DocumentFormatVersionDetected);
        dst[IppAttributeNames.DocumentFormatVersionDetected].Should().HaveCount(1);
        dst[IppAttributeNames.DocumentFormatVersionDetected][0].Tag.Should().Be(Tag.TextWithoutLanguage);
        dst[IppAttributeNames.DocumentFormatVersionDetected][0].Value.Should().Be("1.2.3");
    }
}
