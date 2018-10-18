using System;

namespace Accountant.Models.Enums
{
    public abstract class Base<T> : IComparable<T>, IComparable
        where T : Base<T>
    {
        private static int _value = 0;
        private readonly int value;
        private readonly string name;

        private Base() { }
        protected Base(string name)
        {
            this.name = name ?? throw new ArgumentNullException("Base name cannot be null");
            this.value = _value++;
        }

        public override string ToString()
        {
            return this.name;
        }

        public static bool operator==(Base<T> a, Base<T> b)
        {
            return a?.name == b?.name;
        }

        public static bool operator!=(Base<T> a, Base<T> b)
        {
            return a?.name != b?.name;
        }

        public override bool Equals(object obj)
        {
            T other = obj as T;
            return this.name.Equals(other?.name);
        }

        public override int GetHashCode()
        {
            return (_value, value, name).GetHashCode();
        }

        public int CompareTo(object obj)
        {
            T other = obj as T;

            return this.CompareTo(other);
        }

        public int CompareTo(T obj)
        {
            return this.value.CompareTo(obj.value);
        }
    }
}