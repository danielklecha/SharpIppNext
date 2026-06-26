# Promote-Job Example

Here is a basic example of how to initialize a `PromoteJobRequest` and send it using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

var request = new PromoteJobRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123
    }
};

var response = await client.PromoteJobAsync(request);
```
