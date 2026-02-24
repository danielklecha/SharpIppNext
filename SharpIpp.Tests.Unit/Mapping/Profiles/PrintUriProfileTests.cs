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
    [TestMethod]
    public void Map_PrintUriRequestToIppRequestMessage_DocumentUriNull_ThrowsArgumentException()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Using proper setup for PrintUriRequest
        var request = new PrintUriRequest
        {
            OperationAttributes = new PrintUriOperationAttributes
            {
                DocumentUri = null!
            }
        };

        // Act
        Action act = () => mapper.Map<PrintUriRequest, IppRequestMessage>(request);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage($"{nameof(JobAttribute.DocumentUri)} must be set");
    }

    [TestMethod]
    public void Map_PrintUriRequestToIppRequestMessage_OperationAttributesNull_ThrowsArgumentException()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        // Using proper setup for PrintUriRequest
        var request = new PrintUriRequest
        {
            OperationAttributes = null!
        };

        // Act
        Action act = () => mapper.Map<PrintUriRequest, IppRequestMessage>(request);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage($"{nameof(JobAttribute.DocumentUri)} must be set");
    }
}
