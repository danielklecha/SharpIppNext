# Delete-Printer Example

Deletes a Printer object from an IPP System Service.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.3.5**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new DeletePrinterRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system"),
        PrinterId = 1
    }
};

var response = await client.DeletePrinterAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

