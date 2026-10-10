# Fetch-Document Example

Fetches a document data stream from a print service for output processing.

> [!NOTE]
> Specification Reference: **PWG 5100.18 Section 6.5**

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new FetchDocumentRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 42,
        DocumentNumber = 1
    }
};

var response = await client.FetchDocumentAsync(request);
Console.WriteLine($"Status: {response.StatusCode}");

if (response.Document != null)
{
    // Consume or save the fetched document stream
    using var fileStream = File.Create("downloaded_doc.pdf");
    await response.Document.CopyToAsync(fileStream);
}
```

