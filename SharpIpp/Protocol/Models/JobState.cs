namespace SharpIpp.Protocol.Models
{
    /// <summary>
    ///     https://tools.ietf.org/html/rfc2911#section-4.3.7
    /// </summary>
    public enum JobState
    {
        /// <summary>
        ///     The job is a candidate to start processing, but is
        ///     not yet processing.
        /// </summary>
        Pending = 3,

        /// <summary>
        ///     The job is not a candidate for processing for
        ///     any number of reasons but will return to the 'pending'
        ///     state as soon as the reasons are no longer present.  The
        ///     job's "job-state-reason" attribute MUST indicate why the
        ///     job is no longer a candidate for processing.
        /// </summary>
        PendingHeld = 4,

        /// <summary>
        ///     One or more of:
        ///     1.  the job is using, or is attempting to use, one or
        ///     more purely software processes that are analyzing,
        ///     creating, or interpreting a PDL, etc.,
        ///     2.  the job is using, or is attempting to use, one or
        ///     more hardware devices that are interpreting a PDL, making
        ///     marks on a medium, and/or performing finishing, such as
        ///     stapling, etc.,
        ///     3. the Printer object has made the job ready for
        ///     printing, but the output device is not yet printing it,
        ///     either because the job hasn't reached the output device
        ///     or because the job is queued in the output device or some
        ///     other spooler, awaiting the output device to print it.
        ///     When the job is in the 'processing' state, the entire job
        ///     state includes the detailed status represented in the
        ///     Printer object's "printer-state", "printer-state-
        ///     reasons", and "printer-state-message" attributes.
        ///     Implementations MAY, though they NEED NOT,  include
        ///     additional values in the job's "job-state-reasons"
        ///     attribute to indicate the progress of the job, such as
        ///     adding the 'job-printing' value to indicate when the
        ///     output device is actually making marks on paper and/or
        ///     the 'processing-to-stop-point' value to indicate that the
        ///     IPP object is in the process of canceling or aborting the
        ///     job.  Most implementations won't bother with this nuance.
        /// </summary>
        Processing = 5,

        /// <summary>
        ///     The job has stopped while processing
        ///     for any number of reasons and will return to the
        ///     'processing' state as soon as the reasons are no longer
        ///     present.
        ///     The job's "job-state-reason" attribute MAY indicate why
        ///     the job has stopped processing.  For example, if the
        ///     output device is stopped, the 'printer-stopped' value MAY
        ///     be included in the job's "job-state-reasons" attribute.
        ///     Note:  When an output device is stopped, the device
        ///     usually indicates its condition in human readable form
        ///     locally at the device.  A client can obtain more complete
        ///     device status remotely by querying the Printer object's
        ///     "printer-state", "printer-state-reasons" and "printer-
        ///     state-message" attributes.
        /// </summary>
        ProcessingStopped = 6,

        /// <summary>
        ///     The job has been canceled by a Cancel-Job
        ///     operation and the Printer object has completed canceling
        ///     the job and all job status attributes have reached their
        ///     final values for the job.  While the Printer object is
        ///     canceling the job, the job remains in its current state,
        ///     but the job's "job-state-reasons" attribute SHOULD
        ///     contain the 'processing-to-stop-point' value and one of
        ///     the 'canceled-by-user', 'canceled-by-operator', or
        ///     'canceled-at-device' value.  When the job moves to the
        ///     'canceled' state, the  'processing-to-stop-point' value,
        ///     if present, MUST be removed, but the 'canceled-by-xxx',
        ///     if present, MUST remain.
        /// </summary>
        Canceled = 7,

        /// <summary>
        ///     The job has been aborted by the system, usually
        ///     while the job was in the 'processing' or 'processing-
        ///     stopped' state and the Printer has completed aborting the
        ///     job and all job status attributes have reached their
        ///     final values for the job.  While the Printer object is
        ///     aborting the job, the job remains in its current state,
        ///     but the job's "job-state-reasons" attribute SHOULD
        ///     contain the 'processing-to-stop-point' and 'aborted-by-
        ///     system' values.  When the job moves to the 'aborted'
        ///     state, the  'processing-to-stop-point' value, if present,
        ///     MUST be removed, but the 'aborted-by-system' value, if
        ///     present, MUST remain.
        /// </summary>
        Aborted = 8,

        /// <summary>
        ///     The job has completed successfully or with
        ///     warnings or errors after processing and all of the job
        ///     media sheets have been successfully stacked in the
        ///     appropriate output bin(s) and all job status attributes
        ///     have reached their final values for the job.  The job's
        ///     "job-state-reasons" attribute SHOULD contain one of:
        ///     'completed-successfully', 'completed-with-warnings', or
        ///     'completed-with-errors' values.
        /// </summary>
        Completed = 9,
    }
}
