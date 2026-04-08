# Allocate-Printer-Resources Example

```csharp
var client = new SharpIppClient();
var request = new AllocatePrinterResourcesRequest();
var response = await client.AllocatePrinterResourcesAsync(request);
```
