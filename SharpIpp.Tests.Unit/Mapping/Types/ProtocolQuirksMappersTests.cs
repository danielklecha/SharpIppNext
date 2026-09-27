using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;
using Range = SharpIpp.Protocol.Models.Range;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class ProtocolQuirksMappersTests : MapperTestBase
{
    [TestMethod]
    public void ProtocolQuirksMappers_RegistersAndExecutesLambdas()
    {
        var mockMapper = new Mock<IMapperConstructor>();
        Func<Range, IMapperApplier, bool>? rangeToBool = null;
        Func<Range, IMapperApplier, IppValue<bool>>? rangeToIppValueBool = null;
        Func<NoValue, IMapperApplier, bool>? noValueToBool = null;

        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<Range, IMapperApplier, bool>>()))
            .Callback<Func<Range, IMapperApplier, bool>>(f => rangeToBool = f);
        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<Range, IMapperApplier, IppValue<bool>>>()))
            .Callback<Func<Range, IMapperApplier, IppValue<bool>>>(f => rangeToIppValueBool = f);
        mockMapper.Setup(x => x.CreateMap(It.IsAny<Func<NoValue, IMapperApplier, bool>>()))
            .Callback<Func<NoValue, IMapperApplier, bool>>(f => noValueToBool = f);

        var mapperType = typeof(SimpleMapper).Assembly.GetType("SharpIpp.Mapping.ProtocolQuirksMappers");
        var configureMethod = mapperType!.GetMethod("Configure", BindingFlags.Public | BindingFlags.Static);
        configureMethod!.Invoke(null, new object[] { mockMapper.Object });

        rangeToBool.Should().NotBeNull();
        rangeToBool!(new Range(1, 10), Mock.Of<IMapperApplier>()).Should().BeTrue();

        rangeToIppValueBool.Should().NotBeNull();
        var ippVal = rangeToIppValueBool!(new Range(1, 10), Mock.Of<IMapperApplier>());
        ippVal.IsValue.Should().BeTrue();
        ippVal.Value.Should().BeTrue();

        noValueToBool.Should().NotBeNull();
        noValueToBool!(NoValue.Instance, Mock.Of<IMapperApplier>()).Should().BeFalse();
    }

    [TestMethod]
    public void Map_RangeToBool_MapsToTrue()
    {
        var result = _mapper.Map<Range, bool>(new Range(1, 10));
        result.Should().BeTrue();
    }

    [TestMethod]
    public void Map_RangeToIppValueBool_MapsToTrue()
    {
        var result = _mapper.Map<Range, IppValue<bool>>(new Range(1, 10));
        result.IsValue.Should().BeTrue();
        result.Value.Should().BeTrue();
    }

    [TestMethod]
    public void Map_NoValueToBool_MapsToFalse()
    {
        var result = _mapper.Map<NoValue, bool>(NoValue.Instance);
        result.Should().BeFalse();
    }
}