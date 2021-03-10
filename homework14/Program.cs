using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace homework14
{
    internal static class Program
    {
        private static void Main()
        {
            // 1
            var list = new MyList<int> {1, 2, 3};
            list[0] = 4;
            list.Add(9);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"total: {list.Count}");

            //2
            var dictionary = new MyDictionary<int, int> {[1] = 0};
            dictionary.Add(2, 5);
            dictionary.Add(3, 6);
            foreach (var (key, value) in dictionary)
            {
                Console.WriteLine($"key: {key}; value: {value}");
            }

            //throw Exception on redeclare key: 6
            dictionary.Add(3, 7);
        }
    }

    internal class MyList<T> : IEnumerable<T>
    {
        private T[] _array = Array.Empty<T>();

        public void Add(T element)
        {
            Array.Resize(ref _array, _array.Length + 1);
            _array[^1] = element;
        }

        public T this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }

        public int Count => _array.Length;

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) _array).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private readonly MyList<KeyValuePair<TKey, TValue>> _name = new();

        public void Add(TKey key, TValue value)
        {
            if (_name.Any(k => k.Key.Equals(key)))
            {
                throw new Exception("Duplicate key");
            }

            _name.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public TValue this[TKey index]
        {
            get
            {
                foreach (var key in _name.Where(key => key.Key.Equals(index)))
                    return key.Value;
                throw new KeyNotFoundException();
            }
            set => Add(index, value);
        }

        public int Count => _name.Count;

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _name.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}