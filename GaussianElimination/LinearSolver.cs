#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinearSolver.cs">
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
    #region Usings

    using System;
    using System.Linq;

    #endregion

    /// <summary>
    ///     The linear solver.
    /// </summary>
    internal static class LinearSolver
    {
        /// <summary>
        /// The full gauss.
        /// </summary>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        /// <param name="vector">
        /// The vector.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MyMatrix"/>.
        /// </returns>
        public static MyMatrix<T> FullGauss<T>(MyMatrix<T> matrix, MyMatrix<T> vector)
            where T : Value<T>, new()
        {
            var m = new MyMatrix<T>(matrix);
            var v = new MyMatrix<T>(vector);
            int[] columnsLocations = Enumerable.Range(0, vector.Height).ToArray();
            for (int i = 0; i < vector.Height; i++)
            {
#if DEBUG
                Console.WriteLine($"Full GAUSS: ROW:{i}/{vector.Height}");

#endif
                int rowToSwap = i;
                int columnToSwap = i;

                // Select best column and row to swap.
                for (int j = i + 1; j < vector.Height; j++)
                {
                    for (int k = i; k < matrix.Width; k++)
                    {
                        if (m[j, k].Absolute() > m[rowToSwap, columnToSwap].Absolute())
                        {
                            rowToSwap = j;
                            columnToSwap = k;
                        }
                    }
                }

                if (rowToSwap != i || columnToSwap != i)
                {
                    // Swap information about columns for future reading result.
                    columnsLocations.Swap(columnToSwap, i);

                    // Swap rows and columns
                    m.SwapRows(rowToSwap, i);
                    m.SwapColumns(columnToSwap, i);
                    v.SwapRows(rowToSwap, i);
                }

                CreateStep(m, v, i);
            }

            // Retrieve result and sort it according to the columns locations
            var result = RetrieveResult(m, v);
            var originalResult = new MyMatrix<T>(result);
            for (int j = 0; j < v.Height; j++)
                originalResult[columnsLocations[j], 0] = result[j, 0];

            return originalResult;
        }

        /// <summary>
        /// The gauss.
        /// </summary>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        /// <param name="vector">
        /// The vector.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MyMatrix{T}"/>.
        /// </returns>
        public static MyMatrix<T> Gauss<T>(MyMatrix<T> matrix, MyMatrix<T> vector)
            where T : Value<T>, new()
        {
            // Create deep copies of received matrices
            var m = new MyMatrix<T>(matrix);
            var v = new MyMatrix<T>(vector);
            for (var row = 0; row < v.Height; row++)
            {
#if DEBUG
                Console.WriteLine($"GAUSS: ROW:{row}/{v.Height}");
#endif
                CreateStep(m, v, row);
            }

            return RetrieveResult(m, v);
        }

        /// <summary>
        /// The partial gauss.
        /// </summary>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        /// <param name="vector">
        /// The vector.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MyMatrix{T}"/>.
        /// </returns>
        public static MyMatrix<T> PartialGauss<T>(MyMatrix<T> matrix, MyMatrix<T> vector)
            where T : Value<T>, new()
        {
            var m = new MyMatrix<T>(matrix);
            var v = new MyMatrix<T>(vector);
            for (int i = 0; i < vector.Height; i++)
            {
#if DEBUG
                Console.WriteLine($"Partial GAUSS: ROW:{i}/{vector.Height}");
#endif
                int rowToSwap = i;
                for (int j = i + 1; j < vector.Height; j++)
                {
                    if (m[j, i].Absolute() > m[rowToSwap, i].Absolute())
                    {
                        rowToSwap = j;
                    }
                }

                if (rowToSwap != i)
                {
                    m.SwapRows(rowToSwap, i);
                    v.SwapRows(rowToSwap, i);

                    // Swap rows
                }

                CreateStep(m, v, i);
            }

            return RetrieveResult(m, v);
        }

        /// <summary>
        /// Swaps element in array.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <param name="j">
        /// The j.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void Swap<T>(this T[] a, int i, int j)
        {
            var t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        /// <summary>
        /// For given row create step with zeros on left.
        /// </summary>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        /// <param name="vector">
        /// The vector.
        /// </param>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        private static void CreateStep<T>(MyMatrix<T> matrix, MyMatrix<T> vector, int row)
            where T : Value<T>, new()
        {
            for (var i = row + 1; i < vector.Height; i++)
            {
                var alpha = matrix[i, row] / matrix[row, row];
                vector[i, 0] = vector[i, 0] - (alpha * vector[row, 0]);
                for (var j = row; j < vector.Height; j++)
                {
                    matrix[i, j] = matrix[i, j] - (alpha * matrix[row, j]);
                }
            }
        }

        /// <summary>
        /// The retrieve result.
        /// </summary>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        /// <param name="vector">
        /// The vector.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MyMatrix{T}"/>.
        /// </returns>
        private static MyMatrix<T> RetrieveResult<T>(MyMatrix<T> matrix, MyMatrix<T> vector)
            where T : Value<T>, new()
        {
            var resultVector = new MyMatrix<T>(1, vector.Height);
            for (var i = vector.Height - 1; i >= 0; i--)
            {
                Value<T> sum = new T();
                for (var j = i + 1; j < vector.Height; j++)
                {
                    sum += matrix[i, j] * resultVector[j, 0];
                }

                resultVector[i, 0] = (vector[i, 0] - sum) / matrix[i, i];
            }

            return resultVector;
        }
    }
}