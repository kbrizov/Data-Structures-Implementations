using System;
using System.Linq;

namespace StaticStack.Implementation
{
    public class StaticStack<T>
    {
        private const int INITIAL_CAPACITY = 4;
        private T[] array;
        private int topIndex;
        private int count;
        private int capacity;

        public StaticStack()
        {
            this.array = new T[INITIAL_CAPACITY];
            this.topIndex = -1;
            this.count = 0;
            this.capacity = INITIAL_CAPACITY;
        }

        public int Count
        {
            get
            {
                return this.count;
            }

            private set
            {
                this.count = value;
            }
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }

            private set
            {
                this.capacity = value;
            }
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Cannot peek in empty stack.");
            }

            return this.array[this.topIndex];
        }

        public void Push(T item)
        {
            if (this.Count == this.Capacity)
            {
                this.ExpandBufferCapacity();
            }

            this.topIndex++;
            this.array[this.topIndex] = item;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Cannot pop from empty stack.");
            }

            T topElement = this.array[this.topIndex];
            this.topIndex--;
            this.Count--;

            return topElement;
        }

        public void Clear()
        {
            this.array = new T[INITIAL_CAPACITY];
            this.topIndex = -1;
            this.Capacity = this.array.Length;
            this.Count = 0;
        }

        public bool Contains(T item)
        {
            return this.array.Contains<T>(item);
        }

        public T[] ToArray()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The stack is empty, cannot convert to array.");
            }

            T[] clone = new T[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                clone[i] = this.array[i];
            }

            return clone;
        }

        private void ExpandBufferCapacity()
        {
            T[] resizedArray = new T[this.array.Length * 2];
            
            for (int i = 0; i < this.Count; i++)
            {
                resizedArray[i] = this.array[i];
            }

            this.Capacity = resizedArray.Length;
            this.array = resizedArray;
        }
    }
}
