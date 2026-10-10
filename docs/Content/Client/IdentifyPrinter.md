# Identify-Printer Example

Commands the printer to identify itself physically (e.g., flashing an LED, beeping, or displaying a message).

> [!NOTE]
> Specification Reference: **PWG 5100.13 Section 5.1**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new IdentifyPrinterRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        IdentifyActions = [IdentifyAction.Display, IdentifyAction.Sound]
    }
};

var response = await client.IdentifyPrinterAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

