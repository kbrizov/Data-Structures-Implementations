using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashTable.Implementation
{
    public class HashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private const int INITIAL_CAPACITY = 16;
        private const float DEFAULT_LOAD_FACTOR = 0.75f;
        private LinkedList<KeyValuePair<TKey, TValue>>[] table;
        private float loadFactor;
        private int threshold;
        private int count;
        private int capacity;

        public HashTable()
            : this(INITIAL_CAPACITY, DEFAULT_LOAD_FACTOR)
        {
        }

        public HashTable(int capacity, float loadFactor = DEFAULT_LOAD_FACTOR)
        {
            this.table = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];
            this.count = 0;
            this.capacity = this.table.Length;
            this.loadFactor = loadFactor;
            this.threshold = (int)(this.loadFactor * this.capacity);
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return this.GetKeys();
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return this.GetValues();
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.Get(key);
            }

            set
            {
                this.Set(key, value);
            }
        }

        public TValue Find(TKey key)
        {
            TValue value = this.Get(key);

            return value;
        }

        public void Add(TKey key, TValue value)
        {
            LinkedList<KeyValuePair<TKey, TValue>> currentBucket = this.FindBucket(key, true);
            foreach (var pair in currentBucket)
            {
                if (pair.Key.Equals(key))
                {
                    throw new ArgumentException("An item with the same key is already contained!");
                }
            }

            KeyValuePair<TKey, TValue> newEntry = new KeyValuePair<TKey, TValue>(key, value);
            currentBucket.AddLast(newEntry);
            this.count++;
            if (this.count >= this.threshold)
            {
                this.Expand();
            }
        }

        public bool Remove(TKey key)
        {
            bool removed = false;
            LinkedList<KeyValuePair<TKey, TValue>> currentBucket = this.FindBucket(key, false);

            KeyValuePair<TKey, TValue> toBeRemoved = null;
            if (currentBucket != null)
            {
                foreach (var pair in currentBucket)
                {
                    if (key.Equals(pair.Key))
                    {
                        toBeRemoved = pair;
                    }
                }

                currentBucket.Remove(toBeRemoved);
                this.count--;
                removed = true;
            }
            
            return removed;
        }

        public void Clear()
        {
            this.table = new LinkedList<KeyValuePair<TKey, TValue>>[INITIAL_CAPACITY];
            this.count = 0;
            this.capacity = this.table.Length;
            this.loadFactor = DEFAULT_LOAD_FACTOR;
            this.threshold = (int)(this.loadFactor * this.capacity);
        }

        public bool ContainsKey(TKey key)
        {
            LinkedList<KeyValuePair<TKey, TValue>> currentBucket = this.FindBucket(key, false);
            if (currentBucket != null)
            {
                foreach (var pair in currentBucket)
                {
                    if (key.Equals(pair.Key))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var bucket in this.table)
            {
                if (bucket != null)
                {
                    foreach (var pair in bucket)
                    {
                        yield return pair;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private ICollection<TKey> GetKeys()
        {
            LinkedList<TKey> keys = new LinkedList<TKey>();
            foreach (var bucket in this.table)
            {
                if (bucket != null)
                {
                    foreach (var pair in bucket)
                    {
                        keys.AddLast(pair.Key);
                    }
                }
            }

            return keys.ToArray();
        }

        private ICollection<TValue> GetValues()
        {
            LinkedList<TValue> values = new LinkedList<TValue>();
            foreach (var bucket in this.table)
            {
                if (bucket != null)
                {
                    foreach (var pair in bucket)
                    {
                        values.AddLast(pair.Value);
                    }
                }
            }

            return values.ToArray();
        }

        private TValue Get(TKey key)
        {
            LinkedList<KeyValuePair<TKey, TValue>> currentBucket = this.FindBucket(key, false);

            if (currentBucket == null)
            {
                throw new ArgumentException("No pair with the given key exists!");
            }

            foreach (var pair in currentBucket)
            {
                if (key.Equals(pair.Key))
                {
                    return pair.Value;
                }
            }

            return default(TValue);
        }

        private void Set(TKey key, TValue value)
        {
            LinkedList<KeyValuePair<TKey, TValue>> currentBucket = this.FindBucket(key, false);

            KeyValuePair<TKey, TValue> toBeUpdated = null;
            foreach (var pair in currentBucket)
            {
                if (key.Equals(pair.Key))
                {
                    toBeUpdated = pair;
                }
            }

            toBeUpdated.Value = value;
        }

        private void Expand()
        {
            int newCapacity = this.capacity * 2;
            LinkedList<KeyValuePair<TKey, TValue>>[] oldTable = this.table;
            this.table = new LinkedList<KeyValuePair<TKey, TValue>>[newCapacity];
            this.capacity = this.table.Length;
            this.threshold = (int)(this.capacity * this.loadFactor);

            foreach (LinkedList<KeyValuePair<TKey, TValue>> oldBucket in oldTable)
            {
                if (oldBucket != null)
                {
                    foreach (KeyValuePair<TKey, TValue> pair in oldBucket)
                    {
                        LinkedList<KeyValuePair<TKey, TValue>> bucket = this.FindBucket(pair.Key, true);
                        bucket.AddLast(pair);
                    }
                }
            }
        }

        private int GetKeyIndex(TKey key)
        {
            int hashCode = key.GetHashCode();
            int index = Math.Abs(hashCode % this.capacity);

            return index;
        }

        private LinkedList<KeyValuePair<TKey, TValue>> FindBucket(TKey key, bool createIfMissing)
        {
            int index = this.GetKeyIndex(key);

            if (this.table[index] == null && createIfMissing)
            {
                this.table[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }

            return this.table[index];
        }
    }
}
