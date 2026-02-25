# Release-Job Example

Here is a basic example of how to initialize a `ReleaseJobRequest` and release a previously held print job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the release job request with required operation attributes
var request = new ReleaseJobRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123
    }
};

// Send the release job request and await the response 
var response = await client.ReleaseJobAsync(request);

Console.WriteLine("Job released successfully!");
```
