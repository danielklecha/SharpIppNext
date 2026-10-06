using System;

namespace SharpIpp.Mapping;

/// <summary>
/// Specifies that a class contains mapper configuration methods to be dynamically
/// registered by the source generator.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class MapperConfigurationAttribute : Attribute
{
    /// <summary>
    /// Gets the registration order (lower values execute first).
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Gets the model types handled by this configuration class that should be skipped by the source generator.
    /// </summary>
    public Type[] HandledTypes { get; set; }

    /// <summary>
    /// Gets or sets a single model type handled by this configuration class.
    /// </summary>
    public Type? HandledType
    {
        get => HandledTypes.Length > 0 ? HandledTypes[0] : null;
        set => HandledTypes = value != null ? new[] { value } : Type.EmptyTypes;
    }

    public MapperConfigurationAttribute() : this(0)
    {
    }

    public MapperConfigurationAttribute(int order)
    {
        Order = order;
        HandledTypes = Type.EmptyTypes;
    }

    public MapperConfigurationAttribute(int order, params Type[] handledTypes)
    {
        Order = order;
        HandledTypes = handledTypes ?? Type.EmptyTypes;
    }

    public MapperConfigurationAttribute(int order, Type handledType) : this(order)
    {
        HandledType = handledType;
    }

    public MapperConfigurationAttribute(params Type[] handledTypes) : this(0, handledTypes)
    {
    }

    public MapperConfigurationAttribute(Type handledType) : this(0, handledType)
    {
    }
}

