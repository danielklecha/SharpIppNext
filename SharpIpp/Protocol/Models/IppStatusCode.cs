namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// IPP status codes returned in operation responses.
    /// See: RFC 8011 Section 4.1.6
    /// </summary>
    public enum IppStatusCode : short
    {
        /// <summary>
        /// successful-ok
        /// See: RFC 8011 Section 4.1.6.1
        /// </summary>
        SuccessfulOk = 0x0000,

        /// <summary>
        /// successful-ok-ignored-or-substituted-attributes
        /// See: RFC 8011 Section 4.1.6.1
        /// </summary>
        SuccessfulOkIgnoredOrSubstitutedAttributes = 0x0001,

        /// <summary>
        /// successful-ok-conflicting-attributes
        /// See: RFC 8011 Section 4.1.6.1
        /// </summary>
        SuccessfulOkConflictingAttributes = 0x0002,

        /// <summary>
        /// successful-ok-ignored-subscriptions
        /// See: RFC 3995 Section 5.1
        /// </summary>
        SuccessfulOkIgnoredSubscriptions = 0x0003,

        /// <summary>
        /// successful-ok-too-many-events
        /// See: RFC 3995 Section 5.1
        /// </summary>
        SuccessfulOkTooManyEvents = 0x0005,

        /// <summary>
        /// successful-ok-events-complete
        /// See: RFC 3996 Section 5.1
        /// </summary>
        SuccessfulOkEventsComplete = 0x0007,

        /// <summary>
        /// client-error-bad-request
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorBadRequest = 0x0400,

        /// <summary>
        /// client-error-forbidden
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorForbidden = 0x0401,

        /// <summary>
        /// client-error-not-authenticated
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorNotAuthenticated = 0x0402,

        /// <summary>
        /// client-error-not-authorized
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorNotAuthorized = 0x0403,

        /// <summary>
        /// client-error-not-possible
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorNotPossible = 0x0404,

        /// <summary>
        /// client-error-timeout
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorTimeout = 0x0405,

        /// <summary>
        /// client-error-not-found
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorNotFound = 0x0406,

        /// <summary>
        /// client-error-gone
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorGone = 0x0407,

        /// <summary>
        /// client-error-request-entity-too-large
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorRequestEntityTooLarge = 0x0408,

        /// <summary>
        /// client-error-request-value-too-long
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorRequestValueTooLong = 0x0409,

        /// <summary>
        /// client-error-document-format-not-supported
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorDocumentFormatNotSupported = 0x040A,

        /// <summary>
        /// client-error-attributes-or-values-not-supported
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorAttributesOrValuesNotSupported = 0x040B,

        /// <summary>
        /// client-error-uri-scheme-not-supported (https://tools.ietf.org/html/RFC8011)
        /// See: RFC 8011
        /// </summary>
        ClientErrorUriSchemeNotSupported = 0x040C,

        /// <summary>
        /// client-error-charset-not-supported
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorCharsetNotSupported = 0x040D,

        /// <summary>
        /// client-error-conflicting-attributes
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorConflictingAttributes = 0x040E,

        /// <summary>
        /// client-error-compression-not-supported
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorCompressionNotSupported = 0x040F,

        /// <summary>
        /// client-error-compression-error
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorCompressionError = 0x0410,

        /// <summary>
        /// client-error-document-format-error
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorDocumentFormatError = 0x0411,

        /// <summary>
        /// client-error-document-access-error
        /// See: RFC 8011 Section 4.1.6
        /// </summary>
        ClientErrorDocumentAccessError = 0x0412,

        /// <summary>
        /// client-error-attributes-not-settable
        /// See: RFC 3380 Section 7.1
        /// </summary>
        ClientErrorAttributesNotSettable = 0x0413,

        /// <summary>
        /// client-error-ignored-all-subscriptions
        /// See: RFC 3995 Section 5.1
        /// </summary>
        ClientErrorIgnoredAllSubscriptions = 0x0414,

        /// <summary>
        /// client-error-too-many-subscriptions
        /// See: RFC 3995 Section 5.1
        /// </summary>
        ClientErrorTooManySubscriptions = 0x0415,

        /// <summary>
        /// client-error-document-password-error
        /// See: PWG 5100.13-2023 Section 9.1
        /// </summary>
        ClientErrorDocumentPasswordError = 0x0418,

        /// <summary>
        /// client-error-document-permission-error
        /// See: PWG 5100.13-2023 Section 9.1
        /// </summary>
        ClientErrorDocumentPermissionError = 0x0419,

        /// <summary>
        /// client-error-document-security-error
        /// See: PWG 5100.13-2023 Section 9.1
        /// </summary>
        ClientErrorDocumentSecurityError = 0x041A,

        /// <summary>
        /// client-error-document-unprintable-error
        /// See: PWG 5100.13-2023 Section 9.1
        /// </summary>
        ClientErrorDocumentUnprintableError = 0x041B,

        /// <summary>
        /// client-error-account-info-needed
        /// See: PWG 5100.11-2024 Section 5.4.1
        /// </summary>
        ClientErrorAccountInfoNeeded = 0x041C,

        /// <summary>
        /// client-error-account-closed
        /// See: PWG 5100.11-2024 Section 5.4.1
        /// </summary>
        ClientErrorAccountClosed = 0x041D,

        /// <summary>
        /// client-error-account-limit-reached
        /// See: PWG 5100.11-2024 Section 5.4.1
        /// </summary>
        ClientErrorAccountLimitReached = 0x041E,

        /// <summary>
        /// client-error-account-authorization-failed
        /// See: PWG 5100.11-2024 Section 5.4.1
        /// </summary>
        ClientErrorAccountAuthorizationFailed = 0x041F,

        /// <summary>
        /// client-error-not-fetchable
        /// See: PWG 5100.18-2025 Section 9.1
        /// </summary>
        ClientErrorNotFetchable = 0x0420,

        /// <summary>
        /// server-error-internal-error (https://tools.ietf.org/html/RFC8011)
        /// See: RFC 8011
        /// </summary>
        ServerErrorInternalError = 0x0500,

        /// <summary>
        /// server-error-operation-not-supported (https://tools.ietf.org/html/RFC8011)
        /// See: RFC 8011
        /// </summary>
        ServerErrorOperationNotSupported = 0x0501,

        /// <summary>
        /// server-error-service-unavailable (https://tools.ietf.org/html/RFC8011)
        /// See: RFC 8011
        /// </summary>
        ServerErrorServiceUnavailable = 0x0502,

        /// <summary>
        /// server-error-version-not-supported (https://tools.ietf.org/html/RFC8011)
        /// See: RFC 8011
        /// </summary>
        ServerErrorVersionNotSupported = 0x0503,

        /// <summary>
        /// server-error-device-error (https://tools.ietf.org/html/RFC8011)
        /// See: RFC 8011
        /// </summary>
        ServerErrorDeviceError = 0x0504,

        /// <summary>
        /// server-error-temporary-error (https://tools.ietf.org/html/RFC8011)
        /// See: RFC 8011
        /// </summary>
        ServerErrorTemporaryError = 0x0505,

        /// <summary>
        /// server-error-not-accepting-jobs (https://tools.ietf.org/html/RFC8011)
        /// See: RFC 8011
        /// </summary>
        ServerErrorNotAcceptingJobs = 0x0506,

        /// <summary>
        /// server-error-busy (https://tools.ietf.org/html/RFC8011)
        /// See: RFC 8011
        /// </summary>
        ServerErrorBusy = 0x0507,

        /// <summary>
        /// server-error-job-canceled (https://tools.ietf.org/html/RFC8011)
        /// See: RFC 8011
        /// </summary>
        ServerErrorJobCanceled = 0x0508,

        /// <summary>
        /// server-error-multiple-document-jobs-not-supported (https://tools.ietf.org/html/RFC8011)
        /// See: RFC 8011
        /// </summary>
        ServerErrorMultipleDocumentJobsNotSupported = 0x0509,

        /// <summary>
        /// server-error-printer-is-deactivated (https://tools.ietf.org/html/RFC3998)
        /// See: RFC 3998 Section 6
        /// </summary>
        ServerErrorPrinterIsDeactivated = 0x050A,

        /// <summary>
        /// server-error-too-many-jobs
        /// See: PWG 5100.7-2023 Section 5.1
        /// </summary>
        ServerErrorTooManyJobs = 0x050B,

        /// <summary>
        /// server-error-too-many-documents
        /// See: PWG 5100.7-2023 Section 5.1
        /// </summary>
        ServerErrorTooManyDocuments = 0x050C,

        /// <summary>
        /// server-error-too-many-printers
        /// See: PWG 5100.22-2025 Section 10.1
        /// </summary>
        ServerErrorTooManyPrinters = 0x050D,
    }
}
