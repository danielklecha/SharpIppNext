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
public class DestinationStatusProfileTests
{
    private readonly IMapper _mapper;

    public DestinationStatusProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_DestinationStatus_To_Attributes_WritesTransmissionStatusEnum()
    {
        var status = new DestinationStatus
        {
            DestinationUri = "fax:+12025550123",
            ImagesCompleted = 2,
            TransmissionStatus = TransmissionStatus.Processing
        };

        var attrs = _mapper.Map<IEnumerable<IppAttribute>>(status).ToList();

        attrs.Should().Contain(x => x.Name == "destination-uri" && x.Tag == Tag.Uri);
        attrs.Should().Contain(x => x.Name == "images-completed" && x.Tag == Tag.Integer && (int)x.Value! == 2);
        attrs.Should().Contain(x => x.Name == "transmission-status" && x.Tag == Tag.Enum && (int)x.Value! == (int)TransmissionStatus.Processing);
    }
}
