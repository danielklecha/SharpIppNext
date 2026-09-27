using System.Collections.Immutable;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Generators;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace SharpIpp.Generators.Tests.Unit;

public partial class MapperSourceGeneratorTests
{
    [TestMethod]
    public void HelperMethods_ShouldCoverDirectInvocations()
    {
        MapperSourceGenerator.ToKebabCase(string.Empty).Should().Be(string.Empty);
        MapperSourceGenerator.ToKebabCase(null!).Should().BeNull();
        MapperSourceGenerator.ToKebabCase("SomeName").Should().Be("some-name");

        var syntaxTree = CSharpSyntaxTree.ParseText("public class NoAttrClass<T> { }");
        var compilation = CSharpCompilation.Create("TestAssembly", new[] { syntaxTree }, references: new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);
        var objectType = compilation.GetSpecialType(SpecialType.System_Object);

        MapperSourceGenerator.ImplementsOrInherits(intType, intType).Should().BeTrue();
        MapperSourceGenerator.ImplementsOrInherits(stringType, objectType).Should().BeTrue();
        MapperSourceGenerator.ImplementsOrInherits(intType, stringType).Should().BeFalse();
        MapperSourceGenerator.ImplementsOrInherits(null, null).Should().BeFalse();
        MapperSourceGenerator.ImplementsOrInherits(intType, null).Should().BeFalse();
        MapperSourceGenerator.ImplementsOrInherits(null, intType).Should().BeFalse();

        MapperSourceGenerator.HasAttribute(null, null).Should().BeFalse();
        MapperSourceGenerator.GetAttribute(null, null).Should().BeNull();

        MapperSourceGenerator.GetTypeAndBaseTypes(null).Should().BeEmpty();
        MapperSourceGenerator.GetTypeAndBaseTypes(objectType).Should().BeEmpty();

        var ifaceType = compilation.GetSpecialType(SpecialType.System_Collections_IEnumerable);
        MapperSourceGenerator.GetTypeAndBaseTypes(ifaceType).Should().ContainSingle();

        var model = compilation.GetSemanticModel(syntaxTree);
        var typeSym = (INamedTypeSymbol)model.GetDeclaredSymbol(syntaxTree.GetRoot().DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.ClassDeclarationSyntax>().First())!;
        MapperSourceGenerator.GetAttribute(typeSym, intType).Should().BeNull();
        MapperSourceGenerator.HasAttribute(typeSym, intType).Should().BeFalse();
        MapperSourceGenerator.HasAttribute(typeSym, null).Should().BeFalse();
        MapperSourceGenerator.GetAttribute(typeSym, null).Should().BeNull();

        MapperSourceGenerator.HasIppAttributeAnnotations(typeSym, null).Should().BeFalse();

        var set = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);
        MapperSourceGenerator.AddTypeIfValid(set, null);
        var errType = compilation.CreateErrorTypeSymbol(null, "Err", 0);
        MapperSourceGenerator.AddTypeIfValid(set, errType);
        MapperSourceGenerator.AddTypeIfValid(set, intType);
        set.Should().ContainSingle();

        MapperSourceGenerator.TryGetEnumFieldName(null, null).Should().BeNull();
        MapperSourceGenerator.TryGetEnumFieldName(intType, 1).Should().BeNull();
        MapperSourceGenerator.TryGetEnumFieldName(intType, null).Should().BeNull();

        MapperSourceGenerator.UnwrapNullable(intType).Should().Be(intType);
        var nullableT = compilation.GetSpecialType(SpecialType.System_Nullable_T);
        var nullableInt = nullableT.Construct(intType);
        MapperSourceGenerator.UnwrapNullable(nullableInt).Should().Be(intType);

        MapperSourceGenerator.GetIppValueArrayInfo(false, null).Should().Be((false, null));
        MapperSourceGenerator.GetIppValueArrayInfo(true, null).Should().Be((false, null));
        MapperSourceGenerator.GetIppValueArrayInfo(true, compilation.CreateArrayTypeSymbol(intType)).Should().Be((true, intType));
        MapperSourceGenerator.GetIppValueArrayInfo(true, intType).Should().Be((false, null));

        var listType = compilation.GetTypeByMetadataName("System.Collections.Generic.List`1")!.Construct(intType);
        var roCollType = compilation.GetTypeByMetadataName("System.Collections.Generic.IReadOnlyCollection`1")!.Construct(intType);
        var ienumType = compilation.GetTypeByMetadataName("System.Collections.Generic.IEnumerable`1")!.Construct(intType);
        var ilistType = compilation.GetTypeByMetadataName("System.Collections.Generic.IList`1")!.Construct(intType);
        var hashSetType = compilation.GetTypeByMetadataName("System.Collections.Generic.HashSet`1")!.Construct(intType);

