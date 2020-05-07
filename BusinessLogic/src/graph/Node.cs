using BusinessLogic.src.models;
using System;
using System.Collections.Generic;

namespace BusinessLogic.src.graph
{
    public class Node<T>: Comparer<Node<T>>, INode
    {
        public string Type { get; }
        public string Key { get; }
        public T Value { get; }
        public Node(string key, T value)
        {
            this.Key = key;
            this.Value = value;
            this.Type = value.GetType().Name;
        }
        public int CompareTo(INode node)
        {
            return StringComparer.CurrentCulture.Compare(this.Key, node.Key);
        }
        public int CompareTo(Node<T> node)
        {
            return StringComparer.CurrentCulture.Compare(this.Key, node.Key);
        }
        public int CompareTo<G>(Node<G> x)
        {
            if(this.GetType().Name != x.GetType().Name)
            {
                var exceptionString = "Unable to compare " + this.GetType().Name + " to " + x.GetType().Name + ".";
                throw new InvalidOperationException(exceptionString);
            }
            if(NumericIdentifier.IsNullableNumeric(x.Value.GetType()) && NumericIdentifier.IsNullableNumeric(this.Value.GetType()))
            {
                var res = Convert.ToDouble(x.Value) - Convert.ToDouble(this.Value);
                if (res < 0) return -1;
                else if (res > 0) return 1;
                else return 0;
            }
            else if(x.Type == "string" && this.Type == "string")
            {
                return Convert.ToString(x.Value).CompareTo(Convert.ToString(this.Value));
            }
            return x.Key.CompareTo(this.Key);
        }
        public override int Compare(Node<T> x, Node<T> y)
        {
            return x.Value.ToString().CompareTo(y.Value.ToString());
        }
    }
}
