#region copyright

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyedMatrix.cs">
// Karol Gzik 253923 University of Gdańsk Faculty of Mathematics, Physics and Informatics
// krilek@gmail.com
// </copyright>
// <summary>
// KeyedMatrix for storing Value<T> indexed by key THeightKey and TWidthKey
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

#endregion

namespace GaussianElimination
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GaussianElimination.DataTypes;

    #endregion

    /// <summary>
    /// The keyed matrix.
    /// </summary>
    /// <typeparam name="TRowKey">
    /// </typeparam>
    /// <typeparam name="TColKey">
    /// </typeparam>
    /// <typeparam name="TVal">
    /// </typeparam>
    public class KeyedMatrix<TRowKey, TColKey, TVal> : MyMatrix<TVal>
        where TVal : Value<TVal>, new()
    {
        /// <summary>
        /// The columns keys.
        /// </summary>
        public readonly List<TColKey> ColumnsKeys;

        /// <summary>
        /// The rows keys.
        /// </summary>
        public readonly List<TRowKey> RowsKeys;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedMatrix{TRowKey,TColKey,TVal}"/> class.
        /// </summary>
        /// <param name="rowsKeys">
        /// The rows keys.
        /// </param>
        /// <param name="columnsKeys">
        /// The columns keys.
        /// </param>
        public KeyedMatrix(List<TRowKey> rowsKeys, List<TColKey> columnsKeys)
            : base(columnsKeys.Count, rowsKeys.Count)
        {
            this.RowsKeys = rowsKeys;
            this.ColumnsKeys = columnsKeys;
        }

        public KeyedMatrix(List<TRowKey> rowsKeys, List<TColKey> columnsKeys, MyMatrix<TVal> content)
        :this(rowsKeys, columnsKeys)
        {
            for (int i = 0; i < content.Height; i++)
            {
                for (int j = 0; j < content.Width; j++)
                {
                    this[i, j] = content[i,j].Clone();
                }
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedMatrix{TRowKey,TColKey,TVal}"/> class.
        /// </summary>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        public KeyedMatrix(KeyedMatrix<TRowKey, TColKey, TVal> matrix)
            : base(matrix)
        {
            // This should implement in some way copying of lists BUT HOW THE FUCK CLONE INT.
            //this.RowsKeys = typeof(TRowKey).IsValueType ? new List<TRowKey>(matrix.RowsKeys) : matrix.RowsKeys.ConvertAll(x => (TRowKey)x.Clone()).ToList();
            ////this.RowsKeys = matrix.RowsKeys.Select(x => (THeightKey)x.Clone()).ToList();
            // this.ColumnsKeys = matrix.ColumnsKeys.Select(x => (TWidthKey)x.Clone()).ToList();
        }

        /// <summary>
        /// The height.
        /// </summary>
        public override int Height => this.RowsKeys.Count;

        /// <summary>
        /// The width.
        /// </summary>
        public override int Width => this.ColumnsKeys.Count;

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="hKey">
        /// The h key.
        /// </param>
        /// <param name="wKey">
        /// The w key.
        /// </param>
        /// <returns>
        /// The <see cref="Value"/>.
        /// </returns>
        public Value<TVal> this[TRowKey hKey, TColKey wKey]
        {
            get => this[this.RowsKeys.IndexOf(hKey), this.ColumnsKeys.IndexOf(wKey)];
            set => this[this.RowsKeys.IndexOf(hKey), this.ColumnsKeys.IndexOf(wKey)] = value;
        }
        public static KeyedMatrix<TRowKey, TColKey, TVal> operator *(KeyedMatrix<TRowKey, TColKey, TVal> b, Value<TVal> scalar)
        {
            return b.MultiplyByScalar(scalar);
        }

        public static KeyedMatrix<TRowKey, TColKey, TVal> operator +(
            KeyedMatrix<TRowKey, TColKey, TVal> a,
            KeyedMatrix<TRowKey, TColKey, TVal> b)
        {
            return a.Add(b);
        }

        protected KeyedMatrix<TRowKey, TColKey, TVal> Add(MyMatrix<TVal> keyedMatrix)
        {
            MyMatrix<TVal> addedMatrices = base.Add(keyedMatrix);
            return new KeyedMatrix<TRowKey, TColKey, TVal>(this.RowsKeys, this.ColumnsKeys, addedMatrices);
        }

        public KeyedMatrix<TColKey, TRowKey, TVal> GetRow(TRowKey key, bool copy = true)
        {
            MyMatrix<TVal> vector = base.GetRow(this.RowsKeys.IndexOf(key));

            return new KeyedMatrix<TColKey, TRowKey, TVal>(
                this.ColumnsKeys, /*Enumerable.Range(0, vector.Height).ToList()*/
                new List <TRowKey> { key },
                vector);
        }

        public KeyedMatrix<TRowKey, TColKey,  TVal> GetColumn(TColKey key, bool copy = true)
        {
            MyMatrix<TVal> vector = base.GetColumn(this.ColumnsKeys.IndexOf(key));

            return new KeyedMatrix<TRowKey, TColKey, TVal>(this.RowsKeys, new List <TColKey> { key } /*Enumerable.Range(0, vector.Height).ToList()*/, vector);
        }

        protected new KeyedMatrix<TRowKey, TColKey, TVal> MultiplyByScalar(Value<TVal> scalar) // TODO: ADD Copy?
        {
            var result = new KeyedMatrix<TRowKey, TColKey, TVal>(this.RowsKeys, this.ColumnsKeys);//TODO: This should have copy of lists
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    result[i, j] = this[i, j] * scalar;
                }
            }

            return result;
        }

        /// <summary>
        /// The get random normalized.
        /// </summary>
        /// <param name="heightKeys">
        /// The height keys.
        /// </param>
        /// <param name="widthKeys">
        /// The width heys.
        /// </param>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <param name="seed">
        /// The seed.
        /// </param>
        /// <returns>
        /// The <see cref="KeyedMatrix"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public static KeyedMatrix<TRowKey, TColKey, TVal> GetRandomNormalized(
            List<TRowKey> heightKeys,
            List<TColKey> widthKeys,
            NormalizedMethod method,
            int? seed = null)
        {
            KeyedMatrix<TRowKey, TColKey, TVal> keyedMatrix =
                new KeyedMatrix<TRowKey, TColKey, TVal>(heightKeys, widthKeys);
            if (seed == null)
            {
                seed = Guid.NewGuid().GetHashCode();
            }

            switch (method)
            {
                // TODO: PLIS REFACTOR ME!
                case NormalizedMethod.NormalizeColumns:
                    {
                        for (var j = 0; j < keyedMatrix.Width; j++)
                        {
                            Value<TVal>[] randomNormalizedArray = ValueUtilities<TVal>
                                .RandomNormalizedArray(keyedMatrix.Height, new TVal().SetValue(1), seed).ToArray(); // TODO: FIX ME PLIS, GETTING ALL THE SAME VALUES
                            for (var i = 0; i < keyedMatrix.Height; i++)
                            {
                                keyedMatrix.Matrix[i, j] = randomNormalizedArray[i];
                            }
                        }

                        break;
                    }

                case NormalizedMethod.NormalizeRow:
                    {
                        for (var j = 0; j < keyedMatrix.Height; j++)
                        {
                            Value<TVal>[] randomNormalizedArray = ValueUtilities<TVal>
                                .RandomNormalizedArray(keyedMatrix.Width, new TVal().SetValue(1), seed).ToArray();
                            for (var i = 0; i < keyedMatrix.Width; i++)
                            {
                                keyedMatrix.Matrix[j, i] = randomNormalizedArray[i];
                            }
                        }

                        break;
                    }

                default:
                    throw new ArgumentException("What?");
            }

            return keyedMatrix;
        }


    }

    /// <summary>
    /// The normalized method.
    /// </summary>
    public enum NormalizedMethod
    {
        /// <summary>
        /// The normalize row.
        /// </summary>
        NormalizeRow,

        /// <summary>
        /// The normalize columns.
        /// </summary>
        NormalizeColumns
    }
}