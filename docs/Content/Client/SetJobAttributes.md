# Set-Job-Attributes Example

Modifies attributes of an existing job before or during printing.

> [!NOTE]
> Specification Reference: **RFC 3380 Section 4.1**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new SetJobAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 42
    },
    JobTemplateAttributes = new()
    {
        Copies = 3
    }
};

var response = await client.SetJobAttributesAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

