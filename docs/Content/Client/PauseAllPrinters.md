# Pause-All-Printers Example

```csharp
var client = new SharpIppClient();
var request = new PauseAllPrintersRequest();
var response = await client.PauseAllPrintersAsync(request);
```
