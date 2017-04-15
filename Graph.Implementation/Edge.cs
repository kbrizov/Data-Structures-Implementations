using System;

namespace Graph.Implementation
{
    public class Edge<TNode> : IComparable<Edge<TNode>>
    {
        private Node<TNode> source;
        private Node<TNode> target;
        private double weight;

        public Edge(Node<TNode> source, Node<TNode> target, double weight = Constants.DefaultEdgeWeight)
        {
            this.Source = source;
            this.Target = target;
            this.Weight = weight;
        }

        public Node<TNode> Source
        {
            get
            {
                return this.source;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("source");
                }

                this.source = value;
            }
        }

        public Node<TNode> Target
        {
            get
            {
                return this.target;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("target");
                }

                this.target = value;
            }
        }

        public double Weight
        {
            get
            {
                return this.weight;
            }
            private set
            {
                if (value < 0d)
                {
                    throw new ArgumentException("The weight must be positive.", "weight");
                }

                this.weight = value;
            }
        }

        public void Reverse()
        {
            var oldSource = this.source;
            var oldTarget = this.target;

            this.source = oldTarget;
            this.target = oldSource;
        }

        public override bool Equals(object obj)
        {
            var node = obj as Edge<TNode>;

            if (node == null)
            {
                return false;
            }

            // The edge is undirected, so edge (s, t) should be equal to (t, s).
            if (Object.Equals(this.Source, node.Target) && 
                Object.Equals(this.Target, node.Source) &&
                this.Weight == node.Weight)
            {
                return true;
            }

            if (Object.Equals(this.Source, node.Source) &&
                Object.Equals(this.Target, node.Target) &&
                this.Weight == node.Weight)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hash = this.Source.GetHashCode() + this.Target.GetHashCode() + this.Weight.GetHashCode();

            return hash;
        }

        public override string ToString()
        {
            var result = string.Format("({0}, {1}): {2}", Source, Target, Weight);

            return result;
        }

        public int CompareTo(Edge<TNode> other)
        {
            if (other == null)
            {
                return 1;
            }

            if (this.weight < other.weight)
            {
                return -1;
            }

            if (this.weight > other.weight)
            {
                return 1;
            }

            return 0;
        }

        public static bool operator >(Edge<TNode> firstOperand, Edge<TNode> secondOperand)
        {
            return firstOperand.CompareTo(secondOperand) == 1;
        }

        public static bool operator <(Edge<TNode> firstOperand, Edge<TNode> secondOperand)
        {
            return firstOperand.CompareTo(secondOperand) == -1;
        }

        public static bool operator >=(Edge<TNode> firstOperand, Edge<TNode> secondOperand)
        {
            return firstOperand.CompareTo(secondOperand) >= 0;
        }

        public static bool operator <=(Edge<TNode> firstOperand, Edge<TNode> secondOperand)
        {
            return firstOperand.CompareTo(secondOperand) <= 0;
        }
    }
}
