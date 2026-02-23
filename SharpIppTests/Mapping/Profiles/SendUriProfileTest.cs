using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Mapping.Profiles;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class SendUriProfileTest
{
    [TestMethod]
    public void Map_SendUriRequestToIppRequestMessage_DocumentUriNullAndLastDocumentFalse_ThrowsArgumentException()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        var request = new SendUriRequest
        {
            OperationAttributes = new SendUriOperationAttributes
            {
                DocumentUri = null!,
                LastDocument = false
            }
        };

        // Act
        Action act = () => mapper.Map<SendUriRequest, IppRequestMessage>(request);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage($"{nameof(SendUriOperationAttributes.DocumentUri)} must be set for non-last document");
    }
}
