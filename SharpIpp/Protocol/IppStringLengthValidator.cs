using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

/// <summary>
/// Validates that string-based and octet-based attributes do not exceed the octet (byte) length limits
/// specified in RFC 8011 Section 5.1.
/// </summary>
internal static class IppStringLengthValidator
{
    public static void Validate(
        IEnumerable<IppAttribute> attributes,
        Encoding encoding,
        Action<string> onError)
    {
        if (!TryValidate(attributes, encoding, out var error))
        {
            onError(error!);
        }
    }

    public static bool TryValidate(
        IEnumerable<IppAttribute> attributes,
        Encoding encoding,
        out string? error)
    {
        foreach (var attr in attributes)
        {
            if (attr.Value == null || attr.Value is NoValue)
                continue;

            foreach (var val in GetStringOrOctetValues(attr.Value))
            {
                switch (attr.Tag)
                {
                    case Tag.TextWithoutLanguage:
                        if (!ValidateTextOrName(val, 1023, hasWithLanguageTag: false, attr, encoding, out error))
                            return false;
                        break;

                    case Tag.TextWithLanguage:
                        if (!ValidateTextOrName(val, 1023, hasWithLanguageTag: true, attr, encoding, out error))
                            return false;
                        break;

                    case Tag.NameWithoutLanguage:
                        if (!ValidateTextOrName(val, 255, hasWithLanguageTag: false, attr, encoding, out error))
                            return false;
                        break;

                    case Tag.NameWithLanguage:
                        if (!ValidateTextOrName(val, 255, hasWithLanguageTag: true, attr, encoding, out error))
                            return false;
                        break;

                    case Tag.UriScheme:
                    case Tag.Charset:
                    case Tag.NaturalLanguage:
                        if (val.ToString() is string s63 && !ValidateStringLength(s63, 63, attr, encoding, out error))
                            return false;
                        break;

                    case Tag.Keyword:
                    case Tag.MimeMediaType:
                        if (val.ToString() is string s255 && !ValidateStringLength(s255, 255, attr, encoding, out error))
                            return false;
                        break;

                    case Tag.Uri:
                        if (val.ToString() is string s1023 && !ValidateStringLength(s1023, 1023, attr, encoding, out error))
                            return false;
                        break;

                    case Tag.OctetStringWithAnUnspecifiedFormat:
                        if (!ValidateOctetString(val, attr, encoding, out error))
                            return false;
                        break;
                }
            }
        }

        error = null;
        return true;
    }

    private static bool ValidateStringLength(
        string str,
        int maxBytes,
        IppAttribute attr,
        Encoding encoding,
        out string? error,
        string descriptor = "length")
    {
        var bytes = encoding.GetByteCount(str);
        if (bytes > maxBytes)
        {
            error = $"Attribute '{attr.Name}' of tag '{attr.Tag}' {descriptor} ({bytes} octets) exceeds RFC 8011 limit of {maxBytes} octets";
            return false;
        }

        error = null;
        return true;
    }

    private static bool ValidateTextOrName(
        object val,
        int maxBytes,
        bool hasWithLanguageTag,
        IppAttribute attr,
        Encoding encoding,
        out string? error)
    {
        if (val is StringWithLanguage swl)
        {
            var valueDescriptor = hasWithLanguageTag ? "value length" : "length";
            if (!ValidateStringLength(swl.Value ?? string.Empty, maxBytes, attr, encoding, out error, valueDescriptor))
                return false;

            if (swl.HasLanguage && !ValidateStringLength(swl.Language!, 63, attr, encoding, out error, "language length"))
                return false;

            error = null;
            return true;
        }

        return ValidateStringLength(val.ToString() ?? string.Empty, maxBytes, attr, encoding, out error, "length");
    }

    private static bool ValidateOctetString(
        object val,
        IppAttribute attr,
        Encoding encoding,
        out string? error)
    {
        int octetLength;
        if (val is OctetString os && os.Value is byte[] octetStringBytes)
            octetLength = octetStringBytes.Length;
        else if (val is byte[] bytes)
            octetLength = bytes.Length;
        else
            octetLength = encoding.GetByteCount(val.ToString() ?? string.Empty);

        if (octetLength > 1023)
        {
            error = $"Attribute '{attr.Name}' of tag '{attr.Tag}' length ({octetLength} octets) exceeds RFC 8011 limit of 1023 octets";
            return false;
        }

        error = null;
        return true;
    }

    public static IEnumerable<object> GetStringOrOctetValues(object? value)
    {
        if (value == null || value is NoValue)
            yield break;

        if (value is string || value is StringWithLanguage || value is OctetString || value is byte[])
        {
            yield return value;
        }
        else if (value is IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                foreach (var subItem in GetStringOrOctetValues(item))
                {
                    yield return subItem;
                }
            }
        }
        else
        {
            yield return value;
        }
    }
}
