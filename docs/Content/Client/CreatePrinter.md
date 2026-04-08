# Create-Printer Example

```csharp
var client = new SharpIppClient();
var request = new CreatePrinterRequest();
var response = await client.CreatePrinterAsync(request);
```
