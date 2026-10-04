using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SharpIpp.Mapping.Extensions;
using SharpIpp.Protocol.Models;
using SharpIpp.Tests.Unit.Mapping;

namespace SharpIpp.Tests.Unit.Mapping.Profiles;

[TestClass]
[ExcludeFromCodeCoverage]
public class SmartEnumMappingTests : MapperTestBase
{

    public static IEnumerable<object[]> KeywordSmartEnumTypeData =>
        typeof(IKeywordOrNameEnum).Assembly
            .GetTypes()
            .Where(x => typeof(IKeywordOrNameEnum).IsAssignableFrom(x) && x is { IsValueType: true, IsAbstract: false, IsInterface: false })
            .OrderBy(x => x.FullName)
            .Select(x => new object[] { x });

    public static IEnumerable<object[]> NonKeywordSmartEnumTypeData =>
        typeof(IKeywordEnum).Assembly
            .GetTypes()
            .Where(x => typeof(IKeywordEnum).IsAssignableFrom(x) && !typeof(IKeywordOrNameEnum).IsAssignableFrom(x) && x is { IsValueType: true, IsAbstract: false, IsInterface: false })
            .OrderBy(x => x.FullName)
            .Select(x => new object[] { x });


    [TestMethod]
    public void Map_FromDictionary_ForKeywordOrNameEnums_PreservesIsKeywordFlag()
    {
        AssertMapFromAttributes(Tag.Keyword, true);
        AssertMapFromAttributes(Tag.NameWithoutLanguage, false);
    }

    [TestMethod]
    [DynamicData(nameof(KeywordSmartEnumTypeData))]
    public void Map_StringToKeywordSmartEnum_ShouldSetKeywordFlag(Type smartEnumType)
    {
        const string value = "mapped-value";

        var mapped = _mapper.Map(value, typeof(string), smartEnumType);

        mapped.Should().BeAssignableTo<IKeywordOrNameEnum>();
        var keywordSmartEnum = (IKeywordOrNameEnum)mapped;
        keywordSmartEnum.Value.Should().Be(value);
        keywordSmartEnum.IsKeyword.Should().BeTrue();
    }

    [TestMethod]
    [DynamicData(nameof(NonKeywordSmartEnumTypeData))]
    public void Map_StringToNonKeywordSmartEnum_ShouldSetValue(Type smartEnumType)
    {
        const string value = "mapped-value";

        var mapped = _mapper.Map(value, typeof(string), smartEnumType);

        mapped.Should().BeAssignableTo<IKeywordEnum>();
        mapped.Should().NotBeAssignableTo<IKeywordOrNameEnum>();
        var smartEnum = (IKeywordEnum)mapped;
        smartEnum.Value.Should().Be(value);
    }

    private void AssertMapFromAttributes(Tag tag, bool expectedIsKeyword)
    {
        const string customValue = "custom-value";

        var finishingsCol = _mapper.Map<FinishingsCol>(new Dictionary<string, IppAttribute[]>
        {
            ["finishing-template"] = [new IppAttribute(tag, "finishing-template", customValue)],
            ["imposition-template"] = [new IppAttribute(tag, "imposition-template", customValue)],
            ["media-size-name"] = [new IppAttribute(tag, "media-size-name", customValue)]
        });
        AssertFlag(finishingsCol.FinishingTemplate, customValue, expectedIsKeyword);
        AssertFlag(finishingsCol.ImpositionTemplate, customValue, expectedIsKeyword);
        AssertFlag(finishingsCol.MediaSizeName, customValue, expectedIsKeyword);

        var baling = _mapper.Map<Baling>(new Dictionary<string, IppAttribute[]>
        {
            ["baling-type"] = [new IppAttribute(tag, "baling-type", customValue)]
        });
        AssertFlag(baling.BalingType, customValue, expectedIsKeyword);

        var binding = _mapper.Map<Binding>(new Dictionary<string, IppAttribute[]>
        {
            ["binding-type"] = [new IppAttribute(tag, "binding-type", customValue)]
        });
        AssertFlag(binding.BindingType, customValue, expectedIsKeyword);

        var coating = _mapper.Map<Coating>(new Dictionary<string, IppAttribute[]>
        {
            ["coating-type"] = [new IppAttribute(tag, "coating-type", customValue)]
        });
        AssertFlag(coating.CoatingType, customValue, expectedIsKeyword);

        var covering = _mapper.Map<Covering>(new Dictionary<string, IppAttribute[]>
        {
            ["covering-name"] = [new IppAttribute(tag, "covering-name", customValue)]
        });
        AssertFlag(covering.CoveringName, customValue, expectedIsKeyword);

        var laminating = _mapper.Map<Laminating>(new Dictionary<string, IppAttribute[]>
        {
            ["laminating-type"] = [new IppAttribute(tag, "laminating-type", customValue)]
        });
        AssertFlag(laminating.LaminatingType, customValue, expectedIsKeyword);

        var trimming = _mapper.Map<Trimming>(new Dictionary<string, IppAttribute[]>
        {
            ["trimming-type"] = [new IppAttribute(tag, "trimming-type", customValue)]
        });
        AssertFlag(trimming.TrimmingType, customValue, expectedIsKeyword);

        var cover = _mapper.Map<Cover>(new Dictionary<string, IppAttribute[]>
        {
            ["media"] = [new IppAttribute(tag, "media", customValue)]
        });
        AssertFlag(cover.Media, customValue, expectedIsKeyword);

        var mediaCol = _mapper.Map<MediaCol>(new Dictionary<string, IppAttribute[]>
        {
            ["media-key"] = [new IppAttribute(tag, "media-key", customValue)]
        });
        AssertFlag(mediaCol.MediaKey, customValue, expectedIsKeyword);

        var jobAccountingSheets = _mapper.Map<JobAccountingSheets>(new Dictionary<string, IppAttribute[]>
        {
            ["job-accounting-output-bin"] = [new IppAttribute(tag, "job-accounting-output-bin", customValue)]
        });
        AssertFlag(jobAccountingSheets.JobAccountingOutputBin, customValue, expectedIsKeyword);
    }

    private static void AssertFlag<T>(IppValue<T>? value, string expectedValue, bool expectedIsKeyword)
        where T : struct, IKeywordOrNameEnum
    {
        value.Should().NotBeNull();
        value!.Value.IsValue.Should().BeTrue();
        var smartEnum = value.Value.Value;
        smartEnum.Value.Should().Be(expectedValue);
        smartEnum.IsKeyword.Should().Be(expectedIsKeyword);
    }
}
