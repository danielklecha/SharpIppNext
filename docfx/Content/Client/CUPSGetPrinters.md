# CUPS-Get-Printers Example

Here is a basic example of how to initialize a `CUPSGetPrintersRequest` and return the printer attributes for every printer known to the system using `SharpIppClient`. Optional attributes have been omitted for clarity.

```csharp
using System;
using System.Threading.Tasks;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

// Initialize the get printers request with required operation attributes
var request = new CUPSGetPrintersRequest
{
    OperationAttributes = new()
    {
        // The printer URI is unused but required by the interface base class
        PrinterUri = new Uri("ipp://localhost:631")
    }
};

// Send the request and await the response 
var response = await client.GetCUPSPrintersAsync(request);

Console.WriteLine($"Total printers found: {response.Printers.Length}");
foreach (var printer in response.Printers)
{
    Console.WriteLine($"Printer Name: {printer.PrinterName}, URI: {printer.PrinterUriSupported?[0]}");
}
```
