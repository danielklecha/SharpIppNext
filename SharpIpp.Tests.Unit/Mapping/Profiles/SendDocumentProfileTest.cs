using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Mapping.Profiles;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class SendDocumentProfileTest
{
    [DataRow(true, true)]
    [DataRow(true, false)]
    [TestMethod]
    public void Map_SendDocumentRequestToIppRequestMessage_InvalidRequest_DoesNotThrow(bool isDocumentNull, bool isOperationAttributesNull)
    {
        // Arrange
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        var request = new SendDocumentRequest
        {
            Document = isDocumentNull ? null : new MemoryStream(),
            OperationAttributes = isOperationAttributesNull ? null : new SendDocumentOperationAttributes
            {
                LastDocument = false
            }
        };

        // Act
        Action act = () => mapper.Map<SendDocumentRequest, IppRequestMessage>(request);

        // Assert
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Map_IppRequestMessageToSendDocumentRequest_MissingLastDocumentAttribute_DoesNotThrow()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        var request = new IppRequestMessage
        {
            IppOperation = IppOperation.SendDocument
        };

        // Act
        Action act = () => mapper.Map<IIppRequestMessage, SendDocumentRequest>(request);

        // Assert
        act.Should().NotThrow();
    }
}
