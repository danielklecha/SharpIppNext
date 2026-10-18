using System.Collections.Generic;

namespace SharpIpp.Validation;

internal static class ValidationHelper
{
    public static bool ValidateEmpty(object obj, HashSet<object> visited)
    {
        visited.Add(obj);
        return true;
    }
}
