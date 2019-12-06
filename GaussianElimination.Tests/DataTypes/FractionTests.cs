#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FractionTests.cs">
// Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// krilek@gmail.com
// </copyright>
// <summary>
// The fraction tests.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

#endregion

namespace GaussianElimination.Tests
{
    #region Usings

    using GaussianElimination.DataTypes;

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
        /// The divide test.
        /// </summary>
        /// <param name="an">
        /// The a nominator.
        /// </param>
        /// <param name="ad">
        /// The a denominator.
        /// </param>
        /// <param name="bn">
        /// The b nominator.
        /// </param>
        /// <param name="bd">
        /// The b denominator.
        /// </param>
        /// <param name="rn">
        /// The result nominator.
        /// </param>
        /// <param name="rd">
        /// The result denominator.
        /// </param>
        [Test]
        [TestCase(1, 2, 1, 5, 5, 2)]
        [TestCase(-1, 2, -1, 5, 5, 2)]
        [TestCase(-1, 2, 1, 5, -5, 2)]
        [TestCase(5, 2, 1, 5, 25, 2)]
        [TestCase(5, 2, 5, 3, 3, 2)]
        [TestCase(5, 2, 5, 2, 1, 1)]
        [TestCase(1, 3, 1, 5, 5, 3)]
        public void DivideTest(int an, int ad, int bn, int bd, int rn, int rd)
        {
            Assert.True(new Fraction(an, ad) / new Fraction(bn, bd) == new Fraction(rn, rd));
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

        /// <summary>
        /// The multiply test.
        /// </summary>
        /// <param name="an">
        /// The a nominator.
        /// </param>
        /// <param name="ad">
        /// The a denominator.
        /// </param>
        /// <param name="bn">
        /// The b nominator.
        /// </param>
        /// <param name="bd">
        /// The b denominator.
        /// </param>
        /// <param name="rn">
        /// The result nominator.
        /// </param>
        /// <param name="rd">
        /// The result denominator.
        /// </param>
        [Test]
        [TestCase(1, 2, 1, 5, 1, 10)]
        [TestCase(-1, 2, -1, 5, 1, 10)]
        [TestCase(5, 2, 11, 5, 11, 2)]
        [TestCase(5, 2, 1, 1, 5, 2)]
        [TestCase(1, 3, 1, 5, 1, 15)]
        public void MultiplyTest(int an, int ad, int bn, int bd, int rn, int rd)
        {
            Assert.True(new Fraction(an, ad) * new Fraction(bn, bd) == new Fraction(rn, rd));
        }

        /// <summary>
        /// The subtract test.
        /// </summary>
        /// <param name="an">
        /// The a nominator.
        /// </param>
        /// <param name="ad">
        /// The a denominator.
        /// </param>
        /// <param name="bn">
        /// The b nominator.
        /// </param>
        /// <param name="bd">
        /// The b denominator.
        /// </param>
        /// <param name="rn">
        /// The result nominator.
        /// </param>
        /// <param name="rd">
        /// The result denominator.
        /// </param>
        [Test]
        [TestCase(1, 2, 1, 5, 3, 10)]
        [TestCase(-1, 2, -1, 5, -3, 10)]
        [TestCase(5, 2, 1, 5, 23, 10)]
        [TestCase(5, 2, 5, 2, 0, 1)]
        [TestCase(1, 3, 1, 5, 2, 15)]
        public void SubtractTest(int an, int ad, int bn, int bd, int rn, int rd)
        {
            Assert.True(new Fraction(an, ad) - new Fraction(bn, bd) == new Fraction(rn, rd));
        }
    }
}