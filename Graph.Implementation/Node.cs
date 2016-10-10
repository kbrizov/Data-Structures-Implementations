using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Implementation
{
    public class Node<TNode>
    {
        private TNode value;
        private ICollection<Edge<TNode>> edges;

        public Node(TNode value)
        {
            this.Value = value;
            this.Edges = new HashSet<Edge<TNode>>();
        }

        public TNode Value
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

        public ICollection<Edge<TNode>> Edges
        {
            get
            {
                return this.edges;
            }
            private set
            {
                this.edges = value;
            }
        }

        public bool HasEdges
        {
            get
            {
                return (Edges.Count > 0);
            }
        }

        public void AddEdgeTo(Node<TNode> target, double weight = Constants.DefaultEdgeWeight)
        {
            var sourceToTarget = new Edge<TNode>(this, target, weight);
            if (!this.Edges.Contains(sourceToTarget))
            {
                this.Edges.Add(sourceToTarget);
            }

            var targetToSource = new Edge<TNode>(target, this, weight);
            if (!target.Edges.Contains(targetToSource))
            {
                target.Edges.Add(targetToSource);
            }
        }

        public void RemoveEdgeTo(Node<TNode> target)
        {
            // Removing the edge (s, t) from the set of edges of the current node.
            var sourceToTarget = this.Edges.First(edge => edge.Source.Equals(this) && edge.Target.Equals(target));
            this.Edges.Remove(sourceToTarget);

            // Removing the edge (t, s) from the set of edges of the target node.
            var targetToSource = target.Edges.First(edge => edge.Source.Equals(target) && edge.Target.Equals(this));
            target.Edges.Remove(targetToSource);
        }

        public Edge<TNode> GetEdgeTo(Node<TNode> target)
        {
            var edge = this.Edges.First(e => e.Target.Equals(target));

            return edge;
        }

        public override bool Equals(object obj)
        {
            var node = obj as Node<TNode>;

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
            var hash = this.Value.GetHashCode();

            return hash;
        }

        public override string ToString()
        {
            var result = this.Value.ToString();

            return result;
        }
    }
}
