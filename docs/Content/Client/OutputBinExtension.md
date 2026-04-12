# PWG 5100.2 Output-Bin Extension

PWG 5100.2 defines the `output-bin` Job Template attribute plus the related printer description attributes `output-bin-default` and `output-bin-supported`. SharpIppNext exposes those values through `JobTemplateAttributes.OutputBin` and `PrinterDescriptionAttributes.OutputBinDefault` / `OutputBinSupported`.

Use `OutputBin` for the standard keywords defined by the specification, for generated keyword forms such as `stacker-N`, `mailbox-N`, and `tray-N`, for vendor extension keywords, and for site-defined administrator names.

## Submit a job to a specific output bin

```csharp
using System;
using System.IO;
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Protocol.Models;

var client = new SharpIppClient();

using var stream = File.OpenRead(@"C:\example.pdf");

var request = new PrintJobRequest
{
    Document = stream,
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        DocumentFormat = "application/pdf"
    },
    JobTemplateAttributes = new JobTemplateAttributes
    {
        OutputBin = OutputBin.Stacker(2)
    }
};

var response = await client.PrintJobAsync(request);
Console.WriteLine($"Submitted job {response.JobAttributes?.JobId} to {request.JobTemplateAttributes?.OutputBin}.");
```

## Read supported output bins from the printer

```csharp
using System;
using System.Linq;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

var request = new GetPrinterAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
};

var response = await client.GetPrinterAttributesAsync(request);
var attributes = response.PrinterAttributes;

Console.WriteLine($"Default output bin: {attributes?.OutputBinDefault}");
Console.WriteLine("Supported output bins:");

foreach (var outputBin in attributes?.OutputBinSupported ?? Array.Empty<SharpIpp.Protocol.Models.OutputBin>())
{
    Console.WriteLine($" - {outputBin}");
}
```

## Custom administrator-defined names

The specification allows output bins to be exposed as local `name(MAX)` values in addition to keywords. Use an explicit cast from `string` when you need one of those values:

```csharp
using SharpIpp.Protocol.Models;

var namedBin = (OutputBin)"Accounting Mailbox";
```

If you need to force a keyword-like token to be serialized as a local name, construct `OutputBin` with `isKeyword: false`.

```csharp
using SharpIpp.Protocol.Models;

var extensionKeyword = new OutputBin("vendor-bin-42", true);
var forcedName = new OutputBin("custom-finisher-bin", false);
```

SharpIppNext will serialize standard and extension keyword forms with the IPP `keyword` tag and local names with the IPP `nameWithoutLanguage` tag.

The request validator also enforces the PWG 5100.2 constraint that `output-bin` is single-valued and uses only `keyword` or `nameWithoutLanguage` syntax.

For capability-aware validation, set `IppRequestValidator.Context.OutputBinSupported` to the printer's supported bins. When `UseIppAttributeFidelityForCapabilityValidation` is `true`, unsupported values are rejected only when `ipp-attribute-fidelity=true`; otherwise they are always rejected when support context is provided.
