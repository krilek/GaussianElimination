// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValueUtilities.cs">
// Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// krilek@gmail.com
// </copyright>
// <summary>
// 
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace GaussianElimination.DataTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The value utilities.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public static class ValueUtilities<T> where T : Value<T>, new()
    {
        /// <summary>
        /// Generate random array of Value<T>
        /// </summary>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <param name="sumUpTo">
        /// The sum up to.
        /// </param>
        /// <param name="seed">
        /// The seed.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<Value<T>> RandomNormalizedArray(int amount, Value<T> sumUpTo, int? seed = null)
        {
            if (seed == null)
            {
                seed = Guid.NewGuid().GetHashCode();
            }
            var rnd = new Random(seed.Value);
            List<Value<T>> rand = new List<Value<T>>(amount);
            Value<T> sum = new T();
            for (int i = 0; i < amount; i++)
            {
                Value<T> value = new T().SetValue(rnd.Next(), 1);
                rand.Add(value);
                sum += value;
            }

            return rand.Select(x => (x / (sum * sumUpTo)));

        }
    }
}