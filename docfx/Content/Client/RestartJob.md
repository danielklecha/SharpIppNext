# Restart-Job Example

Here is a basic example of how to initialize a `RestartJobRequest` and restart a print job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the restart job request with required operation attributes
var request = new RestartJobRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123
    }
};

// Send the restart job request and await the response 
var response = await client.RestartJobAsync(request);

Console.WriteLine("Job restarted successfully!");
```
