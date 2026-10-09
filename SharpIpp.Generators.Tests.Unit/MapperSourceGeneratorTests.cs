using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Generators;
using SharpIpp.Generators.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpIpp.Generators.Tests.Unit;

[TestClass]
[ExcludeFromCodeCoverage]
public partial class MapperSourceGeneratorTests
{
    private static (Compilation OutputCompilation, IEnumerable<SyntaxTree> GeneratedTrees, IEnumerable<Diagnostic> Diagnostics) RunGenerator(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.Latest));

        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
            .Select(a => MetadataReference.CreateFromFile(a.Location))
            .Cast<MetadataReference>()
            .Append(MetadataReference.CreateFromFile(typeof(SharpIpp.SharpIppClient).Assembly.Location))
            .ToList();

        var compilation = CSharpCompilation.Create(
            "TestAssembly",
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, nullableContextOptions: NullableContextOptions.Enable));

        var generator = new MapperSourceGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diagnostics);

        var runResult = driver.GetRunResult();
        var generatedTrees = runResult.GeneratedTrees;

        return (outputCompilation, generatedTrees, diagnostics);
    }

    [TestMethod]
    public void GeneratedClasses_ShouldContain_ExcludeFromCodeCoverage_And_GeneratedCode()
    {
        // Arrange: Minimal compilation with one synthetic annotated model
        var source = """
            using SharpIpp.Mapping;

            namespace TestNamespace;

            [IppAttribute]
            public class SyntheticJobModel
            {
                [IppAttribute("job-name")]
                public string? JobName { get; set; }
            }
            """;

        // Act
        var (_, generatedTrees, _) = RunGenerator(source);
        var generatedSources = generatedTrees.ToDictionary(t => System.IO.Path.GetFileName(t.FilePath), t => t.ToString());

        // Assert
        generatedSources.Should().ContainKey("GeneratedTypeConverters.g.cs");
        generatedSources.Should().ContainKey("GeneratedMapperRegistry.g.cs");
        generatedSources.Should().ContainKey("GeneratedModelMappers.g.cs");

        foreach (var (fileName, content) in generatedSources)
        {
            content.Should().Contain("[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]",
                because: $"{fileName} must be excluded from code coverage");
            content.Should().Contain("[global::System.CodeDom.Compiler.GeneratedCode(\"SharpIpp.Generators\", \"1.0.0.0\")]",
                because: $"{fileName} must declare GeneratedCode attribute");
        }
    }

    [TestMethod]
    public void ModelWithIppAttribute_ShouldGenerateReadAndWriteMethods()
    {
        // Arrange
        var source = """
            using SharpIpp.Mapping;

            namespace TestNamespace;

            [IppAttribute]
            public class SyntheticPrintDocument
            {
                [IppAttribute("document-name")]
                public string? DocumentName { get; set; }

                [IppAttribute("copies")]
                public int? Copies { get; set; }
            }
            """;

        // Act
        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();

        // Assert
        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("public static global::TestNamespace.SyntheticPrintDocument ReadTestNamespace_SyntheticPrintDocument(");
        modelMappers.Should().Contain("public static List<IppAttribute> WriteTestNamespace_SyntheticPrintDocument(");
        modelMappers.Should().Contain("global::SharpIpp.Protocol.Models.IppAttributeNames.DocumentName");
        modelMappers.Should().Contain("global::SharpIpp.Protocol.Models.IppAttributeNames.Copies");
    }

    [TestMethod]
    public void EnumInProtocolModels_ShouldGenerateTypeConverters()
    {
        // Arrange
        var source = """
            namespace SharpIpp.Protocol.Models;

            public enum SyntheticJobState
            {
                Pending = 3,
                Processing = 5,
                Completed = 9
            }
            """;

        // Act
        var (_, generatedTrees, _) = RunGenerator(source);
        var typeConverters = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedTypeConverters.g.cs"))?.ToString();

        // Assert
        typeConverters.Should().NotBeNull();
        typeConverters.Should().Contain("mapper.CreateIppMap<int, global::SharpIpp.Protocol.Models.SyntheticJobState>((src, _) => (global::SharpIpp.Protocol.Models.SyntheticJobState)src);");
        typeConverters.Should().Contain("mapper.CreateIppMap<global::SharpIpp.Protocol.Models.SyntheticJobState, int>((src, _) => (int)src);");
    }

    [TestMethod]
    public void CollectionModel_ShouldGenerateNoValueHandling()
    {
        // Arrange
        var source = """
            using SharpIpp.Mapping;
            using SharpIpp.Protocol.Models;

            namespace TestNamespace;

            [IppAttribute("media-col")]
            public class SyntheticMediaCollection : IIppCollection
            {
                [IppAttribute("media-size")]
                public string? MediaSize { get; set; }
            }
            """;

        // Act
        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();

        // Assert
        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("ReadTestNamespace_SyntheticMediaCollection");
        modelMappers.Should().Contain("WriteTestNamespace_SyntheticMediaCollection");
    }

    [TestMethod]
    public void RequestAndResponse_ShouldGenerateMessageMappers()
    {
        // Arrange
        var source = """
            using SharpIpp.Mapping;
            using SharpIpp.Protocol.Models;

            namespace TestNamespace;

            [IppRequest(IppOperation.PrintJob)]
            public class SyntheticPrintJobRequest
            {
                [IppAttribute("job-name")]
                public string? JobName { get; set; }
            }

            [IppResponse(IppOperation.PrintJob)]
            public class SyntheticPrintJobResponse
            {
                [IppAttribute("job-id")]
                public int? JobId { get; set; }
            }
            """;

        // Act
        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();
        var registry = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedMapperRegistry.g.cs"))?.ToString();

        // Assert
        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("ReadTestNamespace_SyntheticPrintJobRequest");
        modelMappers.Should().Contain("WriteTestNamespace_SyntheticPrintJobRequest");
        modelMappers.Should().Contain("ReadTestNamespace_SyntheticPrintJobResponse");
        modelMappers.Should().Contain("WriteTestNamespace_SyntheticPrintJobResponse");

        registry.Should().NotBeNull();
        registry.Should().Contain("mapper.CreateMap<global::TestNamespace.SyntheticPrintJobRequest, global::SharpIpp.Protocol.Models.IppRequestMessage>");
        registry.Should().Contain("mapper.CreateMap<global::TestNamespace.SyntheticPrintJobResponse, global::SharpIpp.Protocol.Models.IppResponseMessage>");
    }

    [TestMethod]
    public void ModelInheritance_ShouldCallBaseReadAndWrite()
    {
        // Arrange
        var source = """
            using SharpIpp.Mapping;

            namespace TestNamespace;

            [IppAttribute]
            public class SyntheticBaseModel
            {
                [IppAttribute("base-prop")]
                public string? BaseProp { get; set; }
            }

            [IppAttribute]
            public class SyntheticDerivedModel : SyntheticBaseModel
            {
                [IppAttribute("derived-prop")]
                public string? DerivedProp { get; set; }
            }
            """;

        // Act
        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();

        // Assert
        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("ReadTestNamespace_SyntheticBaseModel(src, dst, map);");
        modelMappers.Should().Contain("WriteTestNamespace_SyntheticBaseModel(src, dst, map);");
    }

    [TestMethod]
    public async Task RoslynTestingVerifier_ShouldSucceedWithZeroCompilerErrors()
    {
        // Test using Microsoft.CodeAnalysis.CSharp.SourceGenerators.Testing verifier
        var source = """
            using SharpIpp.Mapping;

            namespace TestNamespace;

            [IppAttribute]
            public class SyntheticVerifiedModel
            {
                [IppAttribute("job-name")]
                public string? JobName { get; set; }
            }
            """;

        var test = new CSharpSourceGeneratorVerifier<MapperSourceGenerator>.Test
        {
            TestState =
            {
                Sources = { source }
            }
        };

        var (_, generatedTrees, _) = RunGenerator(source);
        foreach (var tree in generatedTrees)
        {
            var hintName = System.IO.Path.GetFileName(tree.FilePath);
            test.TestState.GeneratedSources.Add((typeof(MapperSourceGenerator), hintName, SourceText.From(tree.ToString(), Encoding.UTF8)));
        }

        // Run verification (checks both source generator matching and compiles the output to verify 0 compiler errors)
        await test.RunAsync();
    }

    [TestMethod]
    public void StructuredStringModel_ShouldGenerateDirectParseAndNoValueHandling()
    {
        var source = """
            using SharpIpp.Mapping;
            using SharpIpp.Protocol.Models;

            namespace TestNamespace;

            [IppAttribute]
            public class SyntheticDocModel
            {
                [IppAttribute("document-metadata")]
                public DocumentMetadata? Metadata { get; set; }

                [IppAttribute("printer-alert")]
                public PrinterAlert[]? Alerts { get; set; }
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();

        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("DocumentMetadata.Parse(");
    }
    [TestMethod]
    public void ConfiguredMapper_ShouldRegisterInMapperRegistry()
    {
        var source = """
            using SharpIpp.Mapping;

            namespace TestNamespace;

            [MapperConfiguration(Order = 5)]
            public class CustomConfiguredMapper
            {
            }

            [MapperConfiguration(10)]
            public class AnotherConfiguredMapper
            {
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var registry = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedMapperRegistry.g.cs"))?.ToString();

        registry.Should().NotBeNull();
        registry.Should().Contain("global::TestNamespace.CustomConfiguredMapper.Configure(mapper);");
        registry.Should().Contain("global::TestNamespace.AnotherConfiguredMapper.Configure(mapper);");
    }

    [TestMethod]
    public void ConfiguredMapper_ShouldSkipHandledModels_FromGeneratedModelMappersAndRegistry()
    {
        var source = """
            using System.Collections.Generic;
            using SharpIpp.Mapping;
            using SharpIpp.Protocol.Models;

            namespace TestNamespace;

            [IppAttribute]
            public class SyntheticHandledModel : IIppCollection
            {
                [IppAttribute("dummy-attr")]
                public string? DummyAttr { get; set; }
            }

            [IppAttribute]
            public class SyntheticUnhandledModel : IIppCollection
            {
                [IppAttribute("normal-attr")]
                public string? NormalAttr { get; set; }
            }

            [MapperConfiguration(1, typeof(SyntheticHandledModel))]
            public static class CustomModelMapper
            {
                public static void Configure(IMapperConstructor mapper)
                {
                    mapper.CreateMap<IDictionary<string, IppAttribute[]>, SyntheticHandledModel>((src, map) => new SyntheticHandledModel());
                }
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();
        var registry = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedMapperRegistry.g.cs"))?.ToString();

        modelMappers.Should().NotBeNull();
        modelMappers.Should().NotContain("ReadTestNamespace_SyntheticHandledModel");
        modelMappers.Should().NotContain("WriteTestNamespace_SyntheticHandledModel");
        modelMappers.Should().Contain("ReadTestNamespace_SyntheticUnhandledModel");
        modelMappers.Should().Contain("WriteTestNamespace_SyntheticUnhandledModel");

        registry.Should().NotBeNull();
        registry.Should().NotContain("mapper.CreateMap<IDictionary<string, IppAttribute[]>, global::TestNamespace.SyntheticHandledModel>");
        registry.Should().Contain("mapper.CreateMap<IDictionary<string, IppAttribute[]>, global::TestNamespace.SyntheticUnhandledModel>");
        registry.Should().Contain("global::TestNamespace.CustomModelMapper.Configure(mapper);");
    }

    [TestMethod]
    public void ConfiguredMapper_AutoDetectsHandledModelsFromCreateMapInvocations()
    {
        var source = """
            using System.Collections.Generic;
            using SharpIpp.Mapping;
            using SharpIpp.Protocol.Models;

            namespace TestNamespace;

            [IppAttribute]
            public class SyntheticInferredModel : IIppCollection
            {
                [IppAttribute("inferred-attr")]
                public string? InferredAttr { get; set; }
            }

            [IppAttribute]
            public class SyntheticOtherModel : IIppCollection
            {
                [IppAttribute("other-attr")]
                public string? OtherAttr { get; set; }
            }

            [MapperConfiguration(1)]
            public static class InferredModelMapper
            {
                public static void Configure(IMapperConstructor mapper)
                {
                    mapper.CreateMap<IDictionary<string, IppAttribute[]>, SyntheticInferredModel>((src, map) => new SyntheticInferredModel());
                }
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();
        var registry = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedMapperRegistry.g.cs"))?.ToString();

        modelMappers.Should().NotBeNull();
        modelMappers.Should().NotContain("ReadTestNamespace_SyntheticInferredModel");
        modelMappers.Should().NotContain("WriteTestNamespace_SyntheticInferredModel");
        modelMappers.Should().Contain("ReadTestNamespace_SyntheticOtherModel");
        modelMappers.Should().Contain("WriteTestNamespace_SyntheticOtherModel");

        registry.Should().NotBeNull();
        registry.Should().NotContain("mapper.CreateMap<IDictionary<string, IppAttribute[]>, global::TestNamespace.SyntheticInferredModel>");
        registry.Should().Contain("mapper.CreateMap<IDictionary<string, IppAttribute[]>, global::TestNamespace.SyntheticOtherModel>");
        registry.Should().Contain("global::TestNamespace.InferredModelMapper.Configure(mapper);");
    }

    [TestMethod]
    public void SectionAttribute_ShouldGenerateSectionMapper()
    {
        var source = """
            using SharpIpp.Mapping;
            using SharpIpp.Protocol.Models;

            namespace TestNamespace;

            [IppSection(SectionTag.JobAttributesTag)]
            public class SyntheticJobSection
            {
                [IppAttribute("job-name")]
                public string? JobName { get; set; }
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();

        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("ReadTestNamespace_SyntheticJobSection");
        modelMappers.Should().Contain("WriteTestNamespace_SyntheticJobSection");

        var registry = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedMapperRegistry.g.cs"))?.ToString();
        registry.Should().NotBeNull();
        registry.Should().Contain("dst.JobAttributes");
    }

    [TestMethod]
    public void ConversionOperator_ShouldGenerateTypeMap()
    {
        var source = """
            namespace TestNamespace;

            public struct CustomConvertedType
            {
                public int Value { get; set; }

                public static implicit operator int(CustomConvertedType c) => c.Value;
                public static explicit operator CustomConvertedType(int v) => new CustomConvertedType { Value = v };
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var typeConverters = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedTypeConverters.g.cs"))?.ToString();

        typeConverters.Should().NotBeNull();
        typeConverters.Should().Contain("mapper.CreateIppMap<global::TestNamespace.CustomConvertedType, int>");
        typeConverters.Should().Contain("mapper.CreateIppMap<int, global::TestNamespace.CustomConvertedType>");
    }

    [TestMethod]
    public void ConversionOperator_WithReferenceType_ShouldGenerateArrayAndListMaps()
    {
        var source = """
            using System.Collections.Generic;

            namespace TestNamespace;

            public class CustomRefModel
            {
                public string Value { get; set; } = string.Empty;
            }

            public struct CustomValueToRefModel
            {
                public string Text { get; set; }

                public static implicit operator CustomRefModel(CustomValueToRefModel src) => new CustomRefModel { Value = src.Text };
                public static explicit operator CustomValueToRefModel(CustomRefModel src) => new CustomValueToRefModel { Text = src.Value };
            }
            """;

        var (_, generatedTrees, diagnostics) = RunGenerator(source);
        diagnostics.Should().NotContain(d => d.Severity == DiagnosticSeverity.Error);

        var typeConverters = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedTypeConverters.g.cs"))?.ToString();
        typeConverters.Should().NotBeNull();

        // Reference destination type: CustomRefModel is a reference type (lines 506-527)
        typeConverters.Should().Contain("mapper.CreateMap<global::TestNamespace.CustomValueToRefModel[], global::TestNamespace.CustomRefModel[]>((src, _) =>");
        typeConverters.Should().Contain("mapper.CreateMap<global::TestNamespace.CustomValueToRefModel[], List<global::TestNamespace.CustomRefModel>>((src, _) =>");
        typeConverters.Should().Contain("var list = new List<global::TestNamespace.CustomRefModel>(src.Length);");
        typeConverters.Should().Contain("var item = (global::TestNamespace.CustomRefModel)src[i];");
        typeConverters.Should().Contain("if (item != null) list.Add(item);");
        typeConverters.Should().Contain("return list.ToArray();");
        typeConverters.Should().Contain("return list;");
        typeConverters.Should().Contain("mapper.CreateMap<global::TestNamespace.CustomValueToRefModel[], IEnumerable<global::TestNamespace.CustomRefModel>>((src, map) => map.Map<global::TestNamespace.CustomRefModel[]>(src));");
        typeConverters.Should().Contain("mapper.CreateMap<global::TestNamespace.CustomValueToRefModel[], IReadOnlyCollection<global::TestNamespace.CustomRefModel>>((src, map) => map.Map<global::TestNamespace.CustomRefModel[]>(src));");
    }

    [TestMethod]
    public void PropertyWithoutAttributeName_ShouldDeriveKebabCaseName()
    {
        var source = """
            using SharpIpp.Mapping;

            namespace TestNamespace;

            [IppAttribute]
            public class SyntheticKebabModel
            {
                [IppAttribute]
                public string? DocumentFormat { get; set; }

                [IppAttribute]
                public int? JobPriority { get; set; }
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();

        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("global::SharpIpp.Protocol.Models.IppAttributeNames.DocumentFormat");
        modelMappers.Should().Contain("global::SharpIpp.Protocol.Models.IppAttributeNames.JobPriority");
    }

    [TestMethod]
    public void ComplexCollectionsAndIppValue_ShouldGenerateProperMappers()
    {
        var source = """
            using System;
            using System.Collections.Generic;
            using SharpIpp.Mapping;
            using SharpIpp.Protocol.Models;

            namespace TestNamespace;

            [IppAttribute]
            public class SyntheticComplexModel
            {
                [IppAttribute("int-list")]
                public List<int>? IntList { get; set; }

                [IppAttribute("string-array")]
                public string[]? StringArray { get; set; }

                [IppAttribute("date-prop")]
                public DateTimeOffset? DateProp { get; set; }

                [IppAttribute("uri-prop")]
                public Uri? UriProp { get; set; }

                [IppAttribute("range-prop")]
                public SharpIpp.Protocol.Models.Range? RangeProp { get; set; }

                [IppAttribute("resolution-prop")]
                public Resolution? ResolutionProp { get; set; }

                [IppAttribute("octet-prop")]
                public OctetString? OctetProp { get; set; }

                [IppAttribute("lang-prop")]
                public StringWithLanguage? LangProp { get; set; }

                [IppAttribute("ipp-val-int")]
                public IppValue<int>? IppValInt { get; set; }

                [IppAttribute("ipp-val-str")]
                public IppValue<string>? IppValStr { get; set; }
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var modelMappers = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedModelMappers.g.cs"))?.ToString();

        modelMappers.Should().NotBeNull();
        modelMappers.Should().Contain("ReadTestNamespace_SyntheticComplexModel");
        modelMappers.Should().Contain("WriteTestNamespace_SyntheticComplexModel");
    }

    [TestMethod]
    public void EnumsNamedTagOrSectionTag_ShouldBeIgnored()
    {
        var source = """
            namespace SharpIpp.Protocol.Models;

            public enum Tag
            {
                Zero = 0
            }

            public enum SectionTag
            {
                Zero = 0
            }
            """;

        var (_, generatedTrees, _) = RunGenerator(source);
        var typeConverters = generatedTrees.FirstOrDefault(t => t.FilePath.EndsWith("GeneratedTypeConverters.g.cs"))?.ToString();

        typeConverters.Should().NotBeNull();
        typeConverters.Should().NotContain("Tag, int");
        typeConverters.Should().NotContain("SectionTag, int");
    }
    [TestMethod]
    public void ConversionMapping_EqualityAndHashCode_ShouldCoverAllBranches()
    {
        var compilation = CSharpCompilation.Create("TestAssembly");
        var intType = compilation.GetSpecialType(SpecialType.System_Int32);
        var stringType = compilation.GetSpecialType(SpecialType.System_String);
        var boolType = compilation.GetSpecialType(SpecialType.System_Boolean);

        var mapping = new ConversionMapping(intType, stringType);
        var equalMapping = new ConversionMapping(intType, stringType);
        var diffSource = new ConversionMapping(stringType, stringType);
        var diffDest = new ConversionMapping(intType, boolType);

        // Equals(ConversionMapping other) - true and both false branches
        mapping.Equals(equalMapping).Should().BeTrue();
        mapping.Equals(diffSource).Should().BeFalse();
        mapping.Equals(diffDest).Should().BeFalse();

        // Equals(object? obj) - true and false branches
        mapping.Equals((object)equalMapping).Should().BeTrue();
        mapping.Equals((object)diffDest).Should().BeFalse();
        mapping.Equals((object)"not a mapping").Should().BeFalse();
        mapping.Equals(null).Should().BeFalse();

        // GetHashCode()
        mapping.GetHashCode().Should().Be(equalMapping.GetHashCode());
        mapping.GetHashCode().Should().NotBe(diffDest.GetHashCode());
    }
}
