# Close-Job Example

Here is a basic example of how to initialize a `CloseJobRequest` and close a multi-document job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

var request = new CloseJobRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123
    }
};

var response = await client.CloseJobAsync(request);

Console.WriteLine("Job closed successfully!");
```
