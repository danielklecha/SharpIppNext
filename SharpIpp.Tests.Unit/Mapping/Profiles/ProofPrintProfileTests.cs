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
public class ProofPrintProfileTests
{
    private readonly IMapper _mapper;

    public ProofPrintProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

[TestMethod]
    public void Map_Dictionary_To_ProofPrint_Should_Map_MediaCol()
    {
        // Arrange
        var mediaCol = new MediaCol { MediaLeftMargin = 5 };
        var mediaColCollection = _mapper.Map<IEnumerable<IppAttribute>>(mediaCol).ToBegCollection("media-col").ToArray();
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "media-col", mediaColCollection }
        };

        // Act
        var result = _mapper.Map<ProofPrint>(dict);

        // Assert
        result.MediaCol.Should().NotBeNull();
        result.MediaCol!.MediaLeftMargin.Should().Be(5);
    }

[TestMethod]
    public void Map_ProofPrint_To_Attributes_Should_Include_MediaCol()
    {
        // Arrange
        var proofPrint = new ProofPrint
        {
            MediaCol = new MediaCol { MediaBottomMargin = 7 }
        };

        // Act
        var result = _mapper.Map<IEnumerable<IppAttribute>>(proofPrint).ToList();

        // Assert
        result.Should().Contain(a => a.Name == "media-col" && a.Tag == Tag.BegCollection);
        result.Should().Contain(a => a.Tag == Tag.MemberAttrName && a.Value!.Equals("media-bottom-margin"));
        result.Should().Contain(a => a.Tag == Tag.Integer && a.Value!.Equals(7));
    }
}
