# Create-Resource Example

```csharp
var client = new SharpIppClient();
var request = new CreateResourceRequest
{
	OperationAttributes = new CreateResourceOperationAttributes
	{
		SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
		RequestingUserName = "admin",
		ResourceType = "template",
		ResourceFormat = "application/postscript",
		ResourceName = "corporate-template"
	}
};
var response = await client.CreateResourceAsync(request);
```
