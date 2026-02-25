# Resume-Printer Example

Here is a basic example of how to initialize a `ResumePrinterRequest` and resume a paused printer using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the resume printer request with required operation attributes
var request = new ResumePrinterRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
};

// Send the request and await the response 
var response = await client.ResumePrinterAsync(request);

Console.WriteLine("Printer resumed successfully!");
```
