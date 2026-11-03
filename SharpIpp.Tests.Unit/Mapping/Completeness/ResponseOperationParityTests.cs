using SharpIpp.Protocol;
using SharpIpp.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Completeness;

/// <summary>
/// Tests verifying that each [IppResponse] model specifies its matching IppOperation.
/// </summary>
[TestClass]
public class ResponseOperationParityTests
{
    private static readonly Assembly SharpIppAssembly = typeof(ISharpIppClient).Assembly;

    [TestMethod]
    public void EveryResponse_HasMatchingDeclaredOperation()
    {
        var responseTypes = SharpIppAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<IppResponseAttribute>() != null)
            .ToList();

        var requestTypesByOp = SharpIppAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<IppRequestAttribute>() != null)
            .ToDictionary(t => t.Name.Replace("Request", ""), t => t.GetCustomAttribute<IppRequestAttribute>()!.Operation);

        var mismatches = new List<string>();

        foreach (var respType in responseTypes)
        {
            var attr = respType.GetCustomAttribute<IppResponseAttribute>()!;
            if (!attr.Operation.HasValue)
            {
                mismatches.Add($"{respType.Name}: missing declared IppOperation");
                continue;
            }

            var baseName = respType.Name.Replace("Response", "");
            if (requestTypesByOp.TryGetValue(baseName, out var expectedOp))
            {
                if (attr.Operation.Value != expectedOp)
                {
                    mismatches.Add($"{respType.Name}: declared operation {attr.Operation.Value} does not match expected {expectedOp}");
                }
            }
        }

        mismatches.Should().BeEmpty(
            $"all [IppResponse] classes must declare matching IppOperation. Mismatches:\n{string.Join("\n", mismatches)}");
    }

    [TestMethod]
    public void EveryResponse_IsRecognizedBy_IppResponseExtensions_GetOperation()
    {
        var responseTypes = SharpIppAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IIppResponse).IsAssignableFrom(t) && t.GetCustomAttribute<IppResponseAttribute>() != null)
            .ToList();

        var mismatches = new List<string>();

        foreach (var respType in responseTypes)
        {
            var attr = respType.GetCustomAttribute<IppResponseAttribute>()!;
            var expectedOp = attr.Operation;

            var instance = (IIppResponse)Activator.CreateInstance(respType)!;
            var actualOp = instance.GetOperation();

            if (actualOp != expectedOp)
            {
                mismatches.Add($"{respType.Name}: ResponseOperations.GetOperation returned {actualOp?.ToString() ?? "null"}, expected {expectedOp?.ToString() ?? "null"}");
            }
        }

        mismatches.Should().BeEmpty(
            $"IppResponseExtensions.GetOperation must resolve every [IppResponse] type. Mismatches:\n{string.Join("\n", mismatches)}");
    }
}

