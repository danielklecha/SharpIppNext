# Update-Job-Status Example

Updates the status of a print job currently processing on an output device.

> [!NOTE]
> Specification Reference: **PWG 5100.18 Section 6.10**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new UpdateJobStatusRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 42
    }
};

var response = await client.UpdateJobStatusAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

