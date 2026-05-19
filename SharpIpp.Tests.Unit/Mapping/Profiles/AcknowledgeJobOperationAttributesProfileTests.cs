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
public class AcknowledgeJobOperationAttributesProfileTests : MapperTestBase
{
    [TestMethod]
    public void Map_AcknowledgeJobOperationAttributes_To_Attributes_ShouldEmitAcknowledgeJobAttributes()
    {
        // Arrange
        var src = new AcknowledgeJobOperationAttributes
        {
            PrinterUri = new Uri("ipp://127.0.0.1/printers/printer1"),
            RequestingUserName = "user",
            OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174001"),
            OutputDeviceJobStates = new[] { JobState.Processing },
            FetchStatusCode = IppStatusCode.ClientErrorDocumentAccessError,
            FetchStatusMessage = "Access denied"
        };

        // Act
        var attrs = _mapper.Map<List<IppAttribute>>(src);

        // Assert
        attrs.Should().Contain(a => a.Name == JobAttribute.PrinterUri && a.Tag == Tag.Uri && (string)a.Value == "ipp://127.0.0.1/printers/printer1");
        attrs.Should().Contain(a => a.Name == JobAttribute.RequestingUserName && a.Tag == Tag.NameWithoutLanguage && (string)a.Value == "user");
        attrs.Should().Contain(a => a.Name == JobAttribute.OutputDeviceUuid && a.Tag == Tag.Uri && (string)a.Value == "urn:uuid:123e4567-e89b-12d3-a456-426614174001");
        attrs.Should().Contain(a => a.Name == JobAttribute.OutputDeviceJobStates && a.Tag == Tag.Enum && (int)a.Value == (int)JobState.Processing);
        attrs.Should().Contain(a => a.Name == JobAttribute.FetchStatusCode && a.Tag == Tag.Enum && (int)a.Value == (int)IppStatusCode.ClientErrorDocumentAccessError);
        attrs.Should().Contain(a => a.Name == JobAttribute.FetchStatusMessage && a.Tag == Tag.TextWithoutLanguage && (string)a.Value == "Access denied");
    }

    [TestMethod]
    public void Map_Attributes_To_AcknowledgeJobOperationAttributes_ShouldReadAttributes()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { JobAttribute.PrinterUri, new[] { new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://127.0.0.1/printers/printer1") } },
            { JobAttribute.RequestingUserName, new[] { new IppAttribute(Tag.NameWithoutLanguage, JobAttribute.RequestingUserName, "user") } },
            { JobAttribute.OutputDeviceUuid, new[] { new IppAttribute(Tag.Uri, JobAttribute.OutputDeviceUuid, "urn:uuid:123e4567-e89b-12d3-a456-426614174001") } },
            { JobAttribute.OutputDeviceJobStates, new[] { new IppAttribute(Tag.Enum, JobAttribute.OutputDeviceJobStates, (int)JobState.Processing) } },
            { JobAttribute.FetchStatusCode, new[] { new IppAttribute(Tag.Enum, JobAttribute.FetchStatusCode, (int)IppStatusCode.ClientErrorDocumentAccessError) } },
            { JobAttribute.FetchStatusMessage, new[] { new IppAttribute(Tag.TextWithoutLanguage, JobAttribute.FetchStatusMessage, "Access denied") } }
        };

        // Act
        var dst = _mapper.Map<AcknowledgeJobOperationAttributes>(dict);

        // Assert
        dst.PrinterUri.Should().Be(new Uri("ipp://127.0.0.1/printers/printer1"));
        dst.RequestingUserName.Should().Be("user");
        dst.OutputDeviceUuid.Should().Be(new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174001"));
        dst.OutputDeviceJobStates.Should().ContainSingle().Which.Should().Be(JobState.Processing);
        dst.FetchStatusCode.Should().Be(IppStatusCode.ClientErrorDocumentAccessError);
        dst.FetchStatusMessage.Should().Be("Access denied");
    }
}
