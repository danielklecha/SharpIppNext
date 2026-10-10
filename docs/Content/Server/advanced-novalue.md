# Advanced NoValue in Server

In IPP requests and responses, the `no-value` out-of-band tag (`Tag.NoValue` / `0x13`) indicates that an attribute is supported but currently has no assigned value. SharpIppNext natively handles `Tag.NoValue` across all server models using `IppValue<T>`, `StringWithLanguage`, and `OctetString`.

## Server-Side Receiving: Inspecting `NoValue` in Requests

When SharpIppNext deserializes an IPP attribute sent with `Tag.NoValue`, it sets the corresponding model property to an instance where `IsValue == false`.

```csharp
var request = (GetJobsRequest)await sharpIppServer.ReceiveRequestAsync(stream);

// Check if the client explicitly sent Tag.NoValue for an integer attribute
if (request.OperationAttributes.Limit == NoValue.Instance)
{
    Console.WriteLine("Client sent a NoValue tag for limit.");
}

// Check if the client sent Tag.NoValue for a StringWithLanguage attribute
if (request.OperationAttributes.DocumentName == NoValue.Instance)
{
    Console.WriteLine("Client sent a NoValue tag for document-name.");
}

// Or check via pattern matching on IsValue
if (request.OperationAttributes.MyJobs is { IsValue: false })
{
    Console.WriteLine("Client sent NoValue for my-jobs.");
}
```

## Server-Side Responding: Returning `NoValue` in Responses

To return `Tag.NoValue` for any attribute in a server response, assign `NoValue.Instance` to the response model property:

```csharp
public Task<GetPrinterAttributesResponse> GetPrinterAttributesAsync(GetPrinterAttributesRequest request)
{
    return Task.FromResult(new GetPrinterAttributesResponse
    {
        StatusCode = IppStatusCode.SuccessfulOk,
        PrinterAttributes = new PrinterDescriptionAttributes
        {
            PrinterState = PrinterState.Idle,

            // Return NoValue for an integer attribute (encoded as Tag.NoValue)
            QueuedJobCount = NoValue.Instance,

            // Return NoValue for a boolean attribute (encoded as Tag.NoValue)
            ColorSupported = NoValue.Instance,

            // Return NoValue for a StringWithLanguage attribute (encoded as Tag.NoValue)
            PrinterInfo = NoValue.Instance,

            // Return NoValue for an array attribute (encoded as Tag.NoValue)
            PrinterStateReasons = NoValue.Instance
        }
    });
}
```

## Attribute Tri-State Reference

Server handlers can distinguish between an attribute that was omitted and one that was sent as `NoValue`:

| Property State | Wire IPP Tag | Server Behavior |
| :--- | :--- | :--- |
| `null` | *(Omitted)* | Omit attribute from the response / client omitted the attribute. |
| `NoValue.Instance` (`IsValue == false`) | `Tag.NoValue` (`0x13`) | Attribute is supported but currently has no value. |
| Concrete value (`IsValue == true`) | Standard type tag (`Tag.Integer`, `Tag.Boolean`, `Tag.TextWithoutLanguage`, `Tag.TextWithLanguage`, etc.) | Normal attribute value. |
