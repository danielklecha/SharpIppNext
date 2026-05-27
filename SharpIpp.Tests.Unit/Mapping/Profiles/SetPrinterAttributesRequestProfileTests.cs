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
                AttributesCharset = (SharpIpp.Protocol.Models.Charset)"utf-8",
                AttributesNaturalLanguage = "en",
                RequestingUserName = "alice",
                DocumentFormat = (SharpIpp.Protocol.Models.DocumentFormat)"application/pdf"
            },
            PrinterAttributes = new PrinterDescriptionAttributes
            {
                PrinterInfo = "Main floor"
            }
        };

        var message = _mapper.Map<SetPrinterAttributesRequest, IppRequestMessage>(request);

        message.IppOperation.Should().Be(IppOperation.SetPrinterAttributes);
        message.OperationAttributes.Should().ContainSingle(x => x.Name == IppAttributeNames.PrinterUri && Equals(x.Value, "ipp://printer/example"));
        message.OperationAttributes.Should().ContainSingle(x => x.Name == IppAttributeNames.DocumentFormat && Equals(x.Value, "application/pdf"));
        message.PrinterAttributes.Should().ContainSingle(x => x.Name == IppAttributeNames.PrinterInfo && Equals(x.Value, "Main floor"));
    }

    [TestMethod]
    public void Map_IppRequestMessage_ToSetPrinterAttributesRequest_RestoresPrinterAttributes()
    {
        var message = new IppRequestMessage
        {
            IppOperation = IppOperation.SetPrinterAttributes
        };
        message.OperationAttributes.Add(new IppAttribute(Tag.Charset, IppAttributeNames.AttributesCharset, "utf-8"));
        message.OperationAttributes.Add(new IppAttribute(Tag.NaturalLanguage, IppAttributeNames.AttributesNaturalLanguage, "en"));
        message.OperationAttributes.Add(new IppAttribute(Tag.Uri, IppAttributeNames.PrinterUri, "ipp://printer/example"));
        message.OperationAttributes.Add(new IppAttribute(Tag.MimeMediaType, IppAttributeNames.DocumentFormat, "application/pdf"));
        message.PrinterAttributes.Add(new IppAttribute(Tag.TextWithoutLanguage, IppAttributeNames.PrinterInfo, "Main floor"));

        var request = _mapper.Map<IIppRequestMessage, SetPrinterAttributesRequest>(message);

        request.PrinterAttributes.Should().NotBeNull();
        request.PrinterAttributes!.PrinterInfo.Should().Be("Main floor");
        request.OperationAttributes.Should().NotBeNull();
        request.OperationAttributes!.PrinterUri.Should().Be(new Uri("ipp://printer/example"));
        ((string?)request.OperationAttributes!.DocumentFormat).Should().Be("application/pdf");
    }
}
