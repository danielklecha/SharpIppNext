# Pause-All-Printers-After-Current-Job Example

```csharp
var client = new SharpIppClient();
var request = new PauseAllPrintersAfterCurrentJobRequest();
var response = await client.PauseAllPrintersAfterCurrentJobAsync(request);
```
