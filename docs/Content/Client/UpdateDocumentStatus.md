# Update-Document-Status Example

Updates the status of a specific document on an output device.

> [!NOTE]
> Specification Reference: **PWG 5100.18 Section 6.9**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new UpdateDocumentStatusRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 42,
        DocumentNumber = 1
    }
};

var response = await client.UpdateDocumentStatusAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

