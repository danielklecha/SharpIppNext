# Acknowledge-Job Example

```csharp
var client = new SharpIppClient();
var request = new AcknowledgeJobRequest
{
    OperationAttributes = new AcknowledgeJobOperationAttributes
    {
        PrinterUri = new Uri("ipp://127.0.0.1/printers/printer1"),
        RequestingUserName = "user",
        OutputDeviceUuid = new Uri("urn:uuid:123e4567-e89b-12d3-a456-426614174001"),
        OutputDeviceJobStates = new[] { JobState.Processing },
        FetchStatusCode = IppStatusCode.ClientErrorDocumentAccessError,
        FetchStatusMessage = "Unable to fetch job data"
    }
};
var response = await client.AcknowledgeJobAsync(request);
```
