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
    using System;
    using System.Collections.Generic;

    public class Value<T> : IEquatable<Value<T>>
    {
        public T Val { get; set; }

        public bool Equals(Value<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(this.Val, other.Val);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Value<T>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(this.Val);
        }

        public static bool operator ==(Value<T> left, Value<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Value<T> left, Value<T> right)
        {
            return !Equals(left, right);
        }

        public Value(T val)
        {
            Val = val;
        }

        public static Value<T> operator -(Value<T> v1, Value<T> v2)
        {
            return new Value<T>((dynamic)v1.Val - (dynamic)v2.Val);
        }
        public static Value<T> operator +(Value<T> v1, Value<T> v2)
        {
            return new Value<T>((dynamic)v1.Val + (dynamic)v2.Val);
        }
        public static Value<T> operator *(Value<T> v1, Value<T> v2)
        {
            return new Value<T>((dynamic)v1.Val * (dynamic)v2.Val);
        }
        public static Value<T> operator /(Value<T> v1, Value<T> v2)
        {
            return new Value<T>((dynamic)v1.Val / (dynamic)v2.Val);
        }
    }
}