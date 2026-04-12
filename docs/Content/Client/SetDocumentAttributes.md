# Set-Document-Attributes Example

Here is a basic example of how to initialize a `SetDocumentAttributesRequest` and set the attributes of a specific document using `SharpIppClient`. Optional attributes have been omitted for clarity.

Per PWG 5100.5 Section 5.1.3, you can supply Document Description attributes (for example `document-name`) and/or Document Template attributes.

At least one Document attribute must be present in the request.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

// Initialize the set document attributes request with required operation attributes
var request = new SetDocumentAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123,
        DocumentNumber = 1
    },
    DocumentName = "Quarterly Report",
    DocumentTemplateAttributes = new()
    {
        Copies = 5,
        PageOrderReceived = PageOrderReceived.OneToNOrder
    }
};

// Send the request and await the response 
var response = await client.SetDocumentAttributesAsync(request);

Console.WriteLine("Document attributes set successfully!");
```
