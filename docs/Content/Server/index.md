# SharpIpp Server

SharpIppNext provides server-side IPP support through `ISharpIppServer` and `SharpIppServer`. This enables building custom IPP print servers, virtual printers, print spools, or IPP-to-cloud gateways in .NET and ASP.NET Core.

## 1. Overview and Architecture

The server pipeline performs two main functions:
1. **Request Deserialization & Validation:** Reads incoming binary IPP data from an HTTP request stream (`ReceiveRequestAsync` or `ReceiveRawRequestAsync`) and deserializes it into strongly-typed request models (`IIppRequest`) or raw messages (`IIppRequestMessage`).
2. **Response Serialization:** Takes strongly-typed response models (`IIppResponse`) or raw response messages (`IIppResponseMessage`) and serializes them into the HTTP response stream (`SendResponseAsync` or `SendRawResponseAsync`).

## 2. Dependency Injection Setup

Register `ISharpIppServer` as a singleton in your ASP.NET Core `Program.cs`:

```csharp
using SharpIpp;

var builder = WebApplication.CreateBuilder(args);

// Register SharpIppServer as a singleton service
builder.Services.AddSingleton<ISharpIppServer, SharpIppServer>();

var app = builder.Build();
```

## 3. ASP.NET Core Minimal API Integration

IPP clients communicate over HTTP POST using the `application/ipp` MIME type. IPP specifies that application-level errors (such as unknown jobs or unsupported formats) must still return HTTP `200 OK` with an IPP status code in the response body.

Below is a complete example of an ASP.NET Core endpoint handling printer discovery (`GetPrinterAttributes`), job validation, job submission (`PrintJob`), and cancellation:

```csharp
using SharpIpp;
using SharpIpp.Models.Requests;
using SharpIpp.Models.Responses;
using SharpIpp.Protocol.Models;

app.MapPost("/printers/{printerName}", async (
    string printerName,
    HttpContext context,
    ISharpIppServer server) =>
{
    // Read and parse the incoming IPP request from the HTTP request stream
    IIppRequest request = await server.ReceiveRequestAsync(context.Request.Body);

    // Dispatch based on the strongly typed request type
    IIppResponse response = request switch
    {
        GetPrinterAttributesRequest req => HandleGetPrinterAttributes(req, printerName, context.Request),
        ValidateJobRequest req => HandleValidateJob(req),
        PrintJobRequest req => await HandlePrintJobAsync(req, printerName, context.Request),
        GetJobAttributesRequest req => HandleGetJobAttributes(req),
        CancelJobRequest req => HandleCancelJob(req),
        _ => new GetPrinterAttributesResponse
        {
            RequestId = request.RequestId,
            Version = request.Version,
            StatusCode = IppStatusCode.ServerErrorOperationNotSupported
        }
    };

    // Ensure the response matches IPP Content-Type
    context.Response.ContentType = "application/ipp";

    // Serialize and stream the IPP response back to the client
    await server.SendResponseAsync(response, context.Response.Body);
});

app.Run();
```

## 4. Implementing Operation Handlers

### Handling `GetPrinterAttributes` (Printer Discovery)

Operating systems (Windows Spooler, CUPS, macOS AirPrint) always query printer attributes first to discover capabilities, supported formats, and state.

```csharp
static GetPrinterAttributesResponse HandleGetPrinterAttributes(
    GetPrinterAttributesRequest request,
    string printerName,
    HttpRequest httpRequest)
{
    var printerUri = new Uri($"{httpRequest.Scheme}://{httpRequest.Host}/printers/{printerName}");

    return new GetPrinterAttributesResponse
    {
        RequestId = request.RequestId,
        Version = request.Version,
        StatusCode = IppStatusCode.SuccessfulOk,
        PrinterAttributes = new PrinterDescriptionAttributes
        {
            PrinterName = printerName,
            PrinterState = PrinterState.Idle,
            PrinterStateReasons = [PrinterStateReason.None],
            PrinterUriSupported = [printerUri],
            UriAuthenticationSupported = [UriAuthentication.None],
            UriSecuritySupported = [UriSecurity.None],
            DocumentFormatSupported = [DocumentFormat.ApplicationPdf, DocumentFormat.ApplicationPostscript, DocumentFormat.ImagePwgRaster],
            OperationsSupported =
            [
                IppOperation.PrintJob,
                IppOperation.ValidateJob,
                IppOperation.GetPrinterAttributes,
                IppOperation.GetJobAttributes,
                IppOperation.GetJobs,
                IppOperation.CancelJob
            ]
        }
    };
}
```

