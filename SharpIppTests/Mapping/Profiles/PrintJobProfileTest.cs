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
using SharpIpp.Protocol;

namespace SharpIpp.Tests.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrintJobProfileTest
{
    [TestMethod]
    public void Map_PrintJobRequestToIppRequestMessage_DocumentNull_ThrowsArgumentException()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(TypesProfile));
        mapper.FillFromAssembly(assembly!);

        var request = new PrintJobRequest
        {
            Document = null!
        };

        // Act
        Action act = () => mapper.Map<PrintJobRequest, IppRequestMessage>(request);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Document must be set");
    }

    [TestMethod]
    public void Map_IIppRequestMessageToPrintJobRequest_DocumentNull_ThrowsArgumentException()
    {
        // Arrange
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(TypesProfile));
        mapper.FillFromAssembly(assembly!);

        IIppRequestMessage request = new IppRequestMessage
        {
            Document = null!
        };

        // Act
        Action act = () => mapper.Map<IIppRequestMessage, PrintJobRequest>(request);

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Document must be set");
    }
}
