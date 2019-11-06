#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Fraction.cs">
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
    using System.Numerics;

    public sealed class Fraction : Value<Fraction>
    {
        public static Fraction Zero = new Fraction(0, 1);

        public Fraction()
            : this(0, 1)
        {
        }

        public Fraction(BigInteger nominator, BigInteger denominator)
        {
            if (denominator == BigInteger.Zero) throw new ArgumentOutOfRangeException(nameof(denominator));
            this.Nominator = nominator;
            this.Denominator = denominator;
            this.Reduce();
        }

        public BigInteger Denominator { get; private set; }

        public BigInteger Nominator { get; private set; }

        public override Value<Fraction> Clone()
        {
            return new Fraction(this.Nominator, this.Denominator);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Nominator.GetHashCode() * 397) ^ this.Denominator.GetHashCode();
            }
        }

        public override Value<Fraction> Rand(int nominator)
        {
            return new Fraction(nominator, 1 << 16);
        }

        public Fraction Reduce()
        {
            var gcd = BigInteger.GreatestCommonDivisor(this.Nominator, this.Denominator);
            this.Nominator /= gcd;
            this.Denominator /= gcd;
            return this;
        }

        public override string ToString()
        {
            if (this.Nominator == this.Denominator) return "1";
            if (this.Nominator == 0) return "0";
            return $"{this.Nominator}/{this.Denominator}";
        }

        protected override Value<Fraction> Add(Value<Fraction> v2)
        {
            var n = this.Nominator * ((Fraction)v2).Denominator + ((Fraction)v2).Nominator * this.Denominator;
            if (n == BigInteger.Zero) return Zero;
            var d = this.Denominator * ((Fraction)v2).Denominator;
            return new Fraction(n, d).Reduce();
        }

        protected override Value<Fraction> Divide(Value<Fraction> v2)
        {
            var n = this.Nominator * ((Fraction)v2).Denominator;
            if (n == BigInteger.Zero) return Zero;
            var d = this.Denominator * ((Fraction)v2).Nominator;
            return new Fraction(n, d).Reduce();
        }

        protected override bool Equals(Value<Fraction> v2)
        {
            if (ReferenceEquals(null, v2)) return false;
            if (ReferenceEquals(this, v2)) return true;

            return this.Nominator.Equals(((Fraction)v2).Nominator)
                   && this.Denominator.Equals(((Fraction)v2).Denominator);
        }

        protected override Value<Fraction> Multiply(Value<Fraction> v2)
        {
            var n = this.Nominator * ((Fraction)v2).Nominator;
            if (n == BigInteger.Zero) return Zero;
            var d = this.Denominator * ((Fraction)v2).Denominator;
            return new Fraction(n, d).Reduce();
        }

        protected override Value<Fraction> Subtract(Value<Fraction> v2)
        {
            var n = this.Nominator * ((Fraction)v2).Denominator - ((Fraction)v2).Nominator * this.Denominator;
            if (n == BigInteger.Zero) return Zero;
            var d = this.Denominator * ((Fraction)v2).Denominator;
            return new Fraction(n, d).Reduce();
        }
    }
}