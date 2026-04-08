# Restart-System Example

```csharp
var client = new SharpIppClient();
var request = new RestartSystemRequest();
var response = await client.RestartSystemAsync(request);
```
