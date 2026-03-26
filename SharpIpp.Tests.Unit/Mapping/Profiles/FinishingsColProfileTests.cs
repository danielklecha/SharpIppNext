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

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class FinishingsColProfileTests
{
    private readonly IMapper _mapper;

    public FinishingsColProfileTests()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    public void Map_FinishingsCol_WithOutOfBandCondition_WhenCount1_Length1_IsOutOfBand()
    {
        // Assert: src.Count == 1, first.Length == 1, and Tag == IsOutOfBand (e.g., NoValue)
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "finishings-col", new[] { new IppAttribute(Tag.NoValue, "finishings-col", NoValue.Instance) } }
        };

        var result = _mapper.Map<FinishingsCol>(dict);

        // Should return a FinishingsCol with IsNoValue set to true
        ((IIppCollection)result).IsNoValue.Should().BeTrue();
        // Sub-properties remain at their defaults (null) since only IsNoValue is set.
        result.FinishingTemplate.Should().BeNull();
        result.Stitching.Should().BeNull();
        result.Binding.Should().BeNull();
    }

    [TestMethod]
    public void Map_FinishingsCol_NoValue_ReturnsNoValueTag()
    {
        var finishingsCol = NoValue.GetNoValue<FinishingsCol>();
        var result = _mapper.Map<IEnumerable<IppAttribute>>(finishingsCol).ToList();

        result.Should().HaveCount(1);
        result[0].Tag.Should().Be(Tag.NoValue);
        result[0].Name.Should().Be(JobAttribute.FinishingsCol);
        result[0].Value.Should().Be(NoValue.Instance);
    }

    public void Map_FinishingsCol_FromDictionary_WithComplexMembers_MapsMediaSizeAndFinishingCollections()
    {
        var dict = new Dictionary<string, IppAttribute[]>
        {
            ["media-size"] = _mapper.Map<IEnumerable<IppAttribute>>(new MediaSize { XDimension = 21000, YDimension = 29700 }).ToBegCollection("media-size").ToArray(),
            ["baling"] = _mapper.Map<IEnumerable<IppAttribute>>(new Baling { BalingType = BalingType.Band, BalingWhen = BalingWhen.AfterJob }).ToBegCollection("baling").ToArray(),
            ["coating"] = _mapper.Map<IEnumerable<IppAttribute>>(new Coating { CoatingSides = CoatingSides.Both, CoatingType = CoatingType.Glossy }).ToBegCollection("coating").ToArray(),
            ["covering"] = _mapper.Map<IEnumerable<IppAttribute>>(new Covering { CoveringName = CoveringName.Plain }).ToBegCollection("covering").ToArray(),
            ["folding"] = _mapper.Map<IEnumerable<IppAttribute>>(new Folding { FoldingDirection = FoldingDirection.Inward, FoldingOffset = 100, FoldingReferenceEdge = FinishingReferenceEdge.Top }).ToBegCollection("folding").ToArray(),
            ["laminating"] = _mapper.Map<IEnumerable<IppAttribute>>(new Laminating { LaminatingSides = CoatingSides.Front, LaminatingType = LaminatingType.Archival }).ToBegCollection("laminating").ToArray(),
            ["punching"] = _mapper.Map<IEnumerable<IppAttribute>>(new Punching { PunchingLocations = [50, 100], PunchingOffset = 10, PunchingReferenceEdge = FinishingReferenceEdge.Top }).ToBegCollection("punching").ToArray(),
            ["trimming"] = _mapper.Map<IEnumerable<IppAttribute>>(new Trimming { TrimmingOffset = [5], TrimmingReferenceEdge = FinishingReferenceEdge.Right, TrimmingType = TrimmingType.DrawLine, TrimmingWhen = TrimmingWhen.AfterJob }).ToBegCollection("trimming").ToArray(),
        };

        var result = _mapper.Map<FinishingsCol>(dict);

        result.MediaSize.Should().NotBeNull();
        result.Baling.Should().NotBeNull();
        result.Coating.Should().NotBeNull();
        result.Covering.Should().NotBeNull();
        result.Folding.Should().NotBeNull().And.HaveCount(1);
        result.Laminating.Should().NotBeNull();
        result.Punching.Should().NotBeNull();
        result.Trimming.Should().NotBeNull().And.HaveCount(1);
    }
}
