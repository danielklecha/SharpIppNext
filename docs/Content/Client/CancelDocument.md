# Cancel-Document Example

Here is a basic example of how to initialize a `CancelDocumentRequest` and cancel a specific document within a job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the cancel document request with required operation attributes
var request = new CancelDocumentRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123,
        DocumentNumber = 1
    }
};

// Send the cancel document request and await the response 
var response = await client.CancelDocumentAsync(request);

Console.WriteLine("Document canceled successfully!");
```
