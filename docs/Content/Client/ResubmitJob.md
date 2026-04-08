# Resubmit-Job Example

Here is a basic example of how to initialize a `ResubmitJobRequest` and resubmit an existing job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

var request = new ResubmitJobRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123,
        IppAttributeFidelity = true,
        JobMandatoryAttributes = new[] { "copies", "media" }
    },
    JobTemplateAttributes = new()
    {
        Copies = 2
    }
};

var response = await client.ResubmitJobAsync(request);

Console.WriteLine("Job resubmitted successfully!");
```
