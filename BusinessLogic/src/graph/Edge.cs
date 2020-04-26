using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.src.graph
{
    public class Edge<T>
    {
        private Node<T>[] Nodes;
        public Node<T> this[int index]
        {
            get {
                if(1 <= index && index <= 2)
                    return this.Nodes[index - 1];
                throw new IndexOutOfRangeException("The entered value is outside of the edge's range.");
            }
        }
        public Node<T> this[string key]
        {
            get
            {
                if(((INode)Nodes[0]).key.CompareTo(key) == 0)
                {
                    return Nodes[0];
                }
                else if(((INode)Nodes[1]).key.CompareTo(key) == 0)
                {
                    return Nodes[1];
                }
                return null;
            }
        }
        public double? weight { get; }

        public Edge(Node<T> n1, Node<T> n2, double? w = null)
        {
            this.Nodes = new Node<T>[2];
            this.Nodes[0] = n1;
            this.Nodes[1] = n2;
            this.weight = w;
        }
        public bool HasKeys(string keyOne, string keyTwo)
        {
            var hasNodes = (((INode)this.Nodes[0]).key.Equals(keyOne) && ((INode)this.Nodes[1]).key.Equals(keyTwo));
            hasNodes = hasNodes || (((INode)this.Nodes[1]).key.Equals(keyOne) && ((INode)this.Nodes[0]).key.Equals(keyTwo));
            return hasNodes;
        }
        public bool HasKey(string key)
        {
            return (((INode)this.Nodes[0]).key.Equals(key) || ((INode)this.Nodes[1]).key.Equals(key));
        }
    }
}
