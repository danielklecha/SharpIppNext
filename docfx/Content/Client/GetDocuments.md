# Get-Documents Example

Here is a basic example of how to initialize a `GetDocumentsRequest` and retrieve the list of documents belonging to a specific job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the get documents request with required operation attributes
var request = new GetDocumentsRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123
    }
};

// Send the request and await the response 
var response = await client.GetDocumentsAsync(request);

Console.WriteLine($"Total documents found: {response.Documents.Count}");
foreach (var document in response.Documents)
{
    Console.WriteLine($"Document Number: {document.DocumentNumber}, State: {document.DocumentState}");
}
```
