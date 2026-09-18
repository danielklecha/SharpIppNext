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
public class SystemOperationAttributesProfileTests : MapperTestBase
{

[TestMethod]
    public void Map_SystemOperationAttributes_WithNotifyFields_To_Attributes_ShouldEmitNotifyAttributes()
    {
        // Arrange
        var src = new SystemOperationAttributes
        {
            NotifyPrinterIds = new[] { 1, 2 },
            NotifyResourceId = 42,
            RestartGetInterval = 15,
            WhichPrinters = WhichPrinters.All
        };

        // Act
        var attrs = _mapper.Map<List<IppAttribute>>(src);

        // Assert
        attrs.Should().Contain(a => a.Name == "notify-printer-ids" && a.Tag == Tag.Integer);
        attrs.Should().Contain(a => a.Name == "notify-resource-id" && a.Tag == Tag.Integer);
        attrs.Should().Contain(a => a.Name == "restart-get-interval" && a.Tag == Tag.Integer);
        attrs.Should().Contain(a => a.Name == "which-printers" && a.Tag == Tag.Keyword);
    }
}
