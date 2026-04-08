# Get-Printers Example

```csharp
var client = new SharpIppClient();
var request = new GetPrintersRequest();
var response = await client.GetPrintersAsync(request);
```
