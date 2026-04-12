using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class NoValueTests
{
    private IMapper? _mapper;

    public static IEnumerable<object[]> IppCollectionTypes =>
        typeof(IIppCollection).Assembly
            .GetTypes()
            .Where(type => typeof(IIppCollection).IsAssignableFrom(type)
                && type.IsClass
                && !type.IsAbstract
                && !type.ContainsGenericParameters
                && SupportsNoValue(type))
            .Select(type => new object[] { type });

    private static bool SupportsNoValue(Type type)
    {
        try
        {
            _ = NoValue.GetNoValue(type);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    [TestInitialize]
    public void TestInitialize()
    {
        var mapper = new SimpleMapper();
        var assembly = Assembly.GetAssembly(typeof(SimpleMapper));
        mapper.FillFromAssembly(assembly!);
        _mapper = mapper;
    }

    [TestMethod]
    [DynamicData(nameof(IppCollectionTypes))]
    public void Map_Dictionary_WithOutOfBandAttribute_ShouldReturnCollectionNoValue(Type collectionType)
    {
        var src = new Dictionary<string, IppAttribute[]>
        {
            ["dummy"] = [ new IppAttribute(Tag.NoValue, "dummy", NoValue.Instance) ]
        };

        var result = _mapper!.Map(src, src.GetType(), collectionType);

        NoValue.IsNoValue(result).Should().BeTrue($"{collectionType.Name} should map out-of-band dictionary to NoValue");
    }

    [TestMethod]
    [DynamicData(nameof(IppCollectionTypes))]
    public void Map_CollectionNoValue_ToNoValueAttribute_ShouldReturnIppNoValue(Type collectionType)
    {
        var source = NoValue.GetNoValue(collectionType);

        var result = _mapper!.Map<IEnumerable<IppAttribute>>(source);

        result.Should().HaveCount(1);
        result.First().Tag.Should().Be(Tag.NoValue);
        result.First().Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void ToString_ShouldReturnNoValueString()
    {
        var noValue = new NoValue();
        noValue.ToString().Should().Be("no value");
    }

    [TestMethod]
    public void Equals_WithNoValue_ShouldReturnTrue()
    {
        var noValue1 = new NoValue();
        var noValue2 = new NoValue();

        noValue1.Equals(noValue2).Should().BeTrue();
    }

    [TestMethod]
    public void Equals_WithObjectOfNoValue_ShouldReturnTrue()
    {
        var noValue = new NoValue();
        object obj = new NoValue();

        noValue.Equals(obj).Should().BeTrue();
    }

    [TestMethod]
    public void Equals_WithDifferentObject_ShouldReturnFalse()
    {
        var noValue = new NoValue();
        object obj = new object();

        noValue.Equals(obj).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_WithNullObject_ShouldReturnFalse()
    {
        var noValue = new NoValue();
        object? obj = null;

        noValue.Equals(obj!).Should().BeFalse();
    }

    [TestMethod]
    public void GetHashCode_ShouldReturnZero()
    {
        var noValue = new NoValue();
        noValue.GetHashCode().Should().Be(0);
    }

    [TestMethod]
    public void Instance_ShouldBeExpected()
    {
        var instance = NoValue.Instance;
        instance.Should().BeOfType<NoValue>();
    }

    private enum TestShortEnum : short { }
    private enum TestIntEnum : int { }

    [TestMethod]
    public void IsNoValue_WithShortEnumMinValue_ShouldReturnTrue()
    {
        var result = NoValue.IsNoValue((TestShortEnum)short.MinValue);
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsNoValue_WithShortEnumNotMinValue_ShouldReturnFalse()
    {
        var result = NoValue.IsNoValue((TestShortEnum)0);
        result.Should().BeFalse();
    }

    [TestMethod]
    public void IsNoValue_WithIntEnumMinValue_ShouldReturnTrue()
    {
        var result = NoValue.IsNoValue((TestIntEnum)int.MinValue);
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsNoValue_WithIntEnumNotMinValue_ShouldReturnFalse()
    {
        var result = NoValue.IsNoValue((TestIntEnum)0);
        result.Should().BeFalse();
    }

    [TestMethod]
    public void IsNoValue_WithStringNoValueString_ShouldReturnTrue()
    {
        var result = NoValue.IsNoValue(NoValue.NoValueString);
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsNoValue_WithNormalString_ShouldReturnFalse()
    {
        var result = NoValue.IsNoValue("Some string");
        result.Should().BeFalse();
    }

    [TestMethod]
    public void IsNoValue_WithDefaultDateTime_ShouldReturnTrue()
    {
        var result = NoValue.IsNoValue(default(DateTime));
        result.Should().BeTrue();
    }

    [TestMethod]
    public void IsNoValue_WithNormalDateTime_ShouldReturnFalse()
    {
        var result = NoValue.IsNoValue(DateTime.Now);
        result.Should().BeFalse();
    }

    [TestMethod]
    public void IsNoValue_WithDefaultDateTimeOffset_ShouldReturnTrue()
    {
        var result = NoValue.IsNoValue(default(DateTimeOffset));
        result.Should().BeTrue();
    }



    [TestMethod]
    public void GetNoValue_WithString_ShouldReturnNoValueString()
    {
        var result = NoValue.GetNoValue<string>();
        result.Should().Be(NoValue.NoValueString);
    }

    [TestMethod]
    public void GetNoValue_WithStringAndKeywordTag_ShouldReturnEmptyString()
    {
        var result = NoValue.GetNoValue<string>(Tag.Keyword);
        result.Should().Be(string.Empty);
    }

    [TestMethod]
    public void GetNoValue_WithStringWithLanguage_ShouldReturnDefaultNew()
    {
        var result = NoValue.GetNoValue<StringWithLanguage>();
        result.Should().Be(new StringWithLanguage());
    }

    [TestMethod]
    public void IsNoValue_WithCollectionNoValue_ShouldReturnTrue()
    {
        var mediaCol = new MediaCol { IsValue = false };
        var result = NoValue.IsNoValue(mediaCol);
        result.Should().BeTrue();
    }

    public static IEnumerable<object[]> SmartEnumData => SmartEnumTests.SmartEnumData;

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void IsNoValue_WithDefaultSmartEnum_ShouldReturnTrue(Type type, string _)
    {
        var result = NoValue.IsNoValue(Activator.CreateInstance(type)!);
        result.Should().BeTrue($"{type.Name} default should be NoValue");
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void IsNoValue_WithPopulatedSmartEnum_ShouldReturnFalse(Type type, string value)
    {
        var result = NoValue.IsNoValue(SmartEnumTests.CreatePopulatedSmartEnum(type, value));
        result.Should().BeFalse($"{type.Name} with value should not be NoValue");
    }

    [TestMethod]
    [DynamicData(nameof(SmartEnumData))]
    public void GetNoValue_WithSmartEnumType_ShouldReturnEmptyValue(Type type, string _)
    {
        var result = NoValue.GetNoValue(type);
        NoValue.IsNoValue(result).Should().BeTrue($"{type.Name} NoValue should be recognized as NoValue");
        result.Should().BeAssignableTo<INoValue>();
        ((INoValue)result).IsValue.Should().BeFalse($"{type.Name} NoValue should have IsValue false");
    }

    [TestMethod]
    [DynamicData(nameof(IppCollectionTypes))]
    public void GetNoValue_WithCollectionType_ShouldReturnIsValueFalse(Type type)
    {
        var result = (IIppCollection)NoValue.GetNoValue(type);
        result.Should().BeAssignableTo(type);
        result.IsValue.Should().BeFalse();
    }
}

