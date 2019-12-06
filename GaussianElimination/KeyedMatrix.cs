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
    using System.Linq;

    using GaussianElimination.DataTypes;

    #endregion

    public class KeyedMatrix<THeightKey, TWidthKey, TVal> : MyMatrix<TVal> 
        where THeightKey : ICloneable 
        where TWidthKey : ICloneable
        where TVal : Value<TVal>, new()
    {
        private readonly List<THeightKey> heightKeys;

        private readonly List<TWidthKey> widthKeys;

        public KeyedMatrix(int width, int height, List<THeightKey> heightKeys, List<TWidthKey> widthKeys)
            : base(width, height)
        {
            this.heightKeys = heightKeys;
            this.widthKeys = widthKeys;
        }

        public KeyedMatrix(KeyedMatrix<THeightKey, TWidthKey, TVal> matrix) : base(matrix)
        {
            this.heightKeys = matrix.heightKeys.Select(x => (THeightKey)x.Clone()).ToList();
            this.widthKeys = matrix.widthKeys.Select(x => (TWidthKey)x.Clone()).ToList();
        }
        public Value<TVal> GetCell(THeightKey userKey, TWidthKey productKey)
        {
            return this[this.heightKeys.IndexOf(userKey), this.widthKeys.IndexOf(productKey)];
        }
    }
}