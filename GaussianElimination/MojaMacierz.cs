#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MojaMacierz.cs">
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
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MojaMacierz<T> : IEquatable<MojaMacierz<T>>
        where T : Value<T>, new()
    {
        public MojaMacierz(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Matrix = new Value<T>[this.Height, this.Width];
        }

        public MojaMacierz(Value<T>[,] values)
        {
            this.Width = values.GetLength(1);
            this.Height = values.GetLength(0);
            this.Matrix = values;
        }

        public MojaMacierz(MojaMacierz<T> matrixToCopy)
            : this(matrixToCopy.Width, matrixToCopy.Height)
        {
            for (var i = 0; i < this.Height; i++)
            for (var j = 0; j < this.Width; j++)
                this.Matrix[i, j] = matrixToCopy[i, j].Clone();
        }

        public int Height { get; }

        public Value<T>[,] Matrix { get; set; }

        public int Width { get; }

        public Value<T> this[int i, int j]
        {
            get => this.Matrix[i, j];
            set => this.Matrix[i, j] = value;
        }

        public static MojaMacierz<T> GetRandomMatrix(int width, int height, int? seed = null)
        {
            if (seed == null) seed = Guid.NewGuid().GetHashCode();
            const int Min = -1 << 16;
            const int Max = (1 << 16) - 1;
            var rnd = new Random(seed.Value);
            var matrix = new MojaMacierz<T>(width, height);
            for (var i = 0; i < matrix.Height; i++)
            for (var j = 0; j < matrix.Width; j++)
                matrix.Matrix[i, j] = new T().Rand(rnd.Next(Min, Max));

            return matrix;
        }

        public static bool operator ==(MojaMacierz<T> left, MojaMacierz<T> right)
        {
            return EqualityComparer<MojaMacierz<T>>.Default.Equals(left, right);
        }

        public static bool operator !=(MojaMacierz<T> left, MojaMacierz<T> right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as MojaMacierz<T>);
        }

        public bool Equals(MojaMacierz<T> other)
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

        public void FillMatrixWithValue(Value<T> value)
        {
            for (var i = 0; i < this.Height; i++)
            for (var j = 0; j < this.Width; j++)
                this.Matrix[i, j] = value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Width, this.Height, this.Matrix);
        }

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