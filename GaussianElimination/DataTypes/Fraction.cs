﻿#region copyright

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

namespace GaussianElimination.DataTypes
{
    #region Usings

    using System;
    using System.Numerics;

    #endregion

    /// <summary>
    ///     The fraction.
    /// </summary>
    public sealed class Fraction : Value<Fraction>
    {
        /// <summary>
        ///     The zero.
        /// </summary>
        public static Fraction Zero = new Fraction(0, 1);

        /// <summary>
        ///     Initializes a new instance of the <see cref="Fraction" /> class.
        /// </summary>
        public Fraction()
            : this(0, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fraction"/> class.
        /// </summary>
        /// <param name="nominator">
        /// The nominator.
        /// </param>
        /// <param name="denominator">
        /// The denominator.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public Fraction(BigInteger nominator, BigInteger denominator)
        {
            if (denominator == BigInteger.Zero) throw new ArgumentOutOfRangeException(nameof(denominator));
            this.Nominator = nominator;
            this.Denominator = denominator;
            this.MoveSignToNominator();
            this.Reduce();
        }

        /// <summary>
        ///     Gets the denominator.
        /// </summary>
        public BigInteger Denominator { get; private set; }

        /// <summary>
        ///     Gets the nominator.
        /// </summary>
        public BigInteger Nominator { get; private set; }

        /// <summary>
        ///     Clone fraction.
        /// </summary>
        /// <returns>
        ///     The <see cref="Value" />.
        /// </returns>
        public override Value<Fraction> Clone()
        {
            return new Fraction(this.Nominator, this.Denominator);
        }

        /// <summary>
        ///     The get hash code.
        /// </summary>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Nominator.GetHashCode() * 397) ^ this.Denominator.GetHashCode();
            }
        }

        /// <summary>
        ///     Reduce fraction
        /// </summary>
        /// <returns>
        ///     The <see cref="Fraction" />.
        /// </returns>
        public Fraction Reduce()
        {
            var gcd = BigInteger.GreatestCommonDivisor(this.Nominator, this.Denominator);
            this.Nominator /= gcd;
            this.Denominator /= gcd;
            return this;
        }

        /// <summary>
        /// Generic way to create Fraction
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
        public override Value<Fraction> SetValue(double nominator, double denominator)
        {
            this.Nominator = new BigInteger(nominator);
            this.Denominator = new BigInteger(denominator);
            return this;
        }

        /// <summary>
        ///     The to string.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public override string ToString()
        {
            if (this.Nominator == this.Denominator) return "1";
            if (this.Nominator == 0) return "0";
            return $"{this.Nominator}/{this.Denominator}";
        }

        /// <summary>
        /// Add Fractions
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        protected override Value<Fraction> Add(Value<Fraction> v2)
        {
            var n = this.Nominator * ((Fraction)v2).Denominator + ((Fraction)v2).Nominator * this.Denominator;
            if (n == BigInteger.Zero) return Zero;
            var d = this.Denominator * ((Fraction)v2).Denominator;
            return new Fraction(n, d);
        }

        /// <summary>
        /// Comparing functions
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="int"/> 1 if left is bigger, -1 if right is bigger.
        /// </returns>
        protected override int Compare(Value<Fraction> v2)
        {
            var diff = (Fraction)(this - v2);
            if (diff.Nominator > BigInteger.Zero)
            {
                return 1;
            }

            if (diff.Nominator == BigInteger.Zero)
            {
                return 0;
            }

            return -1;
        }

        /// <summary>
        /// Divide fractions.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        protected override Value<Fraction> Divide(Value<Fraction> v2)
        {
            var n = this.Nominator * ((Fraction)v2).Denominator;
            if (n == BigInteger.Zero) return Zero;
            var d = this.Denominator * ((Fraction)v2).Nominator;
            return new Fraction(n, d).MoveSignToNominator();
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
        protected override bool Equals(Value<Fraction> v2)
        {
            if (ReferenceEquals(null, v2)) return false;
            if (ReferenceEquals(this, v2)) return true;

            return this.Nominator.Equals(((Fraction)v2).Nominator)
                   && this.Denominator.Equals(((Fraction)v2).Denominator);
        }

        /// <summary>
        /// Multiply fractions.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        protected override Value<Fraction> Multiply(Value<Fraction> v2)
        {
            var n = this.Nominator * ((Fraction)v2).Nominator;
            if (n == BigInteger.Zero) return Zero;
            var d = this.Denominator * ((Fraction)v2).Denominator;
            return new Fraction(n, d);
        }

        /// <summary>
        /// Subtract fractions.
        /// </summary>
        /// <param name="v2">
        /// The v 2.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        protected override Value<Fraction> Subtract(Value<Fraction> v2)
        {
            // TODO: Fix, there is implementation in comparison
            var n = this.Nominator * ((Fraction)v2).Denominator - ((Fraction)v2).Nominator * this.Denominator;
            if (n == BigInteger.Zero) return Zero;
            var d = this.Denominator * ((Fraction)v2).Denominator;
            return new Fraction(n, d).MoveSignToNominator();
        }

        /// <summary>
        ///     Fix notation of Fraction to store sign in nominator.
        /// </summary>
        /// <returns>
        ///     The <see cref="Fraction" />.
        /// </returns>
        private Fraction MoveSignToNominator()
        {
            if (this.Nominator < 0 && this.Denominator < 0 || this.Nominator > 0 && this.Denominator < 0)
            {
                this.Nominator *= -1;
                this.Denominator *= -1;
            }

            return this;
        }
    }
}