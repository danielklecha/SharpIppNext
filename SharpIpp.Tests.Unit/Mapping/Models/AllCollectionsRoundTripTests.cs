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
            // 1. NoValue -> IppValue<TCollection> instance
            var ippValType = typeof(IppValue<>).MakeGenericType(type);
            var noValueInst = _mapper!.Map(NoValue.Instance, typeof(NoValue), ippValType);
            noValueInst.Should().NotBeNull();
            var isValProp = ippValType.GetProperty("IsValue")!;
            ((bool)isValProp.GetValue(noValueInst)!).Should().BeFalse($"IppValue<{type.Name}> from NoValue.Instance must have IsValue == false");
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
            var attrs = (List<IppAttribute>)_mapper!.Map(instance, type, typeof(List<IppAttribute>));
            attrs.Should().NotBeNull();

            // List<IppAttribute> -> Model
            var dict = attrs.ToIppDictionary();
            var roundTripped = _mapper.Map(dict, typeof(IDictionary<string, IppAttribute[]>), type);
            roundTripped.Should().NotBeNull();
        }
    }
}
