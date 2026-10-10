# Pause-All-Printers-After-Current-Job Example

Pauses job scheduling on all printers once their currently processing jobs finish.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.2.9**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new PauseAllPrintersAfterCurrentJobRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system")
    }
};

var response = await client.PauseAllPrintersAfterCurrentJobAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

