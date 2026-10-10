# Get-Resource-Attributes Example

Queries the attributes and metadata of an installed IPP resource.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.4.2**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new GetResourceAttributesRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system"),
        ResourceId = 101
    }
};

var response = await client.GetResourceAttributesAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

