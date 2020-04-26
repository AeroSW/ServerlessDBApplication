using System;
using System.Collections.Generic;

namespace BusinessLogic.src.graph
{
    public class Node<T>: Comparer<Node<T>>, INode
    {
        public string key { get; }
        public T value { get; }
        public Node(string key, T value)
        {
            this.key = key;
            this.value = value;
        }
        public int CompareTo(INode node)
        {
            return StringComparer.CurrentCulture.Compare(this.key, node.key);
        }
        public int CompareTo(Node<T> node)
        {
            return StringComparer.CurrentCulture.Compare(this.key, node.key);
        }

        public override int Compare(Node<T> x, Node<T> y)
        {
            return x.value.ToString().CompareTo(y.value.ToString());
        }
    }
}
