# Enable-All-Printers Example

Enables job submission on all printers managed by the IPP System.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.2.4**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new EnableAllPrintersRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system")
    }
};

var response = await client.EnableAllPrintersAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

