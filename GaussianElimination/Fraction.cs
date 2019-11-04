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

    public class Fraction : IEquatable<Fraction>
    {
        public static Fraction Zero = new Fraction(0, 1);
        public Fraction() : this(0, 1) { }
        public Fraction(BigInteger nominator, BigInteger denominator)
        {
            if (denominator == BigInteger.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(denominator));
            }
            this.Nominator = nominator;
            this.Denominator = denominator;
            this.Reduce();
        }
        public Fraction Reduce()
        {
            var gcd = BigInteger.GreatestCommonDivisor(Nominator, Denominator);
            this.Nominator /= gcd;
            this.Denominator /= gcd;
            return this;
        }
        public BigInteger Denominator { get; private set; }

        public BigInteger Nominator { get; private set; }

        public static Fraction operator +(Fraction v1, Fraction v2)
        {
            var n = v1.Nominator * v2.Denominator + v2.Nominator * v1.Denominator;
            if (n == BigInteger.Zero) return Zero;
            var d = v1.Denominator * v2.Denominator;
            return new Fraction(n, d).Reduce(); 
        }

        public static Fraction operator /(Fraction v1, Fraction v2)
        {
            var n = v1.Nominator * v2.Denominator;
            if (n == BigInteger.Zero) return Zero;
            var d = v1.Denominator * v2.Nominator;
            return new Fraction(n, d).Reduce();
        }

        public static bool operator ==(Fraction left, Fraction right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Fraction left, Fraction right)
        {
            return !Equals(left, right);
        }

        public static Fraction operator *(Fraction v1, Fraction v2)
        {
            var n = v1.Nominator * v2.Nominator;
            if (n == BigInteger.Zero) return Zero;
            var d = v1.Denominator * v2.Denominator;
            return new Fraction(n, d).Reduce();
        }

        public static Fraction operator -(Fraction v1, Fraction v2)
        {
            var n = v1.Nominator * v2.Denominator - v2.Nominator * v1.Denominator;
            if (n == BigInteger.Zero) return Zero;
            var d = v1.Denominator * v2.Denominator;
            return new Fraction(n, d).Reduce();
        }

        public bool Equals(Fraction other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.Nominator.Equals(other.Nominator) && this.Denominator.Equals(other.Denominator);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Fraction)) return false;
            return this.Equals((Fraction)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Nominator.GetHashCode() * 397) ^ this.Denominator.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"{this.Nominator}/{this.Denominator}";
        }
    }
}