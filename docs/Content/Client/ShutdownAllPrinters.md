# Shutdown-All-Printers Example

Shuts down all printer services hosted on the IPP System.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.2.13**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new ShutdownAllPrintersRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system")
    }
};

var response = await client.ShutdownAllPrintersAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

