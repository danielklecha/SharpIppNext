# Acknowledge-Document Example

```csharp
var client = new SharpIppClient();
var request = new AcknowledgeDocumentRequest
{
    OperationAttributes = new AcknowledgeDocumentOperationAttributes
    {
        PrinterUri = new Uri("ipp://127.0.0.1/printers/printer1"),
        RequestingUserName = "user",
        DocumentNumber = 2,
        OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174001"),
        FetchStatusCode = IppStatusCode.ClientErrorDocumentFormatError,
        FetchStatusMessage = "Unsupported document format"
    }
};
var response = await client.AcknowledgeDocumentAsync(request);
```
