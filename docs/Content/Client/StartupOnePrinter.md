# Startup-One-Printer Example

Starts up a single stopped printer service on the IPP System.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.3.9**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new StartupOnePrinterRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system"),
        PrinterId = 1
    }
};

var response = await client.StartupOnePrinterAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

