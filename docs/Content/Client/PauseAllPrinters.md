# Pause-All-Printers Example

Pauses job scheduling on all printers managed by the IPP System.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.2.8**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new PauseAllPrintersRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system")
    }
};

var response = await client.PauseAllPrintersAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

