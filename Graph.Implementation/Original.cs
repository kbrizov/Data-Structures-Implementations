using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.Implementation
{
    public class Program
    {
        public static void Main()
        {
            var graph = new UndirectedGraph<int>();

            graph.AddNode(1);
            graph.AddNode(2);
            graph.AddNode(3);
            graph.AddNode(4);

            graph.AddEdge(1, 2, weight: 10);
            graph.AddEdge(1, 3, weight: 1);
            graph.AddEdge(2, 3, weight: 2);
            graph.AddEdge(3, 4, weight: 3);

            var one = graph.GetNode(1);
            var four = graph.GetNode(4);

            FindShortestPath<int>(one, four);

            //TraverseUsingDepthFirst(one, (TNode) => Console.Write("{0} ", TNode.Value));
            //Console.WriteLine();
            //TraverseUsingBreathFirst(one, (TNode) => Console.Write("{0} ", TNode.Value));
        }

        public static void TraverseUsingDepthFirst<TNode>(Node<TNode> node, Action<Node<TNode>> action)
        {
            var visitedNodes = new HashSet<Node<TNode>>();
            var frontier = new Stack<Node<TNode>>();
            frontier.Push(node);

            while (frontier.Count > 0)
            {
                var currentNode = frontier.Pop();

                visitedNodes.Add(currentNode);
                action(currentNode);

                foreach (var edge in currentNode.Edges)
                {
                    if (!visitedNodes.Contains(edge.Target) && !frontier.Contains(edge.Target))
                    {
                        frontier.Push(edge.Target);
                    }
                }
            }
        }

        public static void TraverseUsingBreathFirst<TNode>(Node<TNode> node, Action<Node<TNode>> action)
        {
            var visitedNodes = new HashSet<Node<TNode>>();
            var frontier = new Queue<Node<TNode>>();
            frontier.Enqueue(node);

            while (frontier.Count > 0)
            {
                var currentNode = frontier.Dequeue();
                visitedNodes.Add(currentNode);

                action(currentNode);

                foreach (var edge in currentNode.Edges)
                {
                    if (!visitedNodes.Contains(edge.Target) && !frontier.Contains(edge.Target))
                    {
                        frontier.Enqueue(edge.Target);
                    }
                }
            }
        }

        public static void FindShortestPath<TNode>(Node<TNode> start, Node<TNode> end)
        {
            
        }
    }
}
