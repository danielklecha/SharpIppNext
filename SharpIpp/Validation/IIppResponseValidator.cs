using SharpIpp.Protocol;

namespace SharpIpp.Validation
{
    /// <summary>
    /// Defines a validator for high-level IPP responses using property attributes.
    /// </summary>
    public interface IIppResponseValidator
    {
        /// <summary>
        /// Validates the high-level response object recursively.
        /// </summary>
        /// <typeparam name="T">The response type.</typeparam>
        /// <param name="response">The response to validate.</param>
        void Validate<T>(T response) where T : IIppResponse;
    }
}
