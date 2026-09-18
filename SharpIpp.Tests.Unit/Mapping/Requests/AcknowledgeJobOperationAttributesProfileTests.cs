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
        attrs.Should().Contain(a => a.Name == IppAttributeNames.PrinterUri && a.Tag == Tag.Uri && (string)a.Value == "ipp://127.0.0.1/printers/printer1");
        attrs.Should().Contain(a => a.Name == IppAttributeNames.RequestingUserName && a.Tag == Tag.NameWithoutLanguage && (string)a.Value == "user");
        attrs.Should().Contain(a => a.Name == IppAttributeNames.OutputDeviceUuid && a.Tag == Tag.Uri && (string)a.Value == "urn:uuid:123e4567-e89b-12d3-a456-426614174001");
        attrs.Should().Contain(a => a.Name == IppAttributeNames.OutputDeviceJobStates && a.Tag == Tag.Enum && (int)a.Value == (int)JobState.Processing);
        attrs.Should().Contain(a => a.Name == IppAttributeNames.FetchStatusCode && a.Tag == Tag.Enum && (int)a.Value == (int)IppStatusCode.ClientErrorDocumentAccessError);
        attrs.Should().Contain(a => a.Name == IppAttributeNames.FetchStatusMessage && a.Tag == Tag.TextWithoutLanguage && (string)a.Value == "Access denied");
    }

    [TestMethod]
    public void Map_Attributes_To_AcknowledgeJobOperationAttributes_ShouldReadAttributes()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { IppAttributeNames.PrinterUri, new[] { new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://127.0.0.1/printers/printer1") } },
            { IppAttributeNames.RequestingUserName, new[] { new IppAttribute(Tag.NameWithoutLanguage, IppAttributeNames.RequestingUserName, "user") } },
            { IppAttributeNames.OutputDeviceUuid, new[] { new IppAttribute(Tag.Uri, IppAttributeNames.OutputDeviceUuid, "urn:uuid:123e4567-e89b-12d3-a456-426614174001") } },
            { IppAttributeNames.OutputDeviceJobStates, new[] { new IppAttribute(Tag.Enum, IppAttributeNames.OutputDeviceJobStates, (int)JobState.Processing) } },
            { IppAttributeNames.FetchStatusCode, new[] { new IppAttribute(Tag.Enum, IppAttributeNames.FetchStatusCode, (int)IppStatusCode.ClientErrorDocumentAccessError) } },
            { IppAttributeNames.FetchStatusMessage, new[] { new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.FetchStatusMessage, "Access denied") } }
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
