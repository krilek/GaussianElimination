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
    using System.Linq;

    /// <summary>
    ///     The linear solver.
    /// </summary>
    internal static class LinearSolver
    {
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
                int rowToSwap = i;
                for (int j = i + 1; j < vector.Height; j++)
                {
                    if (m[j, i] > m[rowToSwap, i])
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

        public static MyMatrix<T> FullGauss<T>(MyMatrix<T> matrix, MyMatrix<T> vector) where T : Value<T>, new()
        {
            var m = new MyMatrix<T>(matrix);
            var v = new MyMatrix<T>(vector);
            int[] columnsLocations = Enumerable.Range(0, vector.Height).ToArray(); // Use vector height because matrix could we wider.
            for (int i = 0; i < vector.Height; i++)
            {
                int rowToSwap = i;
                int columnToSwap = i;

                // Select best column and row to swap.
                for (int j = i + 1; j < vector.Height; j++)
                {
                    for (int k = 0; k < matrix.Width; k++)
                    {
                        if (m[j, k] > m[rowToSwap, columnToSwap])
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

                    CreateStep(m, v, i);
                }

            }

            // Retrieve result and sort it according to the columns locations
            var result = RetrieveResult(m, v);

            for (int i = 0; i < result.Height; i++)
            {
                result.SwapRows(i, columnsLocations[i]);
            }

            return result;
        }
        public static void Swap<T>(this T[] a, int i, int j)
        {
            T t = a[i];
            a[i] = a[j];
            a[j] = t;
        }
        /// <summary>
        /// The create step.
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
                vector[i, 0] = vector[i, 0] - alpha * vector[row, 0];
                for (var j = row; j < vector.Height; j++)
                {
                    matrix[i, j] = matrix[i, j] - alpha * matrix[row, j];
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