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

    public class MojaMacierz<T>
    {
        public int Width { get; }

        public int Height { get; }

        public Value<T>[,] Matrix { get; set; }
        public MojaMacierz(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Matrix = new Value<T>[this.Height, this.Width];
        }

        public void FillMatrixWithValue(Value<T> value)
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this.Matrix[i, j] = value;
                }
            }
        }

        public static MojaMacierz<Fraction> FillWithRandomFractions(int width, int height)
        {
            const int Min = -1 << 16;
            const int Max = (1 << 16) - 1;
            var matrix = new MojaMacierz<Fraction>(width,height);
            for (int i = 0; i < matrix.Height; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    var rnd = new Random();
                    var val = rnd.Next(Min, Max);
                    matrix.Matrix[i, j] = new Value<Fraction>(new Fraction(val, 1 << 16));
                }
            }

            return matrix;
        }
        public static MojaMacierz<double> FillWithRandomDouble(int width, int height)
        {
            const int Min = -1 << 16;
            const int Max = (1 << 16) - 1;
            var matrix = new MojaMacierz<double>(width, height);
            for (int i = 0; i < matrix.Height; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    var rnd = new Random();
                    var val = rnd.Next(Min, Max);
                    matrix.Matrix[i, j] = new Value<double>((double)val / (1 << 16));
                }
            }

            return matrix;
        }

        public static MojaMacierz<float> FillWithRandomFloat(int width, int height)
        {
            const int Min = -1 << 16;
            const int Max = (1 << 16) - 1;
            var matrix = new MojaMacierz<float>(width, height);
            for (int i = 0; i < matrix.Height; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    var rnd = new Random();
                    var val = rnd.Next(Min, Max);
                    matrix.Matrix[i, j] = new Value<float>((float)val / (1 << 16));
                }
            }

            return matrix;
        }
    }
}