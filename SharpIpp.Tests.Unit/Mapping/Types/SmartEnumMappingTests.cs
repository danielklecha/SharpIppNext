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
        typeof(IMarkedSmartEnum).Assembly
            .GetTypes()
            .Where(x => typeof(IMarkedSmartEnum).IsAssignableFrom(x) && x is { IsValueType: true, IsAbstract: false, IsInterface: false })
            .OrderBy(x => x.FullName)
            .Select(x => new object[] { x });

    public static IEnumerable<object[]> NonKeywordSmartEnumTypeData =>
        typeof(ISmartEnum).Assembly
            .GetTypes()
            .Where(x => typeof(ISmartEnum).IsAssignableFrom(x) && !typeof(IMarkedSmartEnum).IsAssignableFrom(x) && x is { IsValueType: true, IsAbstract: false, IsInterface: false })
            .OrderBy(x => x.FullName)
            .Select(x => new object[] { x });


    [TestMethod]
    public void Map_FromDictionary_ForKeywordSmartEnums_PreservesIsMarkedFlag()
    {
        AssertMapFromAttributes(Tag.Keyword, true);
        AssertMapFromAttributes(Tag.NameWithoutLanguage, false);
    }

    [TestMethod]
    [DynamicData(nameof(KeywordSmartEnumTypeData))]
    public void Map_StringToKeywordSmartEnum_ShouldSetKeywordAndValueFlags(Type smartEnumType)
    {
        const string value = "mapped-value";

        var mapped = _mapper.Map(value, typeof(string), smartEnumType);

        mapped.Should().BeAssignableTo<IMarkedSmartEnum>();
        var keywordSmartEnum = (IMarkedSmartEnum)mapped;
        keywordSmartEnum.Value.Should().Be(value);
        keywordSmartEnum.IsMarked.Should().BeTrue();
        keywordSmartEnum.IsValue.Should().BeTrue();
    }

    [TestMethod]
    [DynamicData(nameof(NonKeywordSmartEnumTypeData))]
    public void Map_StringToNonKeywordSmartEnum_ShouldSetValueFlag(Type smartEnumType)
    {
        const string value = "mapped-value";

        var mapped = _mapper.Map(value, typeof(string), smartEnumType);

        mapped.Should().BeAssignableTo<ISmartEnum>();
        mapped.Should().NotBeAssignableTo<IMarkedSmartEnum>();
        var smartEnum = (ISmartEnum)mapped;
        smartEnum.Value.Should().Be(value);
        smartEnum.IsValue.Should().BeTrue();
    }

    private void AssertMapFromAttributes(Tag tag, bool expectedIsMarked)
    {
        const string customValue = "custom-value";

        var finishingsCol = _mapper.Map<FinishingsCol>(new Dictionary<string, IppAttribute[]>
        {
            ["finishing-template"] = [new IppAttribute(tag, "finishing-template", customValue)],
            ["imposition-template"] = [new IppAttribute(tag, "imposition-template", customValue)],
            ["media-size-name"] = [new IppAttribute(tag, "media-size-name", customValue)]
        });
        AssertFlag(finishingsCol.FinishingTemplate, customValue, expectedIsMarked);
        AssertFlag(finishingsCol.ImpositionTemplate, customValue, expectedIsMarked);
        AssertFlag(finishingsCol.MediaSizeName, customValue, expectedIsMarked);

        var baling = _mapper.Map<Baling>(new Dictionary<string, IppAttribute[]>
        {
            ["baling-type"] = [new IppAttribute(tag, "baling-type", customValue)]
        });
        AssertFlag(baling.BalingType, customValue, expectedIsMarked);

        var binding = _mapper.Map<Binding>(new Dictionary<string, IppAttribute[]>
        {
            ["binding-type"] = [new IppAttribute(tag, "binding-type", customValue)]
        });
        AssertFlag(binding.BindingType, customValue, expectedIsMarked);

        var coating = _mapper.Map<Coating>(new Dictionary<string, IppAttribute[]>
        {
            ["coating-type"] = [new IppAttribute(tag, "coating-type", customValue)]
        });
        AssertFlag(coating.CoatingType, customValue, expectedIsMarked);

        var covering = _mapper.Map<Covering>(new Dictionary<string, IppAttribute[]>
        {
            ["covering-name"] = [new IppAttribute(tag, "covering-name", customValue)]
        });
        AssertFlag(covering.CoveringName, customValue, expectedIsMarked);

        var laminating = _mapper.Map<Laminating>(new Dictionary<string, IppAttribute[]>
        {
            ["laminating-type"] = [new IppAttribute(tag, "laminating-type", customValue)]
        });
        AssertFlag(laminating.LaminatingType, customValue, expectedIsMarked);

        var trimming = _mapper.Map<Trimming>(new Dictionary<string, IppAttribute[]>
        {
            ["trimming-type"] = [new IppAttribute(tag, "trimming-type", customValue)]
        });
        AssertFlag(trimming.TrimmingType, customValue, expectedIsMarked);

        var cover = _mapper.Map<Cover>(new Dictionary<string, IppAttribute[]>
        {
            ["media"] = [new IppAttribute(tag, "media", customValue)]
        });
        AssertFlag(cover.Media, customValue, expectedIsMarked);

        var mediaCol = _mapper.Map<MediaCol>(new Dictionary<string, IppAttribute[]>
        {
            ["media-key"] = [new IppAttribute(tag, "media-key", customValue)]
        });
        AssertFlag(mediaCol.MediaKey, customValue, expectedIsMarked);

        var jobAccountingSheets = _mapper.Map<JobAccountingSheets>(new Dictionary<string, IppAttribute[]>
        {
            ["job-accounting-output-bin"] = [new IppAttribute(tag, "job-accounting-output-bin", customValue)]
        });
        AssertFlag(jobAccountingSheets.JobAccountingOutputBin, customValue, expectedIsMarked);
    }

    private static void AssertFlag<T>(T? value, string expectedValue, bool expectedIsMarked)
        where T : struct, IMarkedSmartEnum
    {
        value.HasValue.Should().BeTrue();
        var smartEnum = value.GetValueOrDefault();
        smartEnum.Value.Should().Be(expectedValue);
        smartEnum.IsMarked.Should().Be(expectedIsMarked);
    }
}
