#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MyMatrix.cs">
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
    using System.Collections.Generic;
    using System.Text;

    #endregion

    /// <summary>
    /// The my matrix.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class MyMatrix<T> : IEquatable<MyMatrix<T>>
        where T : Value<T>, new()
    {
        /// <summary>
        /// The matrix.
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
            for (var j = 0; j < this.Width; j++)
                this.Matrix[i, j] = matrixToCopy[i, j].Clone();
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets or sets the matrix.
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
        /// Gets the width.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The this.
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
        /// The get random matrix.
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
            if (seed == null) seed = Guid.NewGuid().GetHashCode();
            const int Min = -1 << 16;
            const int Max = (1 << 16) - 1;
            var rnd = new Random(seed.Value);
            var matrix = new MyMatrix<T>(width, height);
            for (var i = 0; i < matrix.Height; i++)
            for (var j = 0; j < matrix.Width; j++)
                matrix.Matrix[i, j] = new T().Rand(rnd.Next(Min, Max));

            return matrix;
        }

        /// <summary>
        /// The ==.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator ==(MyMatrix<T> left, MyMatrix<T> right)
        {
            return EqualityComparer<MyMatrix<T>>.Default.Equals(left, right);
        }

        /// <summary>
        /// The !=.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator !=(MyMatrix<T> left, MyMatrix<T> right)
        {
            return !(left == right);
        }

        /// <summary>
        /// The *.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// </returns>
        public static MyMatrix<T> operator *(MyMatrix<T> left, MyMatrix<T> right)
        {
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
            for (var j = 0; j < this.Width; j++)
            {
                if (!flag) return false;
                var a = this[i, j];
                var b = other[i, j];
                flag = a == b;
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
            for (var j = 0; j < this.Width; j++)
                this.Matrix[i, j] = value;
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Width, this.Height, this.Matrix);
        }

        /// <summary>
        /// The get row.
        /// </summary>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <returns>
        /// The <see cref="MyMatrix"/>.
        /// </returns>
        public MyMatrix<T> GetRow(int i)
        {
            return null;
        }

        /// <summary>
        /// The solve linear equation.
        /// </summary>
        /// <param name="vector">
        /// The vector.
        /// </param>
        /// <param name="solvingFunc">
        /// The solving func.
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
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < this.Height; i++)
            {
                for (var j = 0; j < this.Width; j++) sb.Append(this.Matrix[i, j]);
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}