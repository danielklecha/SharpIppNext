# Get-Resources Example

Lists the resources installed or managed by the IPP System.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.4.1**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new GetResourcesRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system")
    }
};

var response = await client.GetResourcesAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

