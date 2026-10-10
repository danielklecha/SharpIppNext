# Get-Printer-Resources Example

Queries resources associated with or supported by a specific printer.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.3.7**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new GetPrinterResourcesRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system"),
        PrinterId = 1
    }
};

var response = await client.GetPrinterResourcesAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

