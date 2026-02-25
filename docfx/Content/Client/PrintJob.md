# Print-Job Example

Here is a basic example of how to initialize a `PrintJobRequest` and send a document using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Open the document stream
using var stream = File.Open(@"C:\example.pdf", FileMode.Open);

// Initialize the print job request with required operation attributes
var request = new PrintJobRequest
{
    Document = stream,
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        DocumentFormat = "application/octet-stream"
    }
};

// Send the print job and await the response 
var response = await client.PrintJobAsync(request);

Console.WriteLine($"Print job submitted successfully! Job ID: {response.JobAttributes.JobId}");
```
