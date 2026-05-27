using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpIpp.Mapping.Profiles;
using SharpIpp.Mapping.Profiles.Requests;

namespace SharpIpp.Tests.Unit.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class OperationAttributesTests
{
    [TestMethod]
    public void GetIppAttributes_ShouldDefaultCharset_WhenAttributesCharsetIsNull()
    {
        // Arrange
        var operationAttributes = new OperationAttributes
        {
            AttributesCharset = null,
            AttributesNaturalLanguage = "en-us",
            PrinterUri = new Uri("ipp://printer")
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var attributes = mapper.Map<OperationAttributes, List<IppAttribute>>(operationAttributes);

        // Assert
        attributes.Should().Contain(x => x.Name == IppAttributeNames.AttributesCharset && (string)x.Value == "utf-8");
    }

    [TestMethod]
    public void GetIppAttributes_ShouldDefaultNaturalLanguage_WhenAttributesNaturalLanguageIsNull()
    {
        // Arrange
        var operationAttributes = new OperationAttributes
        {
            AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8",
            AttributesNaturalLanguage = null,
            PrinterUri = new Uri("ipp://printer")
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var attributes = mapper.Map<OperationAttributes, List<IppAttribute>>(operationAttributes);

        // Assert
        attributes.Should().Contain(x => x.Name == IppAttributeNames.AttributesNaturalLanguage && (string)x.Value == "en");
    }

    [TestMethod]
    public void GetIppAttributes_ShouldReturnAttributes_WhenAllPropertiesAreSet()
    {
        // Arrange
        var operationAttributes = new OperationAttributes
        {
            AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8",
            AttributesNaturalLanguage = "en-us",
            PrinterUri = new Uri("ipp://printer"),
            RequestingUserName = "user"
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var attributes = mapper.Map<OperationAttributes, List<IppAttribute>>(operationAttributes);

        // Assert
        attributes.Should().Contain(x => x.Name == IppAttributeNames.AttributesCharset && (string)x.Value == "utf-8");
        attributes.Should().Contain(x => x.Name == IppAttributeNames.AttributesNaturalLanguage && (string)x.Value == "en-us");
        attributes.Should().Contain(x => x.Name == IppAttributeNames.PrinterUri && (string)x.Value == "ipp://printer/");
        attributes.Should().Contain(x => x.Name == IppAttributeNames.RequestingUserName && (string)x.Value == "user");
    }

    [TestMethod]
    public void Create_ShouldReturnAttributes_WhenAllPropertiesAreSet()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.AttributesCharset, new[] { new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8") } },
            { IppAttributeNames.AttributesNaturalLanguage, new[] { new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en-us") } },
            { IppAttributeNames.PrinterUri, new[] { new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://printer") } },
            { IppAttributeNames.RequestingUserName, new[] { new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "user") } }
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var attributes = mapper.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(dict);

        // Assert
        attributes.AttributesCharset.Should().Be((SharpIpp.Protocol.Models.Charset)"utf-8");
        ((string?)attributes.AttributesNaturalLanguage).Should().Be("en-us");
        attributes.PrinterUri.Should().Be(new Uri("ipp://printer"));
        attributes.RequestingUserName.Should().Be("user");
    }

    [TestMethod]
    public void Create_ShouldDefaultAttributesCharsetAndNaturalLanguage_WhenNotPresent()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.PrinterUri, new[] { new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://printer") } }
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var attributes = mapper.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(dict);

        // Assert
        attributes.AttributesCharset.Should().Be((SharpIpp.Protocol.Models.Charset)"utf-8");
        ((string?)attributes.AttributesNaturalLanguage).Should().Be("en");
        attributes.PrinterUri.Should().Be(new Uri("ipp://printer"));
    }

    [TestMethod]
    public void Create_ShouldMapRequestingUserUri_WhenPresent()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.RequestingUserUri, new[] { new IppAttribute(Tag.Uri, IppAttributeNames.RequestingUserUri, "urn:uuid:00000000-0000-0000-0000-000000000000") } }
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var attributes = mapper.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(dict);

        // Assert
        attributes.RequestingUserUri.Should().Be(new Uri("urn:uuid:00000000-0000-0000-0000-000000000000"));
    }

    [TestMethod]
    public void Create_ShouldNotSetPrinterUri_WhenPrinterUriIsInvalid()
    {
        // Arrange
         // space in URI is invalid
        var invalidUri = "http://invalid uri";
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.PrinterUri, new[] { new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, invalidUri) } }
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var attributes = mapper.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(dict);

        // Assert
        attributes.PrinterUri.Should().BeNull();
    }

    [TestMethod]
    public void Create_ShouldNotSetPrinterUri_WhenPrinterUriIsNull()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>();
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var attributes = mapper.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(dict);

        // Assert
        attributes.PrinterUri.Should().BeNull();
    }

    [TestMethod]
    public void Map_ToJobOperationAttributes_ShouldHandleNullAndExistingDestination()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.JobId, new[] { new IppAttribute(Tag.Integer, IppAttributeNames.JobId, 123) } }
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act - dst is null
        var result1 = mapper.Map<IDictionary<string, IppAttribute[]>, JobOperationAttributes>(dict);
        
