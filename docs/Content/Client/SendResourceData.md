# Send-Resource-Data Example

```csharp
var client = new SharpIppClient();
var request = new SendResourceDataRequest
{
	OperationAttributes = new SendResourceDataOperationAttributes
	{
		SystemUri = new Uri("ipp://127.0.0.1:8631/system"),
		RequestingUserName = "admin",
		ResourceId = 1001,
		ResourceKOctets = 256
	}
};
var response = await client.SendResourceDataAsync(request);
```
