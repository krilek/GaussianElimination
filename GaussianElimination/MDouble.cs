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
using System;

namespace GaussianElimination
{
    public sealed class MDouble : Value<MDouble>
    {
        const int equalityParameter = 5;
        private double Value { get; set; }
        protected override Value<MDouble> Add(Value<MDouble> v2)
        {
            return new MDouble(this.Value + ((MDouble)v2).Value);
        }

        protected override Value<MDouble> Subtract(Value<MDouble> v2)
        {
            return new MDouble(this.Value - ((MDouble)v2).Value);
        }

        protected override Value<MDouble> Divide(Value<MDouble> v2)
        {
            return new MDouble(this.Value / ((MDouble)v2).Value);
        }

        protected override Value<MDouble> Multiply(Value<MDouble> v2)
        {
            return new MDouble(this.Value * ((MDouble)v2).Value);
        }

        protected override bool Equals(Value<MDouble> v2)
        {
            if (ReferenceEquals(null, v2)) return false;
            if (ReferenceEquals(this, v2)) return true;
            return Math.Round(this.Value, equalityParameter) == Math.Round(((MDouble)v2).Value, equalityParameter);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override Value<MDouble> Rand(int nominator)
        {
            return new MDouble(nominator / (double)(1 << 16));
        }

        public MDouble(double val)
        {
            Value = val;
        }
        public MDouble() : this(0) { }
    }
}
