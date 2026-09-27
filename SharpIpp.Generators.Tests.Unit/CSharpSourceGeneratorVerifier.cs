using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using SharpIpp.Generators;
using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Generators.Tests.Unit;

[ExcludeFromCodeCoverage]
public static class CSharpSourceGeneratorVerifier<TSourceGenerator>
    where TSourceGenerator : IIncrementalGenerator, new()
{
    public class Test : CSharpSourceGeneratorTest<TSourceGenerator, DefaultVerifier>
    {
        public Test()
        {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net80;
            TestState.AdditionalReferences.Add(typeof(SharpIpp.SharpIppClient).Assembly);
            CompilerDiagnostics = CompilerDiagnostics.None;
        }

        protected override CompilationOptions CreateCompilationOptions()
        {
            var compilationOptions = (CSharpCompilationOptions)base.CreateCompilationOptions();
            return compilationOptions
                .WithNullableContextOptions(NullableContextOptions.Enable)
                .WithSpecificDiagnosticOptions(new Dictionary<string, ReportDiagnostic>
                {
                    ["CS1705"] = ReportDiagnostic.Suppress
                });
        }

        protected override ParseOptions CreateParseOptions()
        {
            return ((CSharpParseOptions)base.CreateParseOptions())
                .WithLanguageVersion(LanguageVersion.Latest);
        }
    }
}
