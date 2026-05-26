using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
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
            { IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")] },
            { IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")] },
            { IppAttributeNames.RequestedAttributes, [new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, "attr1"), new IppAttribute(Tag.Keyword, IppAttributeNames.RequestedAttributes, "attr2")] }
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
            { IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")] },
            { IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")] }
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
            { IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")] },
            { IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")] },
            { IppAttributeNames.RequestedAttributes, [] } 
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
            { IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")] },
            { IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")] }
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

        dst.Should().Contain(x => x.Name == IppAttributeNames.DocumentMessage && Equals(x.Value, "operator note"));
    }

    [TestMethod]
    public void Map_SendDocumentOperationAttributes_WithDocumentMessage_MapsFromIppAttributes()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")] },
            { IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")] },
            { IppAttributeNames.DocumentMessage, [new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.DocumentMessage, "doc note")] },
            { IppAttributeNames.LastDocument, [new IppAttribute(Tag.Boolean, IppAttributeNames.LastDocument, true)] }
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
        dst.Should().Contain(x => x.Name == IppAttributeNames.OutputDeviceUuid && x.Tag == Tag.Uri && x.Value.Equals("urn:uuid:123e4567-e89b-12d3-a456-426614174003"));
        dst.Should().Contain(x => x.Name == IppAttributeNames.RequestedAttributes && x.Tag == Tag.Keyword && x.Value.Equals("printer-name"));
        dst.Should().Contain(x => x.Name == IppAttributeNames.RequestedAttributes && x.Tag == Tag.Keyword && x.Value.Equals("printer-state"));
    }

    [TestMethod]
    public void Map_GetPrinterAttributesOperationAttributes_WithOutputDeviceUuid_MapsToIppAttributes()
    {
        var src = new GetPrinterAttributesOperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en",
            PrinterUri = new Uri("ipp://printer"),
            OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174003")
        };

        var dst = _mapper.Map<GetPrinterAttributesOperationAttributes, List<IppAttribute>>(src);

        dst.Should().Contain(x => x.Name == IppAttributeNames.OutputDeviceUuid && x.Tag == Tag.Uri && x.Value.Equals("urn:uuid:123e4567-e89b-12d3-a456-426614174003"));
    }

    [TestMethod]
    public void Map_GetPrinterAttributesOperationAttributes_WithOutputDeviceUuid_MapsFromIppAttributes()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")] },
            { IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")] },
            { IppAttributeNames.PrinterUri, [new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://printer")] },
            { IppAttributeNames.OutputDeviceUuid, [new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, "urn:uuid:123e4567-e89b-12d3-a456-426614174003")] }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, GetPrinterAttributesOperationAttributes>(src);

        dst.OutputDeviceUuid.Should().Be(new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174003"));
    }

    [TestMethod]
    public void Map_GetJobsOperationAttributes_WithOutputDeviceUuid_MapsToIppAttributes()
    {
        var src = new GetJobsOperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en",
            PrinterUri = new Uri("ipp://printer"),
            OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174003")
        };

        var dst = _mapper.Map<GetJobsOperationAttributes, List<IppAttribute>>(src);

        dst.Should().Contain(x => x.Name == IppAttributeNames.OutputDeviceUuid && x.Tag == Tag.Uri && x.Value.Equals("urn:uuid:123e4567-e89b-12d3-a456-426614174003"));
    }

    [TestMethod]
    public void Map_GetJobsOperationAttributes_WithOutputDeviceUuid_MapsFromIppAttributes()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")] },
            { IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")] },
            { IppAttributeNames.PrinterUri, [new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://printer")] },
            { IppAttributeNames.OutputDeviceUuid, [new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, "urn:uuid:123e4567-e89b-12d3-a456-426614174003")] }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, GetJobsOperationAttributes>(src);

        dst.OutputDeviceUuid.Should().Be(new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174003"));
    }

    [TestMethod]
    public void Map_ReleaseJobOperationAttributes_WithOutputDeviceUuid_MapsToIppAttributes()
    {
        var src = new ReleaseJobOperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en",
            PrinterUri = new Uri("ipp://printer"),
            JobId = 123,
            OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174003")
        };

        var dst = _mapper.Map<ReleaseJobOperationAttributes, List<IppAttribute>>(src);

        dst.Should().Contain(x => x.Name == IppAttributeNames.OutputDeviceUuid && x.Tag == Tag.Uri && x.Value.Equals("urn:uuid:123e4567-e89b-12d3-a456-426614174003"));
    }

    [TestMethod]
    public void Map_ReleaseJobOperationAttributes_WithOutputDeviceUuid_MapsFromIppAttributes()
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")] },
            { IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")] },
            { IppAttributeNames.PrinterUri, [new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://printer")] },
            { IppAttributeNames.JobId, [new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 123)] },
            { IppAttributeNames.OutputDeviceUuid, [new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, "urn:uuid:123e4567-e89b-12d3-a456-426614174003")] }
        };

        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, ReleaseJobOperationAttributes>(src);

        dst.OutputDeviceUuid.Should().Be(new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174003"));
    }

    [TestMethod]
    public void Map_SystemOperationAttributes_WithoutCharsetAndNaturalLanguage_DefaultsValues()
    {
        // Arrange
        var src = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.SystemUri, [new IppAttribute(Tag.Uri, IppAttributeNames.SystemUri, "ipp://system")] }
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
            { IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")] },
            { IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")] },
            {
                IppAttributeNames.ResourceIds,
                [
                    new IppAttribute(Tag.Integer, IppAttributeNames.ResourceIds, 101),
                    new IppAttribute(Tag.Integer, IppAttributeNames.ResourceIds, 202)
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
        dst.Where(x => x.Name == IppAttributeNames.ResourceIds)
            .Select(x => (int)x.Value)
            .Should()
            .BeEquivalentTo(new[] { 301, 302 });
    }

    [TestMethod]
    public void Map_CreateJobOperationAttributes_WithDestinationAccesses_MapsFromIppAttributes()
    {
        // Arrange
        var src = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")] },
            { IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")] },
            {
                IppAttributeNames.DestinationAccesses,
                [
                    new IppAttribute(Tag.BegCollection, IppAttributeNames.DestinationAccesses, NoValue.Instance),
                    new IppAttribute(Tag.MemberAttrName, "", "access-user-name"),
                    new IppAttribute(Tag.NameWithoutLanguage, "", "scan-user"),
                    new IppAttribute(Tag.MemberAttrName, "", "access-password"),
                    new IppAttribute(Tag.TextWithoutLanguage, "", "secret"),
                    new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
                ]
            }
        };

        // Act
        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, CreateJobOperationAttributes>(src);

        // Assert
        dst.DestinationAccesses.Should().NotBeNull();
        dst.DestinationAccesses!.Should().ContainSingle();
        dst.DestinationAccesses[0].AccessUserName.Should().Be("scan-user");
        dst.DestinationAccesses[0].AccessPassword.Should().Be("secret");
    }

    [TestMethod]
    public void Map_CreateJobOperationAttributes_WithDestinationAccesses_MapsToIppAttributes()
    {
        // Arrange
        var src = new CreateJobOperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en",
            DestinationAccesses =
            [
                new DocumentAccess
                {
                    AccessUserName = "scan-user",
                    AccessPassword = "secret"
                }
            ]
        };

        // Act
        var dst = _mapper.Map<CreateJobOperationAttributes, List<IppAttribute>>(src);

        // Assert
        dst.Should().Contain(x => x.Name == IppAttributeNames.DestinationAccesses && x.Tag == Tag.BegCollection);
        dst.Should().Contain(x => x.Tag == Tag.MemberAttrName && (string)x.Value == "access-user-name");
        dst.Should().Contain(x => x.Tag == Tag.NameWithoutLanguage && (string)x.Value == "scan-user");
        dst.Should().Contain(x => x.Tag == Tag.MemberAttrName && (string)x.Value == "access-password");
        dst.Should().Contain(x => x.Tag == Tag.TextWithoutLanguage && (string)x.Value == "secret");
    }

    [TestMethod]
    public void Map_SendDocumentOperationAttributes_WithResourceIds_MapsFromIppAttributes()
    {
        // Arrange
        var src = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")] },
            { IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")] },
            {
                IppAttributeNames.ResourceIds,
                [
                    new IppAttribute(Tag.Integer, IppAttributeNames.ResourceIds, 401),
                    new IppAttribute(Tag.Integer, IppAttributeNames.ResourceIds, 402)
                ]
            },
            { IppAttributeNames.LastDocument, [new IppAttribute(Tag.Boolean, IppAttributeNames.LastDocument, true)] }
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
        dst.Where(x => x.Name == IppAttributeNames.ResourceIds)
            .Select(x => (int)x.Value)
            .Should()
            .BeEquivalentTo(new[] { 501, 502 });
    }
    [TestMethod]
    public void Map_GetNextDocumentDataOperationAttributes_ToIppAttributes_ShouldIncludeJobIdAndExcludeJobUri()
    {
        // Arrange
        var src = new GetNextDocumentDataOperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en",
            PrinterUri = new Uri("ipp://printer"),
            JobId = 123,
            DocumentDataWait = true
        };

        // Act
        var dst = _mapper.Map<GetNextDocumentDataOperationAttributes, List<IppAttribute>>(src);

        // Assert
        dst.Should().Contain(x => x.Name == IppAttributeNames.PrinterUri && Equals(x.Value, "ipp://printer/"));
        dst.Should().Contain(x => x.Name == IppAttributeNames.JobId && Equals(x.Value, 123));
        dst.Should().Contain(x => x.Name == IppAttributeNames.DocumentDataWait && Equals(x.Value, true));
        dst.Should().NotContain(x => x.Name == IppAttributeNames.JobUri);
    }

    [TestMethod]
    public void Map_GetNextDocumentDataOperationAttributes_FromIppAttributes_ShouldSetJobId()
    {
        // Arrange
        var src = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")] },
            { IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")] },
            { IppAttributeNames.PrinterUri, [new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://printer/")] },
            { IppAttributeNames.JobId, [new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 456)] },
            { IppAttributeNames.DocumentDataWait, [new IppAttribute(Tag.Boolean, IppAttributeNames.DocumentDataWait, false)] }
        };

        // Act
        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, GetNextDocumentDataOperationAttributes>(src);

        // Assert
        dst.PrinterUri.Should().Be(new Uri("ipp://printer/"));
        dst.JobId.Should().Be(456);
        dst.DocumentDataWait.Should().Be(false);
    }

    [TestMethod]
    public void Map_AddDocumentImagesOperationAttributes_ToIppAttributes_ShouldIncludeJobIdAndExcludeJobUri()
    {
        // Arrange
        var src = new AddDocumentImagesOperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en",
            PrinterUri = new Uri("ipp://printer/"),
            JobId = 789,
            InputAttributes = new DocumentTemplateAttributes { DocumentFormat = "image/pwg-raster" }
        };

        // Act
        var dst = _mapper.Map<AddDocumentImagesOperationAttributes, List<IppAttribute>>(src);

        // Assert
        dst.Should().Contain(x => x.Name == IppAttributeNames.PrinterUri && Equals(x.Value, "ipp://printer/"));
        dst.Should().Contain(x => x.Name == IppAttributeNames.JobId && Equals(x.Value, 789));
        dst.Should().Contain(x => Equals(x.Value, "image/pwg-raster"));
        dst.Should().NotContain(x => x.Name == IppAttributeNames.JobUri);
    }

    [TestMethod]
    public void Map_AddDocumentImagesOperationAttributes_FromIppAttributes_ShouldSetJobId()
    {
        // Arrange
        var src = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.AttributesCharset, [new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8")] },
            { IppAttributeNames.AttributesNaturalLanguage, [new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en")] },
            { IppAttributeNames.PrinterUri, [new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://printer")] },
            { IppAttributeNames.JobId, [new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 101)] },
            { IppAttributeNames.InputAttributes, [new IppAttribute(Tag.BegCollection, IppAttributeNames.InputAttributes, NoValue.Instance), new IppAttribute(Tag.MemberAttrName, "", IppAttributeNames.DocumentFormat), new IppAttribute(Tag.MimeMediaType, "", "image/tiff"), new IppAttribute(Tag.EndCollection, "", NoValue.Instance)] }
        };

        // Act
        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, AddDocumentImagesOperationAttributes>(src);

        // Assert
        dst.PrinterUri.Should().Be(new Uri("ipp://printer"));
        dst.JobId.Should().Be(101);
        dst.InputAttributes.Should().NotBeNull();
        dst.InputAttributes!.DocumentFormat.Should().Be("image/tiff");
    }

    [TestMethod]
    public void Map_IdentifyPrinterOperationAttributes_WithAllProperties_MapsBidirectionally()
    {
        // Arrange
        var src = new IdentifyPrinterOperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en",
            PrinterUri = new Uri("ipp://printer/"),
            IdentifyActions = new[] { IdentifyAction.Flash, IdentifyAction.Sound },
            OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174001"),
            JobId = 123,
            Message = "Test identify message"
        };

        // Act
        var attributesList = _mapper.Map<IdentifyPrinterOperationAttributes, List<IppAttribute>>(src);
        var dictionary = attributesList.ToIppDictionary();
        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, IdentifyPrinterOperationAttributes>(dictionary);

        // Assert
        dst.AttributesCharset.Should().Be(src.AttributesCharset);
        dst.AttributesNaturalLanguage.Should().Be(src.AttributesNaturalLanguage);
        dst.PrinterUri.Should().Be(src.PrinterUri);
        dst.IdentifyActions.Should().BeEquivalentTo(src.IdentifyActions);
        dst.OutputDeviceUuid.Should().Be(src.OutputDeviceUuid);
        dst.JobId.Should().Be(src.JobId);
        dst.Message.Should().Be(src.Message);
    }

    [TestMethod]
    public void Map_GetUserPrinterAttributesOperationAttributes_WithAllProperties_MapsBidirectionally()
    {
        // Arrange
        var src = new GetUserPrinterAttributesOperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en",
            PrinterUri = new Uri("ipp://printer/"),
            FirstIndex = 10,
            Limit = 5,
            PrinterId = 42,
            DocumentFormat = "application/pdf",
            OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174002"),
            RequestedAttributes = new[] { "printer-name", "printer-state" },
            RequestingUserVcard = new[] { "vcard-line-1", "vcard-line-2" }
        };

        // Act
        var attributesList = _mapper.Map<GetUserPrinterAttributesOperationAttributes, List<IppAttribute>>(src);
        var dictionary = attributesList.ToIppDictionary();
        var dst = _mapper.Map<IDictionary<string, IppAttribute[]>, GetUserPrinterAttributesOperationAttributes>(dictionary);

        // Assert
        dst.AttributesCharset.Should().Be(src.AttributesCharset);
        dst.AttributesNaturalLanguage.Should().Be(src.AttributesNaturalLanguage);
        dst.PrinterUri.Should().Be(src.PrinterUri);
        dst.FirstIndex.Should().Be(src.FirstIndex);
        dst.Limit.Should().Be(src.Limit);
        dst.PrinterId.Should().Be(src.PrinterId);
        dst.DocumentFormat.Should().Be(src.DocumentFormat);
        dst.OutputDeviceUuid.Should().Be(src.OutputDeviceUuid);
        dst.RequestedAttributes.Should().BeEquivalentTo(src.RequestedAttributes);
        dst.RequestingUserVcard.Should().BeEquivalentTo(src.RequestingUserVcard);
    }
}
