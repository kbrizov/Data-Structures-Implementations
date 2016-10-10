﻿using System;

namespace CustomDataStructures
{
    // Denis' Min Heap
    public class MinHeap<T> where T : IComparable
    {
        private const int INITIAL_CAPACITY = 16;

        private T[] arr;
        private int lastItemIndex;

        public MinHeap()
            : this(INITIAL_CAPACITY)
        {
        }

        public MinHeap(int capacity)
        {
            this.arr = new T[capacity];
            this.lastItemIndex = -1;
        }

        public int Count
        {
            get
            {
                return this.lastItemIndex + 1;
            }
        }

        public void Add(T item)
        {
            if (this.lastItemIndex == this.arr.Length - 1)
            {
                this.Resize();
            }

            this.lastItemIndex++;
            this.arr[this.lastItemIndex] = item;

            this.MinHeapifyUp(this.lastItemIndex);
        }

        public T Remove()
        {
            if (this.lastItemIndex == -1)
            {
                throw new InvalidOperationException("The heap is empty");
            }

            T removedItem = this.arr[0];
            this.arr[0] = this.arr[this.lastItemIndex];
            this.lastItemIndex--;

            this.MinHeapifyDown(0);

            return removedItem;
        }

        public T Peek()
        {
            if (this.lastItemIndex == -1)
            {
                throw new InvalidOperationException("The heap is empty");
            }

            return this.arr[0];
        }

        public void Clear()
        {
            this.lastItemIndex = -1;
            this.arr = new T[INITIAL_CAPACITY];
        }

        private void MinHeapifyUp(int index)
        {
            if (index == 0)
            {
                return;
            }

            int childIndex = index;
            int parentIndex = (index - 1) / 2;

            if (this.arr[childIndex].CompareTo(this.arr[parentIndex]) < 0)
            {
                // swap the parent and the child
                T temp = this.arr[childIndex];
                this.arr[childIndex] = this.arr[parentIndex];
                this.arr[parentIndex] = temp;

                this.MinHeapifyUp(parentIndex);
            }
        }

        private void MinHeapifyDown(int index)
        {
            int leftChildIndex = index * 2 + 1;
            int rightChildIndex = index * 2 + 2;
            int smallestItemIndex = index; // The index of the parent

            if (leftChildIndex <= this.lastItemIndex &&
                this.arr[leftChildIndex].CompareTo(this.arr[smallestItemIndex]) < 0)
            {
                smallestItemIndex = leftChildIndex;
            }

            if (rightChildIndex <= this.lastItemIndex &&
                this.arr[rightChildIndex].CompareTo(this.arr[smallestItemIndex]) < 0)
            {
                smallestItemIndex = rightChildIndex;
            }

            if (smallestItemIndex != index)
            {
                // swap the parent with the smallest of the child items
                T temp = this.arr[index];
                this.arr[index] = this.arr[smallestItemIndex];
                this.arr[smallestItemIndex] = temp;

                this.MinHeapifyDown(smallestItemIndex);
            }
        }

        private void Resize()
        {
            T[] newArr = new T[this.arr.Length * 2];
            for (int i = 0; i < this.arr.Length; i++)
            {
                newArr[i] = this.arr[i];
            }

            this.arr = newArr;
        }
    }
}
