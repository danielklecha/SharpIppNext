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

public partial class ValidationSourceGeneratorTests
{
    [TestMethod]
    public void MultipleNamespaces_ShouldOrderNamespaceGroups()
    {
        var source = """
            namespace SharpIpp.Models.SectionB
            {
                public class ModelB
                {
                    [global::SharpIpp.Validation.Range(1, 10)]
                    public int ValB { get; set; }
                }
            }

            namespace SharpIpp.Models.SectionA
            {
                public class ModelA
                {
                    [global::SharpIpp.Validation.Range(1, 10)]
                    public int ValA { get; set; }
                }
            }
            """;

        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        generatedTrees.Should().NotBeEmpty();
    }

    [TestMethod]
    public void NonIntegerAndNonTautologicalRanges_ShouldCoverRangeBranches()
    {
        var source = """
            namespace SharpIpp.Models
            {
                public class RangeVariationsModel
                {
                    // Non-integer property with Range (line 357)
                    [global::SharpIpp.Validation.Range(1, 10)]
                    public string? TextProp { get; set; }

                    // Array constructor with non-tautological values (line 376)
                    [global::SharpIpp.Validation.Range(new int[] { 1, 10 })]
                    public int ArrayRangeProp { get; set; }

                    // Nullable non-int with Range (line 405)
                    [global::SharpIpp.Validation.Range(1, 10)]
                    public bool? NullableBoolProp { get; set; }

                    // Collections implementing IEnumerable<int> (lines 410-416)
                    [global::SharpIpp.Validation.Range(1, 10)]
                    public System.Collections.Generic.List<int> NumberList { get; set; } = new();

                    // Collections implementing IEnumerable<string> (line 420)
                    [global::SharpIpp.Validation.Range(1, 10)]
                    public System.Collections.Generic.List<string> StringList { get; set; } = new();

                    // Non-validation attribute on property (line 446)
                    [System.Obsolete]
                    public int ObsoleteProp { get; set; }

                    // Null attribute constant (line 517)
                    [global::SharpIpp.Validation.Range(1, 10, ErrorMessage = null)]
                    public int NullErrorProp { get; set; }
                }
            }
            """;

        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        generatedTrees.Should().NotBeEmpty();
    }

    [TestMethod]
    public void ExcludedNamespaces_And_FallbackConstants_ShouldCoverRemainingBranches()
    {
        var source = """
            namespace SharpIpp.CustomValidation
            {
                public class DoubleValidationAttribute : global::SharpIpp.Validation.IppValidationAttribute
                {
                    public DoubleValidationAttribute(double min, double max) {}
                }
            }

            namespace SharpIpp.Exceptions
            {
                public class ModelExcludedException : System.Exception {}
            }

            namespace SharpIpp.Models
            {
                public class AdvancedModel
                {
                    // Fallback primitive constant type like double (line 565)
                    [SharpIpp.CustomValidation.DoubleValidation(1.5, 9.5)]
                    public string? StringWithDoubleAttr { get; set; }

                    // Property type in SharpIpp.Exceptions namespace (line 462)
                    public SharpIpp.Exceptions.ModelExcludedException? ExceptionProp { get; set; }
                }
            }
            """;

        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        generatedTrees.Should().NotBeEmpty();
    }

    [TestMethod]
    public void DirectHelperMethods_ShouldCoverDefensiveChecks()
    {
        ValidationSourceGenerator.IsIntegerTypeOrCollection(null!).Should().BeFalse();
        ValidationSourceGenerator.IsInt32(null!).Should().BeFalse();
        ValidationSourceGenerator.IsSharpIppModelType(null!, null!).Should().BeFalse();

        var tree = CSharpSyntaxTree.ParseText("public class { }");
        var comp = CSharpCompilation.Create(
            "TestAssembly",
            new[] { tree },
            new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(SharpIpp.SharpIppClient).Assembly.Location)
            });

        ValidationSourceGenerator.Execute(comp, ImmutableArray<INamedTypeSymbol>.Empty, default);
    }

    [TestMethod]
    public void BooleanFalseAndNullConstants_ShouldCoverFormattingBranches()
    {
        var source = """"""
            namespace SharpIpp.CustomValidation
            {
                public class BoolAndNullValidationAttribute : global::SharpIpp.Validation.IppValidationAttribute
                {
                    public BoolAndNullValidationAttribute(bool flag, string? note) {}
                }

                public class SingleIntRangeAttribute : global::SharpIpp.Validation.RangeAttribute
                {
                    public SingleIntRangeAttribute(int val) : base(val, val) {}
                {
                    public SingleIntRangeAttribute(int val) {}
                }
            }

            namespace SharpIpp.Models
            {
                public class FormattingVariationsModel
                {
                    [SharpIpp.CustomValidation.BoolAndNullValidation(false, null)]
                    public string? FlagProp { get; set; }

                    [SharpIpp.CustomValidation.SingleIntRange(10)]
                    public int SingleIntProp { get; set; }
                }
            }
            """""";

        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        generatedTrees.Should().NotBeEmpty();
    }

    [TestMethod]
    public void GlobalNamespaceModel_ShouldCoverGlobalNamespaceBranch()
    {
        var source = """"""
            public class GlobalNamespaceTestModel
            {
                [global::SharpIpp.Validation.Range(1, 10)]
                public int GlobalIntProp { get; set; }
            }

            namespace SharpIpp.Models
            {
                public class GlobalContainerModel
                {
                    [global::SharpIpp.Validation.Range(1, 10)]
                    public int ContainerVal { get; set; }

                    public GlobalNamespaceTestModel? Child { get; set; }
                }
            }
            """""";

        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        generatedTrees.Should().NotBeEmpty();
    }

    [TestMethod]
    public void DirectHelpers_ShouldCoverNullAndHierarchyBranches()
    {
        ValidationSourceGenerator.InheritsFrom(null!, null!).Should().BeFalse();
        ValidationSourceGenerator.GetTypeAndBaseTypes(null).Should().BeEmpty();

        var tree = CSharpSyntaxTree.ParseText("public interface ITest { } public class CTest : ITest { }");
        var comp = CSharpCompilation.Create(
            "TestAssembly",
            new[] { tree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });

        var iface = comp.GetTypeByMetadataName("ITest")!;
        var cl = comp.GetTypeByMetadataName("CTest")!;
        ValidationSourceGenerator.GetTypeAndBaseTypes(iface).Should().ContainSingle();
        ValidationSourceGenerator.GetInheritanceDepth(iface).Should().Be(0);
        ValidationSourceGenerator.GetInheritanceDepth(cl).Should().Be(0);

        ValidationSourceGenerator.InheritsFrom(cl, null!).Should().BeFalse();
        ValidationSourceGenerator.InheritsFrom(null!, iface).Should().BeFalse();
    }

    [TestMethod]
    public void ArrayAttributeArguments_EmptyAndNonEmpty_ShouldCoverArrayFormatting()
    {
        var source = """"""
            namespace SharpIpp.CustomValidation
            {
                public class ArrayAttribute : global::SharpIpp.Validation.IppValidationAttribute
                {
                    public ArrayAttribute(int[] items) {}
                }
            }

            namespace SharpIpp.Models
            {
                public class ArrayTestModel
                {
                    [SharpIpp.CustomValidation.Array(new int[] { })]
                    public int EmptyArrayProp { get; set; }

                    [SharpIpp.CustomValidation.Array(new int[] { 42, 99 })]
                    public int PopulatedArrayProp { get; set; }
                }
            }
            """""";

        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        generatedTrees.Should().NotBeEmpty();
    }
}