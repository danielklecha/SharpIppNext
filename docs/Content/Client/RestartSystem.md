# Restart-System Example

Restarts the entire IPP System Service.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.2.10**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new RestartSystemRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system")
    }
};

var response = await client.RestartSystemAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

