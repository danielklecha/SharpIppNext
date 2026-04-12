# PWG 5100.6 Page Overrides

PWG 5100.6 defines page-level override behavior through these attributes:

- `overrides` (Job Template attribute)
- `overrides-supported` (Printer Description attribute)
- `overrides-actual` (Job Description attribute)

SharpIppNext models those attributes with:

- `JobTemplateAttributes.Overrides`
- `PrinterDescriptionAttributes.OverridesSupported`
- `JobDescriptionAttributes.OverridesActual`

Each `OverrideInstruction` can target page/document/document-copy ranges and carry nested Job Template attributes that should apply only to matching pages.

Validation notes:

- `pages`, `document-numbers`, and `document-copies` ranges use `1:MAX` bounds.
- Selector ranges must be ascending and non-overlapping.
- Selector members must appear before overriding Job Template members inside each collection.
- `overrides` is accepted on job-submission/update operations (`Print-Job`, `Print-URI`, `Validate-Job`, `Create-Job`, `Set-Job-Attributes`, `Send-Document`, and `Send-URI`).
- Members with Job/Document scope (for example `copies`, `job-priority`, `job-hold-until`, `multiple-document-handling`) are not valid override members.

## Submit a job with page overrides

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
        Media = (Media)"iso_a4_210x297mm",
        Sides = Sides.TwoSidedLongEdge,
        Overrides =
        [
            new OverrideInstruction
            {
                PageRanges = [new Range(1, 1)],
                DocumentNumberRanges = [new Range(1, int.MaxValue)],
                JobTemplateAttributes = new JobTemplateAttributes
                {
                    Media = (Media)"na_letter_8.5x11in",
                    Sides = Sides.OneSided
                }
            }
        ]
    }
};

var response = await client.PrintJobAsync(request);
Console.WriteLine($"Submitted job {response.JobAttributes?.JobId}.");
```

## Discover supported override members

```csharp
using System;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

var response = await client.GetPrinterAttributesAsync(new GetPrinterAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer")
    }
});

foreach (var member in response.PrinterAttributes?.OverridesSupported ?? Array.Empty<SharpIpp.Protocol.Models.OverrideSupported>())
{
    Console.WriteLine(member);
}
```

## Inspect actual overrides used by the printer

```csharp
using System;
using SharpIpp;
using SharpIpp.Models.Requests;

var client = new SharpIppClient();

var response = await client.GetJobAttributesAsync(new GetJobAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://localhost:631/printers/my-printer"),
        JobId = 123
    }
});

foreach (var actual in response.JobAttributes?.OverridesActual ?? Array.Empty<SharpIpp.Protocol.Models.OverrideInstruction>())
{
    Console.WriteLine($"Pages: {actual.Pages}");
}
```
