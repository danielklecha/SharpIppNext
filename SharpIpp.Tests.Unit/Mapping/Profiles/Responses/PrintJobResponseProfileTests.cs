using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Models.Responses;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles.Responses;

[TestClass]
[ExcludeFromCodeCoverage]
public class PrintJobResponseProfileTests
{
    private readonly IMapper _mapper;

    public PrintJobResponseProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_IppResponseMessageToPrintJobResponse_SetsDocumentAttributesWhenPresent()
    {
        // Arrange
        var src = new IppResponseMessage
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk,
            DocumentAttributes = { new List<IppAttribute> { new IppAttribute(Tag.Integer, DocumentAttribute.DocumentNumber, 1) } }
        };

        // Act
        var dst = _mapper.Map<PrintJobResponse>(src);

        // Assert
        dst.DocumentAttributes.Should().NotBeNull();
        dst.DocumentAttributes!.DocumentNumber.Should().Be(1);
    }

    [TestMethod]
    public void Map_IppResponseMessageToPrintJobResponse_SetsDocumentAttributesToNullWhenAbsent()
    {
        // Arrange
        var src = new IppResponseMessage
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk
        };

        // Act
        var dst = _mapper.Map<PrintJobResponse>(src);

        // Assert
        dst.DocumentAttributes.Should().BeNull();
    }

    [TestMethod]
    public void Map_PrintJobResponseToIppResponseMessage_SetsDocumentAttributesWhenPresent()
    {
        // Arrange
        var src = new PrintJobResponse
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk,
            DocumentAttributes = new DocumentAttributes { DocumentNumber = 1 }
        };

        // Act
        var dst = _mapper.Map<IppResponseMessage>(src);

        // Assert
        dst.DocumentAttributes.Should().NotBeEmpty();
        dst.DocumentAttributes.First().Any(a => a.Name == DocumentAttribute.DocumentNumber && (int)a.Value == 1).Should().BeTrue();
    }

    [TestMethod]
    public void Map_PrintJobResponseToIppResponseMessage_SetsDocumentAttributesToEmptyWhenNull()
    {
        // Arrange
        var src = new PrintJobResponse
        {
            Version = new IppVersion(1, 1),
            RequestId = 42,
            StatusCode = IppStatusCode.SuccessfulOk,
            DocumentAttributes = null
        };

        // Act
        var dst = _mapper.Map<IppResponseMessage>(src);

        // Assert
        dst.DocumentAttributes.Should().BeEmpty();
    }
}
