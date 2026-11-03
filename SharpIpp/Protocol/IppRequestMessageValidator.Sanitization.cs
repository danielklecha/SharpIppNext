using System.Linq;
using SharpIpp.Exceptions;
using SharpIpp.Protocol.Extensions;
using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol;

public partial class IppRequestMessageValidator
{
    /// <summary>
    /// Validates that printer-input-tray, printer-output-tray, and printer-supply collection attributes
    /// do not contain control characters (0x00-0x1F, 0x7F) in their string values.
    /// Spec: PWG 5100.13 Sections 6.6.9, 6.6.10, and 6.6.11.
    /// </summary>
    private void ValidateTrayAndSupplyControlCharacters(IIppRequestMessage request)
    {
        var targetCollectionNames = new[]
        {
            IppAttributeNames.PrinterInputTray,
            IppAttributeNames.PrinterOutputTray,
            IppAttributeNames.PrinterSupply
        };

        foreach (var colName in targetCollectionNames)
        {
            var collections = request.PrinterAttributes.EnumerateNamedCollections(colName);
            foreach (var col in collections)
            {
                if (col.Count == 1 && col[0].Tag.IsOutOfBand())
                    continue;

                var members = col.FromBegCollection().Where(x => x.Tag != Tag.EndCollection);
                foreach (var member in members)
                {
                    if (member.Value is NoValue)
                        continue;

                    if (HasControlCharacters(member.Value))
                    {
                        throw new IppRequestException(
                            $"Attribute '{member.Name}' in '{colName}' collection contains forbidden control character(s) (0x00-0x1F, 0x7F)",
                            request,
                            IppStatusCode.ClientErrorBadRequest);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Validates PWG 5100.15 input-attributes collection rules:
    /// input-brightness, input-contrast, and input-sharpness MUST NOT be supplied when input-auto-exposure is true.
    /// Spec: PWG 5100.15 Section 7.2.
    /// </summary>
    private void ValidateInputAttributesRules(IIppRequestMessage request)
    {
        var groups = new[]
        {
            request.OperationAttributes,
            request.JobAttributes,
            request.DocumentAttributes
        };

        foreach (var group in groups)
        {
            var collections = group.EnumerateNamedCollections(IppAttributeNames.InputAttributes);
            foreach (var col in collections)
            {
                if (col.Count == 1 && col[0].Tag.IsOutOfBand())
                    continue;

                var members = col.FromBegCollection().Where(x => x.Tag != Tag.EndCollection).ToArray();
                var autoExposureAttr = members.FirstOrDefault(x => x.Name == IppAttributeNames.InputAutoExposure);
                if (autoExposureAttr.Value != null)
                {
                    bool isAutoExposureTrue = false;
                    if (autoExposureAttr.Value is bool autoExposureVal)
                    {
                        isAutoExposureTrue = autoExposureVal;
                    }
                    else if (autoExposureAttr.Value is int autoExposureInt)
                    {
                        isAutoExposureTrue = autoExposureInt != 0;
                    }
                    else if (autoExposureAttr.Value.ToString() == "true")
                    {
                        isAutoExposureTrue = true;
                    }

                    if (isAutoExposureTrue)
                    {
                        var forbidden = new[] { IppAttributeNames.InputBrightness, IppAttributeNames.InputContrast, IppAttributeNames.InputSharpness };
                        foreach (var name in forbidden)
                        {
                            if (members.Any(x => x.Name == name))
                            {
                                throw new IppRequestException(
                                    $"invalid input-attributes: '{name}' MUST NOT be supplied when 'input-auto-exposure' is true",
                                    request,
                                    IppStatusCode.ClientErrorBadRequest);
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Helper method to check if a value is a string or StringWithLanguage and contains control characters.
    /// </summary>
    private static bool HasControlCharacters(object? value)
    {
        if (value is string str)
        {
            return HasControlCharacters(str);
        }
        if (value is StringWithLanguage swl)
        {
            return HasControlCharacters(swl.Value) || HasControlCharacters(swl.Language);
        }
        return false;
    }

    private static bool HasControlCharacters(string? str)
    {
        return str != null && str.Any(c => c < 0x20 || c == 0x7F);
    }

    /// <summary>
    /// Converts an attribute value to a raw byte array if it represents an octet string or byte array.
    /// </summary>
    private static byte[]? GetOctetStringBytes(object? value)
    {
        if (value is OctetString octetString)
            return octetString.Value;
        if (value is byte[] bytes)
            return bytes;
        return null;
    }

    /// <summary>
    /// Validates the structure and encoding of the document-metadata attribute values.
    /// Spec: PWG 5100.13 Section 6.1.1 (document-metadata).
    /// </summary>
    private void ValidateDocumentMetadata(IIppRequestMessage request)
    {
        var metadataAttributes = request.OperationAttributes
            .Concat(request.JobAttributes)
            .Concat(request.DocumentAttributes)
            .Where(x => x.Name == IppAttributeNames.DocumentMetadata);

        foreach (var attr in metadataAttributes)
        {
            if (attr.Tag.IsOutOfBand())
                continue;

            var bytes = GetOctetStringBytes(attr.Value);
            if (bytes == null)
                continue;

            if (!IppStructuredString.IsValidUtf8String(bytes))
                throw new IppRequestException("invalid document-metadata value encoding: must be valid UTF-8 and contain no control characters", request, IppStatusCode.ClientErrorBadRequest);

            var decoded = System.Text.Encoding.UTF8.GetString(bytes);
            var eqIndex = decoded.IndexOf('=');
            if (eqIndex <= 0)
                throw new IppRequestException("invalid document-metadata value: must be in keyword=value format", request, IppStatusCode.ClientErrorBadRequest);

            var keyword = decoded.Substring(0, eqIndex);
            if (!DocumentMetadata.IsValidDocumentMetadataKeyword(keyword))
                throw new IppRequestException($"invalid document-metadata keyword '{keyword}'", request, IppStatusCode.ClientErrorBadRequest);
        }
    }
}

