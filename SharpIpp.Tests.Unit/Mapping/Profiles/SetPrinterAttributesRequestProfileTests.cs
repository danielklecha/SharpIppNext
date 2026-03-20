using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
public class SetPrinterAttributesRequestProfileTests
{
    private SimpleMapper _mapper = null!;

    [TestInitialize]
    public void Setup()
    {
        _mapper = new SimpleMapper();
        _mapper.FillFromAssembly(typeof(SimpleMapper).Assembly);
    }

    [TestMethod]
    public void Map_SetPrinterAttributesRequest_ToIppRequestMessage_AddsPrinterAttributes()
    {
        var request = new SetPrinterAttributesRequest
        {
            OperationAttributes = new SetPrinterAttributesOperationAttributes
            {
                PrinterUri = new Uri("ipp://printer/example"),
                AttributesCharset = "utf-8",
                AttributesNaturalLanguage = "en",
                RequestingUserName = "alice"
            },
            PrinterAttributes = new PrinterDescriptionAttributes
            {
                PrinterInfo = "Main floor"
            }
        };

        var message = _mapper.Map<SetPrinterAttributesRequest, IppRequestMessage>(request);

        message.IppOperation.Should().Be(IppOperation.SetPrinterAttributes);
        message.OperationAttributes.Should().ContainSingle(x => x.Name == JobAttribute.PrinterUri && Equals(x.Value, "ipp://printer/example"));
        message.PrinterAttributes.Should().ContainSingle(x => x.Name == PrinterAttribute.PrinterInfo && Equals(x.Value, "Main floor"));
    }

    [TestMethod]
    public void Map_IppRequestMessage_ToSetPrinterAttributesRequest_RestoresPrinterAttributes()
    {
        var message = new IppRequestMessage
        {
            IppOperation = IppOperation.SetPrinterAttributes
        };
        message.OperationAttributes.Add(new IppAttribute(Tag.Charset, JobAttribute.AttributesCharset, "utf-8"));
        message.OperationAttributes.Add(new IppAttribute(Tag.NaturalLanguage, JobAttribute.AttributesNaturalLanguage, "en"));
        message.OperationAttributes.Add(new IppAttribute(Tag.Uri, JobAttribute.PrinterUri, "ipp://printer/example"));
        message.PrinterAttributes.Add(new IppAttribute(Tag.TextWithoutLanguage, PrinterAttribute.PrinterInfo, "Main floor"));

        var request = _mapper.Map<IIppRequestMessage, SetPrinterAttributesRequest>(message);

        request.PrinterAttributes.Should().NotBeNull();
        request.PrinterAttributes!.PrinterInfo.Should().Be("Main floor");
        request.OperationAttributes.Should().NotBeNull();
        request.OperationAttributes!.PrinterUri.Should().Be(new Uri("ipp://printer/example"));
    }
}
