using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Generators;

namespace SharpIpp.Tests.Unit.Generators;

[TestClass]
public class MapperSourceGeneratorTests
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
        typeConverters.Should().Contain("mapper.CreateIppMap<NoValue, global::SharpIpp.Protocol.Models.SyntheticJobState>((_, _) => NoValue.GetNoValue<global::SharpIpp.Protocol.Models.SyntheticJobState>());");
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
        modelMappers.Should().Contain("if (src.IsOutOfBandNoValue())");
        modelMappers.Should().Contain("return NoValue.GetNoValue<global::TestNamespace.SyntheticMediaCollection>();");
        modelMappers.Should().Contain("if (NoValue.IsNoValue(src))");
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
        modelMappers.Should().Contain("global::SharpIpp.Protocol.Models.NoValue.GetNoValue<global::SharpIpp.Protocol.Models.DocumentMetadata>()");
        modelMappers.Should().Contain("!((global::SharpIpp.Protocol.Models.INoValue)src.Metadata).IsValue");
    }
}
