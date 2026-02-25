# Create-Job Example

Here is a basic example of how to initialize a `CreateJobRequest` and create a print job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the create job request with required operation attributes
var request = new CreateJobRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
};

// Send the create job request and await the response 
var response = await client.CreateJobAsync(request);

Console.WriteLine($"Job created successfully! Job ID: {response.JobAttributes.JobId}");
```
