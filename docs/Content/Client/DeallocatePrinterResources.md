# Deallocate-Printer-Resources Example

Deallocates resources previously assigned to a printer.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.3.3**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new DeallocatePrinterResourcesRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system"),
        PrinterId = 1,
        ResourceIds = [101]
    }
};

var response = await client.DeallocatePrinterResourcesAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

