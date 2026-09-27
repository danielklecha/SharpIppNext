using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpIpp.Protocol.Models;
using SharpIpp.Validation;

namespace SharpIpp.Tests.Unit.Validation;

[TestClass]
[ExcludeFromCodeCoverage]
public class GeneratedModelValidatorTests
{
    private static readonly Assembly SharpIppAssembly = typeof(GeneratedModelValidator).Assembly;

    [TestMethod]
    public void TryValidate_WhenNull_ReturnsTrue()
    {
        var results = new List<string>();
        var visited = new HashSet<object>();

        var handled = GeneratedModelValidator.TryValidate(null, Encoding.UTF8, results, visited);

        handled.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void TryValidate_WhenPrimitiveOrStringOrEnum_ReturnsTrue()
    {
        var results = new List<string>();
        var visited = new HashSet<object>();

        // string
        GeneratedModelValidator.TryValidate("some string", Encoding.UTF8, results, visited).Should().BeTrue();

        // primitive
        GeneratedModelValidator.TryValidate(12345, Encoding.UTF8, results, visited).Should().BeTrue();
        GeneratedModelValidator.TryValidate(true, Encoding.UTF8, results, visited).Should().BeTrue();

        // enum
        GeneratedModelValidator.TryValidate(Tag.Unsupported, Encoding.UTF8, results, visited).Should().BeTrue();
        GeneratedModelValidator.TryValidate(IppStatusCode.SuccessfulOk, Encoding.UTF8, results, visited).Should().BeTrue();

        results.Should().BeEmpty();
    }

    [TestMethod]
    public void TryValidate_WhenAlreadyVisited_ReturnsTrue()
    {
        var results = new List<string>();
        var visited = new HashSet<object>();
        var dummyObj = new object();
        visited.Add(dummyObj);

        var handled = GeneratedModelValidator.TryValidate(dummyObj, Encoding.UTF8, results, visited);

        handled.Should().BeTrue();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void TryValidate_WhenUnknownType_ReturnsFalse()
    {
        var results = new List<string>();
        var visited = new HashSet<object>();
        var unknownObj = new StringBuilder();

        var handled = GeneratedModelValidator.TryValidate(unknownObj, Encoding.UTF8, results, visited);

        handled.Should().BeFalse();
        results.Should().BeEmpty();
    }

    [TestMethod]
    public void TryValidate_AllModelTypes_DefaultAndValidValues_ReturnsTrueWithNoErrors()
    {
        var modelTypes = GetGeneratedModelTypes();
        modelTypes.Should().NotBeEmpty();

        foreach (var type in modelTypes)
        {
            var instance = CreateInstance(type);
            EnsureValidValues(instance, type);

            var results = new List<string>();
            var visited = new HashSet<object>();

            var handled = GeneratedModelValidator.TryValidate(instance, Encoding.UTF8, results, visited);

            handled.Should().BeTrue($"TryValidate should handle model type {type.FullName}");
            results.Should().BeEmpty($"Model type {type.FullName} should have no validation errors when valid");
            visited.Should().Contain(instance);
        }
    }

    [TestMethod]
    public void TryValidate_AllModelTypes_PopulatedChildrenAndCollections_TraversesRecursively()
    {
        var modelTypes = GetGeneratedModelTypes();

        foreach (var type in modelTypes)
        {
            var instance = CreateInstance(type);
            EnsureValidValues(instance, type);
            PopulateChildrenAndCollections(instance, type);

            var results = new List<string>();
            var visited = new HashSet<object>();

            var handled = GeneratedModelValidator.TryValidate(instance, Encoding.UTF8, results, visited);

            handled.Should().BeTrue($"TryValidate should handle model type {type.FullName} with children");
        }
    }

    [TestMethod]
    public void TryValidate_AllModelTypes_WithInvalidAttributes_CollectsErrors()
    {
        var modelTypes = GetGeneratedModelTypes();

        foreach (var type in modelTypes)
        {
            var propertiesWithAttrs = GetValidatedProperties(type);
            if (propertiesWithAttrs.Count == 0)
                continue;

            // For each validated property, create an instance where this property is invalid
            foreach (var (prop, attr) in propertiesWithAttrs)
            {
                var instance = CreateInstance(type);
                EnsureValidValues(instance, type);
                if (!SetInvalidValue(instance, prop, attr))
                    continue;

                var results = new List<string>();
                var visited = new HashSet<object>();

                var handled = GeneratedModelValidator.TryValidate(instance, Encoding.UTF8, results, visited);

                handled.Should().BeTrue();
                results.Should().NotBeEmpty($"Property {type.Name}.{prop.Name} should have generated a validation error");
                results.Should().Contain(msg => msg.Contains(prop.Name));
            }
        }
    }

    [TestMethod]
    public void TryValidate_WhenModelHasTautologicalRange_ValidatesSuccessfully()
    {
        var results = new List<string>();
        var visited = new HashSet<object>();

        var printObject = new PrintObject
        {
            TransformationMatrix = new[] { int.MinValue, 0, int.MaxValue }
        };

        var handled = GeneratedModelValidator.TryValidate(printObject, Encoding.UTF8, results, visited);

        handled.Should().BeTrue();
        results.Should().BeEmpty();

        var jobDescription = new JobDescriptionAttributes
        {
            XImageShiftActual = new[] { int.MinValue, int.MaxValue },
            YImageShiftActual = new[] { 0 }
        };

        results.Clear();
        visited.Clear();
        handled = GeneratedModelValidator.TryValidate(jobDescription, Encoding.UTF8, results, visited);

        handled.Should().BeTrue();
        results.Should().BeEmpty();
    }

    private static List<Type> GetGeneratedModelTypes()
    {
        return SharpIppAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType && !t.IsNested)
            .Where(t => t.Namespace != null &&
                        !t.Namespace.StartsWith("SharpIpp.Exceptions") &&
                        !t.Namespace.StartsWith("SharpIpp.Generators") &&
                        !t.Namespace.StartsWith("SharpIpp.Validation") &&
                        (t.Namespace.StartsWith("SharpIpp.Models") || t.Namespace.StartsWith("SharpIpp.Protocol.Models") || HasValidationAttributes(t)))
            .Where(t => t.Name != "ExtendedValue" && t.Name != "UnknownValue")
            .OrderByDescending(GetInheritanceDepth)
            .ThenBy(t => t.FullName)
            .ToList();
    }

    private static int GetInheritanceDepth(Type type)
    {
        int depth = 0;
        var curr = type.BaseType;
        while (curr != null && curr != typeof(object))
        {
            depth++;
            curr = curr.BaseType;
        }
        return depth;
    }

    private static bool HasValidationAttributes(Type type)
    {
        return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Any(p => p.GetCustomAttributes<IppValidationAttribute>(true).Any());
    }

    private static object CreateInstance(Type type)
    {
        if (type == typeof(Uri))
            return new Uri("http://127.0.0.1/");
        if (type == typeof(System.IO.Stream))
            return new System.IO.MemoryStream();
        if (type == typeof(string))
            return string.Empty;

        try
        {
            var ctor = type.GetConstructor(Type.EmptyTypes);
            if (ctor != null)
                return Activator.CreateInstance(type)!;
        }
        catch { }

        return RuntimeHelpers.GetUninitializedObject(type);
    }

    private static object? CreateElementInstance(Type type)
    {
        if (type.IsAbstract || type.IsInterface)
        {
            var concrete = SharpIppAssembly.GetTypes().FirstOrDefault(t => !t.IsAbstract && !t.IsInterface && type.IsAssignableFrom(t));
            if (concrete != null)
                return CreateInstance(concrete);
            return null;
        }
        return CreateInstance(type);
    }

    private static void EnsureValidValues(object obj, Type type)
    {
        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!prop.CanWrite)
                continue;

            foreach (var attr in prop.GetCustomAttributes<IppValidationAttribute>(true))
            {
                if (attr is RangeAttribute rangeAttr)
                {
                    if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                    {
                        prop.SetValue(obj, rangeAttr.Minimum);
                    }
                    else if (prop.PropertyType == typeof(SharpIpp.Protocol.Models.Range) || prop.PropertyType == typeof(SharpIpp.Protocol.Models.Range?))
                    {
                        prop.SetValue(obj, new SharpIpp.Protocol.Models.Range(rangeAttr.Minimum, rangeAttr.Maximum));
                    }
                }
            }
        }
    }

    private static void PopulateChildrenAndCollections(object obj, Type type)
    {
        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (IsSharpIppModelType(prop.PropertyType))
            {
                if (prop.CanWrite)
                {
                    var child = CreateInstance(prop.PropertyType);
                    EnsureValidValues(child, prop.PropertyType);
                    prop.SetValue(obj, child);
                }
            }
            else if (IsClassArrayOrEnumerable(prop.PropertyType, out var elemType))
            {
                var child = CreateElementInstance(elemType!);
                if (child == null)
                    continue;

                EnsureValidValues(child, child.GetType());

                if (prop.PropertyType.IsArray)
                {
                    if (prop.CanWrite)
                    {
                        var arr = Array.CreateInstance(elemType!, 2);
                        arr.SetValue(null, 0); // covers item == null branch
                        arr.SetValue(child, 1); // covers item != null branch
                        prop.SetValue(obj, arr);
                    }
                }
                else
                {
                    var listType = typeof(List<>).MakeGenericType(elemType!);
                    if (prop.CanWrite && prop.PropertyType.IsAssignableFrom(listType))
                    {
                        var list = (IList)Activator.CreateInstance(listType)!;
                        list.Add(null);
                        list.Add(child);
                        prop.SetValue(obj, list);
                    }
                    else if (!prop.CanWrite && prop.GetValue(obj) is IList existingList)
                    {
                        existingList.Add(null);
                        existingList.Add(child);
                    }
                }
            }
        }
    }

    private static bool IsSharpIppModelType(Type? type)
    {
        if (type == null || !type.IsClass || type.IsArray || type == typeof(string))
            return false;
        if (type.Assembly != SharpIppAssembly)
            return false;
        var ns = type.Namespace ?? "";
        if (ns.StartsWith("SharpIpp.Exceptions") || ns.StartsWith("SharpIpp.Generators") || ns.StartsWith("SharpIpp.Validation"))
            return false;
        return true;
    }

    private static bool IsClassArrayOrEnumerable(Type type, out Type? elementType)
    {
        elementType = null;
        if (type == null || type == typeof(string))
            return false;

        if (type.IsArray)
        {
            var elem = type.GetElementType()!;
            if (elem.IsClass && elem != typeof(string))
            {
                elementType = elem;
                return true;
            }
            return false;
        }

        foreach (var iface in type.GetInterfaces().Concat(new[] { type }))
        {
            if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var elem = iface.GetGenericArguments()[0];
                if (elem.IsClass && elem != typeof(string))
                {
                    elementType = elem;
                    return true;
                }
            }
        }
        return false;
    }

    private static List<(PropertyInfo Property, IppValidationAttribute Attribute)> GetValidatedProperties(Type type)
    {
        var list = new List<(PropertyInfo, IppValidationAttribute)>();
        var visitedNames = new HashSet<string>();
        var curr = type;

        while (curr != null && curr != typeof(object))
        {
            foreach (var prop in curr.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (!visitedNames.Add(prop.Name))
                    continue;

                foreach (var attr in prop.GetCustomAttributes<IppValidationAttribute>(false))
                {
                    list.Add((prop, attr));
                }
            }
            curr = curr.BaseType;
        }

        return list;
    }

    private static bool SetInvalidValue(object obj, PropertyInfo prop, IppValidationAttribute attr)
    {
        if (!prop.CanWrite)
            return false;

        if (attr is RangeAttribute rangeAttr)
        {
            if (rangeAttr.Ranges.Length > 2)
            {
                if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                {
                    prop.SetValue(obj, -999999);
                    return true;
                }
                if (prop.PropertyType == typeof(int[]))
                {
                    prop.SetValue(obj, new int[] { -999999 });
                    return true;
                }
            }

            if (rangeAttr.Minimum == int.MinValue && rangeAttr.Maximum == int.MaxValue)
                return false;

            int invalidVal = rangeAttr.Minimum > int.MinValue ? rangeAttr.Minimum - 1 : rangeAttr.Maximum + 1;
            if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
            {
                prop.SetValue(obj, invalidVal);
                return true;
            }
            if (prop.PropertyType == typeof(SharpIpp.Protocol.Models.Range) || prop.PropertyType == typeof(SharpIpp.Protocol.Models.Range?))
            {
                prop.SetValue(obj, new SharpIpp.Protocol.Models.Range(invalidVal, invalidVal));
                return true;
            }
            if (prop.PropertyType == typeof(int[]))
            {
                prop.SetValue(obj, new int[] { invalidVal });
                return true;
            }
            if (prop.PropertyType == typeof(int?[]))
            {
                prop.SetValue(obj, new int?[] { invalidVal });
                return true;
            }
        }
        else if (attr is ByteRangeAttribute byteRangeAttr)
        {
            if (prop.PropertyType == typeof(string))
            {
                prop.SetValue(obj, new string('x', byteRangeAttr.Maximum + 10));
                return true;
            }
            if (prop.PropertyType == typeof(OctetString) || prop.PropertyType == typeof(OctetString?))
            {
                prop.SetValue(obj, new OctetString(new byte[byteRangeAttr.Maximum + 10]));
                return true;
            }
        }
        else if (attr is MetadataAttribute)
        {
            if (prop.PropertyType == typeof(DocumentMetadata))
            {
                prop.SetValue(obj, new DocumentMetadata(new Dictionary<string, string> { { "invalid keyword with spaces", "value" } }));
                return true;
            }
        }

        return false;
    }
}
