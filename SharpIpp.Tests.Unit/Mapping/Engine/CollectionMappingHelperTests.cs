using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpIpp.Mapping;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Tests.Unit.Mapping.Engine;

[TestClass]
public class CollectionMappingHelperTests
{
    private class TestModel
    {
        public string Value { get; set; } = string.Empty;
    }

    [TestMethod]
    public void MapArrayToList_ShouldConvertArrayToList()
    {
        var src = new[] { 1, 2, 3 };
        var result = CollectionMappingHelper.MapArrayToList(src, Mock.Of<IMapperApplier>());

        result.Should().BeOfType<List<int>>();
        result.Should().Equal(1, 2, 3);
    }

    [TestMethod]
    public void MapListToArray_ShouldConvertListToArray()
    {
        var src = new List<int> { 1, 2, 3 };
        var result = CollectionMappingHelper.MapListToArray(src, Mock.Of<IMapperApplier>());

        result.Should().BeOfType<int[]>();
        result.Should().Equal(1, 2, 3);
    }

    [TestMethod]
    public void MapItemToArray_ShouldWrapItemInArray()
    {
        var result = CollectionMappingHelper.MapItemToArray(42, Mock.Of<IMapperApplier>());

        result.Should().Equal(42);
    }

    [TestMethod]
    public void MapEnumerableToList_ShouldConvertEnumerableToList()
    {
        IEnumerable<int> src = new[] { 10, 20 };
        var result = CollectionMappingHelper.MapEnumerableToList(src, Mock.Of<IMapperApplier>());

        result.Should().BeOfType<List<int>>();
        result.Should().Equal(10, 20);
    }

    [TestMethod]
    public void MapEnumerableToArray_ShouldConvertEnumerableToArray()
    {
        IEnumerable<int> src = new List<int> { 10, 20 };
        var result = CollectionMappingHelper.MapEnumerableToArray(src, Mock.Of<IMapperApplier>());

        result.Should().BeOfType<int[]>();
        result.Should().Equal(10, 20);
    }

