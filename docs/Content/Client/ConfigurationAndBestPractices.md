# Client Configuration & Best Practices

While `new SharpIppClient()` is sufficient for quick tests and simple local network setups, production applications usually require finer control over network connections, authentication, and error handling. This guide covers how to properly configure the `SharpIppClient`.

## 1. Custom HttpClient Injection

`SharpIppClient` relies on `HttpClient` to communicate with the IPP printer. You can pass your own `HttpClient` instance into the constructor. This is useful for configuring timeouts, proxies, custom SSL validation, or adding default headers.

```csharp
using System.Net.Http;
using SharpIpp;

// Configure a custom handler (e.g., to bypass SSL errors on local network printers)
var handler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
};

// Create the HttpClient with a custom timeout
var httpClient = new HttpClient(handler)
{
    Timeout = TimeSpan.FromSeconds(30)
};

// Inject it into SharpIppClient
// Note: SharpIppClient will NOT dispose the injected HttpClient. You manage its lifecycle.
var client = new SharpIppClient(httpClient);
```

## 2. Using IHttpClientFactory (Dependency Injection)

In modern .NET applications (like ASP.NET Core or Worker Services), you should manage `HttpClient` lifetimes using `IHttpClientFactory` to avoid socket exhaustion.

You can register `ISharpIppClient` as a typed client in your `Startup.cs` or `Program.cs`:

```csharp
// Program.cs
builder.Services.AddHttpClient<ISharpIppClient, SharpIppClient>(client =>
{
    // Optional: Add default headers, timeouts, etc.
    client.Timeout = TimeSpan.FromSeconds(60);
});
```

Then simply inject `ISharpIppClient` into your services or controllers:

```csharp
public class PrintService
{
    private readonly ISharpIppClient _ippClient;

    public PrintService(ISharpIppClient ippClient)
    {
        _ippClient = ippClient;
    }

    public async Task CheckPrinterAsync()
    {
        var request = new GetPrinterAttributesRequest { ... };
        var response = await _ippClient.GetPrinterAttributesAsync(request);
    }
}
```

## 3. Adding Authentication

Many enterprise printers or CUPS servers require basic authentication or bearer tokens to execute privileged operations (like canceling a job). You can add authentication via the `HttpClient`:

### Basic Authentication

```csharp
using System.Net.Http.Headers;
using System.Text;

var httpClient = new HttpClient();
var authString = Convert.ToBase64String(Encoding.ASCII.GetBytes("username:password"));
httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

var client = new SharpIppClient(httpClient);
```

### Bearer Token Authentication (e.g., OAuth)

```csharp
using System.Net.Http.Headers;

var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "your-access-token");

var client = new SharpIppClient(httpClient);
```

## 4. Error Handling and Status Codes

When calling `SharpIppClient` methods, failures can happen at two levels:

1. **Network / Protocol Level:** If the printer is entirely unreachable or returns a non-IPP HTTP error (like `502 Bad Gateway`), a standard `HttpRequestException` will be thrown. Or if the IPP payload is severely malformed, an `IppRequestException` may occur.
2. **IPP Application Level:** The printer received the request successfully, but rejected the IPP operation (e.g., "Job not found", "Not authorized"). In this case, **no exception is thrown**. Instead, you must inspect the `StatusCode` property of the `IIppResponse`.

### Best Practice for Checking Success

Always verify `response.StatusCode` after a request:

```csharp
var response = await client.CancelJobAsync(request);

if (response.StatusCode == IppStatusCode.SuccessfulOk)
{
    Console.WriteLine("Job canceled successfully.");
}
else if (response.StatusCode == IppStatusCode.ClientErrorNotPossible)
{
    Console.WriteLine("Cannot cancel job. It may have already completed.");
}
else if (response.StatusCode == IppStatusCode.ClientErrorNotAuthorized)
{
    Console.WriteLine("You do not have permission to cancel this job.");
}
else
{
    Console.WriteLine($"Operation failed with status: {response.StatusCode}");
}
```

