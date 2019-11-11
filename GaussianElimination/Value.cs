#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Value.cs">
// Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// krilek@gmail.com
// </copyright>
// <summary>
// 
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

#endregion

namespace GaussianElimination
{
    /// <summary>
    /// The value.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract class Value<T>
        where T : Value<T>, new()
    {
        /// <summary>
        /// The +.
        /// </summary>
        /// <param name="v1">
        /// The v 1.
        /// </param>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// </returns>
        public static Value<T> operator +(Value<T> v1, Value<T> v2)
        {
            return v1.Add(v2);
        }

        /// <summary>
        /// The /.
        /// </summary>
        /// <param name="v1">
        /// The v 1.
        /// </param>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// </returns>
        public static Value<T> operator /(Value<T> v1, Value<T> v2)
        {
            return v1.Divide(v2);
        }

        /// <summary>
        /// The ==.
        /// </summary>
        /// <param name="v1">
        /// The v 1.
        /// </param>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator ==(Value<T> v1, Value<T> v2)
        {
            return v1.Equals(v2);
        }

        /// <summary>
        /// The &gt;.
        /// </summary>
        /// <param name="v1">
        /// The v 1.
        /// </param>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator >(Value<T> v1, Value<T> v2)
        {
            return v1.Compare(v2) == 1;
        }

        /// <summary>
        /// The !=.
        /// </summary>
        /// <param name="v1">
        /// The v 1.
        /// </param>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator !=(Value<T> v1, Value<T> v2)
        {
            return !v1.Equals(v2);
        }

        /// <summary>
        /// The &lt;.
        /// </summary>
        /// <param name="v1">
        /// The v 1.
        /// </param>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator <(Value<T> v1, Value<T> v2)
        {
            return v1.Compare(v2) == -1;
        }

        /// <summary>
        /// The *.
        /// </summary>
        /// <param name="v1">
        /// The v 1.
        /// </param>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// </returns>
        public static Value<T> operator *(Value<T> v1, Value<T> v2)
        {
            return v1.Multiply(v2);
        }

        /// <summary>
        /// The -.
        /// </summary>
        /// <param name="v1">
        /// The v 1.
        /// </param>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// </returns>
        public static Value<T> operator -(Value<T> v1, Value<T> v2)
        {
            return v1.Subtract(v2);
        }

        /// <summary>
        /// The clone.
        /// </summary>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        public abstract Value<T> Clone();

        /// <summary>
        /// The rand.
        /// </summary>
        /// <param name="nominator">
        /// The nominator.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        public abstract Value<T> Rand(int nominator);

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        protected abstract Value<T> Add(Value<T> v2);

        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected abstract int Compare(Value<T> v2);

        /// <summary>
        /// The divide.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        protected abstract Value<T> Divide(Value<T> v2);

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected abstract bool Equals(Value<T> v2);

        /// <summary>
        /// The multiply.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        protected abstract Value<T> Multiply(Value<T> v2);

        /// <summary>
        /// The subtract.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        protected abstract Value<T> Subtract(Value<T> v2);

        public abstract Value<T> SetValue(int nominator, int denominator);

        public Value<T> Absolute()
        {
            if (this > new T())
            {
                return this;
            }

            return this * new T().SetValue(-1, 1);
        }
    }
}