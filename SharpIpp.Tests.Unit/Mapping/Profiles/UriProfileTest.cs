using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class UriProfileTest
{
    [TestMethod]
    public void Map_StringToUri_ValidUri_ReturnsUri()
    {
        var mapper = new SimpleMapper();
        mapper.FillFromAssembly(Assembly.GetAssembly(typeof(SimpleMapper))!);

        var uri = mapper.Map<string, Uri?>("http://example.com");

        uri.Should().NotBeNull();
        uri!.AbsoluteUri.Should().Be("http://example.com/");
    }

    [TestMethod]
    public void Map_StringToUri_InvalidUri_ReturnsNull()
    {
        var mapper = new SimpleMapper();
        mapper.FillFromAssembly(Assembly.GetAssembly(typeof(SimpleMapper))!);

        var uri = mapper.MapNullable<string, Uri?>("http://::invalid");

        uri.Should().BeNull();
    }

    [TestMethod]
    public void Map_UriToString_ReturnsString()
    {
        var mapper = new SimpleMapper();
        mapper.FillFromAssembly(Assembly.GetAssembly(typeof(SimpleMapper))!);

        var str = mapper.Map<Uri, string>(new Uri("http://example.com"));

        str.Should().Be("http://example.com/");
    }

    [TestMethod]
    public void Map_NoValueToUri_ReturnsNull()
    {
        var mapper = new SimpleMapper();
        mapper.FillFromAssembly(Assembly.GetAssembly(typeof(SimpleMapper))!);

        var uri = mapper.MapNullable<SharpIpp.Protocol.Models.NoValue, Uri?>(new SharpIpp.Protocol.Models.NoValue());

        uri.Should().BeNull();
    }
}
