using System;
using System.Collections.Generic;
using System.Linq;

namespace Tree.Implementation
{
    public class Tree<T>
    {
        public TreeNode<T> Root { get; private set; }
        public ICollection<TreeNode<T>> Nodes { get; private set; }

        public Tree(TreeNode<T> root)
        {
            this.Root = root;
            this.Nodes = new HashSet<TreeNode<T>>();
            this.InitializeTree();
        }

        public TreeNode<T> GetNodeByValue(T value)
        {
            return this.Nodes.First(node => node.Value.Equals(value));
        }

        /// <summary>
        /// Traverses the tree using Depth-First-Search(DFS) by starting from the root and does specified action to it's nodes.
        /// </summary>
        /// <param name="action">The action to be performed to each node.</param>
        public void TraverseUsingDepthFirst(Action<TreeNode<T>> action)
        {
            this.TraverseUsingDepthFirst(this.Root, action);
        }

        /// <summary>
        /// Traverses the tree using Depth-First-Search(DFS) and does specified action to it's nodes.
        /// </summary>
        /// <param name="node">The node(root) to start from.</param>
        /// <param name="action">The action to be performed to each node.</param>
        public void TraverseUsingDepthFirst(TreeNode<T> node, Action<TreeNode<T>> action)
        {
            if (node == null)
            {
                return;
            }

            action(node);

            foreach (var childNode in node.ChildNodes)
            {
                TraverseUsingDepthFirst(childNode, action);
            }

            // The commented code is an iterative implementation of DFS.

            //Stack<TreeNode<T>> frontier = new Stack<TreeNode<T>>();
            //frontier.Push(node);

            //while (frontier.Count > 0)
            //{
            //    TreeNode<T> currentNode = frontier.Pop();

            //    action(currentNode);

            //    foreach (var childNode in currentNode.ChildNodes)
            //    {
            //        frontier.Push(childNode);
            //    }
            //}
        }

        /// <summary>
        /// Traverses the tree using Breadth-First-Search(BFS) by starting from the root and does specified action to it's nodes.
        /// </summary>
        /// <param name="action">The action to be performed to each node.</param>
        public void TraverseUsingBreathFirst(Action<TreeNode<T>> action)
        {
            this.TraverseUsingBreathFirst(this.Root, action);
        }

        /// <summary>
        /// Traverses the tree using Breadth-First-Search(BFS) and does specified action to it's nodes.
        /// </summary>
        /// <param name="node">The node(root) to start from. </param>
        /// <param name="action">The action to be performed to each node.</param>
        public void TraverseUsingBreathFirst(TreeNode<T> node, Action<TreeNode<T>> action)
        {
            Queue<TreeNode<T>> frontier = new Queue<TreeNode<T>>();
            frontier.Enqueue(node);

            while (frontier.Count > 0)
            {
                TreeNode<T> currentNode = frontier.Dequeue();

                action(currentNode);

                foreach (var childNode in currentNode.ChildNodes)
                {
                    frontier.Enqueue(childNode);
                }
            }
        }

        private void InitializeTree()
        {
            this.TraverseUsingBreathFirst((T) => this.Nodes.Add(T));
        }
    }
}
