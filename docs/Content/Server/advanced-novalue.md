# Advanced NoValue in Server

In IPP requests and responses, the `no-value` out-of-band tag (`0x13`) indicates that an attribute is supported but currently has no value. SharpIppNext uses `IppValue<T>` to seamlessly handle `Tag.NoValue` on the server side.

## Server-Side Receiving: Inspecting `NoValue` in Requests

When SharpIppNext receives an attribute with `Tag.NoValue`, it sets the corresponding property to an `IppValue<T>` where `IsValue == false`.

```csharp
var request = (GetJobsRequest)await sharpIppServer.ReceiveRequestAsync(stream);

// Check if the client explicitly sent Tag.NoValue
if (request.OperationAttributes.Limit == NoValue.Instance)
{
    Console.WriteLine("Client sent a NoValue tag for limit.");
}

// Or check via property
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
            // Return NoValue for QueuedJobCount (encoded as Tag.NoValue in the IPP response)
            QueuedJobCount = NoValue.Instance,

            // Return NoValue for a boolean attribute (encoded as Tag.NoValue)
            ColorSupported = NoValue.Instance,

            // Return NoValue for an array attribute
            PrinterStateReasons = NoValue.Instance
        }
    });
}
```

## Attribute Tri-State Reference

Server handlers can distinguish between an attribute that was omitted and one that was sent as `NoValue`:

| Property State | Wire IPP Tag | Server Behavior |
| :--- | :--- | :--- |
| `null` | *(Omitted)* | Do not include attribute in response / client omitted attribute. |
| `NoValue.Instance` (`IsValue == false`) | `Tag.NoValue` (`0x13`) | Attribute is supported but has no value. |
| `value` (`IsValue == true`) | Standard type tag (`Tag.Integer`, `Tag.Boolean`, etc.) | Normal attribute value. |

