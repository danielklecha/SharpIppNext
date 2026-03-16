# Cancel-Jobs Example

Here is a basic example of how to initialize a `CancelJobsRequest` and cancel multiple jobs using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

var request = new CancelJobsRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobIds = new[] { 123, 456 },
        Message = "Canceling jobs from batch"
    }
};

var response = await client.CancelJobsAsync(request);

Console.WriteLine("Jobs canceled successfully!");
```