        MapperSourceGenerator.GetIppValueArrayInfo(true, listType).Should().Be((true, intType));
        MapperSourceGenerator.GetIppValueArrayInfo(true, roCollType).Should().Be((true, intType));
        MapperSourceGenerator.GetIppValueArrayInfo(true, ienumType).Should().Be((true, intType));
        MapperSourceGenerator.GetIppValueArrayInfo(true, ilistType).Should().Be((true, intType));
        MapperSourceGenerator.GetIppValueArrayInfo(true, hashSetType).Should().Be((false, null));

        var typeParam = typeSym.TypeParameters[0];
        MapperSourceGenerator.UnwrapNullable(typeParam).Should().Be(typeParam);
        MapperSourceGenerator.GetIppValueArrayInfo(true, typeParam).Should().Be((false, null));
    }

    [TestMethod]
    public void SyntaxErrorClass_ShouldHandleGracefully()
    {
        var source = "public class { }";
        var (_, generatedTrees, _) = RunGenerator(source);
        generatedTrees.Should().NotBeNull();
    }

    [TestMethod]
    public void AllSectionTags_ShouldMapToAppropriateSectionProperties()
    {
        var source = """
            using SharpIpp.Mapping;
            using SharpIpp.Protocol.Models;

            namespace TestNamespace;

            [IppSection(SectionTag.OperationAttributesTag)]
            public class SecOp { [IppAttribute("p1")] public string? P1 { get; set; } }

            [IppSection(SectionTag.JobAttributesTag)]
            public class SecJob { [IppAttribute("p2")] public string? P2 { get; set; } }

            [IppSection(SectionTag.PrinterAttributesTag)]
            public class SecPrinter { [IppAttribute("p3")] public string? P3 { get; set; } }

            [IppSection(SectionTag.UnsupportedAttributesTag)]
            public class SecUnsupported { [IppAttribute("p4")] public string? P4 { get; set; } }

            [IppSection(SectionTag.SubscriptionAttributesTag)]
            public class SecSub { [IppAttribute("p5")] public string? P5 { get; set; } }

            [IppSection(SectionTag.EventNotificationAttributesTag)]
            public class SecEvent { [IppAttribute("p6")] public string? P6 { get; set; } }

            [IppSection(SectionTag.ResourceAttributesTag)]
            public class SecRes { [IppAttribute("p7")] public string? P7 { get; set; } }

            [IppSection(SectionTag.DocumentAttributesTag)]
            public class SecDoc { [IppAttribute("p8")] public string? P8 { get; set; } }

            [IppSection(SectionTag.SystemAttributesTag)]
            public class SecSys { [IppAttribute("p9")] public string? P9 { get; set; } }

            [IppSection((SectionTag)99)]
            public class SecUnknown { [IppAttribute("p10")] public string? P10 { get; set; } }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var registry = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedMapperRegistry.g.cs"))?.ToString();

        registry.Should().NotBeNull();
        registry.Should().Contain("dst.OperationAttributes");
        registry.Should().Contain("dst.JobAttributes");
        registry.Should().Contain("dst.PrinterAttributes");
        registry.Should().Contain("dst.UnsupportedAttributes");
        registry.Should().Contain("dst.SubscriptionAttributes");
        registry.Should().Contain("dst.EventNotificationAttributes");
        registry.Should().Contain("dst.ResourceAttributes");
        registry.Should().Contain("dst.DocumentAttributes");
        registry.Should().Contain("dst.SystemAttributes");
    }

    [TestMethod]
    public void SmartEnumsAndStructuredStrings_ShouldGenerateConvertersAndModelMappings()
    {
        var source = """
            using System;
            using System.Collections.Generic;
            using SharpIpp.Mapping;
            using SharpIpp.Protocol.Models;

            namespace SharpIpp.Protocol.Models
            {
                public enum ProtoEnumAlpha { Alpha = 1 }
                public enum ProtoEnumBeta { Beta = 2 }
            }

            namespace TestNamespace
            {
                public struct SyntheticSmartEnum : ISmartEnum
                {
                    public string Value { get; set; }
                    public SyntheticSmartEnum(string val) => Value = val;
                    public override string ToString() => Value;
                }

                public class SyntheticClassSmartEnum : ISmartEnum
                {
                    public string Value => "class-smart";
                    public override string ToString() => Value;
                }

                public struct SyntheticMarkedSmartEnum : IMarkedSmartEnum
                {
                    public string Value { get; set; }
                    public bool IsMarked { get; set; }
                    public SyntheticMarkedSmartEnum(string val, bool isMarked) { Value = val; IsMarked = isMarked; }
                    public override string ToString() => Value;
                }

                public class SyntheticStructuredString : IIppStructuredString
                {
                    public string Text { get; set; } = string.Empty;
                    public SyntheticStructuredString() { }
                    public SyntheticStructuredString(string text) => Text = text;
                    public static SyntheticStructuredString Parse(string s) => new(s);
                    public static SyntheticStructuredString Parse(IEnumerable<string> s) => new(string.Join(",", s));
                    public override string ToString() => Text;
                }

                public class SyntheticStructuredStringSingle : IIppStructuredString
                {
                    public string Text { get; set; } = string.Empty;
                    public SyntheticStructuredStringSingle() { }
                    public SyntheticStructuredStringSingle(string text) => Text = text;
                    public static SyntheticStructuredStringSingle Parse(string s) => new(s);
                    public override string ToString() => Text;
                }

                [IppAttribute]
                public class SyntheticSmartEnumModel
                {
                    [IppAttribute("smart-enum")]
                    public SyntheticSmartEnum SmartEnum { get; set; }

                    [IppAttribute("nullable-smart-enum")]
                    public SyntheticSmartEnum? NullableSmartEnum { get; set; }

                    [IppAttribute("smart-enum-array")]
                    public SyntheticSmartEnum[]? SmartEnumArray { get; set; }

                    [IppAttribute("smart-enum-list")]
                    public List<SyntheticSmartEnum>? SmartEnumList { get; set; }

                    [IppAttribute("marked-smart-enum")]
                    public SyntheticMarkedSmartEnum MarkedSmartEnum { get; set; }

                    [IppAttribute("nullable-marked-smart-enum")]
                    public SyntheticMarkedSmartEnum? NullableMarkedSmartEnum { get; set; }

                    [IppAttribute("marked-smart-enum-array")]
                    public SyntheticMarkedSmartEnum[]? MarkedSmartEnumArray { get; set; }

                    [IppAttribute("structured-string")]
                    public SyntheticStructuredString? StructuredString { get; set; }

                    [IppAttribute("structured-string-array")]
                    public SyntheticStructuredString[]? StructuredStringArray { get; set; }

                    [IppAttribute("structured-string-single")]
                    public SyntheticStructuredStringSingle? StructuredStringSingle { get; set; }
                }
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var typeConverters = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedTypeConverters.g.cs"))?.ToString();
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();

        typeConverters.Should().NotBeNull();
        typeConverters.Should().Contain("mapper.CreateMap<string[], global::TestNamespace.SyntheticSmartEnum[]>");
        typeConverters.Should().Contain("mapper.CreateMap<global::TestNamespace.SyntheticSmartEnum[], string[]>");
        typeConverters.Should().Contain("mapper.CreateMap<string[], List<global::TestNamespace.SyntheticSmartEnum>>");
        typeConverters.Should().Contain("mapper.CreateMap<List<global::TestNamespace.SyntheticSmartEnum>, string[]>");
        typeConverters.Should().Contain("mapper.CreateMap<string[], IEnumerable<global::TestNamespace.SyntheticSmartEnum>>");
        typeConverters.Should().Contain("mapper.CreateMap<string[], IReadOnlyCollection<global::TestNamespace.SyntheticSmartEnum>>");

        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("ReadTestNamespace_SyntheticSmartEnumModel");
        modelMappers.Should().Contain("WriteTestNamespace_SyntheticSmartEnumModel");
    }

    [TestMethod]
    public void IppValueSpecialTypesAndFinishings_ShouldGenerateProperMappers()
    {
        var source = """
            using System;
            using System.Collections.Generic;
            using SharpIpp.Mapping;
            using SharpIpp.Protocol.Models;

            namespace TestNamespace;

            public enum SyntheticJobState
            {
                Pending = 3,
                Processing = 5,
                Completed = 9
            }

            public struct SyntheticSmartEnum : ISmartEnum
            {
                public string Value { get; set; }
                public SyntheticSmartEnum(string val) => Value = val;
                public override string ToString() => Value;
            }

            public class SyntheticClassSmartEnum : ISmartEnum
            {
                public string Value => "class-smart";
                public override string ToString() => Value;
            }

            public struct SyntheticMarkedSmartEnum : IMarkedSmartEnum
            {
                public string Value { get; set; }
                public bool IsMarked { get; set; }
                public SyntheticMarkedSmartEnum(string val, bool isMarked) { Value = val; IsMarked = isMarked; }
                public override string ToString() => Value;
            }

            public class SyntheticClassMarkedSmartEnum : IMarkedSmartEnum
            {
                public string Value => "class-marked";
                public bool IsMarked => true;
                public override string ToString() => Value;
            }

            public class SyntheticStructuredString : IIppStructuredString
            {
                public string Text { get; set; } = string.Empty;
                public SyntheticStructuredString() { }
                public SyntheticStructuredString(string text) => Text = text;
                public static SyntheticStructuredString Parse(string s) => new(s);
                public static SyntheticStructuredString Parse(IEnumerable<string> s) => new(string.Join(",", s));
                public override string ToString() => Text;
            }

            public class SyntheticStructuredStringSingle : IIppStructuredString
            {
                public string Text { get; set; } = string.Empty;
                public SyntheticStructuredStringSingle() { }
                public SyntheticStructuredStringSingle(string text) => Text = text;
                public static SyntheticStructuredStringSingle Parse(string s) => new(s);
                public override string ToString() => Text;
            }

            [IppAttribute(Name = "custom-collection-named")]
            public class SyntheticCollection : IIppCollection
            {
                [IppAttribute("item-name")]
                public string? ItemName { get; set; }
            }

            [IppAttribute]
            public class SyntheticIppValueModel
            {
                [IppAttribute("ipp-val-marked")]
                public IppValue<SyntheticMarkedSmartEnum>? IppValMarked { get; set; }

                [IppAttribute("ipp-val-class-marked")]
                public IppValue<SyntheticClassMarkedSmartEnum>? IppValClassMarked { get; set; }

                [IppAttribute("ipp-val-marked-arr")]
                public IppValue<SyntheticMarkedSmartEnum[]>? IppValMarkedArr { get; set; }

                [IppAttribute("ipp-val-smart")]
                public IppValue<SyntheticSmartEnum>? IppValSmart { get; set; }

                [IppAttribute("ipp-val-class-smart")]
                public IppValue<SyntheticClassSmartEnum>? IppValClassSmart { get; set; }

                [IppAttribute("ipp-val-smart-arr")]
                public IppValue<SyntheticSmartEnum[]>? IppValSmartArr { get; set; }

                [IppAttribute("ipp-val-struct-str")]
                public IppValue<SyntheticStructuredString>? IppValStructStr { get; set; }

                [IppAttribute("ipp-val-struct-str-arr")]
                public IppValue<SyntheticStructuredString[]>? IppValStructStrArr { get; set; }

                [IppAttribute("ipp-val-struct-str-single")]
                public IppValue<SyntheticStructuredStringSingle>? IppValStructStrSingle { get; set; }

                [IppAttribute("ipp-val-col")]
                public IppValue<SyntheticCollection>? IppValCol { get; set; }

                [IppAttribute("ipp-val-col-arr")]
                public IppValue<SyntheticCollection[]>? IppValColArr { get; set; }

                [IppAttribute("ipp-val-finishings-arr")]
                public IppValue<Finishings[]>? IppValFinishingsArr { get; set; }

                [IppAttribute("ipp-val-enum-arr")]
                public IppValue<SyntheticJobState[]>? IppValEnumArr { get; set; }

                [IppAttribute("ipp-val-uri-arr")]
                public IppValue<Uri[]>? IppValUriArr { get; set; }

                [IppAttribute("ipp-val-date-arr")]
                public IppValue<DateTimeOffset[]>? IppValDateArr { get; set; }

                [IppAttribute("ipp-val-single-enum")]
                public IppValue<SyntheticJobState>? IppValSingleEnum { get; set; }

                [IppAttribute("ipp-val-single-uri")]
                public IppValue<Uri>? IppValSingleUri { get; set; }

                [IppAttribute("ipp-val-range-tag", Tag.RangeOfInteger)]
                public IppValue<SharpIpp.Protocol.Models.Range>? IppValRangeTag { get; set; }

                [IppAttribute("ipp-val-range-notag")]
                public IppValue<SharpIpp.Protocol.Models.Range>? IppValRangeNoTag { get; set; }

                [IppAttribute("finishings-arr")]
                public Finishings[]? FinishingsArr { get; set; }

                [IppAttribute("enum-nonnull")]
                public SyntheticJobState EnumNonNull { get; set; }

                [IppAttribute("enum-null")]
                public SyntheticJobState? EnumNull { get; set; }

                [IppAttribute("enum-arr")]
                public SyntheticJobState[]? EnumArr { get; set; }

                [IppAttribute("lang-arr")]
                public StringWithLanguage[]? LangArr { get; set; }

                [IppAttribute("dict-arr")]
                public IDictionary<string, IppAttribute[]>[]? DictArr { get; set; }

                [IppAttribute("col-standalone")]
                public SyntheticCollection? ColStandalone { get; set; }

                [IppAttribute("col-arr")]
                public SyntheticCollection[]? ColArr { get; set; }

                [IppAttribute("int-nonnull")]
                public int IntNonNull { get; set; }

                [IppAttribute("date-nonnull")]
                public DateTimeOffset DateNonNull { get; set; }

                [IppAttribute("date-arr")]
                public DateTimeOffset[]? DateArr { get; set; }

                [IppAttribute("uri-prop")]
                public Uri? UriProp { get; set; }

                [IppAttribute("uri-arr")]
                public Uri[]? UriArr { get; set; }

                [IppAttribute("range-tag", Tag.RangeOfInteger)]
                public SharpIpp.Protocol.Models.Range? RangeTag { get; set; }

                [IppAttribute("range-tag-nonnull", Tag.RangeOfInteger)]
                public SharpIpp.Protocol.Models.Range RangeTagNonNull { get; set; }

                [IppAttribute("range-notag-nonnull")]
                public SharpIpp.Protocol.Models.Range RangeNoTagNonNull { get; set; }

                [IppAttribute("range-arr")]
                public SharpIpp.Protocol.Models.Range[]? RangeArr { get; set; }

                [IppAttribute("res-nonnull")]
                public Resolution ResNonNull { get; set; }

                [IppAttribute("res-arr")]
                public Resolution[]? ResArr { get; set; }

                [IppAttribute("octet-nonnull")]
                public OctetString OctetNonNull { get; set; }

                [IppAttribute("octet-arr")]
                public OctetString[]? OctetArr { get; set; }

                [IppAttribute(Tag = Tag.BegCollection)]
                public string[]? ExplicitBegColArr { get; set; }

                [IppAttribute(Tag = Tag.BegCollection)]
                public string? ExplicitBegColSingle { get; set; }

                [IppAttribute("bool-val")]
                public bool BoolVal { get; set; }

                [IppAttribute("bool-nullable")]
                public bool? BoolNullable { get; set; }

                [IppAttribute("bool-arr")]
                public bool[]? BoolArr { get; set; }

                [IppAttribute("obj-arr")]
                public object[]? ObjArr { get; set; }

                [IppAttribute("custom-unmapped")]
                public SharpIpp.SharpIppClient? UnmappedObject { get; set; }

                [IppAttribute("nullables-in-arr")]
                public int?[]? NullablesInArr { get; set; }

                [IppAttribute("nullables-in-list")]
                public List<int?>? NullablesInList { get; set; }

                [IppAttribute("struct-in-list")]
                public List<SyntheticStructuredString>? StructInList { get; set; }

                [IppAttribute("col-in-list")]
                public List<SyntheticCollection>? ColInList { get; set; }

                [IppAttribute("readonly-coll")]
                public IReadOnlyCollection<string>? ReadOnlyColl { get; set; }

                [IppAttribute("ilist-coll")]
                public IList<int>? IListColl { get; set; }

                [IppAttribute("ienumerable-coll")]
                public IEnumerable<string>? IEnumerableColl { get; set; }
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();

        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("ReadTestNamespace_SyntheticIppValueModel");
        modelMappers.Should().Contain("WriteTestNamespace_SyntheticIppValueModel");
        modelMappers.Should().Contain("global::SharpIpp.Protocol.Models.Finishings.None");
    }

    [TestMethod]
    public void ComprehensiveRequests_ShouldGenerateAllRequestMappings()
    {
        var source = """
            using System.IO;
            using SharpIpp.Mapping;
            using SharpIpp.Models.Requests;
            using SharpIpp.Protocol.Models;

            namespace TestNamespace;

            [IppRequest(IppOperation.PrintJob)]
            public class FullFeaturedRequest
            {
                public IppVersion Version { get; set; } = new();
                public int RequestId { get; set; } = 1;
                public Stream? Document { get; set; }
                public PrintJobOperationAttributes? OperationAttributes { get; set; }
                public JobTemplateAttributes? JobTemplateAttributes { get; set; }
                public DocumentTemplateAttributes? DocumentTemplateAttributes { get; set; }
                public DocumentDescriptionAttributes? DocumentDescriptionAttributes { get; set; }
                public PrinterDescriptionAttributes? PrinterAttributes { get; set; }
                public JobStatusAttributes? JobAttributes { get; set; }
                public DocumentAttributes? DocumentAttributes { get; set; }
            }

            [IppRequest((IppOperation)8888)]
            public class CustomOpRequest
            {
                public IppVersion Version { get; set; } = new();
                public int RequestId { get; set; } = 1;
            }

            [IppRequest]
            public class EmptyArgRequest
            {
                public int RequestId { get; set; }
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();

        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("ReadTestNamespace_FullFeaturedRequest");
        modelMappers.Should().Contain("WriteTestNamespace_FullFeaturedRequest");
        modelMappers.Should().Contain("ReadTestNamespace_CustomOpRequest");
        modelMappers.Should().Contain("WriteTestNamespace_CustomOpRequest");
        modelMappers.Should().Contain("((global::SharpIpp.Protocol.Models.IppOperation)8888)");
        modelMappers.Should().NotContain("ReadTestNamespace_EmptyArgRequest");
    }

    [TestMethod]
    public void ComprehensiveResponses_ShouldGenerateAllResponseMappings()
    {
        var source = """
            using System.Collections.Generic;
            using SharpIpp.Mapping;
            using SharpIpp.Models.Responses;
            using SharpIpp.Protocol.Models;

            namespace TestNamespace;

            [IppResponse(IppOperation.GetJobs)]
            public class FullFeaturedResponse1
            {
                public IppVersion Version { get; set; } = new();
                public int RequestId { get; set; } = 1;
                public IppStatusCode StatusCode { get; set; } = IppStatusCode.SuccessfulOk;
                public OperationAttributes? OperationAttributes { get; set; }
                public int[]? JobIds { get; set; }
                public JobState[]? OutputDeviceJobStates { get; set; }
                public SharpIpp.Models.Responses.JobAttributes? JobAttributes { get; set; }
                public JobDescriptionAttributes[]? JobsAttributes { get; set; }
                public DocumentAttributes? DocumentAttributes { get; set; }
                public List<DocumentAttributes>? Documents { get; set; }
                public PrinterDescriptionAttributes? PrinterAttributes { get; set; }
                public PrinterDescriptionAttributes[]? PrintersAttributes { get; set; }
                public int[]? PrinterResourceIds { get; set; }
                public ResourceStatusAttributes? ResourceAttributes { get; set; }
                public ResourceDescriptionAttributes[]? ResourcesAttributes { get; set; }
                public SystemStatusAttributes? SystemAttributes { get; set; }
                public SystemDescriptionAttributes? SystemDescriptionAttributes { get; set; }
                public SubscriptionDescriptionAttributes? SubscriptionAttributes { get; set; }
                public SubscriptionDescriptionAttributes[]? SubscriptionsAttributes { get; set; }
            }

            [IppResponse]
            public class FullFeaturedResponse2
            {
                public IppVersion Version { get; set; } = new();
                public int RequestId { get; set; } = 1;
                public IppStatusCode StatusCode { get; set; } = IppStatusCode.SuccessfulOk;
                public ValidateOperationAttributes? OperationAttributes { get; set; }
                public JobDescriptionAttributes? JobAttributes { get; set; }
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();

        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("ReadTestNamespace_FullFeaturedResponse1");
        modelMappers.Should().Contain("WriteTestNamespace_FullFeaturedResponse1");
        modelMappers.Should().Contain("ReadTestNamespace_FullFeaturedResponse2");
        modelMappers.Should().Contain("WriteTestNamespace_FullFeaturedResponse2");
        modelMappers.Should().Contain("dst.JobIds =");
        modelMappers.Should().Contain("dst.OutputDeviceJobStates =");
        modelMappers.Should().Contain("dst.PrinterResourceIds =");
    }

    [TestMethod]
    public void AttributeVariationsAndTypeFilters_ShouldCoverAllBranches()
    {
        var source = """
            using SharpIpp.Mapping;
            using SharpIpp.Protocol.Models;

            namespace TestNamespace;

            public interface ICustomInterface
            {
                string ExplicitProp { get; }
            }

            [IppAttribute]
            public class SyntheticAttributeVariationsModel : ICustomInterface
            {
                string ICustomInterface.ExplicitProp => "explicit";

                [IppAttribute("explicit-pos", Tag.Keyword, 4)]
                public string? ExplicitPos { get; set; }

                [IppAttribute("order-only", 7)]
                public string? OrderOnly { get; set; }

                [IppAttribute(Name = "named-param", Tag = Tag.Uri, Order = 2, DefaultValue = "defaultVal")]
                public string? NamedParam { get; set; }

                [IppAttribute(Tag = Tag.Unsupported)]
                public string? UnsupportedTag { get; set; }

                [IppAttribute("exact-col")]
                public IIppCollection? ExactCol { get; set; }

                [IppAttribute("derived-col")]
                public DerivedColModel? DerivedCol { get; set; }
            }

            public class BaseColModel : IIppCollection { }
            public class DerivedColModel : BaseColModel { }

            [IppAttribute]
            public abstract class SyntheticAbstractModel
            {
                [IppAttribute("abstract-prop")]
                public string? AbstractProp { get; set; }
            }

            [IppAttribute]
            public class SyntheticGenericModel<T>
            {
                [IppAttribute("generic-prop")]
                public T? GenericProp { get; set; }
            }

            public struct SyntheticSelfConv
            {
                public int Value { get; set; }
                public static implicit operator SyntheticSelfConv(SyntheticSelfConv x) => x;
                public static implicit operator IppValue<int>(SyntheticSelfConv x) => default!;
            }

            public struct GenericConv<T>
            {
                public static implicit operator T(GenericConv<T> x) => default!;
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();

        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("ReadTestNamespace_SyntheticAttributeVariationsModel");
        modelMappers.Should().Contain("WriteTestNamespace_SyntheticAttributeVariationsModel");
        modelMappers.Should().NotContain("ReadTestNamespace_SyntheticAbstractModel");
        modelMappers.Should().NotContain("ReadTestNamespace_SyntheticGenericModel");
    }

    [TestMethod]
    public void ShortEnumsAndCollectionsAndFinishings_ShouldCoverAllBranches()
    {
        var source = """"""
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using SharpIpp.Mapping;
            using SharpIpp.Protocol.Models;

            namespace SharpIpp.Protocol.Models
            {
                public enum SyntheticShortEnum : short
                {
                    First = 1,
                    Second = 2
                }
            }

            namespace TestNamespace
            {
                public class SyntheticNestedCol : IIppCollection
                {
                    [IppAttribute("nested-text")]
                    public string? NestedText { get; set; }
                }

                public class SyntheticCoverStructuredString : IIppStructuredString
                {
                    public string Text { get; set; } = string.Empty;
                    public SyntheticCoverStructuredString() { }
                    public SyntheticCoverStructuredString(string text) => Text = text;
                    public static SyntheticCoverStructuredString Parse(string s) => new(s);
                    public static SyntheticCoverStructuredString Parse(IEnumerable<string> s) => new(string.Join(",", s));
                    public override string ToString() => Text;
                }

                public class SyntheticCoverStructuredStringSingle : IIppStructuredString
                {
                    public string Text { get; set; } = string.Empty;
                    public SyntheticCoverStructuredStringSingle() { }
                    public SyntheticCoverStructuredStringSingle(string text) => Text = text;
                    public static SyntheticCoverStructuredStringSingle Parse(string s) => new(s);
                    public override string ToString() => Text;
                }

                public class SyntheticClassSmartEnum : ISmartEnum
                {
                    public string Value => "class-smart";
                    public override string ToString() => Value;
                }

                [IppAttribute]
                public class SyntheticBaseAnnotatedModel
                {
                    [IppAttribute("base-prop")]
                    public string? BaseProp { get; set; }
                }

                [IppAttribute]
                public class SyntheticDerivedAnnotatedModel : SyntheticBaseAnnotatedModel
                {
                    [IppAttribute("short-enum")]
                    public SyntheticShortEnum ShortEnum { get; set; }

                    [IppAttribute("short-enum-array")]
                    public SyntheticShortEnum[]? ShortEnumArray { get; set; }

                    [IppAttribute("short-enum-wrapped")]
                    public IppValue<SyntheticShortEnum>? ShortEnumWrapped { get; set; }

                    [IppAttribute("finishings-direct")]
                    public Finishings[]? FinishingsDirect { get; set; }

                    [IppAttribute("finishings-wrapped")]
                    public IppValue<Finishings[]>? FinishingsWrapped { get; set; }

                    [IppAttribute("col-array")]
                    public SyntheticNestedCol[]? ColArray { get; set; }

                    [IppAttribute("col-list")]
                    public List<SyntheticNestedCol>? ColList { get; set; }

                    [IppAttribute("col-single")]
                    public SyntheticNestedCol? ColSingle { get; set; }

                    [IppAttribute("col-wrapped")]
                    public IppValue<SyntheticNestedCol>? ColWrapped { get; set; }

                    [IppAttribute("col-array-wrapped")]
                    public IppValue<SyntheticNestedCol[]>? ColArrayWrapped { get; set; }

                    [IppAttribute("structured-array-direct")]
                    public SyntheticCoverStructuredString[]? StructuredArrayDirect { get; set; }

                    [IppAttribute("structured-list-direct")]
                    public List<SyntheticCoverStructuredString>? StructuredListDirect { get; set; }

                    [IppAttribute("structured-single-direct")]
                    public SyntheticCoverStructuredString? StructuredSingleDirect { get; set; }

                    [IppAttribute("structured-wrapped")]
                    public IppValue<SyntheticCoverStructuredString>? StructuredWrapped { get; set; }

                    [IppAttribute("structured-single-wrapped")]
                    public IppValue<SyntheticCoverStructuredStringSingle>? StructuredSingleWrapped { get; set; }

                    [IppAttribute("class-smart-enum")]
                    public SyntheticClassSmartEnum? ClassSmartEnum { get; set; }

                    [IppAttribute("non-null-default", DefaultValue = "def")]
                    public int NonNullDefault { get; set; }

                    [IppAttribute("pos-unsupported", Tag.Unsupported)]
                    public string? PosUnsupported { get; set; }

                    [IppAttribute("pos-unknown-tag", (Tag)9999)]
                    public string? PosUnknownTag { get; set; }

                    [IppAttribute("named-unknown-tag", Tag = (Tag)9999)]
                    public string? NamedUnknownTag { get; set; }

                    [IppAttribute("neg-order", -5)]
                    public string? NegOrder { get; set; }
                }
            }
            """""";

        var (_, generatedTrees, _) = RunGenerator(source);
        var typeConverters = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedTypeConverters.g.cs"))?.ToString();
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();

        typeConverters.Should().NotBeNull();
        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("ReadTestNamespace_SyntheticDerivedAnnotatedModel");
        modelMappers.Should().Contain("WriteTestNamespace_SyntheticDerivedAnnotatedModel");
    }

    [TestMethod]
    public void EdgeCaseScenarios_ShouldCoverRemainingBranches()
    {
        var source = """
            using SharpIpp.Mapping;
            using SharpIpp.Protocol.Models;
            using System.Collections.Generic;

            namespace SharpIpp.Protocol.Models
            {
                public enum Tag { One = 1 }
                public enum SectionTag { Two = 2 }

                public static class IppAttributeNames
                {
                    public const int NonStringField = 123;
                    public const string ValidField = "valid-field";
                }
            }

            namespace TestNamespace;

            // Empty request attribute (0 args)
            [IppRequest]
            public class EmptyRequestModel { }

            // Parameterless config attribute
            [SharpIppMapperConfig]
            public class DefaultConfigMapper { }

            // Custom conversion with arrays and IppValue
            public class CustomConversionModel
            {
                public static implicit operator int[](CustomConversionModel src) => new int[0];
                public static implicit operator CustomConversionModel(int[] src) => new CustomConversionModel();
                public static implicit operator IppValue(CustomConversionModel src) => default;
                public static implicit operator CustomConversionModel(IppValue src) => new CustomConversionModel();
            }

            public class EnumerableParseStructuredString : IIppStructuredString
            {
                public static EnumerableParseStructuredString Parse(IEnumerable<string> values) => new();
                public override string ToString() => "";
            }

            public class ArrayParseStructuredString : IIppStructuredString
            {
                public static ArrayParseStructuredString Parse(string[] values) => new();
                public static void Parse() { } // 0 args
                public override string ToString() => "";
            }

            public class VariationsStructuredString : IIppStructuredString
            {
                public void Parse(string[] a) { } // non-static
                public static void Parse(int[] a) { } // non-string array
                public static void Parse(IEnumerable<int> a) { } // non-string enumerable
                public static void Parse(string a, int b) { } // 2 parameters
                public override string ToString() => "";
            }

            public class FullEdgeCaseModel
            {
                [IppAttribute("int-order-pos", 10)]
                public string? IntOrderPos { get; set; }

                [IppAttribute("int-order-neg", -5)]
                public string? IntOrderNeg { get; set; }

                [IppAttribute("named-unsupported-tag", Tag = Tag.Unsupported)]
                public string? NamedUnsupportedTag { get; set; }

                [IppAttribute("named-unknown-tag", Tag = (Tag)9999)]
                public string? NamedUnknownTag { get; set; }

                [IppAttribute("named-neg-order", Order = -1)]
                public string? NamedNegOrder { get; set; }

                [IppAttribute("nullable-with-default", DefaultValue = "def1")]
                public string? NullableWithDefault { get; set; }

                [IppAttribute("non-nullable-with-default", DefaultValue = "def2")]
                public string NonNullableWithDefault { get; set; } = "def2";

                [IppAttribute("int-with-default", DefaultValue = "0")]
                public int? IntWithDefault { get; set; }

                [IppAttribute("enumerable-ss")]
                public IppValue<EnumerableParseStructuredString>? EnumerableSs { get; set; }

                [IppAttribute("array-ss")]
                public IppValue<ArrayParseStructuredString>? ArraySs { get; set; }

                [IppAttribute("list-ippvalue")]
                public IppValue<List<int>>? ListIppValue { get; set; }

                [IppAttribute("readonly-coll-ippvalue")]
                public IppValue<IReadOnlyCollection<string>>? ReadOnlyCollIppValue { get; set; }

                [IppAttribute("ienum-ippvalue")]
                public IppValue<IEnumerable<int>>? IEnumIppValue { get; set; }

                [IppAttribute("ilist-ippvalue")]
                public IppValue<IList<int>>? IListIppValue { get; set; }

                [IppAttribute("single-ippval")]
                public IppValue<int>? SingleIppValue { get; set; }
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        generatedTrees.Should().NotBeEmpty();
    }

    [TestMethod]
    public void CustomCompilationExecute_ShouldCoverIsolatedBranches()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText("""
            namespace SharpIpp.Protocol.Models
            {
                public enum Tag { None = 0 }
                public enum SectionTag { None = 0 }
                public enum SyntheticEnum { Custom = 1 }

                public static class IppAttributeNames
                {
                    public const int NumericConst = 123;
                    public const string StringConst = "str";
                    public static void Method() { }
                }
            }

            public enum GlobalEnum { A, B }

            namespace SharpIpp.Mapping
            {
                [System.AttributeUsage(System.AttributeTargets.Class)]
                public class IppRequestAttribute : System.Attribute
                {
                    public IppRequestAttribute() { }
                }
            }

            namespace TestNamespace
            {
                [SharpIpp.Mapping.IppRequest]
                public class EmptyReqModel { }
            }
            """);

        var comp = CSharpCompilation.Create(
            "IsolatedAssembly",
            new[] { syntaxTree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });

        var types = comp.SyntaxTrees
            .SelectMany(st => st.GetRoot().DescendantNodes().OfType<Microsoft.CodeAnalysis.CSharp.Syntax.BaseTypeDeclarationSyntax>())
            .Select(b => comp.GetSemanticModel(b.SyntaxTree).GetDeclaredSymbol(b))
            .OfType<INamedTypeSymbol>()
            .ToImmutableArray();

        var generator = new MapperSourceGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver.RunGeneratorsAndUpdateCompilation(comp, out _, out _);
    }
}