# Deallocate-Printer-Resources Example

```csharp
var client = new SharpIppClient();
var request = new DeallocatePrinterResourcesRequest();
var response = await client.DeallocatePrinterResourcesAsync(request);
```
