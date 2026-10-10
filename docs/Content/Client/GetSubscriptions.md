# Get-Subscriptions Example

Retrieves active event notification subscriptions from the target printer.

> [!NOTE]
> Specification Reference: **RFC 3995 Section 5.5**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new GetSubscriptionsRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
};

var response = await client.GetSubscriptionsAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

