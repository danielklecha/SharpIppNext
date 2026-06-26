# Create-Printer-Subscriptions Example

Here is a basic example of how to initialize a `CreatePrinterSubscriptionsRequest` and send it using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

var request = new CreatePrinterSubscriptionsRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
};

var response = await client.CreatePrinterSubscriptionsAsync(request);
```
