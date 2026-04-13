# Get-Next-Document-Data Example

```csharp
var client = new SharpIppClient();
var request = new GetNextDocumentDataRequest();
var response = await client.GetNextDocumentDataAsync(request);

if (response.OperationAttributes != null)
{
	var compression = response.OperationAttributes.Compression;
	var retryAfterSeconds = response.OperationAttributes.DocumentDataGetInterval;
	var isLastDocument = response.OperationAttributes.LastDocument;
	var documentNumber = response.OperationAttributes.DocumentNumber;
}
```
