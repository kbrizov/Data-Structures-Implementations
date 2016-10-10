using System;
using System.Linq;

namespace DynamicQueue.Implementation
{
    public class DynamicQueue<T>
    {
        private Node firstNode;
        private Node lastNode;
        private int count;

        public DynamicQueue()
        {
            this.firstNode = null;
            this.lastNode = null;
            this.count = 0;
        }

        public int Count 
        {
            get
            {
                return this.count;
            }
        }

        public void Enqueue(T value)
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
            }

            this.count++;
        }

        public T Dequeue()
        {
            if (this.firstNode == null)
            {
                throw new InvalidOperationException("Cannot dequeue, the queue is already empty.");
            }
            else
            {
                T value = this.firstNode.Value;
                this.firstNode = this.firstNode.NextNode;
                this.count--;

                return value;
            }
        }

        public T Peek()
        {
            if (this.firstNode == null)
            {
                throw new InvalidOperationException("Cannot dequeue, the queue is already empty.");
            }

            return this.firstNode.Value;
        }

        public void Clear()
        {
            this.firstNode = null;
            this.lastNode = null;
            this.count = 0;
        }

        public bool Contains(T value)
        {
            Node currentNode = this.firstNode;
            
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(value))
                {
                    return true;
                }

                currentNode = currentNode.NextNode;
            }

            return false;
        }

        private class Node
        {
            private T value;
            private Node nextNode;

            public Node(T value)
            {
                this.value = value;
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

            public T Value
            {
                get
                {
                    return this.value;
                }

                set
                {
                    this.value = value;
                }
            }
        }
    }
}
