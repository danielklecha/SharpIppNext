# Send-URI Example

Here is a basic example of how to initialize a `SendUriRequest` and add a document (via URI reference) to a multi-document job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the send URI request with required operation attributes
var request = new SendUriRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123,
        DocumentUri = new Uri("http://example.com/example.pdf"),
        LastDocument = true,
        DocumentFormat = "application/pdf"
    }
};

// Send the request and await the response 
var response = await client.SendUriAsync(request);

Console.WriteLine("Document URI sent successfully!");
```
