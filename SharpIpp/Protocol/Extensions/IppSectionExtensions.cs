using System.Collections.Generic;
using System.Linq;

using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IppSection"/>.
    /// </summary>
    public static class IppSectionExtensions
    {
        /// <summary>
        /// Gets all attributes from the IPP section as a dictionary.
        /// </summary>
        /// <param name="ippSection">The IPP section.</param>
        /// <returns>A dictionary containing all attributes grouped by name.</returns>
        public static IDictionary<string, IppAttribute[]> AllAttributes(this IppSection ippSection)
        {
            return ippSection.Attributes.GroupBy(x => x.Name).ToDictionary(g => g.Key, g => g.ToArray());
        }
    }
}
