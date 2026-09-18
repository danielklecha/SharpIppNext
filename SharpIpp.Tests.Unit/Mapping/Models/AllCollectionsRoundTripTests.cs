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

namespace SharpIpp.Tests.Unit.Mapping.Models;

/// <summary>
/// Reflection-based tests covering all IIppCollection types for NoValue handling,
/// serialization to BegCollection attributes, and deserialization.
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class AllCollectionsRoundTripTests : MapperTestBase
{
    private static readonly Assembly SharpIppAssembly = typeof(ISharpIppClient).Assembly;

    private static List<Type> GetAllCollectionTypes() =>
        SharpIppAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IIppCollection).IsAssignableFrom(t))
            .ToList();

    [TestMethod]
    public void AllCollections_NoValue_RoundTripsCorrectly()
    {
        var collectionTypes = GetAllCollectionTypes();
        collectionTypes.Should().NotBeEmpty();

        foreach (var type in collectionTypes)
        {
            // 1. NoValue -> Collection instance
            var noValueInst = _mapper.Map(NoValue.Instance, typeof(NoValue), type);
            noValueInst.Should().NotBeNull();
            NoValue.IsNoValue(noValueInst).Should().BeTrue($"NoValue.IsNoValue must return true for {type.Name}");

            // 2. Collection instance with NoValue -> List<IppAttribute>
            var attrs = (List<IppAttribute>)_mapper.Map(noValueInst, type, typeof(List<IppAttribute>));
            attrs.Should().NotBeNull();
            attrs.Should().HaveCount(1, $"{type.Name} with NoValue must serialize to exactly 1 IppAttribute");
            attrs[0].Tag.Should().Be(Tag.NoValue, $"{type.Name} attribute must have Tag.NoValue");

            // 3. Round-trip back from out-of-band NoValue dictionary -> Collection
            var dict = attrs.ToIppDictionary();
            var restored = _mapper.Map(dict, typeof(IDictionary<string, IppAttribute[]>), type);
            restored.Should().NotBeNull();
            NoValue.IsNoValue(restored).Should().BeTrue($"Restored {type.Name} must retain NoValue state");
        }
    }

    [TestMethod]
    public void AllCollections_EmptyInstance_SerializesAndDeserializesWithoutError()
    {
        var collectionTypes = GetAllCollectionTypes();

        foreach (var type in collectionTypes)
        {
            var instance = Activator.CreateInstance(type)!;

            // Model -> List<IppAttribute>
            var attrs = (List<IppAttribute>)_mapper.Map(instance, type, typeof(List<IppAttribute>));
            attrs.Should().NotBeNull();

            // List<IppAttribute> -> Model
            var dict = attrs.ToIppDictionary();
            var roundTripped = _mapper.Map(dict, typeof(IDictionary<string, IppAttribute[]>), type);
            roundTripped.Should().NotBeNull();
        }
    }
}

