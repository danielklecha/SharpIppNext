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
public class PowerLogEntryProfileTests : MapperTestBase
{

[TestMethod]
    public void Map_Dictionary_To_PowerLogEntry_ShouldMapPowerStateDateTime()
    {
        // Arrange
        var dateTime = new DateTimeOffset(2026, 3, 29, 9, 0, 0, TimeSpan.Zero);
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "log-id", [new IppAttribute(Tag.Integer, "log-id", 301)] },
            { "power-state", [new IppAttribute(Tag.Keyword, "power-state", PowerState.On.Value)] },
            { "power-state-date-time", [new IppAttribute(Tag.DateTime, "power-state-date-time", dateTime)] },
            { "power-state-message", [new IppAttribute(Tag.TextWithoutLanguage, "power-state-message", "power-log-entry")] }
        };

        // Act
        var result = _mapper.Map<PowerLogEntry>(dict);

        // Assert
        result.LogId.Should().Be(301);
        result.PowerState.Should().Be(PowerState.On);
        result.PowerStateDateTime.Should().Be(dateTime);
        result.PowerStateMessage.Should().Be("power-log-entry");
    }

[TestMethod]
    public void Map_PowerLogEntry_To_Attributes_ShouldEmitPowerStateDateTime()
    {
        // Arrange
        var dateTime = new DateTimeOffset(2026, 3, 29, 9, 0, 0, TimeSpan.Zero);
        var src = new PowerLogEntry
        {
            LogId = 301,
            PowerState = PowerState.On,
            PowerStateDateTime = dateTime,
            PowerStateMessage = "power-log-entry"
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(src).ToList();

        // Assert
        result.Should().Contain(a => a.Name == "power-state-date-time" && a.Tag == Tag.DateTime && Equals(a.Value, dateTime));
        result.Should().NotContain(a => a.Name == "date-time-at");
    }

[TestMethod]
    public void Map_Dictionary_To_PowerLogEntry_WithLegacyDateTimeAt_ShouldIgnoreLegacyField()
    {
        // Arrange
        var dateTime = new DateTimeOffset(2026, 3, 29, 9, 0, 0, TimeSpan.Zero);
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "log-id", [new IppAttribute(Tag.Integer, "log-id", 301)] },
            { "power-state", [new IppAttribute(Tag.Keyword, "power-state", PowerState.On.Value)] },
            { "date-time-at", [new IppAttribute(Tag.DateTime, "date-time-at", dateTime)] },
            { "power-state-message", [new IppAttribute(Tag.TextWithoutLanguage, "power-state-message", "power-log-entry")] }
        };

        // Act
        var result = _mapper.Map<PowerLogEntry>(dict);

        // Assert
        result.PowerStateDateTime.Should().BeNull();
    }
}
