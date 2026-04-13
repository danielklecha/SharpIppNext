# Acknowledge-Identify-Printer Example

```csharp
var client = new SharpIppClient();
var request = new AcknowledgeIdentifyPrinterRequest();
var response = await client.AcknowledgeIdentifyPrinterAsync(request);
```
