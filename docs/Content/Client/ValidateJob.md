# Validate-Job Example

Here is a basic example of how to initialize a `ValidateJobRequest` and verify capabilities of a printer object against supplied attributes using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the validate job request with required operation attributes
var request = new ValidateJobRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        DocumentFormat = "application/pdf"
    }
};

// Send the request and await the response 
var response = await client.ValidateJobAsync(request);

Console.WriteLine("Job validated successfully!");
```
