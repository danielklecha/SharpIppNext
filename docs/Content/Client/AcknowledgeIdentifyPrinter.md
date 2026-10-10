# Acknowledge-Identify-Printer Example

Acknowledges that an identify-printer request was received and acted upon by the output device.

> [!NOTE]
> Specification Reference: **PWG 5100.18 Section 6.2**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new AcknowledgeIdentifyPrinterRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
};

var response = await client.AcknowledgeIdentifyPrinterAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

