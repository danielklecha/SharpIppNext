# Print-URI Example

> [!WARNING]
> **Deprecated operation:** The Print-URI operation is deprecated across modern standards (e.g., PWG 5100.18-2025). Sending raw document data directly via Print-Job is preferred.

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
        // Optional: reference pre-installed resources.
        ResourceIds = [101, 102],
        DocumentUri = new Uri("http://example.com/example.pdf"),
        DocumentFormat = "application/pdf"
    }
};

// Send the request and await the response 
var response = await client.PrintUriAsync(request);

Console.WriteLine($"Print job submitted successfully! Job ID: {response.JobAttributes.JobId}");
```
