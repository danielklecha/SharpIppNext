# Update-Active-Jobs Example

Informs the print service of the active job queue and states on an output device.

> [!NOTE]
> Specification Reference: **PWG 5100.18 Section 6.8**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new UpdateActiveJobsRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobIds = [42, 43]
    }
};

var response = await client.UpdateActiveJobsAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