    [TestMethod]
    public void MapObjectArrayToArray_ShouldMapNonNullItemsAndDefaultNullItems()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<int>("10")).Returns(10);
        mapperMock.Setup(m => m.Map<int>("20")).Returns(20);

        var src = new object[] { "10", null!, "20" };
        var result = CollectionMappingHelper.MapObjectArrayToArray<int>(src, mapperMock.Object);

        result.Should().Equal(10, 0, 20);
    }

    [TestMethod]
    public void MapObjectArrayToList_ShouldMapNonNullItemsAndDefaultNullItems()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<int>("10")).Returns(10);
        mapperMock.Setup(m => m.Map<int>("20")).Returns(20);

        var src = new object[] { "10", null!, "20" };
        var result = CollectionMappingHelper.MapObjectArrayToList<int>(src, mapperMock.Object);

        result.Should().Equal(10, 0, 20);
    }

    [TestMethod]
    public void MapStringArrayToEnumArray_ShouldMapEachStringToEnum()
    {
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<JobState>("pending")).Returns(JobState.Pending);
        mapperMock.Setup(m => m.Map<JobState>("processing")).Returns(JobState.Processing);

        var src = new[] { "pending", "processing" };
        var result = CollectionMappingHelper.MapStringArrayToEnumArray<JobState>(src, mapperMock.Object);

        result.Should().Equal(JobState.Pending, JobState.Processing);
    }

    [TestMethod]
    public void MapEnumArrayToStringArray_ShouldConvertEnumToString()
    {
        var src = new[] { JobState.Pending, JobState.Processing };
        var result = CollectionMappingHelper.MapEnumArrayToStringArray(src, Mock.Of<IMapperApplier>());

        result.Should().Equal("Pending", "Processing");
    }

    [TestMethod]
    public void MapStringArrayToEnumList_ShouldMapEachStringToEnumInList()
    {
        // Tests lines 58-60 (MapStringArrayToEnumList)
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<JobState>("pending")).Returns(JobState.Pending);
        mapperMock.Setup(m => m.Map<JobState>("completed")).Returns(JobState.Completed);

        var src = new[] { "pending", "completed" };
        var result = CollectionMappingHelper.MapStringArrayToEnumList<JobState>(src, mapperMock.Object);

        result.Should().BeOfType<List<JobState>>();
        result.Should().Equal(JobState.Pending, JobState.Completed);
    }

    [TestMethod]
    public void MapEnumListToStringArray_ShouldConvertEnumListToStringArray()
    {
        // Tests lines 65-67 (MapEnumListToStringArray)
        var src = new List<JobState> { JobState.Pending, JobState.Canceled };
        var result = CollectionMappingHelper.MapEnumListToStringArray(src, Mock.Of<IMapperApplier>());

        result.Should().BeOfType<string[]>();
        result.Should().Equal("Pending", "Canceled");
    }

    [TestMethod]
    public void MapStringArrayToEnumEnumerable_ShouldDelegateToMapper()
    {
        var mapperMock = new Mock<IMapperApplier>();
        var expected = new[] { JobState.Pending };
        var src = new[] { "pending" };
        mapperMock.Setup(m => m.Map<JobState[]>(src)).Returns(expected);

        var result = CollectionMappingHelper.MapStringArrayToEnumEnumerable<JobState>(src, mapperMock.Object);

        result.Should().BeSameAs(expected);
    }

    [TestMethod]
    public void MapStringArrayToEnumReadOnlyCollection_ShouldDelegateToMapper()
    {
        var mapperMock = new Mock<IMapperApplier>();
        var expected = new[] { JobState.Processing };
        var src = new[] { "processing" };
        mapperMock.Setup(m => m.Map<JobState[]>(src)).Returns(expected);

        var result = CollectionMappingHelper.MapStringArrayToEnumReadOnlyCollection<JobState>(src, mapperMock.Object);

        result.Should().BeSameAs(expected);
    }

    [TestMethod]
    public void MapObjectArrayToFilteredArray_ShouldSkipNullAndNoValueAndSkipNullMapped()
    {
        // Tests line 80 (item == null || item is NoValue continue, mapped != null)
        var mapperMock = new Mock<IMapperApplier>();
        var model1 = new TestModel { Value = "1" };
        mapperMock.Setup(m => m.MapNullable<TestModel>("valid")).Returns(model1);
        mapperMock.Setup(m => m.MapNullable<TestModel>("returns-null")).Returns((TestModel?)null);

        var src = new object[] { "valid", null!, NoValue.Instance, "returns-null" };
        var result = CollectionMappingHelper.MapObjectArrayToFilteredArray<TestModel>(src, mapperMock.Object);

        result.Should().BeOfType<TestModel[]>();
        result.Should().HaveCount(1);
        result[0].Should().BeSameAs(model1);
    }

    [TestMethod]
    public void MapObjectArrayToFilteredList_ShouldSkipNullAndNoValueAndSkipNullMapped()
    {
        // Tests lines 89-97 (MapObjectArrayToFilteredList)
        var mapperMock = new Mock<IMapperApplier>();
        var model1 = new TestModel { Value = "A" };
        var model2 = new TestModel { Value = "B" };
        mapperMock.Setup(m => m.MapNullable<TestModel>("first")).Returns(model1);
        mapperMock.Setup(m => m.MapNullable<TestModel>("second")).Returns(model2);
        mapperMock.Setup(m => m.MapNullable<TestModel>("null-map")).Returns((TestModel?)null);

        var src = new object[] { "first", null!, NoValue.Instance, "null-map", "second" };
        var result = CollectionMappingHelper.MapObjectArrayToFilteredList<TestModel>(src, mapperMock.Object);

        result.Should().BeOfType<List<TestModel>>();
        result.Should().HaveCount(2);
        result.Should().Equal(model1, model2);
    }

    [TestMethod]
    public void MapModelToIppAttributeEnumerable_ShouldDelegateToMapper()
    {
        var mapperMock = new Mock<IMapperApplier>();
        var expected = new List<IppAttribute> { new(Tag.NameWithoutLanguage, "test", "val") };
        var model = new TestModel { Value = "test" };
        mapperMock.Setup(m => m.Map<List<IppAttribute>>(model)).Returns(expected);

        var result = CollectionMappingHelper.MapModelToIppAttributeEnumerable(model, mapperMock.Object);

        result.Should().BeSameAs(expected);
    }

    [TestMethod]
    public void MapModelToIppDictionary_ShouldReturnDictionary()
    {
        var mapperMock = new Mock<IMapperApplier>();
        var attrList = new List<IppAttribute> { new(Tag.NameWithoutLanguage, "attr1", "val1") };
        var model = new TestModel { Value = "m" };
        mapperMock.Setup(m => m.Map<List<IppAttribute>>(model)).Returns(attrList);

        var result = CollectionMappingHelper.MapModelToIppDictionary(model, mapperMock.Object);

        result.Should().ContainKey("attr1");
        result["attr1"].Should().HaveCount(1);
        result["attr1"][0].Value.Should().Be("val1");
    }

    [TestMethod]
    public void MapModelToConcreteIppDictionary_ShouldReturnConcreteDictionary()
    {
        // Tests line 112 (MapModelToConcreteIppDictionary)
        var mapperMock = new Mock<IMapperApplier>();
        var attrList = new List<IppAttribute>
        {
            new(Tag.NameWithoutLanguage, "printer-name", "my-printer"),
            new(Tag.Integer, "copies", 2)
        };
        var model = new TestModel { Value = "model" };
        mapperMock.Setup(m => m.Map<List<IppAttribute>>(model)).Returns(attrList);

        var result = CollectionMappingHelper.MapModelToConcreteIppDictionary(model, mapperMock.Object);

        result.Should().BeOfType<Dictionary<string, IppAttribute[]>>();
        result.Should().ContainKey("printer-name");
        result["printer-name"][0].Value.Should().Be("my-printer");
        result.Should().ContainKey("copies");
        result["copies"][0].Value.Should().Be(2);
    }

    [TestMethod]
    public void MapAttributeGroupListToModelArray_ShouldMapEachGroupToModel()
    {
        var mapperMock = new Mock<IMapperApplier>();
        var group1 = new List<IppAttribute> { new(Tag.NameWithoutLanguage, "id", "1") };
        var group2 = new List<IppAttribute> { new(Tag.NameWithoutLanguage, "id", "2") };
        var model1 = new TestModel { Value = "1" };
        var model2 = new TestModel { Value = "2" };

        mapperMock.Setup(m => m.Map<TestModel>(It.Is<IDictionary<string, IppAttribute[]>>(d => d.ContainsKey("id") && (string)d["id"][0].Value == "1")))
            .Returns(model1);
        mapperMock.Setup(m => m.Map<TestModel>(It.Is<IDictionary<string, IppAttribute[]>>(d => d.ContainsKey("id") && (string)d["id"][0].Value == "2")))
            .Returns(model2);

        var src = new List<List<IppAttribute>> { group1, group2 };
        var result = CollectionMappingHelper.MapAttributeGroupListToModelArray<TestModel>(src, mapperMock.Object);

        result.Should().Equal(model1, model2);
    }

    [TestMethod]
    public void MapModelArrayToAttributeGroupList_ShouldMapEachModelToAttributeList()
    {
        var mapperMock = new Mock<IMapperApplier>();
        var model1 = new TestModel { Value = "1" };
        var model2 = new TestModel { Value = "2" };
        var list1 = new List<IppAttribute> { new(Tag.NameWithoutLanguage, "id", "1") };
        var list2 = new List<IppAttribute> { new(Tag.NameWithoutLanguage, "id", "2") };

        mapperMock.Setup(m => m.Map<List<IppAttribute>>(model1)).Returns(list1);
        mapperMock.Setup(m => m.Map<List<IppAttribute>>(model2)).Returns(list2);

        var src = new[] { model1, model2 };
        var result = CollectionMappingHelper.MapModelArrayToAttributeGroupList(src, mapperMock.Object);

        result.Should().HaveCount(2);
        result[0].Should().BeSameAs(list1);
        result[1].Should().BeSameAs(list2);
    }

    [TestMethod]
    public void MapDictionaryToIppValue_ShouldReturnDefault_WhenSingleOutOfBandAttribute()
    {
        // Tests lines 133-134 (MapDictionaryToIppValue out-of-band branch)
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "media-col", new[] { new IppAttribute(Tag.NoValue, "media-col", NoValue.Instance) } }
        };

        var result = CollectionMappingHelper.MapDictionaryToIppValue<TestModel>(dict, Mock.Of<IMapperApplier>());

        result.IsValue.Should().BeFalse();
        result.ValueAsObject.Should().BeNull();
        Action act = () => _ = result.Value;
        act.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void MapDictionaryToIppValue_ShouldMapValue_WhenNotOutOfBand()
    {
        // Tests lines 133-134 (MapDictionaryToIppValue value branch)
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "media-col", new[] { new IppAttribute(Tag.NameWithoutLanguage, "media-col", "stationery") } }
        };
        var expectedModel = new TestModel { Value = "stationery" };
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<TestModel>(dict)).Returns(expectedModel);

        var result = CollectionMappingHelper.MapDictionaryToIppValue<TestModel>(dict, mapperMock.Object);

        result.IsValue.Should().BeTrue();
        result.Value.Should().BeSameAs(expectedModel);
    }

    [TestMethod]
    public void MapDictionaryToIppValue_ShouldMapValue_WhenMultipleAttributesOrValues()
    {
        // Out-of-band check requires Count == 1 and Values.First().Length == 1
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "a1", new[] { new IppAttribute(Tag.NoValue, "a1", NoValue.Instance) } },
            { "a2", new[] { new IppAttribute(Tag.NoValue, "a2", NoValue.Instance) } }
        };
        var expectedModel = new TestModel { Value = "multi" };
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<TestModel>(dict)).Returns(expectedModel);

        var result = CollectionMappingHelper.MapDictionaryToIppValue<TestModel>(dict, mapperMock.Object);

        result.IsValue.Should().BeTrue();
        result.Value.Should().BeSameAs(expectedModel);
    }

    [TestMethod]
    public void MapDictionaryToIppValue_ShouldMapValue_WhenMultipleValuesForSingleAttribute()
    {
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "media-col", new[]
                {
                    new IppAttribute(Tag.NoValue, "media-col", NoValue.Instance),
                    new IppAttribute(Tag.NoValue, "media-col", NoValue.Instance)
                }
            }
        };
        var expectedModel = new TestModel { Value = "multi-val" };
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<TestModel>(dict)).Returns(expectedModel);

        var result = CollectionMappingHelper.MapDictionaryToIppValue<TestModel>(dict, mapperMock.Object);

        result.IsValue.Should().BeTrue();
        result.Value.Should().BeSameAs(expectedModel);
    }

    [TestMethod]
    public void MapConcreteDictionaryToIppValue_ShouldReturnDefault_WhenSingleOutOfBandAttribute()
    {
        // Tests lines 139-140 (MapConcreteDictionaryToIppValue out-of-band branch)
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "media-col", new[] { new IppAttribute(Tag.NoValue, "media-col", NoValue.Instance) } }
        };

        var result = CollectionMappingHelper.MapConcreteDictionaryToIppValue<TestModel>(dict, Mock.Of<IMapperApplier>());

        result.IsValue.Should().BeFalse();
        result.ValueAsObject.Should().BeNull();
        Action act = () => _ = result.Value;
        act.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void MapConcreteDictionaryToIppValue_ShouldMapValue_WhenNotOutOfBand()
    {
        // Tests lines 139-140 (MapConcreteDictionaryToIppValue value branch: tag not out-of-band)
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "media-col", new[] { new IppAttribute(Tag.NameWithoutLanguage, "media-col", "stationery") } }
        };
        var expectedModel = new TestModel { Value = "stationery" };
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<TestModel>(dict)).Returns(expectedModel);

        var result = CollectionMappingHelper.MapConcreteDictionaryToIppValue<TestModel>(dict, mapperMock.Object);

        result.IsValue.Should().BeTrue();
        result.Value.Should().BeSameAs(expectedModel);
    }

    [TestMethod]
    public void MapConcreteDictionaryToIppValue_ShouldMapValue_WhenMultipleAttributes()
    {
        // Tests lines 139-140 (MapConcreteDictionaryToIppValue value branch: Count != 1)
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "a1", new[] { new IppAttribute(Tag.NoValue, "a1", NoValue.Instance) } },
            { "a2", new[] { new IppAttribute(Tag.NoValue, "a2", NoValue.Instance) } }
        };
        var expectedModel = new TestModel { Value = "multi-attr" };
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<TestModel>(dict)).Returns(expectedModel);

        var result = CollectionMappingHelper.MapConcreteDictionaryToIppValue<TestModel>(dict, mapperMock.Object);

        result.IsValue.Should().BeTrue();
        result.Value.Should().BeSameAs(expectedModel);
    }

    [TestMethod]
    public void MapConcreteDictionaryToIppValue_ShouldMapValue_WhenMultipleValuesForSingleAttribute()
    {
        // Tests lines 139-140 (MapConcreteDictionaryToIppValue value branch: Values.First().Length != 1)
        var dict = new Dictionary<string, IppAttribute[]>
        {
            { "media-col", new[]
                {
                    new IppAttribute(Tag.NoValue, "media-col", NoValue.Instance),
                    new IppAttribute(Tag.NoValue, "media-col", NoValue.Instance)
                }
            }
        };
        var expectedModel = new TestModel { Value = "multi-val" };
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<TestModel>(dict)).Returns(expectedModel);

        var result = CollectionMappingHelper.MapConcreteDictionaryToIppValue<TestModel>(dict, mapperMock.Object);

        result.IsValue.Should().BeTrue();
        result.Value.Should().BeSameAs(expectedModel);
    }

    [TestMethod]
    public void MapIppValueModelToIppAttributeEnumerable_ShouldReturnNoValueAttribute_WhenNotValue()
    {
        var src = default(IppValue<TestModel>);
        var result = CollectionMappingHelper.MapIppValueModelToIppAttributeEnumerable(src, "media-col", Mock.Of<IMapperApplier>()).ToList();

        result.Should().HaveCount(1);
        result[0].Tag.Should().Be(Tag.NoValue);
        result[0].Name.Should().Be("media-col");
        result[0].Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void MapIppValueModelToIppAttributeEnumerable_ShouldMapValue_WhenHasValue()
    {
        var model = new TestModel { Value = "test" };
        var src = new IppValue<TestModel>(model);
        var expected = new List<IppAttribute> { new(Tag.NameWithoutLanguage, "media-col", "test") };
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<IEnumerable<IppAttribute>>(model)).Returns(expected);

        var result = CollectionMappingHelper.MapIppValueModelToIppAttributeEnumerable(src, "media-col", mapperMock.Object);

        result.Should().BeSameAs(expected);
    }

    [TestMethod]
    public void MapIppValueModelToIppAttributeList_ShouldReturnNoValueAttribute_WhenNotValue()
    {
        // Tests line 153 (MapIppValueModelToIppAttributeList !src.IsValue branch)
        var src = default(IppValue<TestModel>);
        var result = CollectionMappingHelper.MapIppValueModelToIppAttributeList(src, "media-col", Mock.Of<IMapperApplier>());

        result.Should().HaveCount(1);
        result[0].Tag.Should().Be(Tag.NoValue);
        result[0].Name.Should().Be("media-col");
        result[0].Value.Should().Be(NoValue.Instance);
    }

    [TestMethod]
    public void MapIppValueModelToIppAttributeList_ShouldMapValue_WhenHasValue()
    {
        var model = new TestModel { Value = "test" };
        var src = new IppValue<TestModel>(model);
        var expected = new List<IppAttribute> { new(Tag.NameWithoutLanguage, "media-col", "test") };
        var mapperMock = new Mock<IMapperApplier>();
        mapperMock.Setup(m => m.Map<List<IppAttribute>>(model)).Returns(expected);

        var result = CollectionMappingHelper.MapIppValueModelToIppAttributeList(src, "media-col", mapperMock.Object);

        result.Should().BeSameAs(expected);
    }
}
