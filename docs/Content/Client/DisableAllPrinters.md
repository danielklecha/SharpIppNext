# Disable-All-Printers Example

```csharp
var client = new SharpIppClient();
var request = new DisableAllPrintersRequest();
var response = await client.DisableAllPrintersAsync(request);
```
