#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyMatrix.cs">
// Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// krilek@gmail.com
// </copyright>
// <summary>
// Matrix for storing Value<T> types
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

#endregion

namespace GaussianElimination
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Text;

    using GaussianElimination.DataTypes;

    #endregion

    /// <summary>
    /// Matrix for storing Value types
    /// </summary>
    /// <typeparam name="T">
    /// Classes implementing Value abstract class.
    /// </typeparam>
    public class MyMatrix<T> : IEquatable<MyMatrix<T>>
        where T : Value<T>, new()
    {
        /// <summary>
        ///     The matrix.
        /// </summary>
        private Value<T>[,] matrix;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyMatrix{T}"/> class.
        /// </summary>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        public MyMatrix(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Matrix = new Value<T>[this.Height, this.Width];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyMatrix{T}"/> class.
        /// </summary>
        /// <param name="values">
        /// The values.
        /// </param>
        public MyMatrix(Value<T>[,] values)
        {
            this.Matrix = values;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyMatrix{T}"/> class.
        /// </summary>
        /// <param name="matrixToCopy">
        /// The matrix to copy.
        /// </param>
        public MyMatrix(MyMatrix<T> matrixToCopy)
            : this(matrixToCopy.Width, matrixToCopy.Height)
        {
            for (var i = 0; i < this.Height; i++)
            {
                for (var j = 0; j < this.Width; j++)
                {
                    this.Matrix[i, j] = matrixToCopy[i, j].Clone();
                }
            }
        }

        /// <summary>
        ///     Gets the height.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        ///     Gets or sets the matrix.
        /// </summary>
        public Value<T>[,] Matrix
        {
            get => this.matrix;
            set
            {
                this.Width = value.GetLength(1);
                this.Height = value.GetLength(0);
                this.matrix = value;
            }
        }

        /// <summary>
        ///     Gets the width.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Simplified indexing
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        public Value<T> this[int x, int y]
        {
            get => this.Matrix[x, y];
            set => this.Matrix[x, y] = value;
        }

        /// <summary>
        /// Create random matrix.
        /// </summary>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <param name="seed">
        /// The seed.
        /// </param>
        /// <returns>
        /// The <see cref="MyMatrix"/>.
        /// </returns>
        public static MyMatrix<T> GetRandomMatrix(int width, int height, int? seed = null)
        {
            if (seed == null)
            {
                seed = Guid.NewGuid().GetHashCode();
            }

            const int Min = -1 << 16;
            const int Max = (1 << 16) - 1;
            var rnd = new Random(seed.Value);
            var matrix = new MyMatrix<T>(width, height);
            for (var i = 0; i < matrix.Height; i++)
            {
                for (var j = 0; j < matrix.Width; j++)
                {
                    matrix.Matrix[i, j] = new T().SetValue(rnd.Next(Min, Max), 1 << 16);
                }
            }

            return matrix;
        }

        /// <summary>
        ///     The ==.
        /// </summary>
        /// <param name="left">
        ///     The left.
        /// </param>
        /// <param name="right">
        ///     The right.
        /// </param>
        /// <returns>
        ///     <see cref="bool" /> true if matrices are equal.
        /// </returns>
        public static bool operator ==(MyMatrix<T> left, MyMatrix<T> right)
        {
            return EqualityComparer<MyMatrix<T>>.Default.Equals(left, right);
        }

        /// <summary>
        ///     The !=.
        /// </summary>
        /// <param name="left">
        ///     The left.
        /// </param>
        /// <param name="right">
        ///     The right.
        /// </param>
        /// <returns>
        ///     <see cref="bool" /> true if matrices are not equal.
        /// </returns>
        public static bool operator !=(MyMatrix<T> left, MyMatrix<T> right)
        {
            return !(left == right);
        }

        /// <summary>
        ///     The *.
        /// </summary>
        /// <param name="left">
        ///     The left.
        /// </param>
        /// <param name="right">
        ///     The right.
        /// </param>
        /// <returns>
        ///     <see cref="Matrix" /> multiplied matrices.
        /// </returns>
        public static MyMatrix<T> operator *(MyMatrix<T> left, MyMatrix<T> right)
        {
            if (left.Width != right.Height)
            {
                throw new ArgumentException("When multiplying matrix A must have equal width with height of matrix B.");
            }

            var result = new MyMatrix<T>(right.Width, left.Height);
            result.FillMatrixWithValue(new T());
            for (int i = 0; i < left.Height; i++)
            {
                for (int j = 0; j < right.Width; j++)
                {
                    for (int k = 0; k < right.Height; k++)
                    {
                        result[i, j] += left[i, k] * right[k, j];
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///     The -.
        /// </summary>
        /// <param name="left">
        ///     The left.
        /// </param>
        /// <param name="right">
        ///     The right.
        /// </param>
        /// <returns>
        ///     <see cref="Matrix" /> subtracted matrices.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     Thrown when matrices are not equal.
        /// </exception>
        public static MyMatrix<T> operator -(MyMatrix<T> left, MyMatrix<T> right)
        {
            if (left.Width != right.Width || left.Height != right.Height)
            {
                throw new ArgumentException("Subtract is allowed for matrices of equal dimensions.");
            }

            MyMatrix<T> result = new MyMatrix<T>(left.Width, right.Height);
            for (int i = 0; i < left.Height; i++)
            {
                for (int j = 0; j < left.Width; j++)
                {
                    result[i, j] = left[i, j] - right[i, j];
                }
            }

            return result;
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as MyMatrix<T>);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Equals(MyMatrix<T> other)
        {
            var flag = other != null && this.Width == other.Width && this.Height == other.Height;
            for (var i = 0; i < this.Height; i++)
            {
                for (var j = 0; j < this.Width; j++)
                {
                    if (!flag)
                    {
                        return false;
                    }

                    var a = this[i, j];
                    var b = other[i, j];
                    flag = a == b;
                }
            }

            return flag;
        }

        /// <summary>
        /// The fill matrix with value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void FillMatrixWithValue(Value<T> value)
        {
            for (var i = 0; i < this.Height; i++)
            {
                for (var j = 0; j < this.Width; j++)
                {
                    this.Matrix[i, j] = value;
                }
            }
        }

        /// <summary>
        ///     The get hash code.
        /// </summary>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Width, this.Height, this.Matrix);
        }

        /// <summary>
        /// The relative error.
        /// </summary>
        /// <param name="expected">
        /// The expected.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        public Value<T> RelativeError(MyMatrix<T> expected)
        {
            Value<T> sum = new T();
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    var err = expected[i, j] - this[i, j];
                    sum += err * err;
                }
            }

            return sum;
        }

        /// <summary>
        /// The solve linear equation.
        /// </summary>
        /// <param name="vector">
        /// The vector.
        /// </param>
        /// <param name="solvingFunc">
        /// The solving function to use.
        /// </param>
        /// <returns>
        /// The <see cref="MyMatrix"/>.
        /// </returns>
        public MyMatrix<T> SolveLinearEquation(
            MyMatrix<T> vector,
            Func<MyMatrix<T>, MyMatrix<T>, MyMatrix<T>> solvingFunc)
        {
            return solvingFunc(this, vector);
        }

        /// <summary>
        /// The swap columns.
        /// </summary>
        /// <param name="firstCol">
        /// The first col.
        /// </param>
        /// <param name="secondCol">
        /// The second col.
        /// </param>
        public void SwapColumns(int firstCol, int secondCol)
        {
            for (int y = 0; y < this.Height; y++)
            {
                var temp = this[y, firstCol];
                this[y, firstCol] = this[y, secondCol];
                this[y, secondCol] = temp;
            }
        }

        /// <summary>
        /// The swap rows.
        /// </summary>
        /// <param name="firstRow">
        /// The first row.
        /// </param>
        /// <param name="secondRow">
        /// The second row.
        /// </param>
        public void SwapRows(int firstRow, int secondRow)
        {
            for (int x = 0; x < this.Width; x++)
            {
                var temp = this[secondRow, x];
                this[secondRow, x] = this[firstRow, x];
                this[firstRow, x] = temp;
            }
        }

        /// <summary>
        ///     The to string.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < this.Height; i++)
            {
                sb.Append("[");

                for (var j = 0; j < this.Width; j++)
                {
                    sb.Append($"{this.Matrix[i, j]}, ");
                }

                sb.AppendLine("],");
            }

            return sb.ToString();
        }
    }
}