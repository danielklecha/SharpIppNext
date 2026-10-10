# Get-Printers Example

Retrieves the list of Printer objects hosted on an IPP System Service.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.2.5**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new GetPrintersRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system"),
        WhichPrinters = WhichPrinters.All
    }
};

var response = await client.GetPrintersAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

