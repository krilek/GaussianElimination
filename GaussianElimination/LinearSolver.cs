#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Fraction.cs">
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
    static class LinearSolver
    {
        private static void CleanMatrix<T>(MojaMacierz<T> matrix, MojaMacierz<T> vector, int row) where T : new()
        {
            for (int i = row + 1; i < vector.Height; i++)
            {
                Value<T> alpha = matrix[i, row] / matrix[row, row];
                vector[i, 0] = vector[i, 0] - alpha * vector[row, 0];
                for (int j = row; j < vector.Height; j++)
                {
                    matrix[i, j] = matrix[i, j] - (alpha * matrix[row, j]);
                }
            }
        }

        private static MojaMacierz<T> CountBackwardResult<T>(MojaMacierz<T> matrix, MojaMacierz<T> vector) where T : new()
        {
            MojaMacierz<T> resultVector = new MojaMacierz<T>(1, vector.Height);
            for (int i = vector.Height - 1; i >= 0; i--)
            {
                Value<T> sum = new Value<T>();
                for (int j = i + 1; j < vector.Height; j++)
                {
                    sum += matrix[i, j] * resultVector[j, 0];
                }
                resultVector[i, 0] = (vector[i, 0] - sum) / matrix[i, i];
            }
            return resultVector;
        }
        public static MojaMacierz<T> Gauss<T>(MojaMacierz<T> matrix, MojaMacierz<T> vector) where T : new()
        {
            for (int row = 0; row < vector.Height; row++)
            {
                CleanMatrix(matrix, vector, row);
            }

            MojaMacierz<T> resultVector = CountBackwardResult(matrix, vector);
            return resultVector;
        }
    }
}
