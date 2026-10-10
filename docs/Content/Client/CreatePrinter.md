# Create-Printer Example

Creates a new Printer object on an IPP System Service.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.3.1**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new CreatePrinterRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system"),
        PrinterServiceType = [PrinterServiceType.Print]
    }
};

var response = await client.CreatePrinterAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

