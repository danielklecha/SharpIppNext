# Print-URI Example

Here is a basic example of how to initialize a `PrintUriRequest` and print a document from a URI using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the print URI request with required operation attributes
var request = new PrintUriRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        DocumentUri = new Uri("http://example.com/example.pdf"),
        DocumentFormat = "application/pdf"
    }
};

// Send the request and await the response 
var response = await client.PrintUriAsync(request);

Console.WriteLine($"Print job submitted successfully! Job ID: {response.JobAttributes.JobId}");
```
