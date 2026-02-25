# Pause-Printer Example

Here is a basic example of how to initialize a `PausePrinterRequest` and stop a printer from scheduling jobs using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the pause printer request with required operation attributes
var request = new PausePrinterRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
};

// Send the request and await the response 
var response = await client.PausePrinterAsync(request);

Console.WriteLine("Printer paused successfully!");
```
