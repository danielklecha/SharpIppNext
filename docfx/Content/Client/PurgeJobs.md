# Purge-Jobs Example

Here is a basic example of how to initialize a `PurgeJobsRequest` and purge all jobs from a printer using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the purge jobs request with required operation attributes
var request = new PurgeJobsRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
};

// Send the request and await the response 
var response = await client.PurgeJobsAsync(request);

Console.WriteLine("Jobs purged successfully!");
```
