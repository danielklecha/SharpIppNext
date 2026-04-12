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

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrintJobProfileTest
{
    [DataRow(typeof(PrintJobRequest), typeof(IppRequestMessage))]
    [DataRow(typeof(IppRequestMessage), typeof(PrintJobRequest))]
    [TestMethod]
    public void Map_InvalidRequest_DoesNotThrow(Type sourceType, Type destType)
    {
        // Arrange
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);

        object source = sourceType == typeof(PrintJobRequest) 
            ? new PrintJobRequest { Document = null } 
            : new IppRequestMessage { Document = null };

        // Act
        Action act = () => mapper.Map(source, sourceType, destType);

        // Assert
        act.Should().NotThrow();
    }
}
