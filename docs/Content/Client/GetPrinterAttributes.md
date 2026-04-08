# Get-Printer-Attributes Example

Here is a basic example of how to initialize a `GetPrinterAttributesRequest` and retrieve the attributes of a printer object using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the get printer attributes request with required operation attributes
var request = new GetPrinterAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
};

// Send the request and await the response 
var response = await client.GetPrinterAttributesAsync(request);

Console.WriteLine($"Printer State: {response.PrinterAttributes.PrinterState}");
```
