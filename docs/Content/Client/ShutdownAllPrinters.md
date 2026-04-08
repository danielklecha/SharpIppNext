# Shutdown-All-Printers Example

```csharp
var client = new SharpIppClient();
var request = new ShutdownAllPrintersRequest();
var response = await client.ShutdownAllPrintersAsync(request);
```
