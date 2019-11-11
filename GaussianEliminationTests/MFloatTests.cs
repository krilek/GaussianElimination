// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MFloatTests.cs" company="">
//   
// </copyright>
// <summary>
//   The m double tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MFloatTests.cs">
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
    ///     The m double tests.
    /// </summary>
    [TestFixture]
    public class MFloatTests
    {
        /// <summary>
        ///     The absolute t cs.
        /// </summary>
        private static object[] absoluteTCs =
            {
                new object[] { new MFloat().SetValue(-1, 1), new MFloat().SetValue(1, 1) },
                new object[] { new MFloat().SetValue(-5, 2), new MFloat().SetValue(5, 2) },
                new object[] { new MFloat().SetValue(-10, 3), new MFloat().SetValue(10, 3) },
                new object[] { new MFloat().SetValue(-2, 4), new MFloat().SetValue(2, 4) },
                new object[] { new MFloat().SetValue(5, -2), new MFloat().SetValue(5, 2) },
            };

        /// <summary>
        ///     The add test cases.
        /// </summary>
        private static object[] addTestCases =
            {
                new object[] { new MFloat().SetValue(1, 3), new MFloat().SetValue(1, 3), new MFloat().SetValue(2, 3) },
                new object[] { new MFloat().SetValue(-1, 3), new MFloat().SetValue(1, 3), new MFloat() },
                new object[] { new MFloat().SetValue(1, 3), new MFloat().SetValue(-1, 3), new MFloat() },
                new object[] { new MFloat(), new MFloat(), new MFloat() },
                new object[] { new MFloat(), new MFloat(), new MFloat().SetValue(0) },
                new object[] { new MFloat(), new MFloat(), new MFloat().SetValue(0, 1) },
                new object[] { new MFloat(5.0f), new MFloat(5.2f), new MFloat(5.2f + 5.0f) },
                new object[] { new MFloat(-5.0f), new MFloat(5.2f), new MFloat(-5.0f + 5.2f) },
                new object[] { new MFloat(5.0f), new MFloat(-5.2f), new MFloat(5.0f + -5.2f) },
            };

        /// <summary>
        ///     The equals t cs.
        /// </summary>
        private static object[] equalsTCs =
            {
                new object[] { new MFloat().SetValue(1, 5), new MFloat().SetValue(5, 25) },
                new object[] { new MFloat().SetValue(1, 1), new MFloat().SetValue(1, 1) },
                new object[] { new MFloat().SetValue(10, 1), new MFloat().SetValue(10, 1) },
                new object[] { new MFloat().SetValue(2, 1), new MFloat().SetValue(2, 1) },
            };

        /// <summary>
        ///     The less than t cs.
        /// </summary>
        private static object[] lessThanTCs =
            {
                new object[] { new MFloat().SetValue(1, 4), new MFloat().SetValue(2, 4) },
                new object[] { new MFloat().SetValue(1, 2), new MFloat().SetValue(2, 3) },
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
        public void AbsoluteTest(MFloat a, MFloat b)
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
        public void AddTest(MFloat a, MFloat b, MFloat c)
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
        public void ComparisonTest(MFloat a, MFloat b)
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
        public void EqualityTest(MFloat a, MFloat b)
        {
            Assert.True(a == b);
        }
    }
}