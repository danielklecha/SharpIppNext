using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Models.Requests;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class AcknowledgeDocumentOperationAttributesProfileTests : MapperTestBase
{
    [TestMethod]
    public void Map_AcknowledgeDocumentOperationAttributes_To_Attributes_ShouldEmitAcknowledgeDocumentAttributes()
    {
        // Arrange
        var src = new AcknowledgeDocumentOperationAttributes
        {
            PrinterUri = new Uri("ipp://127.0.0.1/printers/printer1"),
            RequestingUserName = "user",
            DocumentNumber = 2,
            OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174001"),
            FetchStatusCode = IppStatusCode.ClientErrorDocumentFormatError,
            FetchStatusMessage = "Unsupported format"
        };

        // Act
        var attrs = _mapper.Map<List<IppAttribute>>(src);

        // Assert
        attrs.Should().Contain(a => a.Name == JobAttribute.PrinterUri && a.Tag == Tag.Uri && (string)a.Value == "ipp://127.0.0.1/printers/printer1");
        attrs.Should().Contain(a => a.Name == JobAttribute.RequestingUserName && a.Tag == Tag.NameWithoutLanguage && (string)a.Value == "user");
        attrs.Should().Contain(a => a.Name == DocumentAttribute.DocumentNumber && a.Tag == Tag.Integer && (int)a.Value == 2);
        attrs.Should().Contain(a => a.Name == JobAttribute.OutputDeviceUuid && a.Tag == Tag.Uri && (string)a.Value == "urn:uuid:123e4567-e89b-12d3-a456-426614174001");
        attrs.Should().Contain(a => a.Name == JobAttribute.FetchStatusCode && a.Tag == Tag.Enum && (int)a.Value == (int)IppStatusCode.ClientErrorDocumentFormatError);
        attrs.Should().Contain(a => a.Name == JobAttribute.FetchStatusMessage && a.Tag == Tag.TextWithoutLanguage && (string)a.Value == "Unsupported format");
    }

    [TestMethod]
    public void Map_Attributes_To_AcknowledgeDocumentOperationAttributes_ShouldReadAttributes()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { JobAttribute.PrinterUri, new[] { new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1/printers/printer1") } },
            { JobAttribute.RequestingUserName, new[] { new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "user") } },
            { DocumentAttribute.DocumentNumber, new[] { new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, 2) } },
            { JobAttribute.OutputDeviceUuid, new[] { new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, "urn:uuid:123e4567-e89b-12d3-a456-426614174001") } },
            { JobAttribute.FetchStatusCode, new[] { new IppAttribute(Tag.Enum, JobAttribute.FetchStatusCode, (int)IppStatusCode.ClientErrorDocumentFormatError) } },
            { JobAttribute.FetchStatusMessage, new[] { new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.FetchStatusMessage, "Unsupported format") } }
        };

        // Act
        var dst = _mapper.Map<AcknowledgeDocumentOperationAttributes>(dict);

        // Assert
        dst.PrinterUri.Should().Be(new Uri("ipp://127.0.0.1/printers/printer1"));
        dst.RequestingUserName.Should().Be("user");
        dst.DocumentNumber.Should().Be(2);
        dst.OutputDeviceUuid.Should().Be(new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174001"));
        dst.FetchStatusCode.Should().Be(IppStatusCode.ClientErrorDocumentFormatError);
        dst.FetchStatusMessage.Should().Be("Unsupported format");
    }
}
