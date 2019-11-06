#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MDouble.cs">
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
    using System;

    public sealed class MDouble : Value<MDouble>
    {
        private const int equalityParameter = 5;

        public MDouble(double val)
        {
            this.Value = val;
        }

        public MDouble()
            : this(0)
        {
        }

        private double Value { get; }

        public override Value<MDouble> Clone()
        {
            return new MDouble(this.Value);
        }

        public override Value<MDouble> Rand(int nominator)
        {
            return new MDouble(nominator / (double)(1 << 16));
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        protected override Value<MDouble> Add(Value<MDouble> v2)
        {
            return new MDouble(this.Value + ((MDouble)v2).Value);
        }

        protected override Value<MDouble> Divide(Value<MDouble> v2)
        {
            return new MDouble(this.Value / ((MDouble)v2).Value);
        }

        protected override bool Equals(Value<MDouble> v2)
        {
            if (ReferenceEquals(null, v2)) return false;
            if (ReferenceEquals(this, v2)) return true;
            return Math.Round(this.Value, equalityParameter) == Math.Round(((MDouble)v2).Value, equalityParameter);
        }

        protected override Value<MDouble> Multiply(Value<MDouble> v2)
        {
            return new MDouble(this.Value * ((MDouble)v2).Value);
        }

        protected override Value<MDouble> Subtract(Value<MDouble> v2)
        {
            return new MDouble(this.Value - ((MDouble)v2).Value);
        }
    }
}