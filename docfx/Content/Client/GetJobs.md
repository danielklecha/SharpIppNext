# Get-Jobs Example

Here is a basic example of how to initialize a `GetJobsRequest` and retrieve the list of print jobs using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the get jobs request with required operation attributes
var request = new GetJobsRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
};

// Send the request and await the response 
var response = await client.GetJobsAsync(request);

Console.WriteLine($"Total jobs found: {response.Jobs.Length}");
foreach (var job in response.Jobs)
{
    Console.WriteLine($"Job ID: {job.JobId}, State: {job.JobState}");
}
```
