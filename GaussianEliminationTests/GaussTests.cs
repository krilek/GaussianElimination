// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GaussTests.cs" company="">
//   
// </copyright>
// <summary>
//   The gauss tests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GaussTests.cs">
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
    /// The gauss tests.
    /// </summary>
    public class GaussTests
    {
        /// <summary>
        /// The gauss correctness test cases.
        /// </summary>
        private static object[] gaussCorrectnessTestCases =
            {
                new object[]
                    {
                        new MyMatrix<MDouble>(
                            new[,]
                                {
                                    {
                                       new MDouble(3.0), new MDouble(2.0), new MDouble(-4.0) 
                                    },
                                    {
                                       new MDouble(2.0), new MDouble(3.0), new MDouble(3.0) 
                                    },
                                    {
                                       new MDouble(5.0), new MDouble(-3.0), new MDouble(1.0) 
                                    }
                                }),
                        new MyMatrix<MDouble>(
                            new[,] { { new MDouble(3.0) }, { new MDouble(15.0) }, { new MDouble(14.0) } }),
                        new MyMatrix<MDouble>(
                            new[,] { { new MDouble(3.0) }, { new MDouble(1.0) }, { new MDouble(2.0) } })
                    }
            };

        /// <summary>
        /// The check equality of solving methods.
        /// </summary>
        /// <param name="size">
        /// The size.
        /// </param>
        [Test]
        [TestCase(10)]
        [TestCase(15)]
        [TestCase(20)]
        [TestCase(50)]
        public void CheckEqualityOfSolvingMethods(int size)
        {
            var templateMatrix = MyMatrix<MDouble>.GetRandomMatrix(size, size);
            var templateVector = MyMatrix<MDouble>.GetRandomMatrix(1, size);
            var solved1 = templateMatrix.SolveLinearEquation(templateVector, LinearSolver.Gauss);
            var solved2 = templateMatrix.SolveLinearEquation(templateVector, LinearSolver.PartialGauss);
            var solved3 = templateMatrix.SolveLinearEquation(templateVector, LinearSolver.FullGauss);
            Assert.AreEqual(solved1, solved2);
            Assert.AreEqual(solved2, solved3);
        }

        /// <summary>
        /// The check solving methods with multiplication.
        /// </summary>
        /// <param name="size">
        /// The size.
        /// </param>
        [Test]
        [TestCase(10)]
        [TestCase(15)]
        [TestCase(20)]
        [TestCase(50)]
        public void CheckSolvingMethodsWithMultiplication(int size)
        {
            var a = MyMatrix<MDouble>.GetRandomMatrix(size, size);
            var x = MyMatrix<MDouble>.GetRandomMatrix(1, size);
            MyMatrix<MDouble> b = a * x;

            var solved1 = a.SolveLinearEquation(b, LinearSolver.Gauss);
            var solved2 = a.SolveLinearEquation(b, LinearSolver.PartialGauss);
            var solved3 = a.SolveLinearEquation(b, LinearSolver.FullGauss);
            Assert.AreEqual(solved1, x);
            Assert.AreEqual(solved2, x);
            Assert.AreEqual(solved3, x);
        }

        /// <summary>
        /// The verify correctness of full gauss.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        [Test]
        [TestCaseSource(nameof(gaussCorrectnessTestCases))]
        public void VerifyCorrectnessOfFullGauss(MyMatrix<MDouble> a, MyMatrix<MDouble> v, MyMatrix<MDouble> expected)
        {
            Assert.AreEqual(a.SolveLinearEquation(v, LinearSolver.FullGauss), expected);
        }

        /// <summary>
        /// The verify correctness of gauss.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        [Test]
        [TestCaseSource(nameof(gaussCorrectnessTestCases))]
        public void VerifyCorrectnessOfGauss(MyMatrix<MDouble> a, MyMatrix<MDouble> v, MyMatrix<MDouble> expected)
        {
            Assert.AreEqual(a.SolveLinearEquation(v, LinearSolver.Gauss), expected);
        }

        /// <summary>
        /// The verify correctness of partial gauss.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <param name="expected">
        /// The expected.
        /// </param>
        [Test]
        [TestCaseSource(nameof(gaussCorrectnessTestCases))]
        public void VerifyCorrectnessOfPartialGauss(
            MyMatrix<MDouble> a,
            MyMatrix<MDouble> v,
            MyMatrix<MDouble> expected)
        {
            Assert.AreEqual(a.SolveLinearEquation(v, LinearSolver.PartialGauss), expected);
        }
    }
}