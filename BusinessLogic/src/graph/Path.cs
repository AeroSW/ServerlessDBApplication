using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.src.graph
{
    public class Path<T>
    {
        public double Distance { get; }
        public List<Node<T>> Nodes { get; }
        public Path(double distance, List<Node<T>> nodes)
        {
            this.Distance = distance;
            this.Nodes = nodes;
        }
    }
}
