# Get-Notifications Example

Retrieves pending event notifications for active subscriptions using the IPP-GET method.

> [!NOTE]
> Specification Reference: **RFC 3996 Section 5.1**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new GetNotificationsRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        NotifySubscriptionIds = [10],
        NotifyPullMethod = NotifyPullMethod.IppGet
    }
};

var response = await client.GetNotificationsAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

