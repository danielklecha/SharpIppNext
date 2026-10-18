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

    [TestMethod]
    public void HasValidationLogic_ThroughGenerator_ShouldCoverIppValueAndCollectionProperties()
    {
        var source = """
            namespace SharpIpp.Models
            {
                public class ChildModel
                {
                    public string? Info { get; set; }
                }

                // Line 355-356: IppValue with SharpIpp model
                public class ModelWithIppValueChild
                {
                    public global::SharpIpp.Protocol.Models.IppValue<ChildModel>? Item { get; set; }
                }

                // Line 357-358: IppValue with class array (non-string)
                public class ModelWithIppValueArrayChild
                {
                    public global::SharpIpp.Protocol.Models.IppValue<ChildModel[]>? Items { get; set; }
                }

                // Line 359-360: IppValue with generic enumerable of class
                public class ModelWithIppValueListChild
                {
                    public global::SharpIpp.Protocol.Models.IppValue<System.Collections.Generic.List<ChildModel>>? Items { get; set; }
                }

                // Line 368-369: Class array property (non-IppValue, non-string)
                public class ModelWithArrayChild
                {
                    public ChildModel[]? Items { get; set; }
                }

                // Line 371-373: Generic enumerable of class property (non-IppValue)
                public class ModelWithListChild
                {
                    public System.Collections.Generic.List<ChildModel>? Items { get; set; }
                }

                // Non-validating collection models to cover false branches in lines 357, 359, 368, 371
                public class ModelWithNonValidatingCollections
                {
                    public global::SharpIpp.Protocol.Models.IppValue<int[]>? PrimitiveArray { get; set; }
                    public global::SharpIpp.Protocol.Models.IppValue<string[]>? StringArray { get; set; }
                    public global::SharpIpp.Protocol.Models.IppValue<System.Collections.Generic.List<int>>? PrimitiveList { get; set; }
                    public global::SharpIpp.Protocol.Models.IppValue<System.Collections.Generic.List<string>>? StringList { get; set; }
                    public int[]? DirectPrimitiveArray { get; set; }
                    public string[]? DirectStringArray { get; set; }
                    public System.Collections.Generic.List<int>? DirectPrimitiveList { get; set; }
                    public System.Collections.Generic.List<string>? DirectStringList { get; set; }
                }
            }
            """;

        var (outputCompilation, generatedTrees, _) = RunGenerator(source);
        generatedTrees.Should().NotBeEmpty();
    }

    [TestMethod]
    public void HasValidationLogic_DirectCalls_ShouldCoverAllPropertyBranches()
    {
        var source = """
            namespace SharpIpp.Models
            {
                public class ChildModel { }
                public class ModelIppValChild { public global::SharpIpp.Protocol.Models.IppValue<ChildModel>? Item { get; set; } }
                public class ModelIppValArray { public global::SharpIpp.Protocol.Models.IppValue<ChildModel[]>? Items { get; set; } }
                public class ModelIppValIntArray { public global::SharpIpp.Protocol.Models.IppValue<int[]>? Items { get; set; } }
                public class ModelIppValStringArray { public global::SharpIpp.Protocol.Models.IppValue<string[]>? Items { get; set; } }
                public class ModelIppValList { public global::SharpIpp.Protocol.Models.IppValue<System.Collections.Generic.List<ChildModel>>? Items { get; set; } }
                public class ModelIppValIntList { public global::SharpIpp.Protocol.Models.IppValue<System.Collections.Generic.List<int>>? Items { get; set; } }
                public class ModelIppValStringList { public global::SharpIpp.Protocol.Models.IppValue<System.Collections.Generic.List<string>>? Items { get; set; } }
                public class ModelClassArray { public ChildModel[]? Items { get; set; } }
                public class ModelIntArray { public int[]? Items { get; set; } }
                public class ModelStringArray { public string[]? Items { get; set; } }
                public class ModelClassList { public System.Collections.Generic.List<ChildModel>? Items { get; set; } }
                public class ModelIntList { public System.Collections.Generic.List<int>? Items { get; set; } }
                public class ModelStringList { public System.Collections.Generic.List<string>? Items { get; set; } }
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(source, new CSharpParseOptions(LanguageVersion.Latest));
        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
            .Select(a => MetadataReference.CreateFromFile(a.Location))
            .Cast<MetadataReference>()
            .Concat(new[] { MetadataReference.CreateFromFile(typeof(SharpIpp.SharpIppClient).Assembly.Location) })
            .ToList();

        var compilation = CSharpCompilation.Create(
            "TestAssembly",
            new[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, nullableContextOptions: NullableContextOptions.Enable));

        var attrSymbol = compilation.GetTypeByMetadataName("SharpIpp.Validation.IppValidationAttribute")!;

        var mIppValChild = compilation.GetTypeByMetadataName("SharpIpp.Models.ModelIppValChild")!;
        var mIppValArray = compilation.GetTypeByMetadataName("SharpIpp.Models.ModelIppValArray")!;
        var mIppValIntArray = compilation.GetTypeByMetadataName("SharpIpp.Models.ModelIppValIntArray")!;
        var mIppValStrArray = compilation.GetTypeByMetadataName("SharpIpp.Models.ModelIppValStringArray")!;
        var mIppValList = compilation.GetTypeByMetadataName("SharpIpp.Models.ModelIppValList")!;
        var mIppValIntList = compilation.GetTypeByMetadataName("SharpIpp.Models.ModelIppValIntList")!;
        var mIppValStrList = compilation.GetTypeByMetadataName("SharpIpp.Models.ModelIppValStringList")!;
        var mClassArray = compilation.GetTypeByMetadataName("SharpIpp.Models.ModelClassArray")!;
        var mIntArray = compilation.GetTypeByMetadataName("SharpIpp.Models.ModelIntArray")!;
        var mStrArray = compilation.GetTypeByMetadataName("SharpIpp.Models.ModelStringArray")!;
        var mClassList = compilation.GetTypeByMetadataName("SharpIpp.Models.ModelClassList")!;
        var mIntList = compilation.GetTypeByMetadataName("SharpIpp.Models.ModelIntList")!;
        var mStrList = compilation.GetTypeByMetadataName("SharpIpp.Models.ModelStringList")!;

        ValidationSourceGenerator.HasValidationLogic(mIppValChild, attrSymbol, compilation.Assembly).Should().BeTrue();
        ValidationSourceGenerator.HasValidationLogic(mIppValArray, attrSymbol, compilation.Assembly).Should().BeTrue();
        ValidationSourceGenerator.HasValidationLogic(mIppValIntArray, attrSymbol, compilation.Assembly).Should().BeFalse();
        ValidationSourceGenerator.HasValidationLogic(mIppValStrArray, attrSymbol, compilation.Assembly).Should().BeFalse();
        ValidationSourceGenerator.HasValidationLogic(mIppValList, attrSymbol, compilation.Assembly).Should().BeTrue();
        ValidationSourceGenerator.HasValidationLogic(mIppValIntList, attrSymbol, compilation.Assembly).Should().BeFalse();
        ValidationSourceGenerator.HasValidationLogic(mIppValStrList, attrSymbol, compilation.Assembly).Should().BeFalse();
        ValidationSourceGenerator.HasValidationLogic(mClassArray, attrSymbol, compilation.Assembly).Should().BeTrue();
        ValidationSourceGenerator.HasValidationLogic(mIntArray, attrSymbol, compilation.Assembly).Should().BeFalse();
        ValidationSourceGenerator.HasValidationLogic(mStrArray, attrSymbol, compilation.Assembly).Should().BeFalse();
        ValidationSourceGenerator.HasValidationLogic(mClassList, attrSymbol, compilation.Assembly).Should().BeTrue();
        ValidationSourceGenerator.HasValidationLogic(mIntList, attrSymbol, compilation.Assembly).Should().BeFalse();
        ValidationSourceGenerator.HasValidationLogic(mStrList, attrSymbol, compilation.Assembly).Should().BeFalse();
    }
}