using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Requests;

/// <summary>
/// Exhaustive smoke tests verifying that all [IppRequest] classes serialize to
/// and deserialize from IPP request protocol messages cleanly.
/// </summary>
[TestClass]
[ExcludeFromCodeCoverage]
public class AllRequestsSmokeTests : MapperTestBase
{
    private static readonly Assembly SharpIppAssembly = typeof(ISharpIppClient).Assembly;

    private static List<Type> GetAllRequestTypes() =>
        SharpIppAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<IppRequestAttribute>() != null)
            .ToList();

    [TestMethod]
    public void AllRequests_CanSerializeToIIppRequestMessage_PreservingHeaderAndOperation()
    {
        var requestTypes = GetAllRequestTypes();
        requestTypes.Should().NotBeEmpty();

        foreach (var type in requestTypes)
        {
            var reqAttr = type.GetCustomAttribute<IppRequestAttribute>()!;
            var instance = (IIppRequest)Activator.CreateInstance(type)!;
            instance.RequestId = 42;
            instance.Version = new IppVersion(1, 1);

            var msg = (IIppRequestMessage)_mapper.Map(instance, type, typeof(IIppRequestMessage));

            msg.Should().NotBeNull();
            msg.RequestId.Should().Be(42, $"{type.Name} must preserve RequestId");
            msg.Version.Should().Be(new IppVersion(1, 1), $"{type.Name} must preserve Version");
            msg.IppOperation.Should().Be(reqAttr.Operation, $"{type.Name} must set operation {reqAttr.Operation}");
        }
    }

    [TestMethod]
    public void AllRequests_CanSerializeToIppRequestMessage_PreservingHeaderAndOperation()
    {
        var requestTypes = GetAllRequestTypes();
        requestTypes.Should().NotBeEmpty();

        foreach (var type in requestTypes)
        {
            var reqAttr = type.GetCustomAttribute<IppRequestAttribute>()!;
            var instance = (IIppRequest)Activator.CreateInstance(type)!;
            instance.RequestId = 42;
            instance.Version = new IppVersion(1, 1);

            var msg = (IppRequestMessage)_mapper.Map(instance, type, typeof(IppRequestMessage));

            msg.Should().NotBeNull();
            msg.RequestId.Should().Be(42, $"{type.Name} must preserve RequestId");
            msg.Version.Should().Be(new IppVersion(1, 1), $"{type.Name} must preserve Version");
            msg.IppOperation.Should().Be(reqAttr.Operation, $"{type.Name} must set operation {reqAttr.Operation}");
        }
    }

    [TestMethod]
    public void AllRequests_CanDeserializeFromIIppRequestMessage_PreservingHeader()
    {
        var requestTypes = GetAllRequestTypes();

        foreach (var type in requestTypes)
        {
            var msg = new IppRequestMessage
            {
                RequestId = 1234,
                Version = new IppVersion(2, 0),
            };

            var req = (IIppRequest)_mapper.Map(msg, typeof(IIppRequestMessage), type);

            req.Should().NotBeNull();
            req.RequestId.Should().Be(1234, $"{type.Name} must receive RequestId from message");
            req.Version.Should().Be(new IppVersion(2, 0), $"{type.Name} must receive Version from message");
        }
    }

    [TestMethod]
    public void AllRequests_CanDeserializeFromIppRequestMessage_PreservingHeader()
    {
        var requestTypes = GetAllRequestTypes();

        foreach (var type in requestTypes)
        {
            var msg = new IppRequestMessage
            {
                RequestId = 5678,
                Version = new IppVersion(2, 0),
            };

            var req = (IIppRequest)_mapper.Map(msg, typeof(IppRequestMessage), type);

            req.Should().NotBeNull();
            req.RequestId.Should().Be(5678, $"{type.Name} must receive RequestId from message");
            req.Version.Should().Be(new IppVersion(2, 0), $"{type.Name} must receive Version from message");
        }
    }
}

