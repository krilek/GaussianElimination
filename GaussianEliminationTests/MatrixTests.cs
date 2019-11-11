#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MatrixTests.cs">
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
    ///     The matrix tests.
    /// </summary>
    [TestFixture]
    public class MatrixTests
    {
        /// <summary>
        ///     The matrix multiply cases.
        /// </summary>
        private static object[] matrixMultiplyCases =
            {
                new object[]
                    {
                        new MyMatrix<MDouble>(
                            new[,]
                                {
                                    {
                                        new MDouble().SetValue(12), new MDouble().SetValue(7), new MDouble().SetValue(3)
                                    },
                                    { new MDouble().SetValue(4), new MDouble().SetValue(5), new MDouble().SetValue(6) },
                                    { new MDouble().SetValue(7), new MDouble().SetValue(8), new MDouble().SetValue(9) }
                                }),
                        new MyMatrix<MDouble>(
                            new[,]
                                {
                                    {
                                        new MDouble().SetValue(5), new MDouble().SetValue(8), new MDouble().SetValue(1),
                                        new MDouble().SetValue(2)
                                    },
                                    {
                                        new MDouble().SetValue(6), new MDouble().SetValue(7), new MDouble().SetValue(3),
                                        new MDouble().SetValue(0)
                                    },
                                    {
                                        new MDouble().SetValue(4), new MDouble().SetValue(5), new MDouble().SetValue(9),
                                        new MDouble().SetValue(1)
                                    }
                                }),
                        new MyMatrix<MDouble>(
                            new[,]
                                {
                                    {
                                        new MDouble().SetValue(114), new MDouble().SetValue(160),
                                        new MDouble().SetValue(60), new MDouble().SetValue(27)
                                    },
                                    {
                                        new MDouble().SetValue(74), new MDouble().SetValue(97),
                                        new MDouble().SetValue(73), new MDouble().SetValue(14)
                                    },
                                    {
                                        new MDouble().SetValue(119), new MDouble().SetValue(157),
                                        new MDouble().SetValue(112), new MDouble().SetValue(23)
                                    }
                                })
                    }
            };

        [Test]
        [TestCaseSource(nameof(matrixMultiplyCases))]
        public void ClonedMatrixIsEqualTest(MyMatrix<MDouble> a, MyMatrix<MDouble> b, MyMatrix<MDouble> c)
        {
            var clonedMatrixA = new MyMatrix<MDouble>(a);
            Assert.AreEqual(a, clonedMatrixA);
            var clonedMatrixB = new MyMatrix<MDouble>(b);
            Assert.AreEqual(b, clonedMatrixB);
            Assert.AreNotEqual(clonedMatrixA, clonedMatrixB);
            var clonedMatrixC = new MyMatrix<MDouble>(c);
            Assert.AreEqual(c, clonedMatrixC);
            Assert.AreNotEqual(clonedMatrixA, clonedMatrixC);
            Assert.AreNotEqual(clonedMatrixB, clonedMatrixC);
        }

        [Test]
        [TestCase(0, 0, 6)]
        [TestCase(1, 2, 5.0)]
        public void ReplaceValue(int x, int y, double value)
        {
            var matrix = new MyMatrix<MDouble>(
                new[,]
                    {
                        { new MDouble().SetValue(3.0), new MDouble().SetValue(15.0), new MDouble().SetValue(14.0) },
                        { new MDouble().SetValue(2.1), new MDouble().SetValue(2.2), new MDouble().SetValue(2.3), },
                        { new MDouble().SetValue(4.1), new MDouble().SetValue(2.4), new MDouble().SetValue(1.6), }
                    });
            var matrixCopy = new MyMatrix<MDouble>(matrix);
            var oldValue = matrixCopy[x, y];
            matrixCopy[x, y] = new MDouble(value);
            Assert.AreNotEqual(matrix, matrixCopy);
            matrixCopy[x, y] = oldValue;
            Assert.AreEqual(matrix, matrixCopy);
        }

        /// <summary>
        ///     The swap column test.
        /// </summary>
        /// <param name="columnA">
        ///     The column a.
        /// </param>
        /// <param name="columnB">
        ///     The column b.
        /// </param>
        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(2, 1)]
        public void SwapColumnTest(int columnA, int columnB)
        {
            var matrix = new MyMatrix<MDouble>(
                new Value<MDouble>[,]
                    {
                        { new MDouble().SetValue(3.0), new MDouble().SetValue(15.0), new MDouble().SetValue(14.0) },
                        { new MDouble().SetValue(2.1), new MDouble().SetValue(2.2), new MDouble().SetValue(2.3), },
                        { new MDouble().SetValue(4.1), new MDouble().SetValue(2.4), new MDouble().SetValue(1.6), }
                    });
            var matrixCopy = new MyMatrix<MDouble>(matrix);
            matrixCopy.SwapColumns(columnA, columnB);
            Assert.AreNotEqual(matrix, matrixCopy);
            matrixCopy.SwapColumns(columnA, columnB);
            Assert.AreEqual(matrix, matrixCopy);
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(2, 1)]
        public void SwapRowTest(int columnA, int columnB)
        {
            var matrix = new MyMatrix<MDouble>(
                new Value<MDouble>[,]
                    {
                        { new MDouble().SetValue(3.0), new MDouble().SetValue(15.0), new MDouble().SetValue(14.0) },
                        { new MDouble().SetValue(2.1), new MDouble().SetValue(2.2), new MDouble().SetValue(2.3), },
                        { new MDouble().SetValue(4.1), new MDouble().SetValue(2.4), new MDouble().SetValue(1.6), }
                    });
            var matrixCopy = new MyMatrix<MDouble>(matrix);
            matrixCopy.SwapRows(columnA, columnB);
            Assert.AreNotEqual(matrix, matrixCopy);
            matrixCopy.SwapRows(columnA, columnB);
            Assert.AreEqual(matrix, matrixCopy);
        }

        /// <summary>
        ///     The verify multiply double.
        /// </summary>
        /// <param name="a">
        ///     The a.
        /// </param>
        /// <param name="b">
        ///     The b.
        /// </param>
        /// <param name="res">
        ///     The res.
        /// </param>
        [Test]
        [TestCaseSource(nameof(matrixMultiplyCases))]
        public void VerifyMultiplyDouble(MyMatrix<MDouble> a, MyMatrix<MDouble> b, MyMatrix<MDouble> res)
        {
            TestContext.WriteLine(a);
            TestContext.WriteLine("Times");
            TestContext.WriteLine(b);
            TestContext.WriteLine("Equals");
            var calculatedResult = a * b;
            TestContext.WriteLine(calculatedResult);
            Assert.AreEqual(calculatedResult, res);
        }
    }
}