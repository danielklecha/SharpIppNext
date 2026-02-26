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
    public void GetIppAttributes_ShouldThrowArgumentNullException_WhenAttributesCharsetIsNull()
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
        Action act = () => mapper.Map<OperationAttributes, List<IppAttribute>>(operationAttributes);

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
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        Action act = () => mapper.Map<OperationAttributes, List<IppAttribute>>(operationAttributes);

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
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var attributes = mapper.Map<OperationAttributes, List<IppAttribute>>(operationAttributes);

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
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var attributes = mapper.Map<IDictionary<string, IppAttribute[]>, OperationAttributes>(dict);

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
            { JobAttribute.JobId, new[] { new IppAttribute(Tag.Integer, JobAttribute.JobId, 123) } }
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
            AttributesCharset = "utf-8",
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
        result1.Should().Contain(x => x.Name == JobAttribute.JobId && (int)x.Value == 456);

        // Act - dst is existing
        var existing = new List<IppAttribute>();
        var result2 = mapper.Map(src, existing);
        
        // Assert
        result2.Should().BeSameAs(existing);
        result2.Should().Contain(x => x.Name == JobAttribute.JobId && (int)x.Value == 456);
    }

    [TestMethod]
    public void Map_ToGetJobsOperationAttributes_ShouldMapMyJobs()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { JobAttribute.MyJobs, new[] { new IppAttribute(Tag.Boolean, JobAttribute.MyJobs, true) } }
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
    public void Map_FromValidateJobOperationAttributes_ShouldMapAdditionalProperties()
    {
        // Arrange
        var src = new ValidateJobOperationAttributes
        {
            AttributesCharset = "utf-8",
            AttributesNaturalLanguage = "en-us",
            JobKOctets = 100,
            DocumentName = "test.txt",
            Compression = Compression.Gzip,
            DocumentFormat = "text/plain",
            DocumentNaturalLanguage = "en-us"
        };
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Act
        var result = mapper.Map<ValidateJobOperationAttributes, List<IppAttribute>>(src);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().Contain(x => x.Name == JobAttribute.JobKOctets && (int)x.Value == 100);
        result.Should().Contain(x => x.Name == JobAttribute.DocumentName && (string)x.Value == "test.txt");
        result.Should().Contain(x => x.Name == JobAttribute.Compression && (string)x.Value == "gzip");
        result.Should().Contain(x => x.Name == JobAttribute.DocumentFormat && (string)x.Value == "text/plain");
        result.Should().Contain(x => x.Name == JobAttribute.DocumentNaturalLanguage && (string)x.Value == "en-us");
    }
    [TestMethod]
    public void Map_ToPrintUriOperationAttributes_ShouldNotSetDocumentUri_WhenDocumentUriIsInvalid()
    {
        // Arrange
         // space in URI is invalid
        var invalidUri = "http://invalid uri";
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { JobAttribute.DocumentUri, new[] { new IppAttribute(Tag.Uri, JobAttribute.DocumentUri, invalidUri) } }
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
