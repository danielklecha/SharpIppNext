using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Models.Requests;
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;


[TestClass]
[ExcludeFromCodeCoverage]
public class SystemConfiguredPrinterProfileTests : MapperTestBase
{

[TestMethod]
    public void Map_Dictionary_To_SystemConfiguredPrinter_ShouldMapAllFields()
    {
        // Arrange
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "printer-id", [new IppAttribute(Tag.Integer, "printer-id", 42)] },
            { "printer-info", [new IppAttribute(Tag.TextWithoutLanguage, "printer-info", "Test Printer")] },
            { "printer-is-accepting-jobs", [new IppAttribute(Tag.Boolean, "printer-is-accepting-jobs", true)] },
            { "printer-name", [new IppAttribute(Tag.NameWithoutLanguage, "printer-name", "Printer ABC")] },
            { "printer-service-type", [new IppAttribute(Tag.Keyword, "printer-service-type", "print") ] },
            { "printer-state", [new IppAttribute(Tag.Enum, "printer-state", (int)PrinterState.Idle)] },
            { "printer-state-reasons", [new IppAttribute(Tag.Keyword, "printer-state-reasons", "none")] },
            { "printer-xri-supported", [new IppAttribute(Tag.BegCollection, "printer-xri-supported", NoValue.Instance), new IppAttribute(Tag.MemberAttrName, "", "xri-uri"), new IppAttribute(Tag.Uri, "", "ipp://test"), new IppAttribute(Tag.EndCollection, "", NoValue.Instance)] }
        };

        // Act
        var result = _mapper.Map<SystemConfiguredPrinter>(dict);

        // Assert
        result.PrinterId.Should().Be(42);
        result.PrinterInfo.Should().Be("Test Printer");
        result.PrinterIsAcceptingJobs.Should().BeTrue();
        result.PrinterName.Should().Be("Printer ABC");
        result.PrinterServiceType.Should().Be(PrinterServiceType.Print);
        result.PrinterState.Should().Be(PrinterState.Idle);
        result.PrinterStateReasons.Should().Contain(PrinterStateReason.None);
        result.PrinterXriSupported.Should().NotBeNull();
    }

[TestMethod]
    public void Map_SystemConfiguredPrinter_To_Attributes_ShouldContainCollectionName()
    {
        // Arrange
        var src = new SystemConfiguredPrinter
        {
            PrinterId = 42,
            PrinterInfo = "Test Printer",
            PrinterIsAcceptingJobs = true,
            PrinterName = "Printer ABC",
            PrinterServiceType = PrinterServiceType.Print,
            PrinterState = PrinterState.Idle,
            PrinterStateReasons = new[] { PrinterStateReason.None },
            PrinterXriSupported = new[] { new SystemXri { XriUri = new Uri("ipp://test") } }
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(src).ToList();

        // Assert
        result.Should().Contain(a => a.Name == "printer-id");
        result.Should().Contain(a => a.Name == "printer-info");
        result.Should().Contain(a => a.Name == "printer-is-accepting-jobs");
        result.Should().Contain(a => a.Name == "printer-name");
        result.Should().Contain(a => a.Name == "printer-service-type");
        result.Should().Contain(a => a.Name == "printer-state");
        result.Should().Contain(a => a.Name == "printer-state-reasons");
        result.Should().Contain(a => a.Name == "printer-xri-supported");
    }
}
