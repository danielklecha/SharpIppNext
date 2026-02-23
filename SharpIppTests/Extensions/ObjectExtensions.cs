using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpIppTests.Extensions;

[ExcludeFromCodeCoverage]
public static class ObjectExtensions
{
    public static bool VerifyAssertionScope<T>( this T value, Action<T> assertion )
    {
        using var assertionScope = new AssertionScope();
        assertion.Invoke( value );
        var failures = assertionScope.Discard();
        if(failures.Length == 0)
            return true;
        foreach(var failure in failures )
            System.Diagnostics.Trace.WriteLine( failure );
        return false;
    }
}
