# Get-User-Printer-Attributes Example

Queries printer attributes personalized for the authenticated user.

> [!NOTE]
> Specification Reference: **PWG 5100.13**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new GetUserPrinterAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
};

var response = await client.GetUserPrinterAttributesAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");
```

