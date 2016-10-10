using System.Collections.Generic;

namespace Graph.Implementation
{
    internal class NodeDictionary<TNode>
    {
        private Dictionary<Node<TNode>, Node<TNode>> dictionary;

        public NodeDictionary()
        {
            this.dictionary = new Dictionary<Node<TNode>, Node<TNode>>();
        }

        public Node<TNode> this[Node<TNode> node]
        {
            get
            {
                return this.dictionary[node];
            }
            set
            {
                this.dictionary[node] = value;
            }
        }

        public IEnumerable<Node<TNode>> Nodes
        {
            get
            {
                return this.dictionary.Keys;
            }
        }

        public void Add(Node<TNode> node, Node<TNode> previous = null)
        {
            this.dictionary.Add(node, previous);
        }

        public bool Remove(Node<TNode> node)
        {
            bool isRemoved = this.dictionary.Remove(node);

            return isRemoved;
        }

        public bool Contains(Node<TNode> node)
        {
            bool isNodeContained = this.dictionary.ContainsKey(node);

            return isNodeContained;
        }

        public Node<TNode> GetPrevious(Node<TNode> node)
        {
            return this[node];
        }
    }
}
