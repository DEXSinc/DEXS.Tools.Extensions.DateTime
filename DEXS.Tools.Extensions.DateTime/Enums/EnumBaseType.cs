using System.Collections.Generic;
using System.Linq;

namespace DEXS.Tools.Extensions.DateTime.Enums
{
    public abstract class EnumBaseType<T> where T : EnumBaseType<T>, IEnumType
    {
        protected static List<T> EnumValues = new List<T>();

        protected EnumBaseType(int key, string value, string description)
        {
            Key = key;
            Value = value;
            Description = description;
            EnumValues.Add((T)this);
        }

        public int Key { get; private set; }
        public string Value { get; private set; }
        public string Description { get; private set; }

        protected static IEnumerable<T> GetBaseValues()
        {
            return EnumValues.AsReadOnly();
        }

        protected static T GetBaseByKey(int key)
        {
            return EnumValues.FirstOrDefault(t => t.Key == key);
        }

        protected static T GetBaseByValue(string value)
        {
            return EnumValues.FirstOrDefault(t => t.Value == value);
        }

        protected static T GetBaseByDescription(string description)
        {
            return EnumValues.FirstOrDefault(t => t.Description == description);
        }

        public override string ToString()
        {
            return Value;
        }

        protected bool Equals(EnumBaseType<T> other)
        {
            return Key == other.Key && string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == GetType() && Equals((EnumBaseType<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Key * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }
    }
}
