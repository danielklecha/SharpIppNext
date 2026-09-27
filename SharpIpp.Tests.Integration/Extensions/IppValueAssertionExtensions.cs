using FluentAssertions;
using FluentAssertions.Collections;
using FluentAssertions.Primitives;
using SharpIpp.Protocol.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SharpIpp.Tests.Integration;

[ExcludeFromCodeCoverage]
public static class IppValueAssertionExtensions
{
    public static GenericCollectionAssertions<T> Should<T>(this IppValue<T[]>? value)
    {
        return ((IEnumerable<T>?)((value.HasValue && value.Value.IsValue) ? value.Value.Value : null)).Should();
    }

    public static GenericCollectionAssertions<T> Should<T>(this IppValue<T[]> value)
    {
        return ((IEnumerable<T>?)(value.IsValue ? value.Value : null)).Should();
    }

    public static NullableBooleanAssertions Should(this IppValue<bool>? value)
    {
        return ((bool?)((value.HasValue && value.Value.IsValue) ? value.Value.Value : null)).Should();
    }

    public static BooleanAssertions Should(this IppValue<bool> value)
    {
        return (value.IsValue && value.Value).Should();
    }
}

