# Validate-Document

The Validate-Document operation validates operation and Document Template attributes that would be used with later Send-Document or Send-URI calls, without creating a Document object.

See: PWG 5100.13-2023 Section 5.2.

## Example

```csharp
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

var request = new ValidateDocumentRequest
{
    Version = new IppVersion(2, 0),
    RequestId = 1,
    OperationAttributes = new ValidateDocumentOperationAttributes
    {
        PrinterUri = new Uri("http://127.0.0.1:631"),
        RequestingUserName = "user",
        RequestingUserUri = new Uri("mailto:user@example.com"),
        DocumentName = "report.pdf",
        DocumentFormat = "application/pdf",
        DocumentMetadata = ["meta-a", "meta-b"]
    },
    DocumentTemplateAttributes = new DocumentTemplateAttributes
    {
        Copies = 1,
        PrintQuality = PrintQuality.High
    }
};

var response = await client.ValidateDocumentAsync(request);
```

## Notes

- Per PWG 5100.13, clients MUST NOT send `document-password` in Validate-Document.
- If provided, the server rejects the request with `client-error-bad-request`.
