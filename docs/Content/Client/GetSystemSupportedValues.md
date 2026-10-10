# Get-System-Supported-Values Example

Queries the supported attribute values and ranges supported by an IPP System.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.2.7**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new GetSystemSupportedValuesRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system")
    }
};

var response = await client.GetSystemSupportedValuesAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

