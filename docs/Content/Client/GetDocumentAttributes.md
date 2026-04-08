# Get-Document-Attributes Example

Here is a basic example of how to initialize a `GetDocumentAttributesRequest` and retrieve the attributes of a specific document within a job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the get document attributes request with required operation attributes
var request = new GetDocumentAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123,
        DocumentNumber = 1
    }
};

// Send the request and await the response 
var response = await client.GetDocumentAttributesAsync(request);

Console.WriteLine($"Document Number: {response.DocumentAttributes.DocumentNumber}");
Console.WriteLine($"Document State: {response.DocumentAttributes.DocumentState}");
```
