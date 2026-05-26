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
public class GetPrinterSupportedValuesRequestProfileTests
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
    public void Map_GetPrinterSupportedValuesRequest_ShouldSetCorrectOperation()
    {
        var mapper = CreateMapper();
        var request = new GetPrinterSupportedValuesRequest
        {
            OperationAttributes = new() { PrinterUri = new Uri("ipp://127.0.0.1:631/") }
        };
        var result = mapper.Map<GetPrinterSupportedValuesRequest, IppRequestMessage>(request);
        result.IppOperation.Should().Be(IppOperation.GetPrinterSupportedValues);
    }

    [TestMethod]
    public void Map_IppRequestMessageToGetPrinterSupportedValuesRequest_ShouldNotThrow()
    {
        var mapper = CreateMapper();
        var msg = CreateBaseMessage(IppOperation.GetPrinterSupportedValues);
        Action act = () => mapper.Map<IIppRequestMessage, GetPrinterSupportedValuesRequest>(msg);
        act.Should().NotThrow();
    }
}
