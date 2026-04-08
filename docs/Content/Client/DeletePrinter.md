# Delete-Printer Example

```csharp
var client = new SharpIppClient();
var request = new DeletePrinterRequest();
var response = await client.DeletePrinterAsync(request);
```
