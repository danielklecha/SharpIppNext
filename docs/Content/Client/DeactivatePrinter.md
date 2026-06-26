# Deactivate-Printer Example

Here is a basic example of how to initialize a `DeactivatePrinterRequest` and send it using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

var request = new DeactivatePrinterRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
};

var response = await client.DeactivatePrinterAsync(request);
```
