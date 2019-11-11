#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MFloat.cs">
// Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// krilek@gmail.com
// </copyright>
// <summary>
//   The float container for generics of matrix calculations
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

#endregion

namespace GaussianElimination.Lib
{
    /// <summary>
    ///   The float container for generics of matrix calculations
    /// </summary>
    public sealed class MFloat : Value<MFloat>
    {
        /// <summary>
        /// The number of digits after floating point that matter for comparison.
        /// </summary>
        private const int equalityParameter = 3;

        /// <summary>
        /// Initializes a new instance of the <see cref="MFloat"/> class.
        /// </summary>
        /// <param name="val">
        /// The val.
        /// </param>
        public MFloat(float val)
        {
            this.Value = val;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MFloat"/> class.
        /// </summary>
        public MFloat()
            : this(0)
        {
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        private float Value { get; }

        /// <summary>
        /// The clone.
        /// </summary>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        public override Value<MFloat> Clone()
        {
            return new MFloat(this.Value);
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
        public override Value<MFloat> SetValue(double nominator, double denominator)
        {
            return new MFloat(((float)nominator) / ((float)denominator));
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
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
        protected override Value<MFloat> Add(Value<MFloat> v2)
        {
            return new MFloat(this.Value + ((MFloat)v2).Value);
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
        protected override int Compare(Value<MFloat> v2)
        {
            return this.Value.CompareTo(((MFloat)v2).Value);
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
        protected override Value<MFloat> Divide(Value<MFloat> v2)
        {
            return new MFloat(this.Value / ((MFloat)v2).Value);
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
        protected override bool Equals(Value<MFloat> v2)
        {
            if (ReferenceEquals(null, v2)) return false;
            if (ReferenceEquals(this, v2)) return true;

            return System.Math.Round(this.Value, equalityParameter)
                   == System.Math.Round(((MFloat)v2).Value, equalityParameter);
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
        protected override Value<MFloat> Multiply(Value<MFloat> v2)
        {
            return new MFloat(this.Value * ((MFloat)v2).Value);
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
        protected override Value<MFloat> Subtract(Value<MFloat> v2)
        {
            return new MFloat(this.Value - ((MFloat)v2).Value);
        }
    }
}