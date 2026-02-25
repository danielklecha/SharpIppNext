using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Exceptions;
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
    [DataRow(true, true, "Document must be set for non-last document")]
    [DataRow(true, false, "Document must be set for non-last document")]
    [TestMethod]
    public void Map_SendDocumentRequestToIppRequestMessage_InvalidRequest_ThrowsArgumentException(bool isDocumentNull, bool isOperationAttributesNull, string expectedMessage)
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
        act.Should().Throw<ArgumentException>().WithMessage(expectedMessage);
    }

    [TestMethod]
    public void Map_IppRequestMessageToSendDocumentRequest_MissingLastDocumentAttribute_ThrowsIppRequestException()
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
        act.Should().Throw<IppRequestException>().WithMessage("missing last-document");
    }
}
