#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MFloat.cs">
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

    public sealed class MFloat : Value<MFloat>
    {
        private const int equalityParameter = 5;

        public MFloat(float val)
        {
            this.Value = val;
        }

        public MFloat()
            : this(0)
        {
        }

        private float Value { get; }

        public override Value<MFloat> Clone()
        {
            return new MFloat(this.Value);
        }

        public override Value<MFloat> Rand(int nominator)
        {
            return new MFloat(nominator / (float)(1 << 16));
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        protected override Value<MFloat> Add(Value<MFloat> v2)
        {
            return new MFloat(this.Value + ((MFloat)v2).Value);
        }

        protected override Value<MFloat> Divide(Value<MFloat> v2)
        {
            return new MFloat(this.Value / ((MFloat)v2).Value);
        }

        protected override bool Equals(Value<MFloat> v2)
        {
            if (ReferenceEquals(null, v2)) return false;
            if (ReferenceEquals(this, v2)) return true;

            return Math.Round(this.Value, equalityParameter) == Math.Round(((MFloat)v2).Value, equalityParameter);
        }

        protected override Value<MFloat> Multiply(Value<MFloat> v2)
        {
            return new MFloat(this.Value * ((MFloat)v2).Value);
        }

        protected override Value<MFloat> Subtract(Value<MFloat> v2)
        {
            return new MFloat(this.Value - ((MFloat)v2).Value);
        }
    }
}