# Request Validation

SharpIpp implements two tiers of validation to ensure that IPP requests are correct before being transmitted or processed:

1. **High-level model validation** (`IppRequestValidator`): Validates request properties recursively using standard .NET DataAnnotations validation.
2. **Low-level message validation** (`IppRequestMessageValidator`): Validates structural constraints and protocol rules directly on the mapped `IIppRequestMessage` representation.

---

## High-Level Model Validation (`IppRequestValidator`)

The `IppRequestValidator` processes your high-level request objects (e.g., `PrintJobRequest`) recursively before they are mapped into low-level messages. 

It checks:
* Standard data annotations like `[Required]`.
* Custom validation attributes:
  * `[ByteRange]`: Verifies that string or binary properties do not exceed specific byte size limits when encoded (e.g., `DocumentPassword` is validated to be $\le 1023$ bytes).
  * `[Metadata]`: Verifies that `document-metadata` is properly structured.

---

## Low-Level Message Validation (`IppRequestMessageValidator`)

The `IppRequestMessageValidator` is invoked on the serialized/deserialized `IIppRequestMessage`. It enforces rules defined in RFC 8011, PWG 5100.x, and other specifications.

It validates:
* **Core IPP rules**: Checks for positive request IDs, minimum version numbers, unique attributes within groups, and correct order of operation attributes (e.g., `attributes-charset` must be first).
* **Operation-specific rules**: Checks required attributes (like `document-number` or `printer-id` for target operations) and mutual exclusivity of attributes.
* **Fidelity validation**: Enforces printer capabilities match when `ipp-attribute-fidelity` is enabled.
* **Control character validation**: Verifies that structured metadata and device tray/supply collections (`printer-input-tray`, `printer-output-tray`, `printer-supply`) do not contain forbidden control characters (`0x00-0x1F`, `0x7F`) per PWG 5100.13.
* **Input attribute dependency validation**: Verifies that `input-brightness`, `input-contrast`, or `input-sharpness` are not sent when `input-auto-exposure` is set to `true` inside `input-attributes` collections per PWG 5100.15.

---

## RFC 8011 String Length Validation

RFC 8011 Section 5.1 defines strict maximum octet (byte) length limits for various attribute syntax types:

| Tag Syntax | Tag Codes | RFC 8011 Limit |
| --- | --- | --- |
| `textWithoutLanguage` / `textWithLanguage` | `0x41` / `0x35` | **1023 octets** (Section 5.1.2) |
| `nameWithoutLanguage` / `nameWithLanguage` | `0x42` / `0x36` | **255 octets** (Section 5.1.3) |
| `keyword` | `0x44` | **255 octets** (Section 5.1.4) |
| `uri` | `0x45` | **1023 octets** (Section 5.1.5) |
| `uriScheme` | `0x46` | **255 octets** (Section 5.1.6) |
| `charset` | `0x47` | **255 octets** (Section 5.1.7) |
| `naturalLanguage` | `0x48` | **255 octets** (Section 5.1.8) |
| `mimeMediaType` | `0x49` | **255 octets** (Section 5.1.9) |

### Truncation and Validation Behavior

> [!IMPORTANT]
> **SharpIpp does NOT automatically truncate strings.**
> While the IPP specifications allow external systems to silently truncate too-long strings, SharpIpp prioritizes data integrity. Instead of silent truncation, the library validates these limits directly.

* By default, `IppRequestMessageValidator` validates that all string-based attributes do not exceed their respective octet limits (calculated using UTF-8 byte count).
* If a string attribute exceeds the limit, validation fails and throws an `IppRequestException` with status code `ClientErrorBadRequest`.
* To disable this check and allow longer values, set the `ValidateStringLengthLimits` property to `false`:

```csharp
var validator = new IppRequestMessageValidator
{
    ValidateStringLengthLimits = false
};
```
