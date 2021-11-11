using System;
using System.Collections;
using System.Collections.Generic;

namespace myDictionary
{
    class Dict<TKey, TValue> : IEnumerable
    {
        private int size = 5;
        private Item<TKey, TValue>[] Items;
        private List<TKey> Keys = new List<TKey>();
        private int GetHash(TKey key)
        {
            return key.GetHashCode() % size;
        }

        private Item<TKey, TValue>[] Augmentation(Item<TKey, TValue>[] items)
        {
            size *= 2;
            Items = new Item<TKey, TValue>[size];
            for(var i = 0; i < items.Length; i++)
            {
                Items[i] = items[i];
            }
            return Items;
        }

        public Dict()
        {
            Items = new Item<TKey, TValue>[size];
        }

        public void Add(Item<TKey, TValue> item)
        {
            var hash = GetHash(item.Key);

            if (Keys.Contains(item.Key))
            {
                Console.WriteLine($"Warning: Element with key \"{item.Key}\" are used");
                return;
            }

            if (Items[hash] == null)
            {
                Keys.Add(item.Key);
                Items[hash] = item;
                return;
            }
            else
            {
                var placed = false;
                for (var i = hash; i < size; i++)
                {
                    if (Items[i] == null)
                    {
                        Keys.Add(item.Key);
                        Items[i] = item;
                        return;
                    }

                    if (Items[i].Key.Equals(item.Key))
                    {
                        Console.WriteLine($"Warning: Element with key \"{item.Key}\" are used");
                        return;
                    }
                }

                if (!placed)
                {
                    for (var i = 0; i < hash; i++)
                    {
                        if (Items[i] == null)
                        {
                            Keys.Add(item.Key);
                            Items[i] = item;
                            return;
                        }

                        if (Items[i].Key.Equals(item.Key))
                        {
                            Console.WriteLine($"Warning: Element with key \"{item.Key}\" are used");
                            return;
                        }
                    }
                }

                if (!placed)
                {
                    Items = Augmentation(Items);
                    Add(item);
                    Console.WriteLine($"The size of the dictionary has doubled, size = {size}");
                    return;
                }
            }
        }

        public void Remove(TKey key)
        {
            var hash = GetHash(key);

            // Проверка наличия ключа в списке ключей
            if (!Keys.Contains(key))
            {
                return;
            }

            // Проверка на совпадение объекта по ключу
            if (Items[hash].Key.Equals(key))
            {
                Items[hash] = null;
                Keys.Remove(key);
                return;
            }

            // поиск нужного объекта по всему массиву(если первый объект(по хэшу) типа "null")
            if (Items[hash] == null)
            {
                for (var i = hash; i < size; i++)
                {
                    if (Items[i] != null && Items[i].Key.Equals(key))
                    {
                        Items[i] = null;
                        Keys.Remove(key);
                        return;
                    }
                }

                for (var i = 0; i < hash; i++)
                {
                    if (Items[i] != null && Items[i].Key.Equals(key))
                    {
                        Items[i] = null;
                        Keys.Remove(key);
                        return;
                    }
                }
                return;
            }

            // поиск нужного объекта по всему массиву(если объект(по хэшу) непустой но ключи не совпадают)
            else
            {
                for (var i = hash; i < size; i++)
                {
                    if (Items[i] == null)
                    {
                        continue;
                    }

                    if (Items[i].Key.Equals(key))
                    {
                        Items[i] = null;
                        Keys.Remove(key);
                        return;
                    }
                }

                for (var i = 0; i < hash; i++)
                {
                    if (Items[i] == null)
                    {
                        continue;
                    }

                    if (Items[i].Key.Equals(key))
                    {
                        Items[i] = null;
                        Keys.Remove(key);
                        return;
                    }
                }
            }
        }

        public TValue Search(TKey key)
        {
            var hash = GetHash(key);

            if (!Keys.Contains(key))
            {
                return default(TValue);
            }

            //прямая проверка
            if (Items[hash].Key.Equals(key))
            {
                return Items[hash].Value;
            }

            // Проверка всех елементов массива, если по хэшу "null"
            if (Items[hash] == null)
            {
                foreach (var item in Items)
                {
                    if (item.Key.Equals(key))
                    {
                        return item.Value;
                    }
                }

                return default(TValue);
            }

            else
            {
                for (var i = hash; i < size; i++)
                {
                    if (Items[i] == null)
                    {
                        continue;
                    }

                    if (Items[i].Key.Equals(key))
                    {
                        return Items[i].Value;
                    }
                }

                for (var i = 0; i < hash; i++)
                {
                    if (Items[i] == null)
                    {
                        continue;
                    }
                
                    if (Items[i].Key.Equals(key))
                    {
                        return Items[i].Value;
                    }
                }
            }
            return default;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in Items)
            {
                if (item != null)
                {
                    yield return item;
                }
            }
        }
    }
}