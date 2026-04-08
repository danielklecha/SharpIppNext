# Shutdown-One-Printer Example

```csharp
var client = new SharpIppClient();
var request = new ShutdownOnePrinterRequest();
var response = await client.ShutdownOnePrinterAsync(request);
```
