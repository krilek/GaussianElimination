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
        public static Fraction Zero = new Fraction(0, 0);
        public BigInteger Nominator { get; }

        public BigInteger Denominator { get; }

        public override string ToString()
        {
            return $"{this.Nominator}/{this.Denominator}";
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
            return Equals((Fraction)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Nominator.GetHashCode() * 397) ^ this.Denominator.GetHashCode();
            }
        }

        public static bool operator ==(Fraction left, Fraction right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Fraction left, Fraction right)
        {
            return !Equals(left, right);
        }

        public Fraction(BigInteger nominator, BigInteger denominator)
        {
            this.Nominator = nominator;
            this.Denominator = denominator;
        }

        public static Fraction operator -(Fraction v1, Fraction v2)
        {
            var n = (v1.Nominator * v2.Denominator) - (v2.Nominator * v1.Denominator);
            if (n == BigInteger.Zero)
            {
                return Fraction.Zero;
            }
            var d = v1.Denominator * v2.Denominator;
            var gcd = BigInteger.GreatestCommonDivisor(n, d);
            return new Fraction(n / gcd, d / gcd);
        }

        public static Fraction operator +(Fraction v1, Fraction v2)
        {
            var n = (v1.Nominator * v2.Denominator) + (v2.Nominator * v1.Denominator);
            if (n == BigInteger.Zero)
            {
                return Fraction.Zero;
            }
            var d = v1.Denominator * v2.Denominator;
            var gcd = BigInteger.GreatestCommonDivisor(n, d);
            return new Fraction(n / gcd, d / gcd);
        }

        public static Fraction operator *(Fraction v1, Fraction v2)
        {
            var n = v1.Nominator * v2.Nominator;
            if (n == BigInteger.Zero)
            {
                return Fraction.Zero;
            }
            var d = v1.Denominator * v2.Denominator;
            var gcd = BigInteger.GreatestCommonDivisor(n, d);
            return new Fraction(n / gcd, d / gcd);
        }

        public static Fraction operator /(Fraction v1, Fraction v2)
        {
            var n = v1.Nominator * v2.Denominator;
            if (n == BigInteger.Zero)
            {
                return Fraction.Zero;
            }
            var d = v1.Denominator * v2.Nominator;
            var gcd = BigInteger.GreatestCommonDivisor(n, d);
            return new Fraction(n / gcd, d / gcd);
        }
    }
}