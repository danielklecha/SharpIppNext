# Add-Document-Images Example

Adds pre-processed document images to an existing document in a job.

> [!NOTE]
> Specification Reference: **PWG 5100.5**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new AddDocumentImagesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 42,
        DocumentNumber = 1
    }
};

var response = await client.AddDocumentImagesAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