*Note: IPP defines many "Successful" status codes. Anything starting with `Successful` (like `SuccessfulOkIgnoredOrSubstitutedAttributes`) means the operation broadly succeeded, though minor warnings might exist.*

## 5. Document Streams & Stream Lifecycles

When sending or receiving document binary data (PDF, PostScript, PWG-Raster), keep the following stream nuances in mind:

### Request Stream Position and Disposal
In `PrintJobRequest` and `SendDocumentRequest`, the `Document` property is a standard `System.IO.Stream`:
- **Stream Position:** Always verify that `stream.Position == 0` before passing it to the request. If you read or generated the stream prior to printing (e.g., using a `MemoryStream`), reset `stream.Seek(0, SeekOrigin.Begin)` or `stream.Position = 0`.
- **Disposal Ownership:** `SharpIppClient` does **not** close or dispose your input stream. The caller is responsible for disposing the stream after the async operation completes (e.g., via `using` or `await using`).

```csharp
await using var pdfStream = File.OpenRead("invoice.pdf");

var request = new PrintJobRequest
{
    Document = pdfStream,
    OperationAttributes = new()
    {
        PrinterUri = printerUri,
        DocumentFormat = "application/pdf"
    }
};

var response = await client.PrintJobAsync(request);
```

### Receiving Document Streams (`FetchDocument`)
When calling `FetchDocumentAsync`, the document content is returned in `response.Document`:
- Ensure you consume or copy `response.Document` before disposing the response context.

---

## 6. Job Targeting: `JobUri` vs (`PrinterUri` + `JobId`)

In the IPP specification (RFC 8011), any operation targeted at a job (such as `GetJobAttributes`, `CancelJob`, `HoldJob`, `ReleaseJob`, `RestartJob`) requires the client to identify the job using **either**:
1. `JobUri`: The absolute URI assigned to the job by the printer, **OR**
2. `PrinterUri` + `JobId`: The target printer's URI combined with the integer job ID.

> [!WARNING]
> Do not provide both or neither. If both are supplied or both are omitted, many physical printers will reject the request with `ClientErrorBadRequest` or `ClientErrorAttributesOrValuesInConflict`.

```csharp
// Option 1: Using PrinterUri + JobId
var requestByPrinterAndId = new GetJobAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = new Uri("ipp://192.168.1.50/ipp/print"),
        JobId = 42
    }
};

// Option 2: Using JobUri
var requestByJobUri = new GetJobAttributesRequest
{
    OperationAttributes = new()
    {
        JobUri = new Uri("ipp://192.168.1.50/ipp/print/job-42")
    }
};
```

---

## 7. Multi-Document Job Workflow

When printing a multi-document job (e.g., printing multiple separate files as a single print job), do not use `PrintJobAsync`. Instead, use the `CreateJobAsync` $\rightarrow$ `SendDocumentAsync` pipeline:

1. **Create the Job:** Call `CreateJobAsync` with job-level attributes (copies, sides, finishings). The printer returns a `JobId` or `JobUri`.
2. **Send Intermediate Documents:** Call `SendDocumentAsync` with `LastDocument = false` for each file prior to the last.
3. **Send the Final Document:** Call `SendDocumentAsync` on the final file with `LastDocument = true`. This signals to the printer that all documents have been spooled and the job can be processed.

```csharp
// 1. Create the job container
var createJobResponse = await client.CreateJobAsync(new CreateJobRequest
{
    OperationAttributes = new() { PrinterUri = printerUri }
});
int jobId = createJobResponse.JobAttributes.JobId;

// 2. Send Document 1 (LastDocument = false)
await using var doc1Stream = File.OpenRead("cover.pdf");
await client.SendDocumentAsync(new SendDocumentRequest
{
    Document = doc1Stream,
    OperationAttributes = new()
    {
        PrinterUri = printerUri,
        JobId = jobId,
        LastDocument = false,
        DocumentFormat = "application/pdf"
    }
});

// 3. Send Document 2 (LastDocument = true to close the spool)
await using var doc2Stream = File.OpenRead("content.pdf");
await client.SendDocumentAsync(new SendDocumentRequest
{
    Document = doc2Stream,
    OperationAttributes = new()
    {
        PrinterUri = printerUri,
        JobId = jobId,
        LastDocument = true,
        DocumentFormat = "application/pdf"
    }
});
```

