# Advanced NoValue in Client Requests and Responses

In IPP, the `no-value` out-of-band tag (0x13) is used to indicate that an attribute is supported but currently has no value. SharpIppNext allows you to include this tag in your requests by using specific "special" values in your request models.

## Automatic NoValue Mapping

When you set a property in a request model to one of the following special values, SharpIppNext automatically maps it to an `IppAttribute` with the `Tag.NoValue` tag.

| Type                      | Special Value to trigger `NoValue`        |
| :------------------------ | :---------------------------------------- |
| `int`                     | `int.MinValue`                            |
| `Enum` (short underlying) | `short.MinValue`                          |
| `Enum` (int underlying)   | `int.MinValue`                            |
| `DateTime`                | `DateTime.MinValue`                       |
| `DateTimeOffset`          | `DateTimeOffset.MinValue`                 |
| `string`                  | `NoValue.NoValueString` ("###NOVALUE###") |
| `string` (Keyword)        | `string.Empty`                            |
| `Range`                   | `default(Range)`                          |
| `Resolution`              | `default(Resolution)`                     |
| `StringWithLanguage`      | `default(StringWithLanguage)`             |

### Example: Sending NoValue for an Integer

```csharp
var request = new SendDocumentRequest
{
    // ... other properties
    JobId = NoValue.GetNoValue<int>() // This will be sent as a NoValue attribute
};
```

## The Boolean Exception

For `bool` properties, it is **not possible** to use automatic mapping. This is because both `true` and `false` are valid IPP boolean values, and there is no "special" third state for a standard `bool` type that could represent `NoValue`.

If you need to send a `NoValue` tag for a boolean attribute, you must manually replace the attribute in the raw request message.

### Example: Manually adding NoValue for a boolean attribute

```csharp
// 1. Initialize your request as usual
var request = new MyCustomRequest
{
    MyBooleanProperty = false // This will be mapped as a normal boolean false
};

// 2. Create the raw request message
IIppRequestMessage rawRequest = client.CreateRawRequest(request);

// 3. Find and replace the attribute manually
for (var i = 0; i < rawRequest.OperationAttributes.Count; i++)
{
    var attribute = rawRequest.OperationAttributes[i];
    if (attribute.Name == "my-boolean-attribute")
    {
        rawRequest.OperationAttributes[i] = new IppAttribute(
            Tag.NoValue, 
            attribute.Name, 
            NoValue.Instance);
        break;
    }
}

// 4. Send the modified raw request
await client.SendAsync(printerUri, rawRequest);
```

## Detecting NoValue in Responses

Just like in requests, `bool` properties in response models **cannot** represent a `NoValue` state automatically. IPP boolean attributes are always mapped to either `true` or `false`.

If you need to detect if a boolean attribute in a response was actually a `NoValue` tag, you must inspect the raw `IIppResponseMessage`:

```csharp
IIppResponseMessage rawResponse = await client.SendAsync(printerUri, rawRequest);

// Manually look for the attribute in the raw message
var attribute = rawResponse.AllAttributes().FirstOrDefault(a => a.Name == "my-boolean-attribute");
if (attribute?.Tag == Tag.NoValue)
{
    // This attribute was sent as NoValue
}
```

> [!TIP]
> You can use `NoValue.GetNoValue<T>()` to programmatically retrieve the special value for a given type `T`.
