using System.Linq;
using TomKerkhove.Connectors.ApplicationInsights;

// ReSharper disable once CheckNamespace - Improves discoverability
namespace System.Collections.Generic
{
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Copies over a dictionary into another one
        /// </summary>
        /// <typeparam name="TKey">Type of the key</typeparam>
        /// <typeparam name="TValue">Type of the values</typeparam>
        /// <param name="destination">Dictionary that will contain all entries from both dictionary</param>
        /// <param name="source">Dictionary to copy over</param>
        /// <exception cref="InvalidOperationException">Exception thrown when key from source dictionary already exists in the destionation dictionary</exception>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> destination, IDictionary<TKey, TValue> source)
        {
            Guard.AgainstNull(destination, nameof(destination));

            if (source == null || source.Any() == false)
            {
                return;
            }

            foreach (var entry in source)
            {
                if (destination.ContainsKey(entry.Key))
                {
                    throw new InvalidOperationException($"Key '{entry.Key}' already exists in destination dictionary");
                }

                destination.Add(entry);
            }
        }
    }
}