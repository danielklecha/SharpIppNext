# Get-Job-Attributes Example

Here is a basic example of how to initialize a `GetJobAttributesRequest` and retrieve the attributes of a specific print job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the get job attributes request with required operation attributes
var request = new GetJobAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123
    }
};

// Send the request and await the response 
var response = await client.GetJobAttributesAsync(request);

Console.WriteLine($"Job State: {response.JobAttributes.JobState}");
```
