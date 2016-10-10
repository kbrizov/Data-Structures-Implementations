using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Implementation
{
    public class UndirectedGraph<TNode>
    {
        private ICollection<Node<TNode>> nodes;
        private ICollection<Edge<TNode>> edges;

        public UndirectedGraph() 
            : this(new HashSet<Node<TNode>>(), new HashSet<Edge<TNode>>())
        {
        }

        public UndirectedGraph(ICollection<Node<TNode>> nodes, ICollection<Edge<TNode>> edges)
        {
            this.Nodes = nodes;
            this.Edges = edges;
        }

        public ICollection<Node<TNode>> Nodes
        {
            get
            {
                return this.nodes;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("nodes");
                }

                this.nodes = value;
            }
        }

        public ICollection<Edge<TNode>> Edges
        {
            get
            {
                return this.edges;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("edges");
                }

                this.edges = value;
            }
        }

        public void AddNode(TNode value)
        {
            var node = new Node<TNode>(value);
            this.AddNode(node);
        }

        public void AddNode(Node<TNode> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            if (Nodes.Contains(node))
            {
                return;
            }

            this.Nodes.Add(node);

            if (node.HasEdges)
            {
                foreach (var edge in node.Edges)
                {
                    if (!this.Edges.Contains(edge))
                    {
                        this.Edges.Add(edge);
                    }

                    AddNode(edge.Target);
                }
            }
        }

        public void RemoveNode(TNode value)
        {
            var node = this.Nodes.First(n => n.Value.Equals(value));
            this.RemoveNode(node);
        }

        public void RemoveNode(Node<TNode> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            foreach (var edge in node.Edges)
            {
                node.RemoveEdgeTo(edge.Target);
            }

            this.Nodes.Remove(node);
        }

        public Node<TNode> GetNode(TNode value)
        {
            var node = this.Nodes.First(n => n.Value.Equals(value));

            return node;
        }

        public void AddEdge(TNode sourceValue, TNode targetValue, double weight = Constants.DefaultEdgeWeight)
        {
            var sourceNode = this.Nodes.First(node => node.Value.Equals(sourceValue));
            var targetNode = this.Nodes.First(node => node.Value.Equals(targetValue));

            this.AddEdge(sourceNode, targetNode, weight);
        }

        public void AddEdge(Node<TNode> source, Node<TNode> target, double weight = Constants.DefaultEdgeWeight)
        {
            ValidateSourceAndTargetNodes(source, target);
            source.AddEdgeTo(target, weight);
            var edge = source.GetEdgeTo(target);
            this.Edges.Add(edge);
        }

        public void RemoveEdge(TNode sourceValue, TNode targetValue)
        {
            var sourceNode = this.Nodes.First(node => node.Value.Equals(sourceValue));
            var targetNode = this.Nodes.First(node => node.Value.Equals(targetValue));

            this.RemoveEdge(sourceNode, targetNode);
        }

        public void RemoveEdge(Node<TNode> source, Node<TNode> target)
        {
            ValidateSourceAndTargetNodes(source, target);
            var edge = source.GetEdgeTo(target);
            this.Edges.Remove(edge);
            source.RemoveEdgeTo(target);
        }

        private void ValidateSourceAndTargetNodes(Node<TNode> source, Node<TNode> target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            if (source.Equals(target))
            {
                throw new ArgumentException("The source node cannot be the same as the target node.");
            }

            if (!Nodes.Contains(source))
            {
                throw new ArgumentException("The graph doesn't contain the source node.");
            }

            if (!Nodes.Contains(target))
            {
                throw new ArgumentException("The graph doesn't contain the target node.");
            }
        }
    }
}
