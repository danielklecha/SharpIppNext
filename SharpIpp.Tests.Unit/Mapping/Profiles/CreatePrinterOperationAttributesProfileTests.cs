using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Models.Requests;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;


[TestClass]
[ExcludeFromCodeCoverage]
public class CreatePrinterOperationAttributesProfileTests
{
    private readonly IMapper _mapper;

    public CreatePrinterOperationAttributesProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_Dictionary_To_CreatePrinterOperationAttributes_ShouldMapPrinterXriRequestedAsSystemXri()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "system-uri", [new IppAttribute(Tag.Uri, "system-uri", "http://127.0.0.1:631")] },
            { "printer-xri-requested", [
                new IppAttribute(Tag.BegCollection, "printer-xri-requested", NoValue.Instance),
                new IppAttribute(Tag.MemberAttrName, "", "xri-uri"),
                new IppAttribute(Tag.Uri, "", "ipp://example"),
                new IppAttribute(Tag.EndCollection, "", NoValue.Instance)
            ] }
        };

        // Act
        var result = _mapper.Map<CreatePrinterOperationAttributes>(dict);

        // Assert
        result.PrinterXriRequested.Should().NotBeNull();
        result.PrinterXriRequested.Should().ContainSingle();
        result.PrinterXriRequested![0].XriUri.Should().Be(new Uri("ipp://example"));
    }

[TestMethod]
    public void Map_CreatePrinterOperationAttributes_To_Attributes_ShouldEmitPrinterXriRequestedCollection()
    {
        // Arrange
        var src = new CreatePrinterOperationAttributes
        {
            SystemUri = new Uri("http://127.0.0.1:631"),
            PrinterXriRequested = new[] { new SystemXri { XriUri = new Uri("ipp://example") } }
        };

        // Act
        var attrs = _mapper.Map<List<IppAttribute>>(src);

        // Assert
        attrs.Should().Contain(a => a.Name == "printer-xri-requested");
        attrs.Should().Contain(a => a.Tag == Tag.BegCollection && a.Name == "printer-xri-requested");
    }
}
