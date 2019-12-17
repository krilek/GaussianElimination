#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MDouble.cs">
// Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// krilek@gmail.com
// </copyright>
// <summary>
//   The double container for generics of matrix calculations.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

#endregion

namespace GaussianElimination.DataTypes
{
    /// <summary>
    ///     The double container for generics of matrix calculations
    /// </summary>
    public sealed class MDouble : Value<MDouble>
    {
        /// <summary>
        ///     The number of digits after floating point that matter for comparison.
        /// </summary>
        private const int equalityParameter = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="MDouble"/> class.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        public MDouble(double val)
        {
            this.Value = val;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MDouble" /> class.
        /// </summary>
        public MDouble()
            : this(0)
        {
        }

        /// <summary>
        ///     Gets the value.
        /// </summary>
        public double Value { get; }

        /// <summary>
        ///     The clone.
        /// </summary>
        /// <returns>
        ///     The <see cref="Value" />.
        /// </returns>
        public override Value<MDouble> Clone()
        {
            return new MDouble(this.Value);
        }

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
        public override Value<MDouble> SetValue(double nominator, double denominator)
        {
            return new MDouble(nominator / denominator);
        }

        /// <summary>
        ///     The to string.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public override string ToString()
        {
            return this.Value.ToString();
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
        protected override Value<MDouble> Add(Value<MDouble> v2)
        {
            return new MDouble(this.Value + ((MDouble)v2).Value);
        }

        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected override int Compare(Value<MDouble> v2)
        {
            return this.Value.CompareTo(((MDouble)v2).Value);
        }

        /// <summary>
        /// The divide.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        protected override Value<MDouble> Divide(Value<MDouble> v2)
        {
            return new MDouble(this.Value / ((MDouble)v2).Value);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool Equals(Value<MDouble> v2)
        {
            if (ReferenceEquals(null, v2)) return false;
            if (ReferenceEquals(this, v2)) return true;
            return System.Math.Round(this.Value, equalityParameter)
                   == System.Math.Round(((MDouble)v2).Value, equalityParameter);
        }

        /// <summary>
        /// The multiply.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        protected override Value<MDouble> Multiply(Value<MDouble> v2)
        {
            return new MDouble(this.Value * ((MDouble)v2).Value);
        }

        /// <summary>
        /// The subtract.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        protected override Value<MDouble> Subtract(Value<MDouble> v2)
        {
            return new MDouble(this.Value - ((MDouble)v2).Value);
        }
    }
}