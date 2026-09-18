using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;
using SharpIpp.Tests.Unit.Mapping;
using Range = SharpIpp.Protocol.Models.Range;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class NoValueTests : MapperTestBase
{
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
    public void GetNoValue_WithIppVersion_ShouldReturnDefault()
    {
        var result = NoValue.GetNoValue<IppVersion>();
        result.Should().Be(default(IppVersion));
        result.IsValue.Should().BeFalse();
        NoValue.IsNoValue(result).Should().BeTrue();
    }

    [TestMethod]
    public void IsNoValue_WithDefaultIppVersion_ShouldReturnTrue()
    {
        NoValue.IsNoValue(default(IppVersion)).Should().BeTrue();
    }

    [TestMethod]
    public void IsNoValue_WithConstructedIppVersion_ShouldReturnFalse()
    {
        NoValue.IsNoValue(new IppVersion()).Should().BeFalse();
        NoValue.IsNoValue(new IppVersion(1, 1)).Should().BeFalse();
        NoValue.IsNoValue(new IppVersion("1.1")).Should().BeFalse();
    }

    [TestMethod]
    public void IsNoValue_WithCollectionNoValue_ShouldReturnTrue()
    {
        var mediaCol = NoValue.GetNoValue<MediaCol>();
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

    public static IEnumerable<object[]> IppStructuredStringTypes =>
        typeof(IppStructuredString).Assembly
            .GetTypes()
            .Where(type => typeof(IppStructuredString).IsAssignableFrom(type)
                && type.IsClass
                && !type.IsAbstract
                && !type.ContainsGenericParameters)
            .Select(type => new object[] { type });

    [TestMethod]
    [DynamicData(nameof(IppStructuredStringTypes))]
    public void GetNoValue_WithIppStructuredStringType_ShouldReturnIsValueFalse(Type type)
    {
        var result = (IIppStructuredString)NoValue.GetNoValue(type);
        result.Should().BeAssignableTo(type);
        result.IsValue.Should().BeFalse();
        NoValue.IsNoValue(result).Should().BeTrue();
    }

    [TestMethod]
    public void GetNoValue_WithUnsupportedType_ShouldThrowArgumentException()
    {
        var action = () => NoValue.GetNoValue(typeof(object));
        action.Should().Throw<ArgumentException>()
            .WithMessage($"Type {typeof(object)} is not supported for NoValue mapping and has no non-null default value");
    }

    [TestMethod]
    public void GetNoValue_GenericWithUnsupportedType_ShouldThrowArgumentException()
    {
        var action = () => NoValue.GetNoValue<double>();
        action.Should().Throw<ArgumentException>()
            .WithMessage($"Type {typeof(double)} is not supported for NoValue mapping and has no non-null default value");
    }

    [TestMethod]
    public void GetNoValue_WithUnsupportedNullableType_ShouldThrowArgumentException()
    {
        var action = () => NoValue.GetNoValue(typeof(double?));
        action.Should().Throw<ArgumentException>()
            .WithMessage($"Type {typeof(double?)} is not supported for NoValue mapping and has no non-null default value");
    }

    [TestMethod]
    public void GetNoValue_WithInt_ShouldReturnIntMinValue()
    {
        var result = NoValue.GetNoValue<int>();
        result.Should().Be(int.MinValue);
    }

    [TestMethod]
    public void GetNoValue_WithNullableInt_ShouldReturnIntMinValue()
    {
        var result = NoValue.GetNoValue(typeof(int?));
        result.Should().Be(int.MinValue);
    }

    [TestMethod]
    public void GetNoValue_WithShortEnum_ShouldReturnShortMinValue()
    {
        var result = NoValue.GetNoValue<TestShortEnum>();
        result.Should().Be((TestShortEnum)short.MinValue);
    }

    [TestMethod]
    public void GetNoValue_WithIntEnum_ShouldReturnIntMinValue()
    {
        var result = NoValue.GetNoValue<TestIntEnum>();
        result.Should().Be((TestIntEnum)int.MinValue);
    }

    [TestMethod]
    public void GetNoValue_WithDateTime_ShouldReturnDateTimeMinValue()
    {
        var result = NoValue.GetNoValue<DateTime>();
        result.Should().Be(DateTime.MinValue);
    }

    [TestMethod]
    public void GetNoValue_WithDateTimeOffset_ShouldReturnDateTimeOffsetMinValue()
    {
        var result = NoValue.GetNoValue<DateTimeOffset>();
        result.Should().Be(DateTimeOffset.MinValue);
    }

    [TestMethod]
    public void GetNoValue_WithBool_ShouldReturnFalse()
    {
        var result = NoValue.GetNoValue<bool>();
        result.Should().BeFalse();
    }

    [TestMethod]
    public void GetNoValue_WithRange_ShouldReturnNoValueRange()
    {
        var result = NoValue.GetNoValue<Range>();
        result.IsValue.Should().BeFalse();
    }

    [TestMethod]
    public void GetNoValue_WithResolution_ShouldReturnNoValueResolution()
    {
        var result = NoValue.GetNoValue<Resolution>();
        result.IsValue.Should().BeFalse();
    }

    [TestMethod]
    public void GetNoValue_WithOctetString_ShouldReturnNoValueOctetString()
    {
        var result = NoValue.GetNoValue<OctetString>();
        result.IsValue.Should().BeFalse();
    }

    [TestMethod]
    public void IsNoValue_WithSingleNoValueArray_ShouldReturnTrue()
    {
        NoValue.IsNoValue(new object[] { NoValue.Instance }).Should().BeTrue();
        NoValue.IsNoValue(new NoValue[] { NoValue.Instance }).Should().BeTrue();
    }

    [TestMethod]
    public void IsNoValue_WithNonNoValueArray_ShouldReturnFalse()
    {
        NoValue.IsNoValue(new object[] { "test" }).Should().BeFalse();
        NoValue.IsNoValue(new object[] { NoValue.Instance, NoValue.Instance }).Should().BeFalse();
        NoValue.IsNoValue(Array.Empty<object>()).Should().BeFalse();
    }

    [TestMethod]
    public void Operator_EqualityAndInequality_BetweenNoValues_ShouldWork()
    {
        var a = NoValue.Instance;
        var b = new NoValue();
        (a == b).Should().BeTrue();
        (a != b).Should().BeFalse();
    }

    [TestMethod]
    public void GetNoValue_Parameterless_ShouldReturnInstance()
    {
        var result = NoValue.GetNoValue();
        result.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void ImplicitConversion_ToInt_ShouldReturnIntMinValue()
    {
        int value = NoValue.Instance;
        value.Should().Be(int.MinValue);

        int fromGetNoValue = NoValue.GetNoValue();
        fromGetNoValue.Should().Be(int.MinValue);

        (value == NoValue.Instance).Should().BeTrue();
        (NoValue.Instance == value).Should().BeTrue();
        (value != NoValue.Instance).Should().BeFalse();
        (NoValue.Instance != value).Should().BeFalse();

        (5 == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == 5).Should().BeFalse();
        (5 != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance != 5).Should().BeTrue();

        int? nullableVal = NoValue.Instance;
        nullableVal.Should().Be(int.MinValue);
        (nullableVal == NoValue.Instance).Should().BeTrue();
        (NoValue.Instance == nullableVal).Should().BeTrue();
        (nullableVal != NoValue.Instance).Should().BeFalse();
        (NoValue.Instance != nullableVal).Should().BeFalse();

        int? nullVal = null;
        (nullVal == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == nullVal).Should().BeFalse();
        (nullVal != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance != nullVal).Should().BeTrue();
    }

    [TestMethod]
    public void ImplicitConversion_ToString_ShouldReturnNoValueString()
    {
        string value = NoValue.Instance;
        value.Should().Be(NoValue.NoValueString);

        (value == NoValue.Instance).Should().BeTrue();
        (NoValue.Instance == value).Should().BeTrue();
        (value != NoValue.Instance).Should().BeFalse();
        (NoValue.Instance != value).Should().BeFalse();

        ("test" == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == "test").Should().BeFalse();
        ("test" != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance != "test").Should().BeTrue();

        string? nullStr = null;
        (nullStr == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == nullStr).Should().BeFalse();
        (nullStr != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance != nullStr).Should().BeTrue();
    }

    [TestMethod]
    public void ImplicitConversion_ToDateTime_ShouldReturnDateTimeMinValue()
    {
        DateTime value = NoValue.Instance;
        value.Should().Be(DateTime.MinValue);

        (value == NoValue.Instance).Should().BeTrue();
        (NoValue.Instance == value).Should().BeTrue();
        (value != NoValue.Instance).Should().BeFalse();
        (NoValue.Instance != value).Should().BeFalse();

        (DateTime.Now == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == DateTime.Now).Should().BeFalse();
        (DateTime.Now != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance != DateTime.Now).Should().BeTrue();

        DateTime? nullableVal = NoValue.Instance;
        (nullableVal == NoValue.Instance).Should().BeTrue();
        (NoValue.Instance == nullableVal).Should().BeTrue();
        (nullableVal != NoValue.Instance).Should().BeFalse();
        (NoValue.Instance != nullableVal).Should().BeFalse();

        DateTime? nullVal = null;
        (nullVal == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == nullVal).Should().BeFalse();
        (nullVal != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance != nullVal).Should().BeTrue();
    }

    [TestMethod]
    public void ImplicitConversion_ToDateTimeOffset_ShouldReturnDateTimeOffsetMinValue()
    {
        DateTimeOffset value = NoValue.Instance;
        value.Should().Be(DateTimeOffset.MinValue);

        (value == NoValue.Instance).Should().BeTrue();
        (NoValue.Instance == value).Should().BeTrue();
        (value != NoValue.Instance).Should().BeFalse();
        (NoValue.Instance != value).Should().BeFalse();

        (DateTimeOffset.Now == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == DateTimeOffset.Now).Should().BeFalse();
        (DateTimeOffset.Now != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance != DateTimeOffset.Now).Should().BeTrue();

        DateTimeOffset? nullableVal = NoValue.Instance;
        (nullableVal == NoValue.Instance).Should().BeTrue();
        (NoValue.Instance == nullableVal).Should().BeTrue();
        (nullableVal != NoValue.Instance).Should().BeFalse();
        (NoValue.Instance != nullableVal).Should().BeFalse();

        DateTimeOffset? nullVal = null;
        (nullVal == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == nullVal).Should().BeFalse();
        (nullVal != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance != nullVal).Should().BeTrue();
    }

    [TestMethod]
    public void ImplicitConversion_ToRange_ShouldReturnNoValueRange()
    {
        Range value = NoValue.Instance;
        value.IsValue.Should().BeFalse();

        (value == NoValue.Instance).Should().BeTrue();
        (NoValue.Instance == value).Should().BeTrue();
        (value != NoValue.Instance).Should().BeFalse();
        (NoValue.Instance != value).Should().BeFalse();

        var normalRange = new Range(1, 10);
        (normalRange == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == normalRange).Should().BeFalse();
        (normalRange != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance != normalRange).Should().BeTrue();

        // Edge case: Range(0, 0) has same Lower and Upper as new Range(), but has IsValue = true
        var zeroRange = new Range(0, 0);
        zeroRange.IsValue.Should().BeTrue();
        (zeroRange == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == zeroRange).Should().BeFalse();
    }

    [TestMethod]
    public void ImplicitConversion_ToResolution_ShouldReturnNoValueResolution()
    {
        Resolution value = NoValue.Instance;
        value.IsValue.Should().BeFalse();

        (value == NoValue.Instance).Should().BeTrue();
        (NoValue.Instance == value).Should().BeTrue();
        (value != NoValue.Instance).Should().BeFalse();
        (NoValue.Instance != value).Should().BeFalse();

        var normalRes = new Resolution(300, 300, ResolutionUnit.DotsPerInch);
        (normalRes == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == normalRes).Should().BeFalse();
        (normalRes != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance != normalRes).Should().BeTrue();
    }

    [TestMethod]
    public void ImplicitConversion_ToOctetString_ShouldReturnNoValueOctetString()
    {
        OctetString value = NoValue.Instance;
        value.IsValue.Should().BeFalse();

        (value == NoValue.Instance).Should().BeTrue();
        (NoValue.Instance == value).Should().BeTrue();
        (value != NoValue.Instance).Should().BeFalse();
        (NoValue.Instance != value).Should().BeFalse();

        var normal = new OctetString("hello");
        (normal == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == normal).Should().BeFalse();
        (normal != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance != normal).Should().BeTrue();
    }

    [TestMethod]
    public void ImplicitConversion_ToStringWithLanguage_ShouldReturnNoValueStringWithLanguage()
    {
        StringWithLanguage value = NoValue.Instance;
        value.IsValue.Should().BeFalse();

        (value == NoValue.Instance).Should().BeTrue();
        (NoValue.Instance == value).Should().BeTrue();
        (value != NoValue.Instance).Should().BeFalse();
        (NoValue.Instance != value).Should().BeFalse();

        var normal = new StringWithLanguage("en", "hello");
        (normal == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == normal).Should().BeFalse();
        (normal != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance != normal).Should().BeTrue();
    }

    [TestMethod]
    public void ImplicitConversion_ToIppVersion_ShouldReturnNoValueIppVersion()
    {
        IppVersion value = NoValue.Instance;
        value.IsValue.Should().BeFalse();

        (value == NoValue.Instance).Should().BeTrue();
        (NoValue.Instance == value).Should().BeTrue();
        (value != NoValue.Instance).Should().BeFalse();
        (NoValue.Instance != value).Should().BeFalse();

        var normal = new IppVersion(1, 1);
        (normal == NoValue.Instance).Should().BeFalse();
        (NoValue.Instance == normal).Should().BeFalse();
        (normal != NoValue.Instance).Should().BeTrue();
        (NoValue.Instance != normal).Should().BeTrue();
    }
}

