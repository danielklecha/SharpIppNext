using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Generators;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SharpIpp.Generators.Tests.Unit;

[TestClass]
[ExcludeFromCodeCoverage]
public partial class ValidationSourceGeneratorTests
{
    private static (Compilation OutputCompilation, IEnumerable<SyntaxTree> GeneratedTrees, IEnumerable<Diagnostic> Diagnostics) RunGenerator(
        string source,
        string assemblyName = "TestAssembly",
        bool includeSharpIppReference = true)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.Latest));

        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location) && (includeSharpIppReference || a.GetName().Name?.Contains("SharpIpp") != true))
            .Select(a => MetadataReference.CreateFromFile(a.Location))
            .Cast<MetadataReference>()
            .ToList();

        if (includeSharpIppReference)
        {
            references.Add(MetadataReference.CreateFromFile(typeof(SharpIpp.SharpIppClient).Assembly.Location));
        }

        var compilation = CSharpCompilation.Create(
            assemblyName,
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, nullableContextOptions: NullableContextOptions.Enable));

        var generator = new ValidationSourceGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diagnostics);

        var runResult = driver.GetRunResult();
        return (outputCompilation, runResult.GeneratedTrees, diagnostics);
    }

    [TestMethod]
    public void When_IppValidationAttribute_Missing_Should_GenerateNothing()
    {
        var source = "public class Foo {}";
        var (_, generatedTrees, _) = RunGenerator(source, includeSharpIppReference: false);
        generatedTrees.Should().BeEmpty();
    }

    [TestMethod]
    public void When_NoTargetTypes_Should_GenerateNothing()
    {
        var source = """
            namespace SharpIpp.Exceptions;
            public class SomeException : System.Exception {}
            """;
        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        generatedTrees.Should().BeEmpty();
    }

    [TestMethod]
    public void When_ClassIsAbstractStaticOrGeneric_Should_Skip()
    {
        var source = """
            namespace SharpIpp.Models;
            public abstract class AbstractModel {}
            public static class StaticModel {}
            public class GenericModel<T> {}
            public struct StructModel {}
            """;
        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        generatedTrees.Should().BeEmpty();
    }

    [TestMethod]
    public void When_ModelInSharpIppModels_Should_GenerateValidator()
    {
        var source = """
            namespace SharpIpp.Models;

            public class SimpleJobModel
            {
                [global::SharpIpp.Validation.Range(1, 10, ErrorMessage = "Invalid range")]
                public int Value { get; set; }
            }
            """;

        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        var generatedSources = generatedTrees.ToDictionary(t => System.IO.Path.GetFileName(t.FilePath), t => t.ToString());

        generatedSources.Should().ContainKey("GeneratedModelValidator.SharpIpp.Models.g.cs");
        generatedSources.Should().ContainKey("GeneratedModelValidator.g.cs");

        var modelValidator = generatedSources["GeneratedModelValidator.SharpIpp.Models.g.cs"];
        modelValidator.Should().Contain("Validate_SharpIpp_Models_SimpleJobModel");
        modelValidator.Should().Contain("__attr_0.IsValid(val_Value, ctx_Value)");

        var mainValidator = generatedSources["GeneratedModelValidator.g.cs"];
        mainValidator.Should().Contain("case global::SharpIpp.Models.SimpleJobModel target:");
        mainValidator.Should().Contain("Validate_SharpIpp_Models_SimpleJobModel(target, encoding, results, visited);");
    }

    [TestMethod]
    public void When_ModelHasInheritance_Should_ValidateBasePropertiesInDerivedValidator()
    {
        var source = """
            namespace SharpIpp.Models;

            public class BaseJobModel
            {
                [global::SharpIpp.Validation.Range(1, 20)]
                public int BaseVal { get; set; }
            }

            public class DerivedJobModel : BaseJobModel
            {
                [global::SharpIpp.Validation.Range(1, 10)]
                public int DerivedVal { get; set; }
            }
            """;

        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        var generatedSources = generatedTrees.ToDictionary(t => System.IO.Path.GetFileName(t.FilePath), t => t.ToString());
        var validator = generatedSources["GeneratedModelValidator.SharpIpp.Models.g.cs"];

        validator.Should().Contain("Validate_SharpIpp_Models_DerivedJobModel");
        validator.Should().Contain("var val_DerivedVal = obj.DerivedVal;");
        validator.Should().Contain("var val_BaseVal = obj.BaseVal;");
    }

    [TestMethod]
    public void When_ModelHasChildObjectsAndCollections_Should_Recurse()
    {
        var source = """
            using System.Collections.Generic;
            using SharpIpp.Protocol.Models;

            namespace SharpIpp.Models;

            public class ChildItem
            {
                public string? Info { get; set; }
            }

            public class ParentModel
            {
                public ChildItem? SingleChild { get; set; }
                public ChildItem[]? ArrayChild { get; set; }
                public List<ChildItem>? ListChild { get; set; }
                public IppValue<ChildItem>? IppValueChild { get; set; }
                public IppValue<ChildItem[]>? IppValueArrayChild { get; set; }
                public IppValue<List<ChildItem>>? IppValueListChild { get; set; }
            }
            """;

        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        var generatedSources = generatedTrees.ToDictionary(t => System.IO.Path.GetFileName(t.FilePath), t => t.ToString());
        var validator = generatedSources["GeneratedModelValidator.SharpIpp.Models.g.cs"];

        validator.Should().Contain("TryValidate(obj.SingleChild, encoding, results, visited);");
        validator.Should().Contain("foreach (var item in obj.ArrayChild)");
        validator.Should().Contain("foreach (var item in obj.ListChild)");
        validator.Should().Contain("TryValidate(obj.IppValueChild.Value.Value, encoding, results, visited);");
        validator.Should().Contain("foreach (var item in obj.IppValueArrayChild.Value.Value)");
        validator.Should().Contain("foreach (var item in obj.IppValueListChild.Value.Value)");
    }

    [TestMethod]
    public void When_TautologicalRangeAttribute_Should_BeSkipped()
    {
        var source = """
            using System.Collections.Generic;

            namespace SharpIpp.Models;

            public class RangeTestModel
            {
                [global::SharpIpp.Validation.Range(int.MinValue, int.MaxValue)]
                public int TautologicalInt { get; set; }

                [global::SharpIpp.Validation.Range(int.MinValue, int.MaxValue)]
                public int? TautologicalNullableInt { get; set; }

                [global::SharpIpp.Validation.Range(new int[] { int.MinValue, int.MaxValue })]
                public int[]? TautologicalArray { get; set; }

                [global::SharpIpp.Validation.Range(new int[] { int.MinValue, int.MaxValue })]
                public IEnumerable<int>? TautologicalEnumerable { get; set; }

                [global::SharpIpp.Validation.Range(1, 100)]
                public int ValidRangeInt { get; set; }
            }
            """;

        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        var generatedSources = generatedTrees.ToDictionary(t => System.IO.Path.GetFileName(t.FilePath), t => t.ToString());
        var validator = generatedSources["GeneratedModelValidator.SharpIpp.Models.g.cs"];

        validator.Should().NotContain("val_TautologicalInt");
        validator.Should().NotContain("val_TautologicalNullableInt");
        validator.Should().NotContain("val_TautologicalArray");
        validator.Should().NotContain("val_TautologicalEnumerable");
        validator.Should().Contain("val_ValidRangeInt");
    }

    [TestMethod]
    public void When_VariousAttributeConstants_Should_FormatCorrectly()
    {
        var source = """
            using System;
            using SharpIpp.Validation;

            namespace SharpIpp.CustomValidation
            {
                [AttributeUsage(AttributeTargets.Property)]
                public class ComplexValidationAttribute : IppValidationAttribute
                {
                    public ComplexValidationAttribute(
                        string? strVal,
                        bool boolVal,
                        char charVal,
                        int intVal,
                        int minInt,
                        int maxInt,
                        long longVal,
                        long minLong,
                        long maxLong,
                        UriKind enumVal,
                        Type typeVal,
                        int[] arrVal)
                    {
                    }
                }
            }

            namespace SharpIpp.Models
            {
                public class ComplexAttrModel
                {
                    [SharpIpp.CustomValidation.ComplexValidation(
                        "hello",
                        true,
                        'x',
                        42,
                        int.MinValue,
                        int.MaxValue,
                        123456789L,
                        long.MinValue,
                        long.MaxValue,
                        UriKind.Absolute,
                        typeof(string),
                        new int[] { 1, 2, 3 })]
                    public string? ComplexProp { get; set; }
                }
            }
            """;

        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        var generatedSources = generatedTrees.ToDictionary(t => System.IO.Path.GetFileName(t.FilePath), t => t.ToString());
        generatedSources.Should().ContainKey("GeneratedModelValidator.g.cs");

        var main = generatedSources["GeneratedModelValidator.g.cs"];
        main.Should().Contain("\"hello\"");
        main.Should().Contain("true");
        main.Should().Contain("'x'");
        main.Should().Contain("int.MinValue");
        main.Should().Contain("int.MaxValue");
        main.Should().Contain("long.MinValue");
        main.Should().Contain("long.MaxValue");
        main.Should().Contain("123456789L");
        main.Should().Contain("typeof(string)");
        main.Should().Contain("new int[] { 1, 2, 3 }");
    }

    [TestMethod]
    public void When_GlobalNamespace_Should_EmitGlobalFile()
    {
        var source = """
            public class GlobalModel
            {
                [global::SharpIpp.Validation.Range(1, 5)]
                public int GlobalVal { get; set; }
            }
            """;

        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        var generatedSources = generatedTrees.ToDictionary(t => System.IO.Path.GetFileName(t.FilePath), t => t.ToString());

        var diags = outputCompilation.GetDiagnostics().Where(d => d.Severity == DiagnosticSeverity.Error).ToList(); diags.Should().BeEmpty(string.Join("; ", diags.Select(d => d.ToString()))); generatedSources.Should().ContainKey("GeneratedModelValidator.Global.g.cs");
    }
}
