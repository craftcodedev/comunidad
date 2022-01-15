using System.Collections.Generic;
using System.Linq;

namespace App
{
    public abstract class ValueObject<T>
    {
        public T Value { get; }

        public ValueObject(T value)
        {
            Value = value;
        }

        protected IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject<T> other = (ValueObject<T>)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^
                    ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }

                if (thisValues.Current != null &&
                    !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }
            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }
    }

    public sealed class UserFirstName : ValueObject<string>
    {
        public const int MIN_LENGTH = 3;
        public const int MAX_LENGTH = 40;

        public UserFirstName(string value) : base(value)
        {
            if (value.Length < MIN_LENGTH)
            {
                throw InvalidAttributeException.FromMinLength("first name", MIN_LENGTH);
            }

            if (value.Length > MAX_LENGTH)
            {
                throw InvalidAttributeException.FromMaxLength("first name", MAX_LENGTH);
            }
        }
    }
}

