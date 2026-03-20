namespace SharpIpp.Protocol.Models
{
    /// <summary>
    /// PWG 5100.22-2025 - IPP System Service attribute name constants.
    /// </summary>
    public static class SystemAttribute
    {
        /// <summary>
        /// The URI of the target System object.
        /// See: PWG 5100.22-2025 Section 7.1.26
        /// </summary>
        public const string SystemUri = "system-uri";

        /// <summary>
        /// The current state of the System object.
        /// See: PWG 5100.22-2025 Section 7.3.26
        /// </summary>
        public const string SystemState = "system-state";

        /// <summary>
        /// The identifier for a resource (singular).
        /// See: PWG 5100.22-2025 Section 7.9.6
        /// </summary>
        public const string ResourceId = "resource-id";

        /// <summary>
        /// The list of resource identifiers (operation attribute).
        /// See: PWG 5100.22-2025 Section 7.1.15
        /// </summary>
        public const string ResourceIds = "resource-ids";
    }
}
