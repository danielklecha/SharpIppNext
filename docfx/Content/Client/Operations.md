# SharpIppNext Operations

The `ISharpIppClient` interface provides a comprehensive set of operations defined in the IPP (Internet Printing Protocol) specifications, specifically targeting [RFC 2911](https://tools.ietf.org/html/rfc2911) and CUPS-specific extensions.

## Supported V1.1 Operations

### Job Operations

| Operation              | Method                  | Description                                                                                                                                              |
| ---------------------- | ----------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Cancel-Job**         | `CancelJobAsync`        | Allows a client to cancel a Print Job from the time the job is created up to the time it is completed, canceled, or aborted.                             |
| **Create-Job**         | `CreateJobAsync`        | Similar to the Print-Job operation except that in the Create-Job request, a client does not supply document data. Followed by Send-Document or Send-URI. |
| **Get-Job-Attributes** | `GetJobAttributesAsync` | Requests the values of attributes of a specific Job object.                                                                                              |
| **Get-Jobs**           | `GetJobsAsync`          | Retrieves the list of Job objects belonging to the target Printer object.                                                                                |
| **Hold-Job**           | `HoldJobAsync`          | Holds a pending job in the queue so that it is not eligible for scheduling.                                                                              |
| **Print-Job**          | `PrintJobAsync`         | Submits a print job with only one document and supplies the document data.                                                                               |
| **Print-URI**          | `PrintUriAsync`         | Submits a print job with a URI reference to the document data rather than including the document data itself.                                            |
| **Purge-Jobs**         | `PurgeJobsAsync`        | Removes all jobs from an IPP Printer object, regardless of their job states.                                                                             |
| **Release-Job**        | `ReleaseJobAsync`       | Releases a previously held job so that it is again eligible for scheduling.                                                                              |
| **Restart-Job**        | `RestartJobAsync`       | Restarts a job that is retained in the queue after processing has completed.                                                                             |
| **Send-Document**      | `SendDocumentAsync`     | Adds a document to a multi-document Job object created by `Create-Job`.                                                                                  |
| **Send-URI**           | `SendUriAsync`          | Adds a document (via URI reference) to a multi-document Job object.                                                                                      |
| **Validate-Job**       | `ValidateJobAsync`      | Verifies capabilities of a printer object against supplied attributes without actually creating a job.                                                   |

### Printer Operations

| Operation                  | Method                      | Description                                                       |
| -------------------------- | --------------------------- | ----------------------------------------------------------------- |
| **Get-Printer-Attributes** | `GetPrinterAttributesAsync` | Requests the values of attributes of a Printer object.            |
| **Pause-Printer**          | `PausePrinterAsync`         | Stops the Printer object from scheduling jobs on all its devices. |
| **Resume-Printer**         | `ResumePrinterAsync`        | Resumes the Printer object scheduling jobs on all its devices.    |

## CUPS Specific Operations

| Operation             | Method                 | Description                                                           |
| --------------------- | ---------------------- | --------------------------------------------------------------------- |
| **CUPS-Get-Printers** | `GetCUPSPrintersAsync` | Returns the printer attributes for every printer known to the system. |

## Custom Operations

If you need to send a custom operation or an operation not currently exposed by a strongly-typed method in `ISharpIppClient`, you can use the generic `SendAsync` overload. See the [Custom Requests](CustomRequests.md) documentation for more details.
