using System;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Mapping;

/// <summary>
/// Specifies that this class is an IPP response message model,
/// triggering compile-time generation of response mappings.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class IppResponseAttribute : Attribute
{
    /// <summary>
    /// Gets the IPP operation associated with this response model, if any.
    /// </summary>
    public IppOperation? Operation { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IppResponseAttribute"/> class without an associated operation.
    /// </summary>
    public IppResponseAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IppResponseAttribute"/> class with the specified operation.
    /// </summary>
    /// <param name="operation">The IPP operation associated with this response model.</param>
    public IppResponseAttribute(IppOperation operation)
    {
        Operation = operation;
    }
}
