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
    /// <typeparam name="THeightKey">
    /// </typeparam>
    /// <typeparam name="TWidthKey">
    /// </typeparam>
    /// <typeparam name="TVal">
    /// </typeparam>
    public class KeyedMatrix<THeightKey, TWidthKey, TVal> : MyMatrix<TVal>
        where TVal : Value<TVal>, new()
    {
        /// <summary>
        /// The columns keys.
        /// </summary>
        public readonly List<TWidthKey> ColumnsKeys;

        /// <summary>
        /// The rows keys.
        /// </summary>
        public readonly List<THeightKey> RowsKeys;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedMatrix{THeightKey,TWidthKey,TVal}"/> class.
        /// </summary>
        /// <param name="rowsKeys">
        /// The rows keys.
        /// </param>
        /// <param name="columnsKeys">
        /// The columns keys.
        /// </param>
        public KeyedMatrix(List<THeightKey> rowsKeys, List<TWidthKey> columnsKeys)
            : base(columnsKeys.Count, rowsKeys.Count)
        {
            this.RowsKeys = rowsKeys;
            this.ColumnsKeys = columnsKeys;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedMatrix{THeightKey,TWidthKey,TVal}"/> class.
        /// </summary>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        public KeyedMatrix(KeyedMatrix<THeightKey, TWidthKey, TVal> matrix)
            : base(matrix)
        {
            // This should implement in some way copying of lists BUT HOW THE FUCK CLONE INT.
            // this.RowsKeys = typeof(THeightKey).IsValueType ? new List<THeightKey>(matrix.RowsKeys) : matrix.RowsKeys.ConvertAll(x => (THeightKey)x.Clone()).ToList();
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
        public virtual Value<TVal> this[THeightKey hKey, TWidthKey wKey]
        {
            get => this[this.RowsKeys.IndexOf(hKey), this.ColumnsKeys.IndexOf(wKey)];
            set => this[this.RowsKeys.IndexOf(hKey), this.ColumnsKeys.IndexOf(wKey)] = value;
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
        public static KeyedMatrix<THeightKey, TWidthKey, TVal> GetRandomNormalized(
            List<THeightKey> heightKeys,
            List<TWidthKey> widthKeys,
            NormalizedMethod method,
            int? seed = null)
        {
            KeyedMatrix<THeightKey, TWidthKey, TVal> keyedMatrix =
                new KeyedMatrix<THeightKey, TWidthKey, TVal>(heightKeys, widthKeys);
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