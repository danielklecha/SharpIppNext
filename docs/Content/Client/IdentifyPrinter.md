# Identify-Printer Example

```csharp
var client = new SharpIppClient();
var request = new IdentifyPrinterRequest();
var response = await client.IdentifyPrinterAsync(request);
```
