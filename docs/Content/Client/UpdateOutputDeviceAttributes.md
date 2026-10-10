# Update-Output-Device-Attributes Example

Updates status and configuration attributes of a registered output device.

> [!NOTE]
> Specification Reference: **PWG 5100.18 Section 6.11**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new UpdateOutputDeviceAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        OutputDeviceUuid = new Uri("urn:uuid:12345678-1234-1234-1234-123456789abc")
    }
};

var response = await client.UpdateOutputDeviceAttributesAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

