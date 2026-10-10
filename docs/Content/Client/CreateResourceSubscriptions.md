# Create-Resource-Subscriptions Example

Creates subscriptions for events relating to IPP resources.

> [!NOTE]
> Specification Reference: **PWG 5100.22-2025 Section 6.5.1**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new CreateResourceSubscriptionsRequest
{
    OperationAttributes = new()
    {
        SystemUri = new Uri("ipp://localhost:631/system"),
        NotifyResourceId = 101
    }
};

var response = await client.CreateResourceSubscriptionsAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

