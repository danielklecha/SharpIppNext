using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using SharpIpp.Protocol;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
public class OperationAttributesRequestProfileTests
{
    private SimpleMapper _mapper = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mapper = new SimpleMapper();
        var assembly = typeof(SimpleMapper).Assembly;
        _mapper.FillFromAssembly(assembly);
    }

    [TestMethod]
    public void Map_GetDocumentAttributesOperationAttributes_WithRequestedAttributes_SetsProperty()
    {
        // Arrange
        var src = new Dictionary<string, IppAttribute[]>
        {
            { JobAttribute.AttributesCharset, [new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8")] },
            { JobAttribute.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en")] },
            { JobAttribute.RequestedAttributes, [new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, "attr1"), new IppAttribute(Tag.Keyword, JobAttribute.RequestedAttributes, "attr2")] }
        };

        // Act
        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, GetDocumentAttributesOperationAttributes>(src);

        // Assert
        dst.RequestedAttributes.Should().NotBeNull();
        dst.RequestedAttributes.Should().BeEquivalentTo("attr1", "attr2");
    }

    [TestMethod]
    public void Map_GetDocumentAttributesOperationAttributes_WithoutRequestedAttributes_DoesNotSetProperty()
    {
        // Arrange
        var src = new Dictionary<string, IppAttribute[]>
        {
            { JobAttribute.AttributesCharset, [new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8")] },
            { JobAttribute.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en")] }
        };

        // Act
        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, GetDocumentAttributesOperationAttributes>(src);

        // Assert
        dst.RequestedAttributes.Should().BeNull();
    }

    [TestMethod]
    public void Map_GetDocumentAttributesOperationAttributes_WithEmptyRequestedAttributes_DoesNotSetProperty()
    {
        // Arrange
        var src = new Dictionary<string, IppAttribute[]>
        {
            { JobAttribute.AttributesCharset, [new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8")] },
            { JobAttribute.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en")] },
            { JobAttribute.RequestedAttributes, [] } 
        };

        // Act
        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, GetDocumentAttributesOperationAttributes>(src);

        // Assert
        dst.RequestedAttributes.Should().BeNull();
    }

    [TestMethod]
    public void Map_GetDocumentAttributesOperationAttributes_ExistingRequestedAttributes_ArePreservedWhenSourceMissing()
    {
        // Arrange
        var src = new Dictionary<string, IppAttribute[]>
        {
            { JobAttribute.AttributesCharset, [new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8")] },
            { JobAttribute.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en")] }
        };
        var dst = new GetDocumentAttributesOperationAttributes { RequestedAttributes = ["existing"] };

        // Act
        _mapper.Map(src, dst);

        // Assert
        dst.RequestedAttributes.Should().Contain("existing");
    }
}
