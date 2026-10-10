# StringWithLanguage Standard Type

In the Internet Printing Protocol (IPP), text and name attributes can be represented in two wire formats:
- **Without language**: `Tag.TextWithoutLanguage` (`0x41`) or `Tag.NameWithoutLanguage` (`0x42`), where the text inherits the natural language specified at the operation or message level.
- **With language**: `Tag.TextWithLanguage` (`0x35`) or `Tag.NameWithLanguage` (`0x36`), where the attribute carries its own specific natural language tag (e.g., `en-US`, `de-DE`, `fr-FR`).

In SharpIppNext, `StringWithLanguage` is the **standard type** used across all typed request and response classes for IPP text and name attributes (such as `JobName`, `DocumentName`, `PrinterInfo`, `PrinterLocation`, etc.). It is built directly into the core object model and does not require raw request manipulation or external wrappers.

## Creating and Assigning `StringWithLanguage`

`StringWithLanguage` provides seamless conversions from standard C# strings and out-of-band `NoValue` instances.

### 1. Plain String (Without Language)

Assign a standard C# string literal or variable directly to any `StringWithLanguage?` property via implicit conversion. SharpIppNext will encode it with the appropriate `*WithoutLanguage` tag:

```csharp
var request = new PrintJobRequest
{
    OperationAttributes = new PrintJobOperationAttributes
    {
        // Implicitly converted to StringWithLanguage without a language tag
        JobName = "Annual Report"
    }
};
```

### 2. Specifying a Natural Language Tag

When you need to explicitly attach a natural language code to an attribute, instantiate `StringWithLanguage` with the language tag. SharpIppNext will encode it with the appropriate `*WithLanguage` tag:

```csharp
var request = new PrintJobRequest
{
    OperationAttributes = new PrintJobOperationAttributes
    {
        // Encoded as Tag.NameWithLanguage or Tag.TextWithLanguage with language "en-US"
        JobName = new StringWithLanguage("en-US", "Annual Report")
    }
};
```

### 3. Setting `NoValue`

`StringWithLanguage` implements `INoValue` natively. Assign `NoValue.Instance` to transmit the out-of-band `Tag.NoValue` (`0x13`):

```csharp
var request = new ValidateJobRequest
{
    OperationAttributes = new ValidateJobOperationAttributes
    {
        // Encoded as Tag.NoValue in the IPP request
        DocumentName = NoValue.Instance
    }
};
```

## Inspecting and Reading `StringWithLanguage`

Properties of type `StringWithLanguage?` returned in IPP responses provide convenient inspection methods:

```csharp
var response = await client.GetJobAttributesAsync(request);
StringWithLanguage? jobName = response.JobAttributes?.JobName;

if (jobName == null)
{
    Console.WriteLine("JobName was omitted from the response.");
}
else if (jobName == NoValue.Instance) // or if (!jobName.Value.IsValue)
{
    Console.WriteLine("JobName is supported but reported as NoValue.");
}
else
{
    // Access string value and language tag
    Console.WriteLine($"Value: {jobName.Value.Value}");

    if (jobName.Value.HasLanguage)
    {
        Console.WriteLine($"Language: {jobName.Value.Language}");
    }

    // Implicit conversion to string? (evaluates to null if NoValue)
    string? text = jobName;
    Console.WriteLine($"String representation: {text}");
}
```

## Summary of Properties and Operators

| Member | Description |
| :--- | :--- |
| `Value` (`string`) | The underlying string text. |
| `Language` (`string?`) | The natural language tag (e.g., `"en-US"`), or `null` if none. |
| `HasLanguage` (`bool`) | `true` if a language tag is present. |
| `IsValue` (`bool`) | `false` if the instance represents `Tag.NoValue`, `true` otherwise. |
| `implicit operator string?` | Converts to `string?`, returning `null` if in the `NoValue` state. |
| `implicit operator StringWithLanguage(string?)` | Converts a `string` into a `StringWithLanguage` without a language tag. |
| `implicit operator StringWithLanguage(NoValue)` | Converts `NoValue.Instance` into the `Tag.NoValue` state. |
| `==` / `!=` | Supports equality comparison with other `StringWithLanguage` instances, `string`, and `NoValue.Instance`. |
