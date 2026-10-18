using System.Collections.Generic;
using System.Text;

namespace SharpIpp.Validation;

internal static class IppModelValidator
{
    [System.ThreadStatic]
    private static HashSet<object>? _visitedPool;

    [System.ThreadStatic]
    private static List<string>? _resultsPool;

    public static void Validate(object obj, Encoding encoding)
    {
        if (obj == null)
            return;

        var visited = _visitedPool;
        if (visited != null)
            _visitedPool = null;
        else
            visited = new HashSet<object>();

        var results = _resultsPool;
        if (results != null)
            _resultsPool = null;
        else
            results = new List<string>();

        try
        {
            GeneratedModelValidator.TryValidate(obj, encoding, results, visited);

            if (results.Count > 0)
            {
                var errors = string.Join("; ", results);
                throw new ValidationException($"Validation failed: {errors}");
            }
        }
        finally
        {
            visited.Clear();
            _visitedPool = visited;
            results.Clear();
            _resultsPool = results;
        }
    }
}
