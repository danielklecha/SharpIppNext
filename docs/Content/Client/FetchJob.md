# Fetch-Job Example

Fetches the attributes and status of a print job for output processing.

> [!NOTE]
> Specification Reference: **PWG 5100.18 Section 6.6**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new FetchJobRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 42
    }
};

var response = await client.FetchJobAsync(request);
Console.WriteLine($"Status: {response.StatusCode}, JobId: {response.JobAttributes?.JobId}");
```

