#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Value.cs">
// Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// krilek@gmail.com
// </copyright>
// <summary>
// The value container.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

#endregion

namespace GaussianElimination.DataTypes
{
    /// <summary>
    /// The value container.
    /// </summary>
    /// <typeparam name="T">
    /// Implementations of Value
    /// </typeparam>
    public abstract class Value<T>
        where T : Value<T>, new()
    {
        /// <summary>
        ///     The +.
        /// </summary>
        /// <param name="v1">
        ///     The v 1.
        /// </param>
        /// <param name="v2">
        ///     The v 2.
        /// </param>
        /// <returns>
        ///     Added value
        /// </returns>
        public static Value<T> operator +(Value<T> v1, Value<T> v2)
        {
            return v1.Add(v2);
        }

        /// <summary>
        ///     The /.
        /// </summary>
        /// <param name="v1">
        ///     The v 1.
        /// </param>
        /// <param name="v2">
        ///     The v 2.
        /// </param>
        /// <returns>
        ///     Divided value
        /// </returns>
        public static Value<T> operator /(Value<T> v1, Value<T> v2)
        {
            return v1.Divide(v2);
        }

        /// <summary>
        ///     The ==.
        /// </summary>
        /// <param name="v1">
        ///     The v 1.
        /// </param>
        /// <param name="v2">
        ///     The v 2.
        /// </param>
        /// <returns>
        ///     True if equal
        /// </returns>
        public static bool operator ==(Value<T> v1, Value<T> v2)
        {
            return v1.Equals(v2);
        }

        /// <summary>
        ///     The &gt;.
        /// </summary>
        /// <param name="v1">
        ///     The v 1.
        /// </param>
        /// <param name="v2">
        ///     The v 2.
        /// </param>
        /// <returns>
        ///     True if
        ///     <param name="v1"></param>
        ///     bigger than
        ///     <param name="v2"></param>
        ///     .
        /// </returns>
        public static bool operator >(Value<T> v1, Value<T> v2)
        {
            return v1.Compare(v2) == 1;
        }

        /// <summary>
        ///     The !=.
        /// </summary>
        /// <param name="v1">
        ///     The v 1.
        /// </param>
        /// <param name="v2">
        ///     The v 2.
        /// </param>
        /// <returns>
        ///     True if
        ///     <param name="v1"></param>
        ///     not equal equal
        ///     <param name="v2"></param>
        ///     .
        /// </returns>
        public static bool operator !=(Value<T> v1, Value<T> v2)
        {
            return !v1.Equals(v2);
        }

        /// <summary>
        ///     The &lt;.
        /// </summary>
        /// <param name="v1">
        ///     The v 1.
        /// </param>
        /// <param name="v2">
        ///     The v 2.
        /// </param>
        /// <returns>
        ///     True if
        ///     <param name="v1"></param>
        ///     smaller than
        ///     <param name="v2"></param>
        ///     .
        /// </returns>
        public static bool operator <(Value<T> v1, Value<T> v2)
        {
            return v1.Compare(v2) == -1;
        }

        /// <summary>
        ///     The *.
        /// </summary>
        /// <param name="v1">
        ///     The v 1.
        /// </param>
        /// <param name="v2">
        ///     The v 2.
        /// </param>
        /// <returns>
        ///     Multiplied value
        /// </returns>
        public static Value<T> operator *(Value<T> v1, Value<T> v2)
        {
            return v1.Multiply(v2);
        }

        /// <summary>
        ///     The -.
        /// </summary>
        /// <param name="v1">
        ///     The v 1.
        /// </param>
        /// <param name="v2">
        ///     The v 2.
        /// </param>
        /// <returns>
        ///     Subtracted value
        /// </returns>
        public static Value<T> operator -(Value<T> v1, Value<T> v2)
        {
            return v1.Subtract(v2);
        }

        /// <summary>
        ///     Return change given value to absolute.
        /// </summary>
        /// <returns>
        ///     The <see cref="Value" />.
        /// </returns>
        public Value<T> Absolute()
        {
            if (this > new T())
            {
                return this;
            }

            return this * new T().SetValue(-1, 1);
        }

        /// <summary>
        ///     Clone given value
        /// </summary>
        /// <returns>
        ///     The <see cref="Value" />.
        /// </returns>
        public abstract Value<T> Clone();

        /// <summary>
        /// The set value.
        /// </summary>
        /// <param name="nominator">
        /// The nominator.
        /// </param>
        /// <param name="denominator">
        /// The denominator.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        public abstract Value<T> SetValue(double nominator, double denominator);

        /// <summary>
        /// The set value.
        /// </summary>
        /// <param name="nominator">
        /// The nominator.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        public Value<T> SetValue(double nominator)
        {
            return this.SetValue(nominator, 1);
        }

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
    }
}