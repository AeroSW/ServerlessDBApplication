using System;

namespace BusinessLogic.src.graph
{
    internal class Edge<T,G>: IEdge
    {
        private Node<T> TNode;
        private Node<G> GNode;
        public double? Weight { get; set; }
        public INode this[char letter]
        {
            get {
                if(letter == 'G' || letter == 'g')
                {
                    return GNode;
                }
                else if(letter == 'T' || letter == 't')
                {
                    return TNode;
                }
                throw new IndexOutOfRangeException("The entered value is outside of the edge's range.");
            }
        }
        public Node<H> GetNode<H>(string key)
        {
            if(typeof(H).Name == TNode.Type && TNode.Key.CompareTo(key) == 0)
            {
                return (Node<H>)(INode)TNode;
            }
            else if (typeof(H).Name == GNode.Type && GNode.Key.CompareTo(key) == 0)
            {
                return (Node<H>)(INode)GNode;
            }
            return null;
        }

        public Edge(Node<T> tNode, Node<G> gNode, double? w = null)
        {
            if(tNode == null || gNode == null)
            {
                throw new NullReferenceException("Unable to create edge with endpoint equal to null.");
            }
            this.TNode = tNode;
            this.GNode = gNode;
            this.Weight = w;
        }
        public bool HasKeys(string keyOne, string keyTwo) // Typeless check
        {
            return ((GNode.Key.CompareTo(keyOne) == 0 && TNode.Key.CompareTo(keyTwo) == 0)
                 || (GNode.Key.CompareTo(keyTwo) == 0 && TNode.Key.CompareTo(keyOne) == 0));
        }
        public bool HasKeys<X,Y>(string keyOne, string keyTwo) // Typed Check
        {
            return (typeof(X).Name.CompareTo(TNode.Type) == 0 && typeof(Y).Name.CompareTo(GNode.Type) == 0)
                && (TNode.Key.CompareTo(keyOne) == 0 && GNode.Key.CompareTo(keyTwo) == 0);
        }
        public bool HasKey(string key) // Typeless Check
        {
            return TNode.Key.CompareTo(key) == 0 || GNode.Key.CompareTo(key) == 0;
        }
        public bool HasKey<H>(string key) // Typed Check
        {
            var hType = typeof(H).Name;
            if(hType.CompareTo(TNode.Type) == 0)
            {
                return TNode.Key.CompareTo(key) == 0;
            }
            else if (hType.CompareTo(GNode.Type) == 0)
            {
                return GNode.Key.CompareTo(key) == 0;
            }
            return false;
        }
        public Edge<X,Y> GetSelfTyped<X, Y>()
        {
            var xName = typeof(X).Name;
            var yName = typeof(Y).Name;
            if (xName.CompareTo(TNode.Type) == 0 && yName.CompareTo(GNode.Type) == 0)
            {
                return (Edge<X, Y>)(IEdge)this;
            }
            return null;
        }
    }
}
