# Enable-All-Printers Example

```csharp
var client = new SharpIppClient();
var request = new EnableAllPrintersRequest();
var response = await client.EnableAllPrintersAsync(request);
```
