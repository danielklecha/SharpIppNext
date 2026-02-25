# Send-Document Example

Here is a basic example of how to initialize a `SendDocumentRequest` and add a document to a multi-document job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Open the document stream
using var stream = File.Open(@"C:\example.pdf", FileMode.Open);

// Initialize the send document request with required operation attributes
var request = new SendDocumentRequest
{
    Document = stream,
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123,
        LastDocument = true,
        DocumentFormat = "application/octet-stream"
    }
};

// Send the request and await the response 
var response = await client.SendDocumentAsync(request);

Console.WriteLine("Document sent successfully!");
```
