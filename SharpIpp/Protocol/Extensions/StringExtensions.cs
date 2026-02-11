using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpIpp.Protocol.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="string"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a dash-separated string to CamelCase.
        /// </summary>
        /// <param name="input">The dash-separated string.</param>
        /// <returns>The CamelCase string.</returns>
        public static string ConvertKebabCaseToCamelCase(this string input)
        {
            return string.Join("", input.Split('-').Select(x => x.First().ToString().ToUpper() + x.Substring(1).ToLower()));
        }

        /// <summary>
        /// Converts a CamelCase string to dash-separated.
        /// </summary>
        /// <param name="input">The CamelCase string.</param>
        /// <returns>The dash-separated string.</returns>
        public static string ConvertCamelCaseToKebabCase(this string input)
        {
            return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) && (char.IsLower(input[i - 1]) || i < input.Length - 1 && char.IsLower(input[i + 1]))
                ? "-" + x
                : x.ToString())).ToLowerInvariant();
        }
    }
}