### Handling `ValidateJob`

Clients call `ValidateJob` before sending large documents to verify that the printer accepts the target format and job attributes.

```csharp
static ValidateJobResponse HandleValidateJob(ValidateJobRequest request)
{
    // Verify requested format
    var format = request.OperationAttributes.DocumentFormat;
    if (format != null && format != "application/pdf" && format != "application/postscript")
    {
        return new ValidateJobResponse
        {
            RequestId = request.RequestId,
            Version = request.Version,
            StatusCode = IppStatusCode.ClientErrorDocumentFormatNotSupported
        };
    }

    return new ValidateJobResponse
    {
        RequestId = request.RequestId,
        Version = request.Version,
        StatusCode = IppStatusCode.SuccessfulOk
    };
}
```

### Handling `PrintJob` and Spooling the Document Stream

When `PrintJobRequest` arrives, `request.Document` contains the incoming binary document data (PDF, PostScript, PWG-Raster, etc.).

```csharp
static async Task<PrintJobResponse> HandlePrintJobAsync(
    PrintJobRequest request,
    string printerName,
    HttpRequest httpRequest)
{
    int jobId = Random.Shared.Next(1, 100000);
    var jobUri = new Uri($"{httpRequest.Scheme}://{httpRequest.Host}/printers/{printerName}/jobs/{jobId}");

    // Spool the incoming document stream to disk or processing pipeline
    var spoolPath = Path.Combine(Path.GetTempPath(), $"job_{jobId}.bin");
    await using (var fileStream = File.Create(spoolPath))
    {
        await request.Document.CopyToAsync(fileStream);
    }

    return new PrintJobResponse
    {
        RequestId = request.RequestId,
        Version = request.Version,
        StatusCode = IppStatusCode.SuccessfulOk,
        JobAttributes = new JobDescriptionAttributes
        {
            JobId = jobId,
            JobUri = jobUri,
            JobState = JobState.Pending,
            JobStateReasons = [JobStateReason.None],
            NumberOfInterveningJobs = 0
        }
    };
}
```

### Handling `GetJobAttributes` and `CancelJob`

```csharp
static GetJobAttributesResponse HandleGetJobAttributes(GetJobAttributesRequest request)
{
    int? jobId = request.OperationAttributes.JobId;
    // In production, look up the job by jobId or JobUri in your database/spool
    return new GetJobAttributesResponse
    {
        RequestId = request.RequestId,
        Version = request.Version,
        StatusCode = IppStatusCode.SuccessfulOk,
        JobAttributes = new JobDescriptionAttributes
        {
            JobId = jobId ?? 1,
            JobState = JobState.Processing,
            JobStateReasons = [JobStateReason.JobPrinting]
        }
    };
}

static CancelJobResponse HandleCancelJob(CancelJobRequest request)
{
    return new CancelJobResponse
    {
        RequestId = request.RequestId,
        Version = request.Version,
        StatusCode = IppStatusCode.SuccessfulOk
    };
}
```

## 5. Working with Raw IPP Messages

If you are implementing custom vendor-specific operations or need low-level access to attribute groups, use the raw messaging API:

```csharp
// Receive raw message
IIppRequestMessage rawRequest = await server.ReceiveRawRequestAsync(context.Request.Body);

// Inspect raw operation and attributes
Console.WriteLine($"Operation: {rawRequest.IppOperation}, RequestId: {rawRequest.RequestId}");

// Construct raw response message
var rawResponse = new IppResponseMessage
{
    RequestId = rawRequest.RequestId,
    Version = rawRequest.Version,
    StatusCode = IppStatusCode.SuccessfulOk
};

// Write raw response to HTTP response stream
await server.SendRawResponseAsync(rawResponse, context.Response.Body);
```

## 6. Configuring Server Validation

`SharpIppServer` uses built-in validators by default (`IppRequestMessageValidator.ServerDefault`, `IppRequestValidator.Default`). You can configure or disable them:

```csharp
var server = new SharpIppServer();

// Disable request message validation if handling non-standard client payloads
server.RequestMessageValidator = null;

// Customize typed request validator
server.RequestValidator = null;
```

## 7. Advanced Scenarios

- For handling `Tag.NoValue` (`0x13`) on the server side, see [Advanced NoValue in Server](advanced-novalue.md).
