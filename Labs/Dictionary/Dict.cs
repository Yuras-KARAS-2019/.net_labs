using System;
using System.Collections;
using System.Collections.Generic;

namespace myDictionary
{
    public class Dict<TKey, TValue> : IEnumerable<Item<TKey, TValue>>
    {
        private int _size = 128;
        private int _counter = 0;
        private Item<TKey, TValue>[] _items;
        public int Count => _counter;

        private int GetHash(TKey key)
        {
            return key.GetHashCode() % _size;
        }

        private void Augmentation()
        {
            var items = _items;
            _size *= 2;
            _items = new Item<TKey, TValue>[_size];
            for (var i = 0; i < items.Length; i++)
            {
                Add(items[i]);
            }
        }

        private int SearchIndex(TKey key)
        {
            var hash = GetHash(key);
            int i = hash;

            // Проверка на совпадение объекта по ключу
            while (_items[i] is not null)
            {
                if (_items[i].Key.Equals(key))
                {
                    return i;
                }
                i = (i + 1) % _size;
            }
            throw new KeyNotFoundException($"Warning: Key \"{key}\" does not exist");
        }

        public Dict()
        {
            _items = new Item<TKey, TValue>[_size];
        }

        public void Add(Item<TKey, TValue> item)
        {
            if ((_size / 2) < ++_counter)
            {
                Augmentation();
                Add(item);
                return;
            }
            var hash = GetHash(item.Key);

            if (_items[hash] is null)
            {
                _items[hash] = item;
            }
            else
            {
                for (int i = 0; i < _size; i++)
                {
                    var index = (hash + i) % _size;

                    if (_items[index] is null || _items[index].Key.Equals(item.Key))
                    {
                        _items[index] = item;
                        return;
                    }
                }
            }
        }

        public void Remove(TKey key)
        {
            int index = SearchIndex(key);
            _counter -= 1;
            _items[index] = null;
            index = (index + 1) % _size;

            var temp = new List<Item<TKey, TValue>>();

            while (_items[index] is not null)
            {
                temp.Add(_items[index]);
                _items[index] = null;
                index = (index + 1) % _size;
            }
            foreach (var elem in temp)
            {
                Add(elem);
            }
        }

        public TValue Search(TKey key) => _items[SearchIndex(key)].Value;

        public IEnumerator<Item<TKey, TValue>> GetEnumerator()
        {
            foreach (var item in _items)
            {
                if (item is not null)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public TValue this[TKey key] => Search(key);
    }
}