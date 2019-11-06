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
    internal static class LinearSolver
    {
        public static MojaMacierz<T> Gauss<T>(MojaMacierz<T> matrix, MojaMacierz<T> vector)
            where T : Value<T>, new()
        {
            for (var row = 0; row < vector.Height; row++) CleanMatrix(matrix, vector, row);

            var resultVector = CountBackwardResult(matrix, vector);
            return resultVector;
        }

        private static void CleanMatrix<T>(MojaMacierz<T> matrix, MojaMacierz<T> vector, int row)
            where T : Value<T>, new()
        {
            for (var i = row + 1; i < vector.Height; i++)
            {
                var alpha = matrix[i, row] / matrix[row, row];
                vector[i, 0] = vector[i, 0] - alpha * vector[row, 0];
                for (var j = row; j < vector.Height; j++) matrix[i, j] = matrix[i, j] - alpha * matrix[row, j];
            }
        }

        private static MojaMacierz<T> CountBackwardResult<T>(MojaMacierz<T> matrix, MojaMacierz<T> vector)
            where T : Value<T>, new()
        {
            var resultVector = new MojaMacierz<T>(1, vector.Height);
            for (var i = vector.Height - 1; i >= 0; i--)
            {
                Value<T> sum = new T();
                for (var j = i + 1; j < vector.Height; j++) sum += matrix[i, j] * resultVector[j, 0];
                resultVector[i, 0] = (vector[i, 0] - sum) / matrix[i, i];
            }

            return resultVector;
        }
    }
}