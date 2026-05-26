using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using SharpIpp.Protocol;

namespace SharpIpp.Validation;

/// <summary>
/// Validates high-level IPP responses using DataAnnotations validation recursively.
/// </summary>
public class IppResponseValidator : IIppResponseValidator
{
    /// <summary>
    /// Gets the default instance of the validator.
    /// </summary>
    public static IppResponseValidator Default => new();

    /// <inheritdoc />
    public void Validate<T>(T response) where T : IIppResponse
    {
        if (response == null)
            throw new ArgumentNullException(nameof(response));

        var validationResults = new List<ValidationResult>();
        var visited = new HashSet<object>();

        var charset = response.OperationAttributes?.AttributesCharset ?? "utf-8";
        Encoding encoding;
        try
        {
            encoding = Encoding.GetEncoding(charset);
        }
        catch
        {
            encoding = Encoding.UTF8;
        }

        var items = new Dictionary<object, object?>
        {
            { "Encoding", encoding }
        };

        ValidateObjectRecursive(response, validationResults, visited, items);

        if (validationResults.Any())
        {
            var errors = string.Join("; ", validationResults.Select(r => r.ErrorMessage));
            throw new ValidationException($"Validation failed: {errors}");
        }
    }

    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode", Justification = "Validation models are preserved via ILLink.Descriptors.xml")]
    [System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessage("Trimming", "IL2075:DynamicallyAccessedMembersOnTypeGetType", Justification = "Validation models are preserved via ILLink.Descriptors.xml")]
    private void ValidateObjectRecursive(
        object obj,
        List<ValidationResult> results,
        HashSet<object> visited,
        IDictionary<object, object?> items)
    {
        // Prevent circular references
        if (!visited.Add(obj))
            return;

        // Validate properties of the current object
        var context = new ValidationContext(obj, serviceProvider: null, items: items);
        Validator.TryValidateObject(obj, context, results, validateAllProperties: true);

        var type = obj.GetType();
        var sharpIppAssembly = typeof(IppResponseValidator).Assembly;

        // Retrieve properties to recurse into
        var properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        foreach (var property in properties)
        {
            // Skip indexed properties
            if (property.GetIndexParameters().Length > 0)
                continue;

            var value = property.GetValue(obj);
            if (value == null)
                continue;

            var propType = property.PropertyType;

            // Recurse into elements of collections/arrays
            if (typeof(IEnumerable).IsAssignableFrom(propType) && propType != typeof(string))
            {
                foreach (var item in (IEnumerable)value)
                {
                    if (item == null)
                        continue;

                    var itemType = item.GetType();
                    if (itemType.Assembly == sharpIppAssembly && !itemType.IsEnum)
                    {
                        ValidateObjectRecursive(item, results, visited, items);
                    }
                }
            }
            // Recurse into complex objects (defined in our assembly, not enums)
            else if (propType.Assembly == sharpIppAssembly && !propType.IsEnum)
            {
                ValidateObjectRecursive(value, results, visited, items);
            }
        }
    }
}
