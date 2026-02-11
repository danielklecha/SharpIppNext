using System.Collections.Generic;

using SharpIpp.Protocol.Models;

namespace SharpIpp.Protocol.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="List{T}"/> of <see cref="IppAttribute"/>.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Populates the list with attributes from another collection.
        /// If an attribute with the same name already exists in the list, it is replaced.
        /// </summary>
        /// <param name="list">The target list.</param>
        /// <param name="other">The collection containing additional attributes.</param>
        public static void Populate(this List<IppAttribute> list, IEnumerable<IppAttribute>? other)
        {
            if (other == null)
            {
                return;
            }

            foreach (var additionalAttribute in other)
            {
                list.RemoveAll(x => !string.IsNullOrEmpty(x.Name) && x.Name == additionalAttribute.Name);
                list.Add(additionalAttribute);
            }
        }
    }
}
