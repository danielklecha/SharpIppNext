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
/// Tests verifying that each [IppRequest] model sets the exact IppOperation
/// defined in its attribute when mapped to IppRequestMessage.
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class RequestOperationParityTests : MapperTestBase
{
    private static readonly Assembly SharpIppAssembly = typeof(ISharpIppClient).Assembly;

    [TestMethod]
    public void EveryRequest_SetsDeclaredOperation_WhenMappedToIppRequestMessage()
    {
        var requestTypes = SharpIppAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<IppRequestAttribute>() != null)
            .ToList();

        var mismatches = new List<string>();

        foreach (var reqType in requestTypes)
        {
            var attr = reqType.GetCustomAttribute<IppRequestAttribute>()!;
            var expectedOp = attr.Operation;

            var instance = Activator.CreateInstance(reqType)!;
            var mapped = _mapper.Map<IppRequestMessage>(instance);

            if (mapped.IppOperation != expectedOp)
            {
                mismatches.Add($"{reqType.Name}: expected operation {expectedOp} ({(int)expectedOp}) but got {mapped.IppOperation} ({(int)mapped.IppOperation})");
            }
        }

        mismatches.Should().BeEmpty(
            $"all [IppRequest] classes must serialize to their declared IppOperation. Mismatches:\n{string.Join("\n", mismatches)}");
    }
}

