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

    [TestMethod]
    public void Map_DocumentAttributes_StringWithLanguage_AllStatesRoundTrip()
    {
        // 1. Without language
        var src1 = new DocumentAttributes { DocumentName = "Doc 1" };
        var dict1 = _mapper.Map<IDictionary<string, IppAttribute[]>>(src1);
        dict1[IppAttributeNames.DocumentName][0].Tag.Should().Be(Tag.NameWithoutLanguage);
        dict1[IppAttributeNames.DocumentName][0].Value.Should().Be("Doc 1");
        var back1 = _mapper.Map<DocumentAttributes>(dict1);
        back1.DocumentName.Should().Be("Doc 1");
        back1.DocumentName!.Value.HasLanguage.Should().BeFalse();

        // 2. With language
        var src2 = new DocumentAttributes { DocumentName = new StringWithLanguage("fr", "Doc 2") };
        var dict2 = _mapper.Map<IDictionary<string, IppAttribute[]>>(src2);
        dict2[IppAttributeNames.DocumentName][0].Tag.Should().Be(Tag.NameWithLanguage);
        dict2[IppAttributeNames.DocumentName][0].Value.Should().Be(new StringWithLanguage("fr", "Doc 2"));
        var back2 = _mapper.Map<DocumentAttributes>(dict2);
        back2.DocumentName.Should().Be("Doc 2");
        back2.DocumentName!.Value.HasLanguage.Should().BeTrue();
        back2.DocumentName!.Value.Language.Should().Be("fr");

        // 3. NoValue
        var src3 = new DocumentAttributes { DocumentName = NoValue.Instance };
        var dict3 = _mapper.Map<IDictionary<string, IppAttribute[]>>(src3);
        dict3[IppAttributeNames.DocumentName][0].Tag.Should().Be(Tag.NoValue);
        var back3 = _mapper.Map<DocumentAttributes>(dict3);
        back3.DocumentName.Should().Be(NoValue.Instance);
        back3.DocumentName!.Value.IsValue.Should().BeFalse();

        // 4. Null (not present)
        var src4 = new DocumentAttributes { DocumentName = null };
        var dict4 = _mapper.Map<IDictionary<string, IppAttribute[]>>(src4);
        dict4.ContainsKey(IppAttributeNames.DocumentName).Should().BeFalse();
        var back4 = _mapper.Map<DocumentAttributes>(dict4);
        back4.DocumentName.Should().BeNull();
    }
}
