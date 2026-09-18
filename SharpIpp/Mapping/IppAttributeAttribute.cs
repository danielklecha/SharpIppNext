namespace SharpIpp.Mapping;

using System;
using SharpIpp.Protocol.Models;

/// <summary>
/// Configures IPP attribute mapping for a property.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class IppAttributeAttribute : Attribute
{
    private int _order = -1;

    /// <summary>
    /// Default fallback value for serialization/deserialization if the property value is null.
    /// </summary>
    public string? DefaultValue { get; set; }

    /// <summary>
    /// The IPP attribute name. If null, the kebab-case property name is used.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The IPP attribute tag. If unsupported, the tag is inferred from the property type.
    /// </summary>
    public Tag Tag { get; set; } = Tag.Unsupported;

    /// <summary>
    /// Explicit ordering index for RFC mandatory positioning (e.g. charset at 0, language at 1).
    /// Defaults to -1 (unset).
    /// </summary>
    public int Order
    {
        get => _order;
        set => _order = value;
    }

    /// <summary>
    /// Returns the explicit order as a nullable integer (null when Order was not set).
    /// </summary>
    public int? ExplicitOrder => _order >= 0 ? _order : null;

    public IppAttributeAttribute() { }

    public IppAttributeAttribute(string name)
    {
        Name = name;
    }

    public IppAttributeAttribute(string name, Tag tag)
    {
        Name = name;
        Tag = tag;
    }

    public IppAttributeAttribute(string name, int order)
    {
        Name = name;
        Order = order;
    }

    public IppAttributeAttribute(string name, Tag tag, int order)
    {
        Name = name;
        Tag = tag;
        Order = order;
    }
}

