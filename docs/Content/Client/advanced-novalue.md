# Advanced NoValue in Client

In IPP, the `no-value` out-of-band tag (`0x13`) indicates that an attribute is supported by the printer or client but currently has no value. SharpIppNext represents this state using the generic wrapper `IppValue<T>`.

## Understanding Attribute States

Every IPP attribute mapped via `IppValue<T>?` can be in one of three distinct states:

| State | C# Representation | Meaning in IPP |
| :--- | :--- | :--- |
| **Missing / Omitted** | `null` | The attribute was not sent or is not requested. |
| **NoValue** | `NoValue.Instance` (or `default(IppValue<T>)`) | The attribute was explicitly sent with `Tag.NoValue` (`0x13`). |
| **Concrete Value** | `new IppValue<T>(value)` (or implicitly `value`) | The attribute contains a concrete value of type `T`. |

> [!NOTE]
> Unlike previous versions that used sentinel values (such as `int.MinValue` or `"###NOVALUE###"`), `IppValue<T>` works natively for all types, including `bool`, numbers, strings, dates, enums, custom structs (`Range`, `Resolution`, `OctetString`), and multi-valued arrays (`IppValue<T[]>?`).

## Setting Values in Requests

You can assign values or `NoValue` directly using implicit conversions.

### 1. Assigning Concrete Values

```csharp
var request = new PrintJobRequest
{
    OperationAttributes = new PrintJobOperationAttributes
    {
        JobName = "My Document",          // Implicitly converted to IppValue<string>
        IppAttributeFidelity = true,      // Implicitly converted to IppValue<bool>
        Copies = 2                        // Implicitly converted to IppValue<int>
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
        JobPassword = NoValue.Instance,    // Sends Tag.NoValue for job-password
        DocumentCharset = NoValue.Instance // Sends Tag.NoValue for document-charset
    }
};
```

### 3. Multi-Valued Attributes (Arrays)

Multi-valued attributes use `IppValue<T[]>?`:

```csharp
// Sending a list of values
request.OperationAttributes.RequestedAttributes = new[] { "job-id", "job-state" };

// Sending NoValue for the entire array attribute
request.OperationAttributes.RequestedAttributes = NoValue.Instance;
```

## Reading Values from Responses

`IppValue<T>` provides multiple convenient ways to check for and read values:

### Direct Equality Comparison

```csharp
if (response.JobAttributes.JobImpressions == NoValue.Instance)
{
    Console.WriteLine("The printer reported NoValue for JobImpressions.");
}
```

### Inspecting `IsValue` / `HasValue`

```csharp
if (response.PrinterAttributes.ColorSupported is { } colorSupported)
{
    if (colorSupported.IsValue)
    {
        Console.WriteLine($"Color supported: {colorSupported.Value}");
    }
    else
    {
        Console.WriteLine("ColorSupported attribute was present, but set to NoValue.");
    }
}
else
{
    Console.WriteLine("ColorSupported attribute was omitted.");
}
```

### Safe Fallback (`GetValueOrDefault`)

```csharp
// Returns the value if present, or fallback default (e.g. false) if NoValue or omitted
bool isColor = response.PrinterAttributes.ColorSupported?.GetValueOrDefault(false) ?? false;
```

### Deconstruction

```csharp
if (response.JobAttributes.JobKOctets is { } jobKOctets)
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
if (response.JobAttributes.JobName?.TryGetValue(out var name) == true)
{
    Console.WriteLine($"Job name: {name}");
}
```

