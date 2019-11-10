// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Math.cs" company="">
//   Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// </copyright>
// <summary>
//   Defines the Math type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TaylorTheorem
{
    using System;


    /// <summary>
    ///     The helper methods for calculation.
    /// </summary>
    internal class Math
    {
        /// <summary>
        ///     Calc Absolute val of x.
        /// </summary>
        /// <param name="x">
        ///     The x.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double Abs(double x)
        {
            return x > 0 ? x : x * -1;
        }

        public static double CalculateError(double preciseValue, double calculatedValue)
        {
            return Abs(preciseValue - calculatedValue);
        }

        public static double CalculateRelativeError(double preciseValue, double calculatedValue)
        {
            return (preciseValue - calculatedValue) / preciseValue;
        }

        public static double CalculateRelativeErrorAbs(double preciseValue, double calculatedValue)
        {
            return Abs((preciseValue - calculatedValue) / preciseValue);
        }

        /// <summary>
        ///     The factorial calculator.
        /// </summary>
        /// <param name="n">
        ///     The n.
        /// </param>
        /// <returns>
        ///     The <see cref="long" />.
        /// </returns>
        public static double Factorial(double n)
        {
            if (n == 0) return 1;
            return n * Factorial(n - 1);
        }

        /// <summary>
        ///     Calculate power of x^n.
        /// </summary>
        /// <param name="x">
        ///     The x.
        /// </param>
        /// <param name="n">
        ///     The n.
        /// </param>
        /// <returns>
        ///     The <see cref="double" />.
        /// </returns>
        public static double Power(double x, double n)
        {
            double a = 1;
            for (long i = 0; i < n; i++) a *= x;

            return a;
        }

    }
}