# Delete-Document Example

> [!WARNING]
> **Obsolete operation:** The Delete-Document operation is obsolete. See PWG 5100.5-2024 and PWG 5100.18-2025. It destroys accounting information and should not be used by clients.

Here is a basic example of how to initialize a `DeleteDocumentRequest` and delete a document from a job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the delete document request with required operation attributes
var request = new DeleteDocumentRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123,
        DocumentNumber = 1
    }
};

// Send the request and await the response 
var response = await client.DeleteDocumentAsync(request);

Console.WriteLine("Document deleted successfully!");
```
