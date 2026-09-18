using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Completeness;

/// <summary>
/// Reflection-based completeness tests verifying that all annotated requests,
/// responses, models, smart enums, and collections are properly registered in SimpleMapper.
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class RegistryCompletenessTests : MapperTestBase
{
    private static readonly Assembly SharpIppAssembly = typeof(ISharpIppClient).Assembly;

    [TestMethod]
    public void AllAnnotatedRequests_AreRegisteredInSimpleMapper()
    {
        var requestTypes = SharpIppAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<IppRequestAttribute>() != null)
            .ToList();

        requestTypes.Should().NotBeEmpty("requests with [IppRequest] must exist in the assembly");

        var missingMappings = new List<string>();

        foreach (var reqType in requestTypes)
        {
            var reqInstance = Activator.CreateInstance(reqType)!;
            var ippReqMsg = new IppRequestMessage();

            // 1. TRequest -> IppRequestMessage
            try
            {
                var mapped = _mapper.Map(reqInstance, reqType, typeof(IppRequestMessage));
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"{reqType.Name} -> IppRequestMessage: {ex.Message}");
            }

            // 2. TRequest -> IIppRequestMessage
            try
            {
                var mapped = _mapper.Map(reqInstance, reqType, typeof(IIppRequestMessage));
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"{reqType.Name} -> IIppRequestMessage: {ex.Message}");
            }

            // 3. IIppRequestMessage -> TRequest
            try
            {
                var mapped = _mapper.Map(ippReqMsg, typeof(IIppRequestMessage), reqType);
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"IIppRequestMessage -> {reqType.Name}: {ex.Message}");
            }

            // 4. IppRequestMessage -> TRequest
            try
            {
                var mapped = _mapper.Map(ippReqMsg, typeof(IppRequestMessage), reqType);
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"IppRequestMessage -> {reqType.Name}: {ex.Message}");
            }
        }

        missingMappings.Should().BeEmpty(
            $"all [IppRequest] types must have 4 bidirectional maps registered. Failures:\n{string.Join("\n", missingMappings)}");
    }

    [TestMethod]
    public void AllAnnotatedResponses_AreRegisteredInSimpleMapper()
    {
        var responseTypes = SharpIppAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<IppResponseAttribute>() != null)
            .ToList();

        responseTypes.Should().NotBeEmpty("responses with [IppResponse] must exist in the assembly");

        var missingMappings = new List<string>();

        foreach (var respType in responseTypes)
        {
            var respInstance = Activator.CreateInstance(respType)!;
            var ippRespMsg = new IppResponseMessage();

            // 1. TResponse -> IppResponseMessage
            try
            {
                var mapped = _mapper.Map(respInstance, respType, typeof(IppResponseMessage));
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"{respType.Name} -> IppResponseMessage: {ex.Message}");
            }

            // 2. TResponse -> IIppResponseMessage
            try
            {
                var mapped = _mapper.Map(respInstance, respType, typeof(IIppResponseMessage));
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"{respType.Name} -> IIppResponseMessage: {ex.Message}");
            }

            // 3. IIppResponseMessage -> TResponse
            try
            {
                var mapped = _mapper.Map(ippRespMsg, typeof(IIppResponseMessage), respType);
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"IIppResponseMessage -> {respType.Name}: {ex.Message}");
            }

            // 4. IppResponseMessage -> TResponse
            try
            {
                var mapped = _mapper.Map(ippRespMsg, typeof(IppResponseMessage), respType);
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"IppResponseMessage -> {respType.Name}: {ex.Message}");
            }
        }

        missingMappings.Should().BeEmpty(
            $"all [IppResponse] types must have 4 bidirectional maps registered. Failures:\n{string.Join("\n", missingMappings)}");
    }

    [TestMethod]
    public void AllSmartEnums_AreRegisteredInSimpleMapper()
    {
        var smartEnumTypes = SharpIppAssembly.GetTypes()
            .Where(t => t.IsValueType && typeof(ISmartEnum).IsAssignableFrom(t))
            .ToList();

        smartEnumTypes.Should().NotBeEmpty("smart enums must exist in the assembly");

        var missingMappings = new List<string>();

        foreach (var enumType in smartEnumTypes)
        {
            // 1. string -> T
            try
            {
                var mapped = _mapper.Map("sample-val", typeof(string), enumType);
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"string -> {enumType.Name}: {ex.Message}");
            }

            // 2. T -> string
            try
            {
                var enumInstance = _mapper.Map("sample-val", typeof(string), enumType);
                var mapped = _mapper.Map(enumInstance!, enumType, typeof(string));
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"{enumType.Name} -> string: {ex.Message}");
            }

            // 3. NoValue -> T
            try
            {
                var mapped = _mapper.Map(NoValue.Instance, typeof(NoValue), enumType);
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"NoValue -> {enumType.Name}: {ex.Message}");
            }
        }

        missingMappings.Should().BeEmpty(
            $"all smart enums must have string <-> T and NoValue -> T maps. Failures:\n{string.Join("\n", missingMappings)}");
    }

    [TestMethod]
    public void AllCollections_AreRegisteredInSimpleMapper()
    {
        var collectionTypes = SharpIppAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IIppCollection).IsAssignableFrom(t))
            .ToList();

        collectionTypes.Should().NotBeEmpty("collections implementing IIppCollection must exist");

        var missingMappings = new List<string>();

        foreach (var collType in collectionTypes)
        {
            var collInstance = Activator.CreateInstance(collType)!;
            var emptyDict = new Dictionary<string, IppAttribute[]>();

            // 1. IDictionary<string, IppAttribute[]> -> T
            try
            {
                var mapped = _mapper.Map(emptyDict, typeof(IDictionary<string, IppAttribute[]>), collType);
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"IDictionary -> {collType.Name}: {ex.Message}");
            }

            // 2. T -> List<IppAttribute>
            try
            {
                var mapped = _mapper.Map(collInstance, collType, typeof(List<IppAttribute>));
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"{collType.Name} -> List<IppAttribute>: {ex.Message}");
            }

            // 3. NoValue -> T
            try
            {
                var mapped = _mapper.Map(NoValue.Instance, typeof(NoValue), collType);
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"NoValue -> {collType.Name}: {ex.Message}");
            }
        }

        missingMappings.Should().BeEmpty(
            $"all IIppCollection types must have dictionary and NoValue maps registered. Failures:\n{string.Join("\n", missingMappings)}");
    }

    [TestMethod]
    public void AllAnnotatedModels_AreRegisteredInSimpleMapper()
    {
        var modelTypes = SharpIppAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && HasIppAttributeAnnotations(t))
            .ToList();

        modelTypes.Should().NotBeEmpty("models with [IppAttribute] must exist");

        var missingMappings = new List<string>();

        foreach (var modelType in modelTypes)
        {
            var instance = Activator.CreateInstance(modelType)!;
            var emptyDict = new Dictionary<string, IppAttribute[]>();

            // 1. IDictionary<string, IppAttribute[]> -> T
            try
            {
                var mapped = _mapper.Map(emptyDict, typeof(IDictionary<string, IppAttribute[]>), modelType);
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"IDictionary -> {modelType.Name}: {ex.Message}");
            }

            // 2. T -> List<IppAttribute>
            try
            {
                var mapped = _mapper.Map(instance, modelType, typeof(List<IppAttribute>));
                mapped.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                missingMappings.Add($"{modelType.Name} -> List<IppAttribute>: {ex.Message}");
            }
        }

        missingMappings.Should().BeEmpty(
            $"all [IppAttribute] annotated models must have dictionary and list maps. Failures:\n{string.Join("\n", missingMappings)}");
    }

    private static bool HasIppAttributeAnnotations(Type type)
    {
        var curr = type;
        while (curr != null && curr != typeof(object))
        {
            if (curr.GetCustomAttribute<IppAttributeAttribute>() != null)
                return true;

            if (curr.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Any(p => p.GetCustomAttribute<IppAttributeAttribute>() != null))
                return true;

            curr = curr.BaseType;
        }
        return false;
    }
}
