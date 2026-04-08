using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
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

    [TestMethod]
    public void Map_PrintJobOperationAttributes_WithDocumentMessage_MapsToIppAttributes()
    {
        var src = new PrintJobOperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en",
            DocumentMessage = "operator note"
        };

        var dst = _mapper.Map<PrintJobOperationAttributes, List<IppAttribute>>(src);

        dst.Should().Contain(x => x.Name == DocumentAttribute.DocumentMessage && Equals(x.Value, "operator note"));
    }

    [TestMethod]
    public void Map_SendDocumentOperationAttributes_WithDocumentMessage_MapsFromIppAttributes()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            { JobAttribute.AttributesCharset, [new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8")] },
            { JobAttribute.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en")] },
            { DocumentAttribute.DocumentMessage, [new IppAttribute(Tag.TextWithoutLanguage, DocumentAttribute.DocumentMessage, "doc note")] },
            { JobAttribute.LastDocument, [new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, true)] }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, SendDocumentOperationAttributes>(src);

        dst.DocumentMessage.Should().Be("doc note");
    }

    [TestMethod]
    public void Map_GetOutputDeviceAttributesOperationAttributes_WithRequestedAttributesAndOutputDeviceUuid_MapsToIppAttributes()
    {
        // Arrange
        var src = new GetOutputDeviceAttributesOperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en",
            RequestedAttributes = new[] { "printer-name", "printer-state" },
            OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174003")
        };

        // Act
        var dst = _mapper.Map<GetOutputDeviceAttributesOperationAttributes, List<IppAttribute>>(src);

        // Assert
        dst.Should().Contain(x => x.Name == JobAttribute.OutputDeviceUuid && x.Tag == Tag.Uri && x.Value.Equals("urn:uuid:123e4567-e89b-12d3-a456-426614174003"));
        dst.Should().Contain(x => x.Name == JobAttribute.RequestedAttributes && x.Tag == Tag.Keyword && x.Value.Equals("printer-name"));
        dst.Should().Contain(x => x.Name == JobAttribute.RequestedAttributes && x.Tag == Tag.Keyword && x.Value.Equals("printer-state"));
    }

    [TestMethod]
    public void Map_SystemOperationAttributes_WithoutCharsetAndNaturalLanguage_DefaultsValues()
    {
        // Arrange
        var src = new Dictionary<string, IppAttribute[]>
        {
            { SystemAttribute.SystemUri, [new IppAttribute(Tag.Uri, SystemAttribute.SystemUri, "ipp://system")] }
        };

        // Act
        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, SystemOperationAttributes>(src);

        // Assert
        dst.AttributesCharset.Should().Be("utf-8");
        dst.AttributesNaturalLanguage.Should().Be("en");
        dst.SystemUri.Should().Be(new Uri("ipp://system"));
    }

    [TestMethod]
    public void Map_CreateJobOperationAttributes_WithResourceIds_MapsFromIppAttributes()
    {
        // Arrange
        var src = new Dictionary<string, IppAttribute[]>
        {
            { JobAttribute.AttributesCharset, [new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8")] },
            { JobAttribute.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en")] },
            {
                SystemAttribute.ResourceIds,
                [
                    new IppAttribute(Tag.Integer, SystemAttribute.ResourceIds, 101),
                    new IppAttribute(Tag.Integer, SystemAttribute.ResourceIds, 202)
                ]
            }
        };

        // Act
        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, CreateJobOperationAttributes>(src);

        // Assert
        dst.ResourceIds.Should().BeEquivalentTo(new[] { 101, 202 });
    }

    [TestMethod]
    public void Map_CreateJobOperationAttributes_WithResourceIds_MapsToIppAttributes()
    {
        // Arrange
        var src = new CreateJobOperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en",
            ResourceIds = [301, 302]
        };

        // Act
        var dst = _mapper.Map<CreateJobOperationAttributes, List<IppAttribute>>(src);

        // Assert
        dst.Where(x => x.Name == SystemAttribute.ResourceIds)
            .Select(x => (int)x.Value)
            .Should()
            .BeEquivalentTo(new[] { 301, 302 });
    }

    [TestMethod]
    public void Map_SendDocumentOperationAttributes_WithResourceIds_MapsFromIppAttributes()
    {
        // Arrange
        var src = new Dictionary<string, IppAttribute[]>
        {
            { JobAttribute.AttributesCharset, [new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8")] },
            { JobAttribute.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en")] },
            {
                SystemAttribute.ResourceIds,
                [
                    new IppAttribute(Tag.Integer, SystemAttribute.ResourceIds, 401),
                    new IppAttribute(Tag.Integer, SystemAttribute.ResourceIds, 402)
                ]
            },
            { JobAttribute.LastDocument, [new IppAttribute(Tag.Boolean, JobAttribute.LastDocument, true)] }
        };

        // Act
        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, SendDocumentOperationAttributes>(src);

        // Assert
        dst.ResourceIds.Should().BeEquivalentTo(new[] { 401, 402 });
    }

    [TestMethod]
    public void Map_SendDocumentOperationAttributes_WithResourceIds_MapsToIppAttributes()
    {
        // Arrange
        var src = new SendDocumentOperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en",
            LastDocument = true,
            ResourceIds = [501, 502]
        };

        // Act
        var dst = _mapper.Map<SendDocumentOperationAttributes, List<IppAttribute>>(src);

        // Assert
        dst.Where(x => x.Name == SystemAttribute.ResourceIds)
            .Select(x => (int)x.Value)
            .Should()
            .BeEquivalentTo(new[] { 501, 502 });
    }
}
