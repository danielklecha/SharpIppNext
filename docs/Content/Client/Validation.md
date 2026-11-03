# Request Validation

SharpIpp implements two tiers of validation to ensure that IPP requests are correct before being transmitted or processed:

1. **High-level model validation** (`IppRequestValidator`): Validates request properties recursively using a lightweight, built-in validation engine.
2. **Low-level message validation** (`IppRequestMessageValidator`): Validates structural constraints and protocol rules directly on the mapped `IIppRequestMessage` representation.

---

## High-Level Model Validation (`IppRequestValidator`)

The `IppRequestValidator` processes your high-level request objects (e.g., `PrintJobRequest`) recursively before they are mapped into low-level messages. 

It checks:
* Model validation attributes:
  * `[Range]`: Enforces standard integer ranges or `Range` constraints defined by the IPP specifications for single values, IPP `Range` structs, and collections thereof. Supports both single ranges (`[Range(min, max)]`) and disjoint multiple ranges (`[Range(min1, max1, min2, max2, ...)]`).
* Custom validation attributes:
  * `[ByteRange]`: Verifies that string or binary properties (single items or collections) do not exceed specific byte size limits when encoded under the request charset (e.g., `DocumentPassword` is validated to be $\le 1023$ bytes). Supports single and multi-range limits.
  * `[Metadata]`: Verifies that `document-metadata` is properly structured.

---

## Low-Level Message Validation (`IppRequestMessageValidator`)

The `IppRequestMessageValidator` is invoked on the serialized/deserialized `IIppRequestMessage`. It enforces rules defined in RFC 8011, PWG 5100.x, and other specifications.

It validates:
* **Core IPP rules**: Checks for positive request IDs, minimum version numbers, unique attributes within groups, and correct order of operation attributes (e.g., `attributes-charset` must be first).
* **Target attribute ordering**: Validates strict sequence of target URI (`printer-uri`, `job-uri`, `system-uri`) and secondary identifiers (`job-id`, `document-number`, `resource-id`, `printer-id`) per RFC 8011 §4.1.5, PWG 5100.5 §5.1, and PWG 5100.22 §7.1.26 via `ValidateTargetAttributeOrder` (opt-in enhancement, disabled by default for real-world client and server compatibility).
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

* By default, `IppRequestMessageValidator` and `IppResponseMessageValidator` validate that all string-based attributes do not exceed their respective octet limits (calculated using UTF-8 byte count).
* If a string attribute exceeds the limit, validation fails and throws an `IppRequestException` or `IppResponseException`.
* To disable this check and allow longer values, set the `ValidateStringLengthLimits` property to `false`:

```csharp
var validator = new IppRequestMessageValidator
{
    ValidateStringLengthLimits = false
};
```

---

## Low-Level Response Message Validation (`IppResponseMessageValidator`)

The `IppResponseMessageValidator` validates deserialized `IIppResponseMessage` structures:
* **Core IPP rules**: Validates positive request IDs, minimum IPP version (1.0), defined `IppStatusCode`, attribute uniqueness within groups, and mandatory ordering (`attributes-charset` 1st, `attributes-natural-language` 2nd).
* **RFC 8011 String Length Limits**: Validates that response attributes (such as `status-message`, keywords, names, URIs) do not exceed standard octet length limits via `ValidateStringLengthLimits` (enabled by default).
* **Operation & Status-Code Rules**:
  * Status code `ClientErrorAttributesOrValuesNotSupported` (or `SuccessfulOkIgnoredOrSubstitutedAttributes` / `SuccessfulOkConflictingAttributes`) requires the presence of a non-empty `unsupported-attributes` group.
  * `Validate(response, operation)` validates operation-specific expectations (e.g., successful `PrintJob`/`CreateJob` must return `job-id` or `job-uri` and `job-state`; `GetPrinterAttributes` must contain a `printer-attributes` group).

---

## Performance & Optimization

* **Zero-Allocation Validation (Lambda & Closure Elimination)**: Low-level message validators (`IppRequestMessageValidator` and `IppResponseMessageValidator`) consistently use non-allocating `Try*` patterns (`TryValidateCoreHeader`, `TryValidateOperationAttributesPrefix`, `TryValidateUniqueAttributesCore`, and `TryValidateStringLengthLimitsCore`) returning `bool` and outputting errors. This eliminates compiler-generated closure (`DisplayClass`) allocations and delegate instantiations, ensuring zero heap allocations on the validation happy path.
* **Fast-Path Encoding Resolution**: `IppEncodingHelper` includes zero-allocation ordinal matching for common charsets (`utf-8`, `utf8`, `us-ascii`, `ascii`), bypassing .NET dictionary lookups and exception handling for invalid strings.
* **Single-Pass Header & Target Scanning**: Target attributes (`printer-uri`, `system-uri`, `job-uri`) and unique attribute groups are evaluated in single passes without repeated LINQ traversals.
* **Modular Partial-Class Organization**: Complex validation rules are segregated into clean partial files:
  * `IppRequestMessageValidator.Operations.cs` (Operation-specific and state rules)
  * `IppRequestMessageValidator.JobTemplate.cs` (Job template attributes and capability matching)
  * `IppRequestMessageValidator.Overrides.cs` (PWG 5100.6 overrides)
  * `IppRequestMessageValidator.Sanitization.cs` (Tray/supply sanitization and document metadata)
  * `IppRequestMessageValidator.DefaultSupported.cs` (RFC 3380 default vs supported rules)
  * `IppRequestMessageValidator.TargetOrder.cs` (RFC 8011 target URI ordering rules)
