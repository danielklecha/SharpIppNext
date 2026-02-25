# Advanced Custom Requests

SharpIppNext handles the serialization and deserialization of strongly-typed models into the raw `IIppRequestMessage` and `IIppResponseMessage` structures out of the box. However, if you need to:
- Add a custom `IppAttribute` that is not available on the strongly-typed request models.
- Perform an operation that is not yet fully modeled.
- Inspect the raw message before it is sent.

You can bypass the standard `ISharpIppClient.<Operation>Async` methods by using the generic `SendAsync` and `CreateRawRequest<T>` methods.

## Example: Adding a Custom Attribute

Here is an example demonstrating how to start with a standard `GetJobsRequest`, convert it to a raw request, inject a custom attribute, and then send the message.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();
var printerUri = new Uri("ipp://localhost:631/printers/my-printer");

// 1. Initialize a strongly-typed request
var getJobsRequest = new GetJobsRequest
{
    PrinterUri = printerUri,
    Limit = 10
};

// 2. Create the raw request message
IIppRequestMessage rawRequest = client.CreateRawRequest(getJobsRequest);

// 3. Add custom attributes to the Operation Attributes
//    (You can also add attributes to JobAttributes depending on your needs)
rawRequest.OperationAttributes.Add(new IppAttribute
{
    Name = "my-custom-attribute",
    Tag = IppTag.NameWithoutLanguage,
    Value = "custom-value"
});

rawRequest.OperationAttributes.Add(new IppAttribute
{
    Name = "my-second-attribute",
    Tag = IppTag.TextWithLanguage,
    Value = new StringWithLanguage("en-US", "custom-text")
});

// 4. Send the raw request and get the raw response
IIppResponseMessage rawResponse = await client.SendAsync(printerUri, rawRequest);

// 5. (Optional) Read the standard response
var getJobsResponse = client.CreateResponse<GetJobsResponse>(rawResponse);

// You can now inspect getJobsResponse or rawResponse
Console.WriteLine($"Jobs returned: {getJobsResponse.Jobs.Length}");
```

### Explanation

1. **`CreateRawRequest<T>`**: Takes your strongly-typed request model (e.g., `GetJobsRequest`) and maps it into the underlying `IIppRequestMessage` using the internal mapping engine.
2. **`IppAttribute`**: This class allows you to manually specify the IPP attribute `Name`, its `Tag` (data type as per the IPP specification), and the string or object `Value`.
3. **`SendAsync`**: The raw fallback method on `ISharpIppClient` that accepts an arbitrary `IIppRequestMessage` and sends it directly to the designated printer URI, returning the unparsed `IIppResponseMessage`.
4. **`CreateResponse<T>`**: Maps the result back into the expected, strongly-typed response model if applicable.
