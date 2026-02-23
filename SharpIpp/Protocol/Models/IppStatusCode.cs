namespace SharpIpp.Protocol.Models
{
    /// <summary>
    ///     https://www.iana.org/assignments/ipp-registrations/ipp-registrations.xhtml#ipp-registrations-11
    /// </summary>
    public enum IppStatusCode : short
    {
        /// <summary>
        ///     successful-ok (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        SuccessfulOk = 0x0000,

        /// <summary>
        ///     successful-ok-ignored-or-substituted-attributes (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        SuccessfulOkIgnoredOrSubstitutedAttributes = 0x0001,

        /// <summary>
        ///     successful-ok-conflicting-attributes (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        SuccessfulOkConflictingAttributes = 0x0002,

        /// <summary>
        ///     successful-ok-ignored-subscriptions (https://tools.ietf.org/html/RFC3995)
        /// </summary>
        SuccessfulOkIgnoredSubscriptions = 0x0003,

        /// <summary>
        ///     successful-ok-too-many-events (https://tools.ietf.org/html/RFC3995)
        /// </summary>
        SuccessfulOkTooManyEvents = 0x0005,

        /// <summary>
        ///     successful-ok-events-complete (https://tools.ietf.org/html/RFC3996)
        /// </summary>
        SuccessfulOkEventsComplete = 0x0007,

        /// <summary>
        ///     client-error-bad-request (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorBadRequest = 0x0400,

        /// <summary>
        ///     client-error-forbidden (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorForbidden = 0x0401,

        /// <summary>
        ///     client-error-not-authenticated (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorNotAuthenticated = 0x0402,

        /// <summary>
        ///     client-error-not-authorized (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorNotAuthorized = 0x0403,

        /// <summary>
        ///     client-error-not-possible (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorNotPossible = 0x0404,

        /// <summary>
        ///     client-error-timeout (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorTimeout = 0x0405,

        /// <summary>
        ///     client-error-not-found (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorNotFound = 0x0406,

        /// <summary>
        ///     client-error-gone (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorGone = 0x0407,

        /// <summary>
        ///     client-error-request-entity-too-large (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorRequestEntityTooLarge = 0x0408,

        /// <summary>
        ///     client-error-request-value-too-long (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorRequestValueTooLong = 0x0409,

        /// <summary>
        ///     client-error-document-format-not-supported (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorDocumentFormatNotSupported = 0x040A,

        /// <summary>
        ///     client-error-attributes-or-values-not-supported (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorAttributesOrValuesNotSupported = 0x040B,

        /// <summary>
        ///     client-error-uri-scheme-not-supported (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorUriSchemeNotSupported = 0x040C,

        /// <summary>
        ///     client-error-charset-not-supported (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorCharsetNotSupported = 0x040D,

        /// <summary>
        ///     client-error-conflicting-attributes (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorConflictingAttributes = 0x040E,

        /// <summary>
        ///     client-error-compression-not-supported (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorCompressionNotSupported = 0x040F,

        /// <summary>
        ///     client-error-compression-error (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorCompressionError = 0x0410,

        /// <summary>
        ///     client-error-document-format-error (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorDocumentFormatError = 0x0411,

        /// <summary>
        ///     client-error-document-access-error (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ClientErrorDocumentAccessError = 0x0412,

        /// <summary>
        ///     client-error-attributes-not-settable (https://tools.ietf.org/html/RFC3380)
        /// </summary>
        ClientErrorAttributesNotSettable = 0x0413,

        /// <summary>
        ///     client-error-ignored-all-subscriptions (https://tools.ietf.org/html/RFC3995)
        /// </summary>
        ClientErrorIgnoredAllSubscriptions = 0x0414,

        /// <summary>
        ///     client-error-too-many-subscriptions (https://tools.ietf.org/html/RFC3995)
        /// </summary>
        ClientErrorTooManySubscriptions = 0x0415,

        /// <summary>
        ///     client-error-document-password-error (https://tools.ietf.org/html/PWG510013])
        /// </summary>
        ClientErrorDocumentPasswordError = 0x0418,

        /// <summary>
        ///     client-error-document-permission-error (https://tools.ietf.org/html/PWG510013])
        /// </summary>
        ClientErrorDocumentPermissionError = 0x0419,

        /// <summary>
        ///     client-error-document-security-error (https://tools.ietf.org/html/PWG510013])
        /// </summary>
        ClientErrorDocumentSecurityError = 0x041A,

        /// <summary>
        ///     client-error-document-unprintable-error (https://tools.ietf.org/html/PWG510013])
        /// </summary>
        ClientErrorDocumentUnprintableError = 0x041B,

        /// <summary>
        ///     client-error-account-info-needed
        /// </summary>
        ClientErrorAccountInfoNeeded = 0x041C,

        /// <summary>
        ///     client-error-account-closed (https://tools.ietf.org/html/PWG510016])
        /// </summary>
        ClientErrorAccountClosed = 0x041D,

        /// <summary>
        ///     client-error-account-limit-reached (https://tools.ietf.org/html/PWG510016])
        /// </summary>
        ClientErrorAccountLimitReached = 0x041E,

        /// <summary>
        ///     client-error-account-authorization-failed (https://tools.ietf.org/html/PWG510016])
        /// </summary>
        ClientErrorAccountAuthorizationFailed = 0x041F,

        /// <summary>
        ///     client-error-not-fetchable (https://tools.ietf.org/html/PWG510018])
        /// </summary>
        ClientErrorNotFetchable = 0x0420,

        /// <summary>
        ///     server-error-internal-error (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ServerErrorInternalError = 0x0500,

        /// <summary>
        ///     server-error-operation-not-supported (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ServerErrorOperationNotSupported = 0x0501,

        /// <summary>
        ///     server-error-service-unavailable (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ServerErrorServiceUnavailable = 0x0502,

        /// <summary>
        ///     server-error-version-not-supported (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ServerErrorVersionNotSupported = 0x0503,

        /// <summary>
        ///     server-error-device-error (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ServerErrorDeviceError = 0x0504,

        /// <summary>
        ///     server-error-temporary-error (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ServerErrorTemporaryError = 0x0505,

        /// <summary>
        ///     server-error-not-accepting-jobs (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ServerErrorNotAcceptingJobs = 0x0506,

        /// <summary>
        ///     server-error-busy (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ServerErrorBusy = 0x0507,

        /// <summary>
        ///     server-error-job-canceled (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ServerErrorJobCanceled = 0x0508,

        /// <summary>
        ///     server-error-multiple-document-jobs-not-supported (https://tools.ietf.org/html/RFC8011)
        /// </summary>
        ServerErrorMultipleDocumentJobsNotSupported = 0x0509,

        /// <summary>
        ///     server-error-printer-is-deactivated (https://tools.ietf.org/html/RFC3998)
        /// </summary>
        ServerErrorPrinterIsDeactivated = 0x050A,

        /// <summary>
        ///     server-error-too-many-jobs (https://tools.ietf.org/html/PWG51007])
        /// </summary>
        ServerErrorTooManyJobs = 0x050B,

        /// <summary>
        ///     server-error-too-many-documents (https://tools.ietf.org/html/PWG51007)
        /// </summary>
        ServerErrorTooManyDocuments = 0x050C,
    }
}
