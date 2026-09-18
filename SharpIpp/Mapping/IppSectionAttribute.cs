namespace SharpIpp.Mapping;

using System;
using SharpIpp.Protocol.Models;

/// <summary>
/// Associates a model class or property with an IPP section group (delimiter tag).
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class IppSectionAttribute : Attribute
{
    public SectionTag SectionTag { get; }

    public IppSectionAttribute(SectionTag sectionTag)
    {
        SectionTag = sectionTag;
    }
}

