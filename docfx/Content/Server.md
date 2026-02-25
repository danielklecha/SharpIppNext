# Server

Here is an example demonstrating how to handle operations on the server side:

```csharp
public async Task MapAsync(HttpContext context)
{
    var inputStream = context.Request.Body;
    var outputStream = context.Response.Body;

    IIppRequest request = await sharpIppServer.ReceiveRequestAsync(inputStream);
    
    IIppResponse response = request switch
    {
        // Handle all operations
        CancelJobRequest x => await GetCancelJobResponseAsync(x),
        _ => throw new NotImplementedException()
    };
    
    IIppResponseMessage rawResponse = await sharpIppServer.CreateRawResponseAsync(response);
    ImproveRawResponse(request, rawResponse);
    await sharpIppServer.SendRawResponseAsync(rawResponse, outputStream);
}

private async Task<CancelJobResponse> GetCancelJobResponseAsync(CancelJobRequest request)
{
    // Implementation
    throw new NotImplementedException();
}
```
