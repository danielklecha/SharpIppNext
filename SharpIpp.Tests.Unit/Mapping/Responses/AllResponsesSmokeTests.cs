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

namespace SharpIpp.Tests.Unit.Mapping.Responses;

/// <summary>
/// Exhaustive smoke tests verifying that all [IppResponse] classes serialize to
/// and deserialize from IPP response protocol messages cleanly.
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class AllResponsesSmokeTests : MapperTestBase
{
    private static readonly Assembly SharpIppAssembly = typeof(ISharpIppClient).Assembly;

    private static List<Type> GetAllResponseTypes() =>
        SharpIppAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<IppResponseAttribute>() != null)
            .ToList();

    [TestMethod]
    public void AllResponses_CanSerializeToIppResponseMessage_PreservingHeader()
    {
        var responseTypes = GetAllResponseTypes();
        responseTypes.Should().NotBeEmpty();

        foreach (var type in responseTypes)
        {
            var instance = (IIppResponse)Activator.CreateInstance(type)!;
            instance.RequestId = 88;
            instance.Version = new IppVersion(1, 1);
            instance.StatusCode = IppStatusCode.SuccessfulOk;

            var msg = (IppResponseMessage)_mapper.Map(instance, type, typeof(IppResponseMessage));

            msg.Should().NotBeNull();
            msg.RequestId.Should().Be(88, $"{type.Name} must preserve RequestId");
            msg.Version.Should().Be(new IppVersion(1, 1), $"{type.Name} must preserve Version");
            msg.StatusCode.Should().Be(IppStatusCode.SuccessfulOk, $"{type.Name} must preserve StatusCode");
        }
    }

    [TestMethod]
    public void AllResponses_CanDeserializeFromIppResponseMessage_PreservingHeader()
    {
        var responseTypes = GetAllResponseTypes();

        foreach (var type in responseTypes)
        {
            var msg = new IppResponseMessage
            {
                RequestId = 999,
                Version = new IppVersion(2, 0),
                StatusCode = IppStatusCode.ClientErrorNotFound
            };

            var resp = (IIppResponse)_mapper.Map(msg, typeof(IIppResponseMessage), type);

            resp.Should().NotBeNull();
            resp.RequestId.Should().Be(999, $"{type.Name} must receive RequestId from message");
            resp.Version.Should().Be(new IppVersion(2, 0), $"{type.Name} must receive Version from message");
            resp.StatusCode.Should().Be(IppStatusCode.ClientErrorNotFound, $"{type.Name} must receive StatusCode from message");
        }
    }
}
