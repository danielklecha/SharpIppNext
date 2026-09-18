using System;

namespace SharpIpp.Mapping;

/// <summary>
/// Specifies that this class is an IPP response message model,
/// triggering compile-time generation of response mappings.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class IppResponseAttribute : Attribute
{
}
