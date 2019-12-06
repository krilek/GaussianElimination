#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MatrixTests.cs">
// Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// krilek@gmail.com
// </copyright>
// <summary>
//     The matrix tests.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

#endregion

namespace GaussianElimination.Tests
{
    #region Usings

    using System.Linq;

    using GaussianElimination.DataTypes;

    using NUnit.Framework;

    #endregion

    /// <summary>
    ///     The matrix tests.
    /// </summary>
    [TestFixture]
    public class KeyedMatrixTests
    {
        // private static object[] matrixIdentityCases =
        // {
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble().SetValue(1), new MDouble().SetValue(0), new MDouble().SetValue(0) },
        // { new MDouble().SetValue(0), new MDouble().SetValue(1), new MDouble().SetValue(0) },
        // { new MDouble().SetValue(0), new MDouble().SetValue(0), new MDouble().SetValue(1) }
        // }),
        // true
        // },
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble().SetValue(1), new MDouble().SetValue(0), new MDouble().SetValue(0) },
        // { new MDouble().SetValue(0), new MDouble().SetValue(2), new MDouble().SetValue(0) },
        // { new MDouble().SetValue(0), new MDouble().SetValue(0), new MDouble().SetValue(1) }
        // }),
        // false
        // },
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble().SetValue(1), new MDouble().SetValue(0), new MDouble().SetValue(0) },
        // { new MDouble().SetValue(0), new MDouble().SetValue(1), new MDouble().SetValue(0) },
        // { new MDouble().SetValue(0), new MDouble().SetValue(2), new MDouble().SetValue(1) }
        // }),
        // false
        // },
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble().SetValue(1), new MDouble().SetValue(0), new MDouble().SetValue(0) },
        // { new MDouble().SetValue(0), new MDouble().SetValue(1), new MDouble().SetValue(0) }
        // }),
        // false
        // },
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble().SetValue(1), new MDouble().SetValue(0) },
        // { new MDouble().SetValue(0), new MDouble().SetValue(1) },
        // { new MDouble().SetValue(0), new MDouble().SetValue(0) }
        // }),
        // false
        // },
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // {
        // new MDouble().SetValue(1), new MDouble().SetValue(0), new MDouble().SetValue(0),
        // new MDouble().SetValue(0)
        // },
        // {
        // new MDouble().SetValue(0), new MDouble().SetValue(1), new MDouble().SetValue(0),
        // new MDouble().SetValue(0)
        // },
        // {
        // new MDouble().SetValue(0), new MDouble().SetValue(0), new MDouble().SetValue(1),
        // new MDouble().SetValue(0)
        // },
        // {
        // new MDouble().SetValue(0), new MDouble().SetValue(0), new MDouble().SetValue(0),
        // new MDouble().SetValue(1)
        // },
        // }),
        // true
        // },
        // new object[] { KeyedMatrix<MDouble>.GetIdentityMatrix(5), true },
        // new object[] { KeyedMatrix<MDouble>.GetIdentityMatrix(50), true },
        // new object[] { KeyedMatrix<MDouble>.GetRandomMatrix(5, 5), false },
        // };

        ///// <summary>
        /////     The matrix multiply cases.
        ///// </summary>
        // private static object[] matrixMultiplyCases =
        // {
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // {
        // new MDouble().SetValue(12), new MDouble().SetValue(7), new MDouble().SetValue(3)
        // },
        // { new MDouble().SetValue(4), new MDouble().SetValue(5), new MDouble().SetValue(6) },
        // { new MDouble().SetValue(7), new MDouble().SetValue(8), new MDouble().SetValue(9) }
        // }),
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // {
        // new MDouble().SetValue(5), new MDouble().SetValue(8), new MDouble().SetValue(1),
        // new MDouble().SetValue(2)
        // },
        // {
        // new MDouble().SetValue(6), new MDouble().SetValue(7), new MDouble().SetValue(3),
        // new MDouble().SetValue(0)
        // },
        // {
        // new MDouble().SetValue(4), new MDouble().SetValue(5), new MDouble().SetValue(9),
        // new MDouble().SetValue(1)
        // }
        // }),
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // {
        // new MDouble().SetValue(114), new MDouble().SetValue(160),
        // new MDouble().SetValue(60), new MDouble().SetValue(27)
        // },
        // {
        // new MDouble().SetValue(74), new MDouble().SetValue(97),
        // new MDouble().SetValue(73), new MDouble().SetValue(14)
        // },
        // {
        // new MDouble().SetValue(119), new MDouble().SetValue(157),
        // new MDouble().SetValue(112), new MDouble().SetValue(23)
        // }
        // })
        // },
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble(1), new MDouble(2), new MDouble(1), },
        // { new MDouble(0), new MDouble(1), new MDouble(0), },
        // { new MDouble(2), new MDouble(3), new MDouble(4), },
        // }),
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble(2), new MDouble(5) }, { new MDouble(6), new MDouble(7) },
        // { new MDouble(1), new MDouble(8) },
        // }),
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble(15), new MDouble(27) }, { new MDouble(6), new MDouble(7) },
        // { new MDouble(26), new MDouble(63) },
        // })
        // }
        // };

        // private static object[] matrixScalarMultiplicationCases =
        // {
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // {
        // new MDouble().SetValue(12), new MDouble().SetValue(7), new MDouble().SetValue(3)
        // },
        // { new MDouble().SetValue(4), new MDouble().SetValue(5), new MDouble().SetValue(6) },
        // { new MDouble().SetValue(7), new MDouble().SetValue(8), new MDouble().SetValue(9) }
        // }),
        // new MDouble().SetValue(3.0),
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // {
        // new MDouble().SetValue(36), new MDouble().SetValue(21),
        // new MDouble().SetValue(9)
        // },
        // {
        // new MDouble().SetValue(12), new MDouble().SetValue(15),
        // new MDouble().SetValue(18)
        // },
        // {
        // new MDouble().SetValue(21), new MDouble().SetValue(24),
        // new MDouble().SetValue(27)
        // }
        // })
        // },
        // new object[]
        // {
        // KeyedMatrix<MDouble>.GetIdentityMatrix(3),
        // new MDouble().SetValue(3.0),
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // {
        // new MDouble().SetValue(3), new MDouble().SetValue(0),
        // new MDouble().SetValue(0)
        // },
        // {
        // new MDouble().SetValue(0), new MDouble().SetValue(3),
        // new MDouble().SetValue(0)
        // },
        // {
        // new MDouble().SetValue(0), new MDouble().SetValue(0),
        // new MDouble().SetValue(3)
        // }
        // })
        // }
        // };

        ///// <summary>
        /////     The matrix subtract cases.
        ///// </summary>
        // private static object[] matrixSubtractCases =
        // {
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // {
        // new MDouble().SetValue(12), new MDouble().SetValue(7), new MDouble().SetValue(3)
        // },
        // { new MDouble().SetValue(4), new MDouble().SetValue(5), new MDouble().SetValue(6) },
        // { new MDouble().SetValue(7), new MDouble().SetValue(8), new MDouble().SetValue(9) }
        // }),
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble().SetValue(5), new MDouble().SetValue(8), new MDouble().SetValue(1) },
        // { new MDouble().SetValue(6), new MDouble().SetValue(7), new MDouble().SetValue(3) },
        // { new MDouble().SetValue(4), new MDouble().SetValue(5), new MDouble().SetValue(9) }
        // }),
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // {
        // new MDouble().SetValue(7), new MDouble().SetValue(-1), new MDouble().SetValue(2)
        // },
        // {
        // new MDouble().SetValue(-2), new MDouble().SetValue(-2),
        // new MDouble().SetValue(3)
        // },
        // { new MDouble().SetValue(3), new MDouble().SetValue(3), new MDouble().SetValue(0) }
        // })
        // },
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble(1), new MDouble(2) }, { new MDouble(0), new MDouble(1) },
        // { new MDouble(2), new MDouble(3) },
        // }),
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble(2), new MDouble(5) }, { new MDouble(6), new MDouble(7) },
        // { new MDouble(1), new MDouble(8) },
        // }),
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble(-1), new MDouble(-3) }, { new MDouble(-6), new MDouble(-6) },
        // { new MDouble(1), new MDouble(-5) },
        // })
        // }
        // };

        // private static object[] matrixTransposeCases =
        // {
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // {
        // new MDouble().SetValue(12), new MDouble().SetValue(7), new MDouble().SetValue(3)
        // },
        // { new MDouble().SetValue(4), new MDouble().SetValue(5), new MDouble().SetValue(6) },
        // { new MDouble().SetValue(7), new MDouble().SetValue(8), new MDouble().SetValue(9) }
        // }),
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // {
        // new MDouble().SetValue(12), new MDouble().SetValue(4), new MDouble().SetValue(7)
        // },
        // { new MDouble().SetValue(7), new MDouble().SetValue(5), new MDouble().SetValue(8) },
        // { new MDouble().SetValue(3), new MDouble().SetValue(6), new MDouble().SetValue(9) }
        // })
        // },
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // {
        // new MDouble().SetValue(12), new MDouble().SetValue(7), new MDouble().SetValue(3)
        // },
        // { new MDouble().SetValue(4), new MDouble().SetValue(5), new MDouble().SetValue(6) },
        // }),
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble().SetValue(12), new MDouble().SetValue(4) },
        // { new MDouble().SetValue(7), new MDouble().SetValue(5) },
        // { new MDouble().SetValue(3), new MDouble().SetValue(6) }
        // })
        // },
        // new object[]
        // {
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble().SetValue(12), new MDouble().SetValue(7) },
        // { new MDouble().SetValue(4), new MDouble().SetValue(5) },
        // { new MDouble().SetValue(7), new MDouble().SetValue(8) }
        // }),
        // new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // {
        // new MDouble().SetValue(12), new MDouble().SetValue(4), new MDouble().SetValue(7)
        // },
        // { new MDouble().SetValue(7), new MDouble().SetValue(5), new MDouble().SetValue(8) },
        // })
        // },
        // new object[] { KeyedMatrix<MDouble>.GetIdentityMatrix(5), KeyedMatrix<MDouble>.GetIdentityMatrix(5) },
        // new object[]
        // {
        // KeyedMatrix<MDouble>.GetRandomMatrix(10, 10, (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()),
        // KeyedMatrix<MDouble>.GetRandomMatrix(10, 10, (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds())
        // .Transpose()
        // },
        // };

        // [Test]
        // [TestCaseSource(nameof(matrixIdentityCases))]
        // public void CheckIdentityMatrices(KeyedMatrix<MDouble> matrix, bool correct)
        // {
        // Assert.AreEqual(matrix.IsIdentity, correct);
        // }

        // [Test]
        // [TestCaseSource(nameof(matrixTransposeCases))]
        // public void CheckTransposedMatrices(KeyedMatrix<MDouble> matrix, KeyedMatrix<MDouble> transposedMatrix)
        // {
        // Assert.AreEqual(matrix.Transpose(), transposedMatrix);
        // }

        // [Test]
        // [TestCaseSource(nameof(matrixMultiplyCases))]
        // public void ClonedMatrixIsEqualTest(KeyedMatrix<MDouble> a, KeyedMatrix<MDouble> b, KeyedMatrix<MDouble> c)
        // {
        // var clonedMatrixA = new KeyedMatrix<MDouble>(a);
        // Assert.AreEqual(a, clonedMatrixA);
        // var clonedMatrixB = new KeyedMatrix<MDouble>(b);
        // Assert.AreEqual(b, clonedMatrixB);
        // Assert.AreNotEqual(clonedMatrixA, clonedMatrixB);
        // var clonedMatrixC = new KeyedMatrix<MDouble>(c);
        // Assert.AreEqual(c, clonedMatrixC);
        // Assert.AreNotEqual(clonedMatrixA, clonedMatrixC);
        // Assert.AreNotEqual(clonedMatrixB, clonedMatrixC);
        // }

        // [Test]
        // [TestCase(0, 0, 6)]
        // [TestCase(1, 2, 5.0)]
        // public void ReplaceValue(int x, int y, double value)
        // {
        // var matrix = new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble().SetValue(3.0), new MDouble().SetValue(15.0), new MDouble().SetValue(14.0) },
        // { new MDouble().SetValue(2.1), new MDouble().SetValue(2.2), new MDouble().SetValue(2.3), },
        // { new MDouble().SetValue(4.1), new MDouble().SetValue(2.4), new MDouble().SetValue(1.6), }
        // });
        // var matrixCopy = new KeyedMatrix<MDouble>(matrix);
        // var oldValue = matrixCopy[x, y];
        // matrixCopy[x, y] = new MDouble(value);
        // Assert.AreNotEqual(matrix, matrixCopy);
        // matrixCopy[x, y] = oldValue;
        // Assert.AreEqual(matrix, matrixCopy);
        // }

        ///// <summary>
        /////     The swap column test.
        ///// </summary>
        ///// <param name="columnA">
        /////     The column a.
        ///// </param>
        ///// <param name="columnB">
        /////     The column b.
        ///// </param>
        // [Test]
        // [TestCase(0, 1)]
        // [TestCase(1, 0)]
        // [TestCase(2, 0)]
        // [TestCase(2, 1)]
        // public void SwapColumnTest(int columnA, int columnB)
        // {
        // var matrix = new KeyedMatrix<MDouble>(
        // new Value<MDouble>[,]
        // {
        // { new MDouble().SetValue(3.0), new MDouble().SetValue(15.0), new MDouble().SetValue(14.0) },
        // { new MDouble().SetValue(2.1), new MDouble().SetValue(2.2), new MDouble().SetValue(2.3), },
        // { new MDouble().SetValue(4.1), new MDouble().SetValue(2.4), new MDouble().SetValue(1.6), }
        // });
        // var matrixCopy = new KeyedMatrix<MDouble>(matrix);
        // matrixCopy.SwapColumns(columnA, columnB);
        // Assert.AreNotEqual(matrix, matrixCopy);
        // matrixCopy.SwapColumns(columnA, columnB);
        // Assert.AreEqual(matrix, matrixCopy);
        // }

        // [Test]
        // [TestCase(0, 1)]
        // [TestCase(1, 0)]
        // [TestCase(2, 0)]
        // [TestCase(2, 1)]
        // public void SwapRowTest(int columnA, int columnB)
        // {
        // var matrix = new KeyedMatrix<MDouble>(
        // new[,]
        // {
        // { new MDouble().SetValue(3.0), new MDouble().SetValue(15.0), new MDouble().SetValue(14.0) },
        // { new MDouble().SetValue(2.1), new MDouble().SetValue(2.2), new MDouble().SetValue(2.3), },
        // { new MDouble().SetValue(4.1), new MDouble().SetValue(2.4), new MDouble().SetValue(1.6), }
        // });
        // var matrixCopy = new KeyedMatrix<MDouble>(matrix);
        // matrixCopy.SwapRows(columnA, columnB);
        // Assert.AreNotEqual(matrix, matrixCopy);
        // matrixCopy.SwapRows(columnA, columnB);
        // Assert.AreEqual(matrix, matrixCopy);
        // }

        ///// <summary>
        /////     The verify multiply double.
        ///// </summary>
        ///// <param name="a">
        /////     The a.
        ///// </param>
        ///// <param name="b">
        /////     The b.
        ///// </param>
        ///// <param name="res">
        /////     The res.
        ///// </param>
        // [Test]
        // [TestCaseSource(nameof(matrixMultiplyCases))]
        // public void VerifyMultiply(KeyedMatrix<MDouble> a, KeyedMatrix<MDouble> b, KeyedMatrix<MDouble> res)
        // {
        // TestContext.WriteLine(a);
        // TestContext.WriteLine("Times");
        // TestContext.WriteLine(b);
        // TestContext.WriteLine("Equals");
        // var calculatedResult = a * b;
        // TestContext.WriteLine(calculatedResult);
        // Assert.AreEqual(calculatedResult, res);
        // }

        // [Test]
        // [TestCaseSource(nameof(matrixScalarMultiplicationCases))]
        // public void VerifyScalarMultiplication(KeyedMatrix<MDouble> a, Value<MDouble> scalar, KeyedMatrix<MDouble> res)
        // {
        // TestContext.WriteLine(a);
        // TestContext.WriteLine("Times");
        // TestContext.WriteLine(scalar);
        // TestContext.WriteLine("Equals");
        // var calculatedResult = a * scalar;
        // TestContext.WriteLine(calculatedResult);
        // Assert.AreEqual(calculatedResult, res);
        // }

        ///// <summary>
        /////     The verify subtract.
        ///// </summary>
        ///// <param name="a">
        /////     The a.
        ///// </param>
        ///// <param name="b">
        /////     The b.
        ///// </param>
        ///// <param name="res">
        /////     The res.
        ///// </param>
        // [Test]
        // [TestCaseSource(nameof(matrixSubtractCases))]
        // public void VerifySubtract(KeyedMatrix<MDouble> a, KeyedMatrix<MDouble> b, KeyedMatrix<MDouble> res)
        // {
        // TestContext.WriteLine(a);
        // TestContext.WriteLine(" minus ");
        // TestContext.WriteLine(b);
        // TestContext.WriteLine(" equals ");
        // var calculatedResult = a - b;
        // TestContext.WriteLine(calculatedResult);
        // Assert.AreEqual(calculatedResult, res);
        // }

        ///// <summary>
        /////     The verify that random matrix is not null.
        ///// </summary>
        ///// <param name="size">
        /////     The size.
        ///// </param>
        // [Test]
        // [TestCase(5)]
        // [TestCase(10)]
        // [TestCase(20)]
        // [TestCase(25)]
        // [TestCase(50)]
        // public void VerifyThatRandomMatrixIsNotNull(int size)
        // {
        // var x = KeyedMatrix<MDouble>.GetRandomMatrix(size, size);
        // Assert.AreEqual(size, x.Height);
        // Assert.AreEqual(size, x.Width);
        // for (int i = 0; i < x.Height; i++)
        // {
        // for (int j = 0; j < x.Width; j++)
        // {
        // Assert.NotNull(x[i, j]);
        // }
        // }
        // }

        ///// <summary>
        /////     The verify that subtract throws exception.
        ///// </summary>
        ///// <param name="a">
        /////     The a.
        ///// </param>
        ///// <param name="b">
        /////     The b.
        ///// </param>
        ///// <param name="res">
        /////     The res.
        ///// </param>
        // [Test]
        // [TestCaseSource(nameof(matrixMultiplyCases))]
        // public void VerifyThatSubtractThrowsException(KeyedMatrix<MDouble> a, KeyedMatrix<MDouble> b, KeyedMatrix<MDouble> res)
        // {
        // TestContext.WriteLine(a);
        // TestContext.WriteLine(" minus ");
        // TestContext.WriteLine(b);
        // TestContext.WriteLine(" equals ");
        // TestContext.WriteLine("Exception!");
        // Assert.Throws<ArgumentException>(
        // () =>
        // {
        // var x = a - b;
        // });
        // }

        /// <summary>
        /// The verify random normalized columns.
        /// </summary>
        /// <param name="rand">
        /// The rand.
        /// </param>
        [Test]
        public void VerifyRandomNormalizedColumns([Random(5)] int rand)
        {
            var randomNormalized = KeyedMatrix<int, int, MDouble>.GetRandomNormalized(
                Enumerable.Range(0, 4).ToList(),
                Enumerable.Range(0, 3).ToList(),
                NormalizedMethod.NormalizeColumns,
                rand);
            Value<MDouble> sum = new MDouble();
            Value<MDouble> expected = new MDouble().SetValue(1);
            foreach (int i in randomNormalized.ColumnsKeys)
            {
                foreach (int j in randomNormalized.RowsKeys)
                {
                    sum += randomNormalized[j, i];
                }

                Assert.IsTrue(sum == expected);
                sum = new MDouble();
            }
        }

        /// <summary>
        /// The verify random normalized rows.
        /// </summary>
        /// <param name="rand">
        /// The rand.
        /// </param>
        [Test]
        public void VerifyRandomNormalizedRows([Random(5)] int rand)
        {
            var randomNormalized = KeyedMatrix<int, int, MDouble>.GetRandomNormalized(
                Enumerable.Range(0, 3).ToList(),
                Enumerable.Range(0, 5).ToList(),
                NormalizedMethod.NormalizeRow,
                rand);
            Value<MDouble> sum = new MDouble();
            Value<MDouble> expected = new MDouble().SetValue(1);
            foreach (int i in randomNormalized.RowsKeys)
            {
                foreach (int j in randomNormalized.ColumnsKeys)
                {
                    sum += randomNormalized[i, j];
                }

                Assert.IsTrue(sum == expected);
                sum = new MDouble();
            }
        }
    }
}