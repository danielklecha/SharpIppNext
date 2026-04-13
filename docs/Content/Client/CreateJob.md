# Create-Job Example

Here is a basic example of how to initialize a `CreateJobRequest` and create a print job using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

// Initialize the create job request with required operation attributes
var request = new CreateJobRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        // Optional: reference pre-installed resources.
        ResourceIds = [101, 102],
        // Optional for Scan: destination URI(s).
        DestinationUris =
        [
            new DestinationUri { DestinationUriValue = "https://example.test/upload" }
        ],
        // Optional for Scan: destination access credentials matching destination-uris cardinality.
        DestinationAccesses =
        [
            new DocumentAccess
            {
                AccessUserName = "scan-user",
                AccessPassword = "secret"
            }
        ],
        // Optional for Scan: output image processing controls.
        OutputAttributes = new OutputAttributes
        {
            NoiseRemoval = 50,
            OutputCompressionQualityFactor = 80
        }
    }
};

// Send the create job request and await the response 
var response = await client.CreateJobAsync(request);

Console.WriteLine($"Job created successfully! Job ID: {response.JobAttributes.JobId}");
```
