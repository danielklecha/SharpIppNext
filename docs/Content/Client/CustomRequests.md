# Advanced Custom Requests

SharpIppNext handles the serialization and deserialization of strongly-typed models into raw `IIppRequestMessage` and `IIppResponseMessage` structures out of the box. However, if you need to:
- Add a custom or vendor-specific `IppAttribute` that is not part of the standard IPP specification.
- Perform an operation that is not yet modeled.
- Inspect or adjust the raw message before it is sent.

You can bypass the high-level `ISharpIppClient.<Operation>Async` methods by using the generic `CreateRawRequest<T>` and `SendAsync` methods.

## Example: Adding a Custom Attribute

Here is an example demonstrating how to start with a standard `GetJobsRequest`, convert it to a raw request, inject custom vendor attributes, and then send the message:

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
//    (You can also add attributes to JobAttributes or other sections depending on your needs)
rawRequest.OperationAttributes.Add(new IppAttribute(
    Tag.NameWithoutLanguage,
    "vendor-custom-attribute",
    "custom-value"));

rawRequest.OperationAttributes.Add(new IppAttribute(
    Tag.TextWithLanguage,
    "vendor-custom-localized-note",
    new StringWithLanguage("en-US", "custom-text")));

// 4. Send the raw request and get the raw response
IIppResponseMessage rawResponse = await client.SendAsync(printerUri, rawRequest);

// 5. (Optional) Read the standard response
var getJobsResponse = client.CreateResponse<GetJobsResponse>(rawResponse);

// You can now inspect getJobsResponse or rawResponse
Console.WriteLine($"Jobs returned: {getJobsResponse.Jobs?.Length ?? 0}");
```

> [!TIP]
> The attribute collections (`OperationAttributes`, `JobAttributes`, etc.) on `IIppRequestMessage` are standard mutable lists (`IList<IppAttribute>`). If a legacy printer quirk ever requires removing or modifying an auto-generated attribute before transmission, you can inspect and manipulate the list in place prior to calling `SendAsync`.

### Explanation

1. **`CreateRawRequest<T>`**: Maps your strongly-typed request model (e.g., `GetJobsRequest`) into the underlying `IIppRequestMessage` using the internal mapper.
2. **`IppAttribute`**: Represents a wire-level IPP attribute containing the attribute `Tag` (data type as per the IPP specification), `Name`, and `Value`.
3. **`SendAsync`**: The raw transport method on `ISharpIppClient` that sends an `IIppRequestMessage` directly to the designated printer URI and returns the raw `IIppResponseMessage`.
4. **`CreateResponse<T>`**: Deserializes the raw response back into the expected, strongly-typed response model.
