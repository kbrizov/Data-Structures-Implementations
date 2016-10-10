using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinarySearchTree.Implementation
{
    public class BinarySearchTree<T> : ICloneable, IEnumerable<T> where T : IComparable<T>
    {
        private BinaryTreeNode<T> root;

        public BinarySearchTree()
        {
            this.root = null;
        }

        /// <summary>
        /// Inserts new value in the binary search tree
        /// </summary>
        /// <param name="value">the value to be inserted</param>
        public void Insert(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Cannot insert null value!");
            }

            this.root = Insert(value, null, root);
        }

        /// <summary>
        /// Removes an element from the tree if it exists
        /// </summary>
        /// <param name="value">the value to be deleted</param>
        public void Remove(T value)
        {
            BinaryTreeNode<T> nodeToDelete = Find(value);
            if (nodeToDelete == null)
            {
                return;
            }
            Remove(nodeToDelete);
        }

        public BinarySearchTree<T> Clone()
        {
            BinarySearchTree<T> treeClone = new BinarySearchTree<T>();
            this.TraverseInOrder(this.root, x => treeClone.Insert(x.value));
            //this.CloneTree(this.root, treeClone);

            return treeClone;
        }

        object ICloneable.Clone()
        {
            object cloneAsObject = this.Clone();

            return cloneAsObject;
        }

        public IEnumerator<T> GetEnumerator()
        {
            IList<T> elements = new List<T>();
            TraverseInOrder(this.root, x => elements.Add(x.value));

            foreach (var element in elements)
            {
                yield return element;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            BinarySearchTree<T> otherTree = obj as BinarySearchTree<T>;
            if (otherTree == null)
            {
                return false;
            }

            bool areEqual = TraverseAndCompareNodes(this.root, otherTree.root);

            return areEqual;
        }

        public override int GetHashCode()
        {
            int treeHashCode = 0;
            TraverseInOrder(this.root, x => treeHashCode ^= x.GetHashCode());

            return treeHashCode;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (var item in this)
            {
                result.AppendFormat("{0} ", item.ToString());
            }

            return result.ToString();
        }

        public static bool operator ==(BinarySearchTree<T> firstTree, BinarySearchTree<T> secondTree)
        {
            return object.Equals(firstTree, secondTree);
        }

        public static bool operator !=(BinarySearchTree<T> firstTree, BinarySearchTree<T> secondTree)
        {
            return !object.Equals(firstTree, secondTree);
        }

        /// <summary>
        /// Inserts node in the binary search tree by given value
        /// </summary>
        /// <param name="value">the new value</param>
        /// <param name="parentNode">the parent of the new node</param>
        /// <param name="node">current node</param>
        /// <returns>the inserted node</returns>
        private BinaryTreeNode<T> Insert(T value, BinaryTreeNode<T> parentNode, BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                node = new BinaryTreeNode<T>(value);
                node.parent = parentNode;
            }
            else
            {
                int compareTo = value.CompareTo(node.value);
                if (compareTo < 0)
                {
                    node.leftChild =
                        Insert(value, node, node.leftChild);
                }
                else if (compareTo > 0)
                {
                    node.rightChild =
                        Insert(value, node, node.rightChild);
                }
            }

            return node;
        }

        /// <summary>
        /// Finds a given value in the tree and returns the node
        /// which contains it if such exsists
        /// </summary>
        /// <param name="value">the value to be found</param>
        /// <returns>the found node or null if not found</returns>
        private BinaryTreeNode<T> Find(T value)
        {
            BinaryTreeNode<T> node = this.root;
            while (node != null)
            {
                int compareTo = value.CompareTo(node.value);
                if (compareTo < 0)
                {
                    node = node.leftChild;
                }
                else if (compareTo > 0)
                {
                    node = node.rightChild;
                }
                else
                {
                    break;
                }
            }

            return node;
        }

        private void Remove(BinaryTreeNode<T> node)
        {
            // Case 3: If the node has two children.
            // Note that if we get here at the end
            // the node will be with at most one child
            if (node.leftChild != null && node.rightChild != null)
            {
                BinaryTreeNode<T> replacement = node.rightChild;
                while (replacement.leftChild != null)
                {
                    replacement = replacement.leftChild;
                }
                node.value = replacement.value;
                node = replacement;

            }
            // Case 1 and 2: If the node has at most one child
            BinaryTreeNode<T> theChild = node.leftChild != null ?
            node.leftChild : node.rightChild;
            // If the element to be deleted has one child
            if (theChild != null)
            {
                theChild.parent = node.parent;
                // Handle the case when the element is the root
                if (node.parent == null)
                {
                    root = theChild;
                }
                else
                {
                    // Replace the element with its child subtree
                    if (node.parent.leftChild == node)
                    {
                        node.parent.leftChild = theChild;
                    }
                    else
                    {
                        node.parent.rightChild = theChild;
                    }
                }
            }
            else
            {
                // Handle the case when the element is the root
                if (node.parent == null)
                {
                    root = null;
                }
                else
                {
                    // Remove the element - it is a leaf
                    if (node.parent.leftChild == node)
                    {
                        node.parent.leftChild = null;
                    }
                    else
                    {
                        node.parent.rightChild = null;
                    }
                }
            }
        }

        ///// <summary>
        ///// Clones a tree by a start node and stores the resulting tree in the treeClone passed as parameter.
        ///// </summary>
        ///// <param name="startNode">The node to start from.</param>
        ///// <param name="treeClone">The tree that will hold the result.</param>
        //private void CloneTree(BinaryTreeNode<T> startNode, BinarySearchTree<T> treeClone)
        //{

        //    if (startNode == null)
        //    {
        //        return;
        //    }

        //    treeClone.Insert(startNode.value);
        //    CloneTree(startNode.leftChild, treeClone);
        //    CloneTree(startNode.rightChild, treeClone);

        //}

        /// <summary>
        /// Traverses and compares subtrees of two trees by using BinaryTreeNode.Equals(object obj)
        /// </summary>
        /// <param name="firstStartNode">The start node of the first tree</param>
        /// <param name="secondStartNode">The start node of the second tree</param>
        /// <returns></returns>
        private bool TraverseAndCompareNodes(BinaryTreeNode<T> firstNode, BinaryTreeNode<T> secondNode)
        {
            if (firstNode == null && secondNode == null)
            {
                return true;
            }

            if (object.Equals(firstNode, secondNode))
            {
                bool result = 
                    TraverseAndCompareNodes(firstNode.leftChild, secondNode.leftChild) &&
                    TraverseAndCompareNodes(firstNode.rightChild, secondNode.rightChild);

                return result;
            }

            return false;
        }

        //private int CalculateTreeHashCode(BinaryTreeNode<T> startNode)
        //{
        //    if (startNode == null)
        //    {
        //        return 0;
        //    }

        //    int hashCode = startNode.GetHashCode();
        //    hashCode += hashCode ^ this.CalculateTreeHashCode(startNode.leftChild);
        //    hashCode += hashCode ^ this.CalculateTreeHashCode(startNode.rightChild);

        //    return hashCode;
        //}

        private void TraverseInOrder(BinaryTreeNode<T> node, Action<BinaryTreeNode<T>> action)
        {
            if (node != null)
            {
                TraverseInOrder(node.leftChild, action);
                action(node);
                TraverseInOrder(node.rightChild, action);
            }
        }

        private class BinaryTreeNode<T> : IComparable<BinaryTreeNode<T>> where T : IComparable<T>
        {
            internal T value;

            internal BinaryTreeNode<T> parent;

            internal BinaryTreeNode<T> leftChild;

            internal BinaryTreeNode<T> rightChild;

            public BinaryTreeNode(T value)
            {
                this.value = value;
                this.parent = null;
                this.leftChild = null;
                this.rightChild = null;
            }

            public override string ToString()
            {
                return this.value.ToString();
            }

            public override int GetHashCode()
            {
                return this.value.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    return false;
                }

                BinaryTreeNode<T> otherNode = obj as BinaryTreeNode<T>;
                if (otherNode == null)
                {
                    return false;
                }

                return this.CompareTo(otherNode) == 0;
            }

            public int CompareTo(BinaryTreeNode<T> other)
            {
                return this.value.CompareTo(other.value);
            }
        }
    }
}