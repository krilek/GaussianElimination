#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MDoubleTests.cs">
// Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// krilek@gmail.com
// </copyright>
// <summary>
// 
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
    ///     The m double tests.
    /// </summary>
    [TestFixture]
    public class MDoubleTests
    {
        /// <summary>
        ///     The absolute t cs.
        /// </summary>
        private static object[] absoluteTCs =
            {
                new object[] { new MDouble().SetValue(-1, 1), new MDouble().SetValue(1, 1) },
                new object[] { new MDouble().SetValue(-5, 2), new MDouble().SetValue(5, 2) },
                new object[] { new MDouble().SetValue(-10, 3), new MDouble().SetValue(10, 3) },
                new object[] { new MDouble().SetValue(-2, 4), new MDouble().SetValue(2, 4) },
                new object[] { new MDouble().SetValue(5, -2), new MDouble().SetValue(5, 2) },
            };

        /// <summary>
        ///     The add test cases.
        /// </summary>
        private static object[] addTestCases =
            {
                new object[]
                    {
                        new MDouble().SetValue(1, 3), new MDouble().SetValue(1, 3), new MDouble().SetValue(2, 3)
                    },
                new object[] { new MDouble().SetValue(-1, 3), new MDouble().SetValue(1, 3), new MDouble() },
                new object[] { new MDouble().SetValue(1, 3), new MDouble().SetValue(-1, 3), new MDouble() },
                new object[] { new MDouble(), new MDouble(), new MDouble() },
                new object[] { new MDouble(), new MDouble(), new MDouble().SetValue(0) },
                new object[] { new MDouble(), new MDouble(), new MDouble().SetValue(0, 1) },
                new object[] { new MDouble(5.0), new MDouble(5.2), new MDouble(5.2 + 5.0) },
                new object[] { new MDouble(-5.0), new MDouble(5.2), new MDouble(-5.0 + 5.2) },
                new object[] { new MDouble(5.0), new MDouble(-5.2), new MDouble(5.0 + -5.2) },
            };

        /// <summary>
        ///     The equals t cs.
        /// </summary>
        private static object[] equalsTCs =
            {
                new object[] { new MDouble().SetValue(1, 5), new MDouble().SetValue(5, 25) },
                new object[] { new MDouble().SetValue(1, 1), new MDouble().SetValue(1, 1) },
                new object[] { new MDouble().SetValue(10, 1), new MDouble().SetValue(10, 1) },
                new object[] { new MDouble().SetValue(2, 1), new MDouble().SetValue(2, 1) },
            };

        /// <summary>
        ///     The less than t cs.
        /// </summary>
        private static object[] lessThanTCs =
            {
                new object[] { new MDouble().SetValue(1, 4), new MDouble().SetValue(2, 4) },
                new object[] { new MDouble().SetValue(1, 2), new MDouble().SetValue(2, 3) },
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
        public void AbsoluteTest(MDouble a, MDouble b)
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
        public void AddTest(MDouble a, MDouble b, MDouble c)
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
        public void ComparisonTest(MDouble a, MDouble b)
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
        public void EqualityTest(MDouble a, MDouble b)
        {
            Assert.True(a == b);
        }
    }
}