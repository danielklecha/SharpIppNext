using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Validation;

internal static class IppModelValidator
{
    public static void Validate(object obj, Encoding encoding)
    {
        if (obj == null)
            return;

        var results = new List<string>();
        var visited = new HashSet<object>();

        GeneratedModelValidator.TryValidate(obj, encoding, results, visited);

        if (results.Count > 0)
        {
            var errors = string.Join("; ", results);
            throw new ValidationException($"Validation failed: {errors}");
        }
    }
}
