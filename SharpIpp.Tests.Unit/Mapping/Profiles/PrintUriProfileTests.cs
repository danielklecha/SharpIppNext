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

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrintUriProfileTests
{
    [DataRow(true, false)]
    [DataRow(false, true)]
    [TestMethod]
    public void Map_PrintUriRequestToIppRequestMessage_InvalidRequest_DoesNotThrow(bool isDocumentUriNull, bool isOperationAttributesNull)
    {
        // Arrange
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        var request = new PrintUriRequest
        {
            OperationAttributes = isOperationAttributesNull ? null! : new PrintUriOperationAttributes
            {
                DocumentUri = isDocumentUriNull ? null! : new Uri("http://localhost")
            }
        };

        // Act
        Action act = () => mapper.Map<PrintUriRequest, IppRequestMessage>(request);

        // Assert
        act.Should().NotThrow();
    }
}
