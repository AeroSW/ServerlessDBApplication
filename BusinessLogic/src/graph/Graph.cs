using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.src.graph
{
    internal class Graph
    {
        internal List<INode> Nodes { get; set; }
        internal List<IEdge> Edges { get; set; }
        internal INode FirstNode;

        internal Graph()
        {
            Nodes = new List<INode>();
            Edges = new List<IEdge>();
        }
        internal bool AddNode<T>(Node<T> node)
        {
            if (this.HasKey<T>(node.Key)) return false;
            this.Nodes.Add(node);
            if (this.FirstNode == null) this.FirstNode = node;
            System.Console.WriteLine("Nodes: {0}", this.Nodes.Count);
            return true;
        }
        internal bool AddEdge<T, G>(string tKey, string gKey, double? weight = null)
        {
            if (this.HasKey<T>(tKey) && this.HasKey<G>(gKey)) {
                Node<T> tNode = this.GetNode<T>(tKey);
                Node<G> gNode = this.GetNode<G>(gKey);
                if (Object.ReferenceEquals(tNode, gNode)) return false; // No circular dependencies allowed.
                var exists = Edges.Any(iEdge => { // Graph is bidirectional.
                    return iEdge.HasKeys<T, G>(tKey, gKey)
                        || iEdge.HasKeys<G, T>(gKey, tKey);
                });
                // If an edge of any combination does not exist.
                if (!exists) {
                    var edge = new Edge<T, G>(tNode, gNode, weight);
                    this.Edges.Add(edge);
                    System.Console.WriteLine("Edges: {0}", this.Edges.Count);
                    return true;
                }
            }
            return false;
        }
        internal bool HasKey(string k) 
        {
            return this.Nodes.Exists(n => n.Key.CompareTo(k) == 0); // Very loose
        }
        internal bool HasKey<T>(string key)
        {
            Type t = typeof(T);
            // Retrieve list of Nodes where the type matches T.
            var tNodes = this.Nodes.Where(n => n.Type == t.Name);
            // Retrieve first Node<T> or null for the given key.
            var kNode = tNodes.Where(n => n.Key == key).FirstOrDefault();
            return (kNode != null) ? true : false; // Type dependent
        }
        internal bool HasEdge<T,G>(string tKey, string gKey)
        {
            bool res = this.Edges.Exists(edge => {
                return edge.HasKeys<T, G>(tKey, gKey);
            });
            res = this.Edges.Exists(edge => {
                return edge.HasKeys<G, T>(gKey, tKey);
            }) || res;
            return res;
        }
        internal bool SetFirstNode<T>(string key)
        {
            if (this.HasKey<T>(key))
            {
                this.FirstNode = this.GetNode<T>(key);
                return true;
            }
            return false;
        }
        internal Node<T> GetNode<T>(string key)
        {
            Type t = typeof(T);
            // Retrieve list of Nodes where the type matches T.
            var tNodes = this.Nodes.Where(n => n.Type == t.Name);
            // Retrieve first Node<T> or null for the given key.
            var kNode = tNodes.Where(n => n.Key == key).FirstOrDefault();
            return (kNode != null) ? (Node<T>)kNode : null;
        }
        internal Path ShortestPath(string searchKey) {
            return Djikstra<object>(searchKey);
        }
        internal Path ShortestPath<T>(string searchKey) {
            var tName = typeof(T).Name;
            return Djikstra<T>(searchKey, tName);
        }
        private Path Djikstra<T>(string searchKey, string nodeType = null)
        {
            if ((nodeType == null && !this.HasKey(searchKey)) || (nodeType != null && !this.HasKey<T>(searchKey))) return null;
            if (this.FirstNode.Key.CompareTo(searchKey) == 0) return new Path(0, new List<INode>() { this.FirstNode });
            // Declare lists used for finding shortest path.
            var visitedNodes = new List<bool>(this.Nodes.Count); // List of nodes visited.
            var currentPaths = new List<List<INode>>(this.Nodes.Count); // List of shortest paths found.
            var distances = new List<double>(this.Nodes.Count); // List of shortest distances recorded.
            var index = this.Nodes.FindIndex(node => ((INode)node).Key.Equals(((INode)this.FirstNode).Key)); // Find index of starting node.

            // Initialize content of lists with default values.
            for (int ii = 0; ii < visitedNodes.Count; ii++) visitedNodes[ii] = (ii == index) ? true : false; // All should be false excluding starting node.
            for (int ii = 0; ii < distances.Count; ii++) distances[ii] = (ii == index) ? 0 : double.PositiveInfinity; // All should be +infinity excluding starting node.
            for (int ii = 0; ii < currentPaths.Count; ii++) currentPaths[ii] = (ii == index) ? new List<INode>() { FirstNode } : null; // All should be null except path with first node.

            // Define variable to flag if we have found our destination.
            bool destination = false;
            // Define list of paths to be iterated over based off the first node.

            var relatedPaths = this.Edges.FindAll(edge => edge.HasKey(FirstNode.Key));

            // Iterate until we find our destination, we have visited all nodes, or we no longer have another path to tread.
            while (!destination && visitedNodes.Exists(b => b == false) && relatedPaths.Count > 0)
            {
                var currEdge = relatedPaths[0]; // Dequeue our first edge to test.
                var nodeOneIndex = this.Nodes.FindIndex(node => (node.Key.Equals(currEdge['T'].Key))); // Get the indices for our Keys.
                var nodeTwoIndex = this.Nodes.FindIndex(node => (node.Key.Equals(currEdge['G'].Key)));
                var edges = new List<IEdge>(); // Form a new list for edges to be pushed onto our priority queue.

                // Define local variables for storing which Key is the visited Key and which Key we have yet to visit.
                int? notVisited = null;
                int? visited = null;
                // Set Key values using ternary conditionals.
                notVisited = (!visitedNodes[nodeOneIndex] && visitedNodes[nodeTwoIndex]) ? nodeOneIndex : notVisited;
                notVisited = (!visitedNodes[nodeTwoIndex] && visitedNodes[nodeOneIndex]) ? nodeTwoIndex : notVisited;
                visited = (!visitedNodes[nodeOneIndex] && visitedNodes[nodeTwoIndex]) ? nodeTwoIndex : visited;
                visited = (!visitedNodes[nodeTwoIndex] && visitedNodes[nodeOneIndex]) ? nodeOneIndex : visited;
                // If we have not visited one edge and visited the other edge
                if (notVisited.HasValue && visited.HasValue)
                {
                    var distanceTravelled = distances[visited.Value]; // Get total distance from visited index.
                    // Calculate the temp distance based off visited distance and edge weight.
                    var tempDistance = distanceTravelled + ((currEdge.Weight.HasValue) ? currEdge.Weight.Value : 1);
                    var nodePath = new List<INode>(currentPaths[visited.Value]); // Construct new path based off visited path.
                    nodePath.Add(this.Nodes[notVisited.Value]); // Append newly visited node onto the path.
                    if (tempDistance < distances[notVisited.Value])
                    {
                        currentPaths[notVisited.Value] = nodePath;
                        distances[notVisited.Value] = tempDistance;
                    }

                    if (nodeType != null)
                    {
                        destination = this.Nodes[notVisited.Value].Key.Equals(searchKey) && this.Nodes[notVisited.Value].Type == nodeType;
                    }
                    else
                    {
                        destination = ((INode)this.Nodes[notVisited.Value]).Key.Equals(searchKey);

                    }

                    edges.AddRange(
                        (notVisited.Value == nodeOneIndex) ?
                        this.Edges.FindAll(edge => edge.HasKey(currEdge['T'].Key)) :
                        this.Edges.FindAll(edge => edge.HasKey(currEdge['G'].Key))
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
                var resultIndex = this.Nodes.FindIndex(node => ((INode)node).Key.Equals(searchKey));
                return new Path(distances[resultIndex], currentPaths[resultIndex]);
            }
            return null;
        }

        private List<IEdge> SortEdges(List<IEdge> edges, List<double> distances)
        {
            List<IEdge> sortedList = new List<IEdge>(edges);
            sortedList.Sort((IEdge edgeOne, IEdge edgeTwo) =>
            {
                var result = double.PositiveInfinity;
                var edgeOneKeyOneIndex = this.Nodes.FindIndex(node => node.Key.Equals(edgeOne['T'].Key));
                var edgeOneKeyTwoIndex = this.Nodes.FindIndex(node => node.Key.Equals(edgeOne['G'].Key));
                var edgeTwoKeyOneIndex = this.Nodes.FindIndex(node => node.Key.Equals(edgeTwo['T'].Key));
                var edgeTwoKeyTwoIndex = this.Nodes.FindIndex(node => node.Key.Equals(edgeTwo['G'].Key));
                if (edgeOneKeyOneIndex == edgeTwoKeyOneIndex)
                {
                    result = distances[edgeOneKeyTwoIndex] - distances[edgeTwoKeyTwoIndex];
                }
                else if (edgeOneKeyOneIndex == edgeTwoKeyTwoIndex)
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
                if (result < 0)
                {
                    return -1;
                }
                else if (result > 0)
                {
                    return 1;
                }
                return 0;
            });
            return sortedList;
        }
    }
}
