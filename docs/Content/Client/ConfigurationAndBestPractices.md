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
