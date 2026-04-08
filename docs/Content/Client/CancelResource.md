# Cancel-Resource Example

```csharp
var client = new SharpIppClient();
var request = new CancelResourceRequest
{
	OperationAttributes = new CancelResourceOperationAttributes
	{
		SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
		RequestingUserName = "admin",
		ResourceId = 1001
	}
};
var response = await client.CancelResourceAsync(request);
```
