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
    public int Order { get; }

    public MapperConfigurationAttribute(int order = 0)
    {
        Order = order;
    }
}

