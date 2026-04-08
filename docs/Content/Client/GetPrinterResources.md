# Get-Printer-Resources Example

```csharp
var client = new SharpIppClient();
var request = new GetPrinterResourcesRequest();
var response = await client.GetPrinterResourcesAsync(request);
```
