using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class DeleteDocumentRequestProfileTests
{
    private static SimpleMapper CreateMapper()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        return mapper;
    }

    private static IppRequestMessage CreateBaseMessage(IppOperation operation)
    {
        var msg = new IppRequestMessage { IppOperation = operation };
        msg.OperationAttributes.Add(new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"));
        msg.OperationAttributes.Add(new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"));
        msg.OperationAttributes.Add(new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://127.0.0.1:631/"));
        return msg;
    }

    [TestMethod]
    public void Map_DeleteDocumentRequest_ShouldSetCorrectOperation()
    {
        var mapper = CreateMapper();
        var request = new DeleteDocumentRequest
        {
            OperationAttributes = new() { PrinterUri = new Uri("ipp://127.0.0.1:631/"), JobId = 1, DocumentNumber = 1 }
        };
        var result = mapper.Map<DeleteDocumentRequest, IppRequestMessage>(request);
        result.IppOperation.Should().Be(IppOperation.DeleteDocument);
    }

    [TestMethod]
    public void Map_IppRequestMessageToDeleteDocumentRequest_ShouldNotThrow()
    {
        var mapper = CreateMapper();
        var msg = CreateBaseMessage(IppOperation.DeleteDocument);
        Action act = () => mapper.Map<IIppRequestMessage, DeleteDocumentRequest>(msg);
        act.Should().NotThrow();
    }

    [TestMethod]
    public void Map_DeleteDocumentRequest_DocumentNumber_ShouldRoundTrip()
    {
        var mapper = CreateMapper();
        var request = new DeleteDocumentRequest
        {
            OperationAttributes = new() { PrinterUri = new Uri("ipp://127.0.0.1:631/"), JobId = 5, DocumentNumber = 3 }
        };
        var ippMsg = mapper.Map<DeleteDocumentRequest, IppRequestMessage>(request);
        var roundTripped = mapper.Map<IIppRequestMessage, DeleteDocumentRequest>(ippMsg);
        roundTripped.OperationAttributes!.DocumentNumber.Should().Be(3);
    }
}
