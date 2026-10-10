# Get-System-Attributes Example

Queries system-level status and description attributes from an IPP System Service.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.2.6**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new GetSystemAttributesRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system"),
        RequestedAttributes = [""system-name"", ""system-state"", ""system-printers""]
    }
};

var response = await client.GetSystemAttributesAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