        // Assert
        result1.Should().NotBeNull();
        result1.JobId.Should().Be(123);

        // Act - dst is existing
        var existing = new JobOperationAttributes();
        var result2 = mapper.Map(dict, existing);
        
        // Assert
        result2.Should().BeSameAs(existing);
        result2.JobId.Should().Be(123);
    }

    [TestMethod]
    public void Map_FromJobOperationAttributes_ShouldHandleNullAndExistingDestination()
    {
        // Arrange
        var src = new JobOperationAttributes
        {
            AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8",
            AttributesNaturalLanguage = "en-us",
            JobId = 456
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act - dst is null
        var result1 = mapper.Map<JobOperationAttributes, List<IppAttribute>>(src);
        
        // Assert
        result1.Should().NotBeNull();
        result1.Should().Contain(x => x.Name == IppAttributeNames.JobId && (int)x.Value == 456);

        // Act - dst is existing
        var existing = new List<IppAttribute>();
        var result2 = mapper.Map(src, existing);
        
        // Assert
        result2.Should().BeSameAs(existing);
        result2.Should().Contain(x => x.Name == IppAttributeNames.JobId && (int)x.Value == 456);
    }

    [TestMethod]
    public void Map_ToGetJobsOperationAttributes_ShouldMapMyJobs()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.MyJobs, new[] { new IppAttribute(Tag.Boolean, IppAttributeNames.MyJobs, true) } }
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var result = mapper.Map<IDictionary<string, IppAttribute[]>, GetJobsOperationAttributes>(dict);
        
        // Assert
        result.MyJobs.Should().BeTrue();
    }

    [TestMethod]
    public void Map_ToGetJobsOperationAttributes_ShouldMapJobIds_WhenPresent()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.JobIds, new[] { new IppAttribute(Tag.Integer, IppAttributeNames.JobIds, 123), new IppAttribute(Tag.Integer, IppAttributeNames.JobIds, 456) } }
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var result = mapper.Map<IDictionary<string, IppAttribute[]>, GetJobsOperationAttributes>(dict);

        // Assert
        result.JobIds.Should().BeEquivalentTo(new[] { 123, 456 });
    }

    [TestMethod]
    public void Map_ToCreateJobOperationAttributes_ShouldMapPwg51007OperationAttributes_WhenPresent()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            {
                IppAttributeNames.ClientInfo,
                new[]
                {
                    new IppAttribute(Tag.BegCollection, IppAttributeNames.ClientInfo, NoValue.Instance),
                    new IppAttribute(Tag.MemberAttrName, "", "client-name"),
                    new IppAttribute(Tag.NameWithoutLanguage, "", "MyClient"),
                    new IppAttribute(Tag.MemberAttrName, "", "client-type"),
                    new IppAttribute(Tag.Integer, "", 3),
                    new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
                }
            },
            {
                IppAttributeNames.DocumentFormatDetails,
                new[]
                {
                    new IppAttribute(Tag.BegCollection, IppAttributeNames.DocumentFormatDetails, NoValue.Instance),
                    new IppAttribute(Tag.MemberAttrName, "", "document-source-application-name"),
                    new IppAttribute(Tag.NameWithoutLanguage, "", "MyApp"),
                    new IppAttribute(Tag.MemberAttrName, "", "document-source-os-name"),
                    new IppAttribute(Tag.NameWithoutLanguage, "", "MyOS"),
                    new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
                }
            },
            {
                IppAttributeNames.JobMandatoryAttributes,
                new[]
                {
                    new IppAttribute(Tag.Keyword, IppAttributeNames.JobMandatoryAttributes, "copies"),
                    new IppAttribute(Tag.Keyword, IppAttributeNames.JobMandatoryAttributes, "media")
                }
            }
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var result = mapper.Map<IDictionary<string, IppAttribute[]>, CreateJobOperationAttributes>(dict);

        // Assert
        result.ClientInfo.Should().NotBeNull().And.HaveCount(1);
        result.ClientInfo![0].ClientName.Should().Be("MyClient");
        result.ClientInfo![0].ClientType.Should().Be(ClientType.Application);
        result.DocumentFormatDetails.Should().NotBeNull();
        result.DocumentFormatDetails!.DocumentSourceApplicationName.Should().Be("MyApp");
        result.DocumentFormatDetails!.DocumentSourceOsName.Should().Be("MyOS");
        result.JobMandatoryAttributes.Should().BeEquivalentTo(new[] { "copies", "media" });
    }

    [TestMethod]
    public void Map_FromCancelJobsOperationAttributes_ShouldWriteJobIdsAndMessage()
    {
        // Arrange
        var src = new CancelJobsOperationAttributes
        {
            AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8",
            AttributesNaturalLanguage = "en-us",
            PrinterUri = new Uri("ipp://printer"),
            JobIds = new[] { 10, 20 },
            Message = "test"
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var result = mapper.Map<CancelJobsOperationAttributes, List<IppAttribute>>(src);

        // Assert
        result.Should().Contain(x => x.Name == IppAttributeNames.JobIds && (int)x.Value == 10);
        result.Should().Contain(x => x.Name == IppAttributeNames.JobIds && (int)x.Value == 20);
        result.Should().Contain(x => x.Name == IppAttributeNames.Message && (string)x.Value == "test");
    }

    [TestMethod]
    public void Map_FromResubmitJobOperationAttributes_ShouldWriteAdditionalProperties()
    {
        // Arrange
        var src = new ResubmitJobOperationAttributes
        {
            AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8",
            AttributesNaturalLanguage = "en-us",
            PrinterUri = new Uri("ipp://printer"),
            JobId = 123,
            IppAttributeFidelity = true,
            JobMandatoryAttributes = new[] { "copies" }
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var result = mapper.Map<ResubmitJobOperationAttributes, List<IppAttribute>>(src);

        // Assert
        result.Should().Contain(x => x.Name == IppAttributeNames.IppAttributeFidelity && (bool)x.Value == true);
        result.Should().Contain(x => x.Name == IppAttributeNames.JobMandatoryAttributes && (string)x.Value == "copies");
    }
    [TestMethod]
    public void Map_FromValidateJobOperationAttributes_ShouldMapAdditionalProperties()
    {
        // Arrange
        var src = new ValidateJobOperationAttributes
        {
            AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8",
            AttributesNaturalLanguage = "en-us",
            JobKOctets = 100,
            JobMandatoryAttributes = new[] { "copies", "media" },
            DocumentName = "test.txt",
            Compression = Compression.Gzip,
            DocumentFormat = (SharpIpp.Protocol.Models.DocumentFormat)"text/plain",
            DocumentNaturalLanguage = "en-us",
            ClientInfo =
            [
                new ClientInfo
                {
                    ClientName = "MyClient",
                    ClientType = ClientType.Application,
                },
            ],
            DocumentFormatDetails = new DocumentFormatDetails
            {
                DocumentSourceApplicationName = "MyApp",
                DocumentSourceOsName = "MyOS",
            },
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var result = mapper.Map<ValidateJobOperationAttributes, List<IppAttribute>>(src);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().Contain(x => x.Name == IppAttributeNames.JobKOctets && (int)x.Value == 100);
        result.Should().Contain(x => x.Name == IppAttributeNames.DocumentName && (string)x.Value == "test.txt");
        result.Should().Contain(x => x.Name == IppAttributeNames.Compression && (string)x.Value == "gzip");
        result.Should().Contain(x => x.Name == IppAttributeNames.DocumentFormat && (string)x.Value == "text/plain");
        result.Should().Contain(x => x.Name == IppAttributeNames.DocumentNaturalLanguage && (string)x.Value == "en-us");
        result.Should().Contain(x => x.Name == IppAttributeNames.JobMandatoryAttributes && (string)x.Value == "copies");
        result.Should().Contain(x => x.Name == IppAttributeNames.JobMandatoryAttributes && (string)x.Value == "media");
        result.Should().Contain(x => x.Name == IppAttributeNames.ClientInfo && x.Tag == Tag.BegCollection);
        result.Should().Contain(x => x.Name == IppAttributeNames.DocumentFormatDetails && x.Tag == Tag.BegCollection);
    }

    [TestMethod]
    public void Map_ToValidateJobOperationAttributes_ShouldMapPwg51007OperationAttributes_WhenPresent()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            {
                IppAttributeNames.ClientInfo,
                new[]
                {
                    new IppAttribute(Tag.BegCollection, IppAttributeNames.ClientInfo, NoValue.Instance),
                    new IppAttribute(Tag.MemberAttrName, "", "client-name"),
                    new IppAttribute(Tag.NameWithoutLanguage, "", "MyClient"),
                    new IppAttribute(Tag.MemberAttrName, "", "client-type"),
                    new IppAttribute(Tag.Integer, "", 3),
                    new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
                }
            },
            {
                IppAttributeNames.DocumentFormatDetails,
                new[]
                {
                    new IppAttribute(Tag.BegCollection, IppAttributeNames.DocumentFormatDetails, NoValue.Instance),
                    new IppAttribute(Tag.MemberAttrName, "", "document-source-application-name"),
                    new IppAttribute(Tag.NameWithoutLanguage, "", "MyApp"),
                    new IppAttribute(Tag.MemberAttrName, "", "document-source-os-name"),
                    new IppAttribute(Tag.NameWithoutLanguage, "", "MyOS"),
                    new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
                }
            },
            {
                IppAttributeNames.JobMandatoryAttributes,
                new[]
                {
                    new IppAttribute(Tag.Keyword, IppAttributeNames.JobMandatoryAttributes, "copies"),
                    new IppAttribute(Tag.Keyword, IppAttributeNames.JobMandatoryAttributes, "media")
                }
            }
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var result = mapper.Map<IDictionary<string, IppAttribute[]>, ValidateJobOperationAttributes>(dict);

        // Assert
        result.ClientInfo.Should().NotBeNull().And.HaveCount(1);
        result.ClientInfo![0].ClientName.Should().Be("MyClient");
        result.ClientInfo![0].ClientType.Should().Be(ClientType.Application);
        result.DocumentFormatDetails.Should().NotBeNull();
        result.DocumentFormatDetails!.DocumentSourceApplicationName.Should().Be("MyApp");
        result.DocumentFormatDetails!.DocumentSourceOsName.Should().Be("MyOS");
        result.JobMandatoryAttributes.Should().BeEquivalentTo(new[] { "copies", "media" });
    }
    [TestMethod]
    public void Map_ToPrintUriOperationAttributes_ShouldNotSetDocumentUri_WhenDocumentUriIsInvalid()
    {
        // Arrange
         // space in URI is invalid
        var invalidUri = "http://invalid uri";
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.DocumentUri, new[] { new IppAttribute(Tag.Uri, IppAttributeNames.DocumentUri, invalidUri) } }
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var attributes = mapper.Map<IDictionary<string, IppAttribute[]>, PrintUriOperationAttributes>(dict);

        // Assert
        attributes.DocumentUri.Should().BeNull();
    }

    [TestMethod]
    public void Map_ToPrintUriOperationAttributes_ShouldNotSetDocumentUri_WhenDocumentUriIsNull()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>();
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var attributes = mapper.Map<IDictionary<string, IppAttribute[]>, PrintUriOperationAttributes>(dict);

        // Assert
        attributes.DocumentUri.Should().BeNull();
    }
}
