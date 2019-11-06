#region copyright
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Value.cs">
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
    public abstract class Value<T> where T : Value<T>, new()
    {
        public static Value<T> operator +(Value<T> v1, Value<T> v2)
        {
            return v1.Add(v2);
        }
        public static Value<T> operator -(Value<T> v1, Value<T> v2)
        {
            return v1.Subtract(v2);
        }
        public static Value<T> operator /(Value<T> v1, Value<T> v2)
        {
            return v1.Divide(v2);
        }
        public static Value<T> operator *(Value<T> v1, Value<T> v2)
        {
            return v1.Multiply(v2);
        }
        public static bool operator ==(Value<T> v1, Value<T> v2)
        {
            return v1.Equals(v2);
        }
        public static bool operator !=(Value<T> v1, Value<T> v2)
        {
            return !v1.Equals(v2);
        }

        public abstract Value<T> Rand(int nominator);
        protected abstract Value<T> Add(Value<T> v2);
        protected abstract Value<T> Subtract(Value<T> v2);
        protected abstract Value<T> Divide(Value<T> v2);
        protected abstract Value<T> Multiply(Value<T> v2);
        protected abstract bool Equals(Value<T> v2);
    }
}