---

## 8. IPP Version Compatibility (`Version = IppVersion.V1_1`)

SharpIpp defaults to `IppVersion.V1_1` in its protocol layer, but certain printers may be strict about supported versions:
- Some older or embedded network printers only accept `1.1` and will immediately reject requests with version `2.0` or higher (`ServerErrorVersionNotSupported`).
- When targeting printers with strict firmware requirements, explicitly specify the version on the request model:

```csharp
var request = new GetPrinterAttributesRequest
{
    Version = IppVersion.V1_1,
    OperationAttributes = new()
    {
        PrinterUri = printerUri
    }
};
```

---

## 9. Attribute Subsetting & Performance (`RequestedAttributes`)

Printers can have hundreds of supported attributes and media capabilities. Querying all attributes on low-power printer controllers can take hundreds of milliseconds.
To query only the specific attributes your application needs, supply `RequestedAttributes`:

```csharp
var request = new GetPrinterAttributesRequest
{
    OperationAttributes = new()
    {
        PrinterUri = printerUri,
        // Request only the attributes you actually need
        RequestedAttributes =
        [
            "printer-name",
            "printer-state",
            "printer-state-reasons",
            "queued-job-count"
        ]
    }
};

var response = await client.GetPrinterAttributesAsync(request);
```

You can also use standard attribute group names:
- `"all"`: All supported attributes.
- `"printer-description"`: Status and capability attributes.
- `"job-template"`: Attributes available for configuring print jobs.

---

## 10. Polling Job Completion Pattern

Because IPP printing is asynchronous, `PrintJobAsync` returns as soon as the print job is accepted and spooled, not when physical printing finishes.
To monitor a job until completion, poll `GetJobAttributesAsync` using a loop:

```csharp
public async Task WaitForJobCompletionAsync(
    ISharpIppClient client,
    Uri printerUri,
    int jobId,
    CancellationToken cancellationToken = default)
{
    var terminalStates = new[]
    {
        JobState.Completed,
        JobState.Canceled,
        JobState.Aborted
    };

    while (!cancellationToken.IsCancellationRequested)
    {
        var response = await client.GetJobAttributesAsync(new GetJobAttributesRequest
        {
            OperationAttributes = new()
            {
                PrinterUri = printerUri,
                JobId = jobId,
                RequestedAttributes = ["job-state", "job-state-reasons"]
            }
        }, cancellationToken);

        var state = response.JobAttributes.JobState;
        Console.WriteLine($"Current Job State: {state}");

        if (terminalStates.Contains(state))
        {
            if (state == JobState.Completed)
                Console.WriteLine("Job finished printing successfully.");
            else
                Console.WriteLine($"Job terminated with state: {state}");
            return;
        }

        // Wait before next poll to prevent flooding the printer
        await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
    }
}
```

## 9. Native AOT & .NET Native (UWP) Compilation

`SharpIpp` is fully compatible with modern **Native AOT** (.NET 8+) and legacy **.NET Native** (Universal Windows Platform / UWP).

- **Trimming & AOT**: Type mapping and validation profiles are source-generated at compile time. Runtime mapper registries are protected from linker trimming via embedded `ILLink.Descriptors.xml`.
- **Zero-Allocation Hot Paths**: Core message validation and mapping pipelines eliminate lambdas and delegate closures using `Try*` patterns (`TryValidate*Core`), guaranteeing zero heap allocations on the validation happy path.
- **UWP & .NET Native**: For legacy UWP projects compiling with `.NET Native` (`nutc_driver.exe`), specific compiler directives and memory optimization guidance are available.

For detailed configuration instructions and benchmarks, see the [.NET Native & UWP Compilation Guide](DotNetNative.md).

