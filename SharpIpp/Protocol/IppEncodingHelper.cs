using System;
using System.Text;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

/// <summary>
/// Provides shared encoding resolution utilities with safe UTF-8 fallback.
/// </summary>
internal static class IppEncodingHelper
{
    /// <summary>
    /// Gets the <see cref="Encoding"/> corresponding to the specified charset name,
    /// falling back to UTF-8 if null, empty, or unrecognized.
    /// </summary>
    /// <param name="charsetName">The name of the character set.</param>
    /// <returns>The resolved <see cref="Encoding"/>.</returns>
    public static Encoding GetEncoding(string? charsetName)
    {
        if (string.IsNullOrWhiteSpace(charsetName))
            return Encoding.UTF8;

        var name = charsetName!.Trim();

        // Fast-path common IPP charsets without Exception/dictionary overhead
        if (string.Equals(name, "utf-8", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(name, "utf8", StringComparison.OrdinalIgnoreCase))
        {
            return Encoding.UTF8;
        }

        if (string.Equals(name, "us-ascii", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(name, "ascii", StringComparison.OrdinalIgnoreCase))
        {
            return Encoding.ASCII;
        }

        try
        {
            return Encoding.GetEncoding(name);
        }
        catch
        {
            return Encoding.UTF8;
        }
    }

    /// <summary>
    /// Resolves the <see cref="Encoding"/> from the provided <see cref="IppAttribute"/> containing a charset name,
    /// falling back to UTF-8 if null, unassigned, or unrecognized.
    /// </summary>
    /// <param name="charsetAttr">The charset attribute.</param>
    /// <returns>The resolved <see cref="Encoding"/>.</returns>
    public static Encoding GetEncoding(IppAttribute? charsetAttr)
    {
        return GetEncoding(charsetAttr?.Value?.ToString());
    }
}

