# Set-Resource-Attributes Example

```csharp
var client = new SharpIppClient();
var request = new SetResourceAttributesRequest
{
	OperationAttributes = new SetResourceAttributesOperationAttributes
	{
		SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
		RequestingUserName = "admin",
		ResourceId = 1001,
		ResourceInfo = "Updated metadata"
	}
};
var response = await client.SetResourceAttributesAsync(request);
```
