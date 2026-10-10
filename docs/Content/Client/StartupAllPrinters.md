# Startup-All-Printers Example

Starts up all printer services hosted on the IPP System.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.2.14**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new StartupAllPrintersRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system")
    }
};

var response = await client.StartupAllPrintersAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

