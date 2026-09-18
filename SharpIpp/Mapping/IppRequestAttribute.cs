using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping;

/// <summary>
/// Associates a request model with its IPP operation for compile-time mapping generation.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class IppRequestAttribute : Attribute
{
    public IppOperation Operation { get; }

    public IppRequestAttribute(IppOperation operation)
    {
        Operation = operation;
    }
}
