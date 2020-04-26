using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.src.graph
{
    public class Graph<T>: IGraph
    {
        private List<Node<T>> Nodes;
        private List<Edge<T>> Edges;
        private Node<T> FirstNode;
        public Graph() {
            Nodes = new List<Node<T>>();
            Edges = new List<Edge<T>>();
        }
        public Graph<G> get<G>()
        {
            IGraph g = this;
            return (Graph<G>)g;
        }

        public bool AddNode(Node<T> node)
        {
            if (this.Nodes != null && !this.Nodes.Exists(n => ((INode)n).key.Equals(((INode)node).key)))
            {
                this.Nodes.Add(node);
                if(this.FirstNode == null)
                {
                    this.FirstNode = node;
                }
                return true;
            }
            return false;
        }
        public bool AddEdge(string nodeOneKey, string nodeTwoKey, long? weight)
        {
            if (nodeOneKey.Equals(nodeTwoKey)) return false;
            var nodeOne = this.Nodes.FirstOrDefault(node => {
                return ((INode)node).key.Equals(nodeOneKey);
            });
            var nodeTwo = this.Nodes.FirstOrDefault(node => {
                return ((INode)node).key.Equals(nodeTwoKey);
            });
            var flag = !this.HasEdge(nodeOneKey, nodeTwoKey);
            if (flag && nodeOne != null && nodeTwo != null)
            {
                Edges.Add(new Edge<T>(nodeOne, nodeTwo));
                return true;
            }
            return false;
        }
        public bool HasNode(string key)
        {
            return this.Nodes.Exists(node => ((INode)node).key.Equals(key));
        }
        public bool HasEdge(string keyOne, string keyTwo)
        {
            return this.Edges.Exists(edge => edge.HasKeys(keyOne, keyTwo));
        }
        public Path<T> ShortestPath(string endKey)
        {
            return this.Djikstra(endKey);
        }
        private Path<T> Djikstra(string endKey)
        {
            if (!this.HasKey(endKey)) return null;
            if (this.IsFirstKey(endKey)) return new Path<T>(0, new List<Node<T>>() { FirstNode });
            // Declare lists used for finding shortest path.
            var visitedNodes = new List<bool>(this.Nodes.Count); // List of nodes visited.
            var currentPaths = new List<List<Node<T>>>(this.Nodes.Count); // List of shortest paths found.
            var distances = new List<double>(this.Nodes.Count); // List of shortest distances recorded.
            var index = this.Nodes.FindIndex(node => ((INode)node).key.Equals(((INode)this.FirstNode).key)); // Find index of starting node.
            
            // Initialize content of lists with default values.
            for (int ii = 0; ii < visitedNodes.Count; ii++) visitedNodes[ii] = (ii == index) ? true : false; // All should be false excluding starting node.
            for (int ii = 0; ii < distances.Count; ii++) distances[ii] = (ii == index) ? 0 : double.PositiveInfinity; // All should be +infinity excluding starting node.
            for (int ii = 0; ii < currentPaths.Count; ii++) currentPaths[ii] = (ii == index) ? new List<Node<T>>() {FirstNode} : null; // All should be null except path with first node.
            
            // Define variable to flag if we have found our destination.
            bool destination = false;
            // Define list of paths to be iterated over based off the first node.
            var relatedPaths = this.Edges.FindAll(edge => edge.HasKey(((INode)FirstNode).key));

            // Iterate until we find our destination, we have visited all nodes, or we no longer have another path to tread.
            while (!destination && visitedNodes.Exists(b => b == false) && relatedPaths.Count > 0)
            {
                var currEdge = relatedPaths[0]; // Dequeue our first edge to test.
                var nodeOneIndex = this.Nodes.FindIndex(node => ((INode)node).key.Equals(((INode)currEdge[1]).key)); // Get the indices for our keys.
                var nodeTwoIndex = this.Nodes.FindIndex(node => ((INode)node).key.Equals(((INode)currEdge[2]).key));
                var edges = new List<Edge<T>>(); // Form a new list for edges to be pushed onto our priority queue.
                
                // Define local variables for storing which key is the visited key and which key we have yet to visit.
                int? notVisited = null;
                int? visited = null;
                // Set key values using ternary conditionals.
                notVisited = (!visitedNodes[nodeOneIndex] && visitedNodes[nodeTwoIndex]) ? nodeOneIndex : notVisited;
                notVisited = (!visitedNodes[nodeTwoIndex] && visitedNodes[nodeOneIndex]) ? nodeTwoIndex : notVisited;
                visited = (!visitedNodes[nodeOneIndex] && visitedNodes[nodeTwoIndex]) ? nodeTwoIndex : visited;
                visited = (!visitedNodes[nodeTwoIndex] && visitedNodes[nodeOneIndex]) ? nodeOneIndex : visited;
                // If we have not visited one edge and visited the other edge
                if (notVisited.HasValue && visited.HasValue)
                {
                    var distanceTravelled = distances[visited.Value]; // Get total distance from visited index.
                    // Calculate the temp distance based off visited distance and edge weight.
                    var tempDistance = distanceTravelled + ((currEdge.weight.HasValue) ? currEdge.weight.Value : 1);
                    var nodePath = new List<Node<T>>(currentPaths[visited.Value]); // Construct new path based off visited path.
                    nodePath.Add(this.Nodes[notVisited.Value]); // Append newly visited node onto the path.
                    if(tempDistance < distances[notVisited.Value])
                    {
                        currentPaths[notVisited.Value] = nodePath;
                        distances[notVisited.Value] = tempDistance;
                    }
                    destination = ((INode)this.Nodes[notVisited.Value]).key.Equals(endKey);

                    edges.AddRange(
                        (notVisited.Value == nodeOneIndex) ?
                        this.Edges.FindAll(edge => edge.HasKey(((INode)currEdge[1]).key)) :
                        this.Edges.FindAll(edge => edge.HasKey(((INode)currEdge[2]).key))
                    );
                    // Set both edges as visited (ensures we do not revisit either edge in future)
                    visitedNodes[nodeOneIndex] = true;
                    visitedNodes[nodeTwoIndex] = true;
                } // else, we should not worry about visiting either index.
                relatedPaths.RemoveAt(0);
                relatedPaths.AddRange(edges);
                relatedPaths = this.SortEdges(relatedPaths, distances);
            }
            if (destination)
            {
                var resultIndex = this.Nodes.FindIndex(node => ((INode)node).key.Equals(endKey));
                return new Path<T>(distances[resultIndex], currentPaths[resultIndex]);
            }
            return null;
        }
        public bool HasKey(string key)
        {
            return this.Nodes.Exists(node => ((INode)node).key.Equals(key));
        }
        private bool IsFirstKey(string key)
        {
            return ((INode)this.FirstNode).key.Equals(key);
        }
        private List<Edge<T>> SortEdges(List<Edge<T>> edges, List<double> distances)
        {
            List<Edge<T>> sortedList = new List<Edge<T>>(edges);
            sortedList.Sort(delegate (Edge<T> edgeOne, Edge<T> edgeTwo)
            {
                var result = double.PositiveInfinity;
                var edgeOneKeyOneIndex = this.Nodes.FindIndex(node => ((INode)node).key.Equals(((INode)edgeOne[1]).key));
                var edgeOneKeyTwoIndex = this.Nodes.FindIndex(node => ((INode)node).key.Equals(((INode)edgeOne[2]).key));
                var edgeTwoKeyOneIndex = this.Nodes.FindIndex(node => ((INode)node).key.Equals(((INode)edgeTwo[1]).key));
                var edgeTwoKeyTwoIndex = this.Nodes.FindIndex(node => ((INode)node).key.Equals(((INode)edgeTwo[2]).key));
                if(edgeOneKeyOneIndex == edgeTwoKeyOneIndex)
                {
                    result = distances[edgeOneKeyTwoIndex] - distances[edgeTwoKeyTwoIndex];
                }
                else if(edgeOneKeyOneIndex == edgeTwoKeyTwoIndex)
                {
                    result = distances[edgeOneKeyTwoIndex] - distances[edgeTwoKeyOneIndex];
                }
                else if (edgeOneKeyTwoIndex == edgeTwoKeyTwoIndex)
                {
                    result = distances[edgeOneKeyOneIndex] - distances[edgeTwoKeyOneIndex];
                }
                else if (edgeOneKeyTwoIndex == edgeTwoKeyOneIndex)
                {
                    result = distances[edgeOneKeyOneIndex] - distances[edgeTwoKeyTwoIndex];
                }
                else
                {
                    var edgeOneMax = (distances[edgeOneKeyOneIndex] > distances[edgeOneKeyTwoIndex]) ? distances[edgeOneKeyOneIndex] : distances[edgeOneKeyTwoIndex];
                    var edgeTwoMax = (distances[edgeTwoKeyOneIndex] > distances[edgeTwoKeyTwoIndex]) ? distances[edgeTwoKeyOneIndex] : distances[edgeTwoKeyTwoIndex];
                    if (double.IsPositiveInfinity(edgeOneMax) && !double.IsPositiveInfinity(edgeTwoMax)) result = 1;
                    else if (!double.IsPositiveInfinity(edgeTwoMax) && double.IsPositiveInfinity(edgeTwoMax)) result = -1;
                    else if (double.IsPositiveInfinity(edgeOneMax) && double.IsPositiveInfinity(edgeTwoMax)) result = 0;
                    else result = edgeOneMax - edgeTwoMax;
                }
                if(result < 0)
                {
                    return -1;
                }
                else if(result > 0)
                {
                    return 1;
                }
                return 0;
            });
            return sortedList;
        }
    }
}
