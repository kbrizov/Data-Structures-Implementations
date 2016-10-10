using System;
using System.Collections.Generic;

namespace Tree.Implementation
{
    public class TreeNode<T>
    {
        private TreeNode<T> parent;
        private T value;
        private IList<TreeNode<T>> childNodes;

        public TreeNode(T value)
        {
            this.Parent = null;
            this.Value = value;
            this.ChildNodes = new List<TreeNode<T>>();
        }

        public TreeNode(T value, params TreeNode<T>[] childNodes) : this(value)
        {
            foreach (var node in childNodes)
            {
                this.AddChild(node);
            }
        }

        public TreeNode<T> Parent
        {
            get
            {
                return this.parent;
            }

            private set
            {
                this.parent = value;
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

        public IList<TreeNode<T>> ChildNodes
        {
            get
            {
                return this.childNodes;
            }

            private set
            {
                this.childNodes = value;
            }
        }

        public bool HasParent
        {
            get
            {
                return (this.parent != null);
            }
        }

        public int ChildrenCount
        {
            get
            {
                return this.childNodes.Count;
            }
        }

        public TreeNode<T> GetChild(int index)
        {
            return this.ChildNodes[index];
        }

        public void AddChild(TreeNode<T> child)
        {
            if (child == null)
            {
                throw new ArgumentNullException("Cannot add null value.");
            }

            if (child.HasParent)
            {
                throw new InvalidOperationException("The new node already has a parent.");
            }

            child.Parent = this;
            this.childNodes.Add(child);
        }

        public override bool Equals(object obj)
        {
            TreeNode<T> node = obj as TreeNode<T>;

            if (node == null)
            {
                return false;
            }

            if (!Object.Equals(this.Value, node.Value))
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
