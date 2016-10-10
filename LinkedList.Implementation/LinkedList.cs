using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList.Implementation
{
    public class LinkedList<T> : IList<T>
    {
        private Node firstNode;
        private Node lastNode;
        private int count;

        public LinkedList()
        {
            this.firstNode = null;
            this.lastNode = null;
            this.Count = 0;
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
                if (index < 0 || index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException("Invalid index : " + index);
                }

                Node currentNode = this.GetNodeAt(index);
                return currentNode.Content;
            }

            set
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new ArgumentOutOfRangeException("Invalid index : " + index);
                }

                Node currentNode = this.GetNodeAt(index);
                currentNode.Content = value;
            }
        }

        public void Add(T value)
        {
            if (this.firstNode == null)
            {
                this.firstNode = new Node(value);
                this.lastNode = this.firstNode;
            }
            else
            {
                Node newLastNode = new Node(value, this.lastNode);
                this.lastNode = newLastNode;
                newLastNode.NextNode = null;
            }

            this.Count++;
        }

        public void Insert(int index, T value)
        {
            if (this.firstNode == null)
            {
                throw new InvalidOperationException("Cannot insert elements in an empty list.");
            }

            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException("Invalid index : " + index);
            }

            if (index == 0)
            {
                Node newfirstNode = new Node(value);
                newfirstNode.NextNode = this.firstNode;
                this.firstNode = newfirstNode;
            }
            else
            {
                Node previousNode = this.GetNodeAt(index - 1);
                Node newNode = new Node(value);
                newNode.NextNode = previousNode.NextNode;
                previousNode.NextNode = newNode;
            }

            this.Count++;
        }

        public bool Remove(T item)
        {
            if (this.Count == 1)
            {
                this.Clear();

                return true;
            }
            else
            {
                int indexOfItem = this.IndexOf(item);
                if (indexOfItem != -1)
                {
                    this.RemoveAt(indexOfItem);

                    return true;
                }
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (index == 0)
            {
                this.firstNode = this.firstNode.NextNode;
            }
            else
            {
                Node prevNode = this.GetNodeAt(index - 1);
                Node nodeToBeRemoved = prevNode.NextNode;
                prevNode.NextNode = nodeToBeRemoved.NextNode;
                nodeToBeRemoved.NextNode = null;
            }

            this.Count--;
        }

        public void Clear()
        {
            this.firstNode = null;
            this.lastNode = null;
            this.Count = 0;
        }

        public int IndexOf(T value)
        {
            int index = 0;
            Node currentNode = this.firstNode;
            while (currentNode != null)
            {
                if (currentNode.Content.Equals(value))
                {
                    return index;
                }

                currentNode = currentNode.NextNode;
                index++;
            }

            return -1;
        }

        public bool Contains(T value)
        {
            if (this.IndexOf(value) != -1)
            {
                return true;
            }

            return false;
        }

        public void CopyTo(T[] destinationArray, int startIndex)
        {
            if (destinationArray == null)
            {
                throw new ArgumentNullException("The destination array cannot be null.");
            }

            if (startIndex < 0 || startIndex >= this.Count)
            {
                throw new IndexOutOfRangeException(
                    string.Format("Invalid index:" + startIndex));
            }

            // Checking if the elements of the list can fit in the array.
            if (destinationArray.Length - 1 - startIndex < this.Count)
            {
                throw new ArgumentException(
                    "The elements being copied are too many and can't fit in the part of the array " +
                    "specified by the startIndex.");
            }

            Node currentNode = this.firstNode;
            for (int i = startIndex; i < destinationArray.Length; i++)
            {
                destinationArray[i] = currentNode.Content;
                currentNode = currentNode.NextNode;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node currentNode = this.firstNode;

            while (currentNode != null)
            {
                T result = currentNode.Content;
                currentNode = currentNode.NextNode;

                yield return result;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private Node GetNodeAt(int index)
        {
            int counter = 0;
            Node currentNode = this.firstNode;
            while (counter < index)
            {
                currentNode = currentNode.NextNode;
                counter++;
            }

            return currentNode;
        }

        private class Node
        {
            private T content;
            private Node nextNode;

            public Node(T value)
            {
                this.content = value;
                this.nextNode = null;
            }

            public Node(T value, Node previousNode) 
                : this(value)
            {
                previousNode.nextNode = this;
            }

            public Node NextNode
            {
                get
                {
                    return this.nextNode;
                }

                set
                {
                    this.nextNode = value;
                }
            }

            public T Content
            {
                get
                {
                    return this.content;
                }

                set
                {
                    this.content = value;
                }
            }
        }
    }
}
