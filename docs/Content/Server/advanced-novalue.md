# Advanced NoValue in Server

In IPP requests and responses, the `no-value` out-of-band tag (0x13) indicates that an attribute is supported but currently has no value. SharpIppNext automatically maps these tags into standard C# types in your models.

## Automatic NoValue Mapping

When SharpIppNext receives an attribute with the `Tag.NoValue` tag, it maps it to the same "strict" special values used in requests. This allows you to check for "no value" by comparing the property to its type's special value (e.g., `int.MinValue`, `DateTime.MinValue`, etc.).

For a full list of these special values, see the [Advanced NoValue in Client](../Client/advanced-novalue.md) documentation.

### Example: Checking for NoValue in a Request

```csharp
// When your server receives a request, SharpIppNext automatically maps
// NoValue tags into the corresponding special values in the request model.
var request = (GetJobsRequest)await sharpIppServer.ReceiveRequestAsync(stream);

if (request.OperationAttributes.Limit == NoValue.GetNoValue<int>())
{
    Console.WriteLine("The client sent a 'no-value' tag for the limit attribute.");
}
```

## The Boolean Exception

Just like in requests, `bool` properties in response models **cannot** represent a `NoValue` state automatically. IPP boolean attributes are always mapped to either `true` or `false`.

If you are implementing a server and need to return a `NoValue` tag for a boolean attribute, you must manually replace the attribute in the raw `IIppResponseMessage` before sending it:

```csharp
IIppResponseMessage rawResponse = await sharpIppServer.CreateRawResponseAsync(response);

// Manually replace the attribute with NoValue
for (var i = 0; i < rawResponse.PrinterAttributes.Count; i++)
{
    var attribute = rawResponse.PrinterAttributes[i];
    if (attribute.Name == "my-boolean-attribute")
    {
        rawResponse.PrinterAttributes[i] = new IppAttribute(
            Tag.NoValue, 
            attribute.Name, 
            NoValue.Instance);
        break;
    }
}
```

## Server-Side: Returning NoValue

If you are implementing an IPP Server and want to return a `NoValue` tag, simply set the property in your response model to its corresponding special value:

```csharp
public Task<GetPrinterAttributesResponse> GetPrinterAttributesAsync(GetPrinterAttributesRequest request)
{
    return Task.FromResult(new GetPrinterAttributesResponse
    {
        PrinterAttributes = new PrinterDescriptionAttributes
        {
            QueuedJobCount = NoValue.GetNoValue<int>() // This will be sent as Tag.NoValue
        }
    });
}
```
