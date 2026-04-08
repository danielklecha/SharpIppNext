# Set-Printer-Attributes Example

This example shows how to update printer description attributes using `SetPrinterAttributesAsync`.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

var request = new SetPrinterAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        AttributesCharset = "utf-8",
        AttributesNaturalLanguage = "en",
        RequestingUserName = "alice"
    },
    PrinterAttributes = new PrinterDescriptionAttributes
    {
        PrinterInfo = "Main floor – color"
    }
};

var response = await client.SetPrinterAttributesAsync(request);
```

Notes:
- Supply only settable printer attributes; immutable attributes will be rejected by compliant implementations.
- The server decides which attributes were applied and may return `successful-ok-ignored-or-substituted-attributes` or `successful-ok-conflicting-attributes` when appropriate.
