# Set-Document-Attributes Example

Here is a basic example of how to initialize a `SetDocumentAttributesRequest` and set the attributes of a specific document using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the set document attributes request with required operation attributes
var request = new SetDocumentAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123,
        DocumentNumber = 1
    },
    DocumentTemplateAttributes = new()
    {
        Copies = 5
    }
};

// Send the request and await the response 
var response = await client.SetDocumentAttributesAsync(request);

Console.WriteLine("Document attributes set successfully!");
```
