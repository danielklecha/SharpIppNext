using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Unit.Protocol.Models;

[TestClass]
[ExcludeFromCodeCoverage]
public class StringWithLanguageTests
{
    [TestMethod]
    public void Constructor_SetsPropertiesAndReturnsToString()
    {
        var strWithLang = new StringWithLanguage("en", "Test Value");

        strWithLang.Language.Should().Be("en");
        strWithLang.Value.Should().Be("Test Value");
        strWithLang.ToString().Should().Be("Test Value (en)");
    }

    [TestMethod]
    public void Equals_SameValues_ShouldReturnTrue()
    {
        var str1 = new StringWithLanguage("en", "Test Value");
        var str2 = new StringWithLanguage("en", "Test Value");

        str1.Equals(str2).Should().BeTrue();
        str1.Equals((object)str2).Should().BeTrue();
        (str1 == str2).Should().BeTrue();
        (str1 != str2).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_DifferentLanguage_ShouldReturnFalse()
    {
        var str1 = new StringWithLanguage("en", "Test Value");
        var str2 = new StringWithLanguage("fr", "Test Value");

        str1.Equals(str2).Should().BeFalse();
        str1.Equals((object)str2).Should().BeFalse();
        (str1 == str2).Should().BeFalse();
        (str1 != str2).Should().BeTrue();
    }

    [TestMethod]
    public void Equals_DifferentValue_ShouldReturnFalse()
    {
        var str1 = new StringWithLanguage("en", "Test Value");
        var str2 = new StringWithLanguage("en", "Other Value");

        str1.Equals(str2).Should().BeFalse();
        str1.Equals((object)str2).Should().BeFalse();
        (str1 == str2).Should().BeFalse();
        (str1 != str2).Should().BeTrue();
    }

    [TestMethod]
    public void Equals_NullOrDifferentType_ShouldReturnFalse()
    {
        var str = new StringWithLanguage("en", "Test Value");

        str.Equals((object?)null).Should().BeFalse();
        str.Equals((string?)null).Should().BeFalse();
        str.Equals(new object()).Should().BeFalse();
    }

    [TestMethod]
    public void GetHashCode_SameValues_ShouldReturnSameHashCode()
    {
        var str1 = new StringWithLanguage("en", "Test Value");
        var str2 = new StringWithLanguage("en", "Test Value");

        str1.GetHashCode().Should().Be(str2.GetHashCode());
    }

    [TestMethod]
    public void GetHashCode_DifferentValues_ShouldReturnDifferentHashCode()
    {
        var str1 = new StringWithLanguage("en", "Test Value");
        var str2 = new StringWithLanguage("fr", "Test Value");

        str1.GetHashCode().Should().NotBe(str2.GetHashCode());
    }

    [TestMethod]
    public void GetHashCode_NullValues_ShouldReturnExpectedHashCode()
    {
        var str1 = new StringWithLanguage(null!, null!);
        
#pragma warning disable CS1718 // Comparison made to same variable
        (str1 == str1).Should().BeTrue();
#pragma warning restore CS1718 // Comparison made to same variable
        str1.GetHashCode().Should().Be(0);

        var str2 = new StringWithLanguage("en", null!);
        var str3 = new StringWithLanguage(null!, "Test");
        
        str2.GetHashCode().Should().NotBe(0);
        str3.GetHashCode().Should().NotBe(0);
        str2.GetHashCode().Should().NotBe(str3.GetHashCode());
    }

    [TestMethod]
    public void SingleParameterConstructor_SetsPropertiesCorrectly()
    {
        var swl = new StringWithLanguage("Single Value");

        swl.Language.Should().BeNull();
        swl.Value.Should().Be("Single Value");
        swl.HasLanguage.Should().BeFalse();
        swl.ToString().Should().Be("Single Value");
    }

    [TestMethod]
    public void TwoParameterConstructor_WithNullOrEmptyLanguage_HasLanguageIsFalse()
    {
        var swlNull = new StringWithLanguage(null, "Test");
        swlNull.Language.Should().BeNull();
        swlNull.HasLanguage.Should().BeFalse();
        swlNull.ToString().Should().Be("Test");

        var swlEmpty = new StringWithLanguage(string.Empty, "Test");
        swlEmpty.Language.Should().BeEmpty();
        swlEmpty.HasLanguage.Should().BeFalse();
        swlEmpty.ToString().Should().Be("Test");

        var swlWithLang = new StringWithLanguage("pl", "Test");
        swlWithLang.HasLanguage.Should().BeTrue();
        swlWithLang.ToString().Should().Be("Test (pl)");
    }

    [TestMethod]
    public void ImplicitConversions_WorkBidirectionally()
    {
        // string -> StringWithLanguage
        StringWithLanguage swl = "converted text";
        swl.Language.Should().BeNull();
        swl.Value.Should().Be("converted text");
        swl.HasLanguage.Should().BeFalse();

        // StringWithLanguage -> string
        string? text = swl;
        text.Should().Be("converted text");

        StringWithLanguage swlWithLang = new("de", "Guten Tag");
        string? extracted = swlWithLang;
        extracted.Should().Be("Guten Tag");
    }

    [TestMethod]
    public void ImplicitConversion_FromNullString_ProducesNoValueState()
    {
        string? nullString = null;
        StringWithLanguage swl = nullString;
        swl.IsValue.Should().BeFalse();
        swl.Language.Should().BeNull();
        swl.Value.Should().BeNull();
        swl.HasLanguage.Should().BeFalse();
    }

    [TestMethod]
    public void ImplicitConversion_FromNoValue_ProducesNoValueState()
    {
        StringWithLanguage swl = NoValue.Instance;
        swl.IsValue.Should().BeFalse();
        swl.Language.Should().BeNull();
        swl.Value.Should().BeNull();
        swl.HasLanguage.Should().BeFalse();
        (swl == NoValue.Instance).Should().BeTrue();
        swl.Equals(NoValue.Instance).Should().BeTrue();
    }

    [TestMethod]
    public void NullableStringWithLanguage_ThreeStates_BehaveCorrectly()
    {
        // 1. null — unset/absent
        StringWithLanguage? unset = null;
        unset.HasValue.Should().BeFalse();

        // 2. default / NoValue — present with Tag.NoValue
        StringWithLanguage? noVal = NoValue.Instance;
        noVal.HasValue.Should().BeTrue();
        noVal.Value.IsValue.Should().BeFalse();
        ((string?)noVal).Should().BeNull();
        noVal.Should().Be(NoValue.Instance);

        // 3. Concrete value without language
        StringWithLanguage? plain = "My Document";
        plain.HasValue.Should().BeTrue();
        plain.Value.IsValue.Should().BeTrue();
        plain.Value.HasLanguage.Should().BeFalse();
        plain.Value.Value.Should().Be("My Document");
        string? plainStr = plain;
        plainStr.Should().Be("My Document");
        plain.Should().Be("My Document");

        // 4. Concrete value with language
        StringWithLanguage? localized = new StringWithLanguage("fr", "Mon Document");
        localized.HasValue.Should().BeTrue();
        localized.Value.IsValue.Should().BeTrue();
        localized.Value.HasLanguage.Should().BeTrue();
        localized.Value.Language.Should().Be("fr");
        localized.Value.Value.Should().Be("Mon Document");
        string? locStr = localized;
        locStr.Should().Be("Mon Document");
        localized.Should().Be("Mon Document");
    }

    [TestMethod]
    public void ToString_WhenNoValue_ReturnsNoValueString()
    {
        default(StringWithLanguage).ToString().Should().Be("no value");
        ((StringWithLanguage)NoValue.Instance).ToString().Should().Be("no value");
    }

    [TestMethod]
    public void Equals_StringWithLanguage_ConcreteVsNoValue_ReturnsFalse()
    {
        var concrete = new StringWithLanguage("en", "Test Value");
        var noVal = default(StringWithLanguage);

        concrete.Equals(noVal).Should().BeFalse();
        noVal.Equals(concrete).Should().BeFalse();
        (concrete == noVal).Should().BeFalse();
        (concrete != noVal).Should().BeTrue();
        (noVal == concrete).Should().BeFalse();
        (noVal != concrete).Should().BeTrue();
    }

    [TestMethod]
    public void Equals_StringWithLanguage_NoValueVsNoValue_ReturnsTrue()
    {
        var noVal1 = default(StringWithLanguage);
        var noVal2 = (StringWithLanguage)NoValue.Instance;

        noVal1.Equals(noVal2).Should().BeTrue();
        noVal2.Equals(noVal1).Should().BeTrue();
        (noVal1 == noVal2).Should().BeTrue();
        (noVal1 != noVal2).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_String_WhenNoValue_BehavesCorrectly()
    {
        var noVal = default(StringWithLanguage);

        noVal.Equals((string?)null).Should().BeTrue();
        noVal.Equals("Test Value").Should().BeFalse();
    }

    [TestMethod]
    public void Equals_String_WhenConcrete_DifferentValueReturnsFalse()
    {
        var concrete = new StringWithLanguage("en", "Test Value");

        concrete.Equals("Test Value").Should().BeTrue();
        concrete.Equals("Other Value").Should().BeFalse();
        concrete.Equals((string?)null).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_NoValue_WhenConcrete_ReturnsFalse()
    {
        var concrete = new StringWithLanguage("en", "Test Value");

        concrete.Equals(NoValue.Instance).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_Object_WithString_BehavesCorrectly()
    {
        var concrete = new StringWithLanguage("en", "Test Value");
        var noVal = default(StringWithLanguage);

        concrete.Equals((object)"Test Value").Should().BeTrue();
        concrete.Equals((object)"Other Value").Should().BeFalse();
        noVal.Equals((object)"Test Value").Should().BeFalse();
    }

    [TestMethod]
    public void Equals_Object_WithINoValue_BehavesCorrectly()
    {
        var concrete = new StringWithLanguage("en", "Test Value");
        var noVal = default(StringWithLanguage);

        // Boxed NoValue (implements INoValue with IsValue = false)
        concrete.Equals((object)NoValue.Instance).Should().BeFalse();
        noVal.Equals((object)NoValue.Instance).Should().BeTrue();

        // Custom INoValue with IsValue = false
        var falseNoVal = new CustomNoValue(isValue: false);
        concrete.Equals((object)falseNoVal).Should().BeFalse();
        noVal.Equals((object)falseNoVal).Should().BeTrue();

        // Custom INoValue with IsValue = true
        var trueNoVal = new CustomNoValue(isValue: true);
        concrete.Equals((object)trueNoVal).Should().BeFalse();
        noVal.Equals((object)trueNoVal).Should().BeFalse();

        // Object that does not implement INoValue
        concrete.Equals(new object()).Should().BeFalse();
        noVal.Equals(new object()).Should().BeFalse();
    }

    [TestMethod]
    public void Equals_Object_WithStringWithLanguage_BehavesCorrectly()
    {
        var concrete1 = new StringWithLanguage("en", "Test Value");
        var concrete2 = new StringWithLanguage("en", "Test Value");
        var noVal = default(StringWithLanguage);

        concrete1.Equals((object)concrete2).Should().BeTrue();
        concrete1.Equals((object)noVal).Should().BeFalse();
        noVal.Equals((object)concrete1).Should().BeFalse();
        noVal.Equals((object)default(StringWithLanguage)).Should().BeTrue();
    }

    [TestMethod]
    public void GetHashCode_WhenNoValue_ReturnsZero()
    {
        default(StringWithLanguage).GetHashCode().Should().Be(0);
        ((StringWithLanguage)NoValue.Instance).GetHashCode().Should().Be(0);
    }

    [TestMethod]
    public void ImplicitConversion_ToString_WhenNoValue_ReturnsNull()
    {
        StringWithLanguage noVal = default;
        string? text = noVal;
        text.Should().BeNull();
    }

    [TestMethod]
    public void StringEquality_WithLanguage_ChecksValueAndLanguage()
    {
        StringWithLanguage swl1 = new("en", "test");
        StringWithLanguage swl2 = new("en", "test");
        StringWithLanguage swl3 = new("fr", "test");

        (swl1 == swl2).Should().BeTrue();
        (swl1 == swl3).Should().BeFalse();
        swl1.Equals("test").Should().BeTrue();
        swl3.Equals("test").Should().BeTrue();
    }

    private sealed class CustomNoValue(bool isValue) : INoValue
    {
        public bool IsValue => isValue;
    }
}



