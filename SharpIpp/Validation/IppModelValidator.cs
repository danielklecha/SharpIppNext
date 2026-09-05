using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SharpIpp.Validation;

internal static class IppModelValidator
{
    private static readonly ConcurrentDictionary<Type, PropertyValidationMetadata[]> MetadataCache = new();
    private static readonly Assembly SharpIppAssembly = typeof(IppModelValidator).Assembly;

    public static void Validate(object obj, Encoding encoding)
    {
        if (obj == null)
            return;

        var results = new List<string>();
        var visited = new HashSet<object>();

        if (!GeneratedModelValidator.TryValidate(obj, encoding, results, visited))
        {
            ValidateRecursiveFallback(obj, encoding, results, visited);
        }

        if (results.Count > 0)
        {
            var errors = string.Join("; ", results);
            throw new ValidationException($"Validation failed: {errors}");
        }
    }

    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode", Justification = "Validation models are preserved via ILLink.Descriptors.xml")]
    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2070:UnsatisfiedPublicProperties", Justification = "Validation models are preserved via ILLink.Descriptors.xml")]
    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2075:DynamicallyAccessedMembersOnTypeGetType", Justification = "Validation models are preserved via ILLink.Descriptors.xml")]
    internal static void ValidateRecursiveFallback(
        object obj,
        Encoding encoding,
        List<string> results,
        HashSet<object> visited)
    {
        if (!visited.Add(obj))
            return;

        var type = obj.GetType();
        var properties = MetadataCache.GetOrAdd(type, GetPropertiesMetadata);

        foreach (var meta in properties)
        {
            var value = meta.Property.GetValue(obj);

            if (meta.Attributes.Length > 0)
            {
                var context = new ValidationContext(encoding, meta.Name);
                foreach (var attr in meta.Attributes)
                {
                    var result = attr.IsValid(value, context);
                    if (result != null && !result.IsSuccess && result.ErrorMessage != null)
                    {
                        results.Add(result.ErrorMessage);
                    }
                }
            }

            if (value == null)
                continue;

            if (meta.IsEnumerable)
            {
                foreach (var item in (IEnumerable)value)
                {
                    if (item == null)
                        continue;

                    if (!GeneratedModelValidator.TryValidate(item, encoding, results, visited))
                    {
                        var itemType = item.GetType();
                        if (itemType.Assembly == SharpIppAssembly && !itemType.IsEnum)
                        {
                            ValidateRecursiveFallback(item, encoding, results, visited);
                        }
                    }
                }
            }
            else if (meta.IsSharpIppType)
            {
                if (!GeneratedModelValidator.TryValidate(value, encoding, results, visited))
                {
                    ValidateRecursiveFallback(value, encoding, results, visited);
                }
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode", Justification = "Validation models are preserved via ILLink.Descriptors.xml")]
    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2070:UnsatisfiedPublicProperties", Justification = "Validation models are preserved via ILLink.Descriptors.xml")]
    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2075:DynamicallyAccessedMembersOnTypeGetType", Justification = "Validation models are preserved via ILLink.Descriptors.xml")]
    private static PropertyValidationMetadata[] GetPropertiesMetadata(Type t)
    {
        var props = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var list = new List<PropertyValidationMetadata>(props.Length);
        foreach (var p in props)
        {
            if (p.GetIndexParameters().Length > 0)
                continue;
            list.Add(new PropertyValidationMetadata(p, SharpIppAssembly));
        }
        return list.ToArray();
    }

    private sealed class PropertyValidationMetadata
    {
        public PropertyInfo Property { get; }
        public string Name { get; }
        public IppValidationAttribute[] Attributes { get; }
        public bool IsSharpIppType { get; }
        public bool IsEnumerable { get; }

        public PropertyValidationMetadata(PropertyInfo property, Assembly sharpIppAssembly)
        {
            Property = property;
            Name = property.Name;
            Attributes = (IppValidationAttribute[])property.GetCustomAttributes(typeof(IppValidationAttribute), true);
            var propType = property.PropertyType;
            IsEnumerable = typeof(IEnumerable).IsAssignableFrom(propType) && propType != typeof(string);
            IsSharpIppType = propType.Assembly == sharpIppAssembly && !propType.IsEnum;
        }
    }
}
