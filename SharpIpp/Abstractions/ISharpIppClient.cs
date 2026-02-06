using System;
using System.Threading;
using System.Threading.Tasks;

using SharpIpp.Models;
using SharpIpp.Protocol;

namespace SharpIpp
{
    public interface ISharpIppClient : IDisposable
    {
        /// <summary>
        ///     Custom Operation, not yet implemented via SharpIpp
        /// </summary>
        Task<IIppResponseMessage> SendAsync(Uri printerUri, IIppRequestMessage request, CancellationToken cancellationToken = default);

        IIppRequestMessage CreateRawRequest<T>(T ippRequestMessage) where T : IIppRequest;

        T CreateResponse<T>(IIppResponseMessage ippResponse) where T : IIppResponseMessage;

        #region V11

        /// <summary>
        ///     <a href="https://tools.ietf.org/html/rfc2911#section-3.3.3">Cancel-Job Operation</a>
        ///     This REQUIRED operation allows a client to cancel a Print Job from
        ///     the time the job is created up to the time it is completed, canceled,
        ///     or aborted.  Since a Job might already be printing by the time a
        ///     Cancel-Job is received, some media sheet pages might be printed
        ///     before the job is actually terminated.
        /// </summary>
        Task<CancelJobResponse> CancelJobAsync(CancelJobRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.2.4">Create-Job Operation</a>
        ///     This OPTIONAL operation is similar to the Print-Job operation
        ///     except that in the Create-Job request, a client does
        ///     not supply document data or any reference to document data.  Also,
        ///     the client does not supply any of the "document-name", "document-
        ///     format", "compression", or "document-natural-language" operation
        ///     attributes.  This operation is followed by one or more Send-Document
        ///     or Send-URI operations.  In each of those operation requests, the
        ///     client OPTIONALLY supplies the "document-name", "document-format",
        ///     and "document-natural-language" attributes for each document in the
        ///     multi-document Job object.
        /// </summary>
        Task<CreateJobResponse> CreateJobAsync(CreateJobRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.3.4">Get-Job-Attributes Operation</a>
        ///     This REQUIRED operation allows a client to request the values of
        ///     attributes of a Job object and it is almost identical to the Get-
        ///     Printer-Attributes operation.  The only
        ///     differences are that the operation is directed at a Job object rather
        ///     than a Printer object, there is no "document-format" operation
        ///     attribute used when querying a Job object, and the returned attribute
        ///     group is a set of Job object attributes rather than a set of Printer
        ///     object attributes.
        /// </summary>
        Task<GetJobAttributesResponse> GetJobAttributesAsync(GetJobAttributesRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.2.6">Get-Jobs Operation</a>
        ///     This REQUIRED operation allows a client to retrieve the list of Job
        ///     objects belonging to the target Printer object.  The client may also
        ///     supply a list of Job attribute names and/or attribute group names.  A
        ///     group of Job object attributes will be returned for each Job object
        ///     that is returned.
        ///     This operation is similar to the Get-Job-Attributes operation, except
        ///     that this Get-Jobs operation returns attributes from possibly more
        ///     than one object.
        /// </summary>
        Task<GetJobsResponse> GetJobsAsync(GetJobsRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.3.4">Get-Job-Attributes Operation</a>
        ///     This REQUIRED operation allows a client to request the values of
        ///     attributes of a Job object and it is almost identical to the Get-
        ///     Printer-Attributes operation.  The only
        ///     differences are that the operation is directed at a Job object rather
        ///     than a Printer object, there is no "document-format" operation
        ///     attribute used when querying a Job object, and the returned attribute
        ///     group is a set of Job object attributes rather than a set of Printer
        ///     object attributes.
        /// </summary>
        Task<GetPrinterAttributesResponse> GetPrinterAttributesAsync(GetPrinterAttributesRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.3.5">Hold-Job Operation</a>
        ///     This OPTIONAL operation allows a client to hold a pending job in the
        ///     queue so that it is not eligible for scheduling.  If the Hold-Job
        ///     operation is supported, then the Release-Job operation MUST be
        ///     supported, and vice-versa.  The OPTIONAL "job-hold-until" operation
        ///     attribute allows a client to specify whether to hold the job
        ///     indefinitely or until a specified time period, if supported.
        /// </summary>
        Task<HoldJobResponse> HoldJobAsync(HoldJobRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.2.7">Pause-Printer Operation</a>
        ///     This OPTIONAL operation allows a client to stop the Printer object
        ///     from scheduling jobs on all its devices.  Depending on
        ///     implementation, the Pause-Printer operation MAY also stop the Printer
        ///     from processing the current job or jobs.  Any job that is currently
        ///     being printed is either stopped as soon as the implementation permits
        ///     or is completed, depending on implementation.  The Printer object
        ///     MUST still accept create operations to create new jobs, but MUST
        ///     prevent any jobs from entering the 'processing' state.
        /// </summary>
        Task<PausePrinterResponse> PausePrinterAsync(PausePrinterRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.2.1">Print-Job Operation</a>
        ///     This REQUIRED operation allows a client to submit a print job with
        ///     only one document and supply the document data (rather than just a
        ///     reference to the data).
        /// </summary>
        Task<PrintJobResponse> PrintJobAsync(PrintJobRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.2.2">Print-URI Operation</a>
        ///     This OPTIONAL operation is identical to the Print-Job operation
        ///     except that a client supplies a URI reference to the
        ///     document data using the "document-uri" (uri) operation attribute (in
        ///     Group 1) rather than including the document data itself.  Before
        ///     returning the response, the Printer MUST validate that the Printer
        ///     supports the retrieval method (e.g., http, ftp, etc.) implied by the
        ///     URI, and MUST check for valid URI syntax.  If the client-supplied URI
        ///     scheme is not supported, i.e. the value is not in the Printer
        ///     object's "referenced-uri-scheme-supported" attribute, the Printer
        ///     object MUST reject the request and return the 'client-error-uri-
        ///     scheme-not-supported' status code.
        /// </summary>
        Task<PrintUriResponse> PrintUriAsync(PrintUriRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://tools.ietf.org/html/rfc2911#section-3.2.9">Purge-Jobs Operation</a>
        ///     This OPTIONAL operation allows a client to remove all jobs from an
        ///     IPP Printer object, regardless of their job states, including jobs in
        ///     the Printer object's Job History.  After a
        ///     Purge-Jobs operation has been performed, a Printer object MUST return
        ///     no jobs in subsequent Get-Job-Attributes and Get-Jobs responses
        ///     (until new jobs are submitted).
        ///     Whether the Purge-Jobs (and Get-Jobs) operation affects jobs that
        ///     were submitted to the device from other sources than the IPP Printer
        ///     object in the same way that the Purge-Jobs operation affects jobs
        ///     that were submitted to the IPP Printer object using IPP, depends on
        ///     implementation, i.e., on whether the IPP protocol is being used as a
        ///     universal management protocol or just to manage IPP jobs,
        ///     respectively.
        /// </summary>
        Task<PurgeJobsResponse> PurgeJobsAsync(PurgeJobsRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://tools.ietf.org/html/rfc2911#section-3.3.6">Release-Job Operation</a>
        ///     This OPTIONAL operation allows a client to release a previously held
        ///     job so that it is again eligible for scheduling.  If the Hold-Job
        ///     operation is supported, then the Release-Job operation MUST be
        ///     supported, and vice-versa.
        ///     This operation removes the "job-hold-until" job attribute, if
        ///     present, from the job object that had been supplied in the create or
        ///     most recent Hold-Job or Restart-Job operation and removes its effect
        ///     on the job.  The IPP object MUST remove the 'job-hold-until-
        ///     specified' value from the job's "job-state-reasons" attribute, if
        ///     present.
        /// </summary>
        Task<ReleaseJobResponse> ReleaseJobAsync(ReleaseJobRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://tools.ietf.org/html/rfc2911#section-3.3.7">Restart-Job Operation</a>
        ///     This OPTIONAL operation allows a client to restart a job that is
        ///     retained in the queue after processing has completed.
        ///     The job is moved to the 'pending' or 'pending-held' job state and
        ///     restarts at the beginning on the same IPP Printer object with the
        ///     same attribute values.  If any of the documents in the job were
        ///     passed by reference (Print-URI or Send-URI), the Printer MUST re-
        ///     fetch the data, since the semantics of Restart-Job are to repeat all
        ///     Job processing.  The Job Description attributes that accumulate job
        ///     progress, such as "job-impressions-completed", "job-media-sheets-
        ///     completed", and "job-k-octets-processed", MUST be reset to 0 so that
        ///     they give an accurate record of the job from its restart point.  The
        ///     job object MUST continue to use the same "job-uri" and "job-id"
        ///     attribute values.
        /// </summary>
        Task<RestartJobResponse> RestartJobAsync(RestartJobRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://tools.ietf.org/html/rfc2911#section-3.2.8">Resume-Printer Operation</a>
        ///     This operation allows a client to resume the Printer object
        ///     scheduling jobs on all its devices.  The Printer object MUST remove
        ///     the 'paused' and 'moving-to-paused' values from the Printer object's
        ///     "printer-state-reasons" attribute, if present.  If there are no other
        ///     reasons to keep a device paused (such as media-jam), the IPP Printer
        ///     is free to transition itself to the 'processing' or 'idle' states,
        ///     depending on whether there are jobs to be processed or not,
        ///     respectively, and the device(s) resume processing jobs.
        /// </summary>
        Task<ResumePrinterResponse> ResumePrinterAsync(ResumePrinterRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://tools.ietf.org/html/rfc2911#section-3.3.1">Send-Document Operation</a>
        ///     This OPTIONAL operation allows a client to create a multi-document
        ///     Job object that is initially "empty" (contains no documents).  In the
        ///     Create-Job response, the Printer object returns the Job object's URI
        ///     (the "job-uri" attribute) and the Job object's 32-bit identifier (the
        ///     "job-id" attribute).  For each new document that the client desires
        ///     to add, the client uses a Send-Document operation.  Each Send-
        ///     Document Request contains the entire stream of document data for one
        ///     document.
        /// </summary>
        Task<SendDocumentResponse> SendDocumentAsync(SendDocumentRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://tools.ietf.org/html/rfc2911#section-3.3.2">Send-URI Operation</a>
        ///     This OPTIONAL operation is identical to the Send-Document operation
        ///     except that a client MUST supply a URI reference
        ///     ("document-uri" operation attribute) rather than the document data
        ///     itself.  If a Printer object supports this operation, clients can use
        ///     both Send-URI or Send-Document operations to add new documents to an
        ///     existing multi-document Job object.  However, if a client needs to
        ///     indicate that the previous Send-URI or Send-Document was the last
        ///     document,  the client MUST use the Send-Document operation with no
        ///     document data and the "last-document" flag set to 'true' (rather than
        ///     using a Send-URI operation with no "document-uri" operation
        ///     attribute).
        /// </summary>
        Task<SendUriResponse> SendUriAsync(SendUriRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        ///     <a href="https://datatracker.ietf.org/doc/html/rfc2911#section-3.2.3">Validate-Job Operation</a>
        ///     This REQUIRED operation is similar to the Print-Job operation
        ///     except that a client supplies no document data and
        ///     the Printer allocates no resources (i.e., it does not create a new
        ///     Job object).  This operation is used only to verify capabilities of a
        ///     printer object against whatever attributes are supplied by the client
        ///     in the Validate-Job request.  By using the Validate-Job operation a
        ///     client can validate that an identical Print-Job operation (with the
        ///     document data) would be accepted. The Validate-Job operation also
        ///     performs the same security negotiation as the Print-Job operation
        ///     (see section 8), so that a client can check that the client and
        ///     Printer object security requirements can be met before performing a
        ///     Print-Job operation.
        /// </summary>
        Task<ValidateJobResponse> ValidateJobAsync(ValidateJobRequest request, CancellationToken cancellationToken = default);

        #endregion


        #region CUPS10

        /// <summary>
        ///     <a href="http://www.cups.org/doc/spec-ipp.html#CUPS_GET_PRINTERS">CUPS-get-printers Request</a>
        ///     The CUPS-Get-Printers operation (0x4002) returns the printer attributes for every printer known to the system. This
        ///     may include printers that are not served directly by the server.
        /// </summary>
        Task<CUPSGetPrintersResponse> GetCUPSPrintersAsync(CUPSGetPrintersRequest request, CancellationToken cancellationToken = default);
        
        #endregion
    }
}
