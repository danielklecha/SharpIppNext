using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Generators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpIpp.Generators.Tests.Unit;

[TestClass]
public class ResponseOperationSourceGeneratorTests
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

        var generator = new ResponseOperationSourceGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var outputCompilation, out var diagnostics);

        var runResult = driver.GetRunResult();
        return (outputCompilation, runResult.GeneratedTrees, diagnostics);
    }

    [TestMethod]
    public void When_SharpIpp_NotReferenced_Should_GenerateNothing()
    {
        var source = @"
namespace TestNamespace;
public class SomeClass { }
";
        var (_, generatedTrees, _) = RunGenerator(source, includeSharpIppReference: false);
        generatedTrees.Should().BeEmpty();
    }

    [TestMethod]
    public void When_ResponsesWithOperation_Should_GenerateSwitchMapping()
    {
        var source = @"
using SharpIpp.Mapping;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;

namespace TestNamespace;

[IppResponse(IppOperation.PrintJob)]
public class SyntheticPrintJobResponse : IppResponse<OperationAttributes>
{
}

[IppResponse(IppOperation.GetJobs)]
public class SyntheticGetJobsResponse : IppResponse<OperationAttributes>
{
}
";
        var (_, generatedTrees, diagnostics) = RunGenerator(source);
        diagnostics.Should().NotContain(d => d.Severity == DiagnosticSeverity.Error);

        var generated = generatedTrees.Should().ContainSingle().Subject;
        var text = generated.ToString();

        text.Should().Contain("public static class IppResponseExtensions");
        text.Should().Contain("public static global::SharpIpp.Protocol.Models.IppOperation? GetOperation(this global::SharpIpp.Protocol.IIppResponse? response) => response switch");
        text.Should().Contain("global::TestNamespace.SyntheticPrintJobResponse => global::SharpIpp.Protocol.Models.IppOperation.PrintJob,");
        text.Should().Contain("global::TestNamespace.SyntheticGetJobsResponse => global::SharpIpp.Protocol.Models.IppOperation.GetJobs,");
        text.Should().Contain("_ => null,");
    }

    [TestMethod]
    public void When_ResponseHasNoOperationArg_Should_ExcludeFromSwitch()
    {
        var source = @"
using SharpIpp.Mapping;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;

namespace TestNamespace;

[IppResponse]
public class SyntheticNoOpResponse : IppResponse<OperationAttributes>
{
}

[IppResponse(IppOperation.GetJobs)]
public class SyntheticWithOpResponse : IppResponse<OperationAttributes>
{
}
";
        var (_, generatedTrees, diagnostics) = RunGenerator(source);
        diagnostics.Should().NotContain(d => d.Severity == DiagnosticSeverity.Error);

        var generated = generatedTrees.Should().ContainSingle().Subject;
        var text = generated.ToString();

        text.Should().Contain("global::TestNamespace.SyntheticWithOpResponse => global::SharpIpp.Protocol.Models.IppOperation.GetJobs,");
        text.Should().NotContain("SyntheticNoOpResponse");
    }

    [TestMethod]
    public void When_ClassIsAbstractOrDoesNotImplementIIppResponse_Should_Ignore()
    {
        var source = @"
using SharpIpp.Mapping;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;

namespace TestNamespace;

[IppResponse(IppOperation.PrintJob)]
public abstract class AbstractResponse : IppResponse<OperationAttributes>
{
}

[IppResponse(IppOperation.PrintJob)]
public class NonIppResponseClass
{
}
";
        var (_, generatedTrees, diagnostics) = RunGenerator(source);
        diagnostics.Should().NotContain(d => d.Severity == DiagnosticSeverity.Error);

        var generated = generatedTrees.Should().ContainSingle().Subject;
        var text = generated.ToString();

        text.Should().NotContain("AbstractResponse");
        text.Should().NotContain("NonIppResponseClass");
    }

    [TestMethod]
    public void When_DerivedClassesExist_Should_SortByInheritanceDepthDescending()
    {
        var source = @"
using SharpIpp.Mapping;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;

namespace TestNamespace;

[IppResponse(IppOperation.PrintJob)]
public class BaseTestResponse : IppResponse<OperationAttributes>
{
}

[IppResponse(IppOperation.PrintUri)]
public class DerivedTestResponse : BaseTestResponse
{
}
";
        var (_, generatedTrees, diagnostics) = RunGenerator(source);
        diagnostics.Should().NotContain(d => d.Severity == DiagnosticSeverity.Error);

        var generated = generatedTrees.Should().ContainSingle().Subject;
        var text = generated.ToString();

        var derivedIdx = text.IndexOf("DerivedTestResponse");
        var baseIdx = text.IndexOf("BaseTestResponse");

        derivedIdx.Should().BePositive();
        baseIdx.Should().BePositive();
        derivedIdx.Should().BeLessThan(baseIdx, "derived response must be matched before base response in pattern matching");
    }

    [TestMethod]
    public void When_IppResponseAttribute_Exists_But_IIppResponse_Missing_Should_GenerateNothing()
    {
        var source = @"
namespace SharpIpp.Mapping
{
    public class IppResponseAttribute : System.Attribute { }
}

namespace TestNamespace
{
    public class SomeClass { }
}
";
        var (_, generatedTrees, _) = RunGenerator(source, includeSharpIppReference: false);
        generatedTrees.Should().BeEmpty();
    }

    [TestMethod]
    public void When_ClassImplementsIIppResponse_WithoutAttribute_Should_BeIgnored()
    {
        var source = @"
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;

namespace TestNamespace;

public class PlainResponse : IppResponse<OperationAttributes>
{
}
";
        var (_, generatedTrees, diagnostics) = RunGenerator(source);
        diagnostics.Should().NotContain(d => d.Severity == DiagnosticSeverity.Error);

        var generated = generatedTrees.Should().ContainSingle().Subject;
        var text = generated.ToString();

        text.Should().NotContain("PlainResponse");
    }

    [TestMethod]
    public void When_AttributeArgument_IsNull_Should_BeIgnored()
    {
        var source = @"
namespace SharpIpp.Mapping
{
    public class IppResponseAttribute : System.Attribute
    {
        public IppResponseAttribute(object? op) { }
    }
}

namespace SharpIpp.Protocol
{
    public interface IIppResponse { }
}

namespace TestNamespace
{
    [SharpIpp.Mapping.IppResponse(null)]
    public class NullArgResponse : SharpIpp.Protocol.IIppResponse
    {
    }
}
";
        var (_, generatedTrees, _) = RunGenerator(source, includeSharpIppReference: false);
        var generated = generatedTrees.Should().ContainSingle().Subject;
        var text = generated.ToString();

        text.Should().NotContain("NullArgResponse");
    }

    [TestMethod]
    public void When_OperationIsCustomOrUnrecognizedEnumValue_Should_GenerateCastExpression()
    {
        var source = @"
using SharpIpp.Mapping;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;

namespace TestNamespace;

[IppResponse((IppOperation)9999)]
public class CustomOpResponse : IppResponse<OperationAttributes>
{
}
";
        var (_, generatedTrees, diagnostics) = RunGenerator(source);
        diagnostics.Should().NotContain(d => d.Severity == DiagnosticSeverity.Error);

        var generated = generatedTrees.Should().ContainSingle().Subject;
        var text = generated.ToString();

        text.Should().Contain("global::TestNamespace.CustomOpResponse => (global::SharpIpp.Protocol.Models.IppOperation)9999,");
    }

    [TestMethod]
    public void When_ClassIsGenericOrStaticOrImplementsOtherInterface_Should_Ignore()
    {
        var source = @"
using SharpIpp.Mapping;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;

namespace TestNamespace;

[IppResponse(IppOperation.PrintJob)]
public static class StaticResponseClass
{
}

[IppResponse(IppOperation.PrintJob)]
public class GenericResponseClass<T> : IppResponse<OperationAttributes>
{
}

[IppResponse(IppOperation.PrintJob)]
public class OtherInterfaceResponseClass : System.IDisposable
{
    public void Dispose() { }
}
";
        var (_, generatedTrees, diagnostics) = RunGenerator(source);
        diagnostics.Should().NotContain(d => d.Severity == DiagnosticSeverity.Error);

        var generated = generatedTrees.Should().ContainSingle().Subject;
        var text = generated.ToString();

        text.Should().NotContain("StaticResponseClass");
        text.Should().NotContain("GenericResponseClass");
        text.Should().NotContain("OtherInterfaceResponseClass");
    }

    [TestMethod]
    public void GetAttribute_ShouldHandleNullInputs()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText("namespace TestNamespace { public class TestClass { } }");
        var compilation = CSharpCompilation.Create("TestComp", new[] { syntaxTree }, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
        var testClassSymbol = compilation.GetTypeByMetadataName("TestNamespace.TestClass");

        ResponseOperationSourceGenerator.GetAttribute(null, testClassSymbol).Should().BeNull();
        ResponseOperationSourceGenerator.GetAttribute(testClassSymbol, null).Should().BeNull();
        ResponseOperationSourceGenerator.GetAttribute(null, null).Should().BeNull();
    }

    [TestMethod]
    public void ImplementsOrInherits_ShouldHandleAllBranches()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(@"
namespace TestNamespace
{
    public interface ITestInterface { }
    public interface IOtherInterface { }
    public class BaseClass { }
    public class DerivedClass : BaseClass, ITestInterface { }
    public class UnrelatedClass : IOtherInterface { }
}
");
        var compilation = CSharpCompilation.Create("TestComp", new[] { syntaxTree }, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
        var baseClass = compilation.GetTypeByMetadataName("TestNamespace.BaseClass");
        var derivedClass = compilation.GetTypeByMetadataName("TestNamespace.DerivedClass");
        var unrelatedClass = compilation.GetTypeByMetadataName("TestNamespace.UnrelatedClass");
        var testInterface = compilation.GetTypeByMetadataName("TestNamespace.ITestInterface");

        // Null checks
        ResponseOperationSourceGenerator.ImplementsOrInherits(null, baseClass).Should().BeFalse();
        ResponseOperationSourceGenerator.ImplementsOrInherits(baseClass, null).Should().BeFalse();
        ResponseOperationSourceGenerator.ImplementsOrInherits(null, null).Should().BeFalse();

        // Symbol equality
        ResponseOperationSourceGenerator.ImplementsOrInherits(baseClass, baseClass).Should().BeTrue();

        // Interface target
        ResponseOperationSourceGenerator.ImplementsOrInherits(derivedClass, testInterface).Should().BeTrue();
        ResponseOperationSourceGenerator.ImplementsOrInherits(unrelatedClass, testInterface).Should().BeFalse();

        // Class target
        ResponseOperationSourceGenerator.ImplementsOrInherits(derivedClass, baseClass).Should().BeTrue();
        ResponseOperationSourceGenerator.ImplementsOrInherits(unrelatedClass, baseClass).Should().BeFalse();
    }

    [TestMethod]
    public void GetInheritanceDepth_WhenTypeIsObject_ShouldReturnZero()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText("public class Dummy { }");
        var compilation = CSharpCompilation.Create("TestComp", new[] { syntaxTree }, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
        var objectSymbol = compilation.GetSpecialType(SpecialType.System_Object);

        ResponseOperationSourceGenerator.GetInheritanceDepth(objectSymbol).Should().Be(0);
    }

    [TestMethod]
    public void TryGetEnumFieldName_ShouldHandleAllBranches()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(@"
namespace TestNamespace
{
    public class NotAnEnum { }
    public enum SampleEnum { First = 1, Second = 2 }
}
");
        var compilation = CSharpCompilation.Create("TestComp", new[] { syntaxTree }, new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
        var notAnEnum = compilation.GetTypeByMetadataName("TestNamespace.NotAnEnum");
        var sampleEnum = compilation.GetTypeByMetadataName("TestNamespace.SampleEnum");

        // Non-enum or null type
        ResponseOperationSourceGenerator.TryGetEnumFieldName(null, 1).Should().BeNull();
        ResponseOperationSourceGenerator.TryGetEnumFieldName(notAnEnum, 1).Should().BeNull();

        // Null value
        ResponseOperationSourceGenerator.TryGetEnumFieldName(sampleEnum, null).Should().BeNull();

        // Existing enum values
        ResponseOperationSourceGenerator.TryGetEnumFieldName(sampleEnum, 1).Should().Be("First");
        ResponseOperationSourceGenerator.TryGetEnumFieldName(sampleEnum, 2).Should().Be("Second");

        // Missing enum value
        ResponseOperationSourceGenerator.TryGetEnumFieldName(sampleEnum, 999).Should().BeNull();
    }
}

