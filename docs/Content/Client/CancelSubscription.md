# Cancel-Subscription Example

Cancels an existing event notification subscription.

> [!NOTE]
> Specification Reference: **RFC 3995 Section 5.3**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new CancelSubscriptionRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        NotifySubscriptionId = 10
    }
};

var response = await client.CancelSubscriptionAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

