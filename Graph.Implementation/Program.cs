using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Implementation
{
    public class Program
    {
        public static void Main()
        {
            var graph = new UndirectedGraph<string>();

            graph.AddNode("a");
            graph.AddNode("b");
            graph.AddNode("c");
            graph.AddNode("d");
            graph.AddNode("e");
            graph.AddNode("f");
            graph.AddNode("g");

            graph.AddEdge("a", "b", weight: 3);
            graph.AddEdge("a", "d", weight: 6);
            graph.AddEdge("a", "c", weight: 5);
            graph.AddEdge("b", "d", weight: 2);
            graph.AddEdge("c", "d", weight: 2);
            graph.AddEdge("c", "f", weight: 3);
            graph.AddEdge("c", "g", weight: 7);
            graph.AddEdge("c", "e", weight: 6);
            graph.AddEdge("d", "f", weight: 9);
            graph.AddEdge("e", "f", weight: 5);
            graph.AddEdge("e", "g", weight: 2);
            graph.AddEdge("f", "g", weight: 1);

            var a = graph.GetNode("a");
            var costs = DijkstraAlgorithm(a, graph);

            foreach (var node in costs)
            {
                Console.WriteLine("{0} -> {1}", node.Key, node.Value);
            }

            var start = graph.GetNode("a");
            var end = graph.GetNode("g");

            var path = FindCheapestPath(start, end, graph);
            DisplayPath(path);

            //var graph = new UndirectedGraph<int>();

            //graph.AddNode(1);
            //graph.AddNode(2);
            //graph.AddNode(3);
            //graph.AddNode(4);
            //graph.AddNode(5);

            //graph.AddEdge(1, 2, weight: 4);
            //graph.AddEdge(1, 4, weight: 8);
            //graph.AddEdge(2, 3, weight: 3);
            //graph.AddEdge(3, 4, weight: 4);
            //graph.AddEdge(4, 5, weight: 7);

            //var start = graph.GetNode(1);
            //var end = graph.GetNode(3);

            //var path = FindCheapestPath(start, end, graph);
            //DisplayPath(path);

            //TraverseUsingDepthFirst(start, (TNode) => Console.Write("{0} ", TNode.Value));
            //Console.WriteLine();
            //TraverseUsingBreathFirst(one, (TNode) => Console.Write("{0} ", TNode.Value));
        }

        public static void TraverseUsingDepthFirst<TNode>(Node<TNode> node, Action<Node<TNode>> action)
        {
            var frontier = new Stack<Node<TNode>>();
            frontier.Push(node);

            var visitedNodes = new HashSet<Node<TNode>>();
            visitedNodes.Add(node);

            while (frontier.Count > 0)
            {
                var currentNode = frontier.Pop();

                action(currentNode);

                foreach (var edge in currentNode.Edges)
                {
                    if (!visitedNodes.Contains(edge.Target))
                    {
                        frontier.Push(edge.Target);
                        visitedNodes.Add(edge.Target);
                    }
                }
            }
        }

        public static void TraverseUsingBreathFirst<TNode>(Node<TNode> node, Action<Node<TNode>> action)
        {
            var frontier = new Queue<Node<TNode>>();
            frontier.Enqueue(node);

            var visitedNodes = new HashSet<Node<TNode>>();
            visitedNodes.Add(node);

            while (frontier.Count > 0)
            {
                var currentNode = frontier.Dequeue();

                action(currentNode);

                foreach (var edge in currentNode.Edges)
                {
                    if (!visitedNodes.Contains(edge.Target))
                    {
                        frontier.Enqueue(edge.Target);
                        visitedNodes.Add(edge.Target);
                    }
                }
            }
        }

        public static Dictionary<Node<TNode>, double> DijkstraAlgorithm<TNode>(Node<TNode> node, UndirectedGraph<TNode> graph)
        {
            var frontier = new Queue<Node<TNode>>();
            frontier.Enqueue(node);

            var visitedNodes = new HashSet<Node<TNode>>();
            visitedNodes.Add(node);

            var costs = InitializeCosts(graph);
            costs[node] = 0d;

            while (frontier.Count > 0)
            {
                var currentNode = frontier.Dequeue();
                var prioritizedEdges = new PriorityQueue<Edge<TNode>>(currentNode.Edges);

                while (prioritizedEdges.Count > 0)
                {
                    var edge = prioritizedEdges.Dequeue();

                    var currentCost = costs[edge.Target];
                    var newCost = costs[currentNode] + edge.Weight;

                    if (newCost < currentCost)
                    {
                        costs[edge.Target] = newCost;
                    }

                    if (!visitedNodes.Contains(edge.Target))
                    {
                        frontier.Enqueue(edge.Target);
                        visitedNodes.Add(edge.Target);
                    }
                }
            }

            return costs;
        }

        /// <summary>
        /// Finds the shortest path with specified start and end using breath first search.
        /// </summary>
        /// <typeparam name="TNode">The value contained in the node.</typeparam>
        /// <param name="start">The start node.</param>
        /// <param name="end">The end node.</param>
        public static IEnumerable<Node<TNode>> FindShortestPath<TNode>(Node<TNode> start, Node<TNode> end)
        {
            var frontier = new Queue<Node<TNode>>();
            frontier.Enqueue(start);

            var visitedNodes = new NodeDictionary<TNode>();
            visitedNodes.Add(start);

            while (frontier.Count > 0)
            {
                var currentNode = frontier.Dequeue();

                if (currentNode.Equals(end))
                {
                    break;
                }

                foreach (var edge in currentNode.Edges)
                {
                    if (!visitedNodes.Contains(edge.Target))
                    {
                        frontier.Enqueue(edge.Target);
                        visitedNodes.Add(edge.Target, currentNode);
                    }
                }
            }

            var path = BacktrackPathTo(end, visitedNodes);

            return path;
        }

        /// <summary>
        /// Finds the cheapest path with specified start and end using the Dijkstra algorithm (AKA Uniform Cost Search).
        /// </summary>
        /// <typeparam name="TNode">The value contained in the node.</typeparam>
        /// <param name="start">The start node.</param>
        /// <param name="end">The end node.</param>
        /// <param name="graph">The graph to operate on.</param>
        public static IEnumerable<Node<TNode>> FindCheapestPath<TNode>(Node<TNode> start, Node<TNode> end, UndirectedGraph<TNode> graph)
        {
            var frontier = new Queue<Node<TNode>>();
            frontier.Enqueue(start);

            var visitedNodes = new NodeDictionary<TNode>();
            visitedNodes.Add(start);

            var costs = InitializeCosts(graph);
            costs[start] = 0d;

            while (frontier.Count > 0)
            {
                var currentNode = frontier.Dequeue();

                if (currentNode.Equals(end))
                {
                    break;
                }

                var prioritizedEdges = new PriorityQueue<Edge<TNode>>(currentNode.Edges);

                while (prioritizedEdges.Count > 0)
                {
                    var edge = prioritizedEdges.Dequeue();

                    var currentCost = costs[edge.Target];
                    var newCost = costs[currentNode] + edge.Weight;

                    if (newCost < currentCost)
                    {
                        costs[edge.Target] = newCost;

                        // A cheaper path is found, so the target node predecesor must be replaced with the current node.
                        if (visitedNodes.Contains(edge.Target))
                        {
                            visitedNodes[edge.Target] = currentNode;
                        }
                    }

                    if (!visitedNodes.Contains(edge.Target))
                    {
                        frontier.Enqueue(edge.Target);
                        visitedNodes.Add(edge.Target, currentNode);
                    }
                }
            }

            var path = BacktrackPathTo(end, visitedNodes);

            return path;
        }

        private static IEnumerable<Node<TNode>> BacktrackPathTo<TNode>(Node<TNode> end, NodeDictionary<TNode> visitedNodes)
        {
            LinkedList<Node<TNode>> path = new LinkedList<Node<TNode>>();

            Node<TNode> current = end;
            Node<TNode> previous = null;

            if (visitedNodes.Contains(end))
            {
                current = end;
                previous = visitedNodes.GetPrevious(current);
            }

            while (previous != null)
            {
                path.AddFirst(current);

                current = previous;
                previous = visitedNodes.GetPrevious(current);
            }

            path.AddFirst(current);

            return path;
        }

        private static Dictionary<Node<TNode>, double> InitializeCosts<TNode>(UndirectedGraph<TNode> graph)
        {
            var costs = new Dictionary<Node<TNode>, double>();

            foreach (var node in graph.Nodes)
            {
                costs.Add(node, double.PositiveInfinity);
            }

            return costs;
        }

        private static void DisplayPath<TNode>(IEnumerable<Node<TNode>> path)
        {
            StringBuilder result = new StringBuilder();

            result.Append("Path: ");

            foreach (var node in path)
            {
                result.Append($"{node.ToString()} ");
            }

            result.AppendLine();

            Console.Write(result);
        }
    }
}
