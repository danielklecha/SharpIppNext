using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles.Responses;

[TestClass]
[ExcludeFromCodeCoverage]
public class GetSystemAttributesResponseProfileTests : MapperTestBase
{

    [TestMethod]
    public void Map_Dictionary_To_SystemStatusAndDescription_ShouldMapSystemTimeSourceConfiguredAsSmartEnum()
    {
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "system-time-source-configured", [new IppAttribute(Tag.Keyword, "system-time-source-configured", "x-vendor-time-source")] }
        };

        var status = _mapper.Map<SystemStatusAttributes>(dict);
        var description = _mapper.Map<SystemDescriptionAttributes>(dict);

        status.SystemTimeSourceConfigured.Should().Be((SystemTimeSourceConfigured)"x-vendor-time-source");
        description.SystemTimeSourceConfigured.Should().Be((SystemTimeSourceConfigured)"x-vendor-time-source");
    }

    [TestMethod]
    public void Map_GetSystemAttributesResponse_To_IppResponseMessage_ShouldSerializeSystemTimeSourceConfiguredAsKeyword()
    {
        var response = new GetSystemAttributesResponse
        {
            RequestId = 1,
            Version = new IppVersion(2, 0),
            StatusCode = IppStatusCode.SuccessfulOk,
            SystemAttributes = new SystemStatusAttributes
            {
                SystemTimeSourceConfigured = (SystemTimeSourceConfigured)"x-vendor-time-source"
            },
            SystemDescriptionAttributes = new SystemDescriptionAttributes
            {
                SystemTimeSourceConfigured = (SystemTimeSourceConfigured)"x-vendor-time-source"
            }
        };

        var message = _mapper.Map<IppResponseMessage>(response);
        var attributes = message.SystemAttributes.SelectMany(x => x).Where(x => x.Name == "system-time-source-configured").ToArray();

        attributes.Should().HaveCount(2);
        attributes.Should().OnlyContain(x => x.Tag == Tag.Keyword && x.Value != null && x.Value.ToString() == "x-vendor-time-source");
    }
}
