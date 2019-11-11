// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FractionTests.cs" company="">
//   
// </copyright>
// <summary>
//   The fraction tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FractionTests.cs">
// Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// krilek@gmail.com
// </copyright>
// <summary>
// 
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

#endregion

namespace GaussianEliminationTests
{
    #region Usings

    using GaussianElimination.Lib;

    using NUnit.Framework;

    #endregion

    /// <summary>
    ///     The fraction tests.
    /// </summary>
    [TestFixture]
    public class FractionTests
    {
        /// <summary>
        ///     The absolute t cs.
        /// </summary>
        private static object[] absoluteTCs =
            {
                new object[] { new Fraction(-1, 1), new Fraction(1, 1) },
                new object[] { new Fraction(-5, 2), new Fraction(5, 2) },
                new object[] { new Fraction(-10, 3), new Fraction(10, 3) },
                new object[] { new Fraction(-2, 4), new Fraction(2, 4) },
                new object[] { new Fraction(5, -2), new Fraction(5, 2) },
            };

        /// <summary>
        ///     The add test cases.
        /// </summary>
        private static object[] addTestCases =
            {
                new object[] { new Fraction(1, 3), new Fraction(1, 3), new Fraction(2, 3) },
                new object[] { new Fraction(-1, 3), new Fraction(1, 3), Fraction.Zero },
                new object[] { new Fraction(1, 3), new Fraction(-1, 3), Fraction.Zero },
                new object[] { new Fraction(), new Fraction(), new Fraction() },
                new object[] { new Fraction(), new Fraction(), Fraction.Zero },
                new object[] { new Fraction(), new Fraction(), new Fraction().SetValue(0) },
                new object[] { new Fraction(), new Fraction(), new Fraction().SetValue(0, 1) },
            };

        /// <summary>
        ///     The equals t cs.
        /// </summary>
        private static object[] equalsTCs =
            {
                new object[] { new Fraction(1, 5), new Fraction(5, 25) },
                new object[] { new Fraction(1, 1), new Fraction(1, 1) },
                new object[] { new Fraction(10, 1), new Fraction(10, 1) },
                new object[] { new Fraction(2, 1), new Fraction(2, 1) },
            };

        /// <summary>
        ///     The less than t cs.
        /// </summary>
        private static object[] lessThanTCs =
            {
                new object[] { new Fraction(1, 4), new Fraction(2, 4) },
                new object[] { new Fraction(1, 2), new Fraction(2, 3) },
            };

        /// <summary>
        /// The absolute test.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        [Test]
        [TestCaseSource(nameof(absoluteTCs))]
        public void AbsoluteTest(Fraction a, Fraction b)
        {
            Assert.True(a.Absolute() == b);
            Assert.True(a != b.Absolute());
        }

        /// <summary>
        /// The add test.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <param name="c">
        /// The c.
        /// </param>
        [Test]
        [TestCaseSource(nameof(addTestCases))]
        public void AddTest(Fraction a, Fraction b, Fraction c)
        {
            Assert.True(c == a + b);
        }

        /// <summary>
        /// The comparison test.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        [Test]
        [TestCaseSource(nameof(lessThanTCs))]
        public void ComparisonTest(Fraction a, Fraction b)
        {
            Assert.True(a < b);
        }

        /// <summary>
        /// The equality test.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        [Test]
        [TestCaseSource(nameof(equalsTCs))]
        public void EqualityTest(Fraction a, Fraction b)
        {
            Assert.True(a == b);
        }
    }
}