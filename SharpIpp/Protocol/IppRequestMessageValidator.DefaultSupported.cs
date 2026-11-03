using System;
using System.Collections.Generic;
using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public partial class IppRequestMessageValidator
{
    /// <summary>
    /// Validates that any 'xxx-default' attribute values are within the corresponding 'xxx-supported' values.
    /// Spec: RFC 3380 Section 4.1.2.
    /// </summary>
    private void ValidateDefaultAgainstSupportedAttributes(IReadOnlyCollection<IppAttribute> attributes, IIppRequestMessage request)
    {
        var attributesByName = new Dictionary<string, List<IppAttribute>>(attributes.Count);
        foreach (var attr in attributes)
        {
            if (attr.Name == null)
                continue;
            if (!attributesByName.TryGetValue(attr.Name, out var list))
            {
                list = new List<IppAttribute>();
                attributesByName[attr.Name] = list;
            }
            list.Add(attr);
        }
        foreach (var kvp in attributesByName)
        {
            var name = kvp.Key;
            if (!name.EndsWith("-default") || name.Length <= "-default".Length)
                continue;

            var baseName = name.Substring(0, name.Length - "-default".Length);
            var supportedName = baseName + "-supported";

            if (!attributesByName.TryGetValue(supportedName, out var supportedAttrs))
                continue;

            foreach (var defaultAttr in kvp.Value)
            {
                var defaultValues = GetAttributeValues(defaultAttr).ToArray();
                foreach (var defaultValue in defaultValues)
                {
                    var isSupported = false;
                    foreach (var supportedAttr in supportedAttrs)
                    {
                        var supportedValues = GetAttributeValues(supportedAttr);
                        foreach (var supportedValue in supportedValues)
                        {
                            if (IsValueSupported(defaultValue, supportedValue))
                            {
                                isSupported = true;
                                break;
                            }
                        }
                        if (isSupported)
                            break;
                    }

                    if (!isSupported)
                    {
                        throw new IppRequestException(
                            $"invalid '{name}' value '{defaultValue}': not in '{supportedName}'",
                            request,
                            IppStatusCode.ClientErrorAttributesOrValuesNotSupported);
                    }
                }
            }
        }
    }

    private static IEnumerable<object> GetAttributeValues(IppAttribute attribute)
    {
        if (attribute.Value == null)
            yield break;

        if (attribute.Value is System.Collections.IEnumerable enumerable && !(attribute.Value is string))
        {
            foreach (var item in enumerable)
            {
                if (item != null)
                    yield return item;
            }
        }
        else
        {
            yield return attribute.Value;
        }
    }

    private static bool IsValueSupported(object defaultValue, object supportedValue)
    {
        if (supportedValue is SharpIpp.Protocol.Models.Range range)
        {
            if (defaultValue is int intVal)
            {
                return intVal >= range.Lower && intVal <= range.Upper;
            }
            if (defaultValue is long longVal)
            {
                return longVal >= range.Lower && longVal <= range.Upper;
            }
            var defStr = defaultValue.ToString();
            if (defStr != null && int.TryParse(defStr, out var parsedInt))
            {
                return parsedInt >= range.Lower && parsedInt <= range.Upper;
            }
        }

        return string.Equals(defaultValue.ToString(), supportedValue.ToString(), StringComparison.Ordinal);
    }
}

