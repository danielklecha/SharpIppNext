# Cancel-My-Jobs Example

Here is a basic example of how to initialize a `CancelMyJobsRequest` and cancel multiple jobs associated with the requesting user using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

var request = new CancelMyJobsRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobIds = new[] { 123, 456 },
        Message = "Canceling my jobs"
    }
};

var response = await client.CancelMyJobsAsync(request);

Console.WriteLine("Jobs canceled successfully!");
```
