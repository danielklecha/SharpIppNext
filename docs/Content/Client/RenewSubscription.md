# Renew-Subscription Example

Renews the lease of an existing event notification subscription before it expires.

> [!NOTE]
> Specification Reference: **RFC 3995 Section 5.2**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new RenewSubscriptionRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        NotifySubscriptionId = 10,
        NotifyLeaseDuration = 3600
    }
};

var response = await client.RenewSubscriptionAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

