# Restart-One-Printer Example

```csharp
var client = new SharpIppClient();
var request = new RestartOnePrinterRequest();
var response = await client.RestartOnePrinterAsync(request);
```
