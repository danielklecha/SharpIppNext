# Hold-Job Example

Here is a basic example of how to initialize a `HoldJobRequest` and hold a print job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the hold job request with required operation attributes
var request = new HoldJobRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123
    }
};

// Send the hold job request and await the response 
var response = await client.HoldJobAsync(request);

Console.WriteLine("Job held successfully!");
```
