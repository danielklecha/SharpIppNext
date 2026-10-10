# Advanced NoValue in Client

In the Internet Printing Protocol (IPP), the `no-value` out-of-band tag (`Tag.NoValue` / `0x13`) indicates that an attribute is supported or requested by an entity, but currently has no configured or assigned value.

SharpIppNext natively integrates `Tag.NoValue` into its strongly-typed object model through standard types that implement `INoValue`:
- **`IppValue<T>?`**: Wraps value and reference types, including `int`, `bool`, `DateTimeOffset`, enums, keywords, complex collections, and multi-valued arrays (`IppValue<T[]>?`).
- **`StringWithLanguage?`**: The standard type for all IPP text and name attributes (`Tag.TextWithoutLanguage`, `Tag.TextWithLanguage`, `Tag.NameWithoutLanguage`, `Tag.NameWithLanguage`). It implements `INoValue` natively without requiring wrapping.
- **`OctetString?`**: The standard type for binary octet string attributes, which also implements `INoValue` natively.

## Understanding Attribute States

Every IPP attribute mapped in SharpIppNext supports three distinct states:

| State | C# Representation | Meaning in IPP |
| :--- | :--- | :--- |
| **Missing / Omitted** | `null` | The attribute was not sent or is omitted from the message. |
| **NoValue** | `NoValue.Instance` (or `default` where `IsValue == false`) | The attribute was explicitly transmitted with `Tag.NoValue` (`0x13`). |
| **Concrete Value** | Value assignment or struct instance (`IsValue == true`) | The attribute contains a concrete value. |

## Setting Values in Requests

All typed properties support seamless assignment of concrete values, `null` (to omit), or `NoValue.Instance` via implicit conversions.

### 1. Assigning Concrete Values

```csharp
var request = new PrintJobRequest
{
    OperationAttributes = new PrintJobOperationAttributes
    {
        // StringWithLanguage: implicit conversion from string
        JobName = "My Document",

        // Or with an explicit natural language tag:
        // JobName = new StringWithLanguage("en-US", "My Document"),

        // IppValue<bool>: implicit conversion from bool
        IppAttributeFidelity = true,

        // IppValue<int>: implicit conversion from int
        Copies = 2
    }
};
```

### 2. Sending `NoValue`

To send the out-of-band `Tag.NoValue` for any attribute, assign `NoValue.Instance`:

```csharp
var request = new ValidateJobRequest
{
    OperationAttributes = new ValidateJobOperationAttributes
    {
        // Sends Tag.NoValue for a StringWithLanguage attribute
        DocumentName = NoValue.Instance,

        // Sends Tag.NoValue for an OctetString attribute
        DocumentPassword = NoValue.Instance,

        // Sends Tag.NoValue for an IppValue<T> attribute
        DocumentCharset = NoValue.Instance
    }
};
```

### 3. Multi-Valued Attributes (Arrays)

Multi-valued attributes use `IppValue<T[]>?`:

```csharp
// Sending a list of values
request.OperationAttributes.RequestedAttributes = new[] { "job-id", "job-state" };

// Sending Tag.NoValue for the entire array attribute
request.OperationAttributes.RequestedAttributes = NoValue.Instance;
```

## Reading Values from Responses

SharpIppNext provides multiple convenient ways to inspect and handle attribute states:

### Direct Equality Comparison

You can compare any `IppValue<T>`, `StringWithLanguage`, or `OctetString` directly against `NoValue.Instance`:

```csharp
if (response.JobAttributes?.JobImpressions == NoValue.Instance)
{
    Console.WriteLine("The printer reported NoValue for JobImpressions.");
}

if (response.JobAttributes?.JobName == NoValue.Instance)
{
    Console.WriteLine("The printer reported NoValue for JobName.");
}
```

### Inspecting `IsValue` / `HasValue`

```csharp
if (response.PrinterAttributes?.ColorSupported is { } colorSupported)
{
    if (colorSupported.IsValue)
    {
        Console.WriteLine($"Color supported: {colorSupported.Value}");
    }
    else
    {
        Console.WriteLine("ColorSupported attribute was present, but set to Tag.NoValue.");
    }
}
else
{
    Console.WriteLine("ColorSupported attribute was omitted.");
}
```

### Safe Fallbacks

```csharp
// For IppValue<T>: returns value if present, or fallback default if NoValue or omitted
bool isColor = response.PrinterAttributes?.ColorSupported?.GetValueOrDefault(false) ?? false;

// For StringWithLanguage: access .Value or use implicit conversion to string? (null if NoValue or omitted)
string? jobName = response.JobAttributes?.JobName;
string displayName = response.JobAttributes?.JobName?.Value ?? "Unnamed Job";
```

### Deconstruction

```csharp
if (response.JobAttributes?.JobKOctets is { } jobKOctets)
{
    var (isValue, octets) = jobKOctets;
    if (isValue)
    {
        Console.WriteLine($"Octets: {octets}");
    }
}
```

### Pattern Matching and `TryGetValue`

```csharp
if (response.JobAttributes?.JobKOctets?.TryGetValue(out var octets) == true)
{
    Console.WriteLine($"Octets: {octets}");
}
```
