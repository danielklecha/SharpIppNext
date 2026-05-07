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
public class DestinationUriProfileTests
{
    private readonly IMapper _mapper;

    public DestinationUriProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_Dictionary_To_DestinationUri_UsesIntegerT33Subaddress()
    {
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "destination-uri", [new IppAttribute(Tag.Uri, "destination-uri", "fax:+12025550123")] },
            { "t33-subaddress", [new IppAttribute(Tag.Integer, "t33-subaddress", 99)] }
        };

        var result = _mapper.Map<DestinationUri>(dict);

        result.DestinationUriValue.Should().Be("fax:+12025550123");
        result.T33Subaddress.Should().Be(99);
    }
}
