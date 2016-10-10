using System;
using System.Collections;
using System.Collections.Generic;

namespace StaticList.Implementation
{
    public class StaticList<T> : IList<T>
    {
        private const int INITIAL_CAPACITY = 4;
        private T[] array;
        private int count;

        public StaticList()
        {
            this.array = new T[INITIAL_CAPACITY];
            this.count = 0;
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > this.Count)
                {
                    throw new IndexOutOfRangeException(
                        string.Format("Invalid index: {0}.", index));
                }

                return this.array[index];
            }

            set
            {
                if (index < 0 || index > this.Count)
                {
                    throw new IndexOutOfRangeException(
                        string.Format("Invalid index: {0}.", index));
                }

                this.array[index] = value;
            }
        }

        public void Add(T item)
        {
            this.Insert(this.Count, item);
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > this.Count)
            {
                throw new IndexOutOfRangeException(
                    string.Format("Invalid index: {0}.", index));
            }

            T[] extendedArr = new T[this.array.Length + 1];
            if (this.Count + 1 > this.array.Length)
            {
                extendedArr = new T[this.array.Length * 2];
            }

            // Copying the part of the array before the inserted element.
            Array.Copy(this.array, extendedArr, index);

            extendedArr[index] = item;
            this.count++;

            // Copying the rest of the array, after the inserted element.
            Array.Copy(this.array, index, extendedArr, index + 1, this.array.Length - index - 1);

            this.array = extendedArr;
        }

        public void Clear()
        {
            this.array = new T[INITIAL_CAPACITY];
            this.count = 0;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.array.Length; i++)
            {
                if (item.Equals(this.array[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Contains(T item)
        {
            if (this.IndexOf(item) != -1)
            {
                return true;
            }

            return false;
        }

        public bool Remove(T item)
        {
            bool isRemoved = false;
            if (this.Contains(item))
            {
                int oldCount = this.Count;
                int indexOfItem = this.IndexOf(item);
                this.RemoveAt(indexOfItem);
                if (this.Count == oldCount - 1)
                {
                    isRemoved = true;
                }
            }

            return isRemoved;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > this.Count)
            {
                throw new IndexOutOfRangeException(
                    string.Format("Invalid index: {0}.", index));
            }

            T[] shrunkArr = new T[this.array.Length - 1];
            Array.Copy(this.array, shrunkArr, index);
            Array.Copy(this.array, index + 1, shrunkArr, index, this.array.Length - index - 1);

            this.array = shrunkArr;
        }

        public void CopyTo(T[] destinationArray, int startIndex)
        {
            if (destinationArray == null)
            {
                throw new ArgumentNullException("The destination array cannot be null.");
            }

            if (startIndex < 0 || startIndex > this.Count)
            {
                throw new IndexOutOfRangeException(
                    string.Format("Invalid index: {0}.", startIndex));
            }

            Array.Copy(this.array, destinationArray, startIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
