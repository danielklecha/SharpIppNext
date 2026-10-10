# Get-Output-Device-Attributes Example

Queries the attributes of an output device registered with the IPP Printer.

> [!NOTE]
> Specification Reference: **PWG 5100.18 Section 6.7**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new GetOutputDeviceAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        OutputDeviceUuid = new Uri("urn:uuid:12345678-1234-1234-1234-123456789abc")
    }
};

var response = await client.GetOutputDeviceAttributesAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

