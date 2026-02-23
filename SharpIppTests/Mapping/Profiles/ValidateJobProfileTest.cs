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

namespace SharpIpp.Tests.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class ValidateJobProfileTest
{
    [TestMethod]
    public void Map_ValidateJobRequestToIppRequestMessage_DocumentNull_ThrowsArgumentException()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        var request = new ValidateJobRequest
        {
            Document = null!
        };

        // Act
        Action act = () => mapper.Map<ValidateJobRequest, IppRequestMessage>(request);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Document must be set");
    }

    [TestMethod]
    public void Map_IppRequestMessageToValidateJobRequest_DocumentNull_ThrowsArgumentException()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        IIppRequestMessage request = new IppRequestMessage
        {
            Document = null!
        };

        // Act
        Action act = () => mapper.Map<IIppRequestMessage, ValidateJobRequest>(request);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Document must be set");
    }
}
